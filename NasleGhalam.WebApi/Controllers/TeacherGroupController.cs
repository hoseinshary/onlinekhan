﻿using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.TeacherGroup;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class TeacherGroupController : ApiController
    {
        private readonly TeacherGroupService _teacherGroupService;
        public TeacherGroupController(TeacherGroupService teacherGroupService)
        {
            _teacherGroupService = teacherGroupService;
        }

        [CheckUserAccess(ActionBits.TeacherGroupReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_teacherGroupService.GetAll());
        }

        [CheckUserAccess(ActionBits.TeacherGroupReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var teacherGroup = _teacherGroupService.GetById(id);
            if (teacherGroup == null)
            {
                return NotFound();
            }
            return Ok(teacherGroup);
        }

        

        [HttpPost]
        [CheckUserAccess(ActionBits.TeacherGroupCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(TeacherGroupCreateViewModel teacherGroupViewModel)
        {
            return Ok(_teacherGroupService.Create(teacherGroupViewModel));
        }

        [HttpPost]
        [CheckUserAccess(ActionBits.TeacherGroupUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(TeacherGroupUpdateViewModel teacherGroupViewModel)
        {
            return Ok(_teacherGroupService.Update(teacherGroupViewModel));
        }
        [HttpPost]
        [CheckUserAccess(ActionBits.TeacherGroupDeleteAccess)]

        public IHttpActionResult Delete(int id)
        {
            return Ok(_teacherGroupService.Delete(id));
        }
    }
}
