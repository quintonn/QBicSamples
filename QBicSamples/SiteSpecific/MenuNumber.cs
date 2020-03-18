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

        public static MenuNumber SampleView = new MenuNumber(2000);
        public static MenuNumber SampleAdd = new MenuNumber(2001);
        public static MenuNumber SampleEdit = new MenuNumber(2002);
        public static MenuNumber SampleDelete = new MenuNumber(2003);

        public static MenuNumber ViewSampleCategories = new MenuNumber(2010);

        public static MenuNumber AdvancedView = new MenuNumber(2020);
        public static MenuNumber AdvancedAdd = new MenuNumber(2021);
        public static MenuNumber AdvancedEdit = new MenuNumber(2022);
        public static MenuNumber AdvancedDelete = new MenuNumber(2023);
        public static MenuNumber AdvancedDetails = new MenuNumber(2024);

        public static MenuNumber SampleBackgroundProcess = new MenuNumber(2030);
    }
}