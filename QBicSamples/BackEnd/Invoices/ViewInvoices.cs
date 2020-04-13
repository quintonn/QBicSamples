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

namespace QBicSamples.BackEnd.Invoices
{
    public class ViewSubjects : CoreView<Invoice>
    {
        public ViewSubjects(DataService dataService) : base(dataService)
        {
        }

        public override bool AllowInMenu => true;

        public override string Description => "View Invoices";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddDateColumn("Ivoice Date", "InvoiceDate");
            columnConfig.AddStringColumn("Name Of Product", "NameOfProduct");
            columnConfig.AddStringColumn("Number Of Units", "NumberOfUnits");
            columnConfig.AddStringColumn("Unit Price", "Price");

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditInvoice);

            columnConfig.AddButtonColumn("", "Id", "X", new UserConfirmation("Delete item?", MenuNumber.DeleteInvoice));
        }

        public override List<Expression<Func<Invoice, object>>> GetFilterItems()
        {
            return new List<Expression<Func<Invoice, object>>>()
            {
                x => x.NameOfProduct
            };
        }

        public override EventNumber GetId()
        {
            return MenuNumber.ViewInvoices;
        }

        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Add", MenuNumber.AddInvoice)
            };
        }
    }
}