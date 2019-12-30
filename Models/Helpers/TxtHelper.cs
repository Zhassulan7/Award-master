using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Awards.Models.Helpers
{
    public class TxtHelper
    {
        public void CreateTxtWithUsers(List<User> users)
        {
            string filePath = "wwwroot/files/usersList.txt";
            if (!File.Exists(filePath))
            {
                var stream = new FileStream(filePath, FileMode.Create);
                stream.Close();
            }
            var streamWriter = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
            streamWriter.WriteLine("#    Возраст    Имя    Дата рождения                  Награды");
            foreach (var user in users)
            {
                var awards = "";
                int i = 1;
                if (user.Awards != null)
                {
                    foreach (var award in user.Awards)
                    {
                        awards += $"{i}) {award.Title} - ({award.Description}); ";
                        i++;
                    }
                }
                if (user.Awards.Count() == 0)
                {
                    awards = "Награды отсутствуют";
                }
                streamWriter.WriteLine($"{user.Id, -3})  {user.Age, -3} -- {user.Name, -10} -- {user.BirthDate.ToLongDateString(), -10} -- {awards} ");
            }
            streamWriter.Close();
        }
        public FileResult GetUsersTxt()
        {
            string filePath = "wwwroot/files/usersList.txt";
            return new FileStreamResult(new FileStream(filePath, FileMode.Open), "text/txt") { FileDownloadName = "users.txt" };
        }
    }
}
