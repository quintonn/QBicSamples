using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.BasicCrudItems;

namespace QBicSamples.Samples.CoursePresenterers
{
    public class CoursePresentererMenuItem : BasicCrudMenuItem<CoursePresenter>
    {
        public override bool AllowInMenu => true;

        public override string GetBaseItemName()
        {
            return "Course Presenterer";
        }

        public override EventNumber GetBaseMenuId()
        {
            return MenuNumber.CoursePresenterMenuItem;
        }

        public override Dictionary<string, string> GetColumnsToShowInView()
        {
            return new Dictionary<string, string>()
            {
                {  "Name", "Name" },
                {  "Email", "Email" }
            };
        }

        public override Dictionary<string, string> GetInputProperties()
        {
            return new Dictionary<string, string>()
            {
                {  "Name", "Name" },
                {  "Email", "Email" }
            };
        }
    }
}