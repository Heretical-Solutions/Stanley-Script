namespace HereticalSolutions.StanleyScript
{
	public interface IStackMachine //TODO: assign to the conext, create "DefaultConcext" in the runtime, on Stop() flush all non-default contexts
	{
		int StackSize { get; }

		void Push(
			IStanleyVariable variable);

		bool Pop(
			out IStanleyVariable variable);

		public bool Peek(
			out IStanleyVariable variable);

		public bool PeekFromTop(
			int relativeIndex,
			out IStanleyVariable variable);

		public bool PeekFromBottom(
			int relativeIndex,
			out IStanleyVariable variable);
	}
}