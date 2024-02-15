using System.Threading.Tasks;

using UnityEngine;

class MyClass
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void LogExceptions()
	{
		Debug.Log("INITIATING CAPTURING ASYNC EXCEPTIONS");

		TaskScheduler.UnobservedTaskException +=
			(_, e) => UnityEngine.Debug.LogException(e.Exception);
	}
}
