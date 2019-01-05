using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping_List_App.Models
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "Sie müssen einen Titel angeben!")]
        public string Title { get; set; }
        [Column(TypeName = "varbinary(MAX)")]
        [Required(ErrorMessage = "Wählen Sie ein Icon aus")]
        public string Icon { get; set; }
    }
}
