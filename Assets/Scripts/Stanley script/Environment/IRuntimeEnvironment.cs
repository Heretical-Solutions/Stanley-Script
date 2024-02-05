using System.Collections.Generic;

namespace HereticalSolutions.StanleyScript
{
	public interface IRuntimeEnvironment
	{
		bool LoadInputVariable(
			string name,
			IStanleyVariable variable);

		bool LoadOperation(
			string name,
			IStanleyOperation operation);


		bool AddRuntimeVariable(
			string name,
			IStanleyVariable variable);


		bool GetRuntimeVariable(
			string name,
			out IStanleyVariable variable);

		bool GetImportVariable(
			string name,
			out IStanleyVariable variable);

		bool GetOperation(
			string opcode,
			out IEnumerable<IStanleyOperation> operations);
	}
}