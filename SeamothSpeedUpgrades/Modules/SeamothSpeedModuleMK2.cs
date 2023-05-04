using System.Collections.Generic;
using SMLHelper.V2.Crafting;
using Sprite = Atlas.Sprite;
namespace SeamothSpeedUpgrades.Modules
{
    public class SeamothSpeedModuleMK2: BaseSeamothSpeedModule
    {

        public SeamothSpeedModuleMK2() : base
        (
            2,
            "Greatly increase the speed of the SeaMoth at the cost of a higher power consumption. Not stackable"
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
                    new(Main.SeamothSpeedModuleMk1.TechType, 1),
                    new(TechType.WiringKit, 1),
                    new(TechType.PowerCell, 1),
                    new(TechType.Magnetite, 1)
                }
            };
        }
        protected override Sprite GetItemSprite()
        {
            return LoadSprite("mk2.png");
        }
    }
}