namespace GameStore.BLL.Predefined.ConstPredefineds
{
    public class PdPaymentMethods
    {
        public class PaymentMethod
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        /// <summary>
        /// Кошелек GameStore
        /// </summary>
        public readonly PaymentMethod internal_wallet = new() { Id = 1, Name = "Кошелек GameStore" };

        /// <summary>
        /// Юмани
        /// </summary>
        public readonly PaymentMethod yoomoney_wallet = new() { Id = 2, Name = "Юмани (Временно не работает)" };

        /// <summary>
        /// QIWI
        /// </summary>
        public readonly PaymentMethod qiwi_wallet = new() { Id = 3, Name = "QIWI (Временно не работает)" };

        /// <summary>
        /// PayPal
        /// </summary>
        public readonly PaymentMethod paypal_wallet = new() { Id = 4, Name = "PayPal (Временно не работает)" };

        public readonly List<PaymentMethod> paymentMethods;

        public PdPaymentMethods()
        {
            paymentMethods = new()
            {
                internal_wallet,yoomoney_wallet,qiwi_wallet,paypal_wallet
            };
        }
    }
}
