using System.Collections.Generic;

namespace HereticalSolutions.StanleyScript
{
	public interface IRuntimeEnvironment
	{
		bool LoadInputVariable(
			IStanleyVariable variable);


		bool LoadEventVariable(
				IStanleyVariable variable);

		void UnsubscribeAllEvents();


		bool LoadOperation(
			IStanleyOperation operation);


		bool AddRuntimeVariable(
			IStanleyVariable variable);

		void RemoveAllRuntimeVariables();


		bool GetRuntimeVariable(
			string name,
			out IStanleyVariable variable);

		bool GetImportVariable(
			string name,
			out IStanleyVariable variable);

		bool GetEventVariable(
			string name,
			out IStanleyVariable variable);

		bool GetOperation(
			string opcode,
			out IEnumerable<IStanleyOperation> operations);
	}
}