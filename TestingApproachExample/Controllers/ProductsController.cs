using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using TestingApproachExample.Models;

namespace TestingApproachExample.Controllers

{
    public class ProductsController : ApiController
    {
        List<Product> products = 
        new List<Product>() { 
            new Product { Id = 1, Name = "Demo1", Price = 1 },
            new Product { Id = 2, Name = "Demo2", Price = 3.75M },
            new Product { Id = 3, Name = "Demo3", Price = 16.99M },
            new Product { Id = 4, Name = "Demo4", Price = 11.00M }};

        public ProductsController() { }

        public ProductsController(List<Product> products)
        {
            this.products = products;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return products;
        }

        //public async Task<IEnumerable<Product>> GetAllProductsAsync()
        //{
        //    return await Task.FromResult(GetAllProducts());
        //}

        public IHttpActionResult GetProduct(int id)
        {
            var product = products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        //public async Task<IHttpActionResult> GetProductAsync(int id)
        //{
        //    return await Task.FromResult(GetProduct(id));
        //}

        
        public IHttpActionResult PostProduct([FromBody] Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }
            products.Add(product);
            return Ok(products);
        }
    }
}
