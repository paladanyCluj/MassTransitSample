using MassTransit;

namespace MyMassTransitSampleApp
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var connstring = "";
			var queueName = "myqueue";
			var busControl = Bus.Factory.CreateUsingAzureServiceBus(cfg =>
			{
				cfg.Host(connstring);
			});

			await busControl.StartAsync();

			try
			{
				var mySample = new Message() { latitude = "1", Latitude = "1" };
				Uri serviceBusAddress = new Uri($"queue:{queueName}");
				var sendEndpoint = await busControl.GetSendEndpoint(serviceBusAddress);

				await sendEndpoint.Send(mySample);

				Console.WriteLine("Message Sent!");
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
			finally
			{
				await busControl.StopAsync();
			}
		}
	}

	public class Message
	{
		public string Latitude { get; set; }
		public string latitude { get; set; }
	}
}