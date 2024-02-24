using System.Threading;
using System.Threading.Tasks;

namespace HereticalSolutions.StanleyScript
{
	public interface IREPL
	{
		Task<bool> Execute(
			string instruction,
			IStanleyContext context = null,
			CancellationToken cancellationToken = default);
	}
}