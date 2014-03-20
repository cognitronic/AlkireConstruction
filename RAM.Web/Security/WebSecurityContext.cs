using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core;
using RAM.Core.Domain.User;
using RAM.Infrastructure.Session;
using RAM.Infrastructure.Domain;
using StructureMap;
using System.Web;

namespace RAM.Web.Security
{
    [Serializable]
    public class WebSecurityContext : IRAMSecurityContext
    {
        #region ISecurityContext Members

        public bool IsAuthenticated
        {
            get
            {
                if (SessionManager.Current["SESSION_ISAUTHENTICATED"] != null)
                {
                    return (bool)SessionManager.Current["SESSION_ISAUTHENTICATED"];
                }
                return false;
            }
            set
            {
                SessionManager.Current["SESSION_ISAUTHENTICATED"] = value;
            }
        }

        public IBaseUser CurrentUser
        {
            get
            {
                if (SessionManager.Current["SESSION_CURRENTUSER"] != null)
                {
                    return (IBaseUser)SessionManager.Current["SESSION_CURRENTUSER"];
                }
                return null;
            }
            set
            {
                SessionManager.Current["SESSION_CURRENTUSER"] = value;
            }
        }

        public string BaseURL
        {
            get
            {
                if (SessionManager.Current["SESSION_CURRENTBASEURL"] != null)
                {
                    return SessionManager.Current["SESSION_CURRENTBASEURL"].ToString();
                }
                else
                {
                    SessionManager.Current["SESSION_CURRENTBASEURL"] = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                    return SessionManager.Current["SESSION_CURRENTBASEURL"].ToString();
                }
            }
            set
            {
                SessionManager.Current["SESSION_CURRENTBASEURL"] = value;
            }
        }

        public int CurrentAccessLevel
        {
            get
            {
                if (SessionManager.Current[ResourceStrings.Session_CurrentAccessLevel] != null)
                {
                    return (int)SessionManager.Current[ResourceStrings.Session_CurrentAccessLevel];
                }
                return 0;
            }
            set
            {
                SessionManager.Current[ResourceStrings.Session_CurrentAccessLevel] = value;
            }
        }

        #endregion

        #region ISecurityContext Members


        public ISystemObject CurrentSystemObject
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
