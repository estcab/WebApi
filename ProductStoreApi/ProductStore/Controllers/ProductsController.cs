using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductStore.Models;

namespace ProductStore.Controllers
{
    public class ProductsController : ApiController
    {
        static readonly IProductRepository Repository = new InMemoryProductRepository();

        public IEnumerable<Product> GetAllProducts()
        {
            return Repository.GetAll();
        }



        //public Product GetProduct(int id)  // HttpResponseException
        //{
        //    var item = Repository.Get(id);
        //    if (item == null)
        //    {
        //        var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
        //        {
        //            Content = new StringContent(string.Format("No product with ID = {0}", id)),
        //            ReasonPhrase = "Product ID Not Found"
        //        };

        //        throw new HttpResponseException(resp);
        //    }

        //    return item;
        //}

        public HttpResponseMessage GetProduct(int id) // HttpResponseMessage
        {
            var item = Repository.Get(id);
            if (item == null)
            {
                var message = string.Format("Product with id = {0} not found", id);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }

            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return Repository.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage PostProduct(Product item)
        {
            item = Repository.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

            var uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public void PutProduct(int id, Product product)
        {
            product.Id = id;
            if (!Repository.Update(product))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        public void DeleteProduct(int id)
        {
            var item = Repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Repository.Remove(id);
        }
    }
}
