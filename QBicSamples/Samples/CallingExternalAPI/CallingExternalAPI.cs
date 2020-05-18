using NHibernate;
using NHibernate.Criterion;
using QBicSamples.SiteSpecific;
using QBicSamples.Models.Samples;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteTemplate.Backend.Services;
using WebsiteTemplate.Menus;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

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
            return 1;
        }
        public override IEnumerable GetData(GetDataSettings settings)
        {
            var responses = GetResponse();
            yield return responses;
        }

        public async Task<IEnumerable<string>> GetResponse()
        {
            var url = "https://api.weather.gov/points/39.7456,-97.0892";

            var result = await GetExternalResponse(url);

            return new string[] { url, result };
        }
        private async Task<string> GetExternalResponse(string url)
        {
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadAsStringAsync();
            return result;
        }
    }
}