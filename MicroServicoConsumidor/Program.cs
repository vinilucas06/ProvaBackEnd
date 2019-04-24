using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using Domain.Entities;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infra.Persistencia.Repositorio.Sistema;
using Dominio.Interfaces;
using Dominio.Servicos;

namespace MicroServicoConsumidor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==CONSUMIDOR==");
            Log.Logger = new LoggerConfiguration()
          .WriteTo.File("logs/log.txt")
          .CreateLogger();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection
                .BuildServiceProvider();
            serviceProvider.GetService<LogInjectConfig>().Start();
            var logger = serviceProvider.GetService<ILogger<Program>>();


            var config = new Dictionary<string, object>
           {
               { "group.id", "sample-consumer" },
               { "bootstrap.servers", "localhost:9092" },
               { "enable.auto.commit", "false"},
               { "queue.buffering.max.ms", 5000 },
               { "auto.commit.interval.ms", 5000}
           };



            using (var consumer = new Consumer<string, string>(config, new StringDeserializer(Encoding.UTF8), new StringDeserializer(Encoding.UTF8)))
            {
                ObjetoGenerico obj = new ObjetoGenerico();
                consumer.Subscribe(new string[] { "hello-topic","hello-topic2"});
                consumer.OnMessage += (_, msg) =>
                {
                    try
                    {
                        Log.Logger.Information("Iniciando leitura mensagem");
                        if (msg.Key != null)
                            obj = JsonConvert.DeserializeObject<ObjetoGenerico>(msg.Key);

                        Console.WriteLine(
                            "=======INICIO=========" +
                            $"Msg: {msg.Value}\n" +
                            $"Micro Serviço: {obj.NameService}\n" +
                            $"TimeStamp: {msg.Timestamp.UtcDateTime} \n" +
                            $"IdRequest: {obj.IdRequest} \n" +
                            $"Topic: {msg.Topic} \n" +
                            $"Partition: {msg.Partition} \n" +
                            $"Offset: {msg.Offset} {msg.Value} \n" +
                              "========FIM========");
                        consumer.CommitAsync(msg);
                    }
                    catch (Exception ex)
                    {
                        Log.Fatal(ex, "ERRO");
                    }
                    finally
                    {
                        Log.CloseAndFlush();
                    }
                };
                while (true)
                    consumer.Poll(100);

            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog())
                    .AddTransient<LogInjectConfig>();
        }
    }
}
