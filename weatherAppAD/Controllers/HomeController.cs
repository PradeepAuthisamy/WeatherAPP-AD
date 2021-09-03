using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using weatherAppAD.Models;

namespace weatherAppAD.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        //private readonly ITokenAcquisition _tokenAcquisition;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger,
                              //ITokenAcquisition tokenAcquisition,
                              IConfiguration config)
        {
            _logger = logger;
            //_tokenAcquisition = tokenAcquisition;
            _config = config;
        }

        //[AuthorizeForScopes(Scopes = new[] { "Read" })]
        public async Task<IActionResult> Index()
        {
            //AuthConfig config = AuthConfig.ReadFromJsonFile("appsettings.json");
            //var scopes = new string[] { "Read" };
            //string accessToken = await _tokenAcquisition.GetAccessTokenForUserAsync(scopes);

            //var client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization =
            //                new AuthenticationHeaderValue("Bearer", accessToken);

            //string uri = _config.GetValue<string>(config.BaseAddress);
            //var result = await client.GetAsync(uri);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}