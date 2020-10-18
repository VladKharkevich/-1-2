using System;
using Newtonsoft.Json;

namespace Faker
{
    class Program
    {
        static void Main(string[] args)
        {
            Faker faker = new Faker();
            ClassDTO_A test = faker.Create<ClassDTO_A>();
            Console.WriteLine(JsonConvert.SerializeObject(test, Formatting.Indented));
        }
    } 
}
