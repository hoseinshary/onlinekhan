using System.Collections.Generic;

namespace NasleGhalam.ViewModels.Topic
{
    public class TopicTreeViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public IEnumerable<TopicTreeViewModel> Children { get; set; }

    }
}
