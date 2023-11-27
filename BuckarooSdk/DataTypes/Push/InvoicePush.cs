using System;

namespace BuckarooSdk.DataTypes.Push
{
    public class InvoicePush : BasePush
    {
        public string InvoiceKey { get; set; }
        public string InvoiceNumber { get; set; }
        public string DebtorCode { get; set; }
        public string DebtorGuid { get; set; }
        public string SchemeKey { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public int InvoiceStatusCode { get; set; }
        public string InvoicePayLink { get; set; }
        public string Currency { get; set; }
        public float AmountDebit { get; set; }
        public float AmountCredit { get; set; }
        public float AmountAdminCosts { get; set; }
        public float AmountCreditNotes { get; set; }
        public float AmountPaid { get; set; }
        public float AmountAdminCostsPaid { get; set; }
        public float AmountPendingSlow { get; set; }
        public float OpenAmount { get; set; }
        public float OpenAmountAdminCosts { get; set; }
        public float OpenAmountInclAdminCosts { get; set; }
        public bool IsPaid { get; set; }
        public object DebtorFileGuid { get; set; }
        public object DebtorFilePayLink { get; set; }
        public object DebtorFileNumber { get; set; }
        public object OriginalInvoiceNumber { get; set; }
    }
}
