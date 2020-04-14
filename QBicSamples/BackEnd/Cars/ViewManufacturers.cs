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

namespace QBicSamples.BackEnd.Cars
{
    public class ViewManufacturers : CoreView<CarEdition>
    {
        public ViewManufacturers(DataService dataService) : base(dataService)
        {
        }

        public override bool AllowInMenu => true;

        public override string Description => "View Manufacturers";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Id", "Id");
            columnConfig.AddStringColumn("Name", "Name");

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditManufacturer);

            columnConfig.AddButtonColumn("", "Id", "X", new UserConfirmation("Delete manufacturer?", MenuNumber.DeleteManufacturer));
        }

        public override IEnumerable TransformData(IList<CarEdition> data)
        {
            return data.Select(x => new
            {
                x.Id,
                x.EditionName,
            }).ToList();
        }

        public override List<Expression<Func<CarEdition, object>>> GetFilterItems()
        {
            return new List<Expression<Func<CarEdition, object>>>()
            {
                x => x.EditionName
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