using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
//using NasleGhalam.ViewModels.Panel;
using NasleGhalam.WebApi.Extensions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: hosein shary
	///     date: 1398/11/14
	/// </author>
	public class PanelController : ApiController
    {
        private readonly QuestionService _questionService;
        public PanelController(QuestionService QuestionService)
        {
            _questionService = QuestionService;
        }

        [HttpGet, CheckUserAccess(ActionBits.PublicAccess)]
        public IHttpActionResult GetData()
        {
            var data = new
            {                
                CountAllJudged =_questionService.CountAllJudgedByUserId(Request.GetUserId())
            };

            return Ok(data);
        }

    }
}
