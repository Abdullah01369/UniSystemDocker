namespace UniSystem.Core.PaymentModels
{
    public class CallBackData
    {
        public string status { get; set; }
        public string paymentId { get; set; }
        public string conversationData { get; set; }
        public string conversationId { get; set; }
        public string mdStatus { get; set; }

    }
}
