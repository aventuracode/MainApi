using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainDatabase.dbo.Tables
{
    public class Source
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdSource { get; set; }

        public string IdSourceName { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Source> Sources { get; set; }
    }
}
