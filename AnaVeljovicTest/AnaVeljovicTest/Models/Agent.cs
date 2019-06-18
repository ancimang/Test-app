using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnaVeljovicTest.Models
{
    public class Agent
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ImeIprezime { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 4)]
        public string Licenca { get; set; }

        [Range(1951, 1995)]
        public int GodinaRodjenja { get; set; }

        [Required]
        [Range(int.MinValue, 50)]
        public int BrojNekretnina { get; set; }
    }
}