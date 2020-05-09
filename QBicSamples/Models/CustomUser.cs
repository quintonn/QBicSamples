using WebsiteTemplate.Models;

namespace QBicSamples.Models
{
    public class CustomUser : UserBase
    {
        // In many cases UserName = Email. For those cases, the following is needed so NHibernate does not treat them as 2 separate fields
        // remove this code if you want UserName to be separate from the email
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

        // Add additional fields to this custom user type
        public virtual string Address { get; set; }

        public virtual int Age { get; set; }
    }

    // This class does not inherit from QBic's DynamicClass and will therefor require NHibernate mapping.
    // This is done by creating a mapping class. 
    //Thi class's mapping is CustomUserMap.cs 
}