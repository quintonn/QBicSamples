using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.MultipleViews.Manufacturers
{
    // You also need an EditManufacturer class
    public class AddManufacturer : ModifyManufacturer
    {
        public AddManufacturer(DataService dataService) : base(dataService, true)
        {
        }

        public override EventNumber GetId()
        {
            return MenuNumber.AddManufacturer;
        }
    }
}