﻿using QBicSamples.SiteSpecific;
using QBicSamples.Models.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using QBicSamples.Models;
using WebsiteTemplate.Utilities;

namespace QBicSamples.BackEnd.MultipleViews.Editions
{
    public class DeleteEdition : DoSomething
    {
        private DataService DataService { get; set; }

        public string ManufacturerId;
        public string ModelId;
        public DeleteEdition(DataService dataService)
        {
            DataService = dataService;
        }

        public override bool AllowInMenu => false;

        public override string Description => "Delete Edition";

        public override EventNumber GetId()
        {
            return MenuNumber.DeleteEdition;
        }

        public override async Task<IList<IEvent>> ProcessAction()
        {
            var id = GetValue("Id");
            using (var session = DataService.OpenSession())
            {
                var edition = session.Get<Edition>(id);
                ManufacturerId = edition.ManufacturerId;
                ModelId = edition.ModelId;

                DataService.TryDelete(session, edition); // Do deletes this way to have it audited
                                                       //session.Delete(model); // This way won't be audited.

                session.Flush();
            }

            var data = new
            {
                data = new
                {
                    Id = ModelId,
                    ManufacturerId
                }
            };
            var json = JsonHelper.SerializeObject(data);

            return new List<IEvent>()
            {
                new ShowMessage("Edition deleted successfully"),
                new CancelInputDialog(),
                new ExecuteAction(MenuNumber.ViewEditions, json)
            };
        }
    }
}