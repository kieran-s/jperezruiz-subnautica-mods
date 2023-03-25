using System.Collections.Generic;
using SMLHelper.V2.Crafting;
using Sprite = Atlas.Sprite;
namespace CyclopsSpeedUpgrades.Modules
{
    public class CyclopsSpeedModuleMKIIII: BaseCyclopsSpeedModule
    {
        public override float ForwardSpeedMultiplier => 6;
        public override float VerticalSpeedMultiplier => 1.5f;
        public override float TurningTorqueMultiplier => 1.4f;

        public override float PowerConsumptionMultiplier => 2f;

        public CyclopsSpeedModuleMKIIII() : base
        (
            4,
            "Increases speed of the cyclops in an incredibly crazy way at the cost of an absurd power consumption. Not stackable"
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
                    new(Main.CyclopsSpeedModuleMKIII.TechType, 1),
                    new(TechType.PrecursorIonBattery, 1),
                    new(TechType.Kyanite, 1)
                }
            };
        }
    }
}