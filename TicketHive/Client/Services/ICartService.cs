using TicketHive.Shared;

namespace TicketHive.Client.Services
{
    public interface ICartService
    {
        // Load Cookies
        Task LoadCookies();

        // Get cart items
        List<CartItemModel> GetShoppingCartItems();

        // Add to cart
        Task AddToCartAsync(EventModel addEvent);

        // Remove from Cart
        Task RemoveFromCartAsync(CartItemModel removeItem);
    }
}
