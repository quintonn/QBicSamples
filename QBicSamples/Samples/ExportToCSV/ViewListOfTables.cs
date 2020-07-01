using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using NHibernate;
using NHibernate.Criterion;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.ExportToCsv
{
    public class ViewListOfTables : ShowView
    {
        private DataService DataService { get; set; }
        public ViewListOfTables(DataService dataService)
        {
            DataService = dataService;
        }
        public override bool AllowInMenu => true;

        public override string Description => "View List of Tables";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Name", "Name");
            columnConfig.AddLinkColumn("", "Id", "Export", MenuNumber.ExportToCSV);
        }

        public override IEnumerable GetData(GetDataSettings settings)
        {
            var data = GetAllTablesAsync().Result;
            return data;
        }

        public override int GetDataCount(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var result = GetAllTablesAsync().Result.Count;
                return result;

            }
        }

        public override EventNumber GetId()
        {
            return MenuNumber.ViewTables;
        }

        public async Task<List<TableName>> GetAllTablesAsync()
        {
            using (var session = DataService.OpenSession())
            { 
                string queryString = $"SELECT name FROM appData WHERE type = 'table'";
                return await session.GetAsync<List<TableName>>(queryString).ConfigureAwait(false);
            }
        }

        
    }
}