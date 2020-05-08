using QBicSamples.SiteSpecific;
using WebsiteTemplate.Data;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.UserSensitiveData.Patients
{
    public class AddPatient : ModifyPatient
    {
        private UserContext UserContext { get; set; }
        public AddPatient(DataService dataService, UserContext userContext) : base(dataService, true, userContext)
        {
            UserContext = userContext;
        }

        public override EventNumber GetId()
        {
            return MenuNumber.AddPatient;
        }
    }
}