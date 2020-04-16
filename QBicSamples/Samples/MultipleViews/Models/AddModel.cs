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
    public class AddModel : GetInput
    {
        private DataService DataService { get; set; }
        public override bool AllowInMenu => false;
        public AddModel(DataService dataService)
        {
            DataService = dataService;
        }


        public override string Description => "Model";
        public override EventNumber GetId()
        {
            return MenuNumber.AddModel;
        }
        private Model Model;

        private string modelId;

        private string manufacturerId;

        private string manufacturerName;

        public override IList<InputField> GetInputFields()
        {
            var result = new List<InputField>();

            result.Add(new StringInput("Name", "Name", Model?.Name, null, true));
            return result;
        }
        public override async Task<InitializeResult> Initialize(string data)
        {
            using (var session = DataService.OpenSession())
            {
                var json = JsonHelper.Parse(data);
                modelId = json.GetValue("Id");
                if (modelId == "")
                {
                    manufacturerId = json.GetValue("CarManufacturerId");
                    Model = new Model();
                }
                else
                {
                    manufacturerId = json.GetValue("ManufacturerId");
                    Model = session.Get<Model>(modelId);
                    
                }
            }

            return new InitializeResult(true);
        }
        public override async Task<IList<IEvent>> ProcessAction(int actionNumber)
        {
            
            if (actionNumber == 1)
            {
                return new List<IEvent>()
                {
                    new CancelInputDialog()
                };
            }
            else if (actionNumber == 0)
            {
                var name = GetValue("Name");
                using (var session = DataService.OpenSession())
                {
                    var models = session.QueryOver<Model>()
                                            .Where(x => x.ManufacturerId == manufacturerId)
                                            .List()
                                            .ToList();
                     
                    var dbItem = models.Where(x => x.Id == modelId).SingleOrDefault();
                    if (dbItem == null)
                    {
                        dbItem = new Model();
                        dbItem.Name = name;
                        dbItem.ManufacturerId = manufacturerId;
                    }
                    else
                    {
                        dbItem.Name = name;
                    }

                    DataService.SaveOrUpdate(session, dbItem); // Using saveOrUpdate adds this task to audit log
                    session.Flush();
                }
                var data = new
                {
                    data = new
                    {
                        Id = manufacturerId,
                    }
                };
                var json = JsonHelper.SerializeObject(data);

                return new List<IEvent>()
                {
                    new CancelInputDialog(),
                    new ExecuteAction(MenuNumber.ViewModels, json)
                };

            }
            return null;
        }
    }
}