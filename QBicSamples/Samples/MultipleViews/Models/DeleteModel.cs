using NHibernate;
using NHibernate.Linq;
using System.Linq;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
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

        public string ParameterToPassToView()
        {
           
                var data = new
                {
                    data = new
                    {
                        Id = ManufacturerId
                    }
                };
                var json = JsonHelper.SerializeObject(data);
                return json;
           
        }
        public override void DeleteOtherItems(ISession session, VehicleModel mainItem)
        {
            session.Query<Edition>().Where(x => x.ModelId == mainItem.Id).Delete();
        }
    }
}