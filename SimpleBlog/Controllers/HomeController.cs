using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.OData.Query;
using SimpleBlog.Models;
using SimpleBlog.ViewModels;

namespace SimpleBlog.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("{pageSize}/{pageIndex}")]
        public async Task<IActionResult> Index(int? pageSize = 10, int? pageIndex = 0)
        {
            var posts = await PostViewModel.Repository.GetModelListAsync("CreatedDateUTC", OrderByDirection.Descending, pageSize, pageIndex);
            return View(posts.Data);
        }

        // GET: Posts/Details/5
        [Route("Details/{id}")]
        public async Task<IActionResult> Details(string id)
        {
            var getPost = await PostViewModel.Repository.GetSingleModelAsync(m => m.Id == id);
            if (!getPost.IsSucceed)
            {
                return NotFound();
            }
            return View(getPost.Data);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
