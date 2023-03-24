namespace CyclopsSolarCharger.Configuration
{
    using SMLHelper.V2.Json;
    using SMLHelper.V2.Options.Attributes;
    /**
     * Class to generate and handle the configuration using SML
     */
    [Menu("Cyclops Solar Charger Options")]
    public class SMLConfig : ConfigFile
    {
        /**
         * Used for validation purposes, to avoid higher values
         */
        public const float MaxDepthMaxValue = 800;

        public const float MaxDepthMinValue = 0;
        /**
         * Used for validation purposes, to avoid higher values
         */
        public const float MaxStackMaxValue = 6;

        public const float MaxStackMinValue = 1;
        public const float MaxChargeMultiplier = 3;
        public const float MinChargeMultiplier = 0.5f;
        [Slider(label: "Max depth", min: MaxDepthMinValue, max: MaxDepthMaxValue, Step = 5, DefaultValue = 200, Tooltip = "The maximum depth at which the solar panels operate. At greater depths the cyclops will not recharge.")]
        public float MaxDepthOption = 200;
        [Slider(label: "Max modules stack", min: MaxStackMinValue, max: MaxStackMaxValue, Step = 1, DefaultValue = 3, Tooltip = "The maximum number of modules that can be installed. Each module provides increases by 100% the generation, so with 2 modules the charge will be double. The charge generation has been balanced for 3 modules.")]
        public int MaxStacksOption = 3;
        [Slider(label: "Recharge rate multiplier", min: MinChargeMultiplier, max: MaxChargeMultiplier, Step = 0.5f, DefaultValue = 1, Tooltip = "Multiplier for the recharge rate. By default 1, if configured to 2, the recharge will be twice faster. If configured to 0.5 will reduce the charging rate by half.")]
        public int RechargeRateMultiplier = 1;
    }
}

