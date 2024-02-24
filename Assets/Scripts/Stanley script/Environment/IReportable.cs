namespace HereticalSolutions.StanleyScript
{
	public interface IReportable
	{
		void Log(
			string contextID,
			string message);

		string[] GetReport();

		void ClearReport();
	}
}