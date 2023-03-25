using HarmonyLib;
using CyclopsSpeedUpgrades.Utils;

namespace CyclopsSpeedUpgrades.Patches
{
    /**
     * Patch the SubControl component, in charge of the movement of the cyclops
     */
    [HarmonyPatch(typeof(SubControl))]
    public class SubControlPatcher
    {
        [HarmonyPatch(nameof(SubControl.FixedUpdate))]
        [HarmonyPrefix]
        public static void FixedUpdate_Prefix(SubControl __instance, ref (float baseForwardAccel, float baseTurningTroque, float baseVerticalAccel) __state)
        {
            if (!__instance.LOD.IsFull() || __instance.powerRelay.GetPowerStatus() == PowerSystem.Status.Offline)
                return;
            // pass to the postfix the original value
            __state.baseForwardAccel = __instance.BaseForwardAccel;
            __state.baseTurningTroque = __instance.BaseTurningTorque;
            __state.baseVerticalAccel = __instance.BaseVerticalAccel;
            var speedModuleInstalled = CyclopsSpeedModuleUtils.GetInstalled(__instance.sub.upgradeConsole);
            float forwardSpeedMultiplier = speedModuleInstalled != null ? speedModuleInstalled.ForwardSpeedMultiplier : 1f;
            float verticalSpeedMultiplier = speedModuleInstalled != null ? speedModuleInstalled.VerticalSpeedMultiplier : 1f;
            float turningTroqueMultiplier = speedModuleInstalled != null ? speedModuleInstalled.TurningTroqueMultiplier : 1f;
            __instance.BaseForwardAccel *= forwardSpeedMultiplier;
            __instance.BaseVerticalAccel *= verticalSpeedMultiplier;
            __instance.BaseTurningTorque *= turningTroqueMultiplier;
        }
        [HarmonyPatch(nameof(SubControl.FixedUpdate))]
        [HarmonyPostfix]
        public static void FixedUpdate_Postfix(SubControl __instance, (float baseForwardAccel, float baseTurningTroque, float baseVerticalAccel) __state)
        {
            if (!__instance.LOD.IsFull() || __instance.powerRelay.GetPowerStatus() == PowerSystem.Status.Offline)
                return;
            __instance.BaseForwardAccel = __state.baseForwardAccel;
            __instance.BaseTurningTorque = __state.baseTurningTroque;
            __instance.BaseVerticalAccel = __state.baseVerticalAccel;
        }
    }
}