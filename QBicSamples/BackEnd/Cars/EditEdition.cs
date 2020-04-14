using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.BackEnd.Cars
{
    public class EditEdition : GetInput
    {
        public override bool AllowInMenu => true;

        public override string Description => "Edit Car Edition";


        public override EventNumber GetId()
        {
            return MenuNumber.EditEdition;
        }

        public override IList<InputField> GetInputFields()
        {
            throw new NotImplementedException();
        }

        public override Task<IList<IEvent>> ProcessAction(int actionNumber)
        {
            throw new NotImplementedException();
        }
    }
}