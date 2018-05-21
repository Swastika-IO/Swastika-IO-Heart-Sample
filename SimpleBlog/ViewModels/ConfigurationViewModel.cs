using Microsoft.EntityFrameworkCore.Storage;
using Newtonsoft.Json;
using SimpleBlog.Data.Blog;
using Swastika.Domain.Data.ViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleBlog.ViewModels
{
    public class ConfigurationViewModel : ViewModelBase<BlogContext, Configuration, ConfigurationViewModel>
    {
        #region Properties

        [JsonProperty("id")]
        public string Id { get; set; }


        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("Type")]
        public ConfigurationType Type { get; set; }

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

        public ConfigurationViewModel()
        {
        }

        public ConfigurationViewModel(Configuration model, BlogContext _context = null, IDbContextTransaction _transaction = null) : base(model, _context, _transaction)
        {
        }

        public ConfigurationViewModel(Configuration model, bool isLazyLoad, BlogContext _context = null, IDbContextTransaction _transaction = null) : base(model, isLazyLoad, _context, _transaction)
        {
        }

        #endregion

        #region Overrides

        public override Configuration ParseModel(BlogContext _context = null, IDbContextTransaction _transaction = null)
        {
            // if new Configuration
            if (string.IsNullOrEmpty(Id))
            {
                Id = Guid.NewGuid().ToString();
                CreatedDateUTC = DateTime.UtcNow;
            }

            return base.ParseModel(_context, _transaction);
        }

        #endregion
    }

    public enum ConfigurationType
    {
        STRING,
        INT,
        HTML,
        MARKDOWN
    }
}