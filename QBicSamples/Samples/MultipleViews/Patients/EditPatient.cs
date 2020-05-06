using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.MultipleViews.Patients
{
    public class EditPatient : ModifyPatient
    {
        public EditPatient(DataService dataService) : base(dataService, false)
        {
        }

        public override EventNumber GetId()
        {
            return MenuNumber.EditPatient;
        }
    }
}