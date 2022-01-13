using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EatAsparagus.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int EatCount { get; set; }
        public DateTime EatDate { get; set; }
    }
}