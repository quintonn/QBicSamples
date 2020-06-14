using NHibernate;
using NHibernate.Criterion;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;

namespace QBicSamples.Samples.Courses
{
    public class ViewCourses : ShowView
    {
        private DataService DataService { get; set; }

        public ViewCourses(DataService dataService)
        {
            DataService = dataService;
        }
        public override bool AllowInMenu => true;

        public override string Description => "View Courses";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Course Type", "CourseType");
            columnConfig.AddStringColumn("Presenter", "Presenter");
            columnConfig.AddStringColumn("Venue", "Venue");
            columnConfig.AddStringColumn("Region", "Region");
            columnConfig.AddStringColumn("Country", "Country");
            columnConfig.AddDateColumn("Start Date", "StartDate");
            columnConfig.AddStringColumn("Course Id", "CourseId");

            columnConfig.AddLinkColumn("Attendees", "Id", "Attendees", MenuNumber.ViewAttendees);

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditCourse);

            columnConfig.AddButtonColumn("", "Id", "X", new UserConfirmation("Delete item?", MenuNumber.DeleteCourse));
        }

        public override IEnumerable GetData(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var data = CreateQuery(session, settings).Skip((settings.CurrentPage - 1) * settings.LinesPerPage)
                                                   .Take(settings.LinesPerPage)
                                                   .List<Course>()
                                                   .ToList();
                var results = TransformData(data);
                return results;
            }
        }
        public IEnumerable TransformData(IList<Course> data)
        {
            var results = new List<object>();
            using (var session = DataService.OpenSession())
            {
                results = data.Select(d => (object)new
                {
                    Region = d.Country?.Region?.Name,
                    d.Venue,
                    Country = d.Country?.Name,
                    d.Id,
                    CourseType = d.CourseType.Name,
                    d.StartDate,
                    Presenter = d.Presenter?.Name,
                    d.CourseNumber,
                    d.CourseId
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

        public IQueryOver<Course> CreateQuery(ISession session, GetDataSettings settings)
        {
            var query = session.QueryOver<Course>();

            if (!String.IsNullOrWhiteSpace(settings.Filter))
            {
                query = query.Where(Restrictions.On<Course>(x => x.Venue).IsLike(settings.Filter, MatchMode.Anywhere));
            }

            query = query.OrderBy(x => x.StartDate).Desc;

            return query;
        }

        public override EventNumber GetId()
        {
            return MenuNumber.ViewCourses;
        }

        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Add", MenuNumber.AddCourse)
            };
        }
    }
}