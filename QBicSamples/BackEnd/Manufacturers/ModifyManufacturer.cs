using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
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

            // you don't need to create a new session here, you are already being given one in the parameters of this function
            // That is what CoreModify does for you, it creates the session so you don't have to re-do it 
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
                session.Flush(); // you also don't need to call flush. it is being done by CoreModify
            }


            // This is also done in CoreModify. You only need to return null if everything is fine.
            // If not, you should return ErrorMessage("Error message")            
            return new List<IEvent>()
                {
                    new CancelInputDialog(),
                    new ExecuteAction(MenuNumber.ViewManufacturers)
                };
        }
    }
}