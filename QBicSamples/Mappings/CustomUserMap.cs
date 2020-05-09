using QBic.Core.Mappings;
using QBicSamples.Models;

namespace QBicSamples.Mappings
{
    public class CustomUserMap : BaseClassMap<CustomUser>
    {
        public CustomUserMap()
        {
            Table("CustomUser");


            //Map(x => x.UserName).Not.Nullable();  // enable if username does not return email in CustomUser class
            //Map(x => x.UserStatus).Not.Nullable();  // can enable this if you need or want it.
            Map(x => x.Email).Not.Nullable(); ;
            Map(x => x.EmailConfirmed);
            Map(x => x.PasswordHash).Not.Nullable();
            Map(x => x.Address).Not.Nullable();
            Map(x => x.Age).Not.Nullable();
        }
    }
}