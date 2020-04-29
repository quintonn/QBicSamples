using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.Samples.MultipleViews.Editions
{
    public class DetailsEdition : DoSomething
    {
        private DataService DataService { get; set; }

        public DetailsEdition(DataService dataService)
        {
            DataService = dataService;
        }

        public override bool AllowInMenu => false;

        public override string Description => "Edition Details";

        public override EventNumber GetId()
        {
            return MenuNumber.DetailsEdition;
        }

        public override async Task<IList<IEvent>> ProcessAction()
        {
            var id = GetValue("Id");

            Edition edition;
            using (var session = DataService.OpenSession())
            {
                edition = session.Get<Edition>(id);
            }

            // x.4
           // try use code like this instead:
            //var message = $"You selected edition with the following details <br/>Id: ${edition.Id}<br/>Name: {edition.EditionName}......";
            var message = $"You select edition with the following details <br/>Id: ${edition.Id}<br/>Name: { edition.EditionName}<br/>Year: { edition.EditionYear.Year}<br/>";

            var result = new List<IEvent>()
            {
                new ShowMessage(message)
            };

            return result;
        }
    }
}