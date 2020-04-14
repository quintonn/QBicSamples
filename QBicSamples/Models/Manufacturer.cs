using QBic.Core.Models;

namespace QBicSamples.Models
{
    // this is a car manufacturer class
    public class Manufacturer : DynamicClass
    {
        public virtual string Name { get; set; }
    }
}