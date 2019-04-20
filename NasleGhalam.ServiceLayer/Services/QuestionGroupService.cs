using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using Microsoft.Office.Interop.Word;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.QuestionGroup;
using System.Drawing.Imaging;
using Microsoft.Office.Interop.Excel;

namespace NasleGhalam.ServiceLayer.Services
{
    public class QuestionGroupService
    {
        private const string Title = "سوال گروهی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<QuestionGroup> _questionGroups;

        public QuestionGroupService(IUnitOfWork uow)
        {
            _uow = uow;
            _questionGroups = uow.Set<QuestionGroup>();
        }


        /// <summary>
        /// گرفتن  سوال گروهی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QuestionGroupViewModel GetById(int id)
        {
            return _questionGroups
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionGroupViewModel>)
                .FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه سوال گروهی ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionGroupViewModel> GetAll()
        {
            return _questionGroups
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<QuestionGroupViewModel>)
                .ToList();
        }


        /// <summary>
        /// ثبت سوال گروهی
        /// </summary>
        /// <param name="questionGroupViewModel"></param>
        /// <param name="word"></param>
        /// <param name="excel"></param>
        /// <returns></returns>
        public MessageResultClient Create(QuestionGroupCreateViewModel questionGroupViewModel, HttpPostedFile word, HttpPostedFile excel)
        {
            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);
            
