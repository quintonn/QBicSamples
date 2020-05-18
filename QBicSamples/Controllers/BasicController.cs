using BasicAuthentication.Security;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebsiteTemplate.Controllers;

namespace QBicSamples.Controllers
{
    [RoutePrefix("test/api")]   // this is an optional value and applies to all methods in this controller class
    public class BasicController : ApiController
    {
        [HttpGet] // support only HTTP GET Method
        [Route("hello")] // the route in the URL to use
        //[Route("{path}")]
        [RequireHttps] // force URL to use HTTPS
        [DeflateCompression] // deflates the response. Good when returning larger results.
        public async Task<IHttpActionResult> This_Can_Be_Any_Name()
        {
            // Browse to https://localhost/qBicSamples/test/api/hello to see the value "Hello World" returned in the browser
           
            HttpClient cli = new HttpClient();
            HttpResponseMessage req = await cli.GetAsync("https://api.weather.gov/points/39.7456,-97.0892");
            
            try
            {
                return Redirect("http://www.kp.md");
                //return Json("Hello World");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }

        [HttpGet]
        [Route("auth")]
        [RequireHttps] // force URL to use HTTPS
        [Authorize] // Requires call to be authorized
        [DeflateCompression] // deflates the response. Good when returning larger results.
        public async Task<IHttpActionResult> Auth_Test()
        {
            // Browse to https://localhost/qBicSamples/test/api/auth to see this request blocked because it is not authenticated
            // To pass authorization, one needs to call the token endpoint with the same credentials used to log into the QBic app and then use the token returned from that. 
            // More on that later.

            try
            {
                return Json("You are authorized");
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }
    }
}