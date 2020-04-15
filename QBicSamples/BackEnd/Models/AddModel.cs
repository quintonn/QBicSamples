using QBicSamples.SiteSpecific;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;

namespace QBicSamples.BackEnd.Models
{
    // Change the name from Model to VehicleModel, it is too similar to the Models already in the project.

        // also need an EditModel class
    public class AddModel : ModifyModel
    {
        public AddModel(DataService dataService) : base(dataService, true)
        {
        }

        public override EventNumber GetId()
        {
            return MenuNumber.AddModel;
        }
    }
}