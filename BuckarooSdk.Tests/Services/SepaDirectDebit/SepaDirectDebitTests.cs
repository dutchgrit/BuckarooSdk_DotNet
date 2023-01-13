using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.Threading.Tasks;
using BuckarooSdk.DataTypes.RequestBases;
using BuckarooSdk.Logging;
using BuckarooSdk.Services.SepaDirectDebit;
using BuckarooSdk.Tests.Constants;

namespace BuckarooSdk.Tests.Services.SepaDirectDebit
{
	[TestClass]
	public class SepaDirectDebitTests
	{
		private SdkClient _sdkClient;

		[TestInitialize]
		public void Setup()
		{
			this._sdkClient = new SdkClient(Constants.TestSettings.Logger);
		}

		[TestMethod]
        public async Task PayRecurrentTest()
        {
            var recurrentPayment = this._sdkClient.CreateRequest(new StandardLogger())
                                       .Authenticate(Constants.TestSettings.WebsiteKey, Constants.TestSettings.SecretKey, TestSettings.IsLive, new CultureInfo("nl-NL")).TransactionRequest()
                                       .SetBasicFields(new TransactionBase
                                                       {
                                                           Currency = "EUR",
                                                           AmountDebit = 0.03m,
                                                           Invoice = $"SDK_TEST_{DateTime.Now.Ticks}",
                                                           Description = "SIMPLESEPADIRECTDEBIT_PAY_RECURRENT_SDK_UNITTEST",
														   OriginalTransactionKey = "70217564D5F94EE090091DF923030257"
                                                       })
                                       .SepaDirectDebit()
                                       .PayRecurrent(new SepaDirectDebitPayRecurrentRequest
                                                     {
														 CollectDate = DateTime.Now.ToString("yyyy-MM-dd")
                                                     });

            var paymentResponse = await recurrentPayment.ExecuteAsync();

            Console.WriteLine(paymentResponse.BuckarooSdkLogger.GetFullLog());
        }
	}
}
