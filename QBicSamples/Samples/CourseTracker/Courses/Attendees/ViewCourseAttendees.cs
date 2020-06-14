using log4net;
using log4net.Repository.Hierarchy;
using NHibernate;
using QBic.Core.Utilities;
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
using WebsiteTemplate.Utilities;

namespace QBicSamples.Samples.Courses.Attendees
{
    public class ViewCourseAttendees : ViewForInput
    {
        private static readonly ILog Logger = SystemLogger.GetLogger<ViewCourseAttendees>();
        public override string Description => "Attendees";

        public override bool AllowInMenu => false;

        private DataService DataService { get; set; }

        public ViewCourseAttendees(DataService dataService)
        {
            DataService = dataService;
        }

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Name", "Name");
            columnConfig.AddStringColumn("Email", "Email");
            columnConfig.AddStringColumn("Gender", "GenderString");
            columnConfig.AddStringColumn("Region", "RegionString");
            columnConfig.AddStringColumn("Outcome", "OutcomeString");

            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditCourseAttendee);
            columnConfig.AddButtonColumn("", "Id", "X", new UserConfirmation("Delete selected item?")
            {
                OnConfirmationUIAction = MenuNumber.DeleteCourseAttendee
            });
        }

        public override IEnumerable GetData(GetDataSettings settings)
        {
            using (var session = DataService.OpenSession())
            {
                var data = CreateQuery(session, settings)
                                                   .List<CourseAttendee>();
                //var results = data.Select(d => TransformFilterItem(session, d)).ToList();
                //return results;
                return data.Select(GetAttendeeDetail).Where(x => x != null).ToList();
            }
        }

        private object GetAttendeeDetail(CourseAttendee attendee)
        {
            try
            {
                var result = new
                {
                    attendee.Id,
                    CourseId = attendee.Course.Id,
                    attendee.Name,
                    attendee.Email,
                    GenderString = attendee.Gender.ToString(),
                    attendee.Gender,
                    RegionString = attendee.Region?.Name,
                    Region = attendee.Region?.Id,
                };

                return result;
            }
            catch (Exception e)
            {
                Logger.Error("error in attendees view: " + e.Message, e);
                Logger.Info(JsonHelper.SerializeObject(attendee));
                return null;
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

        public override EventNumber GetId()
        {
            return MenuNumber.ViewCourseAttendees;
        }

        public virtual IQueryOver<CourseAttendee> CreateQuery(ISession session, GetDataSettings settings)
        {
            var query = session.QueryOver<CourseAttendee>()
                               .Where(q => q.Course.Id == settings.ViewData /* && question.QuestionString != "Categories"*/);

            return query;
        }

        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Add", MenuNumber.AddCourseAttendee)
            };
        }
    }
}