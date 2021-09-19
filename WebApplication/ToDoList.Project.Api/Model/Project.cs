using Newtonsoft.Json;

namespace ToDoList.ProjectManage.Api.Model
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int? ClientId { get; set; }
        [JsonIgnore]
        public Client Client { get; set; }
    }
}
