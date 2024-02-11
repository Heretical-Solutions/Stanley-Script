using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class Perk : MonoBehaviour
	{
		public string Name;

		public bool Passive;

		public void Selected()
		{
			//tag = "Selected";

			GetComponent<MeshRenderer>().material.color = Color.green;

			Debug.Log($"Perk {Name} selected");
		}

		public void MaxOut()
		{
			GetComponent<MeshRenderer>().material.color = Color.red;

			Debug.Log($"Perk {Name} maxed out");
		}
	}
}