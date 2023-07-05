using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelFileData 
{
    public string[] levelnames;
    public int[] levelWonStatus;
    //0 = not available
    //1 = not cleared
    //2 = cleared

    public LevelFileData()
    {

    }
}
