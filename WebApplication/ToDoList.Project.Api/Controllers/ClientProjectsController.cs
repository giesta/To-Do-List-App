using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoList.ProjectManage.Api.Data;
using ToDoList.ProjectManage.Api.Model;

namespace ToDoList.ProjectManage.Api.Controllers
{
    [Route("api/Clients/{clientId}/Projects")]
    [ApiController]
    public class ClientProjectsController : ControllerBase
    {
        private readonly ToDoListProjectApiContext _context;
        public ClientProjectsController(ToDoListProjectApiContext context)
        {
            _context = context;
        }

        // GET: api/Clients/1/Projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetClient(int clientId)
        {
            return await _context.Project.Where(m=>m.ClientId==clientId).ToListAsync();
        }

        [HttpPut("{projectId}")]
        public async Task<IActionResult> PutClient( int clientId, int projectId)
        {
            var project = await _context.Project.FindAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            project.ClientId = clientId;

            _context.Entry(project).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
