using CyclopsSolarCharger.Configuration;
using HarmonyLib;
using UnityEngine;

namespace CyclopsSolarCharger.Patches
{
    [HarmonyPatch(typeof(SubRoot), nameof(SubRoot.Update))]
    public class SubRootUpdatePatcher
    {
        private static void Prefix(SubRoot __instance)
        {
            if (!__instance.LOD.IsMinimal())
            {
                DayNightCycle main = DayNightCycle.main;
                // check if cyclops is ready and if current daytime is day
                if (
                    __instance.upgradeConsole != null
                    && __instance.upgradeConsole.modules != null
                    && main != null
                    && main.IsDay()
                )
                {
                    // get the num of CyclopsSolarCharger installed
                    var numUpgrades = Mathf.Clamp
                    (
                        __instance.upgradeConsole.modules.GetCount(Main.solarModule.TechType),
                        0,
                        SMLConfig.MaxStackMinValue
                    );
                    if (numUpgrades > 0)
                    {
                        var maxDepth = Mathf.Clamp
                        (
                            Main.s_modConfig.MaxDepthOption,
                            SMLConfig.MaxDepthMinValue,
                            SMLConfig.MaxDepthMaxValue
                        );
                        // get a value between 0 and 1 depending on the depth
                        var depth = Mathf.Clamp01((maxDepth + __instance.transform.position.y) / maxDepth);
                        // calculate the amount of energy to charge between 2 numbers (max 0.037, min 0.007) depending on the depth
                        // so if depth = 0 (surface) max charge power
                        // I have obtained these values by trial and error, but i think they are balanced
                        float energy = depth == 0 ? 0 : Mathf.Clamp(depth * 0.05f, 0.007f, 0.05f);
                        // stack up to 3 upgrades
                        float toCharge = energy * Mathf.Clamp(numUpgrades, 0, Main.s_modConfig.MaxStacksOption);
                        float rechargeRate = Mathf.Clamp
                        (
                            Main.s_modConfig.RechargeRateMultiplier,
                            SMLConfig.MinChargeMultiplier,
                            SMLConfig.MaxChargeMultiplier
                        );
                        toCharge *= rechargeRate;
                        __instance.powerRelay.AddEnergy(toCharge, out float _);
                    }
                }
            }
        }
    }
}