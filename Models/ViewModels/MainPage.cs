using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awards.Models.ViewModels
{
    public class MainPage
    {
        public List<User> Users { get; set; }
        public List<Award> Awards { get; set; }
    }
}
