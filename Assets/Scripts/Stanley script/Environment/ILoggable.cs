namespace HereticalSolutions.StanleyScript
{
	public interface ILoggable
	{
		void Log(
			string message);

		string[] GetLogs();
	}
}