using System.Collections.Generic;
using HarmonyLib;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using CyclopsSpeedUpgrades.Modules;

namespace CyclopsSpeedUpgrades
{
    [BepInPlugin(GUID, PluginName, VersionString)]
    [BepInProcess("Subnautica.exe")]
    [BepInDependency("com.ahk1221.smlhelper", BepInDependency.DependencyFlags.HardDependency)]
    public class Main : BaseUnityPlugin
    {
        private const string PluginName = "CyclopsSpeedUpgradesBepinex";
        private const string VersionString = "1.0.0";
        private const string Author = "jperezruiz";
        private const string GUID = "com.jperezruiz.cyclopsspeedupgrades";

        public static ManualLogSource Log = new ManualLogSource(PluginName);
        internal static CyclopsSpeedModuleMKI CyclopsSpeedModuleMKI = new();
        internal static CyclopsSpeedModuleMKII CyclopsSpeedModuleMKII = new();
        internal static CyclopsSpeedModuleMKIII CyclopsSpeedModuleMKIII = new();

        internal static List<BaseCyclopsSpeedModule> CyclopsSpeedModules = new()
        {
            CyclopsSpeedModuleMKI,
            CyclopsSpeedModuleMKII,
            CyclopsSpeedModuleMKIII
        };
        /// <summary>
        /// Initialise the configuration settings and patch methods
        /// </summary>
        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
            CyclopsSpeedModules.ForEach(m => m.Patch());
            Logger.LogInfo($"[{PluginName} {VersionString}] loaded");
            Log = Logger;
        }
    }
}