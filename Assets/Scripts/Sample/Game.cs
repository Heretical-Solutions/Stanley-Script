using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class Game : MonoBehaviour
	{
		private bool playing = false;

		private float speed = 1.0f;

		public void StartGame()
		{
			playing = true;

			Debug.Log("Game started");
		}

		public void PauseGame()
		{
			playing = false;

			Debug.Log("Game paused");
		}

		public void UnpauseGame()
		{
			playing = true;

			Debug.Log("Game unpaused");
		}

		public void SetSpeed(float speed)
		{
			this.speed = speed;

			Debug.Log($"Game speed set to {speed}");
		}
	}
}