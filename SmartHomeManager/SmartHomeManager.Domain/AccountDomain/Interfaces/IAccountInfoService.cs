﻿using System;
using SmartHomeManager.Domain.AccountDomain.Entities;

namespace SmartHomeManager.Domain.AccountDomain.Interfaces
{
	public interface IAccountInfoService 
	{
        public Task<Account?> GetAccountByAccountId(Guid id);
        public Task<bool> CheckAccountExists(Guid id);
    }
}

