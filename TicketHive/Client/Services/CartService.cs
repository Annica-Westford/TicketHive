namespace TicketHive.Client.Services
{
    public class CartService
    {
        public int ItemCount { get; private set; }

        // Inte ett bra system, vill hellre ha ett som är baserat på antalet events i korgen
        public void CartCounterUpdate(int newCounter)
        {
            ItemCount += newCounter;
        }

        public void CartCounterReset()
        {
            ItemCount = 0;
        }
    }
}
