using NHibernate;
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
using WebsiteTemplate.Utilities;

namespace QBicSamples.Samples.MultipleViews.Models
{
    public class ViewModels : CoreView<VehicleModel>
    {
        private string ManufacturerId { get; set; }
        public ViewModels(DataService dataService) : base(dataService)
        {

        }
        public override bool AllowInMenu => false;

        public override string Description => "View Models"; 
        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Name", "Name");
            columnConfig.AddLinkColumn("", "Id", "Editions", MenuNumber.ViewEditions);
            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditModel);
            columnConfig.AddButtonColumn("", "Id", "X", new UserConfirmation("Delete model?", MenuNumber.DeleteModel));
        }
        public override EventNumber GetId()
        {
            return MenuNumber.ViewModels;
        }
        public override IEnumerable GetData(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var modelslist = CreateQuery(session, settings).Skip((settings.CurrentPage - 1) * settings.LinesPerPage)
                                                   .Take(settings.LinesPerPage)
                                                   .List<VehicleModel>()
                                                   .ToList();
                return modelslist;
            }
        }
        public override int GetDataCount(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var result = CreateQuery(session, settings).RowCount();
                return result;
            }
        }
        public IQueryOver<VehicleModel> CreateQuery(ISession session, GetDataSettings settings)
        {
            ManufacturerId = GetParameter("Id", settings);
            var query = session.QueryOver<VehicleModel>()
                               .Where(x => x.ManufacturerId == ManufacturerId)
                               .OrderBy(x => x.Name).Asc;
            return query;
        }
        public override Dictionary<string, string> DataForGettingMenu
        {
            get
            {
                var data = new
                {
                    ManufacturerId,
                };

                return new Dictionary<string, string>()
                {
                    { "Data", JsonHelper.SerializeObject(data) }
                };
            }
        }
        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Back", MenuNumber.ViewManufacturers),
                new MenuItem("Add", MenuNumber.AddModel, dataForMenu["Data"])
            };
        }
        public override List<Expression<Func<VehicleModel, object>>> GetFilterItems()
        {
            return new List<Expression<Func<VehicleModel, object>>>()
            {
                x => x.Name
            };
        }
    }
}