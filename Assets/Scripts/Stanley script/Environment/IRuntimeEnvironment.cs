namespace HereticalSolutions.StanleyScript
{
	public interface IRuntimeEnvironment
	{
		void LoadInputVariable(
			string name,
			IStanleyVariable variable);

		void LoadOperation(
			string name,
			IStanleyOperation operation);
	}
}