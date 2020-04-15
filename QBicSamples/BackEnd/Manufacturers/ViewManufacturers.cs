using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.BackEnd.Manufacturers
{
    public class ViewManufacturers : CoreView<Manufacturer>
    {
        public ViewManufacturers(DataService dataService) : base(dataService)
        {
           
        }

        public override bool AllowInMenu => true;

        public override string Description => "View Manufacturers";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            // Delete these blank lines in between calls to columnConfig
            columnConfig.AddStringColumn("Name", "Name");

            columnConfig.AddLinkColumn("", "Id", "Details", MenuNumber.ViewModels);

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditManufacturer);

            columnConfig.AddButtonColumn("", "Id", "X", new UserConfirmation("Delete manufacturer?", MenuNumber.DeleteManufacturer));
        }

        // You don't need to implement this function at all. 
        // these fields will be returned already
        public override IEnumerable TransformData(IList<Manufacturer> data)
        {
            return data.Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList();
        }

        public override List<Expression<Func<Manufacturer, object>>> GetFilterItems()
        {
            return new List<Expression<Func<Manufacturer, object>>>()
            {
                x => x.Name
            };
        }

        public override EventNumber GetId()
        {
            return MenuNumber.ViewManufacturers;
        }

        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Add", MenuNumber.AddManufacturer)
            };
        }
    }
}