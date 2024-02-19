using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BuckarooSdk.DataTypes.RequestBases;
using BuckarooSdk.Logging;
using BuckarooSdk.Services.SepaDirectDebit;
using BuckarooSdk.Tests.Constants;
using BuckarooSdk.DataTypes.Push;

namespace BuckarooSdk.Tests.Services.SepaDirectDebit
{
	[TestClass]
	public class SepaDirectDebitTests
	{
		private SdkClient sdkClient;

		[TestInitialize]
		public void Setup()
		{
			sdkClient = new SdkClient(TestSettings.Logger);
		}

		[TestMethod]
        public async Task PayRecurrentTest()
        {
            var recurrentPayment = sdkClient.CreateRequest(new StandardLogger())
                                            .Authenticate(TestSettings.WebsiteKey, TestSettings.SecretKey, TestSettings.IsLive, new CultureInfo("nl-NL"))
                                            .TransactionRequest()
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

        [TestMethod]
        public void PayRecurrentPushTest()
        {
            var pushHandler = sdkClient.GetPushHandler(TestSettings.SecretKey); // Retrieving the pushHandler from de SDK client.

            using (var reader = new StreamReader($"{ TestSettings.LogBasePath }SepaDirectDebitPayRecurrentPush.json"))
            {
                // JSON push as it is received by the client system.
                var jsonString = reader.ReadToEnd();
                var bodyAsBytes = Encoding.UTF8.GetBytes(jsonString); // DEZE IS BELANGRIJK: BERICHT AS BYTE[]
                var pushSignature = sdkClient.GetSignatureCalculationService().CalculateSignature(bodyAsBytes, HttpMethod.Post.ToString(),
                                                                                                        string.Empty, string.Empty,
                                                                                                        string.Empty, TestSettings.WebsiteKey, TestSettings.SecretKey);     


                var push = pushHandler.DeserializePush(bodyAsBytes, string.Empty, $"hmac {pushSignature}") as Push;

                var responseData = push.GetActionResponse<SepaDirectDebitPayRecurrentPush>();

                // 5 example values that can be retrieved from the push. The push contains many more though
                var transactionKey = push.Key;
                var transactionStatus = push.Status;

                var iban = responseData.CustomerIban;
                var bic = responseData.CustomerBic;
                var mandateReference = responseData.MandateReference;

                // The following KeyValuePair can be used to update your transaction
                var newTransactionStatus = new KeyValuePair<string, int>(transactionKey, transactionStatus.Code.Code);
            }
        }
	}
}
