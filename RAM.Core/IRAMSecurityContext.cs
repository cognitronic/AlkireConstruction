using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Authentication;

namespace RAM.Core
{
    public interface IRAMSecurityContext : ISecurityContext
    {
        int CurrentAccessLevel { get; set; }
    }
}
