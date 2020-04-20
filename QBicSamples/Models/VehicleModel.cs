using QBic.Core.Models;

namespace QBicSamples.Models
{
    public class VehicleModel : DynamicClass
    {
        public virtual string ManufacturerId { get; set; }
        public virtual string Name { get; set; }
    }
}