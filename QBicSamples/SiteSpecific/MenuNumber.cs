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

        // Use numbers 3000 and above. Numbers below 3000 are for internal menu items.

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

        
        public static MenuNumber SampleView = new MenuNumber(3000);
        public static MenuNumber SampleAdd = new MenuNumber(3001);
        public static MenuNumber SampleEdit = new MenuNumber(3002);
        public static MenuNumber SampleDelete = new MenuNumber(3003);

        public static MenuNumber AdvancedView = new MenuNumber(3020);
        public static MenuNumber AdvancedAdd = new MenuNumber(3021);
        public static MenuNumber AdvancedEdit = new MenuNumber(3022);
        public static MenuNumber AdvancedDelete = new MenuNumber(3023);
        public static MenuNumber AdvancedDetails = new MenuNumber(3024);

        public static MenuNumber ViewSampleCategories = new MenuNumber(3010);

        public static MenuNumber SampleBackgroundProcess = new MenuNumber(4000);
    }
}