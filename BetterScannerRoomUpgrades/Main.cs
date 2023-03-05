using HarmonyLib;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using SMLHelper.V2.Handlers;
using BetterScannerRoomUpgrades.Configuration;
using BetterScannerRoomUpgrades.Modules;

namespace BetterScannerRoomUpgrades
{

    [BepInPlugin(GUID, PluginName, VersionString)]
    public class Main : BaseUnityPlugin
    {
        private const string PluginName = "BetterScannerRoomUpgrades";
        private const string VersionString = "1.1.1";
        private const string Author = "jperezruiz";
        private const string GUID = Author + "_" + PluginName;
        
        public static ManualLogSource Log = new ManualLogSource(PluginName);
        internal static SMLConfig s_modConfig { get; } = OptionsPanelHandler.RegisterModOptions<SMLConfig>();
        
        internal static PowerEfficiencyModule PowerEfficiencyModule = new();
        /// <summary>
        /// Initialise the configuration settings and patch methods
        /// </summary>
        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
            PowerEfficiencyModule.Patch();
            Logger.LogInfo($"[{PluginName} {VersionString}] loaded");
            Log = Logger;
        }
    }
}