using System.Collections;
using System.Collections.Generic;

namespace ToDoList.ProjectManage.Api.Model
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<Project>Projects { get; set; }

    }
}
