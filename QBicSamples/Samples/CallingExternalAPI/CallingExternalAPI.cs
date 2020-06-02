using Newtonsoft.Json;
using QBicSamples.SiteSpecific;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using WebsiteTemplate.Menus.BaseItems;
using WebsiteTemplate.Menus.ViewItems;
using WebsiteTemplate.Utilities;

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

        private List<object> RecordsList = new List<object>();
        private int TotalCount = 0;
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
            return TotalCount;
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

           // var goRestPageNumber = ((settings.CurrentPage - 1) * settings.LinesPerPage) / 20;
            var url = $"https://jsonplaceholder.typicode.com/comments";

            //TODO: you can also take the settings.Filter value and add it to the URL to filter by first_name and email, just 2 should be enough for this sample.
            //      this api does not support using OR in the parameters so you will have to call the api twice if there is a filter value, one to filter by fist_name and again for email.
            //      you will have to then check for and exclude duplicates using the id value.
            //      You will have to use linq's union and/or except to join the lists to get to correct TotalCount value if you make more than 1 call

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
                Comment[] comments = JsonConvert.DeserializeObject<Comment[]>(result);
                RecordsList.Clear();
                foreach (var comment in comments)
                {
                    if (settings.Filter != "")
                    {
                        if (comment.name.ToLower().Contains(settings.Filter.ToLower()) || comment.email.ToLower().Contains(settings.Filter.ToLower()) || comment.body.ToLower().Contains(settings.Filter.ToLower()))
                            RecordsList.Add(new
                            {
                                Id = comment.id,
                                Name = comment.name,
                                Body = comment.body,
                                PostId = comment.postId,
                                Email = comment.email
                            });
                        } else {
                            RecordsList.Add(new
                            {
                                Id = comment.id,
                                Name = comment.name,
                                Body = comment.body,
                                PostId = comment.postId,
                                Email = comment.email
                            });
                    }
                }

                TotalCount = RecordsList.Count;
                //TODO: Make it work when the user selects all
                if (settings.LinesPerPage > RecordsList.Count) // this is when the user sets the items per page to 25, 50, 100 or ALL. 
                {
                    // TODO: download next page from gorest.co.in because the user wants to see more items on the screen
                    //       Use recursion for this. You may need to create a separate method to do the downloading
                }
            }
        }
    }
    public class Comment
    {
        public int postId { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string body { get; set; }
    }
}