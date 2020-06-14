using QBic.Core.Models;

namespace QBicSamples.Models
{
    public class Country : DynamicClass
    {
        public virtual Region Region { get; set; }

        public virtual string Name { get; set; }
    }
}