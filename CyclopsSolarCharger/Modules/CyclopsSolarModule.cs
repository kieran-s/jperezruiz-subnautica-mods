using ModHelpers.Modules;
using System.Collections.Generic;
using UnityEngine;
using SMLHelper.V2.Crafting;

namespace CyclopsSolarCharger.Modules
{
    
    /**
     * Based on https://github.com/MrPurple6411/MrPurple6411-Subnautica-Mods/blob/main/SeamothThermal/Modules/SeamothThermalModule.cs
     * Thanks!
     */
    public class CyclopsSolarModule: BaseCyclopsUpgradeModule
    {
        public CyclopsSolarModule() : base
        (
            "CyclopsSolarModule",
            "Cyclops solar charger",
            "Recharge power cells with sun. Can be stack."
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
                    new(TechType.AdvancedWiringKit, 1),
                    new(TechType.EnameledGlass, 1)
                }
            };
        }

        public override TechType GetTechTypeToCopy()
        {
            return TechType.SeamothSolarCharge;
        }

        protected override Texture2D LoadUpgradeConsoleHUDSprite()
        {
            return GetItemSprite().texture;
        }
    }
}