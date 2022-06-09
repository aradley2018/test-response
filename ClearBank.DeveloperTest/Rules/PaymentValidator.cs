using ClearBank.DeveloperTest.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClearBank.DeveloperTest.Rules
{
    public class PaymentValidator : IPaymentValidator
    {
        public bool Validate(MakePaymentRequest request, Account account)
        {
            if (account == null)
            {
                return false;
            }

            if (request.PaymentScheme == PaymentScheme.Bacs && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Bacs))
            {
                return new BacsPaymentValidator().Validate(request,account);
            }

            if (request.PaymentScheme == PaymentScheme.Chaps && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.Chaps))
            {
                return new ChapsPaymentValidator().Validate(request, account);
            }

            if (request.PaymentScheme == PaymentScheme.FasterPayments && account.AllowedPaymentSchemes.HasFlag(AllowedPaymentSchemes.FasterPayments))
            {
                return new FasterPaymentsPaymentValidator().Validate(request, account);
            }

            return false;
        }
    }
}
