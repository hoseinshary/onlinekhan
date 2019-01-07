using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.QuestionGroup;
using NasleGhalam.ServiceLayer.Util;
using NPOI.XSSF.UserModel;
using NPOI.XWPF.UserModel;

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
        public MessageResultClient Create(QuestionGroupCreateViewModel questionGroupViewModel, HttpPostedFile word , HttpPostedFile excel)
        {
            XWPFDocument document = null;
            document = new XWPFDocument(word.InputStream);
            var allP = document.Paragraphs;
            XSSFWorkbook sheet = new XSSFWorkbook();
            var s = sheet.GetSheetAt(0);
            
           

            var count = allP.Count;

            int questionCount = 0;
            int i = 0;
            while (i < count)
            {
                if (isQuestionParagraph(allP[i].Text))
                {
                    Question newQuestion = new Question();

                    

                    var tempQuestion = new List<XWPFParagraph>();
                    tempQuestion.Add(allP[i]);
                    i++;
                    while (!isQuestionParagraph(allP[i].Text))
                    {
                        tempQuestion.Add(allP[i]);

                        i++;
                    }

                    questionCount++;

                    XWPFDocument doc = new XWPFDocument();
                    int position = 0;
                    foreach (var item in tempQuestion)
                    {
                        doc.SetParagraph(item, position);
                        position++;
                    }
                    FileStream out1 = new FileStream(SitePath.GetQuestionAbsPath(Guid.NewGuid() + ".docx"), FileMode.Create);
                    doc.Write(out1);
                    out1.Close();

                }
                i++;
            }






            /////////////////////////////////

            var questionGroup = Mapper.Map<QuestionGroup>(questionGroupViewModel);
            _questionGroups.Add(questionGroup);

            var msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = questionGroup.Id;

            if (msgRes.MessageType == MessageType.Success && !string.IsNullOrEmpty(questionGroupViewModel.WordFile) && !string.IsNullOrEmpty(questionGroupViewModel.ExcelFile))
            {
                word.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.WordFile));
                excel.SaveAs(SitePath.GetQuestionGroupAbsPath(questionGroupViewModel.ExcelFile));
            }

            return Mapper.Map<MessageResultClient>(msgRes);
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
                    while (char.IsDigit(arraytemp[i]))
                    {
                        i++;
                    }
                    if (arraytemp[i] == '-')
                    {
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
