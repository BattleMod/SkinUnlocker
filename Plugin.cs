using BepInEx;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;

namespace SkinUnlocker;

[BepInPlugin("link.ryhn.battlemod.skinunlocker", "Skin Unlocker", "1.0.0.0")]
public class Plugin : BasePlugin
{
	public static Plugin Instance;

	public override void Load()
	{
		Instance = this;
		Log.LogInfo($"Plugin {MyPluginInfo.PLUGIN_GUID} is loaded!");
		var h = new Harmony(MyPluginInfo.PLUGIN_GUID);
		h.PatchAll();
	}
}

[HarmonyPatch(typeof(PlayerLoadout.Items.AWeaponSkin), "get_Permissions")]
public class SkinUnlocker
{
	[HarmonyPostfix]
	public static void Postfix(PlayerLoadout.Items.AWeaponSkin __instance, Avaibility __result)
	{
		__instance.RankRequired = 0;
		__instance.KillRequired = 0;
		__instance.SkinPermissions = EnumPublicSealedvaNoDeCoVeDrPa12PaPaUnique.None;

		if (__result != null)
		{
			__result.RequiredPermissions = 0;
			__result.RequiredKill = 0;
			__result.RequiredRank = 0;
			__result.RequiredPrestige = 0;
			__result.UnlockableRequirment = null;
			__result.AchievementRequirment = null;
			__result.InventoryAnd = null;
			__result.InventoryOr = null;
			__result.AllowedNation = new AvailableNations()
			{
				USA = true,
				RUS = true,
				TER = true
			};
			__result.AllowedClassRoles = new AvailableRoles
			{
				LeaderAllowed = true,
				AssaultAllowed = true,
				MedicAllowed = true,
				EngineerAllowed = true,
				SupportAllowed = true,
				ReconAllowed = true
			};
		}
	}
}

[HarmonyPatch(typeof(PlayerLoadout.Items.AWeapon), "Method_Public_AWeaponSkin_Int32_Boolean_0")]
public class SkinUnlockerName
{
	[HarmonyPostfix]
	public static void Postfix(PlayerLoadout.Items.AWeapon __instance, PlayerLoadout.Items.AWeaponSkin __result)
	{
		Plugin.Instance.Log.LogInfo("");
		Plugin.Instance.Log.LogInfo("Display:" + __result.DisplayName);
		Plugin.Instance.Log.LogInfo("Full Display:" + __result.FullDisplayName);
		Plugin.Instance.Log.LogInfo("Camo:" + __result.CamoName);
		Plugin.Instance.Log.LogInfo("Drop:" + __result.DropName);
	}
}