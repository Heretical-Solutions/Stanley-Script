using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyInterpreterMono : MonoBehaviour
	{
		[SerializeField]
		private TextAsset scriptFile;

		void Start()
		{
			StanleyInterpreter interpreter = StanleyFactory.BuildInterpreter();

			interpreter.Execute(scriptFile.text);
		}
	}
}