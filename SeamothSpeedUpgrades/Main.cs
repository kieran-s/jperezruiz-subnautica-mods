using System.Collections.Generic;
using HarmonyLib;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using SeamothSpeedUpgrades.Modules;

namespace SeamothSpeedUpgrades
{
    [BepInPlugin(GUID, PluginName, VersionString)]
    [BepInProcess("Subnautica.exe")]
    [BepInDependency("com.ahk1221.smlhelper", BepInDependency.DependencyFlags.HardDependency)]
    public class Main : BaseUnityPlugin
    {
        private const string PluginName = "SeamothSpeedUpgradesBepinex";
        private const string VersionString = "1.0.0";
        private const string Author = "jperezruiz";
        private const string GUID = "com.jperezruiz.SeamothSpeedUpgrades";

        public static ManualLogSource Log = new ManualLogSource(PluginName);
        internal static SeamothSpeedModuleMK1 SeamothSpeedModuleMk1 = new();
        internal static SeamothSpeedModuleMK2 SeamothSpeedModuleMk2 = new();
        internal static SeamothSpeedModuleMK3 SeamothSpeedModuleMk3 = new();
        internal static SeamothSpeedModuleMK4 SeamothSpeedModuleMk4 = new();

        internal static List<BaseSeamothSpeedModule> SeamothSpeedModules = new()
        {
            SeamothSpeedModuleMk1,
            SeamothSpeedModuleMk2,
            SeamothSpeedModuleMk3,
            SeamothSpeedModuleMk4
        };
        /// <summary>
        /// Initialise the configuration settings and patch methods
        /// </summary>
        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
            SeamothSpeedModules.ForEach(m => m.Patch());
            Logger.LogInfo($"[{PluginName} {VersionString}] loaded");
            Log = Logger;
        }
    }
}