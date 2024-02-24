namespace HereticalSolutions.StanleyScript
{
	public interface IStanleyEvent
	{
		void Subscribe(StanleyEventSubscriptionDescriptor descriptor);

		void Unsubscribe(object subscriber);

		void UnsubscribeAll();
	}
}