using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.MultipleViews.Editions
{
    public abstract class ModifyEdition : CoreModify<Edition>
    {
        public ModifyEdition(DataService dataService, bool isNew) : base(dataService, isNew)
        {
        }

        public override string EntityName => "Edition";

        public override EventNumber GetViewNumber()
        {
            return MenuNumber.ViewEditions;
        }

        public override List<InputField> InputFields()
        {
            var result = new List<InputField>();
            result.Add(new HiddenInput("Id", Item?.Id)); // Need to add it so it's available when doing update
            result.Add(new HiddenInput("ManufacturerId", Item?.ManufacturerId));
            result.Add(new HiddenInput("ModelId", Item?.ModelId));
            result.Add(new StringInput("EditionName", "Name", Item?.EditionName, null, true));
            result.Add(new DateInput("EditionYear", "Year", Item?.EditionYear, null, false));

            return result;
        }

        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session)
        {
            var name = GetValue("EditionName");
            var year = GetValue<DateTime>("EditionYear");
            var manufacturerId = GetValue("ManufacturerId");
            var modelId = GetValue("ModelId");

            if (String.IsNullOrEmpty(name))
            {
                return new List<IEvent>()
                    {
                        new ShowMessage("Name is mandatory")
                    };
            }

            Edition edition;

            if (isNew)
            {
                edition = new Edition();
                edition.ManufacturerId = manufacturerId;
                edition.ModelId = modelId;
            }
            else
            {
                edition = session.Get<Edition>(id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                edition.EditionName = name;
                edition.EditionYear = year;
            }

            DataService.SaveOrUpdate(session, edition);

            return null;
        }
    }
}