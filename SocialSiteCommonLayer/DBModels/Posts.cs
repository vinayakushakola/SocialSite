//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Posts Model (Database Table)
//

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialSiteCommonLayer.DBModels
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PostID { get; set; }

        [ForeignKey("Users")]
        public int UserID { get; set; }

        [Required]
        public string PostPath { get; set; }

        [Required]
        [DefaultValue("false")]
        public bool IsRemoved { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ModifiedDate { get; set; }
    }
}
