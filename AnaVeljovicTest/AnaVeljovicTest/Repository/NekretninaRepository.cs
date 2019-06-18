using AnaVeljovicTest.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using AnaVeljovicTest.Models;

namespace AnaVeljovicTest.Repository
{
    public class NekretninaRepository : IDisposable, INekretnina
    {

        public ApplicationDbContext db = new ApplicationDbContext();

        public void Add(Nekretnina nekretnina)
        {
            db.Nekretnine.Add(nekretnina);
            db.SaveChanges();
        }

        public void Delete(Nekretnina nekretnina)
        {
            db.Nekretnine.Remove(nekretnina);
            db.SaveChanges();
        }

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

        public IEnumerable<Nekretnina> GetAll()
        {
            return db.Nekretnine.Include(a => a.Agent).OrderByDescending(c => c.Cena);
        }

        public Nekretnina GetById(int id)
        {
            return db.Nekretnine.Include(a => a.Agent).FirstOrDefault(k => k.Id == id);
        }

        public IEnumerable<Nekretnina> GetNapravljeno(int napravljeno)
        {
            return db.Nekretnine.Include(a => a.Agent).Where(g => g.GodinaIzgradnje > napravljeno).OrderBy(g => g.GodinaIzgradnje);
        }

        public bool NekretninaExist(Nekretnina nekretnina)
        {
            return db.Nekretnine.Count(e => e.Id == nekretnina.Id) > 0;
        }

        public IEnumerable<Nekretnina> PostPretraga(decimal mini, decimal maksi)
        {
            return db.Nekretnine.Include(a => a.Agent).Where(g => g.Kvadratura >=mini && g.Kvadratura < maksi).OrderBy(g => g.Kvadratura);
        }

        public void Update(Nekretnina nekretnina)
        {
            db.Entry(nekretnina).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NekretninaExist(nekretnina))
                {
                    throw;
                }
                throw;
            }
        }
    }
}