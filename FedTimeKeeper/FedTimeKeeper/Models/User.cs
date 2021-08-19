using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FedTimeKeeper.Models
{
    public class User
    {
        [AutoIncrement, PrimaryKey]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
