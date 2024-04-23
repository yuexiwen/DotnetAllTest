using CosmosWideTablePoc;

var generator = new UUIDGenerator(10);
var connect = new CosmosConnect("https://xiwtest.documents.azure.com:443/", "xiwtest", "xiwtest1");
var consumer = new ConnectionStartEntity(generator, connect);
var consumer1 = new NetworkEntity(generator, connect);
generator.StartTask(10000);
consumer.StartTask(5000);
consumer1.StartTask(2000);

while (true) { }
