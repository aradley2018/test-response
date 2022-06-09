using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Tests.Mocks
{
    class AccountStoreMock : IAccountStore
    {
        Account IAccountStore.GetAccount(string accountNumber)
        {
            if (accountNumber == "AllSchemesLive")
            {
                var acc = new Account
                {
                    AccountNumber = "AllSchemesLive",
                    AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments | AllowedPaymentSchemes.Bacs | AllowedPaymentSchemes.Chaps,
                    Balance = 100,
                    Status = AccountStatus.Live
                };
                return acc;
            }

            if (accountNumber == "ChapsNotLive")
            {
                var acc = new Account
                {
                    AccountNumber = "ChapsNotLive",
                    AllowedPaymentSchemes = AllowedPaymentSchemes.Chaps,
                    Status = AccountStatus.Disabled,
                };
                return acc;
            }

            if (accountNumber == "FP")
            {
                var acc = new Account
                {
                    AccountNumber = "FP",
                    AllowedPaymentSchemes = AllowedPaymentSchemes.FasterPayments,
                    Balance = 100
                };
                return acc;
            }

            return null;
        }

        void IAccountStore.UpdateAccount(Account account)
        {
        }
    }

    class AccountStoreFactoryMock : IAccountStoreFactory
    {
        IAccountStore IAccountStoreFactory.GetAccountStore(string key)
        {
            return new AccountStoreMock();
        }
    }
}
