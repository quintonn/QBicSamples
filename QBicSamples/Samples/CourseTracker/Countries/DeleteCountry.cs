using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.CourseTracker.Countries
{
    public class DeleteCountry : CoreDeleteAction<Country>
    {
        public DeleteCountry(DataService dataService) : base(dataService)
        {
        }

        public override string EntityName => "Country";

        public override EventNumber ViewNumber => MenuNumber.ViewCountries;

        public override EventNumber GetId()
        {
            return MenuNumber.DeleteCountry;
        }
    }
}