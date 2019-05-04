using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using AutoMapper;
using Microsoft.Office.Interop.Word;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionAnswer;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionAnswerService
    {
        private const string Title = "جواب سوال";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionAnswer> _questionAnswers;
         //todo :1111
        private readonly IDbSet<Question> _questions;
        private readonly Lazy<QuestionService> _questionService;

        

        public QuestionAnswerService(IUnitOfWork uow, Lazy<QuestionService> questionService1)
        {
            _uow = uow;
            _questionService = questionService1;
            _questionAnswers = uow.Set<QuestionAnswer>();
            _questions = uow.Set<Question>();

        }

        /// <summary>
        /// گرفتن  جواب سوال با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionAnswerViewModel GetById(int id)
        {
            return _questionAnswers
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionAnswerViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه جواب سوال ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionAnswerViewModel> GetAllByQuestionId(int id)
        {
            return _questionAnswers
                .Where(current => current.QuestionId == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionAnswerViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت جواب سوال
        /// </summary>
        /// <param name="questionAnswerViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult Create(QuestionAnswerCreateViewModel questionAnswerViewModel, HttpPostedFile word)
        {

            var questionAnswer = Mapper.Map<QuestionAnswer>(questionAnswerViewModel);
            questionAnswer.LookupId_AnswerType = 1042;

            var wordFileName = Guid.NewGuid().ToString();

            //save Doc file in temp memory
            word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(wordFileName) + ".docx");

            // Open a doc file.
            var app = new Application();
            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(wordFileName) + ".docx";
            var doc = app.Documents.Open(wordFilename);

            foreach (Paragraph paragraph in doc.Paragraphs)
            {
                questionAnswer.Context += paragraph.Range.Text;
            }

            //تبدیل به عکس
            var pane = doc.Windows[1].Panes[1];
            var page = pane.Pages[1];
            var bits = page.EnhMetaFileBits;
            var target = SitePath.GetQuestionAnswerAbsPath(wordFileName) + ".png";

            doc.Close();
            app.Quit();


            _questionAnswers.Add(questionAnswer);
            _uow.ValidateOnSaveEnabled(false);
            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            if (serverResult.MessageType == MessageType.Success && !string.IsNullOrEmpty(wordFileName) && !string.IsNullOrEmpty(wordFileName))
            {
                word.SaveAs(SitePath.GetQuestionAnswerAbsPath(wordFileName) + ".docx");
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
                clientResult.Obj = GetById(questionAnswer.Id);
            return clientResult;

        }

        /// <summary>
        /// ثبت جواب سوال
        /// </summary>
        /// <param name="questionAnswerViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult CreateMulti(QuestionAnswerCreateMultiViewModel questionAnswerViewModel, HttpPostedFile word)
        {
            var wordFileName = Guid.NewGuid().ToString();

            //read questionids from questiongroup
            var questions = _questions.Where(current => current.QuestionGroups.Any(y => y.Id == questionAnswerViewModel.QuestionGroupId)).ToList();



            //save Doc file in temp memory
            word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(wordFileName) + ".docx");


            // Open a doc file.
            var app = new Application();
            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(wordFileName) + ".docx";
            var doc = app.Documents.Open(wordFilename);

            var missing = Type.Missing;


            //split question Answer
            var x = doc.Paragraphs.Count;
            var i = 1;
            var numberOfQ = 0;
            while (i <= x)
            {
                if (doc.Paragraphs[i].Range.Text == "\f" || doc.Paragraphs[i].Range.Text == "\f\r")
                    i++;
                else
                {
                    if (IsQuestionParagraph(doc.Paragraphs[i].Range.Text))
                    {
                        var context = "";

                        numberOfQ++;
                        var newDoc2 = app.Documents.Add(
                            ref missing, ref missing, ref missing, ref missing);

                        doc.Paragraphs[i].Range.Copy();

                        app.Selection.Paste();
                        context += doc.Paragraphs[i].Range.Text;
                        i++;
                        while (i <= x && !IsQuestionParagraph(doc.Paragraphs[i].Range.Text))
                        {
                            if (doc.Paragraphs[i].Range.Text != "\f" && doc.Paragraphs[i].Range.Text != "\f\r")
                            {
                                doc.Paragraphs[i].Range.Copy();

                                app.Selection.Paste();
                                context += doc.Paragraphs[i].Range.Text;
                            }
                            i++;
                        }

                        //create single question Answer
                        var newQuestionAQuestion = new QuestionAnswer();
                        var newGuid = Guid.NewGuid();
                        newQuestionAQuestion.FilePath = newGuid.ToString();
                        newQuestionAQuestion.Context = context;
                        newQuestionAQuestion.UserId = questionAnswerViewModel.UserId;
                        newQuestionAQuestion.QuestionId = questions[numberOfQ-1].Id;
                        newQuestionAQuestion.Author = questionAnswerViewModel.Author;
                        newQuestionAQuestion.IsMaster = true;
                        newQuestionAQuestion.Title = questionAnswerViewModel.Title;
                        newQuestionAQuestion.LookupId_AnswerType = 1042;



                        _questionAnswers.Add(newQuestionAQuestion);

                        var filename2 = SitePath.GetQuestionAnswerAbsPath(newGuid.ToString()) + ".docx";
                        newDoc2.SaveAs(filename2, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing,
                            ref missing);

                        //تبدیل به عکس
                        var pane = newDoc2.Windows[1].Panes[1];
                        var page = pane.Pages[1];
                        var bits = page.EnhMetaFileBits;
                        var target = SitePath.GetQuestionAbsPath(newGuid.ToString()) + ".png";

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
                                File.Delete(pngTarget + "1.png");
                            }
                        }
                        catch (Exception ex)
                        {
                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                        }

                        newDoc2.Close();
                    }
                }
            }

            doc.Close();
            app.Quit();
            /////////////////////////////////


            _uow.ValidateOnSaveEnabled(false);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            
            var returnVal = Mapper.Map<ClientMessageResult>(msgRes);
            returnVal.Obj = _questionService.Value.GetAllByQuestionGroupId(questionAnswerViewModel.QuestionGroupId);
            return returnVal;
        }

        /// <summary>
        /// ثبت جواب سوال
        /// </summary>
        /// <param name="questionAnswerViewModel"></param>
        /// <param name="word"></param>
        /// <returns></returns>
        public ClientMessageResult PreCreateMulti(QuestionAnswerCreateMultiViewModel questionAnswerViewModel, HttpPostedFile word)
        {
            var wordFileName = Guid.NewGuid().ToString();

            //read questionids from questiongroup
            var questions = _questions.Where(current => current.QuestionGroups.Any(y => y.Id == questionAnswerViewModel.QuestionGroupId)).ToList();

          
            var returnGuidList = new List<Object>();

            //save Doc file in temp memory
            word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(wordFileName) + ".docx");

            // Open a doc file.
            var app = new Application();
            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(wordFileName) + ".docx";

            var doc = app.Documents.Open(wordFilename);
            var missing = Type.Missing;

            //split question Answer
            var x = doc.Paragraphs.Count;
            var i = 1;
            var numberOfQ = 0;
            while (i <= x)
            {
                if (doc.Paragraphs[i].Range.Text == "\f" || doc.Paragraphs[i].Range.Text == "\f\r")
                    i++;
                else
                {
                    if (IsQuestionParagraph(doc.Paragraphs[i].Range.Text))
                    {
                        numberOfQ++;
                        var newDoc2 = app.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                        doc.Paragraphs[i].Range.Copy();

                        app.Selection.Paste();
                        i++;
                        while (i <= x && !IsQuestionParagraph(doc.Paragraphs[i].Range.Text))
                        {
                            if (doc.Paragraphs[i].Range.Text != "\f" && doc.Paragraphs[i].Range.Text != "\f\r")
                            {
                                doc.Paragraphs[i].Range.Copy();

                                app.Selection.Paste();
                            }
                            i++;
                        }

                        var newGuid = Guid.NewGuid();
                        var returnItem = new
                        {
                            questionPath = $"/content/question/{questions[numberOfQ-1].FileName}.png".ToFullRelativePath(),
                            answerPath = $"/content/questionGroupTemp/{newGuid}.png".ToFullRelativePath()
                        };
                        returnGuidList.Add(returnItem);

                        //تبدیل به عکس
                        var pane = newDoc2.Windows[1].Panes[1];
                        Thread.Sleep(1000);
                        var page = pane.Pages[1];
                        var bits = page.EnhMetaFileBits;
                        var target = SitePath.GetQuestionGroupTempAbsPath(newGuid.ToString());

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
                                //resizedImage.Save(pngTarget, ImageFormat.Png);
                                var rectangle = ImageUtility.GetCropArea(resizedImage, 10);
                                var croppedImage = ImageUtility.CropImage(resizedImage, rectangle);
                                croppedImage.Save(pngTarget + ".png", ImageFormat.Png);
                                File.Delete(pngTarget + "1.png");
                            }
                        }
                        catch (Exception ex)
                        {
                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                        }

                        newDoc2.Close(WdSaveOptions.wdDoNotSaveChanges, WdOriginalFormat.wdOriginalDocumentFormat, false);
                    }
                }
            }

            doc.Close();
            app.Quit();
            /////////////////////////////////

            var msgRes = new ClientMessageResult { MessageType = MessageType.Success, Obj = returnGuidList };
            return msgRes;
        }

        /// <summary>
        /// ویرایش جواب سوال
        /// </summary>
        /// <param name="questionAnswerViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(QuestionAnswerUpdateViewModel questionAnswerViewModel)
        {
            var questionAnswer = Mapper.Map<QuestionAnswer>(questionAnswerViewModel);
            _uow.MarkAsChanged(questionAnswer);

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }

        /// <summary>
        /// حذف جواب سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var questionAnswerViewModel = GetById(id);
            if (questionAnswerViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var questionAnswer = Mapper.Map<QuestionAnswer>(questionAnswerViewModel);
            _uow.MarkAsDeleted(questionAnswer);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }

        public bool IsQuestionParagraph(string s)
        {
            var arrayTemp = s.ToCharArray();

            var i = 0;
            while (i < arrayTemp.Length)
            {
                if (arrayTemp[i] == ' ' || arrayTemp[i] == '\n' || arrayTemp[i] == '\r')
                {
                    i++;
                }
                else if (char.IsDigit(arrayTemp[i]))
                {
                    i++;
                    while (char.IsDigit(arrayTemp[i]) && i < arrayTemp.Length)
                    {
                        i++;
                    }
                    if (arrayTemp[i] == '-')
                    {
                        var j = 0;
                        while (j < 3 && i < arrayTemp.Length)
                        {
                            i++;
                            j++;
                        }
                        if (j == 3)
                            return true;
                    }
                    return false;
                }
                i++;
            }
            return false;
        }
    }
}
