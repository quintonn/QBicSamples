using System;
using WebsiteTemplate.Models;

namespace Website.Models
{
    public class PatientUser : UserBase
    {
        public override string UserName
        {
            get
            {
                return Email;
            }
            set
            {
                Email = value;
            }
        }

        public virtual string Name { get; set; }

        public virtual string Surname { get; set; }

        public virtual DateTime? FirstLoginDate { get; set; }

    }
}