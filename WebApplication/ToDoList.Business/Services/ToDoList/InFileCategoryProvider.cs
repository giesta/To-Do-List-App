using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Business.Services.ToDoList
{
    public class InFileCategoryProvider : ICategoryProvider
    {
        private readonly string fileName = "category.txt";
        public void Add(CategoryDao categoryDao)
        {
            categoryDao.Id = GetUniqueId();
            WriteToFile(categoryDao);
        }

        public CategoryDao Get(int id)
        {
            return GetFromFileById(id);
        }

        public List<CategoryDao> GetAll()
        {
            return ReadFromFile();
        }
        
        public int GetIndexToInsert()
        {
            return GetUniqueId();
        }

        public void Remove(CategoryDao categoryDao)
        {
            RemoveFromFileById(categoryDao.Id);
        }

        /// <summary>
        /// Read all categories from the file
        /// </summary>
        /// <returns>Returns list of categories</returns>
        private List<CategoryDao> ReadFromFile()
        {
            string[] lines;
            var list = new List<CategoryDao>();
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(";");
                    list.Add(new CategoryDao() { Id = Int16.Parse(lines[0]), Name = lines[1] });
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
            List<CategoryDao> categories = ReadFromFile();
            int index = 0;
            bool find;
            for (int i = 0; i < categories.Count; i++)
            {
                find = true;
                foreach (CategoryDao category in categories)
                {
                    if (index == category.Id)
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
        /// Write a category into the file
        /// </summary>
        private void WriteToFile(CategoryDao categoryDao)
        {
            using (StreamWriter writer = new StreamWriter(fileName, true))
            {
                writer.WriteLine(categoryDao.Id + ";" + categoryDao.Name);
            }
        }
        /// <summary>
        /// Rewrite data of the temp.txt file to the category.txt file
        /// </summary>
        private void RewriteFiles()
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
        /// Get a category from the file by ID
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>Returns a category</returns>
        private CategoryDao GetFromFileById(int id)
        {
            string[] lines;
            CategoryDao categoryDao = null;
            var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
            {
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    lines = line.Split(";");
                    if (Int16.Parse(lines[0]) == id)
                    {
                        categoryDao = new CategoryDao() { Id = Int16.Parse(lines[0]), Name = lines[1] };
                        return categoryDao;
                    }

                }
            }
            return categoryDao;
        }
        /// <summary>
        /// Remove a category from the file by ID
        /// </summary>
        /// <param name="id"> Category ID</param>
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
            RewriteFiles();
        }

        public void Update(CategoryDao categoryDao)
        {
            RemoveFromFileById(categoryDao.Id);
            WriteToFile(categoryDao);
        }
    }
}
