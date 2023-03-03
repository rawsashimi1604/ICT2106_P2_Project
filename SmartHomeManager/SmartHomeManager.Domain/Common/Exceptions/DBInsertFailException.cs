using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.Common.Exceptions
{
    public class DBInsertFailException : Exception
    {
        public DBInsertFailException() : base("DB Insert Fail") { }
    }
}
