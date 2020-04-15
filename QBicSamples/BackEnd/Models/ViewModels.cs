using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.BackEnd.Models
{
    public class ViewModels : ShowView
    {
        private DataService DataService { get; set; }
        private string ManufacturerId { get; set; }
        public ViewModels(DataService dataService)
        {
            DataService = dataService;
        }
        public override string Description => "View Models";
        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Name", "Name");

            columnConfig.AddLinkColumn("", "Id", "Details", MenuNumber.ViewEditions);
        }
        public override bool AllowInMenu => false;
        public override EventNumber GetId()
        {
            return MenuNumber.ViewModels;
        }
        public override IEnumerable GetData(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var data = CreateQuery(session, settings).Skip((settings.CurrentPage - 1) * settings.LinesPerPage)
                                                   .Take(settings.LinesPerPage)
                                                   .List<Model>()
                                                   .ToList();
                var results = TransformData(data);
                return results;
            }
        }
        public IEnumerable TransformData(IList<Model> data)
        {
            return data.Select(x => new
            {
                x.Id,
                x.ManufacturerId,
                x.Name,
            }).ToList();
        }

        public override int GetDataCount(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var result = CreateQuery(session, settings).RowCount();
                return result;
            }
        }
        public IQueryOver<Model> CreateQuery(ISession session, GetDataSettings settings)
        {
            ManufacturerId = GetParameter("Id", settings);
            var query = session.QueryOver<Model>()
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
                    Id = ManufacturerId
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
                new MenuItem("Back", MenuNumber.ViewManufacturers, dataForMenu["Data"]),
                new MenuItem("Add", MenuNumber.AddModel, dataForMenu["Data"])
            };
        }

    }
}