using System;
using UnityEditor;

namespace Build
{
    public static class Build
    {
        private static readonly string[] ServerScenes = new[] { "Assets/Scenes/Main.unity" };
        private static readonly string[] ClientScenes = new[] { "Assets/Scenes/Main.unity" };

        [MenuItem("Build/Build All")]
        public static void BuildAll()
        {
            BuildWindowsServer();
            BuildLinuxServer();
            BuildWindowsClient();
            BuildMacClient();
        }

        [MenuItem("Build/Build Server (Windows)")]
        public static void BuildWindowsServer()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = ServerScenes;
            buildPlayerOptions.locationPathName = "Builds/Windows/Server/Server.exe";
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.options = BuildOptions.CompressWithLz4HC | BuildOptions.EnableHeadlessMode;

            Console.WriteLine("Building Server (Windows)...");
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            Console.WriteLine("Built Server (Windows).");
        }

        [MenuItem("Build/Build Server (Linux)")]
        public static void BuildLinuxServer()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = ServerScenes;
            buildPlayerOptions.locationPathName = "Builds/Linux/Server/Server.x86_64";
            buildPlayerOptions.target = BuildTarget.StandaloneLinux64;
            buildPlayerOptions.options = BuildOptions.CompressWithLz4HC | BuildOptions.EnableHeadlessMode;

            Console.WriteLine("Building Server (Linux)...");
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            Console.WriteLine("Built Server (Linux).");
        }


        [MenuItem("Build/Build Client (Windows)")]
        public static void BuildWindowsClient()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = ClientScenes;
            buildPlayerOptions.locationPathName = "Builds/Windows/Client/Client.exe";
            buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
            buildPlayerOptions.options = BuildOptions.CompressWithLz4HC;

            Console.WriteLine("Building Client (Windows)...");
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            Console.WriteLine("Built Client (Windows).");
        }

        [MenuItem("Build/Build Client (Mac)")]
        public static void BuildMacClient()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = ClientScenes;
            buildPlayerOptions.locationPathName = "Builds/Mac/Client/Client.app";
            buildPlayerOptions.target = BuildTarget.StandaloneOSX;
            buildPlayerOptions.options = BuildOptions.CompressWithLz4HC;

            Console.WriteLine("Building Client (Mac)...");
            BuildPipeline.BuildPlayer(buildPlayerOptions);
            Console.WriteLine("Built Client (Mac).");
        }
    }
}