using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myers
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = new JValue { Object = new Dictionary<string, JValue>() };
            json["name"] = new JValue("Cam Newton");
            json["age"] = new JValue(26);
            json["male"] = new JValue(true);
            byte[] bytes = { 10, 15, 20 };
            json["byte[]"] = new JValue(bytes);

            List<JValue> Cats = new List<JValue>();
            Cats.Add(new JValue("Molly"));
            Cats.Add(new JValue("Squinty"));
            json["cats"] = new JValue(Cats);

            if (json.Type == JType.Object)
            {
                foreach (var value in json.Object)
                {
                    Console.Write(value.Key + ": ");
                    Console.Write(value.Value + "\n");
                }
            }

            Console.WriteLine("\n" + json);
        }
    }
}
