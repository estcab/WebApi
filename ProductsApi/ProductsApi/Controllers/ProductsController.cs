﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductsApi.Models;

namespace ProductsApi.Controllers
{
    public class ProductsController : ApiController
    {
        private Product[] _products = new[]
        {
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M } 
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
