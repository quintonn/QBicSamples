using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.MultipleViews.Patients
{
    public class ViewPatients : CoreView<Patient>
    {
        public ViewPatients(DataService dataService) : base(dataService)
        {
        }
        public override bool AllowInMenu => true;

        public override string Description => "View Doctor Patients";

        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Name", "Name");
            columnConfig.AddStringColumn("Name", "Surname");
            columnConfig.AddLinkColumn("", "Id", "Edit", MenuNumber.EditPatient);
            columnConfig.AddButtonColumn("", "Id", "X", new UserConfirmation("Delete patient?", MenuNumber.DeletePatient));
        }
        public override List<Expression<Func<Patient, object>>> GetFilterItems()
        {
            return new List<Expression<Func<Patient, object>>>()
            {
                x => x.Name
            };
        }
        public override EventNumber GetId()
        {
            return MenuNumber.ViewPatients;
        }
        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Add", MenuNumber.AddPatient)
            };
        }
    }
}