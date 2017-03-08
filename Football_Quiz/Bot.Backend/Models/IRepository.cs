using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Backend.Models
{
    public interface IRepository<T> where T : class
    {
        List<string> GetAll();
        T Get(string name);
    }
}
