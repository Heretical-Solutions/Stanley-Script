namespace HereticalSolutions.StanleyScript
{
	public class StanleyEventSubscriptionDescriptor
	{
		public object Subscriber { get; private set; }

		public StanleyScriptEventDelegate Delegate { get; private set; }

		public StanleyEventSubscriptionDescriptor(
			object subscriber,
			StanleyScriptEventDelegate subscription)
		{
			Subscriber = subscriber;

			Delegate = subscription;
		}
	}
}