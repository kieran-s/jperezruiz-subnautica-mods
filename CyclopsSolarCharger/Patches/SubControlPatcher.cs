using System.Collections.Generic;
using HarmonyLib;
using ModHelpers.Components;
using ModHelpers.Modules;

namespace CyclopsSolarCharger.Patches
{
    /**
     * Patch the SubControl component
     */
    [HarmonyPatch(typeof(SubControl))]
    public class SubControlPatcher
    {
        [HarmonyPatch(nameof(SubControl.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix(SubControl __instance)
        {
            __instance.gameObject.EnsureComponent<CyclopsCustomUpgradeModules>();
            var modules = new List<BaseCyclopsUpgradeModule>
            {
                Main.solarModule
            };
            __instance.GetComponent<CyclopsCustomUpgradeModules>()
                      .RegisterModules(modules);
        }
    }
}