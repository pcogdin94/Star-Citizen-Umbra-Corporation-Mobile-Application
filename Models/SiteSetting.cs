using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace UmbraCorpApp.Models
{

    [Table("site_settings")]

    public class SiteSetting : BaseModel
    {
        [PrimaryKey("id", false)]
        public string Id { get; set; }

        [Column("value")]
        public string Value { get; set; }
    }
}
