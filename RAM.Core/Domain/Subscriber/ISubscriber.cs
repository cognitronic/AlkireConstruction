using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Infrastructure.Domain;

namespace RAM.Core.Domain.Subscriber
{
    public interface ISubscriber : ISystemObject
    {
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Phone { get; set; }
        DateTime DateCreated { get; set; }

    }
}
