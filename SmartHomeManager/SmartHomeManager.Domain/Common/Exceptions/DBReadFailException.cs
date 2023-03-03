using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.Common.Exceptions
{
    public class DBReadFailException : Exception
    {
        public DBReadFailException() : base("DB Read Fail") { }
    }
}
