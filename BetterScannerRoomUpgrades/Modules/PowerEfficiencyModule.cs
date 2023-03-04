using SMLHelper.V2.Json;

namespace BetterScannerRoomUpgrades.Modules
{
    using System.Collections.Generic;
    using UnityEngine;
    using System.Collections;
    using SMLHelper.V2.Assets;
    using SMLHelper.V2.Crafting;
    /**
     * Based on https://github.com/MrPurple6411/MrPurple6411-Subnautica-Mods/blob/main/SeamothThermal/Modules/SeamothThermalModule.cs
     * Thanks!
     */
    public class PowerEfficiencyModule: Equipable
    {
        private const string DescriptionTemplate =
            "Reduces the energy consumed by the scanner room by {percentage}%. Can be stacked";
        public PowerEfficiencyModule() : base
        (
            "ScannerRoomPowerEfficiency",
            "Scanner room power efficiency",
            DescriptionTemplate
        )
        {
            Description = DescriptionTemplate.Replace
                ("{percentage}", Main.s_modConfig.PowerEfficiencyPerModule.ToString());
            Main.s_modConfig.OnFinishedLoading += OnConfigChange;

        }

        private void OnConfigChange(object sender, ConfigFileEventArgs eventData)
        {
            Description = DescriptionTemplate.Replace
                ("{percentage}", Main.s_modConfig.PowerEfficiencyPerModule.ToString());
        }

        public override EquipmentType EquipmentType => EquipmentType.None;

        public override TechType RequiredForUnlock => TechType.BaseMapRoom;

        public override TechGroup GroupForPDA => TechGroup.MapRoomUpgrades;

        public override TechCategory CategoryForPDA => TechCategory.MapRoomUpgrades;

        public override CraftTree.Type FabricatorType => CraftTree.Type.MapRoom;

        public override string[] StepsToFabricatorTab => new[] { "MapRoom" };

        public override QuickSlotType QuickSlotType => QuickSlotType.Passive;
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var taskResult = new TaskResult<GameObject>();
            yield return CraftData.InstantiateFromPrefabAsync(TechType.MapRoomUpgradeScanRange, taskResult);
            var obj = taskResult.Get();
            var prefabIdentifier = obj.GetComponent<PrefabIdentifier>();

            // Change them so they fit to our requirements.
            prefabIdentifier.ClassId = ClassID;
            gameObject.Set(obj);
        }
        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                // same recipe has seamoth solar charge
                Ingredients = new List<Ingredient>()
                {
                    new(TechType.ComputerChip, 1),
                    new(TechType.EnameledGlass, 1)
                }
            };
        }
        protected override Atlas.Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.PowerUpgradeModule);
        }
        
    }
}