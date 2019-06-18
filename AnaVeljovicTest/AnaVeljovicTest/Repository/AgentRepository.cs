using AnaVeljovicTest.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AnaVeljovicTest.Models;

namespace AnaVeljovicTest.Repository
{
    public class AgentRepository : IDisposable, IAgent
    {

        public ApplicationDbContext db = new ApplicationDbContext();

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Agent> GetAll()
        {

            return db.Agenti;
        }

        public Agent GetById(int id)
        {
            return db.Agenti.FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Agent> GetExtremi()
        {

            return (db.Agenti.OrderByDescending(b => b.BrojNekretnina).Take(1)).Union(db.Agenti.OrderBy(b => b.BrojNekretnina).Take(1));
         
        }

        public IEnumerable<Agent> GetNajmladji()
        {
            return db.Agenti.OrderByDescending(a => a.GodinaRodjenja);
        }
    }
}