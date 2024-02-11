using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HereticalSolutions.StanleyScript.Sample
{
	public class PlayerCharacter : MonoBehaviour
	{
		private List<Perk> selectedActivePerks = new List<Perk>();

		private List<Perk> selectedPassivePerks = new List<Perk>();

		public IEnumerable<Perk> SelectedActivePerks => selectedActivePerks;

		public IEnumerable<Perk> SelectedPassivePerks => selectedPassivePerks;

		public void Immortalize()
		{
			GetComponent<MeshRenderer>().material.color = Color.blue;

			Debug.Log($"Player character {name} was given immortality");
		}

		public void Receive(Perk perk)
		{
			var perkInstance = GameObject.Instantiate(perk.gameObject);

			var selectedPerk = perkInstance.GetComponent<Perk>();

			if (selectedPerk.Passive)
				selectedPassivePerks.Add(selectedPerk);
			else
				selectedActivePerks.Add(selectedPerk);
			
			selectedPerk.transform.SetParent(
				transform,
				false);

			selectedPerk.Selected();
		}

		public bool HasPerk(Perk perk)
		{
			return (perk.Passive
				? selectedPassivePerks
				: selectedActivePerks)
				.Any((element) => { return element.Name == perk.Name; });
		}
	}
}