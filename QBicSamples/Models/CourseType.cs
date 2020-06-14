using QBic.Core.Models;

namespace QBicSamples.Models
{
    public class CourseType : DynamicClass
    {
        public virtual string Name { get; set; }

        public virtual string Description { get; set; }
    }
}