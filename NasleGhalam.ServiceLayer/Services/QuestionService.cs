using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.Office.Interop.Word;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.ViewModels.Report;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionService
    {
        private const string Title = "سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Question> _questions;
        private readonly IDbSet<User> _users;
        private readonly IDbSet<QuestionJudge> _questionJudges;
        private readonly IDbSet<QuestionAnswerJudge> _questionAnswerJudges;

        private readonly Lazy<QuestionGroupService> _questionGroupService;

        public QuestionService(IUnitOfWork uow, Lazy<QuestionGroupService> questionGroupService)
        {
            _uow = uow;
            _questions = uow.Set<Question>();
            _questionJudges = uow.Set<QuestionJudge>();
            _questionAnswerJudges = uow.Set<QuestionAnswerJudge>();
            _users = uow.Set<User>();
            _questionGroupService = questionGroupService;
        }

        /// <summary>
        /// گرفتن  سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionViewModel GetById(int id)
        {
            //var question = _questions
            //    .Include(current => current.QuestionOptions)
            //    .Include(current => current.Topics)
            //    .Include(current => current.Tags)
            //    .Include(current => current.Lookup_AreaType)
            //    .Include(current => current.Writer)
            //    .Include(current => current.Supervisors)
            //    .Where(current => current.Id == id)
            //    .AsNoTracking()
            //    .AsEnumerable()
            //    .FirstOrDefault();
            return _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .Include(current => current.Lookup_AreaTypes)
                .Include(current => current.Writer)
                .Include(current => current.Supervisors)
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .FirstOrDefault();

            //returnVal.TopicAnswer.Clear();
            //var tempStringTopicAnswer = question.TopicAnswer.Split(',');
            //foreach (var item in tempStringTopicAnswer)
            //{
            //    returnVal.TopicAnswer.Add(item);
            //}

            //return returnVal;
        }

        public IList<AllUsersReporQuestionViewModel> GetAllUsersReport()
        {


            return _users.Where(x => x.Role.Level == 3)
                .Select(x => new AllUsersReporQuestionViewModel
                {
                    Name = x.Name,
                    Family = x.Family,
                    NumberOfQuestionAnswerJudged = x.QuestionAnswerJudges.Count,
                    NumberOfQuestionJudged = x.QuestionJudges.Count,
                    NumberOfSupervisorQuestion = x.SupervisorQuestions.Count,
                    NumberOfWriteQuestion = x.Questions.Count(question => question.Writer.Id == x.Id)
                }).ToList();

        }

        public object GetAllByTopicIdsNoJudge(IEnumerable<int> ids, int userid, int rollLevel)
        {
            if (rollLevel < 3)
            {
                return _questions
                    .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                    .Where(x => x.QuestionJudges.Count < x.Topics.FirstOrDefault().Lesson.NumberOfJudges)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();
            }
            else
            {
                return _questions
                    .Where(current => current.Topics.Any(x => ids.Contains(x.Id)))
                    .Where(current =>
                        current.QuestionJudges.All(x => x.UserId != userid) /*|| !current.QuestionJudges.Any()*/)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();
            }
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


        /// <summary>
        /// تعداد همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public int CountAllJudgedByUserId(int id)
        {
            return _questions
                .Where(current => current.QuestionJudges.Any(x => x.UserId == id))
                .AsNoTracking()
                .AsEnumerable()
                .Count();
        }



        /// <summary>
        /// گرفتن همه سوالات کارشناسی شده توسط یک کاربر مربوط به درس
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllJudgedByUserIdByLessonId(int userId, int rolllevel, int lessonId)
        {
            if (rolllevel < 3)
            {
                return _questions
                    .Where(x => x.QuestionGroups.Any(y => y.LessonId == lessonId))
                    .Where(x => x.QuestionJudges.Count >= x.Topics.FirstOrDefault().Lesson.NumberOfJudges)
                    .OrderByDescending(x => x.Id)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();

            }
            else
            {
                return _questions
                    .Where(current => current.QuestionJudges.Any(x => x.UserId == userId))
                    .Where(x => x.QuestionGroups.Any(y => y.LessonId == lessonId))
                    .OrderByDescending(x => x.Id)
                    .AsNoTracking()
                    .AsEnumerable()
                    .Select(Mapper.Map<QuestionViewModel>)
                    .ToList();


            }
        }

        /// <summary>
        /// گرفتن همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllActiveByLessonId(int id)
        {
            return _questions
                .Where(current => current.IsActive)
                .Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }

        /// <summary>
        /// تعداد همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public int CountAllActive()
        {
            return _questions
                .Where(current => current.IsActive)
                .AsNoTracking()
                .AsEnumerable()
                .Count();
        }


        /// <summary>
        /// تعداد همه سوالات 
        /// </summary>
        /// <returns></returns>
        public int CountAll()
        {
            return _questions
                .AsNoTracking()
                .AsEnumerable()
                .Count();
        }



        /// <summary>
        /// گرفتن همه سوالات کارشناسی شده توسط یک کاربر
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllJudgedByUserId(int id)
        {
            return _questions
                .Where(current => current.QuestionJudges.Any(x => x.UserId == id))
                .OrderByDescending(x => x.Id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }


        /// <summary>
        /// گرفتن همه سوال های مباحث
        /// </summary>
        /// <returns></returns>
        public IList<QuestionViewModel> GetAllByTopicIdsForAssay(List<int> ids, int lookupId_QuestionHardnessType, int count)
        {
            return _questions
                .Where(current => current.Topics.Any(x => ids.Contains(x.Id) && current.LookupId_QuestionHardnessType == lookupId_QuestionHardnessType))
                .OrderBy(x => Guid.NewGuid())
                .Take(count)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه سوال های یک درس که مبحث ندارند
        /// </summary>
        /// <returns></returns>
        public object GetAllNoTopicByLessonId(int id)
        {
            return _questions
                .Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
                .Where(x => !x.Topics.Any())
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionViewModel>)
                .ToList();
        }

        /// <summary>
        /// گرفتن همه سوال های یک درس
        /// </summary>
        /// <returns></returns>
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
        /// گرفتن همه سوال های یک درس کارشناسی شده کامل به اندازه تعداد کارشناسان آن درس
        /// </summary>
        /// <returns></returns>
        public object GetAllJudgedByLessonId(int id)
        {
            return _questions
                .Where(current => current.QuestionGroups.Any(x => x.LessonId == id))
                .Where(x => x.QuestionJudges.Count >= x.Topics.FirstOrDefault().Lesson.NumberOfJudges)
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
                .Include(current => current.QuestionAnswers)
                //.Include(current => current.QuestionAnswers.OrderBy(y => y.IsMaster))
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


            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";

            //save Doc file in temp memory
            word.SaveAs(wordFilename);

            // Open a doc file.
            var app = new Application();
            var source = app.Documents.Open(wordFilename);

            //حذف عدد اول سوال
            if (QuestionGroupService.IsQuestionParagraph(source.Paragraphs[1].Range.Text))
            {
                int i = 1;
                while (i < source.Paragraphs[1].Range.Characters.Count &&
                       source.Paragraphs[1].Range.Characters[i].Text != "-")
                {
                    source.Paragraphs[1].Range.Characters[i].Delete();
                }
                source.Paragraphs[1].Range.Characters[i].Delete();
            }

            foreach (Paragraph paragraph in source.Paragraphs)
            {
                question.Context += paragraph.Range.Text;
            }


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

            var supervisor = new User() { Id = questionViewModel.SupervisorUserId };
            _uow.MarkAsUnChanged(supervisor);
            question.Supervisors.Add(supervisor);


            _questions.Add(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);



            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName))
            {

                word.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx");
                source.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", WdSaveFormat.wdFormatPDF);
                //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", SitePath.GetQuestionAbsPath(questionViewModel.FileName));

                File.Delete(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf");
                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, question.FileName, question.AnswerNumber);

                target.Close();
            }


            // File.Delete(wordFilename);
            source.Close();
            app.Quit();

            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(question.Id);
            return clientResult;
        }


        /// <summary>
        /// ذخیره عکس فایل ورد
        /// </summary>
        public static void SaveOptionsOfQuestions(Document source, Document target, string FileName, int answer)
        {
            //تریک درست شدن گزینه ها 
            source.ActiveWindow.Selection.WholeStory();
            source.ActiveWindow.Selection.Copy();
            target.ActiveWindow.Selection.Paste();
            target.ActiveWindow.Selection.WholeStory();
            target.ActiveWindow.Selection.Delete();
            target.ActiveWindow.Selection.Paste();

            int j = source.Paragraphs.Count;
            int k = 0;
            Paragraph p1 = null, p2 = null, p3 = null, p4 = null;
            int i1 = 0, i2 = 0, i3 = 0, i4 = 0;
            while (j > 0 && k < 4)
            {
                if (source.Paragraphs[j].Range.Text != "\f" && source.Paragraphs[j].Range.Text != "\f\r"
                                                            && source.Paragraphs[j].Range.Text != "\r")
                {
                    k++;
                    switch (k)
                    {
                        case 1:
                            p4 = source.Paragraphs[j];
                            i4 = j;
                            break;
                        case 2:
                            p3 = source.Paragraphs[j];
                            i3 = j;
                            break;
                        case 3:
                            p2 = source.Paragraphs[j];
                            i2 = j;
                            break;
                        case 4:
                            p1 = source.Paragraphs[j];
                            i1 = j;
                            break;
                    }


                }
                j--;
            }

            var filename3 = Encryption.Base64Encode(Encryption.Encrypt(answer + "-" + FileName));
            source.SaveAs(SitePath.GetQuestionOptionsAbsPath(filename3) + ".docx");
            while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            source.SaveAs(SitePath.GetQuestionAbsPath(filename3) + ".pdf", WdSaveFormat.wdFormatPDF);
            //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(filename3) + ".pdf", SitePath.GetQuestionAbsPath(filename3));

            File.Delete(SitePath.GetQuestionAbsPath(filename3) + ".pdf");

            source.Paragraphs[i4].Range.Copy();
            target.Paragraphs[i1].Range.Paste();

            source.Paragraphs[i1].Range.Copy();
            target.Paragraphs[i2].Range.Paste();

            source.Paragraphs[i2].Range.Copy();
            target.Paragraphs[i3].Range.Paste();

            source.Paragraphs[i3].Range.Copy();
            target.Paragraphs[i4].Range.Paste();

            filename3 = Encryption.Base64Encode(Encryption.Encrypt(((++answer) % 4) + "-" + FileName));
            target.SaveAs(SitePath.GetQuestionOptionsAbsPath(filename3) + ".docx");
            while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            source.SaveAs(SitePath.GetQuestionAbsPath(filename3) + ".pdf", WdSaveFormat.wdFormatPDF);
            //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(filename3) + ".pdf", SitePath.GetQuestionAbsPath(filename3));

            File.Delete(SitePath.GetQuestionAbsPath(filename3) + ".pdf");

            source.Paragraphs[i3].Range.Copy();
            target.Paragraphs[i1].Range.Paste();

            source.Paragraphs[i4].Range.Copy();
            target.Paragraphs[i2].Range.Paste();

            source.Paragraphs[i1].Range.Copy();
            target.Paragraphs[i3].Range.Paste();

            source.Paragraphs[i2].Range.Copy();
            target.Paragraphs[i4].Range.Paste();

            filename3 = Encryption.Base64Encode(Encryption.Encrypt(((++answer) % 4) + "-" + FileName));
            target.SaveAs(SitePath.GetQuestionOptionsAbsPath(filename3) + ".docx");
            while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            source.SaveAs(SitePath.GetQuestionAbsPath(filename3) + ".pdf", WdSaveFormat.wdFormatPDF);
            //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(filename3) + ".pdf", SitePath.GetQuestionAbsPath(filename3));

            File.Delete(SitePath.GetQuestionAbsPath(filename3) + ".pdf");

            source.Paragraphs[i2].Range.Copy();
            target.Paragraphs[i1].Range.Paste();

            source.Paragraphs[i3].Range.Copy();
            target.Paragraphs[i2].Range.Paste();

            source.Paragraphs[i4].Range.Copy();
            target.Paragraphs[i3].Range.Paste();

            source.Paragraphs[i1].Range.Copy();
            target.Paragraphs[i4].Range.Paste();

            filename3 = Encryption.Base64Encode(Encryption.Encrypt(((++answer) % 4) + "-" + FileName));
            target.SaveAs(SitePath.GetQuestionOptionsAbsPath(filename3) + ".docx");
            while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            source.SaveAs(SitePath.GetQuestionAbsPath(filename3) + ".pdf", WdSaveFormat.wdFormatPDF);
            //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(filename3) + ".pdf", SitePath.GetQuestionAbsPath(filename3));

            File.Delete(SitePath.GetQuestionAbsPath(filename3) + ".pdf");
        }




        /// <summary>
        /// پاک کردن فایل های گزینه یک سوال 
        /// </summary>
        public static void DeleteOptionsOfQuestion(string FileName)
        {
            var filename1 = Encryption.Base64Encode(Encryption.Encrypt(1 + "-" + FileName));
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx");
            }
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png");
            }
            filename1 = Encryption.Base64Encode(Encryption.Encrypt(2 + "-" + FileName));
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx");
            }
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png");
            }
            filename1 = Encryption.Base64Encode(Encryption.Encrypt(3 + "-" + FileName));
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx");
            }
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png");
            }
            filename1 = Encryption.Base64Encode(Encryption.Encrypt(0 + "-" + FileName));
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".docx");
            }
            if (File.Exists(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png"))
            {
                File.Delete(SitePath.GetQuestionOptionsAbsPath(filename1) + ".png");
            }
        }




        /// <summary>
        /// ویرایش سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult Update(QuestionUpdateViewModel questionViewModel)
        {
            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .Include(current => current.Lookup_AreaTypes)
                .Include(current => current.Supervisors)
                .First(current => current.Id == questionViewModel.Id);

          

            var previousFileName = questionViewModel.FileName;
            Application app = null;
            Document source = null;
            string wordFilename = null;
            var haveFileUpdate = false;
            if (questionViewModel.FileBytes.Length > 0)
            {
                wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
                haveFileUpdate = true;
                questionViewModel.FileName = Guid.NewGuid().ToString();


                //save Doc file in temp memory
                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(wordFilename, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                // Open a doc file.
                app = new Application();
                source = app.Documents.Open(wordFilename);

                //حذف عدد اول سوال
                if (QuestionGroupService.IsQuestionParagraph(source.Paragraphs[1].Range.Text))
                {
                    int i = 1;
                    while (i < source.Paragraphs[1].Range.Characters.Count &&
                           source.Paragraphs[1].Range.Characters[i].Text != "-")
                    {
                        source.Paragraphs[1].Range.Characters[i].Delete();
                    }
                    source.Paragraphs[1].Range.Characters[i].Delete();
                }

                foreach (Paragraph paragraph in source.Paragraphs)
                {
                    question.Context += paragraph.Range.Text;
                }

            }

            question.WriterId = questionViewModel.WriterId;
            question.Description = questionViewModel.Description;
            question.InsertDateTime = DateTime.Now;
            question.IsActive = questionViewModel.IsActive;
            question.IsStandard = questionViewModel.IsStandard;
            //question.LookupId_AreaType = questionViewModel.LookupId_AreaType;
            question.LookupId_AuthorType = questionViewModel.LookupId_AuthorType;
            question.LookupId_QuestionHardnessType = questionViewModel.LookupId_QuestionHardnessType;
            question.LookupId_QuestionType = questionViewModel.LookupId_QuestionType;
            question.LookupId_RepeatnessType = questionViewModel.LookupId_RepeatnessType;
            question.QuestionNumber = questionViewModel.QuestionNumber;
            question.QuestionPoint = questionViewModel.QuestionPoint;
            question.ResponseSecond = questionViewModel.ResponseSecond;
            question.UseEvaluation = questionViewModel.UseEvaluation;
            question.AnswerNumber = questionViewModel.AnswerNumber;
            question.IsDelete = questionViewModel.IsDelete;
            question.IsHybrid = questionViewModel.IsHybrid;
            question.TopicAnswer = questionViewModel.TopicAnswer;
            question.FileName = questionViewModel.FileName;


            //delete areaTypes
            var deleteAreaTypes = question.Lookup_AreaTypes.Where(oldArea => questionViewModel.LookupId_AreaTypes.All(newAreaId => newAreaId != oldArea.Id)).ToList();
                
            foreach (var area in deleteAreaTypes)
            {
                question.Lookup_AreaTypes.Remove(area);
            }

            //add areaTypes
            var addAreaTypeList = questionViewModel.LookupId_AreaTypes
                .Where(oldAreaId => question.Lookup_AreaTypes.All(newArea => newArea.Id != oldAreaId))
                .ToList();
            foreach (var area in addAreaTypeList)
            {
                var newArea = new Lookup() { Id = area};
                _uow.MarkAsUnChanged(newArea);
                question.Lookup_AreaTypes.Add(newArea);
            }


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

            ////delete supervisor
            //var supervisorsList = question.Supervisors.ToList();
            //foreach (var user in supervisorsList)
            //{
            //    question.Supervisors.Remove(user);
            //}

            //add supervisor
            if (questionViewModel.SupervisorUserId != 0 && questionViewModel.SupervisorUserId != question.Supervisors.FirstOrDefault().Id)
            {
                question.Supervisors.Remove(question.Supervisors.FirstOrDefault());
                var supervisor = new User() { Id = questionViewModel.SupervisorUserId };
                
                _uow.MarkAsUnChanged(supervisor);
                question.Supervisors.Add(supervisor);
            }

            _uow.MarkAsChanged(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) &&
                !string.IsNullOrEmpty(questionViewModel.FileName) && haveFileUpdate)
            {
                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".docx"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".docx");
                }

                if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".png"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".png");
                }

                DeleteOptionsOfQuestion(previousFileName);

                
                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx", FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }
                while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                source.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", WdSaveFormat.wdFormatPDF);
                //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", SitePath.GetQuestionAbsPath(questionViewModel.FileName));

                File.Delete(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf");


                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
            }
            else if (question.AnswerNumber != questionViewModel.AnswerNumber)
            {
                //save Doc file in temp memory
                
                using (var ms = new MemoryStream(questionViewModel.FileBytes))
                {
                    using (var file = new FileStream(wordFilename, FileMode.Create, FileAccess.Write))
                    {
                        ms.WriteTo(file);
                    }
                }

                // Open a doc file.
                app = new Application();
                source = app.Documents.Open(wordFilename);

                DeleteOptionsOfQuestion(previousFileName);

                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
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
                .Include(current => current.Supervisors)
                .First(current => current.Id == questionViewModel.Id);

            var previousFileName = question.FileName;
            Application app = null;
            Document source = null;
            string wordFilename = null;
            var haveFileUpdate = false;
            if (word != null && word.ContentLength > 0)
            {
                questionViewModel.FileName = Guid.NewGuid().ToString();
                wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
                haveFileUpdate = true;



                //save Doc file in temp memory
                word.SaveAs(wordFilename);

                // Open a doc file.
                app = new Application();
                source = app.Documents.Open(wordFilename);


                //حذف عدد اول سوال
                if (QuestionGroupService.IsQuestionParagraph(source.Paragraphs[1].Range.Text))
                {
                    int i = 1;
                    while (i < source.Paragraphs[1].Range.Characters.Count &&
                           source.Paragraphs[1].Range.Characters[i].Text != "-")
                    {
                        source.Paragraphs[1].Range.Characters[i].Delete();
                    }
                    source.Paragraphs[1].Range.Characters[i].Delete();
                }
                foreach (Paragraph paragraph in source.Paragraphs)
                {
                    question.Context += paragraph.Range.Text;
                }

            }

            question.WriterId = questionViewModel.WriterId;
            question.Description = questionViewModel.Description;
            //question.LookupId_AuthorType = questionViewModel.LookupId_AuthorType;
            question.LookupId_QuestionType = questionViewModel.LookupId_QuestionType;
            question.QuestionNumber = questionViewModel.QuestionNumber;
            question.AnswerNumber = questionViewModel.AnswerNumber;
            question.FileName = questionViewModel.FileName;
            question.IsDelete = questionViewModel.IsDelete;
            question.IsHybrid = questionViewModel.IsHybrid;

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


            //add supervisor
            if (questionViewModel.SupervisorUserId != 0 && questionViewModel.SupervisorUserId != question.Supervisors.FirstOrDefault().Id)
            {
                question.Supervisors.Remove(question.Supervisors.FirstOrDefault());
                var supervisor = new User() { Id = questionViewModel.SupervisorUserId };

                _uow.MarkAsUnChanged(supervisor);
                question.Supervisors.Add(supervisor);
            }

            _uow.MarkAsChanged(question);
            if (question.FileName == null)
            {
                _uow.ExcludeFieldsFromUpdate(question, x => x.FileName);
            }
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

                DeleteOptionsOfQuestion(previousFileName);

                word.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx");
                while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                source.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", WdSaveFormat.wdFormatPDF);
                //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
                //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
                ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", SitePath.GetQuestionAbsPath(questionViewModel.FileName));

                File.Delete(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf");

                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
            }
            else if (question.AnswerNumber != questionViewModel.AnswerNumber)
            {
                //save Doc file in temp memory
                word.SaveAs(wordFilename);

                // Open a doc file.
                app = new Application();
                source = app.Documents.Open(wordFilename);

                DeleteOptionsOfQuestion(previousFileName);

                var target = app.Documents.Add();
                SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

                target.Close();
                File.Delete(wordFilename);
                source.Close();
                app.Quit();
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
        public ClientMessageResult UpdateTopic(QuestionUpdateTopicViewModel questionViewModel, int userUpdateId)
        {
            var question = _questions
                .Include(current => current.QuestionOptions)
                .Include(current => current.Topics)
                .Include(current => current.Tags)
                .Include(x => x.Users)
                .First(current => current.Id == questionViewModel.Id);


            //Application app = null;
            //Document source = null;
            //string wordFilename = null;
            //var haveFileUpdate = false;
            //if (word != null && word.ContentLength > 0)
            //{
            //    wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionViewModel.FileName) + ".docx";
            //    haveFileUpdate = true;
            //    questionViewModel.FileName = Guid.NewGuid().ToString();


            //    //save Doc file in temp memory
            //    word.SaveAs(wordFilename);

            //    // Open a doc file.
            //    app = new Application();
            //    source = app.Documents.Open(wordFilename);

            //    //حذف عدد اول سوال
            //    if (QuestionGroupService.IsQuestionParagraph(source.Paragraphs[1].Range.Text))
            //    {
            //        int i = 1;
            //        while (i < source.Paragraphs[1].Range.Characters.Count &&
            //               source.Paragraphs[1].Range.Characters[i].Text != "-")
            //        {
            //            source.Paragraphs[1].Range.Characters[i].Delete();
            //        }
            //        source.Paragraphs[1].Range.Characters[i].Delete();
            //    }
            //    foreach (Paragraph paragraph in source.Paragraphs)
            //    {
            //        question.Context += paragraph.Range.Text;
            //    }

            //}

            //question.LookupId_AreaType = questionViewModel.LookupId_AreaType;
            //foreach (var item in questionViewModel.TopicAnswer)
            //{
            //    question.TopicAnswer += item;
            //    question.TopicAnswer += "@";
            //}
            question.TopicAnswer = questionViewModel.TopicAnswer;
            question.AnswerNumber = questionViewModel.AnswerNumber;
            question.IsHybrid = questionViewModel.IsHybrid;


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

            ////delete tag
            //var deleteTagList = question.Tags
            //    .Where(oldTag => questionViewModel.TagIds.All(newTagId => newTagId != oldTag.Id))
            //    .ToList();
            //foreach (var tag in deleteTagList)
            //{
            //    question.Tags.Remove(tag);
            //}

            ////add tag
            //var addTagList = questionViewModel.TagIds
            //    .Where(oldTagId => question.Tags.All(newTag => newTag.Id != oldTagId))
            //    .ToList();
            //foreach (var tagId in addTagList)
            //{
            //    var tag = new Tag { Id = tagId };
            //    _uow.MarkAsUnChanged(tag);
            //    question.Tags.Add(tag);
            //}

            _uow.MarkAsChanged(question);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            //if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionViewModel.FileName) && !string.IsNullOrEmpty(questionViewModel.FileName) && haveFileUpdate)
            //{
            //    if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".docx"))
            //    {
            //        File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".docx");
            //    }

            //    if (File.Exists(SitePath.GetQuestionAbsPath(previousFileName) + ".png"))
            //    {
            //        File.Delete(SitePath.GetQuestionAbsPath(previousFileName) + ".png");
            //    }

            //    DeleteOptionsOfQuestion(previousFileName);

            //    word.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".docx");
            //    while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //    source.SaveAs(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", WdSaveFormat.wdFormatPDF);
            //    //while (source.Windows[1].Panes[1].Pages.Count < 0) ;
            //    //var bits = source.Windows[1].Panes[1].Pages[1].EnhMetaFileBits;
            //    ImageUtility.SaveImageOfWordPdf(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf", SitePath.GetQuestionAbsPath(questionViewModel.FileName));

            //    File.Delete(SitePath.GetQuestionAbsPath(questionViewModel.FileName) + ".pdf");

            //    var target = app.Documents.Add();
            //    SaveOptionsOfQuestions(source, target, questionViewModel.FileName, question.AnswerNumber);

            //    target.Close();
            //    File.Delete(wordFilename);
            //    source.Close();
            //    app.Quit();
            //}
            //else if (question.AnswerNumber != questionViewModel.AnswerNumber)
            //{
            //    //save Doc file in temp memory
            //    word.SaveAs(wordFilename);

            //    // Open a doc file.
            //    app = new Application();
            //    source = app.Documents.Open(wordFilename);

            //    DeleteOptionsOfQuestion(previousFileName);

            //    var target = app.Documents.Add();
            //    SaveOptionsOfQuestions(source, target, questionViewModel.FileName, questionViewModel.AnswerNumber);

            //    target.Close();
            //    File.Delete(wordFilename);
            //    source.Close();
            //    app.Quit();
            //}

            if (!question.Users.Any())
            {
                var user = new User() { Id = userUpdateId };
                _uow.MarkAsUnChanged(user);
                question.Users.Add(user);
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
                .Include(current => current.Supervisors)
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

            //delete supervisor
            foreach (var user in question.Supervisors.ToList())
            {
                question.Supervisors.Remove(user);
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
                if (File.Exists(SitePath.GetQuestionAbsPath(question.FileName) + ".docx"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(question.FileName) + ".docx");
                }

                if (File.Exists(SitePath.GetQuestionAbsPath(question.FileName) + ".png"))
                {
                    File.Delete(SitePath.GetQuestionAbsPath(question.FileName) + ".png");
                }

                DeleteOptionsOfQuestion(question.FileName);
            }

            return Mapper.Map<ClientMessageResult>(msgRes);
        }







        /// <summary>
        /// برگشت تعدادکارشناس سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// todo:   check with ali
        public int GetNumberOfjudges(int questionId)
        {

            if (_questionGroupService.Value.IsInQuestionGroup(questionId))
            {
                return _questions
                    .Include(x => x.QuestionGroups.Select(y => y.Lesson))
                    .First(x => x.Id == questionId).QuestionGroups.First().Lesson.NumberOfJudges;


            }
            else
            {
                return _questions
                    .Include(x => x.Topics.Select(y => y.Lesson))
                    .First(x => x.Id == questionId).Topics.First().Lesson.NumberOfJudges;
            }
        }



    }
}
