namespace BlogProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        public int ArticleId { get; set; }

        [StringLength(250)]
        public string Title { get; set; }
        
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool IsActive { get; set; }

        public int CategoryId { get; set; }

        public int? PictureId { get; set; }

        public virtual Category Category { get; set; }

        public virtual Picture Picture { get; set; }
    }
}
