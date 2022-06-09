using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Rules
{
    public class ChapsPaymentValidator
    {
        public bool Validate(MakePaymentRequest request, Account account)
        {
                if (account.Status != AccountStatus.Live)
                {
                    return false;
                }

                return true;
        }
    }
}
