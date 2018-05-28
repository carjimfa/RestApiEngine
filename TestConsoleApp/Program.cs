using RestApiEngine;
using System;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var engine = new RestEngine("https://reqres.in/api/", "application/json")
                .AddHeader("User-Agent", "RestEngine .NET");
            var task = engine.ProcessGetSync("users");
            var result = task.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);


            engine.ClearUriParams()
                .ClearHeaders()
                .AddAccept("application/json")
                .AddHeader("User-Agent", ".NET Foundation Repository Reporter")
                .AddUriParam("users")
                .AddQueryParam("page", "3");
            var task2 = engine.ProcessGetSync();
            var result2 = task.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result2);

            engine.AddUriParam("1");
            var task3 = engine.ProcessGetSync();
            var result3 = task.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result3);

            engine.AddUriParam("2");
            task3 = engine.ProcessGetSync();
            result3 = task.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result3);

            engine.ClearUriParams().AddUriParam("users").AddQueryParam("page", "2");
            task3 = engine.ProcessGetSync();
            result3 = task.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result3);

            Console.WriteLine("Press Any Key To Finish");
            Console.ReadLine();
        }
    }
}
