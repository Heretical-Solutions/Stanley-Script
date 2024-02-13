using UnityEngine;
using ImGuiNET;
using UImGui;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyPlaybackMono : MonoBehaviour
	{
		private StanleyPlayback stanleyPlayback;

		private Vector2 lastSize;

		void Awake()
		{
			stanleyPlayback = new StanleyPlayback();
		}

		private void OnEnable()
		{
			UImGuiUtility.Layout += OnLayout;
		}

		private void OnDisable()
		{
			UImGuiUtility.Layout -= OnLayout;
		}

		private void OnLayout(UImGuiBehaviour uImGui)
		{
			stanleyPlayback.ShowWindow();
		}
	}
}

