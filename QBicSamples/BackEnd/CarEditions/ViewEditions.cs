using NHibernate;
using NHibernate.Criterion;
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

namespace QBicSamples.BackEnd.Cars
{
    /// <summary>
    /// This is the more advanced way to create a screen view.
    /// More code is required but you have full control on the columns and data being shown.
    /// </summary>
    public class ViewEditions : ShowView
    {
        private DataService DataService { get; set; }
        private string ManufacturerId { get; set; }
        private string ModelId { get; set; }


        public ViewEditions(DataService dataService)
        {
            DataService = dataService;
        }

        public override bool AllowInMenu => false;

        public override string Description => "View car editions";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Name", "EditionName");
            columnConfig.AddStringColumn("Year", "EditionYear");
            columnConfig.AddStringColumn("Price", "EditionPrice");

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditEdition);

        }

        public override IEnumerable GetData(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var data = CreateQuery(session, settings).Skip((settings.CurrentPage - 1) * settings.LinesPerPage)
                                                   .Take(settings.LinesPerPage)
                                                   .List<CarEdition>()
                                                   .ToList();
                var results = TransformData(data);
                return results;
            }
        }

        public IEnumerable TransformData(IList<CarEdition> data)
        {
            var results = new List<object>();
            using (var session = DataService.OpenSession())
            {
                results = data.Select(d => (object)new
                {
                   
                }).ToList();
            }
            
            return results;
        }

        public override int GetDataCount(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var result = CreateQuery(session, settings).RowCount();
                return result;
            }
        }

        public IQueryOver<CarEdition> CreateQuery(ISession session, GetDataSettings settings)
        {
            ManufacturerId = GetParameter("ManufacturerId", settings);
            ModelId = GetParameter("ModelId", settings);

            var query = session.QueryOver<CarEdition>();

            query = query.Where(c => c.ManufacturerId == ManufacturerId &&
                                     c.ModelId == ModelId);
            return query;
        }

        public override EventNumber GetId()
        {
            return MenuNumber.ViewEditions;
        }

        public override Dictionary<string, string> DataForGettingMenu
        {
            get
            {
                var data = new
                {
                    ManufactId = ManufacturerId,
                    ModId = ModelId
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
                new MenuItem("Back", MenuNumber.ViewModels, dataForMenu["Data"])

            };
        }
    }
}