using QBic.Core.Models;
using System;

namespace QBicSamples.Models
{
    public class Course : DynamicClass
    {
        public virtual CourseType CourseType { get; set; }

        public virtual string Venue { get; set; }

        public virtual Country Country { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual CoursePresenter Presenter { get; set; }

        public virtual int CourseNumber { get; set; }

        public virtual string CourseId { get; set; }
    }
}