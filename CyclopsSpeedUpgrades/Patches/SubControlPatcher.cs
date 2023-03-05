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
        public static void FixedUpdate_Prefix(SubControl __instance, ref float __state)
        {
            if (!__instance.LOD.IsFull() || __instance.powerRelay.GetPowerStatus() == PowerSystem.Status.Offline)
                return;
            // we need to restore the BaseForwardAccel at the end
            var originalSpeed = __instance.BaseForwardAccel;
            // pass to the postfix the original value
            __state = originalSpeed;
            var speedModuleInstalled = CyclopsSpeedModuleUtils.GetInstalled(__instance.sub.upgradeConsole);
            float speedMultiplier = speedModuleInstalled != null ? speedModuleInstalled.SpeedMultiplier : 1f;
            __instance.BaseForwardAccel *= speedMultiplier;
        }
        [HarmonyPatch(nameof(SubControl.FixedUpdate))]
        [HarmonyPostfix]
        public static void FixedUpdate_Postfix(SubControl __instance, float __state)
        {
            if (!__instance.LOD.IsFull() || __instance.powerRelay.GetPowerStatus() == PowerSystem.Status.Offline)
                return;
            __instance.BaseForwardAccel = __state;
        }
    }
}