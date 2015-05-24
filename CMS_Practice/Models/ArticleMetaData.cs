using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMS_Practice.Models
{
    [System.ComponentModel.DataAnnotations.MetadataType(typeof (ArticleMetaData))]
    public partial class Article
    {
        public class ArticleMetaData
        {
            public System.Guid ID { get; set; }

            [Display(Name="主題")]
            public string Subject { get; set; }
            
            [Display(Name="簡介")]
            public string Summary { get; set; }
            
            [Display(Name="內容")]
            [UIHint("HTML")]
            [AllowHtml]
            public string ContentText { get; set; }

            [Display(Name="是否發佈")]
            public bool IsPublich { get; set; }

            [Display(Name="發佈日期")]
            [DataType(DataType.Date)]
            [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
            public System.DateTime PublishDate { get; set; }
            [Display(Name="瀏覽次數")]
            public int ViewCount { get; set; }
            [Display(Name="建立者")]
            public System.Guid CreateUser { get; set; }
            [Display(Name="建立時間")]
            public System.DateTime CreateDate { get; set; }
            [Display(Name="修改者")]
            public Nullable<System.Guid> UpdateUser { get; set; }
            [Display(Name="修改時間")]
            public System.DateTime UpdateDate { get; set; }
        }
    }
}