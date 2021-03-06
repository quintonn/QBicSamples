﻿using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.MultipleViews.Models
{
    public class AddModel : ModifyModel
    {
        public AddModel(DataService dataService) : base(dataService, true)
        {
        }

        public override EventNumber GetId()
        {
            return MenuNumber.AddModel;
        }
    }
}