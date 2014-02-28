using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Subscriber;

namespace RAM.Services.Interfaces
{
    public interface ISubscriberService
    {
        Subscriber GetByID(int subscriberID);
        Subscriber GetByEmail(string email);
        IList<Subscriber> GetAll();
        void Save(Subscriber subscriber);
        void Delete(Subscriber subscriber);
    }
}
