using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.BasicCrudItems;

namespace QBicSamples.Samples.CourseTracker.Countries
{
    public class RegionsMenuItem : BasicCrudMenuItem<Region>
    {
        public override bool AllowInMenu => true;

        public override string GetBaseItemName()
        {
            return "Region";
        }

        public override EventNumber GetBaseMenuId()
        {
            return MenuNumber.RegionMenuItem;
        }

        public override Dictionary<string, string> GetColumnsToShowInView()
        {
            return new Dictionary<string, string>()
            {
                {  "Name", "Name" },
            };
        }

        public override Dictionary<string, string> GetInputProperties()
        {
            return new Dictionary<string, string>()
            {
                {  "Name", "Name" },
            };
        }
    }
}