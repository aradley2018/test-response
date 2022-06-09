using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClearBank.DeveloperTest.Services;
using System;
using System.Collections.Generic;
using System.Text;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Tests.Mocks;

namespace ClearBank.DeveloperTest.Services.Tests
{
    [TestClass()]
    public class PaymentServiceTests
    {
        PaymentService service = new PaymentService(new AccountStoreFactoryMock(), new PaymentConfigMock());
        MakePaymentRequest request = new MakePaymentRequest() 
            {
                DebtorAccountNumber = "AllSchemesLive" //Balance = 100
            };

        [TestMethod()]
        public void givenAccountUnspecified_whenBacs_thenFailure()
        {
            request.PaymentScheme = PaymentScheme.Bacs;
            request.DebtorAccountNumber = "NA";
            var result = service.MakePayment(request);
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void givenAccountFpOnly_whenBacs_thenFailure()
        {
            request.PaymentScheme = PaymentScheme.Bacs;
            request.DebtorAccountNumber = "FP";

            var result = service.MakePayment(request);
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void givenAccountWithFunds_whenFasterPayment_thenSuccess()
        {
            request.PaymentScheme = PaymentScheme.FasterPayments;
            request.Amount = 50;
            
            var result = service.MakePayment(request);
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void givenAccountWithoutFunds_whenFasterPayment_thenFailure()
        {
            request.PaymentScheme = PaymentScheme.FasterPayments;
            request.Amount = 101;

            var result = service.MakePayment(request);
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void givenAccountLive_whenChaps_thenSuccess()
        {
            request.PaymentScheme = PaymentScheme.Chaps;
            
            var result = service.MakePayment(request);
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void givenAccountNotLive_whenChaps_thenFailure()
        {
            request.PaymentScheme = PaymentScheme.Chaps;
            request.DebtorAccountNumber = "ChapsNotLive";

            var result = service.MakePayment(request);
            Assert.IsFalse(result.Success);
        }
    }
}