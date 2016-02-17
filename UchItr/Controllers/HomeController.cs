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
            var posts = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p=>p.Comments);
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
            Post post = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments.Select(c => c.User)).FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Comments(int postid, string userid, string bodyComment)
        {
            Comment comment = new Comment();
            Post post = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments.Select(c => c.User)).FirstOrDefault(p => p.Id == postid);
            if (ModelState.IsValid)
            {
                if (bodyComment.Trim() == "")
                {
                    return PartialView("Comments", post);
                }
                comment.UserID = User.Identity.GetUserId();
                comment.PostId = postid;
                comment.Body = bodyComment;
                comment.DateTime = DateTime.Now;

                db.Comments.Add(comment);
                db.SaveChanges();
                return PartialView("Comments",post);
            }
            //if (post.Comments.Count <= 0)
            //{
            //    return HttpNotFound();
            //}

            return PartialView();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult DeleteComments(int Id, int comment_Id, string comment_UserID)
        {
            Post post = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments.Select(c => c.User)).FirstOrDefault(p => p.Id == Id);
            if (ModelState.IsValid)
            {
                if (comment_UserID != User.Identity.GetUserId())
                {
                    return PartialView("Comments", post);
                }
                Comment comment = db.Comments.Find(comment_Id);
                db.Comments.Remove(comment);
                db.SaveChanges();
                post = db.Posts.Include(p => p.Category).Include(p => p.User).Include(p => p.Comments.Select(c => c.User)).FirstOrDefault(p => p.Id == Id);
                return PartialView("Comments", post);
            }
            //if (post.Comments.Count <= 0)
            //{
            //    return HttpNotFound();
            //}

            return PartialView();
        }

    }
}