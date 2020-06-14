using QBicSamples.SiteSpecific;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.Courses.Attendees
{
    public class AddCourseAttendee : ModifyCourseAttendee
    {
        public AddCourseAttendee() : base(true)
        {
        }

        public override EventNumber GetId()
        {
            return MenuNumber.AddCourseAttendee;
        }
    }
}