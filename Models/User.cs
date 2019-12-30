using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Awards.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Имя не должно быть пустым")]
        [StringLength(50, ErrorMessage = "Длина не должна превышать 50 символов")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Необходимо указать дату рождения")]
        [Remote("CheckDateOfBirth", "Home", ErrorMessage = "Некорректно указана дата рождения")]
        public DateTime BirthDate { get; set; }
        public string Photo { get; set; }
        public int Age { get; set; }
        [NotMapped]
        public List<Award> Awards { get; set; }
        [NotMapped]
        public IFormFile PhotoData { get; set; }
    }
}
