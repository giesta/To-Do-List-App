using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace ToDoList.Web.Services.ToDoList
{
    public class InFileProvider<TypeOfValue>:Provider<TypeOfValue> where TypeOfValue:IHasId
    {
        private readonly string fileName;

        public InFileProvider(string fileName) : base(ReadFile(fileName))
        {
            this.fileName = fileName;
        }
        public override void Add(TypeOfValue dataItem)
        {
            base.Add(dataItem);
            SaveData(GetAll(), fileName);
        }

        public override void Update(TypeOfValue dataItem)
        {
            base.Update(dataItem);
            SaveData(GetAll(), fileName);
        }

        public override void Remove(TypeOfValue type)
        {
            base.Remove(type);
            SaveData(GetAll(), fileName);
        }
        private static List<TypeOfValue> ReadFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                string data = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<List<TypeOfValue>>(data);
            }
            return new List<TypeOfValue>();
        }
        private static void SaveData(List<TypeOfValue> data, string fileName)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            File.WriteAllText(fileName, jsonData);
        }
    }
}
