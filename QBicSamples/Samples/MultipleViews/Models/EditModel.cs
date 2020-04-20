using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.MultipleViews.Models
{
    public class EditModel : ModifyModel
    {
        public EditModel(DataService dataService) : base(dataService, false)
        {
        }

        public override EventNumber GetId()
        {
            return MenuNumber.EditModel;
        }
    }
}