using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.BasicCrudItems;

namespace QBicSamples.Samples.CourseTypes
{
    public class CourseTypeMenuItem : BasicCrudMenuItem<CourseType>
    {
        public override bool AllowInMenu => true;

        public override string GetBaseItemName()
        {
            return "Course Type";
        }

        public override EventNumber GetBaseMenuId()
        {
            return MenuNumber.CourseTypeMenuItem;
        }

        public override Dictionary<string, string> GetColumnsToShowInView()
        {
            return new Dictionary<string, string>()
            {
                {  "Name", "Name" },
                {  "Description", "Description" }
            };
        }

        public override Dictionary<string, string> GetInputProperties()
        {
            return new Dictionary<string, string>()
            {
                {  "Name", "Name" },
                {  "Description", "Description" }
            };
        }
    }
}