using System.Collections.Generic;
using SMLHelper.V2.Crafting;
using Sprite = Atlas.Sprite;
namespace CyclopsSpeedUpgrades.Modules
{
    public class CyclopsSpeedModuleMKIII: BaseCyclopsSpeedModule
    {
        public override float SpeedMultiplier => 4;

        public override float PowerConsumptionMultiplier => 1f;

        public CyclopsSpeedModuleMKIII() : base
        (
            3,
            "Crazily increase the speed of the cyclops at the cost of a much higher power consumption. Not stackable"
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
                    new(Main.CyclopsSpeedModuleMKII.TechType, 1),
                    new(TechType.Kyanite, 1),
                    new(TechType.PrecursorIonCrystal, 1)
                }
            };
        }
        protected override Sprite GetItemSprite()
        {
            return LoadSprite("mkiii.png");
        }
    }
}