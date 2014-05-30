﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Glass.Mapper.Sc;
using Glass.Mapper.Sc.Web.Mvc;
using Projects.Models.Glass.Common;

namespace Projects.Website.Controllers
{
    public abstract class BaseController : GlassController
    {
        protected readonly ISitecoreContext Context;

        protected BaseController(ISitecoreContext context)
        {
            Context = context;
        }

        ///// <summary>
        ///// Redirect to Knowledge Gateway Home Page
        ///// </summary>
        ///// <returns></returns>
        public ActionResult RedirectToHomePage()
        {
            PageBase homeItem = Context.GetHomeItem<PageBase>();
            string homePageUrl = homeItem.URL;
            return Redirect(homePageUrl);
        }        
	}
}