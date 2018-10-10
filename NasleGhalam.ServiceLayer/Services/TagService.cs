using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels;
using NasleGhalam.ViewModels.Tag;

namespace NasleGhalam.ServiceLayer.Services
{
    public class TagService
    {
        private const string Title = "برچسب";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Tag> _tags;

        public TagService(IUnitOfWork uow)
        {
            _uow = uow;
            _tags = uow.Set<Tag>();
        }


        /// <summary>
        /// گرفتن  برچسب با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TagViewModel GetById(int id)
        {
            return _tags
                .Where(current => current.Id == id)
                .Select(current => new TagViewModel
                {
                    Id = current.Id,
                    Name = current.Name,
                    IsSource = current.IsSource
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه برچسب ها
        /// </summary>
        /// <returns></returns>
        public IList<TagViewModel> GetAll()
        {
            return _tags.Select(current => new TagViewModel()
            {
                Id = current.Id,
                Name = current.Name,
                IsSource = current.IsSource
            }).ToList();
        }


        /// <summary>
        /// ثبت برچسب
        /// </summary>
        /// <param name="tagViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(TagViewModel tagViewModel)
        {
            var tag = Mapper.Map<Tag>(tagViewModel);
            _tags.Add(tag);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = tag.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش برچسب
        /// </summary>
        /// <param name="tagViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(TagViewModel tagViewModel)
        {
            var tag = Mapper.Map<Tag>(tagViewModel);
            _uow.MarkAsChanged(tag);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف برچسب
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var tagViewModel = GetById(id);
            if (tagViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var tag = Mapper.Map<Tag>(tagViewModel);
            _uow.MarkAsDeleted(tag);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// گرفتن همه برچسب ها برای لیست کشویی
        /// </summary>
        /// <returns></returns>
        public IList<SelectViewModel> GetAllDdl()
        {
            return _tags.Select(current => new SelectViewModel
            {
                value = current.Id,
                label = current.Name
            }).ToList();
        }
    }
}
