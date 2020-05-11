namespace Shopia.Domain
{
    public class PaymentAddModel : CreateTransactionRequest
    {
        public string TransactionId { get; set; }
    }
}
