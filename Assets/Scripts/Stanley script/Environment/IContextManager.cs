namespace HereticalSolutions.StanleyScript
{
	public interface IContextManager
	{
		IStanleyContext DefaultContext { get; }

		IStanleyContext AllocateContext();

		void ReleaseContext(
			IStanleyContext context);
	}
}