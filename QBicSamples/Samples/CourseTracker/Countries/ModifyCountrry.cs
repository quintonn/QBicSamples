using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.CourseTracker.Countries
{
    public abstract class ModifyCountrry : CoreModify<Country>
    {
        public ModifyCountrry(DataService dataService, bool isNew) : base(dataService, isNew)
        {
        }

        public override string EntityName => "Country";

        public override EventNumber GetViewNumber()
        {
            return MenuNumber.ViewCountries;
        }

        public override List<InputField> InputFields()
        {
            var results = new List<InputField>();
            results.Add(new DataSourceComboBoxInput<Region>("Region", "Region", x => x.Id, x => x.Name, Item?.Region?.Id, null, null, x => x.Name, true, false)
            {
                Mandatory = true,
            });
            results.Add(new StringInput("Name", "Name", Item?.Name, null, true));
            return results;
        }

        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session)
        {
            Country dbItem;
            if (isNew)
            {
                dbItem = new Country();
            }
            else
            {
                dbItem = session.Get<Country>(id);
            }

            dbItem.Name = GetValue("Name");
            dbItem.Region = GetDataSourceValue<Region>("Region");

            DataService.SaveOrUpdate(session, dbItem);

            return null;
        }
    }
}