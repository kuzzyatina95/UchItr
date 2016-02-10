using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Data.Entity;
using System.Web.Mvc;
using UchItr.Models;

namespace UchItr.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public async Task<ActionResult> Index()
        {
            var posts = db.Posts.Include(p => p.Category).Include(p => p.User);
            return View(await posts.ToListAsync());
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

        [Authorize]
        [HttpGet]
        public ActionResult CreatePost()
        {
            ViewBag.CategoryID = new SelectList(db.Categorys, "Id", "Name");
            //ViewBag.UserID = new SelectList(db.Users, "Id", "Email");
            ViewBag.UserID = User.Identity.GetUserId();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost([Bind(Include = "Id,UserID,CategoryID,Title,ShortDescription,Body,Published,NetLikeCount,PostedOn")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.UserID = User.Identity.GetUserId();
                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categorys, "Id", "Name", post.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", post.UserID);
            return View(post);
        }


    }
}