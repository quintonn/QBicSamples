using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.Samples.MultipleViews.Models
{
    public abstract class ModifyModel : GetInput
    {
        private bool IsNew { get; set; }

        public string ModelId;

        public string ManufacturerId;
        public ModifyModel(DataService dataService, bool isNew)
        {
            DataService = dataService;
            IsNew = isNew;
        }

        private Model Model { get; set; }

        private DataService DataService { get; set; }

        public override bool AllowInMenu => false;

        public override string Description => IsNew ? "Add Model" : "Edit Model";

        public override async Task<InitializeResult> Initialize(string data)
        {
            var json = JsonHelper.Parse(data);

            ModelId = json.GetValue("Id");
            ManufacturerId = json.GetValue("ManufacturerId");

            IsNew = String.IsNullOrWhiteSpace(ModelId);
            if (IsNew)
            {
                Model = new Model();
                Model.ManufacturerId = ManufacturerId;
            }
            else
            {
                using (var session = DataService.OpenSession())
                {
                    Model = session.Get<Model>(ModelId);
                }
            }

            return new InitializeResult(true);
        }

        public override IList<InputField> GetInputFields()
        {
            var result = new List<InputField>();

            result.Add(new HiddenInput("Id", Model?.Id)); // Need to add it so it's available when doing update
            result.Add(new HiddenInput("ManufacturerId", Model?.ManufacturerId));
            result.Add(new StringInput("Name", "Name", Model?.Name, null, true));

            return result;
        }

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
                var name = GetValue("Name");
                var manufacturerId = GetValue("ManufacturerId");

                if (String.IsNullOrEmpty(name))
                {
                    return new List<IEvent>()
                    {
                        new ShowMessage("Name is mandatory")
                    };
                }

                using (var session = DataService.OpenSession())
                {
                    Model dbItem;
                    if (IsNew)
                    {
                        dbItem = new Model();
                        dbItem.ManufacturerId = manufacturerId;
                    }
                    else
                    {
                        dbItem = session.Get<Model>(id);
                    }

                    if (!String.IsNullOrEmpty(name))
                    {
                        dbItem.Name = name;
                    }

                    DataService.SaveOrUpdate(session, dbItem); // Using saveOrUpdate adds this task to audit log
                    //session.SaveOrUpdate(dbItem); // this won't be audited

                    session.Flush();
                }


                var data = new
                {
                    data = new
                    {
                        Id = ManufacturerId  
                    }
                };
                var json = JsonHelper.SerializeObject(data);


                return new List<IEvent>()
                {
                    new ShowMessage("Model successfully " + (IsNew ? "added" : "modified")),
                    new CancelInputDialog(),
                    new ExecuteAction(MenuNumber.ViewModels, json)
                };
            }
            else
            {
                return null;
            }
        }
    }
}