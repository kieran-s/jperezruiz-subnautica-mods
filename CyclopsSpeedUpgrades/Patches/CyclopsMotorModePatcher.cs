using HarmonyLib;
using CyclopsSpeedUpgrades.Utils;

namespace CyclopsSpeedUpgrades.Patches
{
    /**
     * Patch the CyclopsMotorModePatcher, in charge of handling the power consumption of the cyclops
     */
    [HarmonyPatch(typeof(CyclopsMotorMode))]
    public class CyclopsMotorModePatcher
    {
        [HarmonyPatch(nameof(CyclopsMotorMode.GetPowerConsumption))]
        [HarmonyPrefix]
        public static bool GetPowerConsumption_Prefix(CyclopsMotorMode __instance, ref float __result)
        {
            var speedModuleInstalled = CyclopsSpeedModuleUtils.GetInstalled(__instance.subRoot.upgradeConsole);
            var originalValue = __instance.motorModePowerConsumption[(int)__instance.cyclopsMotorMode];
            var newValue = speedModuleInstalled != null
                ? originalValue + (originalValue * speedModuleInstalled.PowerConsumptionMultiplier)
                : originalValue;
            __result = newValue;
            return false;
        }
    }
}