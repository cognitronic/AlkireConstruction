using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace RAM.Infrastructure.Configuration
{
    public class WebConfigApplicationSettings : IApplicationSettings
    {
        public string LoggerName
        {
            get
            {
                return ConfigurationManager.AppSettings["LoggerName"];
            }
        }

        public string NumberOfResultsPerPage
        {
            get
            {
                return ConfigurationManager.AppSettings["NumberOfResultsPerPage"];
            }
        }

        //LitStar acct#: 4a20eb1e0169c84d5e30086cee121acdd65539b0
        public string JanrainApiKey
        {
            get
            {
                return ConfigurationManager.AppSettings["JanrainApiKey"];
            }
        }

        //URL: https://rpxnow.com/api/v2/auth_info
        public string JanrainURL
        {
            get
            {
                return ConfigurationManager.AppSettings["JanrainURL"];
            }
        }

        public string SmtpHost
        {
            get
            {
                return ConfigurationManager.AppSettings["SmtpHost"];
            }
        }

        public string SmtpUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["SmtpUsername"];
            }
        }

        public string SmtpPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["SmtpPassword"];
            }
        }

        public string PersistenceStrategy
        {
            get
            {
                return ConfigurationManager.AppSettings["PersistenceStrategy"];
            }
        }

        public string DatabaseServerName
        {
            get
            {
                return ConfigurationManager.AppSettings["DatabaseServerName"];
            }
        }

        public string DatabaseName
        {
            get
            {
                return ConfigurationManager.AppSettings["DatabaseName"];
            }
        }

        public string DBUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["DBUsername"];
            }
        }

        public string DBPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["DBPassword"];
            }
        }
    }
}
