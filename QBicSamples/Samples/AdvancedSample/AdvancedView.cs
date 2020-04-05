using NHibernate;
using NHibernate.Criterion;
using QBicSamples.Models.Samples;
using QBicSamples.SiteSpecific;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;

namespace QBicSamples.BackEnd.AdvancedSample
{
    /// <summary>
    /// This is the more advanced way to create a screen view.
    /// More code is required but you have full control on the columns and data being shown.
    /// </summary>
    public class AdvancedView : ShowView
    {
        private DataService DataService { get; set; }

        public AdvancedView(DataService dataService)
        {
            DataService = dataService;
        }

        public override bool AllowInMenu => true;

        public override string Description => "Advanced View";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("String Value", "StringValue");
            columnConfig.AddStringColumn("Number Value", "NumberValue");
            columnConfig.AddDateColumn("Date Value", "DateValue");
            columnConfig.AddStringColumn("Long Value", "LongTestValue");
            columnConfig.AddStringColumn("Category", "Category");

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.AdvancedEdit);

            columnConfig.AddButtonColumn("", "Id", "X",
                new UserConfirmation("Delete item?")
                {
                    OnConfirmationUIAction = MenuNumber.AdvancedDelete
                });
            columnConfig.AddButtonColumn("Details", "Id", "...", MenuNumber.AdvancedDetails, new ShowHideColumnSetting()
            {
                Display = ColumnDisplayType.Hide,
                Conditions = new List<Condition>()
                {
                    new Condition("Category", Comparison.Equals, "Special") // Can be used to hide buttons for certain types of data
                }
            });
        }

        public override IEnumerable GetData(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var data = CreateQuery(session, settings).Skip((settings.CurrentPage - 1) * settings.LinesPerPage)
                                                   .Take(settings.LinesPerPage)
                                                   .List<SampleModel>()
                                                   .ToList();
                var results = TransformData(data);
                return results;
            }
        }

        public IEnumerable TransformData(IList<SampleModel> data)
        {
            var results = new List<object>();
            using (var session = DataService.OpenSession())
            {
                results = data.Select(d => (object)new
                {
                    StringValue = d.StringValue,
                    NumberValue = d.NumberValue,
                    DateValue = d?.DateValue?.ToShortDateString(),
                    LongTestValue = d.LongTestValue,
                    Category = d.Category?.Description,
                    Id = d.Id
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

        public IQueryOver<SampleModel> CreateQuery(ISession session, GetDataSettings settings)
        {
            var query = session.QueryOver<SampleModel>();

            if (!String.IsNullOrWhiteSpace(settings.Filter))
            {
                query = query.Where(Restrictions.On<SampleModel>(x => x.LongTestValue).IsLike(settings.Filter, MatchMode.Anywhere) ||
                                        Restrictions.On<SampleModel>(x => x.StringValue).IsLike(settings.Filter, MatchMode.Anywhere));
            }

            return query;
        }

        public override EventNumber GetId()
        {
            return MenuNumber.AdvancedView;
        }

        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Add", MenuNumber.AdvancedAdd)
            };
        }
    }
}