using QBicSamples.SiteSpecific;
using QBicSamples.Models.Samples;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.MenuItems.Sample
{
    public class TestDelete : CoreDeleteAction<SampleModel>
    {
        public TestDelete(DataService dataService)
            : base(dataService)
        {
        }

        public override string EntityName => "Sample Model";

        public override EventNumber ViewNumber => MenuNumber.SampleView;

        public override EventNumber GetId()
        {
            return MenuNumber.SampleDelete;
        }
    }
}