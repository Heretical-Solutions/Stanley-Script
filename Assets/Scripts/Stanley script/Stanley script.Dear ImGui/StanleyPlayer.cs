using System;
using System.Numerics;
using ImGuiNET;

namespace HereticalSolutions.StanleyScript
{
	public class StanleyPlayer
	{
		private readonly IStanleyInterpreter interpreter;

		private readonly IExecutable executable;

		private readonly ScenarioNameScriptPair[] scenarioPlaylist;


		private string currentScenarioName;

		private string currentScenario;

		public StanleyPlayer(
			IStanleyInterpreter interpreter,
			IExecutable executable,
			ScenarioNameScriptPair[] scenarioPlaylist)
		{
			this.interpreter = interpreter;

			this.scenarioPlaylist = scenarioPlaylist;

			this.executable = executable;


			currentScenarioName = string.Empty;

			currentScenario = string.Empty;
		}

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

			#region Playback controls

			ImGui.SeparatorText("Playback controls");

			DrawStartButton();

			ImGui.SameLine();

			DrawStepButton();

			ImGui.SameLine();

			DrawPauseButton();

			ImGui.SameLine();

			DrawResumeButton();

			ImGui.SameLine();

			DrawStopButton();

			#endregion

			#region Scenario listing

			ImGui.SeparatorText("Scenario listing");

			DrawScenarioListing();

			#endregion

			#region Scenario playlist

			ImGui.SeparatorText("Scenario playlist");

			DrawScenarioPlaylist();

			#endregion

			// End of ShowDemoWindow()
			ImGui.PopItemWidth();
			ImGui.End();
		}

		private void DrawStartButton()
		{
			if (ImGui.Button("Start"))
			{
				executable.Start();
			}
		}

		private void DrawStepButton()
		{
			if (ImGui.Button("Step"))
			{
				executable.Step();
			}
		}

		private void DrawPauseButton()
		{
			if (ImGui.Button("Pause"))
			{
				executable.Pause();
			}
		}

		private void DrawResumeButton()
		{
			if (ImGui.Button("Resume"))
			{
				executable.Resume();
			}
		}

		private void DrawStopButton()
		{
			if (ImGui.Button("Stop"))
			{
				executable.Stop();
			}
		}

		private void DrawScenarioListing()
		{
			if (!string.IsNullOrEmpty(currentScenarioName))
			{
				ImGui.Text($"Current scenario: {currentScenarioName}");

				ImGui.BeginChild(
					"Listing",
					new Vector2(
						ImGui.GetContentRegionAvail().X * 0.75f,
						300));

				string[] lines = currentScenario.Split('\n'); //executable.Instructions;

				if (lines != null)
				{
					for (int i = 0; i < lines.Length; i++)
					{
						ImGui.Text(lines[i]);
					}
				}

				ImGui.EndChild();
			}
		}

		private void DrawScenarioPlaylist()
		{
			for (int i = 0; i < scenarioPlaylist.Length; i++)
			{
				if (ImGui.Button($"Load {scenarioPlaylist[i].Name}"))
				{
					currentScenarioName = scenarioPlaylist[i].Name;

					currentScenario = scenarioPlaylist[i].Script;

					var instructions = interpreter.InterpretToOpcode(scenarioPlaylist[i].Script);
					
					executable.LoadProgram(instructions);
				}
			}
		}
	}
}