using QBic.Core.Data.BaseTypes;
using QBic.Core.Models;
using System;

namespace QBicSamples.Models.Samples
{
    public class SampleModel : DynamicClass
    {
        public virtual string StringValue { get; set; }

        public virtual int NumberValue { get; set; }

        public virtual DateTime? DateValue { get; set; }

        public virtual LongString LongTestValue { get; set; }

        public virtual SampleCategory Category { get; set; }
    }
}