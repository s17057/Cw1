using System;
using System.Net.Http;

namespace Cw1
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(args[0]);
        }
    }
}
