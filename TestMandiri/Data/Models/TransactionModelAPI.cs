namespace TestMandiri.Data.Models
{
    public class ItemSell
    {
        public string idItem { set; get; }
        public int qty { set; get; }
    }
    public class TransactionModel
    {
      public int idUser { set; get; }
        public List<ItemSell> items { set; get; }
    }
}
