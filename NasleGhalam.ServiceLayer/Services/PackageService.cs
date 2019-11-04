using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Package;

namespace NasleGhalam.ServiceLayer.Services
{
    public class PackageService
    {
        private const string Title = "بسته";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Package> _packages;

        public PackageService(IUnitOfWork uow)
        {
            _uow = uow;
            _packages = uow.Set<Package>();
        }

        /// <summary>
        /// گرفتن  بسته با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PackageViewModel GetById(int id)
        {
            return _packages
                .Where(current => current.Id == id)
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<PackageViewModel>)
                .FirstOrDefault();
        }

        /// <summary>
        /// گرفتن همه بسته ها
        /// </summary>
        /// <returns></returns>
        public IList<PackageViewModel> GetAll()
        {
            return _packages
                .AsNoTracking()
                .AsEnumerable()
                .Select(Mapper.Map<PackageViewModel>)
                .ToList();
        }

        /// <summary>
        /// ثبت بسته
        /// </summary>
        /// <param name="packageViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Create(PackageCreateViewModel packageViewModel)
        {
            var package = Mapper.Map<Package>(packageViewModel);
            _packages.Add(package);

            foreach (var item in packageViewModel.LessonIds)
            {
                var lesson = new Lesson { Id = item };
                _uow.MarkAsUnChanged(lesson);
                package.Lessons.Add(lesson);
            }

            var serverResult = _uow.CommitChanges(CrudType.Create, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(package.Id);

            return clientResult;
        }

        /// <summary>
        /// ویرایش بسته
        /// </summary>
        /// <param name="packageViewModel"></param>
        /// <returns></returns>
        public ClientMessageResult Update(PackageUpdateViewModel packageViewModel)
        {
            var package = Mapper.Map<Package>(packageViewModel);
            _uow.MarkAsChanged(package);

            var serverResult = _uow.CommitChanges(CrudType.Update, Title);
            var clientResult = Mapper.Map<ClientMessageResult>(serverResult);

            if (clientResult.MessageType == MessageType.Success)
                clientResult.Obj = GetById(package.Id);

            return clientResult;
        }

        /// <summary>
        /// حذف بسته
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMessageResult Delete(int id)
        {
            var packageViewModel = GetById(id);
            if (packageViewModel == null)
            {
                return ClientMessageResult.NotFound();
            }

            var package = Mapper.Map<Package>(packageViewModel);
            _uow.MarkAsDeleted(package);

            var msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<ClientMessageResult>(msgRes);
        }
    }
}
