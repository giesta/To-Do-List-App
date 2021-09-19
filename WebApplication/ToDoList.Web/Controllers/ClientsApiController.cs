using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ToDoList.ProjectManage.ApiClient;

namespace ToDoList.Web.Controllers
{
    public class ClientsApiController : Controller
    {
        private readonly ApiClient _projectManageApi;

        public ClientsApiController(ApiClient projectManageApi)
        {
            _projectManageApi = projectManageApi;
        }
        // GET: ClientsApi
        public async Task<IActionResult> Index()
        {
            return View(await _projectManageApi.ClientsGetAsync());
        }

        // GET: ClientsApi/Details/5
        public async Task<IActionResult> Details(int id)
        {
            return View(await _projectManageApi.ClientsGetAsync(id));
        }

       
    }
}
