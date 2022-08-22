using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    [Table("Tbl_User")]
    public class UserModel
    {
        [Key]
        public int userId { get; set; }
        public string userCode { get; set; }
        public string userName { get; set; }
        public bool delFlag { get; set; }
    }
}
