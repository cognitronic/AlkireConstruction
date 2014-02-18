using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RAM.Admin.Controllers.ActionArguments
{
    public class HttpRequestActionArguments : IActionArguments
    {

        #region IActionArguments Members

        public string GetValueForArgument(ActionArgumentKey key)
        {
            return HttpContext.Current.Request.QueryString[key.ToString()];
        }

        #endregion
    }
}
