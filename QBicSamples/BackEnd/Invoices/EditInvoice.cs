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

namespace QBicSamples.BackEnd.Invoices
{
    public class EditInvoice : GetInput
    {
        public override bool AllowInMenu => throw new NotImplementedException();

        public override string Description => throw new NotImplementedException();

        public override EventNumber GetId()
        {
            throw new NotImplementedException();
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