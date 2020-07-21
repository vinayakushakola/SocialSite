using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SocialSiteCommonLayer.DBModels
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(20)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string Password { get; set; }

        [Required]
        [DefaultValue("false")]
        public bool IsActive { get; set; }

        [Required]
        [StringLength(10)]
        public string UserRole { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Column(TypeName = "DateTime2")]
        public DateTime ModifiedDate { get; set; }
    }
}
