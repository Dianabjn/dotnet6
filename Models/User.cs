using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.Models
{
    public class User
    {
        public int ID {get; set;}
        public string Username {get; set;} = string.Empty;
        public byte[] PasswordHash {get; set;}
        public byte[] PasswordSalt {get; set;}
        public List<Character>? Characters {get; set;}
    }
}