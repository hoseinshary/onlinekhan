using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Question;
using NasleGhalam.WebApi.Extentions;


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
        public QuestionCreateViewModel GetById(int id)
        {
            return _questions
                .Where(current => current.Id == id)
                .Select(current => new QuestionCreateViewModel
                {
                    Id = current.Id
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه سوال ها
        /// </summary>
        /// <returns></returns>
        public IList<QuestionCreateViewModel> GetAll()
        {
            return _questions.Select(current => new QuestionCreateViewModel()
            {
                Id = current.Id,
            }).ToList();
        }


        /// <summary>
        /// ثبت سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <returns></returns>
        public MessageResult Create(QuestionCreateViewModel questionViewModel , HttpPostedFile wordFile )
        {
            var question = Mapper.Map<Question>(questionViewModel);
            _questions.Add(question);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = question.Id;
            if (msgRes.MessageType == MessageType.Success)
            {

                HttpPostedFileBase filebase = new HttpPostedFileWrapper(wordFile);
                string strextension = System.IO.Path.GetExtension(wordFile.FileName).Substring(1);
                string strPictureName = questionViewModel.Context;
                string strFullPictureName = $"{strPictureName}.{strextension}";
                string strPhysicalPathName = strFullPictureName.GetQuestionPhysicalPath();
                try
                {
                    wordFile.SaveAs(strPhysicalPathName);
                }
                catch (Exception e)
                {
                    msgRes.EnMessage += e.Message;
                }
            }

            return msgRes;
        }


        /// <summary>
        /// ثبت سوال به صورت گروهی
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <returns></returns>
        public MessageResult CreateMulti(QuestionTempViewModel questionViewModel, HttpPostedFile wordFile, HttpPostedFile excelFile)
        {
            HttpPostedFileBase filebase = new HttpPostedFileWrapper(wordFile);
            string strextension = System.IO.Path.GetExtension(wordFile.FileName).Substring(1);
            string strPictureName = questionViewModel.Context;
            string strFullPictureName = $"{strPictureName}.{strextension}";
            string strPhysicalPathName = strFullPictureName.GetQuestionMultiPhysicalPath();
            wordFile.SaveAs(strPhysicalPathName);



            var question = Mapper.Map<Question>(questionViewModel);
            _questions.Add(question);

            MessageResult msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = question.Id;
            return msgRes;
        }




        /// <summary>
        /// ویرایش سوال
        /// </summary>
        /// <param name="questionViewModel"></param>
        /// <returns></returns>
        public MessageResult Update(QuestionCreateViewModel questionViewModel)
        {
            var question = Mapper.Map<Question>(questionViewModel);
            _uow.MarkAsChanged(question);


            return _uow.CommitChanges(CrudType.Update, Title);

        }


        /// <summary>
        /// حذف سوال
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResult Delete(int id)
        {
            var questionViewModel = GetById(id);
            if (questionViewModel == null)
            {
                return Utility.NotFoundMessage();
            }

            var question = Mapper.Map<Question>(questionViewModel);
            _uow.MarkAsDeleted(question);

            return _uow.CommitChanges(CrudType.Delete, Title);

        }



        //public void f()
        //{
        //    string inputWordFile = @"C:\Users\hosein\Desktop\nasleghalam temp word file\adabiat-sarasari-95.docx";

        //    object missing = Type.Missing;
        //    Application app = new Application();
        //    Document doc = app.Documents.OpenNoRepairDialog(inputWordFile,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing, ref missing,
        //        ref missing, ref missing, ref missing);


        //    string temp = "";
        //    foreach (Paragraph p in doc.Paragraphs)
        //    {
        //        if (p.Range.Text[0].ToString().All(Char.IsDigit))
        //        {
        //            temp = "";
        //            temp += p.Range.Text;
        //        }
        //        else
        //        {
        //            temp += p.Range.Text;
        //        }
        //    }



        //    doc.Close();
        //    app.Quit();
        //}
        








    }
}
