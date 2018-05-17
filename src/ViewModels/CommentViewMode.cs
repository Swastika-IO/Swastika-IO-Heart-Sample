using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Swastika.Domain.Data.ViewModels;
using System;
using DemoSwastikaHeart.Models;
using System.ComponentModel.DataAnnotations;

namespace DemoSwastikaHeart.ViewModels
{
    public class CommentViewModel : ViewModelBase<DemoContext, Comment, CommentViewModel>
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

        public CommentViewModel(Comment model, DemoContext _context = null, IDbContextTransaction _transaction = null) : base(model, _context, _transaction)
        {
        }

        public CommentViewModel(Comment model, bool isLazyLoad, DemoContext _context = null, IDbContextTransaction _transaction = null) : base(model, isLazyLoad, _context, _transaction)
        {
        }

        #endregion

        #region Overrides

        public override Comment ParseModel(DemoContext _context = null, IDbContextTransaction _transaction = null)
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
