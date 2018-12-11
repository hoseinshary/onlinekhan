using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Topic;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: حسین شریعتمداری
	///     date: 10/05/1397
	/// </author>
	public class TopicController : ApiController
    {
        private readonly TopicService _topicService;
        public TopicController(TopicService topicService)
        {
            _topicService = topicService;
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

        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllByEducationGroup_LessonId(int id)
        {
            var topic = _topicService.GetAllByEducationGroup_LessonId(id);
            if (topic == null)
            {
                return NotFound();
            }
            return Ok(topic);
        }

        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllRoot(int id)
        {
            var topic = _topicService.GetAllRoot(id);
            if (topic == null)
            {
                return NotFound();
            }
            return Ok(topic);
        }


        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllTree(int id)
        {
            var topic = _topicService.GetAllTree(id);
            if (topic == null)
            {
                return NotFound();
            }
            return Ok(topic);
        }

        [HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAllChild(int id, int parentId)
        {
            var topic = _topicService.GetAllChild(id,parentId);
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
            var msgRes = _topicService.Create(topicViewModel);
            return Ok(msgRes);
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.TopicUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(TopicCreateViewModel topicViewModel)
        {
            var msgRes = _topicService.Update(topicViewModel);
            return Ok(msgRes);
        }


        [HttpPost, CheckUserAccess(ActionBits.TopicDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _topicService.Delete(id);
            return Ok(msgRes);
        }
    }
}
