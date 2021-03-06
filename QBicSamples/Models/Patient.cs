﻿using QBic.Core.Models;
using System;
using System.Globalization;

namespace QBicSamples.Models
{
    public class Patient : DynamicClass
    {
        public virtual string DoctorId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual DateTime BirthDay { get; set; }
    }
}