using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> GatherAll_Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = new List<CodeInstruction>(instructions);
                for (int i = 0; i < codes.Count; i++)
                {
                    if (codes[i].opcode == OpCodes.Ldc_R4 && (float)codes[i].operand == 500)
                    {
                        codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(uGUI_ResourceTrackerPatcher), nameof(uGUI_ResourceTrackerPatcher.GetMaxRange))));
                        i++;
                    }
                }

                return codes.AsEnumerable();
            }
            [HarmonyPatch(nameof(uGUI_ResourceTracker.GatherScanned))]
            [HarmonyTranspiler]
            public static IEnumerable<CodeInstruction> GatherScanned_Transpiler(IEnumerable<CodeInstruction> instructions)
            {
                var codes = new List<CodeInstruction>(instructions);
                for (int i = 0; i < codes.Count; i++)
                {
                    if (codes[i].opcode == OpCodes.Ldc_R4 && (float)codes[i].operand == 500)
                    {
                        codes.Insert(i + 1, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(uGUI_ResourceTrackerPatcher), nameof(uGUI_ResourceTrackerPatcher.GetMaxRange))));
                        i++;
                    }
                }

                return codes.AsEnumerable();
            }
            public static float GetMaxRange(float maxRange)
            {
                return Main.s_modConfig.MaxRange;
            }
    }
}
