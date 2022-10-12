using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet.DTO.Fight
{
    public class HighscoreDTO
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Fights { get; set; }
        public int Victories { get; set; }
        public int Defeats { get; set; }
    }
}