using System.Threading;

namespace HereticalSolutions.StanleyScript
{
	public interface IStanleyContext
	{
		string ContextID { get; }

		EExecutionStatus Status { get; }

		int CurrentLine { get; set; }

		int ProgramCounter { get; }

		string[] Instructions { get; }

		void LoadProgram(
			string[] instructions);

		CancellationToken CancellationToken { get; }

		void Initialize(
			string contextID,
			IReportable reportable,
			IREPL repl,
			CancellationToken cancellationToken);

		void Cleanup();
	}
}