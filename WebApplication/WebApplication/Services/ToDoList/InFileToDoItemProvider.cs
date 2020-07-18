using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.Models;

namespace WebApplication.Services.ToDoList
{
    public class InFileToDoItemProvider : IInMemoryToDoItemProvider
    {
        private readonly string fileName = "file.txt";

        private List<ToDoItem> ReadFromFile()
        {
            string[] lines;
            var list = new List<ToDoItem>();
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(";");
                    list.Add(new ToDoItem() { Id = Int16.Parse(lines[0]), Name = lines[1], Description = lines[2], Priority = Int16.Parse(lines[3]) });                    
                }
            }
            return list;
        }
        private int FindId()
        {
            List<ToDoItem> toDoItems = ReadFromFile();
            int index = 0;
            bool find;
            for (int i = 0; i < toDoItems.Count; i++)
            {
                find = true;
                foreach(ToDoItem toDoItem in toDoItems)
                { 
                    if (index == toDoItem.Id)
                    {
                        index++;
                        find = false;
                        break;
                    }
                }
                if (find)
                {
                    return index;
                }
            }
            return index;
        }
        private void WriteToFile(ToDoItem toDoItem)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(toDoItem.Id+";"+toDoItem.Name+";"+toDoItem.Description+";"+toDoItem.Priority);
            }
        }
        private void ReWriteToFile(ToDoItem toDoItem)
        {
            string[] lines;
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(";");
                    using (StreamWriter writer = new StreamWriter("temp.txt", true))
                    {
                        if(lines[0] == toDoItem.Id.ToString())
                        {
                            writer.WriteLine(toDoItem.Id + ";" + toDoItem.Name + ";" + toDoItem.Description + ";" + toDoItem.Priority);
                        }
                        else
                        {
                            writer.WriteLine(line);
                        }                       
                    }
                }
            }
            var fileStream2 = new FileStream("temp.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream2, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    using (StreamWriter writer = new StreamWriter(fileName))
                    {
                        writer.WriteLine(line);
                    }
                }
            }

        }
        private ToDoItem GetFromFileById(int id)
        {
            string[] lines;
            ToDoItem toDoItem = null;
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(";");
                    if(Int16.Parse(lines[0]) == id)
                    {
                        toDoItem = new ToDoItem() { Id = Int16.Parse(lines[0]), Name = lines[1], Description = lines[2], Priority = Int16.Parse(lines[3]) };
                        return toDoItem;
                    }                   

                }
            }
            return toDoItem;
        }
        private void RemoveFromFileById(int id)
        {
            int count = 0;
            string[] lines;
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(";");
                    using (StreamWriter writer = new StreamWriter("temp.txt", true))
                    {
                        if (Int16.Parse(lines[0]) != id)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
            if (File.Exists("temp.txt"))
            {
                File.Delete(fileName);
                var fileStream2 = new FileStream("temp.txt", FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream2, Encoding.UTF8))
            {
                string line;
                    
                while ((line = streamReader.ReadLine()) != null)
                {
                    using (StreamWriter writer = new StreamWriter(fileName, true))
                    {
                        writer.WriteLine(line);
                        count++;
                    }
                }
                
                }
                    
                File.Delete("temp.txt");
            }
            if (count == 0)
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    writer.Write("");
                    count++;
                }
            }
        }
        public void Add(ToDoItem toDoItem)
        {
            WriteToFile(toDoItem);
        }

        public ToDoItem Get(int id)
        {
            return GetFromFileById(id);
        }

        public List<ToDoItem> GetAll()
        {
            return ReadFromFile();
        }

        public int GetIndexToInsert()
        {
            
            return FindId();
        }

        public void Remove(ToDoItem toDoItem)
        {
            RemoveFromFileById(toDoItem.Id);
        }
    }
}
