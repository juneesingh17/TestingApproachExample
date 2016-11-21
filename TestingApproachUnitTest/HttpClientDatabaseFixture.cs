using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TestingApproachUnitTest
{
        public class HttpClientDatabaseFixture : IDisposable
        {
        public string baseUri { get; set; } 

        public HttpClient httpClient { get; set; }
        public HttpClientDatabaseFixture()
            {
            httpClient = new HttpClient();
            baseUri = "http://localhost/TestingApproach/api/";
            }

        public void Dispose()
            { 
            }       
        }
  }
