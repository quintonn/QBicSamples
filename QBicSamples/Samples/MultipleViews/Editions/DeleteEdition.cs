using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.BackEnd.MultipleViews.Editions
{
    public class DeleteEdition : CoreDeleteAction<Edition>
    {

        public string ManufacturerId;
        public string ModelId;
        public DeleteEdition(DataService dataService)
           : base(dataService)
        {
        }

        public override bool AllowInMenu => false;

        public override string EntityName => "Edition";

        public override EventNumber ViewNumber => MenuNumber.ViewEditions;

        public override EventNumber GetId()
        {
            return MenuNumber.DeleteEdition;
        }

        public override async Task<IList<IEvent>> ProcessAction()
        {
            var id = GetValue("Id");
            using (var session = DataService.OpenSession())
            {
                var edition = session.Get<Edition>(id);
                ManufacturerId = edition.ManufacturerId;
                ModelId = edition.ModelId;

                DataService.TryDelete(session, edition); // Do deletes this way to have it audited
                                                       //session.Delete(model); // This way won't be audited.

                session.Flush();
            }

            var data = new
            {
                data = new
                {
                    Id = ModelId,
                    ManufacturerId
                }
            };
            var json = JsonHelper.SerializeObject(data);

            return new List<IEvent>()
            {
                new ShowMessage("Edition deleted successfully"),
                new CancelInputDialog(),
                new ExecuteAction(MenuNumber.ViewEditions, json)
            };
        }
    }
}