using BuckarooSdk.Transaction;


namespace BuckarooSdk.Services.SepaDirectDebit
{
    public class SepaDirectDebitTransaction
    {
        /// <summary>
		/// The configured transaction
		/// </summary>
        private ConfiguredTransaction ConfiguredTransaction { get; set; }

        internal SepaDirectDebitTransaction(ConfiguredTransaction configuredTransaction)
        {
            this.ConfiguredTransaction = configuredTransaction;
        }

        /// <summary>
        /// The pay recurrent function creates a configured transaction with an SepaDirectDebitPayRecurrentRequest, 
        /// that is ready to be executed.
        /// </summary>
        /// <param name="request">An SepaDirectDebitPayRecurrentRequest</param>
        /// <returns></returns>
        public ConfiguredServiceTransaction PayRecurrent(SepaDirectDebitPayRecurrentRequest request)
        {
            var parameters = ServiceHelper.CreateServiceParameters(request);
            var configuredServiceTransaction = new ConfiguredServiceTransaction(this.ConfiguredTransaction.BaseTransaction);
            configuredServiceTransaction.BaseTransaction.AddService("SepaDirectDebit", parameters, "payrecurrent", "2");

            return configuredServiceTransaction;
        }
    }
}
