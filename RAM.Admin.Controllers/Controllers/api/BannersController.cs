using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading;
using System.Web.UI;
using RAM.Core.Domain.Banner;
using RAM.Services.Interfaces;
using RAM.Infrastructure.Authentication;
using System.Web.Mvc;
using System.Web;
using System.IO;
using RAM.Admin.Controllers.ActionArguments;

namespace RAM.Admin.Controllers.Controllers.api
{
    public class BannersController : ApiController
    {
        private readonly IBannerService _bannerService;

        public BannersController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IBannerService bannerService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
        {
            _bannerService = bannerService;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public HttpResponseMessage Upload()
        {
            // Get a reference to the file that our jQuery sent.  Even with multiple files, they will all be their own request and be the 0 index
            HttpPostedFile file = HttpContext.Current.Request.Files[0];

            // do something with the file in this space 
            // {....}
            // end of file doing

            // Now we need to wire up a response so that the calling script understands what happened
            HttpContext.Current.Response.ContentType = "text/plain";
            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var result = new { name = file.FileName };

            HttpContext.Current.Response.Write(serializer.Serialize(result));
            HttpContext.Current.Response.StatusCode = 200;

            // For compatibility with IE's "done" event we need to return a result as well as setting the context.response
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.HttpPost]
        public string DeleteBanner(int id)
        {
            if (id > 0)
            {
                var b = _bannerService.GetByID(id);
                _bannerService.DeleteBanner(b);
            }
            return "/Banners";
        }
    }
}
