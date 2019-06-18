using AnaVeljovicTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaVeljovicTest.Interface
{
     public interface INekretnina
    {
        IEnumerable<Nekretnina> GetAll();
        Nekretnina GetById(int id);
        void Add(Nekretnina nekretnina);
        void Update(Nekretnina nekretnina);
        void Delete(Nekretnina nekretnina);
        bool NekretninaExist(Nekretnina nekretnina);
        IEnumerable<Nekretnina> GetNapravljeno(int napravljeno);
        IEnumerable<Nekretnina> PostPretraga(decimal x, decimal y);

    }
}
