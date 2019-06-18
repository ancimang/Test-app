using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AnaVeljovicTest.Models
{
    public class Nekretnina
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Mesto { get; set; }
        [Required]
        [StringLength(5)]
        public string Oznaka { get; set; }

        [Range(1900, 2018)]
        public int GodinaIzgradnje { get; set; }

        [Required]
        [Range(2.1, double.MaxValue)]
        public decimal Kvadratura { get; set; }
        [Required]
        [Range(0.1, 100000.0)]
        public decimal Cena { get; set; }

        public int AgentId { get; set; }
        public Agent Agent { get; set; }
    }
}