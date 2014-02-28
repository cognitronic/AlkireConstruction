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
using RAM.Core.Domain.Blog;
using System.Configuration;
using RAM.Services.Messaging.Blog;
using RAM.Core;
using System.Text.RegularExpressions;

namespace RAM.Admin.Controllers.Controllers
{
    public class BlogController : BaseUserAccountController
    {
        private readonly IBlogService _blogService;
        private readonly IBlogCategoryService _categoryService;
        private readonly ITagService _tagService;
        public BlogController(ILocalAuthenticationService authenticationService,
            IUserService userService,
            IBlogService blogService,
            ITagService tagService,
            IBlogCategoryService categoryService,
            IExternalAuthenticationService externalAuthenticationService,
            IFormsAuthentication formsAuthentication,
            IActionArguments actionArguments)
            : base(authenticationService, userService, externalAuthenticationService, formsAuthentication, actionArguments)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;

        }

        public ActionResult Index()
        {
            HomeView view = new HomeView();
            view.BlogCategories = _categoryService.GetAll().Categories;
            view.Tags = _tagService.GetAll();
            view.NavView.SelectedMenuItem = "nav-blog";
            view.Blogs = _blogService.GetAll().BlogList;
            return View(view);

        }

        public ActionResult BlogList()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            view.Blogs = _blogService.GetAll().BlogList;

