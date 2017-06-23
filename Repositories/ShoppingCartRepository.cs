using ShoppingCartWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace ShoppingCartWeb.Repositories
{
    public class ShoppingCartRepository
    {
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCartRepository GetCart(HttpContext context)
        {
            var cart = new ShoppingCartRepository();
            cart.ShoppingCartId = cart.GetCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCartRepository GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Album album)
        {
            /*
            // Get the matching cart and album instances
            var cartItem = storeDB.Carts.SingleOrDefault(
                c => c.CartId == ShoppingCartId
                && c.AlbumId == album.AlbumId);

            if (cartItem == null)
            {
                // Create a new cart item if no cart item exists
                cartItem = new Cart
                {
                    AlbumId = album.AlbumId,
                    CartId = ShoppingCartId,
                    Count = 1,
                    DateCreated = DateTime.Now
                };
                storeDB.Carts.Add(cartItem);
            }
            else
            {
                // If the item does exist in the cart, 
                // then add one to the quantity
                cartItem.Count++;
            }
            // Save changes
            storeDB.SaveChanges();
             */
        }
        public int RemoveFromCart(int id)
        {
            int itemCount = 0;
            /* 
            // Get the cart
            var cartItem = storeDB.Carts.Single(
                cart => cart.CartId == ShoppingCartId
                && cart.RecordId == id);

            if (cartItem != null)
            {
                if (cartItem.Count > 1)
                {
                    cartItem.Count--;
                    itemCount = cartItem.Count;
                }
                else
                {
                    storeDB.Carts.Remove(cartItem);
                }
                // Save changes
                storeDB.SaveChanges();
            }
            */
            return itemCount;
        }
        public void EmptyCart()
        {
            /* 
            var cartItems = storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId);

            foreach (var cartItem in cartItems)
            {
                storeDB.Carts.Remove(cartItem);
            }
            // Save changes
            storeDB.SaveChanges();
            */
        }
        public List<Cart> GetCartItems()
        {
            /*
            return storeDB.Carts.Where(
                cart => cart.CartId == ShoppingCartId).ToList();
            */
            return new List<Cart>();
        }
        public int GetCount()
        {
            /* 
            // Get the count of each item in the cart and sum them up
            int? count = (from cartItems in storeDB.Carts
                          where cartItems.CartId == ShoppingCartId
                          select (int?)cartItems.Count).Sum();
            // Return 0 if all entries are null
            return count ?? 0;
            */
            return 0;
        }
        public decimal GetTotal()
        {
            /* 
            // Multiply album price by count of that album to get 
            // the current price for each of those albums in the cart
            // sum all album price totals to get the cart total
            decimal? total = (from cartItems in storeDB.Carts
                              where cartItems.CartId == ShoppingCartId
                              select (int?)cartItems.Count *
                              cartItems.Album.Price).Sum();

            return total ?? decimal.Zero;
            */
            return 0;
        }
        public int CreateOrder(Order order)
        {
            /* 
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Album.Price);

                storeDB.OrderDetails.Add(orderDetail);

            }

            order.Total = orderTotal;
            storeDB.Orders.Attach(order);
            storeDB.Entry(order).State = EntityState.Modified;

            // Save the order
            storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
            */
            return 0;
        }
        public int CreateOrder(Order order, String PromoCode)
        {
            /* 
            decimal orderTotal = 0;

            var cartItems = GetCartItems();

            foreach (var item in cartItems)
            {
                var orderDetail = new OrderDetail
                {
                    AlbumId = item.AlbumId,
                    OrderId = order.OrderId,
                    UnitPrice = item.Album.Price,
                    Quantity = item.Count
                };

                orderTotal += (item.Count * item.Album.Price);

                storeDB.OrderDetails.Add(orderDetail);

            }
            // with PromoCode total price = 0
            // Save the orderDetails
            storeDB.SaveChanges();
            // Empty the shopping cart
            EmptyCart();
            // Return the OrderId as the confirmation number
            return order.OrderId;
            */
            return 0;
        }
        // We're using HttpContextBase to allow access to cookies.
        public string GetCartId(HttpContext context)
        {
            var key = context.Session.GetString("CartId");
            if (string.IsNullOrEmpty(key))
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                    key = context.User.Identity.Name;
                else
                    // Generate a new random GUID using System.Guid class
                    key = Guid.NewGuid().ToString();
                // save the cardId  
                context.Session.SetString("CartId", key);
            }
            return key;
        }
        // When a user has logged in, migrate their shopping cart to
        // be associated with their username
        public void MigrateCart(string userName)
        {
            /*
            var shoppingCart = storeDB.Carts.Where(
                c => c.CartId == ShoppingCartId);

            foreach (Cart item in shoppingCart)
            {
                item.CartId = userName;
            }
            storeDB.SaveChanges();
            */
        }
    }
}
