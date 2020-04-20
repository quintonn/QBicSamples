using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.MultipleViews.Manufacturers
{
    public class DeleteManufacturer : CoreDeleteAction<Manufacturer>
    {
        public DeleteManufacturer(DataService dataService)
           : base(dataService)
        {
        }

        public override string EntityName => "Manufacturer";

        public override EventNumber ViewNumber => MenuNumber.ViewManufacturers;

        public override EventNumber GetId()
        {
            return MenuNumber.DeleteManufacturer;
        }

    }
}