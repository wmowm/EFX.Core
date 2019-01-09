using Newtonsoft.Json;
using RdKafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tibos.Test;

namespace Test.Docker
{
    public class Kafka
    {
        public async Task TestAsync()
        {
            // Producer 接受一个或多个 BrokerList
            using (Producer producer = new Producer("132.232.4.73:9092"))
            //发送到一个名为 testtopic 的Topic，如果没有就会创建一个
            using (Topic topic = producer.Topic("testtopic"))
            {
                //将message转为一个 byte[]
                byte[] data = Encoding.UTF8.GetBytes("Hello RdKafka");
                DeliveryReport deliveryReport = await topic.Produce(data);

                Console.WriteLine($"发送到分区：{deliveryReport.Partition}, Offset 为: {deliveryReport.Offset}");
            }





            //配置消费者组
            var config = new Config() { GroupId = "example-csharp-consumer" };
            using (var consumer = new EventConsumer(config, "132.232.4.73:9092"))
            {

                //注册一个事件
                consumer.OnMessage += (obj, msg) =>
                {
                    string text = Encoding.UTF8.GetString(msg.Payload, 0, msg.Payload.Length);
                    Console.WriteLine($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {text}");
                };

                //订阅一个或者多个Topic
                consumer.Subscribe(new[] { "testtopic" });

                //启动
                consumer.Start();

                Console.WriteLine("Started consumer, press enter to stop consuming");
                Console.ReadLine();
            }

        }
    }
}
