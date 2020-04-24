using log4net;
using QBicSamples.SiteSpecific;
using QBic.Core.Utilities;
using QBicSamples.Models.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.BackEnd.AdvancedSample
{
    public abstract class AdvancedModify : GetInput
    {
        private bool IsNew { get; set; }

        private static ILog Logger = SystemLogger.GetLogger<AdvancedModify>();

        public AdvancedModify(DataService dataService, bool isNew)
        {
            DataService = dataService;
            IsNew = isNew;
        }

        private SampleModel Model { get; set; }

        private DataService DataService { get; set; }

        public override bool AllowInMenu => false;

        public override string Description => IsNew ? "Add Advanced Model" : "Edit Advanced Model";

        public override async Task<InitializeResult> Initialize(string data)
        {
            Logger.Info("Advance Modify is initializing"); // Showing an example of how to do logging
            var json = JsonHelper.Parse(data);

            var id = json.GetValue("Id");

            IsNew = String.IsNullOrWhiteSpace(id);
            if (IsNew)
            {
                Model = new SampleModel();
            }
            else
            {
                using (var session = DataService.OpenSession())
                {
                    Model = session.Get<SampleModel>(id);
                }
            }

            return new InitializeResult(true);
        }

        public override IList<InputField> GetInputFields()
        {
            var result = new List<InputField>();
            
            result.Add(new HiddenInput("Id", Model?.Id)); // Need to add it so it's available when doing update
            result.Add(new StringInput("Text", "Text", Model?.StringValue, null, true));
            result.Add(new NumericInput<int>("Number", "Number", Model?.NumberValue, null, true));
            result.Add(new DateInput("Date", "Date", Model?.DateValue ?? DateTime.Now, null, false));
            result.Add(new StringInput("Long", "Long Text", Model?.LongTestValue, null, false)
            {
                MultiLineText = true
            });
            result.Add(new DataSourceComboBoxInput<SampleCategory>("Category", "Category", x => x.Id, x => x.Description, Model?.Category?.Id, null, null, x => x.Description, true, false));

            return result;
        }

        public override async Task<IList<IEvent>> ProcessAction(int actionNumber)
        {
            if (actionNumber == 1) // User clicked cancel
            {
                return new List<IEvent>()
                {
                    new CancelInputDialog()
                };
            }
            else if (actionNumber == 0) // user clicked submit
            {
                Logger.Info("Processing submit action in advanced modify");

                var id = GetValue("Id");
                var text = GetValue("Text");
                var number = GetValue<int>("Number");
                var date = GetValue<DateTime?>("Date");
                var longText = GetValue("Long");
                var category = GetDataSourceValue<SampleCategory>("Category");

                if (date == null)
                {
                    // Example of doing additional validation in code and returning a message
                    // There is already a setting for mandatory check on the client side to eliminate extra network requests.
                    // But always good to check mandatory values in code also
                    return new List<IEvent>()
                    {
                        new ShowMessage("Date is mandatory")
                    };
                }

                using (var session = DataService.OpenSession())
                {
                    SampleModel dbItem;
                    if (IsNew)
                    {
                        dbItem = new SampleModel();
                    }
                    else
                    {
                        dbItem = session.Get<SampleModel>(id);
                    }

                    dbItem.StringValue = text;
                    dbItem.NumberValue = number;
                    if (date != null)
                    {
                        dbItem.DateValue = (DateTime)date;
                    }
                    dbItem.Category = category;
                    dbItem.LongTestValue = longText;

                    DataService.SaveOrUpdate(session, dbItem); // Using saveOrUpdate adds this task to audit log
                    //session.SaveOrUpdate(dbItem); // this won't be audited

                    session.Flush();
                }

                return new List<IEvent>()
                {
                    new ShowMessage("Item successfully " + (IsNew ? "added" : "modified")),
                    new CancelInputDialog(),
                    new ExecuteAction(MenuNumber.AdvancedView)
                };
            }
            else
            {
                return null;
            }
        }
    }
}