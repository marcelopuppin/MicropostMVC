using System;
using System.ComponentModel.DataAnnotations;

namespace MicropostMVC.Models
{
    public class MicropostModel : ModelWithId
    {
        [Required, StringLength(140)]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
    }
} 


