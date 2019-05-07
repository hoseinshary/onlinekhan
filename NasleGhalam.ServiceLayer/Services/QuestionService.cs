using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.Office.Interop.Word;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Question;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionService
    {
        private const string Title = "سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Question> _questions;

        public QuestionService(IUnitOfWork uow)
        {
            _uow = uow;
            _questions = uow.Set<Question>();
        }

        /// <summary>
        /// گرفتن  سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionViewModel GetById(int id)
        {
            return _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .FirstOrDefault();
        }

        public object GetAllByTopicIdsNoJudge(IEnumerable<int> ids, int userid)
        {
            return _questions
                .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                .Where(current => current.QuestionJudges.Any(x => x.UserId != userid) || !current.QuestionJudges.Any())
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه سوال های مباحث
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllByTopicIds(IEnumerable<int> ids)
        {
            return _questions
                .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }

        public object GetAllByLessonId(int id)
        {
            return _questions
                .Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه سوال های سوال گروهی
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllByQuestionGroupId(int id)
        {
            return _questions
                .Include(current => current.QuestionAnswers.OrderBy(y => y.IsMaster))
                .Where(current => current.QuestionGroups.Any(x => x.Id == id))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }


        /// <summary>
        /// گرفتن همه سوال های سوال گروهی
        /// </summary>
        /// <returns></returns>
        public IList<Question> GetAllQuestionsByQuestionGroupId(int id)
        {
            return _questions
                .Include(current => current.QuestionAnswers)
                .Where(current => current.QuestionGroups.Any(x => x.Id == id))
                .AsNoTracking()
                .AsEnumerable()
                .ToList();
        }


        /// <summary>
        /// ثبت سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult Create(QuestionCreateViewModel questionViewModel, HttpPostedFile word)
        {
            var question = Mapper.Map<Question>(questionViewModel);

            //save Doc file in temp memory
            word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx");

            // Open a doc file.
            var app = new Application();
            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
            var doc = app.Documents.Open(wordFilename);

            foreach (Paragraph paragraph in doc.Paragraphs)
            {
                question.Context += paragraph.Range.Text;
            }

            //تبدیل به عکس
            var pane = doc.Windows[1].Panes[1];
            var page = pane.Pages[1];
            var bits = page.EnhMetaFileBits;
            var target = SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".png";

            doc.Close();
            app.Quit();

            foreach (var topicId in questionViewModel.TopicIds)
            {
                var topic = new Topic() { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                question.Topics.Add(topic);
            }

            foreach (var tagId in questionViewModel.TagIds)
            {
                var tag = new Tag() { Id = tagId };
                _uow.MarkAsUnChanged(tag);
                question.Tags.Add(tag);
            }

            foreach (var option in questionViewModel.Options)
            {
                var newOption = new QuestionOption()
                {
                    Context = option.Context,
                    IsAnswer = option.IsAnswer,
                };

                question.QuestionOptions.Add(newOption);
            }

            _questions.Add(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName))
            {
                word.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx");
                //crop and resize
                try
                {
                    using (var ms = new MemoryStream((byte[])(bits)))
                    {
                        var image = Image.FromStream(ms);
                        var pngTarget = target; //Path.ChangeExtension(target , "png");
                        image.Save(pngTarget + "1.png", ImageFormat.Png);
                        image = new Bitmap(pngTarget + "1.png");

                        var resizedImage = ImageUtility.GetImageWithRatioSize(image, 1 / 5d, 1 / 5d);
                        // resizedImage.Save(pngTarget, ImageFormat.Png);
                        var rectangle = ImageUtility.GetCropArea(resizedImage, 10);
                        var croppedImage = ImageUtility.CropImage(resizedImage, rectangle);
                        croppedImage.Save(pngTarget, ImageFormat.Png);
                        croppedImage.Dispose();
                        File.Delete(pngTarget + "1.png");
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }

                File.Delete(wordFilename);

            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }

        /// <summary>
        /// ویرایش سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult Update(QuestionUpdateViewModel questionViewModel, HttpPostedFile word)
        {
            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .First(current => current.Id == questionViewModel.Id);

            var previousFileName = questionViewModel.FileName;
            questionViewModel.FileName = Guid.NewGuid().ToString();
            var target = "";
            dynamic bits = null;
            var haveFileUpdate = false;
            var wordFilename = "";
            if (word != null && word.ContentLength > 0)
            {
                haveFileUpdate = true;

                //save Doc file in temp memory
                word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx");

                // Open a doc file.
                var app = new Application();
                wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
                var doc = app.Documents.Open(wordFilename);

                question.Context = "";
                foreach (Paragraph paragraph in doc.Paragraphs)
                {
                    question.Context += paragraph.Range.Text;
                }

                //تبدیل به عکس
                var pane = doc.Windows[1].Panes[1];
                var page = pane.Pages[1];
                bits = page.EnhMetaFileBits;
                target = SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".png";

                doc.Close();
                app.Quit();
            }

            question.AuthorName = questionViewModel.AuthorName;
            question.Description = questionViewModel.Description;
            question.InsertDateTime = DateTime.Now;
            question.IsActive = questionViewModel.IsActive;
            question.IsStandard = questionViewModel.IsStandard;
            question.LookupId_AreaType = questionViewModel.LookupId_AreaType;
            question.LookupId_AuthorType = questionViewModel.LookupId_AuthorType;
            question.LookupId_QuestionHardnessType = questionViewModel.LookupId_QuestionHardnessType;
            question.LookupId_QuestionType = questionViewModel.LookupId_QuestionType;
            question.LookupId_RepeatnessType = questionViewModel.LookupId_RepeatnessType;
            question.QuestionNumber = questionViewModel.QuestionNumber;
            question.QuestionPoint = questionViewModel.QuestionPoint;
            question.ResponseSecond = questionViewModel.ResponseSecond;
            question.UseEvaluation = questionViewModel.UseEvaluation;
            question.AnswerNumber = questionViewModel.AnswerNumber;

            //delete topics
            var deleteTopicList = question.Topics
                .Where(oldTopic => questionViewModel.TopicIds.All(newTopicId => newTopicId != oldTopic.Id))
                .ToList();
            foreach (var topic in deleteTopicList)
            {
                question.Topics.Remove(topic);
            }

            //add topics
            var addTopicList = questionViewModel.TopicIds
                .Where(oldTopicId => question.Topics.All(newTopic => newTopic.Id != oldTopicId))
                .ToList();
            foreach (var topicId in addTopicList)
            {
                var topic = new Topic { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                question.Topics.Add(topic);
            }

            //delete tag
            var deleteTagList = question.Tags
                .Where(oldTag => questionViewModel.TagIds.All(newTagId => newTagId != oldTag.Id))
                .ToList();
            foreach (var tag in deleteTagList)
            {
                question.Tags.Remove(tag);
            }

            //add tag
            var addTagList = questionViewModel.TagIds
                .Where(oldTagId => question.Tags.All(newTag => newTag.Id != oldTagId))
                .ToList();
            foreach (var tagId in addTagList)
            {
                var tag = new Tag { Id = tagId };
                _uow.MarkAsUnChanged(tag);
                question.Tags.Add(tag);
            }

            _uow.MarkAsChanged(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName) && haveFileUpdate)
            {
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".docx"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".docx");
                }
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".png"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".png");
                }

                word.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx");
                //crop and resize
                try
                {
                    using (var ms = new MemoryStream((byte[])(bits)))
                    {
                        var image = Image.FromStream(ms);
                        var pngTarget = target; //Path.ChangeExtension(target , "png");
                        image.Save(pngTarget + "1.png", ImageFormat.Png);
                        image = new Bitmap(pngTarget + "1.png");

                        var resizedImage = ImageUtility.GetImageWithRatioSize(image, 1 / 5d, 1 / 5d);
                        // resizedImage.Save(pngTarget, ImageFormat.Png);
                        var rectangle = ImageUtility.GetCropArea(resizedImage, 10);
                        var croppedImage = ImageUtility.CropImage(resizedImage, rectangle);
                        croppedImage.Save(pngTarget, ImageFormat.Png);
                        croppedImage.Dispose();
                        File.Delete(pngTarget + "1.png");
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }

                File.Delete(wordFilename);
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }

        /// <summary>
        /// ویرایش سوال بعد از ورود گروهی سوال 
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult UpdateImport(QuestionUpdateImportViewModel questionViewModel, HttpPostedFile word)
        {
            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .First(current => current.Id == questionViewModel.Id);

            var previousFileName = questionViewModel.FileName;
            questionViewModel.FileName = Guid.NewGuid().ToString();
            var target = "";
            dynamic bits = null;
            var haveFileUpdate = false;
            var wordFilename = "";
            if (word != null && word.ContentLength > 0)
            {
                haveFileUpdate = true;

                //save Doc file in temp memory
                word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx");

                // Open a doc file.
                var app = new Application();
                wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
                var doc = app.Documents.Open(wordFilename);

                question.Context = "";
                foreach (Paragraph paragraph in doc.Paragraphs)
                {
                    question.Context += paragraph.Range.Text;
                }

                //تبدیل به عکس
                var pane = doc.Windows[1].Panes[1];
                var page = pane.Pages[1];
                bits = page.EnhMetaFileBits;
                target = SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".png";

                doc.Close();
                app.Quit();

            }
            question.AuthorName = questionViewModel.AuthorName;
            question.Description = questionViewModel.Description;
            question.LookupId_AuthorType = questionViewModel.LookupId_AuthorType;
            question.LookupId_QuestionType = questionViewModel.LookupId_QuestionType;
            question.QuestionNumber = questionViewModel.QuestionNumber;
            question.AnswerNumber = questionViewModel.AnswerNumber;

            //delete tag
            var deleteTagList = question.Tags
                .Where(oldTag => questionViewModel.TagIds.All(newTagId => newTagId != oldTag.Id))
                .ToList();
            foreach (var tag in deleteTagList)
            {
                question.Tags.Remove(tag);
            }

            //add tag
            var addTagList = questionViewModel.TagIds
                .Where(oldTagId => question.Tags.All(newTag => newTag.Id != oldTagId))
                .ToList();
            foreach (var tagId in addTagList)
            {
                var tag = new Tag { Id = tagId };
                _uow.MarkAsUnChanged(tag);
                question.Tags.Add(tag);
            }

            _uow.MarkAsChanged(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName) && haveFileUpdate)
            {
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".docx"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".docx");
                }
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".png"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".png");
                }

                word.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx");
                //crop and resize
                try
                {
                    using (var ms = new MemoryStream((byte[])(bits)))
                    {
                        var image = Image.FromStream(ms);
                        var pngTarget = target; //Path.ChangeExtension(target , "png");
                        image.Save(pngTarget + "1.png", ImageFormat.Png);
                        image = new Bitmap(pngTarget + "1.png");

                        var resizedImage = ImageUtility.GetImageWithRatioSize(image, 1 / 5d, 1 / 5d);
                        // resizedImage.Save(pngTarget, ImageFormat.Png);
                        var rectangle = ImageUtility.GetCropArea(resizedImage, 10);
                        var croppedImage = ImageUtility.CropImage(resizedImage, rectangle);
                        croppedImage.Save(pngTarget, ImageFormat.Png);
                        croppedImage.Dispose();
                        File.Delete(pngTarget + "1.png");
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }

                File.Delete(wordFilename);
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }

        /// <summary>
        /// ویرایش سوال انتساب مبحث
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult UpdateTopic(QuestionUpdateTopicViewModel questionViewModel, HttpPostedFile word)
        {
            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .First(current => current.Id == questionViewModel.Id);

            var previousFileName = questionViewModel.FileName;
            questionViewModel.FileName = Guid.NewGuid().ToString();
            var target = "";
            dynamic bits = null;
            var haveFileUpdate = false;
            var wordFilename = "";
            if (word != null && word.ContentLength > 0)
            {
                haveFileUpdate = true;

                //save Doc file in temp memory
                word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx");

                // Open a doc file.
                var app = new Application();
                wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
                var doc = app.Documents.Open(wordFilename);

                question.Context = "";
                foreach (Paragraph paragraph in doc.Paragraphs)
                {
                    question.Context += paragraph.Range.Text;
                }

                //تبدیل به عکس
                var pane = doc.Windows[1].Panes[1];
                var page = pane.Pages[1];
                bits = page.EnhMetaFileBits;
                target = SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".png";

                doc.Close();
                app.Quit();
            }

            question.LookupId_AreaType = questionViewModel.LookupId_AreaType;
            question.AnswerNumber = questionViewModel.AnswerNumber;

            //delete topics
            var deleteTopicList = question.Topics
                .Where(oldTopic => questionViewModel.TopicIds.All(newTopicId => newTopicId != oldTopic.Id))
                .ToList();
            foreach (var topic in deleteTopicList)
            {
                question.Topics.Remove(topic);
            }

            //add topics
            var addTopicList = questionViewModel.TopicIds
                .Where(oldTopicId => question.Topics.All(newTopic => newTopic.Id != oldTopicId))
                .ToList();
            foreach (var topicId in addTopicList)
            {
                var topic = new Topic { Id = topicId };
                _uow.MarkAsUnChanged(topic);
                question.Topics.Add(topic);
            }

            //delete tag
            var deleteTagList = question.Tags
                .Where(oldTag => questionViewModel.TagIds.All(newTagId => newTagId != oldTag.Id))
                .ToList();
            foreach (var tag in deleteTagList)
            {
                question.Tags.Remove(tag);
            }

            //add tag
            var addTagList = questionViewModel.TagIds
                .Where(oldTagId => question.Tags.All(newTag => newTag.Id != oldTagId))
                .ToList();
            foreach (var tagId in addTagList)
            {
                var tag = new Tag { Id = tagId };
                _uow.MarkAsUnChanged(tag);
                question.Tags.Add(tag);
            }

            _uow.MarkAsChanged(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName) && haveFileUpdate)
            {
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".docx"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".docx");
                }
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".png"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".png");
                }

                word.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx");
                //crop and resize
                try
                {
                    using (var ms = new MemoryStream((byte[])(bits)))
                    {
                        var image = Image.FromStream(ms);
                        var pngTarget = target; //Path.ChangeExtension(target , "png");
                        image.Save(pngTarget + "1.png", ImageFormat.Png);
                        image = new Bitmap(pngTarget + "1.png");

                        var resizedImage = ImageUtility.GetImageWithRatioSize(image, 1 / 5d, 1 / 5d);
                        // resizedImage.Save(pngTarget, ImageFormat.Png);
                        var rectangle = ImageUtility.GetCropArea(resizedImage, 10);
                        var croppedImage = ImageUtility.CropImage(resizedImage, rectangle);
                        croppedImage.Save(pngTarget, ImageFormat.Png);
                        croppedImage.Dispose();
                        File.Delete(pngTarget + "1.png");
                    }
                }
                catch (Exception ex)
                {
                    Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                }

                File.Delete(wordFilename);
            }

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }

        /// <summary>
        /// حذف سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var question = _questions
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .Include(current => current.QuestionOptions)
                .First(current => current.Id == id);

            if (question == null)
                return ClientMessageResult.NotFound();

            //remove topics
            var topics = question.Topics.ToList();
            foreach (var item in topics)
            {
                question.Topics.Remove(item);
            }

            //remove tags
            var tags = question.Tags.ToList();
            foreach (var item in tags)
            {
                question.Tags.Remove(item);
            }

            //remove options
            var options = question.QuestionOptions.ToList();
            foreach (var item in options)
            {
                _uow.MarkAsDeleted(item);
            }

            _uow.MarkAsDeleted(question);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            if (msgRes.MessageType == MessageType.Success)
            {
                File.Delete(SitePath.GetQuestionAbsPath(question.FileName) + ".docx");
                File.Delete(SitePath.GetQuestionAbsPath(question.FileName) + ".png");
            }

            return Mapper.Map<ClientMessageResult>(msgRes);
        }

        public static implicit operator Lazy<object>(QuestionService v)
        {
            throw new NotImplementedException();
        }
    }
}
