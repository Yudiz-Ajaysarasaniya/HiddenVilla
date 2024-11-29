using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Entities.Base
{
    public class BaseIdEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
