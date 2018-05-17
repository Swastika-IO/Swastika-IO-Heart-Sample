using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using Swastika.Domain.Core.ViewModels;
using Swastika.Domain.Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoSwastikaHeart.Models;
using Microsoft.Data.OData.Query;
using System.ComponentModel.DataAnnotations;

namespace DemoSwastikaHeart.ViewModels
{
    public class PostViewModel : ViewModelBase<DemoContext, Post, PostViewModel>
    {
        #region Properties

        #region Model

        [JsonProperty("id")]
        public string Id { get; set; }

        [Required(ErrorMessage ="Nhập Tiêu đề")]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("createdDateUTC")]
        public DateTime CreatedDateUTC { get; set; }

        #endregion

        #region For View

        //Add properties need for view or convert from model to view

        [JsonProperty("createdDateLocal")]
        public DateTime CreatedDateLocal { get { return CreatedDateUTC.ToLocalTime(); } }

        [JsonProperty("comments")]
        public PaginationModel<CommentViewModel> Comments { get; set; }

        #endregion

        #endregion

        #region Contrutors

        public PostViewModel()
        {
        }

        public PostViewModel(Post model, DemoContext _context = null, IDbContextTransaction _transaction = null) : base(model, _context, _transaction)
        {
        }

        public PostViewModel(Post model, bool isLazyLoad, DemoContext _context = null, IDbContextTransaction _transaction = null) : base(model, isLazyLoad, _context, _transaction)
        {
        }

        #endregion

        #region Overrides

        public override Post ParseModel(DemoContext _context = null, IDbContextTransaction _transaction = null)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
                CreatedDateUTC = DateTime.UtcNow;
            }

            return base.ParseModel(_context, _transaction);
        }

        public override PostViewModel ParseView(bool isExpand = true, DemoContext _context = null, IDbContextTransaction _transaction = null)
        {
            
            var view = base.ParseView(isExpand, _context, _transaction);
            var getComments = CommentViewModel.Repository.GetModelListBy(
                    c => c.PostId == Id, // Conditions
                    "CreatedDate", OrderByDirection.Descending, // Order By
                    pageSize: 5, pageIndex: 0, // Pagination
                    _context: _context, _transaction: _transaction // Transaction
                    );
            if (getComments.IsSucceed)
            {
                Comments = getComments.Data;
            }
            return view;

        }

        public override async Task<RepositoryResponse<bool>> RemoveRelatedModelsAsync(PostViewModel view, DemoContext _context = null, IDbContextTransaction _transaction = null)
        {
            var result = new RepositoryResponse<bool>();
            //Remove Related Comments
            if (view.Comments.TotalItems>0)
            {
                result = await CommentViewModel.Repository.RemoveListModelAsync(c => c.PostId == view.Id, _context, _transaction);
            }
            return result;
        }
        #endregion
    }
}
