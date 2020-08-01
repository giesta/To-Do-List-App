using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using WebApplication.Services.ToDoList;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class ToDoItem : IHasId
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
        public virtual Category? Category { get; set; }
        #nullable disable
        [DefaultValue("Backlog")]
        public StatusName Status { get; set; }        
        public override bool Equals(object obj)
        {
            return obj is ToDoItem item &&
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
