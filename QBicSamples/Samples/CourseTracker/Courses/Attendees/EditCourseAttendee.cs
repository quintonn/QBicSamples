using QBicSamples.SiteSpecific;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.Courses.Attendees
{
    public class EditCourseAttendee : ModifyCourseAttendee
    {
        public EditCourseAttendee() : base(false)
        {
        }

        public override EventNumber GetId()
        {
            return MenuNumber.EditCourseAttendee;
        }
    }
}