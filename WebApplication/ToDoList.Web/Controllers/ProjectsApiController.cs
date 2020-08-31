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
    public class ProjectsApiController : Controller
    {
        private readonly ApiClient _projectManageApi;

        public ProjectsApiController(ApiClient projectManageApi)
        {
            _projectManageApi = projectManageApi;
        }
        // GET: ProjectsApi
        public async Task<IActionResult> Index()
        {
            return View(await _projectManageApi.ProjectsGetAsync());
        }

        
        public async Task<IActionResult> Details(int id)
        {
            return View(await _projectManageApi.ProjectsGetAsync(id));
        }
    }
}
