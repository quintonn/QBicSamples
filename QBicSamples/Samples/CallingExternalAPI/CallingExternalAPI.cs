using QBicSamples.SiteSpecific;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;

namespace QBicSamples.CallingExternalAPI
{
    /// <summary>
    /// This is the more advanced way to create a screen view.
    /// More code is required but you have full control on the columns and data being shown.
    /// </summary>
    public class CallingExternalAPI : ShowView
    {
        public CallingExternalAPI()
        {
        }



        public List<Object> RecordsList = new List<Object>();

        public string Request;

        public string Response;
        public override bool AllowInMenu => true;

        public override string Description => "Calling External API";
        public override EventNumber GetId()
        {
            return MenuNumber.ViewExternalAPI;
        }
        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Request", "Request");
            columnConfig.AddStringColumn("Response", "Response");
        }
        public override int GetDataCount(GetDataSettings settings)
        {
            return 3;
        }
        public override IEnumerable GetData(GetDataSettings settings)
        {
            GetResponse();
            return RecordsList;
        }

        public async void GetResponse()
        {
            var url = "https://api.weather.gov/points/39.7456,-97.0892";
            var result = await GetExternalResponse(url);
            Request = url;
            Response = result;
            RecordsList.Add(new { Request = url, Response = result });
        }
        private async Task<string> GetExternalResponse(string url)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}