using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StructureMap;
using System.Web.Mvc;

namespace RAM.Admin.MVC.App_Start
{
    public class GenericModelBinder : DefaultModelBinder
    {
        protected override object CreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return ObjectFactory.GetInstance(modelType);
        }
    }
}