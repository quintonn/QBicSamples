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

        // put all the properties and fields above the constructor, and then all the functions below.
        // try use the same coding style
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
            // You should add ManufacturerId as a hidden input here
            // why?

            // Becuase this method and the initialize method is called when the screen is created.
            // but there is a lot of time until the user clicks the submit button on the screen.
            // If someone opens another screen, the ManufacturerId will change.
            // This is sort of a bug in the system, and that is why you should save the manufacturerid in a hidden input

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

            using (var session = DataService.OpenSession()) // don't create a new session.
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
            model.ManufacturerId = ManufacturerId; // ManufacturerId should be retrieved from the hiddenInput

            DataService.SaveOrUpdate(session, model);
               session.Flush(); // don't call session.flush. When you inherit from CoreModify this is done for you
              }

            // just return null
            return new List<IEvent>()
                {
                    new CancelInputDialog(),
                    new ExecuteAction(MenuNumber.ViewModels)
                };
        }
    }
}