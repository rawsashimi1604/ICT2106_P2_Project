using SmartHomeManager.Domain.AccountDomain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartHomeManager.Domain.NotificationDomain.Entities
{
    public enum NotificationResult
    {
        Error_AccountNotFound,
        Error_DBInsertFail,
        Success,
        Error_DBReadFail,
        Error_Other
    }
}
