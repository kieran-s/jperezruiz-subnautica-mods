using System.Collections.Generic;
using BetterScannerRoomUpgrades.Configuration;
using HarmonyLib;
using UnityEngine;

namespace BetterScannerRoomUpgrades.Patches
{
    /**
     * Patch uGUI_ResourceTracker in charge of rendering the blips for
     * the scanned resources
     */
    [HarmonyPatch(typeof(uGUI_ResourceTracker))]
    public class uGUI_ResourceTrackerPatcher
    {
        [HarmonyPatch(nameof(uGUI_ResourceTracker.GatherAll))]
        [HarmonyPrefix]
        public static bool GatherAll_Prefix(uGUI_ResourceTracker __instance)
        {
            var camera = MainCamera.camera;
            __instance.nodes.Clear();
            __instance.techTypes.Clear();
            ResourceTrackerDatabase.GetTechTypesInRange(camera.transform.position, Main.s_modConfig.MaxRange, (ICollection<TechType>) __instance.techTypes);
            for (int index = 0; index < __instance.techTypes.Count; ++index)
            {
                TechType techType = __instance.techTypes[index];
                ResourceTrackerDatabase.GetNodes(camera.transform.position, Main.s_modConfig.MaxRange, techType, (ICollection<ResourceTrackerDatabase.ResourceInfo>) __instance.nodes);
            }

            return false;
        }

        [HarmonyPatch(nameof(uGUI_ResourceTracker.GatherScanned))]
        [HarmonyPrefix]
        public static bool GatherScanned_Prefix(uGUI_ResourceTracker __instance)
        {
            Camera camera = MainCamera.camera;
            __instance.nodes.Clear();
            __instance.mapRooms.Clear();
            MapRoomScreen screen = uGUI_CameraDrone.main.GetScreen();
            if ((UnityEngine.Object) screen != (UnityEngine.Object) null)
                __instance.mapRooms.Add(screen.mapRoomFunctionality);
            else
                MapRoomFunctionality.GetMapRoomsInRange(camera.transform.position, Main.s_modConfig.MaxRange, (ICollection<MapRoomFunctionality>) __instance.mapRooms);
            for (int index = 0; index < __instance.mapRooms.Count; ++index)
            {
                if (__instance.mapRooms[index].GetActiveTechType() != TechType.None)
                    __instance.mapRooms[index].GetDiscoveredNodes((ICollection<ResourceTrackerDatabase.ResourceInfo>) __instance.nodes);
            }

            return false;
        }
    }
}