using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Awards.Models
{
    public class UsersAwards
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AwardId { get; set; }
        public Award Award { get; set; }
    }
}
