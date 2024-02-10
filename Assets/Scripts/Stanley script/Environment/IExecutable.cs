namespace HereticalSolutions.StanleyScript
{
	public interface IExecutable
	{
		EExecutionStatus Status { get; }

		int CurrentLine { get; }

		void LoadProgram(
			string[] instructions);

		void Start();

		void Step();

		void Pause();

		void Resume();

		void Stop();
	}
}