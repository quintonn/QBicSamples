using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.MultipleViews.Models
{
    public class DeleteModel : CoreDeleteAction<Model>
    {
        public DeleteModel(DataService dataService)
           : base(dataService)
        {
        }

        public override string EntityName => "Model";

        public override EventNumber ViewNumber => MenuNumber.ViewModels;

        public override EventNumber GetId()
        {
            return MenuNumber.DeleteModel;
        }

    }
}