using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace UmbraCorpApp.Models
{

    [Table("member_requests")]

    public class MemberRequest : BaseModel
    {

        [PrimaryKey("id", false)]
        public string Id { get; set; }

        [Column("user_id")]
        public string UserId { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("rsi_handle")]
        public string RsiHandle { get; set; }

        [Column("discord")]
        public string Discord { get; set; }

        [Column("division")]
        public string Division { get; set; }

        [Column("status")]
        public string Status { get; set; }
    }
}
