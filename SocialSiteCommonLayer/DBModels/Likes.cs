//
// Author  : Vinayak Ushakola
// Date    : 22/07/2020
// Purpose : It Contain Likes Model (Database Table)
//

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialSiteCommonLayer.DBModels
{
    public class Likes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Posts")]
        public int PostID { get; set; }

        [Required]
        public int LikeByUserID { get; set; }

        [Required]
        [DefaultValue("false")]
        public bool IsLiked { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ModifiedDate { get; set; }
    }
}
