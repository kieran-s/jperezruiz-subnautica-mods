using System.Collections.Generic;
using SMLHelper.V2.Crafting;
using Sprite = Atlas.Sprite;
namespace CyclopsSpeedUpgrades.Modules
{

    public class CyclopsSpeedModuleMKI: BaseCyclopsSpeedModule
    {
        public override float ForwardSpeedMultiplier => 2f;
        public override float VerticalSpeedMultiplier => 1f;
        public override float TurningTorqueMultiplier => 1f;
        public override float PowerConsumptionMultiplier => 0.3f;

        public CyclopsSpeedModuleMKI() : base
        (
            1,
            "Increase the speed of the cyclops at the cost of power consumption. Not stackable"
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
                    new(TechType.Magnetite, 1),
                    new(TechType.Lubricant, 1)
                }
            };
        }
    }
}