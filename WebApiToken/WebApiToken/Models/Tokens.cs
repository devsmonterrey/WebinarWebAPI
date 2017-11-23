namespace WebApiToken.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Tokens
    {
        [Key]
        public int TokenId { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(250)]
        public string AuthToken { get; set; }

        public DateTime IssuedOn { get; set; }

        public DateTime ExpiresOn { get; set; }

        public virtual Users Users { get; set; }
    }
}
