using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Rules
{
    public class FasterPaymentsPaymentValidator
    {
        public bool Validate(MakePaymentRequest request, Account account)
        {
            if (account.Balance < request.Amount)
            {
                return false;
            }

            return true;
        }
    }
}
