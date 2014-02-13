using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Infrastructure.Session
{
    public interface ISessionProvider
    {
        object this[int index] { get; set; }
        object this[string name] { get; set; }
    }
}
