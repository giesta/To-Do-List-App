using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ToDoList.Business.Models;
using ToDoList.Data.Models.ToDoList;

namespace ToDoList.Web.ViewModel.ToDoList
{
    public class ToDoItemViewModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        #nullable enable
        public string? Description { get; set; }
        #nullable disable
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime CreationDate { get; set; }
        #nullable enable
        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]        
        public DateTime? DeadLineDate { get; set; }
        #nullable disable
        [Range(1, 5)]
        [DefaultValue(3)]
        [Required]
        public int Priority { get; set; }
        #nullable enable
        [DefaultValue(null)]        
        public int? CategoryID { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = true, NullDisplayText = "Uncategorized")]
        [DefaultValue(null)]
        public virtual CategoryDao? Category { get; set; }
        #nullable disable
        [DefaultValue("Backlog")]
        public StatusName Status { get; set; }

        public IList<TagToDoItemDao> TagToDoItems { get; set; }
        public override bool Equals(object obj)
        {
            return obj is ToDoItemViewModel item &&
                   Id == item.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
        
    }
    public enum StatusName
    {
        Backlog,
        Wip,
        Done,
        Archived
    }

}
