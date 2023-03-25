using System;
using System.Linq;
namespace ModHelpers.Modules
{

    /**
     * Base for Cyclops leveled upgrades.
     * Useful to create upgrades that scales, ex MKI, MKII, and so on
     */
    public abstract class BaseCyclopsLeveledUpgradeModule: BaseCyclopsUpgradeModule
    {
        /**
         * The level of the upgrade.
         */
        public new readonly int UpgradeLevel;
        /**
         * The upgradeLevel is concatenated to upgradeBaseName and firendlyBaseName as I.
         * For example:
         * upgradeBaseName: MySuperUpgradeMK
         * upgradeLevel = 2
         *
         * name will be MySuperUpgradeMKII
         *
         * The same applies for the icons. By default "mk" + "i"*upgradeLevel
         * So, if upgradeLevel = 2, the name of the icon will be "mkii.png"
         */
        protected BaseCyclopsLeveledUpgradeModule(
            string upgradeBaseName,
            string friendlyBaseName,
            int upgradeLevel,
            string description
        ) : base
        (
            upgradeBaseName+String.Concat(Enumerable.Repeat("I", upgradeLevel)),
            friendlyBaseName + String.Concat(Enumerable.Repeat("I", upgradeLevel)),
            description
        )
        {
            UpgradeLevel = upgradeLevel;
        }
    }
}