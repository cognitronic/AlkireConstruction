using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core;
using RAM.Web.Security;
using RAM.Infrastructure.Session;
using IdeaSeed.Core.Utils;
using RAM.Web.Utils;
using IdeaSeed.Web;

namespace RAM.Web.Bases
{
    public class CultureBossBasePage : System.Web.UI.Page
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //if (!Request.IsLocal && !Request.IsSecureConnection)
            //    Response.Redirect(Request.Url.ToString().Replace("http:", "https:"));

            if (SecurityContextManager.Current != null)
            {
                //SecurityContextManager.Current.CurrentPage = HttpPageHelper.CurrentPage;
                //SecurityContextManager.Current.CurrentAccessLevel = (AccessLevels)new SecurityServices().GetCurrentPageAccessLevel(SecurityContextManager.Current);
                //if (HttpPageHelper.CurrentItem != null)
                //    SessionManager.Current[ResourceStrings.Session_CurrentItem] = HttpPageHelper.CurrentItem;
            }
            InitializeSession();
            InitializeSecurityContextManagerValues();

            //SecurityContextManager.Current.CurrentManagedApplication = new ApplicationServices().GetByID(Convert.ToInt16(ConfigurationManager.AppSettings["APPLICATIONID"]));
        }

        public void InitializeSession()
        {
            //if (SessionManager.Current == null)
            //{
            //    SessionManager.Current = new WebSessionProvider();
            //}
            //if (SecurityContextManager.Current == null)
            //{
            //    SecurityContextManager.Current = new WebSecurityContext();
            //}
        }

        public void InitializeSecurityContextManagerValues()
        {
            if (HttpPageHelper.CurrentProfile != null)
            {
                if (SecurityContextManager.Current != null)
                {
                    //SecurityContextManager.Current.CurrentProfile = HttpPageHelper.CurrentProfile;
                }
            }
        }
    }
}
