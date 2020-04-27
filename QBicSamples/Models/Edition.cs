using QBic.Core.Models;
using System;
using System.Globalization;

namespace QBicSamples.Models
{
    // if your comment is just naming the class, it is not needed. only add comments if they add information that is not obvious.

    // Also, please move the model class for the entire project so they are together. 
    // your models are fine, but move the Samples models into this same folder.
    // The project is already a samples project so we don't need "Samples" folders anymore.
    // Same for the Samples folder, the content can go into BackEnd with your other code.

    // this is a car edition class
    public class Edition : DynamicClass
    {
        public virtual string ManufacturerId { get; set; }
        public virtual string ModelId { get; set; }
        public virtual string EditionName { get; set; }
        public virtual DateTime EditionYear { get; set; } = DateTime.ParseExact("1990-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
    }
}