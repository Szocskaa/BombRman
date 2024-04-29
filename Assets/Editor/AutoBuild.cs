using System.Linq;
using UnityEditor;

public class AutoBuild
{
    public static void Build()
    {
       
        var scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path) 
            .ToArray();

        
        string outputPath = "Builds/Windows64/BombRman.exe";

        
        BuildPipeline.BuildPlayer(scenes, outputPath, BuildTarget.StandaloneWindows64, BuildOptions.None);

        UnityEngine.Debug.Log("Build completed!");
    }
}
