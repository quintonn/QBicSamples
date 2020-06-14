using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using QBic.Core.Models;

namespace QBicSamples.Models
{
    public class CourseAttendee : DynamicClass
    {
        public virtual Course Course { get; set; }
        public virtual string Name { get; set; }

        public virtual string Email { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual Gender Gender { get; set; }

        public virtual Region Region{ get; set; }
    }
}