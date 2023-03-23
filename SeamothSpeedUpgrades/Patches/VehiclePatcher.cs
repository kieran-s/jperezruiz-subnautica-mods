using HarmonyLib;
using SeamothSpeedUpgrades.Utils;
using UnityEngine;

namespace SeamothSpeedUpgrades.Patches
{
    /**
     * 
     */
    [HarmonyPatch(typeof(Vehicle))]
    public class VehiclePatcher
    {
        [HarmonyPatch(nameof(Vehicle.ApplyPhysicsMove))]
        [HarmonyPrefix]
        public static void ApplyPhysicsMove_Prefix(Vehicle __instance, ref (float forwardForce, float backwardForce, float sidewardForce) __state)
        {
            /* to ensure compatibility with other mods that affects vehicle speed
             * we are going to patch the speed in the ApplyPhysicsMove method,
             * that is the one making the seamoth move.
             * We are going to get the vehicle speed, apply the multiplier depending on the upgrades
             * and then restore them in the postfix.
             * This way we probably wont't interfere with other mods.
             * The way to get the best performance would be by patching the OnUpgradeModuleChange method
             * but that would cause problems for example with SeamothSprint mod
             */
            __state.backwardForce = __instance.backwardForce;
            __state.forwardForce = __instance.forwardForce;
            __state.sidewardForce = __instance.sidewardForce;
            var speedModuleInstalled = SeamothSpeedModuleUtils.GetInstalled(__instance.modules);
            float speedMultiplier = speedModuleInstalled != null ? speedModuleInstalled.SpeedMultiplier : 1f;
            __instance.backwardForce *= speedMultiplier;
            __instance.forwardForce *= speedMultiplier;
            __instance.sidewardForce *= speedMultiplier;
        }
        [HarmonyPatch(nameof(Vehicle.ApplyPhysicsMove))]
        [HarmonyPostfix]
        public static void ApplyPhysicsMove_Postfix(Vehicle __instance, ref (float forwardForce, float backwardForce, float sidewardForce) __state)
        {
            __instance.backwardForce = __state.backwardForce;
            __instance.forwardForce = __state.forwardForce;
            __instance.sidewardForce = __state.sidewardForce;
        }

        [HarmonyPatch(nameof(Vehicle.ConsumeEngineEnergy))]
        [HarmonyPrefix]
        public static void ConsumeEngineEnergy_Prefix(Vehicle __instance, ref float energyCost)
        {
            var speedModuleInstalled = SeamothSpeedModuleUtils.GetInstalled(__instance.modules);
            if (speedModuleInstalled != null)
            {
                float originalEnergyCost = energyCost * 1f;
                float newEnergyCost = originalEnergyCost + (originalEnergyCost*speedModuleInstalled.PowerConsumptionMultiplier);
                energyCost = newEnergyCost;
            }
        }
    }
}