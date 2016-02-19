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
        [Display(Name = "Short description")]
        public string ShortDescription { get; set; }
        [Required]
        [AllowHtml]
        [Display(Name = "Body")]
        public string Body { get; set; }
        public bool Published { get; set; }
        [Display(Name = "Like")]
        [DefaultValue(0)]
        public int NetLikeCount { get; set; }

        [Display(Name = "Date posted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostedOn { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Image Image { get; set; }
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


        [Required]
        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [StringLength(2000, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
        [Display(Name = "ShortDescription")]
        public string ShortDescription { get; set; }
        [Required]
        [AllowHtml]
        [Display(Name = "Body")]
        public string Body { get; set; }

        [Display(Name = "Опубликовать?")]
        public bool Published { get; set; }

        [Display(Name = "Date posted")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PostedOn { get; set; }
        public Image Image { get; set; }
    }

    public class EditPostViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Category")]
        public int CategoryID { get; set; }
        public Category Category { get; set; }

        [StringLength(100, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(2000, ErrorMessage = "Значение {0} должно содержать символов не менее: {2}.", MinimumLength = 6)]
        [Display(Name = "ShortDescription")]
        public string ShortDescription { get; set; }

        [AllowHtml]
        [Display(Name = "Body")]
        public string Body { get; set; }
        [Display(Name = "Опубликовать?")]
        public bool Published { get; set; }
        public Image Image { get; set; }
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

    public class AddCommentViewModel
    {
        public int PostId { get; set; }
        public Post post { get; set; }

        public DateTime DateTime { get; set; }

        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
        [Required]
        public string Body { get; set; }
    }

    public class Image
    {
        public int Id { get; set; }
        public string LinkImg { get; set; }
    }


    //public class ImageInPost
    //{
    //    public int IdImgPost { get; set; }
    //    public int PostId { get; set; }
    //    public Post post { get; set; }
    //    public int IdImg { get; set; }
    //    public ICollection<Image> Images { get; set; }

    //} 

}