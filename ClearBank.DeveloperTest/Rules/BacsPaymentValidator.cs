using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Rules
{
    public class BacsPaymentValidator
    {
        public bool Validate(MakePaymentRequest request, Account account)
        {
                if (account == null)
                {
                    return false;
                }
                else if (!account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
                {
                    return false;
                }

                return true;
        }
    }
}
