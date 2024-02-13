using ImGuiNET;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyPlayback
	{
		public void ShowWindow()
		{
			// Main body of the Demo window starts here.
			if (!ImGui.Begin("Stanley script playback"))
			{
				// Early out if the window is collapsed, as an optimization.
				ImGui.End();

				return;
			}

			// Most "big" widgets share a common width settings by default. See 'Demo->Layout->Widgets Width' for details.
			// e.g. Use 2/3 of the space for widgets and 1/3 for labels (right align)
			//ImGui::PushItemWidth(-ImGui::GetWindowWidth() * 0.35f);
			// e.g. Leave a fixed amount of width for labels (by passing a negative value), the rest goes to widgets.
			ImGui.PushItemWidth(ImGui.GetFontSize() * -12);


			ImGui.SeparatorText("Controls");

			if (ImGui.Button("Play"))
			{
				UnityEngine.Debug.Log("Button pressed");
			}


			// End of ShowDemoWindow()
			ImGui.PopItemWidth();
			ImGui.End();
		}
	}
}