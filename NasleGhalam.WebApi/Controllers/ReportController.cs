using System.Collections.Generic;
using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.ViewModels.Report;
using NasleGhalam.WebApi.FilterAttribute;
//using NasleGhalam.ViewModels.Report;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 1398/11/2
	/// </author>
	public class ReportController : ApiController
    {
        private readonly QuestionService _questionService;
        private readonly QuestionJudgeService _questionJudgeService;
        private readonly LessonService _lessonService;

        public ReportController(QuestionService questionService , QuestionJudgeService questionJudgeService , LessonService lessonService)
        {
            _questionService = questionService;
            _questionJudgeService = questionJudgeService;
            _lessonService = lessonService;

        }

        [HttpGet, CheckUserAccess(ActionBits.ReportReadAccess)]
        public IList<AllQuestionOfEachLessonViewModel> GetAllQuestionOfEachLesson()
        {
            return _lessonService.GetAllQuestionOfEachLesson();
        }


        [HttpGet, CheckUserAccess(ActionBits.ReportReadAccess)]
        public IList<AllUsersReporQuestionViewModel> GetAllUsersReport()
        {
            return _questionService.GetAllUsersReport();
        }




    }
}
