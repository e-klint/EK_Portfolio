using TheBookParlour.Data.Entities;

namespace TheBookParlour.Core.Helpers
{
    public static class CartHelper
    {
        public static decimal GetTotalSum(List<CartItem> items)
        {
            return items.Sum(i => i.Price * i.Quantity);
        }
    }
}
