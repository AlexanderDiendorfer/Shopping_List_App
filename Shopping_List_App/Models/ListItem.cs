using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping_List_App.Models
{
    [Table("ListItems")]
    public class ListItem
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName= "nvarchar(50)")]
        [Required(ErrorMessage = "Sie müssen einen Titel angeben!")]
        public string Title { get; set; }
        public bool Status { get; set; }

        public int ListId { get; set; }
        public List List { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
