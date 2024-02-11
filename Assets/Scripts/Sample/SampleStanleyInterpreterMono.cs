using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class SampleStanleyInterpreterMono : MonoBehaviour
	{
		[SerializeField]
		private TextAsset scriptFile;

		[SerializeField]
		private Game game;

		[SerializeField]
		private PlayerCharacter playerCharacter;

		[SerializeField]
		private Perk[] activePerks;

		[SerializeField]
		private Perk[] passivePerks;

		private StanleyInterpreter interpreter;

		void Awake()
		{
			interpreter = StanleyFactory.BuildInterpreter();

			var environment = interpreter.Environment;

			#region Input variables

			environment.LoadInputVariable(
				new StanleyPollableVariable(
					"game",
					typeof(Game),
					() => { return game; }));

			environment.LoadInputVariable(
				new StanleyPollableVariable(
					"user",
					typeof(MonoBehaviour),
					() => { return this; }));

			environment.LoadInputVariable(
				new StanleyPollableVariable(
					"pc",
					typeof(PlayerCharacter),
					() => { return playerCharacter; }));

			environment.LoadInputVariable(
				new StanleyPollableVariable(
					"all_active_perks",
					typeof(Perk[]),
					() => { return activePerks; }));

			environment.LoadInputVariable(
				new StanleyPollableVariable(
					"all_passive_perks",
					typeof(Perk[]),
					() => { return passivePerks; }));

			environment.LoadInputVariable(
				new StanleyPollableVariable(
					"selected_active_perks",
					typeof(Perk[]),
					() => { return playerCharacter.SelectedActivePerks.ToArray(); }));

			environment.LoadInputVariable(
				new StanleyPollableVariable(
					"selected_passive_perks",
					typeof(Perk[]),
					() => { return playerCharacter.SelectedPassivePerks.ToArray(); }));

			#endregion

			#region Game controls

			environment.LoadOperation(
				new StartGame());

			environment.LoadOperation(
				new PauseGame());

			#endregion

			#region Player controls

			environment.LoadOperation(
				new FaceDirection());

			#endregion

			#region Give's

			environment.LoadOperation(
				new GivePCImmortality());

			environment.LoadOperation(
				new GivePCPerk());

			#endregion

			#region Select's

			environment.LoadOperation(
				new SelectRandomPerk());

			environment.LoadOperation(
				new SelectRandomDirection());

			#endregion

			#region Perks

			//To unwrap concatenated perk variables
			environment.LoadOperation(
				new LeftJoinConcatenationVariables(
					"max",
					new [] { "maxed" }));

			//To unwrap perk arrays
			environment.LoadOperation(
				new LeftJoinArrayValues(
					"max",
					new[] { "maxed" }));

			environment.LoadOperation(
				new MaxOutPerk());

			#endregion
		}

		void Start()
		{
			interpreter.Execute(scriptFile.text);
		}
	}
}