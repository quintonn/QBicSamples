using NHibernate;
using NHibernate.Linq;
using System.Linq;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.UserSpecificData.Patients
{
    public class DeletePatient : CoreDeleteAction<Patient>
    {
        public DeletePatient(DataService dataService)
           : base(dataService)
        {
        }
        public override string EntityName => "Patient";

        public override EventNumber ViewNumber => MenuNumber.ViewPatients;

        public override EventNumber GetId()
        {
            return MenuNumber.DeletePatient;
        }
    }
}