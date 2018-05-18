using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using SimpleBlog.Data.Blog;
using Swastika.Domain.Data.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.ViewModels
{
    public class CommentViewModel : ViewModelBase<BlogContext, Comment, CommentViewModel>
    {
        #region Properties

        [JsonProperty("id")]
        public string Id { get; set; }


        [Required]
        [JsonProperty("postId")]
        public string PostId { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("createdDateUTC")]
        public DateTime CreatedDateUTC { get; set; }

        #region From Model

        #region For View

        //Add properties need for view or convert from model to view

        [JsonProperty("createdDateLocal")]
        public DateTime CreatedDateLocal { get { return CreatedDateUTC.ToLocalTime(); } }

        #endregion


        #endregion

        #endregion

        #region Contrutors

        public CommentViewModel()
        {
        }

        public CommentViewModel(Comment model, BlogContext _context = null, IDbContextTransaction _transaction = null) : base(model, _context, _transaction)
        {
        }

        public CommentViewModel(Comment model, bool isLazyLoad, BlogContext _context = null, IDbContextTransaction _transaction = null) : base(model, isLazyLoad, _context, _transaction)
        {
        }

        #endregion

        #region Overrides

        public override Comment ParseModel(BlogContext _context = null, IDbContextTransaction _transaction = null)
        {
            // if new comment
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
                CreatedDateUTC = DateTime.UtcNow;
            }
            if (string.IsNullOrEmpty(Author))
            {
                Author = "Annonymous";
            }

            return base.ParseModel(_context, _transaction);
        }

        #endregion
    }
}