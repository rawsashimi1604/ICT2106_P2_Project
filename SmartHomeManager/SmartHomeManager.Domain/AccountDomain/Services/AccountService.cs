using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using SmartHomeManager.Domain.AccountDomain.DTOs;
using SmartHomeManager.Domain.AccountDomain.Entities;
using SmartHomeManager.Domain.AccountDomain.Interfaces;

namespace SmartHomeManager.Domain.AccountDomain.Services
{
	public class AccountService:IAccountService
	{
		private readonly IAccountRepository _accountRepository;

		public AccountService(IAccountRepository accountRepository)
		{
			_accountRepository = accountRepository;
		}
		public async Task<int> CreateAccount(AccountWebRequest accountWebRequest) 
		{
			bool isEmailUnique = await _accountRepository.IsEmailUnique(accountWebRequest.Email);

			if (!isEmailUnique)
			{
				return 3;
			}

			// Create new Account object and assign the web request variables to it, except for the password
            Account realAccount = new Account();
			realAccount.AccountId = Guid.NewGuid();
            realAccount.Address = accountWebRequest.Address;
            realAccount.Email = accountWebRequest.Email;
            realAccount.Timezone = accountWebRequest.Timezone;
            realAccount.Username = accountWebRequest.Username;

            //Hash the password of the user using the newly created Guid as the salt
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
				password: accountWebRequest.Password,
				salt: realAccount.AccountId.ToByteArray(),
				prf: KeyDerivationPrf.HMACSHA256,
				iterationCount: 100000,
				numBytesRequested: 256 / 8));

			realAccount.Password = hashedPassword;

			bool addAccountResponse = await _accountRepository.AddAsync(realAccount);
			if (addAccountResponse)
			{
				return await _accountRepository.SaveAsync();
			}

			else
			{
				// if account cannot be added
				return 2;
			}
		}

		public async Task<Account?> GetAccountByAccountId(Guid id)
		{
			Account? account = await _accountRepository.GetByIdAsync(id);

			if (account == null)
			{
				return null;
			}

			return account;
		}

		public async Task<IEnumerable<Account>> GetAccounts()
		{
			IEnumerable<Account> accounts = await _accountRepository.GetAllAsync();

			if (accounts == null)
			{
				return Enumerable.Empty<Account>();
			}

			return accounts;
		}
		
		public async Task<Guid?> VerifyLogin(LoginWebRequest login)
		{
			Account? account = await _accountRepository.GetAccountByEmailAsync(login.Email);
            //Hash the password of the user using the newly created Guid as the salt
            
            if (account != null)
			{
                string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
																password: login.Password,
																salt: account.AccountId.ToByteArray(),
																prf: KeyDerivationPrf.HMACSHA256,
																iterationCount: 100000,
																numBytesRequested: 256 / 8));

                login.Password = hashedPassword;

                if (account.Password == login.Password)
				{
					// account exists and password is correct
					//return 1;
					return account.AccountId;
				}
			}

            // account does not exist/account exists but password is wrong
            return null;
		}

        public async Task<bool> CheckAccountExists(Guid id)
        {
            Account? account = await _accountRepository.GetByIdAsync(id);
            if (account != null)
            {
                Console.WriteLine("account exists");
                return true;
            }
            Console.WriteLine("account noooo  exists");
            return false;
        }
    }
}
