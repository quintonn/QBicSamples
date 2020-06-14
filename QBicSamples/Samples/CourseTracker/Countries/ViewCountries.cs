using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.CourseTracker.Countries
{
    public class ViewCountries : CoreView<Country>
    {
        public ViewCountries(DataService dataService) : base(dataService)
        {
        }

        public override bool AllowInMenu => true;

        public override string Description => "View Countries";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Name", "Name");
            columnConfig.AddStringColumn("Region", "Region.Name");

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditCountry);

            columnConfig.AddButtonColumn("", "Id", "X", new UserConfirmation("Delete item?", MenuNumber.DeleteCountry));
        }

        public override List<Expression<Func<Country, object>>> GetFilterItems()
        {
            return new List<Expression<Func<Country, object>>>()
            {
                x => x.Name
            };
        }

        public override EventNumber GetId()
        {
            return MenuNumber.ViewCountries;
        }

        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Add", MenuNumber.AddCountry)
            };
        }
    }
}