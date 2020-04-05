using NHibernate;
using QBicSamples.Models.Samples;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.MenuItems.Sample
{
    public abstract class TestModify : CoreModify<SampleModel>
    {
        public TestModify(DataService dataService, bool isNew)
            : base(dataService, isNew)
        {
        }

        public override string EntityName => "Sample Model";

        public override EventNumber GetViewNumber()
        {
            return MenuNumber.SampleView;
        }

        public override List<InputField> InputFields()
        {
            var result = new List<InputField>();

            result.Add(new StringInput("Text", "Text", Item?.StringValue, null, true));
            result.Add(new NumericInput<int>("Number", "Number", Item?.NumberValue, null, true));
            result.Add(new DateInput("Date", "Date", Item?.DateValue ?? DateTime.Now, null, false));
            result.Add(new StringInput("Long", "Long Text", Item?.LongTestValue, null, false)
            {
                MultiLineText = true
            });
            result.Add(new DataSourceComboBoxInput<SampleCategory>("Category", "Category", x => x.Id, x => x.Description, Item?.Category?.Id, null, null, x => x.Description, true, false));

            return result;

        }

        public override async Task<IList<IEvent>> PerformModify(bool isNew, string id, ISession session)
        {
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
                return ErrorMessage("Date is mandatory");
            }

            SampleModel dbItem;
            if (isNew)
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

            return null; // will close the input dialog and show the view
        }
    }
}