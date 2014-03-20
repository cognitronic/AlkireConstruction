using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Infrastructure.Configuration
{
    public interface IApplicationSettings
    {
        string LoggerName { get; }
        string NumberOfResultsPerPage { get; }
        string JanrainApiKey { get; }
        string JanrainURL { get; }
        string SmtpHost { get; }
        string SmtpUsername { get; }
        string SmtpPassword { get; }
        string PersistenceStrategy { get; }
        string DatabaseServerName { get; }
        string DatabaseName { get; }
        string DBUsername { get; }
        string DBPassword { get; }
    }
}
