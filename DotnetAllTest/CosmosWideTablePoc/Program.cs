using CosmosWideTablePoc;

var generator = new UUIDGenerator(10);
var connect = new CosmosConnect("https://xiwtest.documents.azure.com:443/", "xiwtest", "xiwtest1");
var consumer = new ConnectionStartEntity(generator, connect);
generator.StartTask(10000);
consumer.StartTask(5000);

while (true) { }
