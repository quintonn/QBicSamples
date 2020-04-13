using QBic.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QBicSamples.Models
{
    // this is a studentabsentee class
    public class Invoice : DynamicClass
    {

        public virtual DateTime InvoiceDate { get; set; }
        public virtual string NameOfProduct { get; set; }
        public virtual int NumberOfUnits { get; set; }
        public virtual decimal UnitPrice { get; set; }
    }
}