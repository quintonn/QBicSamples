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
        public override string ParametersToPassToView
        {
            get
            {
                var manufacturerId = GetValue("ManufacturerId");
                var modelId = GetValue("ModelId");
                var data = new
                {
                    data = new
                    {
                        Id = modelId,
                        ManufacturerId = manufacturerId
                    }
                };
                var json = JsonHelper.SerializeObject(data);
                return json;
            }
        }
    }
}