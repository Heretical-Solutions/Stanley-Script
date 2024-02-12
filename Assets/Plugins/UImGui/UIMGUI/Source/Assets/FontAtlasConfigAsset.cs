﻿using UnityEngine;

namespace UImGui.Assets
{
	// TODO: Make a default resource file as sample.
	[CreateAssetMenu(menuName = "Dear ImGui/Font Atlas Configuration")]
	public class FontAtlasConfigAsset : ScriptableObject
	{
		public uint RasterizerFlags;
		public FontDefinition[] Fonts;
	}
}
