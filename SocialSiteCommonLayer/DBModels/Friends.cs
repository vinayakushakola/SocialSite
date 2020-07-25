using System;
//
// Author  : Vinayak Ushakola
// Date    : 25/07/2020
// Purpose : It Contain Friends Model (Database Table)
//

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialSiteCommonLayer.DBModels
{
    public class Friends
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserID { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int FriendID { get; set; }

        [Required]
        [DefaultValue("false")]
        public bool IsAccepted { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ModifiedDate { get; set; }
    }
}
