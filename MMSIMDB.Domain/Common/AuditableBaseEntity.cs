using System;
using System.Collections.Generic;
using System.Text;

namespace MMSIMDB.Domain.Common
{
    public abstract class BaseTrackedEntity : BaseEntity
    {
        public DateTime CreateDate { get; set; }
        public string CreateUserID { get; set; }
        public DateTime? EditDate { get; set; }
        public string EditUserID { get; set; }
    }
}
