﻿using HarmonyLib;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using SMLHelper.V2.Handlers;
using CyclopsSolarCharger.Configuration;
namespace CyclopsSolarCharger
{

    [BepInPlugin(GUID, PluginName, VersionString)]
    [BepInProcess("Subnautica.exe")]
    [BepInDependency("com.ahk1221.smlhelper", BepInDependency.DependencyFlags.HardDependency)]
    public class Main : BaseUnityPlugin
    {
        private const string PluginName = "CyclopsSolarCharger";
        private const string VersionString = "1.1.1";
        private const string Author = "jperezruiz";
        private const string GUID = Author + "_" + PluginName;
        
        public static ManualLogSource Log = new ManualLogSource(PluginName);
        internal static Modules.CyclopsSolarModule solarModule = new();
        internal static NautilusConfig s_modConfig { get; } = OptionsPanelHandler.RegisterModOptions<NautilusConfig>();
        /// <summary>
        /// Initialise the configuration settings and patch methods
        /// </summary>
        private void Awake()
        {
            Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), GUID);
            solarModule.Patch();
            Logger.LogInfo($"[{PluginName} {VersionString}] loaded");
            Log = Logger;
        }
    }
}