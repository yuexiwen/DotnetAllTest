using CosmosWideTablePoc;

var generator = new UUIDGenerator(10);
var consumer = new ConnectionStartEntity(generator);
generator.StartTask(10000);
consumer.StartTask(5000);


while (true) { }
