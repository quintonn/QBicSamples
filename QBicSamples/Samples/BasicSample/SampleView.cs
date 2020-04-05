using QBicSamples.Models.Samples;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.MenuItems.Sample
{
    /// <summary>
    /// This is the quickest and easiest way to make a screen view.
    /// You have less control on the query and data etc, but for basic scenarios this is the best.
    /// </summary>
    public class TestView : CoreView<SampleModel>
    {
        public TestView(DataService dataService)
            : base(dataService)
        {
        }

        public override bool AllowInMenu => true;

        public override string Description => "Sample View";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("String Value", "StringValue");
            columnConfig.AddStringColumn("Number Value", "NumberValue");
            columnConfig.AddDateColumn("Date Value", "DateValue");
            columnConfig.AddStringColumn("Long Value", "LongTestValue");
            columnConfig.AddStringColumn("Category", "Category.Description"); // You can reference sub fields in the column name field

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.SampleEdit);

            columnConfig.AddButtonColumn("", "Id", "X",
                new UserConfirmation("Delete item?")
                {
                    OnConfirmationUIAction = MenuNumber.SampleDelete
                });
        }

        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Add", MenuNumber.SampleAdd)
            };
        }

        public override List<Expression<Func<SampleModel, object>>> GetFilterItems()
        {
            return new List<Expression<Func<SampleModel, object>>>()
            {
                x => x.StringValue,
                x => x.LongTestValue
            };
        }

        public override EventNumber GetId()
        {
            return MenuNumber.SampleView;
        }
    }
}