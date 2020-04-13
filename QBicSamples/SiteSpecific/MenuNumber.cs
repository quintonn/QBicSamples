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

        public static MenuNumber ViewInvoices = new MenuNumber(2040);
        public static MenuNumber AddInvoice = new MenuNumber(2041);
        public static MenuNumber EditInvoice = new MenuNumber(2042);
        public static MenuNumber DeleteInvoice = new MenuNumber(2048);

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