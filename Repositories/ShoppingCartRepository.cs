using ShoppingCartWeb.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ShoppingCartWeb.Helpers;
using ShoppingCartWeb.ViewModels;
using ShoppingCartWeb.Network;
using Newtonsoft.Json;

namespace ShoppingCartWeb.Repositories
{
    public class ShoppingCartRepository
    {
        string ShoppingCartId { get; set; }
        public const string CartSessionKey = "CartId";
        public static ShoppingCartRepository GetCart(HttpContext context)
        {
            var cart = new ShoppingCartRepository();
            cart.ShoppingCartId = SessionHelper.GetShoppingCartId(context);
            return cart;
        }
        // Helper method to simplify shopping cart calls
        public static ShoppingCartRepository GetCart(Controller controller)
        {
            return GetCart(controller.HttpContext);
        }
        public void AddToCart(Album album)
        {
            CartViewModel cart = new CartViewModel
            {
                AlbumId = album.AlbumId,
                GenreId = album.GenreId,
                ArtistId = album.ArtistId,
                Title = album.Title,
                Price = album.Price,
                CartId = ShoppingCartId
            };
            var response = RequestHelper.Post(ShoppingCartAPI.AddToCartURL, cart);
            Console.WriteLine("AddToCart response: " + response);
        }
        public ShoppingCartRemoveViewModel RemoveFromCart(int id)
        {
            CartDeleteViewModel cart = new CartDeleteViewModel
            {
                ShoppingCartId = this.ShoppingCartId,
                RecordId = id
            };
            var result = RequestHelper.Post(ShoppingCartAPI.RemoveFromCartURL, cart);
            return JsonConvert.DeserializeObject<ShoppingCartRemoveViewModel>(result);
        }

        public int GetCount()
        {
            var url = ShoppingCartAPI.ShoppingCartURL + "/?ShoppingCartId=" + this.ShoppingCartId;
            var result = RequestHelper.Get(url);
            return Int32.Parse(result);
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
        public ShoppingCartViewModel GetCartItems(string ShoppingCartId)
        {
            var response = RequestHelper.Get(ShoppingCartAPI.ShoppingCartURL + "/" + ShoppingCartId);
            Console.WriteLine("GetCartItems result: " + response);
            return JsonConvert.DeserializeObject<ShoppingCartViewModel>(response);;
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
