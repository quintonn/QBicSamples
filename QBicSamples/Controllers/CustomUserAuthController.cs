using BasicAuthentication.Security;
using QBicSamples.SiteSpecific;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;
using WebsiteTemplate.Controllers;
using WebsiteTemplate.Utilities;

namespace QBicSamples.Controllers
{
    /* This class contains code that demonstrates setting up additional API's for external systems,
     * as well as a custom identity service to have users
     * for the external system be separate from the QBic users who are able to log into this website.
     * 
     * To be able to setup a custom Identity Service, a few things are needed, and have been added:
     *   1. A new "User" class. For this we created CustomUser class and can be found in \Models\CustomUser.cs
     *   2. This class is not a QBic DynamicClass and needs a NHibernate mapping file. This can be found in \Mappings\CustomUserMap.cs
     *   3. A custom UserContext class. This can be found in \SiteSpecific\CustomUserContext.cs. 
     *   This allows QBic to search for users and validate their passwords when they try log in.
     *   4. We need to register our CustomUserContext and configure the (token) path or URL to use for loging users in and getting new tokens.
     *      This is done in the \SiteSpecific\AppSettings.cs class.
     * 
     * We have also provided some code in the \SiteSpecific\Startup.cs file which creates a new Custom User, if one doesn't already exist'.
     * This check is performed everytime the web server restarts or recycles. This simply demonstrates how to create a new user.
     * Since users are not simply created by adding a User object into the database.
     * Instead, a UserManager is resolved and this is responsible for hashing and encrypting the password before placing it in the database.
     * 
     * 
     * 
     */
    [RoutePrefix("custom/api")]
    public class CustomUserAuthController : ApiController
    {
        [HttpGet]
        [Route("login")]
        [RequireHttps] // force URL to use HTTPS
        [DeflateCompression] // deflates the response. Good when returning larger results.
        public async Task<IHttpActionResult> Do_Login()
        {
            try
            {
                // Let's use this method to call the custom log in endpoint to demonstrate how that would work
                // This would typically be done from an external system or mobile application, so this is just an example.
                // You probably wouldn't and shouldn't do a login like this

                // Browse to https://localhost/qBicSamples/custom/api/login to have this execute and return a successful message

                var customUserLoginUrl = "https://localhost/QBicSamples" + AppSettings.CUSTOM_TOKEN_PATH;

                // This code is needed to trust our self-signed cert on localhost. Should not be used in real life
                ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

                // This handler is used to decompress the response from the http call
                var handler = new HttpClientHandler()
                {
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                };
                var client = new HttpClient(handler);

                var loginContent = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "bob@example.com"),
                    new KeyValuePair<string, string>("password", "password"),
                    new KeyValuePair<string, string>("client_id", AppSettings.CUSTOM_CLIENT_ID),
                });

                // The same URL is used to get a new token, using a previous and valid refresh token, but with the following data instead
                /*
                        var refreshTokenContent = new FormUrlEncodedContent(new[]
                        {
                            new KeyValuePair<string, string>("grant_type", "refresh_token"),
                            new KeyValuePair<string, string>("refresh_token", "<refresh_token>"),
                            new KeyValuePair<string, string>("client_id", AppSettings.CUSTOM_CLIENT_ID),
                        });
                */
                var response = await client.PostAsync(customUserLoginUrl, loginContent);
                
                /* You can also call the URL as follows from C# code
                var data = "grant_type=password&username=bob@example.com&password=password&client_id=" + AppSettings.CUSTOM_CLIENT_ID;
                var content = new StringContent(data);
                var response = await client.PostAsync(customUserLoginUrl, content);
                */

                var responseString = await response.Content.ReadAsStringAsync();

                var tokenJson = JsonHelper.Parse(responseString);
                var access_token = tokenJson.GetValue("access_token")?.ToString();
                var token_type = tokenJson.GetValue("")?.ToString();
                var expires_in = tokenJson.GetValue("expires_in")?.ToString();
                var refresh_token = tokenJson.GetValue("refresh_token")?.ToString();  // refresh token is used to get a new access_token
                
                var userName = tokenJson.GetValue("userName")?.ToString();

                var message = $"You are authorized as {userName}";

                // let's also call the "test2" method below using our access token
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);
                //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
                response = await client.GetAsync("https://localhost/QBicSamples/custom/api/test");
                responseString = await response.Content.ReadAsStringAsync();


                message = $"{message}    -    {responseString}";

                return Json(message);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [Route("test")]
        [RequireHttps] // force URL to use HTTPS
        [Authorize] // Requires call to be authorized
        [DeflateCompression] // deflates the response. Good when returning larger results.
        public async Task<IHttpActionResult> Test()
        {
            // Browse to https://localhost/qBicSamples/test/api/auth to see this request blocked because it is not authenticated
            // To pass authorization, one needs to call the token endpoint with the same credentials used to log into the QBic app and then use the token returned from that. 
            // More on that later.

            try
            {
                return Json("This is a value behind authorization");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}