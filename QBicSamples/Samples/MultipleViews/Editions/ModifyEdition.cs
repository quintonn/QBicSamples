using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel.Channels;
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
        public string EditionId;

        public string ModelId;

        public string ManufacturerId;
        public ModifyEdition(DataService dataService, bool isNew) : base(dataService, isNew)
        {
        }
        private Edition Edition { get; set; }
        public override bool AllowInMenu => false;
        public override string EntityName => "Edition";
        public override EventNumber GetViewNumber()
        {
            return MenuNumber.ViewEditions;
        }
        public override async Task<InitializeResult> Initialize(string data)
        {
            var json = JsonHelper.Parse(data);

            EditionId = json.GetValue("Id");
            ModelId = json.GetValue("ModelId");
            ManufacturerId = json.GetValue("ManufacturerId");

            IsNew = String.IsNullOrWhiteSpace(EditionId);
            if (IsNew)
            {
                Edition = new Edition();
                Edition.ManufacturerId = ManufacturerId;
                Edition.ModelId = ModelId;
                Edition.EditionYear = DateTime.ParseExact("1990-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                using (var session = DataService.OpenSession())
                {
                    Edition = session.Get<Edition>(EditionId);
                }
            }

            return new InitializeResult(true);
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
        public abstract Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session);
        public override async Task<IList<IEvent>> ProcessAction(int actionNumber)
        {
            if (actionNumber == 1) // User clicked cancel
            {
                return new List<IEvent>()
                {
                    new CancelInputDialog()
                };
            }
            else if (actionNumber == 0) // user clicked submit
            {
                var id = GetValue("Id");
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

                using (var session = DataService.OpenSession())
                {
                    Edition dbItem;
                    if (IsNew)
                    {
                        dbItem = new Edition();
                        dbItem.ManufacturerId = manufacturerId;
                        dbItem.ModelId = modelId;
                    }
                    else
                    {
                        dbItem = session.Get<Edition>(id);
                    }

                    if (!String.IsNullOrEmpty(name))
                    {
                        dbItem.EditionName = name;
                        dbItem.EditionYear = year;
                    }

                    DataService.SaveOrUpdate(session, dbItem); // Using saveOrUpdate adds this task to audit log
                    //session.SaveOrUpdate(dbItem); // this won't be audited

                    session.Flush();
                }


                var data = new
                {
                    data = new
                    {
                        Id = ModelId,
                        ManufacturerId,
                    }
                };
                var json = JsonHelper.SerializeObject(data);


                return new List<IEvent>()
                {
                    new ShowMessage("Edition successfully " + (IsNew ? "added" : "modified")),
                    new CancelInputDialog(),
                    new ExecuteAction(MenuNumber.ViewEditions, json)
                };
            }
            else
            {
                return null;
            }
        }
    }
}