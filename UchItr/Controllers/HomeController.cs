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
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            if((currentUser.Name ==null)|| currentUser.Surname == null)
            {
                return RedirectToAction("AddDescription", "Manage");
            }
            ViewBag.CategoryID = new SelectList(db.Categorys, "Id", "Name");
            ViewBag.UserID = User.Identity.GetUserId();
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreatePost(AddPostViewModel newPost)
        {
            Post post = new Post();
            if (ModelState.IsValid)
            {
                post.UserID = User.Identity.GetUserId();
                post.CategoryID = newPost.CategoryID;
                post.Title = newPost.Title;
                post.ShortDescription = newPost.ShortDescription;
                post.Body = newPost.Body;
                post.Published = newPost.Published;
                post.PostedOn = newPost.PostedOn;

                db.Posts.Add(post);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categorys, "Id", "Name", post.CategoryID);
            ViewBag.UserID = new SelectList(db.Users, "Id", "Email", post.UserID);
            return View(post);
        }

        [HttpGet]
        public async Task<ActionResult> DetailsPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //var post1 = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments);
            //Post post = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments).FirstOrDefault(p => p.Id == id);
            //Post post = new Post();
            //var post1 = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments).Where(p => p.Id == id.Value);

            //foreach (Post p  in db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments))
            //{
            //    if (p.Id == id)
            //    {
            //        post = p;
            //        break;
            //    }
            //}
            //Post post = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments).Where(p => p.Id == id).Select(p=>p.Comments);
            //var post1 = from c in db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments)
            //            where c.Id == id
            //            select c;
            //Post post = (Post)post1;
            Post post = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments.Select(c => c.User)).FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

    }
}