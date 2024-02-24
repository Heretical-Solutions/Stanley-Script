using System;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyDelegateVariable
		: IStanleyVariable
	{
		private Action<object[]> @delegate;

		public StanleyDelegateVariable(
			string name,
			Action<object[]> @delegate)
		{
			Name = name;

			VariableType = typeof(Delegate);

			this.@delegate = @delegate;
		}

		#region IStanleyVariable

		public string Name { get; private set; }

		public Type VariableType { get; private set; }

		public object GetValue()
		{
			return @delegate;
		}

		public T GetValue<T>()
		{
			var result = GetValue();

			switch (result)
			{
				case T value:
					return value;

				default:
					throw new InvalidCastException();
			}
		}

		#endregion
	}
}