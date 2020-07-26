//
// Author  : Vinayak Ushakola
// Date    : 26/07/2020
// Purpose : It Contain Comments Model (Database Table)
//

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialSiteCommonLayer.DBModels
{
    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("Posts")]
        public int PostID { get; set; }

        [Required]
        public int CommentByUserID { get; set; }

        [Required]
        public string Comment { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ModifiedDate { get; set; }
    }
}
