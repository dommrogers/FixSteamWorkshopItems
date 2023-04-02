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
			string text = "";
			DirectoryInfo directoryInfo = Directory.GetParent(Application.dataPath);
			if (directoryInfo != null)
			{
				directoryInfo = directoryInfo.Parent;
			}
			if (directoryInfo != null)
			{
				directoryInfo = directoryInfo.Parent;
			}
			if (directoryInfo != null)
			{
				text = directoryInfo.FullName + "/workshop/content/305620";
			}
			MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization " + text);
			if (Directory.Exists(text))
			{
				MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization exists");
				string[] files = Directory.GetFiles(text, "*.csv", SearchOption.AllDirectories);
				for (int i = 0; i < files.Length; i++)
				{
					MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization " + files[i]);
					Il2CppSystem.Collections.Generic.List<string> entries = Localization.s_WorkshopStringTable.ImportCsv(files[i], null);
					MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization " + files[i] + " | " + entries.Count);
				}
				foreach (string text2 in Localization.s_WorkshopStringTable.GetLanguages())
				{
					MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization each " + text2);
					if (!Localization.s_Languages.Contains(text2))
					{
						MelonLoader.MelonLogger.Warning("LoadWorkshopLocalization add " + text2);
						Localization.s_Languages.Add(text2);
					}
				}
			}

		}
	}

}
