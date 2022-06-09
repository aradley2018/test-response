using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Rules;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IAccountStoreFactory _accountStoreFactory;
        private readonly IPaymentValidator _paymentValidator;
        private readonly IPaymentConfig _paymentConfig;

        public PaymentService(IAccountStoreFactory accountStoreFactory, IPaymentValidator paymentValidator, IPaymentConfig paymentConfig)
        {
            _accountStoreFactory = accountStoreFactory;
            _paymentValidator = paymentValidator;
            _paymentConfig = paymentConfig;
        }

        public MakePaymentResult MakePayment(MakePaymentRequest request)
        {
            var store = _accountStoreFactory.GetAccountStore(_paymentConfig.GetAccountStoreType());

            var account = store.GetAccount(request.DebtorAccountNumber);

            var result = new MakePaymentResult
            {
                Success = _paymentValidator.Validate(request, account)
            };

            if (result.Success)
            {
                account.Balance -= request.Amount;
                store.UpdateAccount(account);
            }

            return result;
        }

    }
}
