using System.Collections.Generic;
using SMLHelper.V2.Crafting;
using Sprite = Atlas.Sprite;
namespace SeamothSpeedUpgrades.Modules
{
    public class SeamothSpeedModuleMK4: BaseSeamothSpeedModule
    {

        public SeamothSpeedModuleMK4() : base
        (
            4,
            "Maybe too much speed increase for the SeaMoth and a serious power consumption. Not stackable"
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
                    new(Main.SeamothSpeedModuleMk3.TechType, 1),
                    new(TechType.AdvancedWiringKit, 1),
                    new(TechType.PrecursorIonPowerCell, 1),
                    new(TechType.Kyanite, 1),
                }
            };
        }
        protected override Sprite GetItemSprite()
        {
            return LoadSprite("mk4.png");
        }
    }
}