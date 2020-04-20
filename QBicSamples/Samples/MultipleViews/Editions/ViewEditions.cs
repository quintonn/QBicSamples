﻿using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.Samples.MultipleViews.Editions
{
    public class ViewEditions : ShowView
    {
        private string ManufacturerId { get; set; }
        private string ModelId { get; set; }
        private DataService DataService { get; set; }
        public ViewEditions(DataService dataService)
        {
            DataService = dataService;
        }

        public override bool AllowInMenu => false;

        public override string Description => "View Editions";
        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Name", "EditionName");

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditEdition);

            columnConfig.AddButtonColumn("Details", "Id", "...", MenuNumber.DetailsEdition, new ShowHideColumnSetting()
            {
                Display = ColumnDisplayType.Hide,
                Conditions = new List<Condition>()
                {
                    new Condition("Category", Comparison.Equals, "Special") // Can be used to hide buttons for certain types of data
                }
            });
            columnConfig.AddButtonColumn("", "Id", "X", new UserConfirmation("Delete edition?", MenuNumber.DeleteEdition));

        }
        public override EventNumber GetId()
        {
            return MenuNumber.ViewEditions;
        }
        public override IEnumerable GetData(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var editionslist = CreateQuery(session, settings).Skip((settings.CurrentPage - 1) * settings.LinesPerPage)
                                                   .Take(settings.LinesPerPage)
                                                   .List<Edition>()
                                                   .ToList();
                return editionslist;
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
        public IQueryOver<Edition> CreateQuery(ISession session, GetDataSettings settings)
        {
            ManufacturerId = GetParameter("ManufacturerId", settings);
            ModelId = GetParameter("Id", settings);

            var query = session.QueryOver<Edition>()
                               .Where(x => x.ManufacturerId == ManufacturerId && x.ModelId == ModelId)
                               .OrderBy(x => x.EditionName).Asc;

            return query;
        }
        public override Dictionary<string, string> DataForGettingMenu
        {
            get
            {
                var adddata = new
                {
                    ManufacturerId,
                    ModelId
                };
                var backdata = new
                {
                    Id = ManufacturerId
                };

                return new Dictionary<string, string>()
                {
                    { "AddData", JsonHelper.SerializeObject(adddata) },
                    { "BackData", JsonHelper.SerializeObject(backdata)}
                };
            }
        }
        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {

            return new List<MenuItem>()
            {
                new MenuItem("Back", MenuNumber.ViewModels, dataForMenu["BackData"]),
                new MenuItem("Add", MenuNumber.AddEdition, dataForMenu["AddData"])
            };
        }
    }
}