using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewApp.Models
{
    public class ChangePassword
    {
        public int UserTypeId { get; set; }
        public string salt { get; set; }
    }
}