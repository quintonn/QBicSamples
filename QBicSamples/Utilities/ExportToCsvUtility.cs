using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WebsiteTemplate.Backend.Services;

namespace QBicSamples.Utilities
{
    public class ExportToCsvUtility
    {
        private DataService DataService { get; set; }

        public ExportToCsvUtility(DataService dataService)
        {
            DataService = dataService;
        }
        public byte[] CreateCSV<T>(ref List<T> entities)
        {

            using (var memCsvStream = new MemoryStream())
            {

                using (var session = DataService.OpenStatelessSession())
                {

                    PropertyInfo[] properties = typeof(T).GetProperties();
                    foreach (PropertyInfo property in properties)
                    {
                        memCsvStream.Write(Encoding.UTF8.GetBytes(property.ToString()), 0, property.ToString().Length);
                        memCsvStream.Write(Encoding.UTF8.GetBytes(","), 0, 1);
                    }
                   
                    var newLineBytes = Encoding.UTF8.GetBytes(Environment.NewLine);
                    memCsvStream.Write(newLineBytes, 0, newLineBytes.Length);

                    foreach (var entity in entities)
                    {
                        foreach (PropertyInfo property in properties)
                        {
                            if (!Convert.IsDBNull(entity + "." + property))
                            {
                                string value = (entity + "." + property).ToString();
                                if (value.Contains(','))
                                {
                                    value = String.Format("\"{0}\"", value);
                                    memCsvStream.Write(Encoding.UTF8.GetBytes(value), 0, value.Length);
                                }
                                else
                                {
                                    memCsvStream.Write(Encoding.UTF8.GetBytes(value), 0, value.Length);
                                }
                            }
                            
                            memCsvStream.Write(Encoding.UTF8.GetBytes(","), 0, 1);
                        }
                        memCsvStream.Write(newLineBytes, 0, newLineBytes.Length);
                    }

                    var result = memCsvStream.ToArray();
                    return result;
                }
            }
        }
    }
}