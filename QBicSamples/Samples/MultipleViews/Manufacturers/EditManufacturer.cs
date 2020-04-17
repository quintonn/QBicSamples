using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.MultipleViews.Manufacturers
{
    // You also need an EditManufacturer class
    public class EditManufacturer : ModifyManufacturer
    {
        public EditManufacturer(DataService dataService) : base(dataService, false)
        {
        }

        public override EventNumber GetId()
        {
            return MenuNumber.EditManufacturer;
        }
    }
}