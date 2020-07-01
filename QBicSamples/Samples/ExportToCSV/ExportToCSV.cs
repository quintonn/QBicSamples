using QBicSamples.Utilities;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Utilities;
using FileInfo = WebsiteTemplate.Menus.InputItems.FileInfo;

namespace QBicSamples.ExportToCSV
{
    public class ExportToCSV : OpenFile
    {
        private DataService DataService { get; set; }
        private ExportToCsvUtility ExportToCsvUtility { get; set; }

        public ExportToCSV(DataService dataService, ExportToCsvUtility exportToCsvUtility)
        {
            DataService = dataService;
            ExportToCsvUtility = exportToCsvUtility;
        }
        private string _fileName { get; set; }
        public override bool AllowInMenu => false;
        public override string Description => "Export To CSV";

        public string zipName = @"D:\PROJECTS\ODYSSEY\QbicSamples\ZipCsv.zip"; 


        public override async Task<FileInfo> GetFileInfo(string data)
        {
            var zipName = "ExportTables.csv";
            var result = new FileInfo();
            using (var session = DataService.OpenStatelessSession())
            {
                var manufacturers = session.QueryOver<Manufacturer>().List().ToList();

                var compressedFileStream = new MemoryStream();
                //Create an archive and store the stream in memory.
                using (var zipArchive = new ZipArchive(compressedFileStream, ZipArchiveMode.Create, false))
                {
                    var manufacturersCsv = ExportToCsvUtility.CreateCSV<Manufacturer>(ref manufacturers);
                    compressedFileStream.Write(manufacturersCsv, 0, manufacturersCsv.Length);
                }

                
                result.FileName = zipName;
                result.FileExtension = "zip";
                result.Data = compressedFileStream.ToArray();
                result.MimeType = "application/zip";

                _fileName = result.GetFullFileName();

                return result;
            }
        }
        public override string GetFileNameAndExtension()
        {
            return _fileName;
        }
        public override EventNumber GetId()
        {
            return MenuNumber.ExportToCSV;
        }
    }
}