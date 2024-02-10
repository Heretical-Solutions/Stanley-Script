namespace HereticalSolutions.StanleyScript
{
	public interface IStackMachine
	{
		void Push(
			IStanleyVariable variable);

		bool Pop(
			out IStanleyVariable variable);

		public bool Peek(
			out IStanleyVariable variable);

		public bool PeekAt(
			int index,
			out IStanleyVariable variable);

		public bool PeekFromTop(
			int relativeIndex,
			out IStanleyVariable variable);
	}
}