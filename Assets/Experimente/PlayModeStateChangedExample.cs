using UnityEngine;
using UnityEditor;

// ensure class initializer is called whenever scripts recompile
#if UNITY_EDITOR
[InitializeOnLoad]
public static class PlayModeStateChangedExample
{
    // register an event handler when the class is initialized
    static PlayModeStateChangedExample()
    {
        EditorApplication.playModeStateChanged += LogPlayModeState;
    }

    private static void LogPlayModeState(PlayModeStateChange state)
    {

        ReadSceneNames.UpdateNamesStatic();
        Management.Instance.levelNames.scenes = ReadSceneNames.scenesStatic;
    }
}
#endif