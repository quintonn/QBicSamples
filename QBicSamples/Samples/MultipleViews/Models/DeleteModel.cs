using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.BackEnd.MultipleViews.Models
{
    public class DeleteModel : CoreDeleteAction<VehicleModel>
    {

        public string ManufacturerId;
        public DeleteModel(DataService dataService)
           : base(dataService)
        {
        }
        public override bool AllowInMenu => false;
        public override string EntityName => "VehicleModel";
        public override EventNumber ViewNumber => MenuNumber.ViewModels;
        public override EventNumber GetId()
        {
            return MenuNumber.DeleteModel;
        }

        public override async Task<IList<IEvent>> ProcessAction()
        {
            var id = GetValue("Id");
            using (var session = DataService.OpenSession())
            {
                var model = session.Get<VehicleModel>(id);
                ManufacturerId = model.ManufacturerId;

                DataService.TryDelete(session, model); // Do deletes this way to have it audited
                                                       //session.Delete(model); // This way won't be audited.

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
                new ShowMessage("Model deleted successfully"),
                new CancelInputDialog(),
                new ExecuteAction(MenuNumber.ViewModels, json)
            };
        }
    }
}