namespace Core
{
    public struct SlotPrice
    {
        public int Id;
        public int Price;

        public SlotPrice(int id, int price)
        {
            Id = id;
            Price = price;
        }
    }
}