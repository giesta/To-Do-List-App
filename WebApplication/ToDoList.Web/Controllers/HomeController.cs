using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.ProjectManage.ApiClient;
using ToDoList.Web.Models;

namespace ToDoList.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApiClient _projectManageApi;

        public HomeController(ILogger<HomeController> logger, ApiClient projectManageApi)
        {
            _logger = logger;
            _projectManageApi = projectManageApi;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<string> TestApi()
        {
            var client = await _projectManageApi.ClientsGetAsync(1);
            return client.Name;
        }
    }
}
