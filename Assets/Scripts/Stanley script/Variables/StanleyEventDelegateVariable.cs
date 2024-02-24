using System;
using System.Collections.Generic;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyEventDelegateVariable
		: IStanleyVariable,
		  IStanleyEvent
	{
		private List<StanleyEventSubscriptionDescriptor> subscriptions;

		private StanleyScriptEventDelegate @delegate;

		public StanleyEventDelegateVariable(
			string name,
			List<StanleyEventSubscriptionDescriptor> subscriptions,
			StanleyScriptEventDelegate @delegate = null)
		{
			Name = name;

			VariableType = typeof(Delegate);

			this.subscriptions = subscriptions;

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

		#region IStanleyDelegate

		public void Subscribe(StanleyEventSubscriptionDescriptor descriptor)
		{
			subscriptions.Add(descriptor);

			@delegate += descriptor.Delegate;
		}

		public void Unsubscribe(object subscriber)
		{
			for (int i = subscriptions.Count - 1; i >= 0; i--)
			{
				if (subscriptions[i].Subscriber == subscriber)
				{
					@delegate -= subscriptions[i].Delegate;

					subscriptions.RemoveAt(i);

					return;
				}
			}
		}

		public void UnsubscribeAll()
		{
			foreach (var subscription in subscriptions)
			{
				@delegate -= subscription.Delegate;
			}

			subscriptions.Clear();
		}

		#endregion
	}
}