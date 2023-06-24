using HarmonyLib;
using Il2Cpp;
using MelonLoader;
using UnityEngine;

namespace FixSteamWorkshopItems;
internal sealed class Main : MelonMod
{
	public override void OnInitializeMelon()
	{
		//Any initialization code goes here.
		//This method can be deleted if no initialization is required.
	}

	[HarmonyPatch(typeof(Localization), nameof(Localization.LoadWorkshopLocalization))]
	internal static class LoadWorkshopLocalization
	{
		private static void Postfix()
		{
			MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization");
			Localization.s_WorkshopStringTable = ScriptableObject.CreateInstance<StringTable>();
			Localization.s_WorkshopStringTable.SetGeneratedByCode();
			
			var directoryInfo = Directory.GetParent(Application.dataPath);
			var text = directoryInfo?.Parent?.Parent?.FullName + "/workshop/content/305620";
			MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization " + text);

			if (!Directory.Exists(text)) return;
			MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization exists");
			
			var files = Directory.GetFiles(text, "*.csv", SearchOption.AllDirectories);
			foreach (var file in files)
			{
				MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization " + file);
				Il2CppSystem.Collections.Generic.List<string> entries = Localization.s_WorkshopStringTable.ImportCsv(file, null);
				MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization " + file + " | " + entries.Count);
			}
			
			foreach (var text2 in Localization.s_WorkshopStringTable.GetLanguages())
			{
				MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization each " + text2);
				if (Localization.s_Languages.Contains(text2)) continue;
				MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization add " + text2);
				Localization.s_Languages.Add(text2);
			}
		}
	}
}
