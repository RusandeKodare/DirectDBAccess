using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectDBAccess
{
    public interface IDatabase
    {
        public void Insert(string firstName, string lastName);
    }
}
