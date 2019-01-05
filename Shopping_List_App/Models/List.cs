using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping_List_App.Models
{
    [Table("Lists")]
    public class List
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName ="nvarchar(50)")]
        [Required(ErrorMessage = "Sie müssen einen Titel angeben!")]
        public string Title { get; set; }
        [DisplayName("User ID")]
        public int? UserId { get; set; }

        public List<ListItem> ListItems { get; set; }
    }
}
