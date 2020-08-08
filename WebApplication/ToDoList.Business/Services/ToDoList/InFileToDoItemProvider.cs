using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ToDoList.Business.Models.ToDoList;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Business.Models;

namespace ToDoList.Web.Services.ToDoList
{
    public class InFileToDoItemProvider : IToDoItemProvider
    {
        private readonly string fileName = "toDoItem.txt";
        
        public void Add(ToDoItemDao toDoItemDao)
        {
            toDoItemDao.Id = GetUniqueId();
            WriteToFile(toDoItemDao);
        }

        public ToDoItemDao Get(int id)
        {
            return GetFromFileById(id);
        }

        public List<ToDoItemDao> GetAll()
        {
            return ReadFromFile();
        }

        public int GetIndexToInsert()
        {            
            return GetUniqueId();
        }

        public void Remove(ToDoItemDao toDoItemDao)
        {
            RemoveFromFileById(toDoItemDao.Id);
        }
        /// <summary>
        /// Read all categories from the file
        /// </summary>
        /// <returns>Returns list of ToDoItems</returns>
        private List<ToDoItemDao> ReadFromFile()
        {
            string[] lines;
            var list = new List<ToDoItemDao>();
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(";");
                    list.Add(new ToDoItemDao() { Id = Int16.Parse(lines[0]), Name = lines[1], Description = lines[2], Priority = Int16.Parse(lines[3]) });
                }
            }
            return list;
        }
        /// <summary>
        /// Get ID that is unique
        /// </summary>
        /// <returns>Returns ID</returns>
        private int GetUniqueId()
        {
            List<ToDoItemDao> toDoItems = ReadFromFile();
            int index = 0;
            bool find;
            for (int i = 0; i < toDoItems.Count; i++)
            {
                find = true;
                foreach (ToDoItemDao toDoItem in toDoItems)
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
        /// <summary>
        /// Write a ToDoItem into the file
        /// </summary>
        private void WriteToFile(ToDoItemDao toDoItemDao)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(toDoItemDao.Id + ";" + toDoItemDao.Name + ";" + toDoItemDao.Description + ";" + toDoItemDao.Priority);
            }
        }
        /// <summary>
        /// Rewrite data of the temp.txt file to the toDoItem.txt file
        /// </summary>
        private void RewriteToFile()
        {
            int count = 0;
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
        /// <summary>
        /// Get a ToDoItem from the file by ID
        /// </summary>
        /// <param name="id">ToDoItem ID</param>
        /// <returns>Returns a ToDoItem</returns>
        private ToDoItemDao GetFromFileById(int id)
        {
            string[] lines;
            ToDoItemDao toDoItemDao = null;
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(";");
                    if (Int16.Parse(lines[0]) == id)
                    {
                        toDoItemDao = new ToDoItemDao() { Id = Int16.Parse(lines[0]), Name = lines[1], Description = lines[2], Priority = Int16.Parse(lines[3]) };
                        return toDoItemDao;
                    }

                }
            }
            return toDoItemDao;
        }
        /// <summary>
        /// Remove a ToDoItem from the file by ID
        /// </summary>
        /// <param name="id"> ToDoItem ID</param>
        private void RemoveFromFileById(int id)
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
                        if (Int16.Parse(lines[0]) != id)
                        {
                            writer.WriteLine(line);
                        }
                    }
                }
            }
            RewriteToFile();
        }

        public void Update(ToDoItemDao toDoItemDao)
        {
            RemoveFromFileById(toDoItemDao.Id);
            WriteToFile(toDoItemDao);
        }
    }
}
