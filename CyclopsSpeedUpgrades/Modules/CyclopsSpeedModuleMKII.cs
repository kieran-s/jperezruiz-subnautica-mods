using System.Collections.Generic;
using SMLHelper.V2.Crafting;
using Sprite = Atlas.Sprite;
namespace CyclopsSpeedUpgrades.Modules
{
    public class CyclopsSpeedModuleMKII: BaseCyclopsSpeedModule
    {
        public override float ForwardSpeedMultiplier => 3f;
        public override float VerticalSpeedMultiplier => 1.2f;
        public override float TurningTorqueMultiplier => 1.1f;
        public override float PowerConsumptionMultiplier => 0.6f;

        public CyclopsSpeedModuleMKII() : base
        (
            2,
            "Greatly increase the speed of the cyclops at the cost of a higher power consumption. Not stackable"
        )
        {
        }
        protected override TechData GetBlueprintRecipe()
        {
            return new TechData()
            {
                craftAmount = 1,
                Ingredients = new List<Ingredient>()
                {
                    new(Main.CyclopsSpeedModuleMKI.TechType, 1),
                    new(TechType.WiringKit, 1),
                    new(TechType.Lithium, 1)
                }
            };
        }
    }
}