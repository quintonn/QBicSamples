using BasicAuthentication.ControllerHelpers;
using Microsoft.Build.Framework.XamlTypes;
using NHibernate;
using QBic.Core.Data;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Data;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;
using WebsiteTemplate.Models;

namespace QBicSamples.Samples.UserSensitiveData.Patients
{
    public abstract class ModifyPatient : CoreModify<Patient>
    {
        private UserContext UserContext { get; set; }
        public ModifyPatient( DataService dataService, bool isNew, UserContext userContext) : base(dataService, isNew)
        {
            UserContext = userContext;
        }
        public override string EntityName => "Patient";
        public override EventNumber GetViewNumber()
        {
            return MenuNumber.ViewPatients;
        }
        
        public override List<InputField> InputFields()
        {
            var result = new List<InputField>();

            result.Add(new StringInput("Name", "Name", Item?.Name, null, true));
            result.Add(new StringInput("Surname", "Surname", Item?.Surname, null, true));
            result.Add(new DateInput("Birthday", "Birth Day ", Item?.BirthDay, null, false));

            return result;
        }
        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session)
        {
            var name = GetValue("Name");
            var surname = GetValue("Surname");
            var birthday = GetValue<DateTime>("Birthday");
          //  UserContext = new UserContext(dataStore);
            var currentUser = await Methods.GetLoggedInUserAsync(UserContext) as User;

            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(surname))
            {
                return new List<IEvent>()
                    {
                        new ShowMessage("Patient Name and Surname are mandatory")
                    };
            }

            Patient patient;

            if (isNew)
            {
                patient = new Patient();
            }
            else
            {
                patient = session.Get<Patient>(id);
            }

           // patient.DoctorId = user.Id;
            patient.Name = name;
            patient.Surname = surname;
            patient.BirthDay = birthday;

            DataService.SaveOrUpdate(session, patient);

            return null;
        }
    }
}