            //save Doc and excel file in temp memory
            word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".docx");
            excel.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".xlsx");

            // Open a doc file.
            var app = new Microsoft.Office.Interop.Word.Application();
            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".docx";
            var excelFilename = SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".xlsx";
            var doc = app.Documents.Open(wordFilename);

            var missing = Type.Missing;

            //read from excel file
            var xlApp = new Microsoft.Office.Interop.Excel.Application();
            var xlWorkbook = xlApp.Workbooks.Open(excelFilename, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            var xlWorksheet = (_Worksheet)xlWorkbook.Sheets[1];
            var xlRange = xlWorksheet.UsedRange;

            var rowCount = xlRange.Rows.Count;
            var colCount = xlRange.Columns.Count;
            var dt = new System.Data.DataTable();
            for (var k = 1; k <= rowCount; k++)
            {
                var dr = dt.NewRow();
                for (var j = 1; j <= colCount; j++)
                {
                    if (k == 1)
                    {
                        dt.Columns.Add(Convert.ToString((xlRange.Cells[k, j] as Microsoft.Office.Interop.Excel.Range).Value2));
                    }
                    else
                    {
                        dr[j - 1] = Convert.ToString((xlRange.Cells[k, j] as Microsoft.Office.Interop.Excel.Range).Value2);
                    }

                }
                if (k != 1)
                    dt.Rows.Add(dr);
            }

            xlWorkbook.Close();
            xlApp.Quit();

            //split question group
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

                        //create single question
                        var newQuestion = new Question();
                        var newGuid = Guid.NewGuid();
                        newQuestion.FileName = newGuid.ToString();
                        newQuestion.Context = context;
                        newQuestion.LookupId_QuestionType = dt.Rows[numberOfQ - 1]["نوع سوال"].ToString() == "تستی" ? 6 : 7;
                        newQuestion.QuestionPoint = Convert.ToInt32(dt.Rows[numberOfQ - 1]["بارم سوال"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["بارم سوال"] : 0);
                        newQuestion.AnswerNumber = Convert.ToInt32(dt.Rows[numberOfQ - 1]["گزینه صحیح"] != DBNull.Value ? dt.Rows[numberOfQ - 1]["گزینه صحیح"] : 0);
                        newQuestion.LookupId_QuestionHardnessType = 1040;
                        newQuestion.LookupId_AreaType = 1036;
                        newQuestion.LookupId_AuthorType = 1039;
                        newQuestion.LookupId_RepeatnessType = 21;
                        newQuestion.InsertDateTime = DateTime.Now;
                        newQuestion.IsStandard = dt.Rows[numberOfQ - 1]["درجه استاندارد"].ToString() == "استاندارد";
                        newQuestion.AuthorName = dt.Rows[numberOfQ - 1]["نام طراح"].ToString();
                        newQuestion.UserId = questionGroupViewModel.UserId;
                        newQuestion.Description = dt.Rows[numberOfQ - 1]["توضیحات"].ToString();
                        newQuestion.IsActive = false;
                        newQuestion.ResponseSecond = Convert.ToInt16(dt.Rows[numberOfQ - 1]["زمان پاسخگویی"]!= DBNull.Value ? dt.Rows[numberOfQ - 1]["زمان پاسخگویی"] : 0);
                        newQuestion.UseEvaluation = false;
                        newQuestion.QuestionNumber = Convert.ToInt32(dt.Rows[numberOfQ - 1]["شماره سوال در منبع اصلی"]!= DBNull.Value ? dt.Rows[numberOfQ - 1]["شماره سوال در منبع اصلی"] : 0);

                        questionGroup.Questions.Add(newQuestion);

                        var filename2 = SitePath.GetQuestionAbsPath(newGuid.ToString()) + ".docx";
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
                            using (var ms = new MemoryStream((byte[]) (bits)))
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

            _questionGroups.Add(questionGroup);
            _uow.ValidateOnSaveEnabled(false);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = questionGroup.Id;

            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionGroupViewModel.File) && !string.IsNullOrEmpty(questionGroupViewModel.File))
            {
               // File.Delete(SitePath.GetQuestionGroupTempAbsPath(wordFilename));
                //File.Delete(SitePath.GetQuestionGroupTempAbsPath(excelFilename) );
                word.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.File) + ".docx");
                excel.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.File) + ".xlsx");
            }

            var returnVal = Mapper.Map<MessageResultClient>(msgRes);
            returnVal.Obj = Mapper.Map<QuestionGroupViewModel>(questionGroup);
            return returnVal;
        }

        public MessageResultClient PreCreate(QuestionGroupCreateViewModel questionGroupViewModel, HttpPostedFile word )
        {
            var returnGuidList = new List<string>();

            //save Doc and excel file in temp memory
            word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) );

            // Open a doc file.
            var app = new Microsoft.Office.Interop.Word.Application();
            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) ;
            
            var doc = app.Documents.Open(wordFilename);
            var missing = Type.Missing;

            //split question group
            var x = doc.Paragraphs.Count;
            var i = 1;
            while (i <= x)
            {
                if (doc.Paragraphs[i].Range.Text == "\f" || doc.Paragraphs[i].Range.Text == "\f\r")
                    i++;
                else
                {
                    if (IsQuestionParagraph(doc.Paragraphs[i].Range.Text))
                    {
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
                        var newEntry =  $"/content/questionGroupTemp/{newGuid}.png".ToFullRelativePath();
                        returnGuidList.Add(newEntry);

                        //تبدیل به عکس
                        var pane = newDoc2.Windows[1].Panes[1];
                        var page = pane.Pages[1];
                        var bits = page.EnhMetaFileBits;
                        var target = SitePath.GetQuestionGroupTempAbsPath(newGuid.ToString());

                        //crop and resize
                        try
                        {
                            using (var ms = new MemoryStream((byte[]) (bits)))
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

                        newDoc2.Close(WdSaveOptions.wdDoNotSaveChanges,WdOriginalFormat.wdOriginalDocumentFormat,false);
                    }
                }
            }

            doc.Close();
            app.Quit();
            /////////////////////////////////

            var msgRes = new MessageResultClient {MessageType = MessageType.Success, Obj = returnGuidList};
            return msgRes;
        }


        /// <summary>
        /// ویرایش سوال گروهی
        /// </summary>
        /// <param name="questionGroupViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(QuestionGroupUpdateViewModel questionGroupViewModel)
        {
            var questionGroup = _questionGroups
                .First(current => current.Id == questionGroupViewModel.Id);
            if (!string.IsNullOrEmpty(questionGroupViewModel.Title))
            {
                questionGroup.Title = questionGroupViewModel.Title;
            }

            if (questionGroupViewModel.LessonId != 0 && questionGroupViewModel.LessonId != null)
            {
                questionGroup.LessonId = questionGroupViewModel.LessonId;
            }
            
            

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف سوال گروهی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var questionGroup = _questionGroups
                .Include(current => current.Questions)
                .First(current => current.Id == id);

            if (questionGroup == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            //remove questions relation
            var questions = questionGroup.Questions.ToList();
            foreach (var item in questions)
            {
                questionGroup.Questions.Remove(item);
                _uow.MarkAsDeleted(item);
            }

            

            _uow.MarkAsDeleted(questionGroup);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            if (msgRes.MessageType == MessageType.Success)
            {
                //remove questions file
                foreach (var item in questions)
                {
                    File.Delete(SitePath.GetQuestionAbsPath(item.FileName) + ".docx");
                    File.Delete(SitePath.GetQuestionAbsPath(item.FileName) + ".png");
                }

                File.Delete(SitePath.GetQuestionGroupAbsPath(questionGroup.File) + ".docx");
                File.Delete(SitePath.GetQuestionGroupAbsPath(questionGroup.File) + ".xlsx");
            }

            return Mapper.Map<MessageResultClient>(msgRes);
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
                        while (j < 20 && i < arrayTemp.Length)
                        {
                            i++;
                            j++;
                        }
                        if (j == 20)
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