using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.Samples.MultipleViews.Models
{
    public abstract class ModifyModel : CoreModify<VehicleModel>
    {
        public string ModelId;

        public string ManufacturerId;
        public ModifyModel(DataService dataService, bool isNew) : base(dataService, isNew)
        {
        }
        public override string EntityName => "Vehicle Model";
        public override EventNumber GetViewNumber()
        {
            return MenuNumber.ViewModels;
        }
        public override List<InputField> InputFields()
        {
            var result = new List<InputField>();

            result.Add(new StringInput("Name", "Name", Item?.Name, null, true));
            result.Add(new HiddenInput("ManufacturerId", Item?.ManufacturerId));

            return result;
        }

        public override async Task<InitializeResult> Initialize(string data)
        {
            var json = JsonHelper.Parse(data);

            ManufacturerId = json.GetValue("ManufacturerId");

            return await base.Initialize(data);
        }
        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session)
        {
            var name = GetValue("Name");

            VehicleModel model;

            if (isNew)
            {
                model = new VehicleModel();
                model.ManufacturerId = ManufacturerId;  // same as x.1
            }
            else
            {
                model = session.Get<VehicleModel>(id);
            }

            model.Name = name;

            DataService.SaveOrUpdate(session, model);

            return null;
        }

        public override string GetParameterToPassToView()
        {
            var data = new
            {
                data = new
                {
                    Id = ManufacturerId
                }
            };
            var json = JsonHelper.SerializeObject(data);
            return json;
        }
    }
}