using NHibernate;
using NHibernate.Linq;
using System.Linq;
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
        public override void DeleteOtherItems(ISession session, Manufacturer mainItem)
        {
            var vehicleModelItems = session.QueryOver<VehicleModel>().Where(x => x.ManufacturerId == mainItem.Id).List().ToList();
            vehicleModelItems.ForEach(item =>
            {
                // DataService does auditing
                DataService.TryDelete(session, item);
            });
            var editionItems = session.QueryOver<Edition>().Where(x => x.ManufacturerId == mainItem.Id).List().ToList();
            editionItems.ForEach(item =>
            {
                // DataService does auditing
                DataService.TryDelete(session, item);
            });
        }
    }
}