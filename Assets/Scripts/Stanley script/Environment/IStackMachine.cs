namespace HereticalSolutions.StanleyScript
{
	public interface IStackMachine
	{
		void AddRuntimeVariable(
			string name,
			IStanleyVariable variable);

		void Push(
			IStanleyVariable variable);

		IStanleyVariable Pop();
	}
}