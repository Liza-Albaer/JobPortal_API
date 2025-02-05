using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.core.Models
{
    public class Job
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int JobId { get; set; } 
        [Required]
        [Column(TypeName = "Nvarchar(50)")]
        public string Title { get; set; }
        [Required]
        [Column(TypeName = "Nvarchar(50)")]
        public string Company { get; set; }
        [Column(TypeName = "Nvarchar(max)")]
        public string Location { get; set; } = "";
        [Required]
        [Column(TypeName = "Nvarchar(max)")]
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "Nvarchar(max)")]
        public string Requirements { get; set; }
        public DateTime PostedDate { get; set; }
    }
}
