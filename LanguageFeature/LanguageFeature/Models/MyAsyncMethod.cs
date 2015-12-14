using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;

namespace LanguageFeature.Models
{
    public class MyAsyncMethod
    {
        /*
         *You will have noticed that we did not provide an MVC example for you to test out the async and await keywords. 
         * This is because using asynchronous methods in MVC controllers requires a special technique, 
         * and we have a lot of information
         */

        // a simple asynchronous method
        public static Task<long?> GetPageLength()
        {
            HttpClient client = new HttpClient();
            var httpTask = client.GetAsync("http://apress.com");
            //we could do other things here while we are waiting
            //for the HTTP request to complete

            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) => {
                return antecedent.Result.Content.Headers.ContentLength;
            });

        }

        //using the async and await keywords 
        public async static Task<long?> GetPageLengthUseKeyWord()
        {
            HttpClient httpClient = new HttpClient();
            var httpMessage = await httpClient.GetAsync("http://appress.com");

            //we could do other things here while we are waiting
            //for the HTTP request to complete

            return httpMessage.Content.Headers.ContentLength;
        }
    }
}