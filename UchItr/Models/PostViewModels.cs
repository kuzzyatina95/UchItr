using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UchItr.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Display(Name = "User")]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }


        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "ShortDescription")]
        public string ShortDescription { get; set; }
        [Required]
        [AllowHtml]
        [Display(Name = "Body")]
        public string Body { get; set; }
        public bool Published { get; set; }
        [DefaultValue(0)]
        public int NetLikeCount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostedOn { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }

    public class AddPostViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User")]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "ShortDescription")]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [Display(Name = "Body")]
        public string Body { get; set; }

        public int NetLikeCount { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostedOn { get; set; }
    }

    public class Category
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

    }

    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }
        public Post post { get; set; }

        public DateTime DateTime { get; set; }

        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        public string Body { get; set; }
        [DefaultValue(0)]
        public int NetLikeCount { get; set; }
        [DefaultValue(false)]
        public bool Deleted { get; set; }
    }


}