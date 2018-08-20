using System.Collections.Generic;

namespace NasleGhalam.ViewModels.Topic
{
    public class TopicTreeViewModel
    {
        public TopicGetNameViewModel Topic { get; set; }

        public IEnumerable<TopicTreeViewModel> Children { get; set; }

    }
}
