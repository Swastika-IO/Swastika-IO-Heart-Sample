using Microsoft.Data.OData.Query;
using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using SimpleBlog.Data.Blog;
using Swastika.Common.Helper;
using Swastika.Domain.Core.ViewModels;
using Swastika.Domain.Data.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SimpleBlog.ViewModels
{
    public class PostViewModel: ViewModelBase<BlogContext, Post, PostViewModel>
    {
        #region Properties

        #region Model

        [JsonProperty("id")]
        public string Id { get; set; }

        [Required(ErrorMessage = "Nhập Tiêu đề")]
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("seoName")]
        public string SeoName { get; private set; }

        [JsonProperty("excerpt")]
        public string Excerpt { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("author")]
        public string Author { get; set; }

        [JsonProperty("createdDateUTC")]
        public DateTime CreatedDateUTC { get; set; }

        #endregion

        #region View

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

        public PostViewModel(Post model, BlogContext _context = null, IDbContextTransaction _transaction = null) : base(model, _context, _transaction)
        {
        }

        public PostViewModel(Post model, bool isLazyLoad, BlogContext _context = null, IDbContextTransaction _transaction = null) : base(model, isLazyLoad, _context, _transaction)
        {
        }

        #endregion

        #region Overrides

        public override Post ParseModel(BlogContext _context = null, IDbContextTransaction _transaction = null)
        {
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
                CreatedDateUTC = DateTime.UtcNow;
            }
            GenerateSEO(_context, _transaction);
            return base.ParseModel(_context, _transaction);
        }

        public override PostViewModel ParseView(bool isExpand = true, BlogContext _context = null, IDbContextTransaction _transaction = null)
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

        public override async Task<RepositoryResponse<bool>> RemoveRelatedModelsAsync(PostViewModel view, BlogContext _context = null, IDbContextTransaction _transaction = null)
        {
            var result = new RepositoryResponse<bool>() { IsSucceed = true };
            //Remove Related Comments
            if (view.Comments.TotalItems > 0)
            {
                result = await CommentViewModel.Repository.RemoveListModelAsync(c => c.PostId == view.Id, _context, _transaction);
            }
            return result;
        }
        #endregion

        #region Expands

        private void GenerateSEO(BlogContext _context = null, IDbContextTransaction _transaction = null)
        {
            if (string.IsNullOrEmpty(this.SeoName))
            {
                this.SeoName = SEOHelper.GetSEOString(this.Title);
            }
            int i = 1;
            string name = SeoName;
            if (PostViewModel.Repository.CheckIsExists(a => a.SeoName == name && a.Id != Id, _context, _transaction))
            {
                name = SeoName + "_" + i;
            }
            SeoName = name;
        }

        #endregion
    }
}
