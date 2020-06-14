using NHibernate;
using NHibernate.Linq;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Linq;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.Courses
{
    public class DeleteCourse : CoreDeleteAction<Course>
    {
        public DeleteCourse(DataService dataService) : base(dataService)
        {
        }

        public override string EntityName => "Course";

        public override EventNumber ViewNumber => MenuNumber.ViewCourses;

        public override EventNumber GetId()
        {
            return MenuNumber.DeleteCourse;
        }

        public override void DeleteOtherItems(ISession session, Course mainItem)
        {

            session.Query<CourseAttendee>().Where(x => x.Course.Id == mainItem.Id).Delete();
        }
    }
}