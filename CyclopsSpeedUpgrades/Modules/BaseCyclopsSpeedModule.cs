using UnityEngine;
using ModHelpers.Modules;

namespace CyclopsSpeedUpgrades.Modules
{

    /**
     * Base for the CyclopsSpeedModules
     */
    public abstract class BaseCyclopsSpeedModule: BaseCyclopsLeveledUpgradeModule
    {
        public abstract float ForwardSpeedMultiplier { get; }
        public abstract float VerticalSpeedMultiplier { get; }
        public abstract float TurningTorqueMultiplier { get; }
        public abstract float PowerConsumptionMultiplier { get; }
        public BaseCyclopsSpeedModule(int upgradeLevel, string description) : base
        (
            "CyclopsSpeedModuleMK",
            "Cyclops Speed Module MK",
            upgradeLevel,
            description
        )
        {}

        public override TechType GetTechTypeToCopy()
        {
            return TechType.CyclopsShieldModule;
        }

        protected override Texture2D LoadUpgradeConsoleHUDSprite()
        {
           return GetItemSprite().texture;
        }

        protected override Atlas.Sprite GetItemSprite()
        {
            var i = base.GetItemSprite();
            Main.Log.LogInfo($"Icon {IconFileName}, assets {AssetsFolder}");
            return i;
        }
    }
}