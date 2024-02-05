using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public interface IStanleyOperation
	{
		string Opcode { get; }

		string[] Aliases { get; }

		bool WillHandle(
			string[] instructionTokens,
			StanleyEnvironment environment);

		Task<bool> Handle(
			string[] instructionTokens,
			StanleyEnvironment environment,
			CancellationToken token);
	}
}