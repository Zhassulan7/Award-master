using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Awards.Models
{
    public class Award
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо прописать название награды")]
        [StringLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+[A-Za-z0-9.-]", ErrorMessage = "Название должно содержать только латинские символы")]
        public string Title { get; set; }
        [StringLength(250, ErrorMessage = "Длина не должна превышать 250 символов")]
        public string Description { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageData { get; set; }
    }
}
