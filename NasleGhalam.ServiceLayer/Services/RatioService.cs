using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AutoMapper;
using NasleGhalam.Common;
using NasleGhalam.DataAccess.Context;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.ViewModels.Ratio;

namespace NasleGhalam.ServiceLayer.Services
{
    public class RatioService
    {
        private const string Title = "ضریب";
        private readonly IUnitOfWork _uow;
        private readonly IDbSet<Ratio> _ratios;

        public RatioService(IUnitOfWork uow)
        {
            _uow = uow;
            _ratios = uow.Set<Ratio>();
        }


        /// <summary>
        /// گرفتن  ضریب با آی دی
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RatioViewModel GetById(int id)
        {
            return _ratios
                .Where(current => current.Id == id)
                .Select(current => new RatioViewModel
                {
                    Id = current.Id,
                    Rate = current.Rate,
                    EducationSubGroupId = current.EducationSubGroupId,
                    LessonId = current.LessonId
                    
                }).FirstOrDefault();
        }


        /// <summary>
        /// گرفتن همه ضریب ها
        /// </summary>
        /// <returns></returns>
        public IList<RatioViewModel> GetAll()
        {
            return _ratios.Select(current => new RatioViewModel()
            {
                Id = current.Id,
                Rate = current.Rate,
                EducationSubGroupId = current.EducationSubGroupId,
                EducationSubGroupName = current.EducationSubGroup.Name,
                LessonId = current.LessonId,
                LessonName = current.Lesson.Name
            }).ToList();
        }


        /// <summary>
        /// ثبت ضریب
        /// </summary>
        /// <param name="ratioViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Create(RatioViewModel ratioViewModel)
        {
            var ratio = Mapper.Map<Ratio>(ratioViewModel);
            _ratios.Add(ratio);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Create, Title);
            msgRes.Id = ratio.Id;
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// ویرایش ضریب
        /// </summary>
        /// <param name="ratioViewModel"></param>
        /// <returns></returns>
        public MessageResultClient Update(RatioViewModel ratioViewModel)
        {
            var ratio = Mapper.Map<Ratio>(ratioViewModel);
            _uow.MarkAsChanged(ratio);


            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Update, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }


        /// <summary>
        /// حذف ضریب
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public MessageResultClient Delete(int id)
        {
            var ratioViewModel = GetById(id);
            if (ratioViewModel == null)
            {
                return Mapper.Map<MessageResultClient>(Utility.NotFoundMessage());
            }

            var ratio = Mapper.Map<Ratio>(ratioViewModel);
            _uow.MarkAsDeleted(ratio);

            MessageResultServer msgRes = _uow.CommitChanges(CrudType.Delete, Title);
            return Mapper.Map<MessageResultClient>(msgRes);
        }

        
    }
}
