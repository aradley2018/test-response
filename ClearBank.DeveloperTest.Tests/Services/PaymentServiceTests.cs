using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClearBank.DeveloperTest.Services;
using System;
using System.Collections.Generic;
using System.Text;
using ClearBank.DeveloperTest.Rules;
using ClearBank.DeveloperTest.Types;
using ClearBank.DeveloperTest.Tests.Mocks;

namespace ClearBank.DeveloperTest.Services.Tests
{
    [TestClass()]
    public class PaymentServiceTests
    {
        readonly PaymentService _service = new PaymentService(new AccountStoreFactoryMock(), new PaymentValidator(), new PaymentConfigMock());

        readonly MakePaymentRequest _request = new MakePaymentRequest() 
            {
                DebtorAccountNumber = "AllSchemesLive" //Balance = 100
            };

        [TestMethod()]
        public void givenAccountUnspecified_whenBacs_thenFailure()
        {
            _request.PaymentScheme = PaymentScheme.Bacs;
            _request.DebtorAccountNumber = "NA";
            var result = _service.MakePayment(_request);
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void givenAccountFpOnly_whenBacs_thenFailure()
        {
            _request.PaymentScheme = PaymentScheme.Bacs;
            _request.DebtorAccountNumber = "FP";

            var result = _service.MakePayment(_request);
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void givenAccountWithFunds_whenFasterPayment_thenSuccess()
        {
            _request.PaymentScheme = PaymentScheme.FasterPayments;
            _request.Amount = 50;
            
            var result = _service.MakePayment(_request);
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void givenAccountWithoutFunds_whenFasterPayment_thenFailure()
        {
            _request.PaymentScheme = PaymentScheme.FasterPayments;
            _request.Amount = 101;

            var result = _service.MakePayment(_request);
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void givenAccountLive_whenChaps_thenSuccess()
        {
            _request.PaymentScheme = PaymentScheme.Chaps;
            
            var result = _service.MakePayment(_request);
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void givenAccountNotLive_whenChaps_thenFailure()
        {
            _request.PaymentScheme = PaymentScheme.Chaps;
            _request.DebtorAccountNumber = "ChapsNotLive";

            var result = _service.MakePayment(_request);
            Assert.IsFalse(result.Success);
        }

        // todo
        // but should have added these tests with the static method validation refactoring
        [TestMethod()]
        public void givenAccountLive_whenChaps_whenValidationSuccessfull_thenAccountIsSaved()
        {
            //mock validation to return true/false
            //BDD-style tests to check expected methods are called on the store mock 
        }
    }
}