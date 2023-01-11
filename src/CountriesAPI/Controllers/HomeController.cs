using Microsoft.AspNetCore.Mvc;

namespace CountriesAPI.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var apiLink = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/swagger";
            var projLink = "https://github.com/CristianoCricas/CountriesAPI/blob/main/README.md";
            var music = "https://www.youtube.com/watch?v=ks7RrRFd-20";

            string welcomeView =
                "<html>" +
                "<body>" +
                "<h1>CountriesAPI</h1>" +
                "<h4>" +
                "For SWAGGER documentation, access: " +
               $"<br><a href={apiLink}>{apiLink}</a>" +
                "</h4>" +
                "<h4>" +
                "For project documentation, access: " +
               $"<br><a href={projLink}>{projLink}</a>" +
                "</h4>" +
               $"<p>Footline: <a href={music} target=\"_blank\">hear this!</a>" +
                "</body>" +
                "</html>";

            return base.Content(welcomeView, "text/html");
        }
    }
}
