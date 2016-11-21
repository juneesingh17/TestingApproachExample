using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using TestingApproachExample.Models;
using TestingApproachExample.Controllers;
using System.Collections.Generic;
using System.Web.Http.Results;
using System.Web.Http;
using System.Net;
using System.Threading;
using System.Net.Http;
using System.Net.Http.Headers;

namespace TestingApproachUnitTest
{    
    public class UnitTest1 : IClassFixture<HttpClientDatabaseFixture>
    {
        HttpClientDatabaseFixture fixture;
        HttpClient httpClient;
        string baseUri;
        public UnitTest1(HttpClientDatabaseFixture fixture)
        {
            this.fixture = fixture;
            httpClient = fixture.httpClient;
            baseUri = fixture.baseUri;
        }

        [Fact]
        public void PassingTest()
        {
            //Simple Math Test
            Assert.Equal(4, Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            //Simple Math Test
            Assert.Equal(8, Add(2, 2));
        }

        //Test to get data from url and save it to person table.
        // Then redirect and send data back to webapi
        [Fact]
        public async void GetAllProducts_ShouldReturnAllProducts()
        {
            using (httpClient)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, baseUri + "products");
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                Product[] products = null;
                HttpResponseMessage response = await httpClient.GetAsync(request.RequestUri);
                if (response.IsSuccessStatusCode)
                {
                    products = await response.Content.ReadAsAsync<Product[]>();
                }
                Assert.Equal(4, products.Length);
                var postUrl = $"{baseUri}products/PostProduct";
                var postResponse =
                    await httpClient.PostAsJsonAsync(postUrl, new Product
                    {
                        Id = 10,
                        Name = "ProductThruPost",
                        Price = 10M
                    });

                Assert.Equal(true, postResponse.IsSuccessStatusCode);
                if (response.IsSuccessStatusCode)
                {
                    products = await postResponse.Content.ReadAsAsync<Product[]>();
                }
                Assert.Equal(5, products.Length);
            }
        }

        [Fact]
        public async void GetProduct_ShouldReturnCorrectModel()
        {
            //var testProducts = GetTestProducts();
            //var controller = new SimpleProductController(testProducts);

            using (httpClient)
            {
                var request = new HttpRequestMessage(HttpMethod.Get, baseUri + "products/1");

                httpClient.DefaultRequestHeaders.Accept.Clear();

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                Product product = null;
                HttpResponseMessage response = await httpClient.GetAsync(request.RequestUri);
                if (response.IsSuccessStatusCode)
                {
                    product = await response.Content.ReadAsAsync<Product>();
                }
                Assert.Equal("Demo1", product.Name);
                //return product;

            }

            //var getResult = controller.GetProduct(1);
            //var posRes = getResult as OkNegotiatedContentResult<Product>;
            //Assert.IsType<OkNegotiatedContentResult<Product>>(getResult);
            //Assert.Equal("Demo1", posRes.Content.Name);
        }

        int Add(int x, int y)
        {
            return x + y;
        }

        private List<Product> GetTestProducts()
        {
            var testProducts = new List<Product>();
            testProducts.Add(new Product { Id = 1, Name = "Demo1", Price = 1 });
            testProducts.Add(new Product { Id = 2, Name = "Demo2", Price = 3.75M });
            testProducts.Add(new Product { Id = 3, Name = "Demo3", Price = 16.99M });
            testProducts.Add(new Product { Id = 4, Name = "Demo4", Price = 11.00M });
            return testProducts;
        }
    }
}
