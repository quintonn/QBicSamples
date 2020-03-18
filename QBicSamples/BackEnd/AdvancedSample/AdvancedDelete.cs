using QBicSamples.Models.Samples;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.BackEnd.AdvancedSample
{
    public class AdvancedDelete : DoSomething
    {
        private DataService DataService { get; set; }

        public AdvancedDelete(DataService dataService)
        {
            DataService = dataService;
        }

        public override bool AllowInMenu => false;

        public override string Description => "Delete Advanced Model";

        public override EventNumber GetId()
        {
            return MenuNumber.AdvancedDelete;
        }

        public override async Task<IList<IEvent>> ProcessAction()
        {
            var id = GetValue("Id");
            using (var session = DataService.OpenSession())
            {
                var model = session.Get<SampleModel>(id);

                DataService.TryDelete(session, model); // Do deletes this way to have it audited
                                                       //session.Delete(model); // This way won't be audited.

                session.Flush();
            }
            return new List<IEvent>()
            {
                new ShowMessage("Advanced Model deleted successfully"),
                new CancelInputDialog(),
                new ExecuteAction(MenuNumber.AdvancedView)
            };
        }
    }
}