using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.AssayAnswerSheet;

namespace NasleGhalam.ServiceLayer.Services
{
    public class AssayAnswerSheetService
    {
        private const string Title = "پاسخ نامه";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<AssayAnswerSheet> _assayAnswerSheets;

        private readonly Lazy<AssayService> _assayService;

        public AssayAnswerSheetService(IUnitOfWork uow , Lazy<AssayService> assayService )
        {
            _uow = uow;
            _assayAnswerSheets = uow.Set<AssayAnswerSheet>();
            _assayService = assayService;
        }

        /// <summary>
        /// گرفتن  پاسخ نامه با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AssayAnswerSheetViewModel GetById(int id)
        {
            AssayAnswerSheetViewModel a = new AssayAnswerSheetViewModel();
            return _assayAnswerSheets
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AssayAnswerSheetViewModel>)
                .FirstOrDefault();

            
        }

        /// <summary>
        /// گرفتن همه پاسخ نامه ها
        /// </summary>
        /// <returns></returns>
        public IList<AssayAnswerSheetViewModel> GetAll()
        {
            return _assayAnswerSheets
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<AssayAnswerSheetViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت پاسخ نامه
        /// </summary>
        /// <param name="assayAnswerSheetViewModel"></param>
        /// <returns></returns>
        public IList<AssayAnswerSheetCorectExamViewModel> Create(AssayAnswerSheetCreateViewModel assayAnswerSheetViewModel)
        {
            var assayAnswerSheet = Mapper.Map<AssayAnswerSheet>(assayAnswerSheetViewModel);

            //foreach (var answer in assayAnswerSheet.Answers)
            //{
            //    assayAnswerSheet.Answers += answer + ";";
            //}

            _assayAnswerSheets.Add(assayAnswerSheet);

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            var returnVal= new List<AssayAnswerSheetCorectExamViewModel>();

            if (clientResult.MessageType == MessageType.Success)
            {
                var assay = _assayService.Value.GetById(assayAnswerSheetViewModel.AssayId);

                for (int i = 0; i < assayAnswerSheetViewModel.Answers.Count ; i++)
                {
                    var tempVal = new AssayAnswerSheetCorectExamViewModel();
                    if (assayAnswerSheetViewModel.Answers[i] == 0)
                    {
                        tempVal.Tashih = Tashih.Non;
                    }
                    else if (assayAnswerSheetViewModel.Answers[i].ToString() == assay.QuestionsAnswer[i])
                    {
                        tempVal.Tashih = Tashih.Correct;
                    }
                    else
                    {
                        tempVal.Tashih = Tashih.Wrong;
                    }

                    tempVal.NumberOfQuestion = i + 1;
                    tempVal.Path = assay.QuestionsFile[i];

                    returnVal.Add(tempVal);


                }


                clientResult.Obj = GetById(assayAnswerSheet.Id);
            }

            return returnVal ;
        }

        /// <summary>
        /// ویرایش پاسخ نامه
        /// </summary>
        /// <param name="assayAnswerSheetViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(AssayAnswerSheetUpdateViewModel assayAnswerSheetViewModel)
        {
            var assayAnswerSheet = Mapper.Map<AssayAnswerSheet>(assayAnswerSheetViewModel);
            _uow.MarkAsChanged(assayAnswerSheet);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(assayAnswerSheet.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف پاسخ نامه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var assayAnswerSheetViewModel = GetById(id);
            if (assayAnswerSheetViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var assayAnswerSheet = Mapper.Map<AssayAnswerSheet>(assayAnswerSheetViewModel);
            _uow.MarkAsDeleted(assayAnswerSheet);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);

            var clientResult = Mapper.Map<ClientMessageResult>(msgRes);
            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = id;
            return clientResult;
        }
    }
}
