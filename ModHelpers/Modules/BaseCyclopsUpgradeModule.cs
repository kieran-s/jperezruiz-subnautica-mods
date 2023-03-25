using UnityEngine;

namespace ModHelpers.Modules
{
    /**
     * Base for the Cyclops upgrades
     */
    public abstract class BaseCyclopsUpgradeModule : BaseVehicleUpgradeModule
    {
        public override EquipmentType EquipmentType => EquipmentType.CyclopsModule;

        public override TechType RequiredForUnlock => TechType.Cyclops;

        public override TechGroup GroupForPDA => TechGroup.Cyclops;

        public override TechCategory CategoryForPDA => TechCategory.CyclopsUpgrades;

        public override CraftTree.Type FabricatorType => CraftTree.Type.CyclopsFabricator;

        public override string[] StepsToFabricatorTab => new[] { "CyclopsModules" };

        public override QuickSlotType QuickSlotType => QuickSlotType.Passive;

        protected BaseCyclopsUpgradeModule(string name, string friendlyName, string description) : base
            (name, friendlyName, description)
        {
        }
        /**
         * Load the texture to use as icon in the UpgradeConsoleHUD
         */
        protected abstract Texture2D LoadUpgradeConsoleHUDSprite();
        /**
         * Get the Unity Sprite for the UpgradeConsoleHUD
         */

        public virtual Sprite GetUpgradeConsoleHUDSprite()
        {
            var texture = LoadUpgradeConsoleHUDSprite();
            var sprite = Sprite.Create
            (
                texture,
                new Rect(0.0f, 0.0f, texture.width, texture.height),
                new Vector2(texture.width * 0.5f, texture.height * 0.5f)
            );
            return sprite;
        }
    }
}