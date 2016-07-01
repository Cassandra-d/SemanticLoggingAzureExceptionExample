using System;
using EventSource;
using Microsoft.Practices.EnterpriseLibrary.SemanticLogging;
using System.Diagnostics.Tracing;
using Microsoft.WindowsAzure.Storage;


namespace SemanticLoggingException
{
	class Program
	{
		static void Main(string[] args)
		{
			ObservableEventListener listener1;
			try
			{
				listener1 = new ObservableEventListener();
				var conString =
					$"DefaultEndpointsProtocol={CloudStorageAccount.DevelopmentStorageAccount.TableEndpoint.Scheme};" +
					$"AccountName={CloudStorageAccount.DevelopmentStorageAccount.Credentials.AccountName};" +
					$"AccountKey={Convert.ToBase64String(CloudStorageAccount.DevelopmentStorageAccount.Credentials.ExportKey())}";
					
				listener1.LogToWindowsAzureTable( // <---- EXCEPTION HERE
						instanceName: "instName",
						connectionString: conString);

				listener1.EnableEvents(MyCompanyEventSource.Log, EventLevel.LogAlways,
					MyCompanyEventSource.Keywords.Perf | MyCompanyEventSource.Keywords.Diagnostic | 
					MyCompanyEventSource.Keywords.Page | MyCompanyEventSource.Keywords.DataBase);
			}
			catch(Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}
