using QBic.Core.Models;

namespace QBicSamples.Models
{
    public class Region : DynamicClass
    {
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}