using System;

namespace NsavinJson
{
    class Program
    {
        static void Main(string[] args)
        {
           var testClass = new TestClass();
           Console.Write(Json.GetObject(testClass)); 
        }
    }
}
