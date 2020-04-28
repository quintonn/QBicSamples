using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Globalization; // remove unused "usings" // there is a right-click option and shortcut to do this
x.7
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.Samples.MultipleViews.Editions
{
    public abstract class ModifyEdition : CoreModify<Edition>
    {
        public string ModelId;

        public string ManufacturerId;
        private Edition Edition { get; set; }
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
        public override async Task<InitializeResult> Initialize(string data)
        {
            var json = JsonHelper.Parse(data);

            ModelId = json.GetValue("ModelId");  // x.2
            ManufacturerId = json.GetValue("ManufacturerId");  // x.1

            return await base.Initialize(data);
        }
        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session)
        {
            var name = GetValue("EditionName");
            var year = GetValue<DateTime>("EditionYear");

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
                edition.ManufacturerId = ManufacturerId; 1. this should be GetValue("ManufacturerId"); // see x.1 above
                edition.ModelId = ModelId;   2. this should be GetValue("ModelId"); // see x.2 above
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

        public override string GetParameterToPassToView()
        {
            var data = new
            {
                data = new
                {
                    Id = ModelId,
                    ManufacturerId,
                }
            };
            var json = JsonHelper.SerializeObject(data);
            return json;
        }
    }
}