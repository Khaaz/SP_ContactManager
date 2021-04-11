using Data;
using Serialization;

namespace ContactManager
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            //Serializer serializer = new BinarySerializer();
            Serializer serializer = new XMLSerializer();
            Engine engine = new Engine(manager, serializer);
            engine.Run();
        }
    }
}
