using System.Threading.Tasks;

using UnityEngine;

class MyClass
{
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	static void LogExceptions()
	{
		UnityEngine.Debug.Log("LogExceptions");

		TaskScheduler.UnobservedTaskException +=
			(_, e) => UnityEngine.Debug.LogException(e.Exception);
	}
}
