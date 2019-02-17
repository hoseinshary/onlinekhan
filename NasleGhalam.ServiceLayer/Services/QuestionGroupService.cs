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
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.QuestionGroup;
using NasleGhalam.ServiceLayer.Util;
using NasleGhalam.ViewModels.Question;
using System.Drawing.Imaging;
using Microsoft.Office.Interop.Excel;
using System.Data;

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
        /// <returns></returns>
        public MessageResultClient Create(QuestionGroupCreateViewModel questionGroupViewModel, HttpPostedFile word, HttpPostedFile excel)
        {
            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);
            _questionGroups.Add(questionGroup);

            //save Doc and excel file in temp memory
            word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".docx");
            excel.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".xlsx");


            // Open a doc file.
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".docx";
            var excelFilename = SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) + ".xlsx";
            Document doc = app.Documents.Open(wordFilename);

            object missing = Type.Missing;

            //split question group
            int x = doc.Paragraphs.Count;
            int i = 1;
            int numberOFQ = 0;
            while (i <= x)
            {
                if (doc.Paragraphs[i].Range.Text == "\f" || doc.Paragraphs[i].Range.Text == "\f\r")
                    i++;
                else
                {
                    if (isQuestionParagraph(doc.Paragraphs[i].Range.Text))
                    {
                        numberOFQ++;
                        Document newdoc2 = app.Documents.Add(
                            ref missing, ref missing, ref missing, ref missing);

                        doc.Paragraphs[i].Range.Copy();

                        app.Selection.Paste();
                        i++;
                        while (i <= x && !isQuestionParagraph(doc.Paragraphs[i].Range.Text))
                        {
                            if (doc.Paragraphs[i].Range.Text != "\f" && doc.Paragraphs[i].Range.Text != "\f\r")
                            {
                                doc.Paragraphs[i].Range.Copy();

                                app.Selection.Paste();
                            }
                            i++;
                        }


                        //read from excel file
                        Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                        Workbook xlWorkbook = xlApp.Workbooks.Open(excelFilename, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                        _Worksheet xlWorksheet = (_Worksheet)xlWorkbook.Sheets[1];
                        Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                        int rowCount = xlRange.Rows.Count;
                        int colCount = xlRange.Columns.Count;
                        System.Data.DataTable dt = new System.Data.DataTable();
                        for (int k = 1; k <= rowCount; k++)
                        {
                            DataRow dr = dt.NewRow();
                            for (int j = 1; j <= colCount; j++)
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

                        //create single question
                        Question newQuestion = new Question();
                        var newGuid = Guid.NewGuid();
                        newQuestion.FileName = newGuid.ToString();

                        newQuestion.LookupId_QuestionType = dt.Rows[numberOFQ - 1]["نوع سوال"].ToString() == "تستی" ? 6 : 7;
                        newQuestion.QuestionPoint = Convert.ToInt32(dt.Rows[numberOFQ - 1]["بارم سوال"] != DBNull.Value ? dt.Rows[numberOFQ - 1]["بارم سوال"] : 0);
                        newQuestion.AnswerNumber = Convert.ToInt32(dt.Rows[numberOFQ - 1]["گزینه صحیح"] != DBNull.Value ? dt.Rows[numberOFQ - 1]["گزینه صحیح"] : 0);
                        newQuestion.LookupId_QuestionHardnessType = 1040;
                        newQuestion.LookupId_AreaType = 1036;
                        newQuestion.LookupId_AuthorType = 1039;
                        newQuestion.LookupId_RepeatnessType = 21;
                        newQuestion.InsertDateTime = DateTime.Now;
                        newQuestion.IsStandard = dt.Rows[numberOFQ - 1]["درجه استاندارد"].ToString() == "استاندارد";
                        newQuestion.AuthorName = dt.Rows[numberOFQ - 1]["نام طراح"].ToString();
                        newQuestion.UserId = questionGroupViewModel.UserId;
                        newQuestion.Description = dt.Rows[numberOFQ - 1]["توضیحات"].ToString();
                        newQuestion.Context = "";
                        newQuestion.IsActive = false;
                        newQuestion.ResponseSecond = Convert.ToInt16(dt.Rows[numberOFQ - 1]["زمان پاسخگویی"]!= DBNull.Value ? dt.Rows[numberOFQ - 1]["زمان پاسخگویی"] : 0);
                        newQuestion.UseEvaluation = false;
                        newQuestion.QuestionNumber = Convert.ToInt32(dt.Rows[numberOFQ - 1]["شماره سوال در منبع اصلی"]!= DBNull.Value ? dt.Rows[numberOFQ - 1]["شماره سوال در منبع اصلی"] : 0);

                        questionGroup.Questions.Add(newQuestion);

                        var filename2 = SitePath.GetQuestionAbsPath(newGuid.ToString()) + ".docx";
                        newdoc2.SaveAs(filename2, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing,
                            ref missing, ref missing, ref missing, ref missing,
                            ref missing);

                        //تبدیل به عکس
                        var pane = newdoc2.Windows[1].Panes[1];
                        var page = pane.Pages[1];
                        var bits = page.EnhMetaFileBits;
                        var target = SitePath.GetQuestionAbsPath(newGuid.ToString()) + ".png";
                       
                        

                        //crop and resize
                        try
                        {
                            using (var ms = new MemoryStream((byte[])(bits)))
                            {
                                var image = System.Drawing.Image.FromStream(ms);
                                var pngTarget = target;//Path.ChangeExtension(target , "png");
                                image.Save(pngTarget + "1.png", ImageFormat.Png);
                                image = new Bitmap(pngTarget + "1.png");

                                var resizedImage = ImageUtility.GetImageWithRatioSize(image, 1 / 5d, 1 / 5d);
                                // resizedImage.Save(pngTarget, ImageFormat.Png);
                                var rectangle = ImageUtility.GetCropArea(resizedImage, 10);
                                var croppedImage = ImageUtility.CropImage(resizedImage, rectangle);
                                croppedImage.Save(pngTarget + ".png", ImageFormat.Png);
                            }
                        }
                        catch (System.Exception ex)
                        { }

                        newdoc2.Close();
                    }
                }
            }


            doc.Close();
            app.Quit();
            /////////////////////////////////



            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = questionGroup.Id;
            

            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionGroupViewModel.File) && !string.IsNullOrEmpty(questionGroupViewModel.File))
            {
               // File.Delete(SitePath.GetQuestionGroupTempAbsPath(wordFilename));
                //File.Delete(SitePath.GetQuestionGroupTempAbsPath(excelFilename) );
                word.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.File) + ".docx");
                excel.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.File) + ".xlsx");
            }

            var returnval = Mapper.Map<MessageResultClient>(msgRes);
            returnval.Obj = Mapper.Map<QuestionGroupViewModel>(questionGroup);
            return returnval;
        }

        public MessageResultClient PreCreate(QuestionGroupCreateViewModel questionGroupViewModel, HttpPostedFile word ,  string host)
        {
            List<string> returnGuidS = new List<string>();

            //save Doc and excel file in temp memory
            word.SaveAs(SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) );
        


            // Open a doc file.
            Microsoft.Office.Interop.Word.Application app = new Microsoft.Office.Interop.Word.Application();
            var wordFilename = SitePath.GetQuestionGroupTempAbsPath(questionGroupViewModel.File) ;
            
            Document doc = app.Documents.Open(wordFilename);

            object missing = Type.Missing;

            //split question group
            int x = doc.Paragraphs.Count;
            int i = 1;
            int numberOFQ = 0;
            while (i <= x)
            {
                if (doc.Paragraphs[i].Range.Text == "\f" || doc.Paragraphs[i].Range.Text == "\f\r")
                    i++;
                else
                {
                    if (isQuestionParagraph(doc.Paragraphs[i].Range.Text))
                    {
                        numberOFQ++;
                        Document newdoc2 = app.Documents.Add(
                            ref missing, ref missing, ref missing, ref missing);

                        doc.Paragraphs[i].Range.Copy();

                        app.Selection.Paste();
                        i++;
                        while (i <= x && !isQuestionParagraph(doc.Paragraphs[i].Range.Text))
                        {
                            if (doc.Paragraphs[i].Range.Text != "\f" && doc.Paragraphs[i].Range.Text != "\f\r")
                            {
                                doc.Paragraphs[i].Range.Copy();

                                app.Selection.Paste();
                            }
                            i++;
                        }



                        var newGuid = Guid.NewGuid();

                        var newEntry = host + @"/content/questiongrouptemp/" + newGuid + ".png";

                        returnGuidS.Add(newEntry);
                 

                        //تبدیل به عکس
                        var pane = newdoc2.Windows[1].Panes[1];
                        var page = pane.Pages[1];
                        var bits = page.EnhMetaFileBits;
                        var target = SitePath.GetQuestionGroupTempAbsPath(newGuid.ToString());
                        

                        //crop and resize
                        try
                        {
                            using (var ms = new MemoryStream((byte[])(bits)))
                            {
                                var image = System.Drawing.Image.FromStream(ms);
                                var pngTarget = target;//Path.ChangeExtension(target , "png");
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
                        catch (System.Exception ex)
                        { }

                        newdoc2.Close(WdSaveOptions.wdDoNotSaveChanges,WdOriginalFormat.wdOriginalDocumentFormat,false);
                    }
                }
            }


            doc.Close();
            app.Quit();
            /////////////////////////////////







            MessageResultClient msgRes = new MessageResultClient();
            msgRes.MessageType = MessageType.Success;
            msgRes.Obj = returnGuidS;
            
            return msgRes;
        }


        /// <summary>
        /// ویرایش سوال گروهی
        /// </summary>
        /// <param name="questionGroupViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(QuestionGroupUpdateViewModel questionGroupViewModel)
        {
            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);
            _uow.MarkAsChanged(questionGroup);

            var msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف سوال گروهی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// todo: delete all questions in one commit
        public MessageResultClient Delete(int id)
        {
            var questionGroupViewModel = GetById(id);
            if (questionGroupViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);
            _uow.MarkAsDeleted(questionGroup);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }

        public bool isQuestionParagraph(string s)
        {
            var arraytemp = s.ToCharArray();

            int i = 0;
            while (i < arraytemp.Length)
            {
                if (arraytemp[i] == ' ' || arraytemp[i] == '\n' || arraytemp[i] == '\r')
                {
                    i++;
                }
                else if (char.IsDigit(arraytemp[i]))
                {
                    i++;
                    while (char.IsDigit(arraytemp[i]) && i < arraytemp.Length)
                    {
                        i++;
                    }
                    if (arraytemp[i] == '-')
                    {
                        int j = 0;
                        while (j < 30 && i < arraytemp.Length)
                        {
                            i++;

                            j++;
                        }
                        if (j == 30)
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