            return PartialView("_BlogList", view);

        }

        public ActionResult Post(int id = 0)
        {
            HomeView view = new HomeView();
            if (id != 0)
                view.SelectedBlog = _blogService.GetByID(id);
            else
                view.SelectedBlog = null;
            view.NavView.SelectedMenuItem = "nav-blog";
            view.BlogCategories = _categoryService.GetAll().Categories;
            //view.SelectedBlogTags = _blogService.g
            return View(view); 
        }

        public ActionResult SavePost(Blog blog, string tags)
        {
            var b = new Blog();
            var isNew = false;
            if (blog.ID == 0)
            {
                b.EnteredBy = SecurityContextManager.Current.CurrentUser.ID;
                b.DatePosted = DateTime.Now;
                isNew = true;
            }
            else
            {
                b = _blogService.GetByID(blog.ID);
            }
            b.BlogCategoryID = blog.BlogCategoryID;
            b.IsActive = blog.IsActive;
            b.Post = blog.Post;
            if (blog.Post.Length > 300)
            {
                b.PostPreview = Regex.Replace(blog.Post.Substring(0, 297), @"<[^>]*>", String.Empty) + "...";
            }
            else
            {
                b.PostPreview = Regex.Replace(blog.Post, @"<[^>]*>", String.Empty); 
            }
            b.SEODescription = blog.SEODescription;
            b.SEOKeywords = blog.SEOKeywords;
            b.Title = blog.Title;
            _blogService.SavePost(b);
            foreach (var bt in b.Tags)
            {
                _blogService.DeleteBlogTag(bt);
            }
            foreach (var t in tags.Split(','))
            {
                var tag = _tagService.GetByName(t);
                var blogtag = new BlogTag();
                var newtag = new Tag();
                if (tag == null)
                {
                    newtag.Name = t;
                    _tagService.SaveTag(newtag);
                }
                else 
                {
                    newtag = tag;
                }
                blogtag.BlogID = blog.ID;
                blogtag.TagID = newtag.ID;
                _blogService.SaveBlogTag(blogtag);

            }
            return Json(new
            {
                Message = "Blog saved!",
                Status = "success",
                BlogID = b.ID,
                IsNew = isNew,
                ReturnUrl = "/Blog"
            });
        }

        public ActionResult SavePostImage()
        {
            var post = new Blog();
            if (Request.Form.Count > 0)
            {
                if (!string.IsNullOrEmpty(Request.Form["blogID"]))
                {
                    post = _blogService.GetByID(Convert.ToInt16(Request.Form["blogID"]));
                }
            }
            foreach (string fileName in Request.Files)
            {
                try
                {
                    var file = Request.Files[fileName];
                    post.ImagePath = ConfigurationSettings.AppSettings["BlogImageURL"] + file.FileName;
                    file.SaveAs(ConfigurationSettings.AppSettings["BlogImageDir"] + file.FileName);
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
                _blogService.SavePost(post);
            }
            catch (Exception exc)
            {
                return Json(new
                {
                    Message = "Blog post failed to save with following error: " + exc.Message,
                    Status = "failed"
                });
            }

            return Json(new
            {
                Message = "Blog Image saved!",
                Status = "success",
                ReturnUrl = "/Blog/Post/" + post.ID.ToString()
            });
        }

        public ActionResult GetByID(int id)
        {
            if (id > 0)
            {
                return Json(new
                {
                    Message = "Blog retreived!",
                    Status = "success",
                    BlogRef = _blogService.GetByID(id),
                    ReturnUrl = "/Blog"
                });
            }
            return Json(new
            {
                Message = "Blog could not be found!",
                Status = "failed",
                ReturnUrl = "/Blog"
            });
        }


        public ActionResult TagList()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            view.Tags = _tagService.GetAll();

            return PartialView("_BlogTags", view);

        }

        public ActionResult GetTagsForAutoSelect(string query) 
        {
            return Json(new
            {
                Tags = _tagService.GetForAutoComplete(query)
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTagByID(int id)
        {
            if (id > 0)
            {
                return Json(new
                {
                    Message = "Tag retreived!",
                    Status = "success",
                    TagRef = _tagService.GetByID(id),
                    ReturnUrl = "/Blog"
                });
            }
            return Json(new
            {
                Message = "Tag could not be found!",
                Status = "failed",
                ReturnUrl = "/Blog"
            });
        }

        public ActionResult SaveTag(Tag tag)
        {
            var t = new Tag();
            if (tag != null)
            {
                if (tag.ID > 0)
                {
                    t = _tagService.GetByID(tag.ID);
                }
                t.Name = tag.Name;
                _tagService.SaveTag(t);
            }

            return Json(new
            {
                Message = "Tag saved!",
                Status = "success",
                ReturnUrl = "/Blog"
            });
        }

        public ActionResult DeleteTag(int id)
        {
            if (id > 0)
            {
                try
                {                    
                    _tagService.DeleteTag(_tagService.GetByID(id));
                    return Json(new
                    {
                        Message = "Tag deleted!",
                        Status = "success",
                        ReturnUrl = "/Blog"
                    });
                }
                catch (Exception exc)
                {
                    return Json(new
                    {
                        Message = "Tag failed to delete!  You must remove all blog posts with this tag before it can be deleted.",
                        Status = "failed",
                        ReturnUrl = "/Blog"
                    });
                }
            }
            return Json(new
            {
                Message = "Tag failed to delete!  Tag was null.",
                Status = "failed",
                ReturnUrl = "/Blog"
            });
        }


        public ActionResult CategoryList()
        {
            HomeView view = new HomeView();
            view.NavView.SelectedMenuItem = "nav-blog";
            view.BlogCategories = _categoryService.GetAll().Categories;

            return PartialView("_BlogCategories", view);

        }

        public ActionResult GetCategoryByID(int id)
        {
            if (id > 0)
            {
                var cat = _categoryService.GetByID(id);
                return Json(new
                {
                    Message = "Category retreived!",
                    Status = "success",
                    CategoryRef = cat,
                    ReturnUrl = "/Blog"
                });
            }
            return Json(new
            {
                Message = "Category could not be found!",
                Status = "failed",
                ReturnUrl = "/Blog"
            });
        }

        public ActionResult SaveCategory(BlogCategory category)
        {
            var c = new BlogCategory();
            if (category != null)
            {
                if (category.ID > 0)
                {
                    c = (BlogCategory)_categoryService.GetByID(category.ID);
                }
                c.Name = category.Name;
                _categoryService.Save(c);
            }

            return Json(new
            {
                Message = "Category saved!",
                Status = "success",
                ReturnUrl = "/Blog"
            });
        }

        public ActionResult DeleteCategory(int id)
        {
            if (id > 0)
            {
                try
                {
                    _categoryService.Delete((BlogCategory)_categoryService.GetByID(id));
                    return Json(new
                    {
                        Message = "Category deleted!",
                        Status = "success",
                        ReturnUrl = "/Blog"
                    });
                }
                catch (Exception exc)
                {
                    return Json(new
                    {
                        Message = "Category failed to delete!  You must remove all blog posts with this category before it can be deleted.",
                        Status = "failed",
                        ReturnUrl = "/Blog"
                    });
                }
            }
            return Json(new
            {
                Message = "Category failed to delete!  Category was null.",
                Status = "failed",
                ReturnUrl = "/Blog"
            });
        }
    }
}
