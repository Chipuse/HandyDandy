using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessManager : MonoBehaviour
{
    
    public string[] needLvl;
    public SpriteRenderer[] clouds;
    // Start is called before the first frame update
    void Start()
    {
        SoundControl.Instance.FadeVolume("CrystalChime", 0);
        for (int i = 0; i < clouds.Length; i++)
        {
            if (Management.Instance.levelCleared.ContainsKey(needLvl[i]))
            {
                if (Management.Instance.levelCleared[needLvl[i]] == 2 && Management.Instance.lvlJustCleared != needLvl[i])
                {

                    clouds[i].color = new Color(clouds[i].color.r, clouds[i].color.g, clouds[i].color.b, 0);
                }
                else
                {
                    clouds[i].color = new Color(clouds[i].color.r, clouds[i].color.g, clouds[i].color.b, 1);
                    if(Management.Instance.lvlJustCleared == needLvl[i])
                    {
                        IEnumerator cor = fadeOut(clouds[i]);
                        StartCoroutine(cor);
                    }                    
                }
            }
        }
        MoveToLevel(Management.Instance.lvlJustCleared); 
    }

    private IEnumerator fadeOut(SpriteRenderer cloud)
    {
        yield return new WaitForSeconds(2);
        while(cloud.color.a > 0)
        {
            cloud.color = new Color(cloud.color.r, cloud.color.g, cloud.color.b, cloud.color.a - 0.05f);
            yield return new WaitForSeconds(0.1f);
        }
    }


    //Should be called as soon as back in menu
    public void MoveToLevel(string LvlName)
    {
        if(LvlName != "")
        {
            MenuComponent[] menuComponents = GetComponentsInChildren<MenuComponent>();
            if (Management.Instance.levelCleared.ContainsKey(LvlName))
            {
                for (int i = 0; i < menuComponents.Length; i++)
                {
                    menuComponents[i].entranceState = false;
                    menuComponents[i].Exit();
                    if (menuComponents[i].sceneToLoadName == LvlName)
                    {
                        menuComponents[i].entranceState = true;
                    }

                }
            }
            MenuManager.Instance.ChangePageNonStatic(2);
            Management.Instance.lvlJustCleared = "";
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //SoundControl.Instance.FadeVolume("CrystalChime", 0);
    }
}


public class MistCloudAccess
{
    public string needLvl;
    public SpriteRenderer cloud;
}
