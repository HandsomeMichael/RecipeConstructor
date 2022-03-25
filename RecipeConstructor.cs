using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Diagnostics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.ModLoader.Config.UI;
using Terraria.UI;
using Terraria.UI.Chat;
using System.IO;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ModLoader.IO;
using Terraria.Localization;
using Terraria.Utilities;
using System.Reflection;
using MonoMod.RuntimeDetour.HookGen;
using Microsoft.Xna.Framework.Audio;
using Terraria.Audio;
using Terraria.Graphics.Capture;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;
using ReLogic.Graphics;
using System.Runtime;
using Microsoft.Xna.Framework.Input;
using Terraria.Graphics.Shaders;

namespace RecipeConstructor
{
	public class RecipeConstructor : Mod
	{
		public override void AddRecipes() {
			if (MyConfig.get == null) {
				Logger.InfoFormat("Error loading recipes, ignoring custom recipes");
				return;
			}
			if (MyConfig.get.itemRequirement == null) {
				Logger.InfoFormat("Error loading recipes, ignoring custom recipes");
				return;
			}
			if (MyConfig.get.itemRequirement.Count < 1)  {
				Logger.InfoFormat("No item requirement found");
				return;
			}
		}
		[Label("Recipe Constructor")]
		public class MyConfig : ModConfig
		{
			public static void SaveConfig(){
				typeof(ConfigManager).GetMethod("Save", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[1] { get });
			}

			public override ConfigScope Mode => ConfigScope.ServerSide;
			public static MyConfig get => ModContent.GetInstance<MyConfig>();

			// dictionary doom
			[ReloadRequired]
			public List<Dictionary<ItemDefinition, int>> itemRequirement = new List<Dictionary<ItemDefinition, int>>();
			public List<List<ItemDefinition>> tileRequirement = new List<List<ItemDefinition>>();
			public List<Dictionary<ItemDefinition, int>> result = new List<Dictionary<ItemDefinition, int>>();
		}
	}
}