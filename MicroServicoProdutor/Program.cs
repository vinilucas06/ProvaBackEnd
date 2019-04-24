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
using Dominio.Interfaces;
using Dominio.Servicos;
using Infra.Persistencia.Repositorio.Sistema;

namespace MicroServicoProdutor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==Serviço Produtor 01==");
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logsProdutor/log.txt")
                .CreateLogger();

            

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection
                 .AddSingleton<IServicoObjetoGenerico, ServicoObjetoGenerico>()
                 .AddSingleton<IRepositorioObjetoGenerico, RepositorioObjetoGenerico>()
                .BuildServiceProvider();
            serviceProvider.GetService<LogInjectConfig>().Start();
            var logger = serviceProvider.GetService<ILogger<Program>>();


            var servicoObjetoGenerico = serviceProvider.GetService<IServicoObjetoGenerico>();
           

            var config = new Dictionary<string, object>
       {
         { "bootstrap.servers", "localhost:9092" },
                 { "queue.buffering.max.ms", 5000 },
                 { "auto.commit.interval.ms", 5000}
       };



            using (var producer = new Producer<string, string>(config, new StringSerializer(Encoding.UTF8), new StringSerializer(Encoding.UTF8)))
            {
                ObjetoGenerico obj = new ObjetoGenerico();
                string text = null;
                string key = null;
                while (text != "exit")
                {
                    //   text = Console.ReadLine();
                    //  key = Guid.NewGuid().ToString("N");
                    text = "Hello World";
                    obj = servicoObjetoGenerico.Cadastrar("Produtor - 01");
                    producer.ProduceAsync("hello-topic", JsonConvert.SerializeObject(obj), text).GetAwaiter().GetResult();
                   // System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                }

                producer.Flush(5000);
            }


        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => configure.AddSerilog())
                    .AddTransient<LogInjectConfig>();
        }
    }
}
