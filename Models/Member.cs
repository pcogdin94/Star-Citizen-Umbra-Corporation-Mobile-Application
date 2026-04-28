using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace UmbraCorpApp.Models
{

    [Table("members")]
    public class Member : BaseModel
    {

        [PrimaryKey("id", false)]
        public string Id { get; set; }

        [Column("email")]
        public string Email { get; set; }
    }
}
