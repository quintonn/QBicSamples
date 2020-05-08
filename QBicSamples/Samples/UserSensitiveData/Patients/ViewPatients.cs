using BasicAuthentication.ControllerHelpers;
using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.ServiceModel.Channels;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;
using WebsiteTemplate.Models;

namespace QBicSamples.Samples.MultipleViews.Patients
{
    public class ViewPatients : CoreView<Patient>
    {
        public ViewPatients(DataService dataService) : base(dataService)
        {
        }
        public override bool AllowInMenu => true;

        public override string Description => "View Patients";

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
        public override IQueryOver<Patient> CreateQuery(NHibernate.ISession session, GetDataSettings settings, Expression<Func<Patient, bool>> additionalCriteria = null)
        {
            var currentUser =  Methods.GetLoggedInUserAsync(UserContext) as User;
            return base.CreateQuery(session, settings, x => x.DoctorId == currentUser.Id);
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