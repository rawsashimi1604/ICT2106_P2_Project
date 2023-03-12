using System;
namespace SmartHomeManager.Domain.Common.Exceptions
{
    [Serializable]
    public class InvalidDateInputException:Exception
	{
		public InvalidDateInputException(): base("Month or Year is invalid") { }
       
	}
}
