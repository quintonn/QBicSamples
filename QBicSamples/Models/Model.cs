using QBic.Core.Models;

namespace QBicSamples.Models
{
    // this is a model edition class
    public class Model : DynamicClass
    {
        public virtual string ManufacturerId { get; set; }
        public virtual string Name { get; set; }
    }
}