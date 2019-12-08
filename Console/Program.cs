using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AttackAsync
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var task = new List<Task>();
            for (var a = 0; a < 100; a++)
            {
                //task.Add(testAsync(a));
                task.Add(test(a));
            }

            await Task.WhenAll(task);
        }

        public static async Task test(int a)
        {

            Console.WriteLine(a);
            var client = new RestClient("https://localhost:44398/api/default/sync");
            var request = new RestRequest(Method.GET);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            Console.WriteLine(response.Content);
        }

        public static async Task testAsync(int a)
        {

            Console.WriteLine(a);
            var client = new RestClient("https://localhost:44398/api/default/async");
            var request = new RestRequest(Method.GET);
            var cancellationTokenSource = new CancellationTokenSource();
            IRestResponse response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            Console.WriteLine(response.Content);
        }
    }
}
