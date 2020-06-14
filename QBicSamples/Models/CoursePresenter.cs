using QBic.Core.Models;

namespace QBicSamples.Models
{
    public class CoursePresenter : DynamicClass
    {
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }
    }
}