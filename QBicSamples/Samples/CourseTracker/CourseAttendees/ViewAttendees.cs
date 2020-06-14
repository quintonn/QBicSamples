using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using WebsiteTemplate.Menus.ViewItems.CoreItems;

namespace QBicSamples.Samples.CourseAttendees
{
    public class ViewAttendees : CoreView<CourseAttendee>
    {
        public ViewAttendees(DataService dataService) : base(dataService)
        {
        }

        public override bool AllowInMenu => false;

        public override string Description => "View Attendees";
        
        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Name", "Name");
            columnConfig.AddStringColumn("Email", "Email");
            columnConfig.AddStringColumn("Region", "Region.Name");
            columnConfig.AddStringColumn("Gender", "Gender");
            columnConfig.AddStringColumn("Course Number", "CourseNumber");

            columnConfig.AddLinkColumn("Certificate", "Id", "Certificate", MenuNumber.AttendeeCertificate);
        }

        public override IQueryOver<CourseAttendee> CreateQuery(ISession session, GetDataSettings settings, Expression<Func<CourseAttendee, bool>> additionalCriteria = null)
        {
            var courseId = GetParameter("Id", settings);
            return base.CreateQuery(session, settings, x => x.Course.Id == courseId);
        }

        public override List<Expression<Func<CourseAttendee, object>>> GetFilterItems()
        {
            return new List<Expression<Func<CourseAttendee, object>>>();
        }

        public override EventNumber GetId()
        {
            return MenuNumber.ViewAttendees;
        }
        public override IList<MenuItem> GetViewMenu(Dictionary<string, string> dataForMenu)
        {
            return new List<MenuItem>()
            {
                new MenuItem("Back", MenuNumber.ViewCourses)
            };
        }
    }
}

//var assembly = Assembly.GetExecutingAssembly();

//var stream = assembly.GetManifestResourceStream("Odyssey.Templates.GradeTemplate.docx");

//var mem = new MemoryStream();

//stream.Seek(0, SeekOrigin.Begin);

//            stream.CopyTo(mem);

//            var wordprocessingDocument = WordprocessingDocument.Open(mem, true);

//Body body = wordprocessingDocument.MainDocumentPart.Document.Body;
//var document = wordprocessingDocument.MainDocumentPart.Document;