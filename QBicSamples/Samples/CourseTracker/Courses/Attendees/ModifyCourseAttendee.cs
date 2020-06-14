using QBicSamples.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.InputItems;
using WebsiteTemplate.Utilities;

namespace QBicSamples.Samples.Courses.Attendees
{
    public abstract class ModifyCourseAttendee : GetInput
    {
        private bool IsNew { get; set; }
        private int RowId { get; set; }
        private JsonHelper RowData { get; set; }

        public ModifyCourseAttendee(bool isNew)
        {
            IsNew = isNew;
        }

        public override string Description
        {
            get
            {
                if (IsNew)
                {
                    return "Add Attendee";
                }
                return "Edit Attendee";
            }
        }

        public override bool AllowInMenu => false;

        public override async Task<InitializeResult> Initialize(string data)
        {
            var json = JsonHelper.Parse(data);

            if (IsNew == false)
            {
                var rowData = data;
                if (!String.IsNullOrWhiteSpace(json.GetValue("rowData")))
                {
                    rowData = json.GetValue("rowData");
                }

                RowId = json.GetValue<int>("rowid");
                RowData = JsonHelper.Parse(rowData);
            }
            else
            {
                RowData = new JsonHelper();
                RowId = -1;
            }

            return new InitializeResult(true);
        }

        public override IList<InputField> GetInputFields()
        {
            var list = new List<InputField>();

            list.Add(new HiddenInput("Id", RowData?.GetValue("Id")));
            list.Add(new HiddenInput("CourseId", RowData?.GetValue("CourseId")));

            list.Add(new StringInput("Name", "Name", RowData?.GetValue("Name"), null, true));
            list.Add(new StringInput("Email", "Email", RowData?.GetValue("Email"), null, true));
            var gender = RowData?.GetValue("Gender");
            list.Add(new EnumComboBoxInput<Gender>("Gender", "Gender", false, null, x => x.Value, RowData?.GetValue("GenderString"), null)
            {
                Mandatory = true
            });

            list.Add(new DataSourceComboBoxInput<Region>("Region", "Region", x => x.Id, x => x.Name, RowData?.GetValue("Region"), null, null, x => x.Name, true, false)
            {
                Mandatory = true,
            });

            list.Add(new HiddenInput("rowId", RowId));

            return list;
        }

        public override async Task<IList<IEvent>> ProcessAction(int actionNumber)
        {
            if (actionNumber == 1)
            {
                return new List<IEvent>()
                {
                    new CancelInputDialog(),
                };
            }
            else if (actionNumber == 0)
            {
                var gender = GetValue<Gender>("Gender");
                InputData.Add("GenderString", gender.ToString());

                var region = GetDataSourceValue<Region>("Region");
                InputData.Add("RegionString", region.Name);

                return new List<IEvent>()
                {
                    new UpdateInputView(InputViewUpdateType.AddOrUpdate),
                    new CancelInputDialog()
                };
            }

            return null;
        }
    }
}