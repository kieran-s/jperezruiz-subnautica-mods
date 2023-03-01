namespace CyclopsSolarCharger.Modules
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
    public class CyclopsSolarModule: Equipable
    {
        public CyclopsSolarModule() : base
        (
            "CyclopsSolarModule",
            "Cyclops solar charger",
            "Recharge power cells with sun. Can be stack."
        )
        {
        }
        public override EquipmentType EquipmentType => EquipmentType.CyclopsModule;

        public override TechType RequiredForUnlock => TechType.Cyclops;

        public override TechGroup GroupForPDA => TechGroup.VehicleUpgrades;

        public override TechCategory CategoryForPDA => TechCategory.VehicleUpgrades;

        public override CraftTree.Type FabricatorType => CraftTree.Type.CyclopsFabricator;

        public override string[] StepsToFabricatorTab => new[] { "CyclopsModules" };

        public override QuickSlotType QuickSlotType => QuickSlotType.Passive;
        public override IEnumerator GetGameObjectAsync(IOut<GameObject> gameObject)
        {
            var taskResult = new TaskResult<GameObject>();
            yield return CraftData.InstantiateFromPrefabAsync(TechType.SeamothSolarCharge, taskResult);
            var obj = taskResult.Get();
            
            // Get the TechTags and PrefabIdentifiers
            var techTag = obj.GetComponent<TechTag>();
            var prefabIdentifier = obj.GetComponent<PrefabIdentifier>();

            // Change them so they fit to our requirements.
            techTag.type = TechType;
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
                    new(TechType.AdvancedWiringKit, 1),
                    new(TechType.EnameledGlass, 1)
                }
            };
        }
        protected override Atlas.Sprite GetItemSprite()
        {
            return SpriteManager.Get(TechType.SeamothSolarCharge);
        }
    }
}