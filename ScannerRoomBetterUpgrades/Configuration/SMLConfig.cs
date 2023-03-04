namespace BetterScannerRoomUpgrades.Configuration
{
    using SMLHelper.V2.Json;
    using SMLHelper.V2.Options.Attributes;
    /**
     * Class to generate and handle the configuration using SML
     */
    [Menu("Better Scanner Room Upgrades Options")]
    public class SMLConfig : ConfigFile
    {
        /**
         * Max range for the scanner. More than 1000m could cause problems
         */
        public const float MaxRange = 1000;
        /**
         * Min interval in seconds between scan blips. Less than 0.5f (half second) could cause problems.
         */
        public const float MinScanInterval = 1f;
       
        [Slider(label: "Scan interval per module", min: 2, max: 14, Step = 1, DefaultValue = 4, Tooltip = "Number of seconds to reduce the scanning interval for each upgrade installed.")]
        public float SpeedPerModule = 4;
        [Slider(label: "Range increase per module", min: 50, max: 1000, Step = 100, DefaultValue = 200, Tooltip = "The number of meters to increase the scan range for each upgrade installed.")]
        public int RangePerModule = 200;
        [Slider(label: "Default range", min: 300, max: 1000, Step = 100, DefaultValue = 600, Tooltip = "The default scan range with no modules installed.")]
        public int DefaultRange = 600;

        [Slider
        (
            label: "Default scan interval",
            min: 1,
            max: 14,
            Step = 1,
            DefaultValue = 9f,
            Tooltip = "The default scan time interval with no upgrades installed."
        )]
        public float DefaultInterval = 9f;
    }
}

