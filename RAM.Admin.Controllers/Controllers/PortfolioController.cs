using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Admin.Controllers.ViewModels;
using RAM.Core.Domain.User;
using RAM.Admin.Controllers.ActionArguments;
using RAM.Infrastructure.Authentication;
using RAM.Services.Interfaces;
using System.Web.Mvc;
using System.Web;
using System.IO;
using RAM.Core.Domain.Project;
using System.Configuration;
using RAM.Core;

namespace RAM.Admin.Controllers.Controllers
{
    public class PortfolioController : BaseUserAccountController
    {
        private readonly IProjectService _projectService;
        public PortfolioController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IProjectService projectService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, formsAuthentication, actionArguments)
        {
            _projectService = projectService;
        }

        public ActionResult Index()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-portfolio";
            view.Projects = _projectService.GetAll().ProjectList;
            return View(view);

        }

        public ActionResult PortfolioList()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-portfolio";
            view.Projects = _projectService.GetAll().ProjectList;
            return PartialView("_PortfolioList", view);
        }

        public ActionResult GetPortfolioImages(string id)
        {
            var imgpaths = "";
            var portfolio = new List<IProjectImage>();

            if (!string.IsNullOrEmpty(id))
            {
                portfolio = _projectService.GetImagesByProjectID(Convert.ToInt16(id)).ToList();
                foreach (var p in portfolio)
                {
                    imgpaths += p.ImagePath + ",";
                }
            }


            return Json(new
            {
                Message = "Banner failed to save with following error: ",
                Paths = imgpaths.Remove(imgpaths.Length - 1, 1),
                Images = portfolio
            });
        }

        public ActionResult SavePortfolio()
        {
            var portfolio = new Project();
            if (Request.Form.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.Form["portfolioID"]))
                {
                    portfolio = _projectService.GetByID(Convert.ToInt16(Request.Form["portfolioID"]));
                }
                else
                {

                    portfolio.DateCreated = DateTime.Now;
                    portfolio.EnteredBy = SecurityContextManager.Current.CurrentUser.ID;
                    portfolio.DefaultImagePath = "";
                }
                portfolio.Title = Request.Form["title"];
                portfolio.Description = Request.Form["description"];
                portfolio.Category = Convert.ToInt16(Request.Form["category"]);
                portfolio.ProjectDate = DateTime.Parse(Request.Form["projectDate"]);
            }

            try
            {
                _projectService.SavePost(portfolio);
            }
            catch (Exception exc)
            {
                return Json(new
                {
                    Message = "Banner failed to save with following error: " + exc.Message,
                    Status = "failed"
                });
            }

            foreach (string fileName in Request.Files)
            {
                try
                {
                    var file = Request.Files[fileName];
                    portfolio.DefaultImagePath = ConfigurationSettings.AppSettings["PortfolioImageURL"] + file.FileName;
                    file.SaveAs(ConfigurationSettings.AppSettings["PortfolioImageDir"] + file.FileName);
                    
                    _projectService.SaveImage(
                        new ProjectImage(portfolio.ID,
                            ConfigurationSettings.AppSettings["PortfolioImageURL"] + file.FileName,
                            portfolio.Title
                        ));
                }
                catch (Exception fileException)
                {
                    return Json(new
                    {
                        Message = "File failed to save with following error: " + fileException.Message,
                        Status = "failed"
                    });
                }
            }
            try
            {
                _projectService.SavePost(portfolio);
            }
            catch (Exception exc)
            {
                return Json(new
                {
                    Message = "Portfolio failed to save with following error: " + exc.Message,
                    Status = "failed"
                });
            }

            return Json(new
            {
                Message = "Portfolio saved!",
                Status = "success",
                ReturnUrl = "/Portfolio"
            });

        }

        public ActionResult SavePortfolioObj(Project portfolio)
        {
            var p = new Project();
            if (portfolio != null)
            {
                p = _projectService.GetByID(portfolio.ID);
                p.Title = portfolio.Title;
                p.Description = portfolio.Description;
                p.Category = portfolio.Category;
                p.ProjectDate = portfolio.ProjectDate;
            }

            try
            {
                _projectService.SavePost(p);
            }
            catch (Exception exc)
            {
                return Json(new
                {
                    Message = "Portfolio failed to save with following error: " + exc.Message,
                    Status = "failed"
                });
            }
            return Json(new
            {
                Message = "Portfolio saved!",
                Status = "success",
                ReturnUrl = "/Portfolio"
            });

        }

        public ActionResult DeletePortfolio(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var p = _projectService.GetByID(Convert.ToInt16(id));
                foreach (var i in p.Images)
                {
                    _projectService.DeleteImage(i);
                }
                _projectService.DeletePost(p);
            }
            return Json(new
            {
                Message = "Banner saved!",
                Status = "success",
                ReturnUrl = "/Portfolio"
            });
        }

        public ActionResult DeletePortfolioImage(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var p = _projectService.GetImageByID(Convert.ToInt16(id));
                _projectService.DeleteImage((ProjectImage)p);
            }
            return Json(new
            {
                Message = "Project Image Deleted!",
                Status = "success",
                ReturnUrl = "/Portfolio"
            });
        }
    }
}
