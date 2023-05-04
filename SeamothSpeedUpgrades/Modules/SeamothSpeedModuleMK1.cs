using System.Collections.Generic;
using SMLHelper.V2.Crafting;
using Sprite = Atlas.Sprite;
namespace SeamothSpeedUpgrades.Modules
{

    public class SeamothSpeedModuleMK1: BaseSeamothSpeedModule
    {
        public override float SpeedMultiplier => 2f;
        public override float PowerConsumptionMultiplier => 0.25f;
        

        public SeamothSpeedModuleMK1() : base
        (
            1,
            "Increase the speed of the SeaMoth at the cost of power consumption. Not stackable"
        )
        {
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
                    new(TechType.Battery, 1),
                    new(TechType.Lubricant, 1),
                }
            };
        }
        protected override Sprite GetItemSprite()
        {
            return LoadSprite("mk1.png");
        }
    }
}