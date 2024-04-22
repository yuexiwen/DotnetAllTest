using CosmosWideTablePoc;

Console.WriteLine("Hello, World!");

var generator = new UUIDGenerator(10);
generator.StartTask(10000);


while (true)
{
    var lst = generator.FetchIds();
    Console.WriteLine("======================");
    foreach (var item in lst)
    {
        Console.WriteLine(item);
    }
    Thread.Sleep(5000);
}

