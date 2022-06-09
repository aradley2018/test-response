using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Rules;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountStoreFactory _accountStoreFactory;
        private readonly IPaymentConfig _paymentConfig;

        public PaymentService(IAccountStoreFactory accountStoreFactory, IPaymentConfig paymentConfig)
        {
            _accountStoreFactory = accountStoreFactory;
            _paymentConfig = paymentConfig;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var store = _accountStoreFactory.GetAccountStore(_paymentConfig.GetAccountStoreType());

            var account = store.GetAccount(request.DebtorAccountNumber);

            var result = new MakePaymentResult
            {
                Success = PaymentValidator.Validate(request, account)
            };

            if (!result.Success) return result;
            account.Balance -= request.Amount;
            store.UpdateAccount(account);

            return result;
        }

    }
}
