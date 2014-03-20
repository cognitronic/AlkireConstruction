using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAM.Infrastructure.Authentication
{
    public class JanrainAuthenticationService : IExternalAuthenticationService
    {
        //public IUserAccount GetUserDetailsFrom(string token)
        //{
        //    var user = new IUserAccount();

        //    string parameters = string.Format("apiKey={0}&token={1}&format=xml", ApplicationSettingsFactory.GetApplicationSettings().JanrainApiKey, token);
        //    string response;
        //    using (var w = new WebClient())
        //    {
        //        response = w.UploadString(ApplicationSettingsFactory.GetApplicationSettings().JanrainURL, parameters);
        //    }
        //    var xmlResponse = XDocument.Parse(response);
        //    var userProfile = (from x in xmlResponse.Descendants("profile")
        //                       select new
        //                       {
        //                           id = x.Element("identifier").Value,
        //                           email = (string)x.Element("email") ?? "No Email"
        //                       }).SingleOrDefault();
        //    if (userProfile != null)
        //    {
        //        user.AuthenticationToken = userProfile.id;
        //        user.Email = userProfile.email;
        //        user.IsAuthenticated = true;
        //    }
        //    else
        //    {
        //        user.IsAuthenticated = false;
        //    }
        //    return user;
        //}
        #region IExternalAuthenticationService Members

        public IUserAccount GetUserDetailsFrom(string token)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
