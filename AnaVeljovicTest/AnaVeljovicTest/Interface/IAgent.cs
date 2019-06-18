using AnaVeljovicTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnaVeljovicTest.Interface
{
    public interface IAgent
    {
        IEnumerable<Agent> GetAll();
        Agent GetById(int id);
        IEnumerable<Agent> GetExtremi();
        IEnumerable<Agent> GetNajmladji();
    }
}
