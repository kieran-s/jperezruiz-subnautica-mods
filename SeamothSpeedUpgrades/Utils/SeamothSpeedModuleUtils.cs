using System.Linq;
using SeamothSpeedUpgrades.Modules;

namespace SeamothSpeedUpgrades.Utils
{
    /**
     * Utils for the mod
     */
    public class SeamothSpeedModuleUtils
    {
        /**
         * Get the greater SeaMoth speed module installed, if there is anyone
         */
        public static BaseSeamothSpeedModule GetInstalled(Equipment modules)
        {
            var installedSpeedModules =
                modules.equipment.Values
                       .Where(m => m != null && m.item != null)
                       .Select
                       (
                           item => Main.SeamothSpeedModules.Find(m => m.TechType == item.techType)
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