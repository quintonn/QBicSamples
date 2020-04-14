using QBic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QBicSamples.Models
{
    // this is a car edition class
    public class CarEdition : DynamicClass
    {
        public virtual string ManufacturerId { get; set; }
        public virtual string ModelId { get; set; }
        public virtual string EditionName { get; set; }
        public virtual DateTime EditionYear { get; set; }
        public virtual decimal Price { get; set; }
    }
}