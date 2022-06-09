using ClearBank.DeveloperTest.Data;
using ClearBank.DeveloperTest.Types;
using System.Configuration;

namespace ClearBank.DeveloperTest.Services
{
    public class PaymentConfig : IPaymentConfig
    {
        public string GetAccountStoreType()
        {
            return ConfigurationManager.AppSettings["DataStoreType"];
        }
    
    }
}
