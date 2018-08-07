using System.Web.Http;
using NasleGhalam.Common;
using NasleGhalam.ServiceLayer.Services;
using NasleGhalam.WebApi.FilterAttribute;
using NasleGhalam.ViewModels.Topic;
using NasleGhalam.WebApi.Extentions;

namespace NasleGhalam.WebApi.Controllers
{
    /// <inheritdoc />
	/// <author>
	///     name: 
	///     date: 
	/// </author>
	public class TopicController : ApiController
	{
        private readonly TopicService _topicService;
		public TopicController(TopicService topicService)
        {
            _topicService = topicService;
        }


		[HttpGet, CheckUserAccess(ActionBits.TopicReadAccess)]
        public IHttpActionResult GetAll()
        {
            return Ok(_topicService.GetAll());
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
        public IHttpActionResult Create(TopicViewModel topicViewModel)
        {
            var msgRes = _topicService.Create(topicViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType,
                Id = msgRes.Id
            });
        }


        [HttpPost]
        [CheckUserAccess(ActionBits.TopicUpdateAccess)]
        [CheckModelValidation]
        public IHttpActionResult Update(TopicViewModel topicViewModel)
        {
            var msgRes = _topicService.Update(topicViewModel);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }


        [HttpPost, CheckUserAccess(ActionBits.TopicDeleteAccess)]
        public IHttpActionResult Delete(int id)
        {
            var msgRes = _topicService.Delete(id);
            return Ok(new MessageResultApi
            {
                Message = msgRes.FaMessage,
                MessageType = msgRes.MessageType
            });
        }
	}
}
