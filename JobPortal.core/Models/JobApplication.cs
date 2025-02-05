using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace JobPortal.core.Models
{
    public class JobApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ApplicationId { get; set; } 
        
       
        [Required]
        [Column(TypeName = "Nvarchar(100)")]

        public string Name { get; set; }
        [Required]
        [Column(TypeName = "Nvarchar(max)")]
        public string Email { get; set; }
        [Required]
        [Column(TypeName = "Nvarchar(max)")]
        public string ResumeUrl { get; set; } 
        public DateTime AppliedDate { get; set; } 

        
        public Job Job { get; set; }
        public int JobId { get; set; }

    }
}
