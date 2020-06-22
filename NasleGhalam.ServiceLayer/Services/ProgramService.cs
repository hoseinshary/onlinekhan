using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Program;

namespace NasleGhalam.ServiceLayer.Services
{
    public class ProgramService
    {
        private const string Title = "برنامه هفتگی";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Program> _programs;

        public ProgramService(IUnitOfWork uow)
        {
            _uow = uow;
            _programs = uow.Set<Program>();
        }

        /// <summary>
        /// گرفتن  برنامه هفتگی با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProgramViewModel GetById(int id)
        {
            return _programs
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<ProgramViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه برنامه هفتگی ها
        /// </summary>
        /// <returns></returns>
        public IList<ProgramViewModel> GetAll()
        {
            return _programs
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<ProgramViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت برنامه هفتگی
        /// </summary>
        /// <param name="programViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(ProgramCreateViewModel programViewModel)
        {
            var program = Mapper.Map<Program>(programViewModel);
            _programs.Add(program);

            foreach (var programItem in programViewModel.ProgramItemViewModels)
            {
                program.ProgramItems.Add(new ProgramItem
                {
                    LookupId_PrgoramItemName = programItem.LookupId_PrgoramItemName, 
                    Hour = programItem.Hour , 
                    DayOfWeak = programItem.DayOfWeak ,
                    Description = programItem.Description 
                });
            }

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(program.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش برنامه هفتگی
        /// </summary>
        /// <param name="programViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(ProgramCreateViewModel programViewModel)
        {
            var program = Mapper.Map<Program>(programViewModel);
            _uow.MarkAsChanged(program);




            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(program.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف برنامه هفتگی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var programViewModel = GetById(id);
            if (programViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var program = Mapper.Map<Program>(programViewModel);
            _uow.MarkAsDeleted(program);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }
    }
}
