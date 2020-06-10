using Newtonsoft.Json;
using QBicSamples.Models;
using QBicSamples.SiteSpecific;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

        private List<Object> RecordsList = new List<Object>();
        private DateTime LastCalled = DateTime.Now;
        private HttpClient Client = new HttpClient();
        public override bool AllowInMenu => true;
        public override string Description => "External View";
        public override EventNumber GetId()
        {
            return MenuNumber.ViewExternalAPI;
        }
        public override void ConfigureColumns(ColumnConfiguration columnConfig)
        {
            columnConfig.AddStringColumn("Id", "Id");
            columnConfig.AddStringColumn("PostId", "PostId");
            columnConfig.AddStringColumn("Name", "Name");
            columnConfig.AddStringColumn("Email", "Email");
            columnConfig.AddStringColumn("Body", "Body");
        }
        public override int GetDataCount(GetDataSettings settings)
        {
            // This is only called the first time the view is shown to get the total number of records
            DownloadData(settings);
            return RecordsList.Count;
        }

        public override IEnumerable GetData(GetDataSettings settings)
        {
            DownloadData(settings); // call again to download the data when user clicks search or next etc.

            // If we had downloaded the entire dataset, we would return only the required items as follows
            return RecordsList.Skip((settings.CurrentPage - 1) * settings.LinesPerPage).Take(settings.LinesPerPage);
        }

        private void DownloadData(GetDataSettings settings)
        {
            var timeSinceLastCall = DateTime.Now.Subtract(LastCalled).TotalSeconds;
            if (timeSinceLastCall < 3)
            {
                return; // this is to prevent calling the web service too often and also because the first time the view is loaded it will be called twice. Not ideal, might get fixed.
            }
            LastCalled = DateTime.Now;

            var url = $"https://jsonplaceholder.typicode.com/comments";

            RecordsList.Clear();

            //GET Method
            Client.DefaultRequestHeaders.Accept.Clear();
            var asyncResponse = Client.GetAsync(url);

            asyncResponse.Wait();
            if (asyncResponse.Status != System.Threading.Tasks.TaskStatus.RanToCompletion)
            {
                throw asyncResponse.Exception;
            };

            var response = asyncResponse.Result;
            var asyncResult = response.Content.ReadAsStringAsync();

            asyncResult.Wait();
            if (asyncResult.Status != System.Threading.Tasks.TaskStatus.RanToCompletion)
            {
                throw asyncResult.Exception;
            }
            var result = asyncResult.Result;

            if (!string.IsNullOrEmpty(result))
            {
                var comments = JsonConvert.DeserializeObject<Comment[]>(result);
                RecordsList.Clear();

                if (!String.IsNullOrWhiteSpace(settings.Filter))
                {
                    foreach (var comment in comments)
                    {
                        if (comment.Name.ToLower().Contains(settings.Filter.ToLower()) || comment.Email.ToLower().Contains(settings.Filter.ToLower()) || comment.Body.ToLower().Contains(settings.Filter.ToLower()))
                            RecordsList.Add(new
                            {
                                Id = comment.Id,
                                Name = comment.Name,
                                Body = comment.Body,
                                PostId = comment.PostId,
                                Email = comment.Email
                            });
                    }
                }
                else
                {
                    RecordsList = comments.ToList<Object>();
                }
            }
        }
    }
}
