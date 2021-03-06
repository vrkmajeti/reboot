﻿#region

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using PagedList;
using Projects.Models;
using Projects.Models.Glass.Reboot.Items;
using Projects.Models.Glass.Reboot.RenderingParameters;
using Projects.Models.ViewModels;
using Projects.Reboot.Common;
using Projects.Reboot.Contracts;
using Microsoft.Web.Mvc;
#endregion

namespace Projects.Website.Controllers
{
    public class MovieController : BaseController
    {
        #region Readonly & Static Fields

        private readonly IMovieSearchService _movieSearchService;

        #endregion

        #region C'tors

        public MovieController(IMovieSearchService movieSearchService, ICommonTextService commonTextService) : base(commonTextService)
        {
            _movieSearchService = movieSearchService;
        }

        public MovieController() 
        {
            _movieSearchService = new ServiceFactory().GetService<IMovieSearchService>();
        }

        #endregion

        #region Instance Methods

        public ActionResult NowRunningMovies()
        {
            IEnumerable<Movie> nowRunningMovies = _movieSearchService.GetNowRunningMovies(6);
            ItemList list = new ItemList
            {
                ListName = TextService.GetTextFor("Now Running"),
                IconClassName = "fa-video-camera",
                Items = nowRunningMovies.Select(x => SitecoreContext.GetItem<Movie>(x.Id))
            };
            return View("RenderItemList", list);
        }

        public ActionResult ComingSoonMovies()
        {
            IEnumerable<Movie> comingSoonMovies = _movieSearchService.GetComingSoonMovies(6);
            ItemList list = new ItemList
            {
                ListName = TextService.GetTextFor("Coming Soon"),
                IconClassName = "fa-clock-o",
                Items = comingSoonMovies.Select(x => SitecoreContext.GetItem<Movie>(x.Id))
            };
            return View("RenderItemList", list);
        }

        public ActionResult Movies(SearchQuery query)
        {
            if(query.PageSize == 0) query.PageSize = 20;
            var parameters = GetRenderingParameters<SearchParameter>();
            int totalResultCount;
            IEnumerable<Movie> movies = _movieSearchService.GetMoviesByPopularity(query, parameters, out totalResultCount);


            var moviesAsIPagedList = new StaticPagedList<Movie>(movies, query.PageNumber == 0 ? query.PageNumber + 1 : query.PageNumber, query.PageSize, totalResultCount);

            return View(moviesAsIPagedList);
        }

        #endregion
    }
}