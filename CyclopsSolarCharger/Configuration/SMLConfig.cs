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
        [Slider(label: "Max depth", min: MaxDepthMinValue, max: MaxDepthMaxValue, Step = 5, DefaultValue = 200, Tooltip = "The maximum depth at which the solar panels operate. At greater depths the cyclops will not recharge.")]
        public float MaxDepthOption = 200;
        [Slider(label: "Max modules stack", min: MaxStackMinValue, max: MaxStackMaxValue, Step = 1, DefaultValue = 3, Tooltip = "The maximum number of modules that can be installed. Each module provides increases by 100% the generation, so with 2 modules the charge will be double. The charge generation has been balanced for 3 modules.")]
        public int MaxStacksOption = 3;
    }
}

