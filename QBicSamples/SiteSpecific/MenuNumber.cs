using Newtonsoft.Json;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.SiteSpecific
{
    [JsonConverter(typeof(EventNumberConverter))]
    public class MenuNumber : EventNumber
    {
        public MenuNumber(int value)
            : base(value)
        {
        }

        // Use numbers 2000 and above. Numbers below 2000 are for internal menu items.

        public static MenuNumber ViewManufacturers = new MenuNumber(2030);
        public static MenuNumber AddManufacturer = new MenuNumber(2031);
        public static MenuNumber EditManufacturer = new MenuNumber(2032);
        public static MenuNumber DeleteManufacturer = new MenuNumber(2033);

        public static MenuNumber ViewModels = new MenuNumber(2040);
        public static MenuNumber AddModel = new MenuNumber(2041);
        public static MenuNumber EditModel = new MenuNumber(2042);
        public static MenuNumber DeleteModel = new MenuNumber(2043);

        public static MenuNumber ViewEditions = new MenuNumber(2050);
        public static MenuNumber AddEdition = new MenuNumber(2051);
        public static MenuNumber EditEdition = new MenuNumber(2052);
        public static MenuNumber DeleteEdition = new MenuNumber(2053);
        public static MenuNumber DetailsEdition = new MenuNumber(2054);

        public static MenuNumber ViewPatients = new MenuNumber(2060);
        public static MenuNumber AddPatient = new MenuNumber(2061);
        public static MenuNumber EditPatient = new MenuNumber(2062);
        public static MenuNumber DeletePatient = new MenuNumber(2063);

        public static MenuNumber ViewExternalAPI = new MenuNumber(2070);

        public static MenuNumber SampleView = new MenuNumber(3000);
        public static MenuNumber SampleAdd = new MenuNumber(3001);
        public static MenuNumber SampleEdit = new MenuNumber(3002);
        public static MenuNumber SampleDelete = new MenuNumber(3003);

        public static MenuNumber ViewSampleCategories = new MenuNumber(3010);

        public static MenuNumber AdvancedView = new MenuNumber(3020);
        public static MenuNumber AdvancedAdd = new MenuNumber(3021);
        public static MenuNumber AdvancedEdit = new MenuNumber(3022);
        public static MenuNumber AdvancedDelete = new MenuNumber(3023);
        public static MenuNumber AdvancedDetails = new MenuNumber(3024);

        public static MenuNumber CourseTypeMenuItem = new MenuNumber(3025);
        public static MenuNumber CoursePresenterMenuItem = new MenuNumber(3030);

        public static MenuNumber ViewAttendees = new MenuNumber(3040);
        public static MenuNumber AttendeeCertificate = new MenuNumber(3041);

        public static MenuNumber ViewCourses = new MenuNumber(3050);
        public static MenuNumber AddCourse = new MenuNumber(3051);
        public static MenuNumber EditCourse = new MenuNumber(3052);
        public static MenuNumber DeleteCourse = new MenuNumber(3053);
        public static MenuNumber ViewCourseAttendees = new MenuNumber(3054);
        public static MenuNumber AddCourseAttendee = new MenuNumber(3055);
        public static MenuNumber EditCourseAttendee = new MenuNumber(3056);
        public static MenuNumber DeleteCourseAttendee = new MenuNumber(3057);

        public static MenuNumber RegionMenuItem = new MenuNumber(3060);

        public static MenuNumber ViewCountries = new MenuNumber(3070);
        public static MenuNumber AddCountry = new MenuNumber(3071);
        public static MenuNumber EditCountry = new MenuNumber(3072);
        public static MenuNumber DeleteCountry = new MenuNumber(3073);

        public static MenuNumber SampleBackgroundProcess = new MenuNumber(4000);
    }
}