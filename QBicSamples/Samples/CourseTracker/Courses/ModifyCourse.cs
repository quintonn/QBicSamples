using Newtonsoft.Json.Linq;
using NHibernate;
using QBicSamples.Samples.Courses.Attendees;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.PropertyChangedEvents;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.Courses
{
    public abstract class ModifyCourse : CoreModify<Course>
    {
        protected ModifyCourse(DataService dataService, bool isNew) : base(dataService, isNew)
        {
        }

        public override string EntityName => "Course";

        public override EventNumber GetViewNumber()
        {
            return MenuNumber.ViewCourses;
        }

        public override List<InputField> InputFields()
        {
            var result = new List<InputField>();
            result.Add(new DataSourceComboBoxInput<CourseType>("CourseType", "Course Type", x => x.Id, x => x.Description, Item?.CourseType?.Id, null, null, x => x.Description, true, false)
            {
                Mandatory = true,
                TabName = "General"
            });

            result.Add(new StringInput("Venue", "Venue", Item?.Venue, "General", true));
            var defaultDate = IsNew ? DateTime.Today : Item.StartDate;
            result.Add(new DateInput("StartDate", "Start Date", defaultDate, "General", true));

            result.Add(new DataSourceComboBoxInput<Region>("Region", "Region", x => x.Id, x => x.Name, Item?.Country?.Region?.Id, "General", null, x => x.Name, true, false)
            {
                RaisePropertyChangedEvent = true,
            });
            //result.Add(new ComboBoxInput("Region", "Region", Item?.Country?.Region, "General", true)
            //{
            //    ListItems = new Dictionary<string, object>()
            //    {
            //        { "Africa", "Africa" },
            //        { "Europe", "Europe" }
            //    },
            //    RaisePropertyChangedEvent = true
            //});
            result.Add(new DataSourceComboBoxInput<Country>("Country", "Country", x => x.Id, x => x.Name, Item?.Country?.Id, "General", null, x => x.Name, true, false));

            result.Add(new DataSourceComboBoxInput<CoursePresenter>("Presenter", "Presenter", x => x.Id, x => x.Name, Item?.Presenter?.Id, null, null, x => x.Name, true, false)
            {
                Mandatory = true,
                TabName = "General"
            });

            if (IsNew == false)
            {
                result.Add(new NumericInput<int>("CourseNumber", "Course Number", Item?.CourseNumber, "General", true, 0));
            }

            result.Add(new ViewInput("Attendees", "Attendees", new ViewCourseAttendees(DataService), Item?.Id, "Attendees", true));

            return result;
        }

        public override async Task<IList<IEvent>> OnPropertyChanged(string propertyName, object propertyValue)
        {
            if (propertyName == "Region")
            {
                if (!string.IsNullOrWhiteSpace(propertyValue?.ToString()))
                {
                    var result = new List<IEvent>();
                    
                    using (var session = DataService.OpenStatelessSession())
                    {
                        var id = propertyValue?.ToString();
                        var countries = session.QueryOver<Country>().Where(x => x.Region.Id == id).List().ToList();
                        var items = countries.ToDictionary(x => x.Id, x => (object)x.Name);
                        result.Add(new UpdateComboBoxSource("Country", items));
                    }
                        
                    return result;
                }
            }

            return await base.OnPropertyChanged(propertyName, propertyValue);
        }

        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session)
        {
            var courseType = GetDataSourceValue<CourseType>("CourseType");
            var venue = GetValue("Venue");

            var country = GetDataSourceValue<Country>("Country");
            var startDate = GetValue<DateTime>("StartDate");
            var presenter = GetDataSourceValue<CoursePresenter>("Presenter");

            var attendees = GetValue<List<JToken>>("Attendees") ?? new List<JToken>();

            Course dbItem;
            if (IsNew)
            {
                dbItem = new Course();

                var nextCourseNumber = 1;
                var existingCourse = session.QueryOver<Course>().Where(x => x.Country.Id == country.Id && x.StartDate.Year == startDate.Year && x.CourseType.Id == courseType.Id)
                                                                 .OrderBy(x => x.CourseNumber).Desc
                                                                 .List()
                                                                 .FirstOrDefault();
                if (existingCourse != null)
                {
                    nextCourseNumber = existingCourse.CourseNumber + 1;
                }

                dbItem.CourseNumber = nextCourseNumber;
            }
            else
            {
                dbItem = session.Get<Course>(id);
                dbItem.CourseNumber = GetValue<int>("CourseNumber");
            }

            dbItem.CourseId = $"{country.Region.Name}/{courseType.Name}/{startDate.Year}/{dbItem.CourseNumber}";

            dbItem.CourseType = courseType;
            dbItem.Venue = venue;
            dbItem.Country = country;
            dbItem.StartDate = startDate;
            dbItem.Presenter = presenter;

            DataService.SaveOrUpdate(session, dbItem);

            var existingAttendees = session.QueryOver<CourseAttendee>().Where(f => f.Course.Id == dbItem.Id).List().ToList();
            /* Process filter items */
            var finalFilterItems = new List<string>();
            foreach (var attendee in attendees)
            {
                var attendeeId = attendee.Value<string>("Id");
                
                CourseAttendee dbAttendee;

                if (!string.IsNullOrWhiteSpace(attendeeId))
                {
                    dbAttendee = session.Get<CourseAttendee>(attendeeId);
                    finalFilterItems.Add(attendeeId);
                }
                else
                {
                    dbAttendee = Activator.CreateInstance<CourseAttendee>();
                }

                var attendeeRegionId = attendee.Value<string>("Region");
                dbAttendee.Region = session.Get<Region>(attendeeRegionId);
                dbAttendee.Course = dbItem;
                dbAttendee.Email = attendee.Value<string>("Email");
                dbAttendee.Gender = (Gender)attendee.Value<int>("Gender");
                dbAttendee.Name = attendee.Value<string>("Name");

                DataService.SaveOrUpdate(session, dbAttendee);
            }

            var attendeesToDelete = existingAttendees.Where(f => !finalFilterItems.Contains(f.Id)).ToList();

            attendeesToDelete.ForEach(f =>
            {
                DataService.TryDelete(session, f);
            });

            return null;
        }
    }
}