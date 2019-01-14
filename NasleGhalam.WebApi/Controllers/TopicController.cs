using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Topic;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
    /// <author>
    ///     name: علیرضا اعتمادی
    ///     date: 1397.08.20
    /// </author>
    public class TopicController : ApiController
    {
        private readonly TopicService _topicService;
        public TopicController(TopicService topicService)
        {
            _topicService = topicService;
        }


        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllByLessonId(int id)
        {
            return Ok(_topicService.GetAllByLessonId(id));
        }


        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetById(int id)
        {
            var topic = _topicService.GetById(id);
            if (topic == null)
            {
                return NotFound();
            }
            return Ok(topic);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.TopicCreateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Create(TopicCreateViewModel topicViewModel)
        {
            return Ok(_topicService.Create(topicViewModel));
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.TopicUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(TopicUpdateViewModel topicViewModel)
        {
            return Ok(_topicService.Update(topicViewModel));
        }


        [HttpPost, CheckUserAccess(ActionBits.TopicDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            return Ok(_topicService.Delete(id));
        }
    }
}
