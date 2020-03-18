using QBicSamples.Models.Samples;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.BackEnd.AdvancedSample
{
    public class AdvancedDetails : DoSomething
    {
        private DataService DataService { get; set; }

        public AdvancedDetails(DataService dataService)
        {
            DataService = dataService;
        }

        public override bool AllowInMenu => false;

        public override string Description => "Advanced Details";

        public override EventNumber GetId()
        {
            return MenuNumber.AdvancedDetails;
        }

        public override async Task<IList<IEvent>> ProcessAction()
        {
            var id = GetValue("Id");

            SampleModel model;
            using (var session = DataService.OpenSession())
            {
                model = session.Get<SampleModel>(id);
            }

            var message = "You select sample model with the following details <br/> " +
                          "Id: " + model.Id + "<br/>" +
                          "String: " + model.StringValue + "<br/>" +
                          "Date: " + model.DateValue?.ToShortDateString() + "<br/>" +
                          "Number: " + model.NumberValue + "<br/>" +
                          "Long Value: " + model.LongTestValue + "<br/>";

            var result = new List<IEvent>()
            {
                new ShowMessage(message)
            };

            return result;
        }
    }
}