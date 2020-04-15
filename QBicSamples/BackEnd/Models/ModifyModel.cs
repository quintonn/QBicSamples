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
using WebsiteTemplate.Utilities;

namespace QBicSamples.BackEnd.Models
{
    public abstract class ModifyModel : CoreModify<Model>
    {
        
        public ModifyModel(DataService dataService, bool isNew) : base(dataService, isNew)
        {
        }

        public override string EntityName => "Model";

        public override EventNumber GetViewNumber()
        {
            return MenuNumber.ViewModels;
        }
        private string ManufacturerId;
        public override List<InputField> InputFields()
        {
            var result = new List<InputField>();

            result.Add(new StringInput("Name", "Name", Item?.Name, null, true));

            return result;
        }
        public override async Task<InitializeResult> Initialize(string data)
        {
            using (var session = DataService.OpenSession())
            {
                var json = JsonHelper.Parse(data);
                ManufacturerId = json.GetValue("Id");
            }

            return new InitializeResult(true);
        }

        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session1)
        {
            var name = GetValue("Name");
            
            Model model;

            using (var session = DataService.OpenSession())
             {
            if (isNew)
            {
                model = new Model();
            }
            else
            {
                model = session.Get<Model>(id);
            }

            model.Name = name;
            model.ManufacturerId = ManufacturerId;

            DataService.SaveOrUpdate(session, model);
               session.Flush();
              }

            return new List<IEvent>()
                {
                    new CancelInputDialog(),
                    new ExecuteAction(MenuNumber.ViewModels)
                };
        }
    }
}