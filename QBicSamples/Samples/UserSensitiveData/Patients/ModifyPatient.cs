using BasicAuthentication.ControllerHelpers;
using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Data;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;
using WebsiteTemplate.Models;
using WebsiteTemplate.Utilities;

namespace QBicSamples.Samples.UserSensitiveData.Patients
{
    public abstract class ModifyPatient : CoreModify<Patient>
    {
        private UserContext UserContext { get; set; }
        private string DoctorId { get; set; }
        public ModifyPatient( DataService dataService, bool isNew) : base(dataService, isNew)
        {
        }
        public override string EntityName => "Patient";
        public override EventNumber GetViewNumber()
        {
            return MenuNumber.ViewPatients;
        }
        public override async Task<InitializeResult> Initialize(string data)
        {
            var json = JsonHelper.Parse(data);

            var id = json.GetValue("Id");
            DoctorId = json.GetValue("DoctorId");

            IsNew = String.IsNullOrWhiteSpace(id);
            if (IsNew)
            {
                Item = new Patient();
                Item.DoctorId = DoctorId;
                Item.BirthDay =  DateTime.ParseExact("1990-01-01", "yyyy-MM-dd", CultureInfo.InvariantCulture);
            }
            else
            {
                using (var session = DataService.OpenSession())
                {
                    Item = session.Get<Patient>(id);
                }
            }

            return new InitializeResult(true);
        }
        public override List<InputField> InputFields()
        {
            var result = new List<InputField>();
            result.Add(new HiddenInput("DoctorId", Item?.DoctorId));
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
           // var currentUser = await Methods.GetLoggedInUserAsync(UserContext) as User;

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
                patient.DoctorId = currentUser.Id;
            }
            else
            {
                patient = session.Get<Patient>(id);
            }
           
            patient.Name = name;
            patient.Surname = surname;
            patient.BirthDay = birthday;

            DataService.SaveOrUpdate(session, patient);

            return null;
        }
    }
}