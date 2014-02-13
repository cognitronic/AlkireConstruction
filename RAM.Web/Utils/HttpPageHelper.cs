using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.User;
using IdeaSeed.Web;
using IdeaSeed.Core;
using System.Configuration;
using System.Web;
using System.Web.UI;

namespace RAM.Web.Utils
{
    public class HttpPageHelper
    {
        public static User CurrentUser
        {
            get { return HttpContextHelper.Get<User>("SQ_CURRENTUSER"); }
            set { HttpContextHelper.Set("SQ_CURRENTUSER", value); }
        }

        public static User CurrentProfile
        {
            get { return HttpContextHelper.Get<User>("SQ_CURRENTPROFILE"); }
            set { HttpContextHelper.Set("SQ_CURRENTPROFILE", value); }
        }
    }
}
