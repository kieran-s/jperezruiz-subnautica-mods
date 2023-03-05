using System.Linq;
using CyclopsSpeedUpgrades.Modules;

namespace CyclopsSpeedUpgrades.Utils
{
    /**
     * Utils for the mod
     */
    public class CyclopsSpeedModuleUtils
    {
        /**
         * Get the greater cyclops speed module installed, if there is anyone
         */
        public static BaseCyclopsSpeedModule GetInstalled(UpgradeConsole upgradeConsole)
        {
            var installedSpeedModules =
                upgradeConsole.modules.equipment.Values
                              .Where(m => m != null && m.item != null)
                              .Select
                              (
                                  item => Main.CyclopsSpeedModules.Find(m => m.TechType == item.techType)
                              )
                              .Where(m => m != null)
                              .OrderByDescending
                              (
                                  item => item.UpgradeLevel
                              )
                              .ToList();
            return installedSpeedModules.FirstOrDefault();
        }
    }
}