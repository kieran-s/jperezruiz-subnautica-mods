using System.Linq;
using HarmonyLib;
using CyclopsSpeedUpgrades.Utils;
using ModHelpers.Components;
using ModHelpers.Modules;

namespace CyclopsSpeedUpgrades.Patches
{
    /**
     * Patch the SubControl component, in charge of the movement of the cyclops
     */
    [HarmonyPatch(typeof(SubControl))]
    public class SubControlPatcher
    {
        [HarmonyPatch(nameof(SubControl.Start))]
        [HarmonyPostfix]
        public static void Start_Postfix(SubControl __instance)
        {
            __instance.gameObject.EnsureComponent<CyclopsCustomUpgradeModules>();
            __instance.GetComponent<CyclopsCustomUpgradeModules>().RegisterModules(Main.CyclopsSpeedModules.Cast<BaseCyclopsUpgradeModule>().ToList());
        }
        [HarmonyPatch(nameof(SubControl.FixedUpdate))]
        [HarmonyPrefix]
        public static void FixedUpdate_Prefix(SubControl __instance, ref (float baseForwardAccel, float baseTurningTorque, float baseVerticalAccel) __state)
        {
            if (!__instance.LOD.IsFull() || __instance.powerRelay.GetPowerStatus() == PowerSystem.Status.Offline)
                return;
            // pass to the postfix the original value
            __state.baseForwardAccel = __instance.BaseForwardAccel;
            __state.baseTurningTorque = __instance.BaseTurningTorque;
            __state.baseVerticalAccel = __instance.BaseVerticalAccel;
            var speedModuleInstalled = CyclopsSpeedModuleUtils.GetInstalled(__instance.sub.upgradeConsole);
            float forwardSpeedMultiplier = speedModuleInstalled != null ? speedModuleInstalled.ForwardSpeedMultiplier : 1f;
            float verticalSpeedMultiplier = speedModuleInstalled != null ? speedModuleInstalled.VerticalSpeedMultiplier : 1f;
            float turningTorqueMultiplier = speedModuleInstalled != null ? speedModuleInstalled.TurningTorqueMultiplier : 1f;
            __instance.BaseForwardAccel *= forwardSpeedMultiplier;
            __instance.BaseVerticalAccel *= verticalSpeedMultiplier;
            __instance.BaseTurningTorque *= turningTorqueMultiplier;
        }
        [HarmonyPatch(nameof(SubControl.FixedUpdate))]
        [HarmonyPostfix]
        public static void FixedUpdate_Postfix(SubControl __instance, (float baseForwardAccel, float baseTurningTorque, float baseVerticalAccel) __state)
        {
            if (!__instance.LOD.IsFull() || __instance.powerRelay.GetPowerStatus() == PowerSystem.Status.Offline)
                return;
            __instance.BaseForwardAccel = __state.baseForwardAccel;
            __instance.BaseTurningTorque = __state.baseTurningTorque;
            __instance.BaseVerticalAccel = __state.baseVerticalAccel;
        }
    }
}