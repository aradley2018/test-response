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

            if (!account.AllowedPaymentSchemes.HasFlag(request.PaymentScheme))
            {
                return false;
            }

            return request.PaymentScheme switch
            {
                PaymentScheme.Bacs => new BacsPaymentValidator().Validate(request, account),
                PaymentScheme.Chaps => new ChapsPaymentValidator().Validate(request, account),
                PaymentScheme.FasterPayments => new FasterPaymentsPaymentValidator().Validate(request, account),
                _ => throw new NotImplementedException("Unknown request PaymentScheme " + request.PaymentScheme)
            };
        }
    }
}
