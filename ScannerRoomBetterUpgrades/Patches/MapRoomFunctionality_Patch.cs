using System;
using HarmonyLib;
using BetterScannerRoomUpgrades.Configuration;
using BetterScannerRoomUpgrades.MonoBehaviors;
using SMLHelper.V2.Json;
using UnityEngine;

namespace BetterScannerRoomUpgrades.Patches
{
    /**
     * Patch the Scanner room component
     */
    [HarmonyPatch(typeof(MapRoomFunctionality))]
    public class MapRoomFunctionality_Patch
    {
        [HarmonyPatch(nameof(MapRoomFunctionality.Start))]
        [HarmonyPostfix]
        private static void Start(MapRoomFunctionality __instance)
        {
            // add the component to update the scanner room when the mod config changes
            __instance.gameObject.AddComponent<MapRoomFunctionalityConfigSyncComponent>();
        }
        /**
         * Patch the mapScale getter. The mapScale controls how big the holo map
         * is going to be, the holo map is rendered based on the maximum scanning range
         * and because we are increasing from the max default of 500 to 1000m the holo map
         * is overflowing and could be seen from outside the scanner room :D
         * So, patch the mapScale getter to calculate the scale with the MaxRange we allow
         */
        [HarmonyPatch(nameof(MapRoomFunctionality.mapScale), MethodType.Getter)]
        [HarmonyPrefix]
        public static bool MapScale_Prefix(MapRoomFunctionality __instance, ref float __result)
        {
            __result = __instance.hologramRadius / SMLConfig.MaxRange;
            return false;
        }
        /**
         * Patch the UpdateScanRangeAndInterval method. This method controls
         * the range and the speed of the scanning. Because the max range and interval
         * are hardcoded in subnautica we need to completely override the default method.
         * The code is very similar to the original subnautica one but with
         * our configuration
         */
        [HarmonyPatch(nameof(MapRoomFunctionality.UpdateScanRangeAndInterval))]
        [HarmonyPrefix]
        private static bool UpdateScanRangeAndInterval_Prefix(MapRoomFunctionality __instance)
        {
            float scanRange = __instance.scanRange;
            // the amount of range upgrades installed 
            int numRangeUpgrades = __instance.storageContainer.container.GetCount
                (TechType.MapRoomUpgradeScanRange);
            // the amount of speed upgrades installed
            int numSpeedUpgrades = __instance.storageContainer.container.GetCount
                (TechType.MapRoomUpgradeScanSpeed);
            // get the new range, the max range allowed is SMLConfig.MaxRange
            // if the new range calculated from the number of modules * range increase
            // is greather than the max range, use the max range
            float newRange = Mathf.Min
            (
                SMLConfig.MaxRange,
                Main.s_modConfig.DefaultRange + (numRangeUpgrades * Main.s_modConfig.RangePerModule)
            );
            // get the new interval, the min scan interval is SMLConfig.MinScanInterval
            // if the new interval calculated from the num of upgrades * interval decrease
            // is lower than the minimum interval, use the MinScanInterval
            float newInterval = Mathf.Max
            (
                SMLConfig.MinScanInterval,
                Main.s_modConfig.DefaultInterval - (numSpeedUpgrades * Main.s_modConfig.SpeedPerModule)
            );
            __instance.scanRange = newRange;
            __instance.scanInterval = newInterval;
            if ((double)__instance.scanRange == (double)scanRange)
                return false;
            __instance.ObtainResourceNodes(__instance.typeToScan);
            if (__instance.onScanRangeChanged == null)
                return false;
            __instance.onScanRangeChanged();
            return false;
        }
    }
}