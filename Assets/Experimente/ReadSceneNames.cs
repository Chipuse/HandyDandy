using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadSceneNames : MonoBehaviour
{
    public string[] scenes;
    public static string[] scenesStatic;
#if UNITY_EDITOR
    private static string[] ReadNames()
    {
        List<string> temp = new List<string>();
        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            if (S.enabled)
            {
                string name = S.path.Substring(S.path.LastIndexOf('/') + 1);
                name = name.Substring(0, name.Length - 6);
                temp.Add(name);
            }
        }
        return temp.ToArray();
    }
    [UnityEditor.MenuItem("CONTEXT/ReadSceneNames/Update Scene Names")]
    private static void UpdateNames(UnityEditor.MenuCommand command)
    {
        
        ReadSceneNames context = (ReadSceneNames)command.context;
        context.scenes = ReadNames();
        ReadSceneNames.scenesStatic = ReadNames();
        
    }

    public static void UpdateNamesStatic()
    {
        ReadSceneNames.scenesStatic = ReadNames();        
    }


    private void Reset()
    {
        scenes = ReadNames();
    }
#endif
    public void InvokeReadNames()
    {

    }

    public static int GetSceneIndexByName(string name)
    {
        for(int i = 0; i< scenesStatic.Length; i++)
        {
            if (name == scenesStatic[i])
                return i;
        }
        return -1;
    }

    public static string GetSceneNameByIndex(int index)
    {
        if (index < scenesStatic.Length)
            return scenesStatic[index];
        return null;
    }
}