using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.BackEnd.Manufacturers
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

        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session1)
        {
            var name = GetValue("Name");

            Manufacturer manufacturer;

            using (var session = DataService.OpenSession())
            {
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
                session.Flush();
            }

            return new List<IEvent>()
                {
                    new CancelInputDialog(),
                    new ExecuteAction(MenuNumber.ViewManufacturers)
                };
        }
    }
}