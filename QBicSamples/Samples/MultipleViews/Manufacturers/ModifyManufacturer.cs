using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.MultipleViews.Manufacturers
{
    public abstract class ModifyManufacturer : CoreModify<Manufacturer>
    {
        public ModifyManufacturer(DataService dataService, bool isNew) : base(dataService, isNew)
        {
        }
        public override string EntityName => "Manufacturer";
        public override EventNumber GetViewNumber()
        {
            return MenuNumber.ViewManufacturers;
        }
        public override List<InputField> InputFields()
        {
            var result = new List<InputField>();

            result.Add(new StringInput("Name", "Name", Item?.Name, null, true));

            return result;
        }
        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session)
        {
            var name = GetValue("Name");

            Manufacturer manufacturer;

            if (isNew)
            {
                manufacturer = new Manufacturer();
            }
            else
            {
                manufacturer = session.Get<Manufacturer>(id);
            }

            manufacturer.Name = name;

            DataService.SaveOrUpdate(session, manufacturer);

            return null;
        }
    }
}