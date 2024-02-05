namespace HereticalSolutions.StanleyScript
{
	public interface IStackMachine
	{
		void Push(
			IStanleyVariable variable);

		IStanleyVariable Pop();
	}
}