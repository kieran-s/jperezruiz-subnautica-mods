using System.Collections.Generic;
using SMLHelper.V2.Crafting;
using Sprite = Atlas.Sprite;
namespace CyclopsSpeedUpgrades.Modules
{
    public class CyclopsSpeedModuleMKII: BaseCyclopsSpeedModule
    {
        public override float ForwardSpeedMultiplier => 3f;
        public override float VerticalSpeedMultiplier => 1.3f;
        public override float TurningTroqueMultiplier => 1.2f;
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
                // same recipe has seamoth solar charge
                Ingredients = new List<Ingredient>()
                {
                    new(Main.CyclopsSpeedModuleMKI.TechType, 1),
                    new(TechType.AdvancedWiringKit, 1),
                    new(TechType.Aerogel, 1)
                }
            };
        }
        protected override Sprite GetItemSprite()
        {
            return LoadSprite("mkii.png");
        }
    }
}