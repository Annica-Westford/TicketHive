using Blazored.LocalStorage;
using TicketHive.Shared;

namespace TicketHive.Client.Services
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService localStorage;
        private List<CartItemModel> shoppingCart;

        public CartService(ILocalStorageService localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task LoadCookies()
        {
            shoppingCart = await localStorage.GetItemAsync<List<CartItemModel>>("shoppingCartCookie");

            if (shoppingCart == null)
            {
                await localStorage.SetItemAsync<List<CartItemModel>>("shoppingCartCookie", new List<CartItemModel>());
                shoppingCart = await localStorage.GetItemAsync<List<CartItemModel>>("shoppingCartCookie");
            }
        }

        public List<CartItemModel> GetShoppingCartItems()
        {
            return shoppingCart;
        }

        // Lägg till i cart
        public async Task AddToCartAsync(EventModel addEvent)
        {
            CartItemModel newCartItem = new()
            {
                Event = addEvent,
                Quantity = 1
            };

            shoppingCart.Add(newCartItem);
            await localStorage.SetItemAsync<List<CartItemModel>>("shoppingCartCookie", shoppingCart);
        }

        // Ta bort från cart
        public async Task RemoveFromCartAsync(CartItemModel removeItem)
        {
            shoppingCart.Remove(removeItem);
            await localStorage.SetItemAsync<List<CartItemModel>>("shoppingCartCookie", shoppingCart);
        }

    }
}
