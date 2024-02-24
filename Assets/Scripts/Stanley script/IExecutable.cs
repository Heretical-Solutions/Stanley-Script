namespace HereticalSolutions.StanleyScript
{
	public interface IExecutable
	{
		void Start();

		void Step();

		void Pause();

		void Resume();

		void Stop();
	}
}