using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.Courses.Attendees
{
    public class DeleteCourseAttendee : DoSomething
    {
        public override bool AllowInMenu => false;

        public override string Description => "Delete Attendee";

        public override EventNumber GetId()
        {
            return MenuNumber.DeleteCourseAttendee;
        }

        public override async Task<IList<IEvent>> ProcessAction()
        {
            return new List<IEvent>()
            {
                new UpdateInputView(InputViewUpdateType.Delete)
            };
        }
    }
}