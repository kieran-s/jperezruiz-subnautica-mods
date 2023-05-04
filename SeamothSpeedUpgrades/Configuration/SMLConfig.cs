namespace SeamothSpeedUpgrades.Configuration
{
    using SMLHelper.V2.Json;
    using SMLHelper.V2.Options.Attributes;

    /**
     * Class to generate and handle the configuration using SML
     */
    [Menu("Seamoth Speed Upgrades Options")]
    public class SMLConfig : ConfigFile
    {
        public const float MinIncrease = 0;
        public const float MaxIncrease = 400;

        [Slider
        (
            label: "MK1 speed increment (%)",
            min: MinIncrease,
            max: MaxIncrease,
            Step = 5,
            Tooltip = "The percentage by which the speed increases when using MK1 upgrade"
        )]
        public float MK1SpeedIncrease = 50;
       
        
        [Slider
        (
            label: "MK2 speed increment (%)",
            min: MinIncrease,
            max: MaxIncrease,
            Step = 5,
            Tooltip = "The percentage by which the speed increases when using MK2 upgrade"
        )]
        public float MK2SpeedIncrease = 75;
        
        [Slider
        (
            label: "MK3 speed increment (%)",
            min: MinIncrease,
            max: MaxIncrease,
            Step = 5,
            Tooltip = "The percentage by which the speed increases when using MK3 upgrade"
        )]
        public float MK3SpeedIncrease = 100;
        
        [Slider
        (
            label: "MK4 speed increment (%)",
            min: MinIncrease,
            max: MaxIncrease,
            Step = 5,
            Tooltip = "The percentage by which the speed increases when using MK4 upgrade"
        )]
        public float MK4SpeedIncrease = 150;

        
        [Slider
        (
            label: "MK1 power consumption increment (%)",
            min: MinIncrease,
            max: MaxIncrease,
            Step = 5,
            Tooltip = "The percentage by which energy consumption is increased when using MK1 upgrade"
        )]
        public float MK1PowerConsumptionIncrease = 15;
        [Slider
        (
            label: "MK2 power consumption increment (%)",
            min: MinIncrease,
            max: MaxIncrease,
            Step = 5,
            Tooltip = "The percentage by which energy consumption is increased when using MK2 upgrade"
        )]
        public float MK2PowerConsumptionIncrease = 30;
        [Slider
        (
            label: "MK3 power consumption increment (%)",
            min: MinIncrease,
            max: MaxIncrease,
            Step = 5,
            Tooltip = "The percentage by which energy consumption is increased when using MK3 upgrade"
        )]
        public float MK3PowerConsumptionIncrease = 45;
        [Slider
        (
            label: "MK4 power consumption increment (%)",
            min: MinIncrease,
            max: MaxIncrease,
            Step = 5,
            Tooltip = "The percentage by which energy consumption is increased when using MK4 upgrade"
        )]
        public float MK4PowerConsumptionIncrease = 100;

        public float GetSpeedForLevel(int upgradeLevel)
        {
            float result = 0f;
            switch (upgradeLevel)
            {
                case 1:
                    result = MK1SpeedIncrease;
                    break;
                case 2:
                    result = MK2SpeedIncrease;
                    break;
                case 3:
                    result = MK3SpeedIncrease;
                    break;
                case 4:
                    result = MK4SpeedIncrease;
                    break;
            }

            return result;
        }
        public float GetPowerConsumptionForLevel(int upgradeLevel)
        {
            float result = 0f;
            switch (upgradeLevel)
            {
                case 1:
                    result = MK1PowerConsumptionIncrease;
                    break;
                case 2:
                    result = MK2PowerConsumptionIncrease;
                    break;
                case 3:
                    result = MK3PowerConsumptionIncrease;
                    break;
                case 4:
                    result = MK4PowerConsumptionIncrease;
                    break;
            }

            return result;
        }
    }
}