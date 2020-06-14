using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using NHibernate;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Utilities;
using FileInfo = WebsiteTemplate.Menus.InputItems.FileInfo;

namespace QBicSamples.Samples.CourseAttendees
{
    public class GetCertificate : OpenFile
    {
        private DataService DataService { get; set; }
        public GetCertificate(DataService dataService)
        {
            DataService = dataService;
        }

        private string _fileName { get; set; }

        public override bool AllowInMenu => false;
        public override string Description => "Grade Report";

        public override async Task<FileInfo> GetFileInfo(string data)
        {
            var json = JsonHelper.Parse(data);
            var id = json.GetValue("Id");

            var result = new FileInfo();
            using (var session = DataService.OpenStatelessSession())
            {
                var attendee = session.Get<CourseAttendee>(id);

                result.FileName = attendee.Name + "_" + attendee.Course.CourseId + ".docx";
                result.FileExtension = "docx";
                result.Data = CreateCertificate(attendee, session);
                result.MimeType = "application/zip";

                _fileName = result.GetFullFileName();
                //System.IO.File.Delete(zipFileName);

                return result;
            }

        }

        private byte[] CreateCertificate(CourseAttendee attendee, IStatelessSession session)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using (var stream = assembly.GetManifestResourceStream("QBicSamples.Samples.CourseTracker.Templates.CertificateTemplate.docx"))
            using (var mem = new MemoryStream())
            {
                stream.Seek(0, SeekOrigin.Begin);
                stream.CopyTo(mem);

                var wordprocessingDocument = WordprocessingDocument.Open(mem, true);
                var document = wordprocessingDocument.MainDocumentPart.Document;

                foreach (var text in document.Descendants<Text>())
                {
                    TryUpdateTextElement(text, "<DATE>", attendee.Course.StartDate.ToString("dd MMMM yyyy").ToUpper());
                    TryUpdateTextElement(text, "<ATTENDEE>", attendee.Name.ToUpper());
                    TryUpdateTextElement(text, "<VENUE>", attendee.Course.Venue.ToUpper());
                    TryUpdateTextElement(text, "<PRESENTER>", attendee.Course.Presenter.Name.ToUpper());
                    TryUpdateTextElement(text, "<COURSEID>", attendee.Course.CourseId.ToUpper());
                }

                // END OF GENERATING REPORT

                wordprocessingDocument.Close();

                var result = mem.ToArray();
                return result;
            }
        }

        private void TryUpdateTextElement(Text element, string textToFind, string textToInsert)
        {
            if (element.Text.Contains(textToFind))
            {
                element.Text = element.Text.Replace(textToFind, textToInsert);
            }
        }

        public override string GetFileNameAndExtension()
        {
            return _fileName;
        }

        public override EventNumber GetId()
        {
            return MenuNumber.AttendeeCertificate;
        }
    }
}