namespace ShoppingGo.ViewModels
{
    class CartRemoveViewModel
    {
        public string Message { get; set; }
        public decimal CartTotalAmount { get; set; }
        public int CartCount { get; set; }
        public int ItemCount { get; set; }
        public int DeletedId { get; set; }
    }
}