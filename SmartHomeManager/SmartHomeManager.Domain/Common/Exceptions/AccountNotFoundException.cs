using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeManager.Domain.Common.Exceptions
{
    [Serializable]
    public class AccountNotFoundException : Exception
    {
        public AccountNotFoundException() : base("Account not found") { }

    }
}
