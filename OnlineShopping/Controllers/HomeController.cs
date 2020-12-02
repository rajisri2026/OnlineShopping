﻿using OnlineShopping.ServiceLayer;
using OnlineShopping.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShopping.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        CategoryServices categoryServices = new CategoryServices();
        public ActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.FeaturedCategories = categoryServices.GetFeaturedCategories();
            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}