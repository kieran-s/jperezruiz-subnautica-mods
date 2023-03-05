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
            var originalValue = __instance.motorModePowerConsumption[(int)__instance.cyclopsMotorMode];
            var newValue = originalValue;
            var speedModuleInstalled = CyclopsSpeedModuleUtils.GetInstalled(__instance.subRoot.upgradeConsole);
            newValue += newValue * speedModuleInstalled.PowerConsumptionMultiplier;
            __result = newValue;
            return false;
        }
    }
}