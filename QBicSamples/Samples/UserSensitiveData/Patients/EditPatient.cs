using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Data;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.UserSensitiveData.Patients
{
    public class EditPatient : ModifyPatient
    {
        public EditPatient(DataService dataService, UserContext userContext) : base(dataService, false)
        {
        }
        public override EventNumber GetId()
        {
            return MenuNumber.EditPatient;
        }
    }
}