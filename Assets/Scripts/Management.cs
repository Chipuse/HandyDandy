
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class Management : MonoBehaviour
{
    public string[] BackToLevelSelect;
    public string lvlJustCleared = "";
    public Dictionary<string, int> levelCleared;
    //0 = not available
    //1 = not cleared
    //2 = cleared
    public ReadSceneNames levelNames;
    public LevelFileData levelData;


    private static Management _instance;
    public static Management Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this);



            //load or create new savefile
            levelCleared = new Dictionary<string, int>();
            levelData = new LevelFileData();
            if (SaveSystem.SaveDataExists())            
            {
                Debug.Log("There is a savefile");
                levelData = SaveSystem.LoadLevelFile();
                for (int i = 0; i < levelData.levelnames.Length; i++)
                {

                    levelCleared.Add(levelData.levelnames[i], levelData.levelWonStatus[i]);
                    
                }
                //Add possible new updatded levels
                for (int i = 0; i < levelNames.scenes.Length; i++)
                {
                    if (!levelCleared.ContainsKey(levelNames.scenes[i]))
                    {
                        levelCleared.Add(levelNames.scenes[i], 0);
                    }
                }
            }
            else
            {
                levelData.levelnames = new string[levelNames.scenes.Length];
                levelData.levelWonStatus = new int[levelNames.scenes.Length];
                for( int i = 0; i < levelNames.scenes.Length; i ++)
                {
                    int defaultValue = 0;
                    levelCleared.Add(levelNames.scenes[i], defaultValue);
                    levelData.levelnames[i] = levelNames.scenes[i];
                    levelData.levelWonStatus[i] = defaultValue;
                }
                //automatically creates a new standard savefile
                SaveSystem.SaveLevelFile(levelData);

                
            }
            foreach (var item in levelCleared)
            {
                //Debug.Log(item.Key + ": " + levelCleared[item.Key]);
            }
        }
    }
    public CameraShake shake;
    
    private GameObject[] lamps;
    private lamp[] scriptArray;
    public float timer;
    private float duration = 4;
    public List<GameObject> allLamps = new List<GameObject>();
    public GameObject text;
    private bool lvlWon;

    private float lingeringSeconds = 2;

    private static int nextSceneIndex = 0;
    private static float musicVolume = 0.231f;
    public int firstMusic = 0;
    public int secondMusic = 0;
    public int thirdMusic = 0;
    public int fourthMusic = 0;
    public int fifthMusic = 0;
    public int menuIndex = 0;
    void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
        
        
      

        Cursor.visible = false;
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        lvlWon = false;
        scriptArray = Object.FindObjectsOfType<lamp>();
        GameObject[] lamps = GameObject.FindGameObjectsWithTag("Lamp");
        foreach (GameObject go in lamps)
            allLamps.Add(go);


        timer = 0;
        /*
        for (int i = 0; i < allLamps.Count; i++)
            print(" is an active object");
            */
        if (SceneManager.GetActiveScene().buildIndex == fourthMusic || SceneManager.GetActiveScene().buildIndex == fourthMusic+1 || SceneManager.GetActiveScene().buildIndex <= menuIndex)
        {

            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", 0.0f);
            SoundControl.Instance.FadeVolume("Credits", musicVolume);
        }
        else if (SceneManager.GetActiveScene().buildIndex < firstMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", musicVolume);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", 0.0f);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex < secondMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", musicVolume);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", 0.0f);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex < thirdMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", musicVolume);
            SoundControl.Instance.FadeVolume("BGMusic4", 0.0f);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex < fourthMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", musicVolume);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex < fifthMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", musicVolume);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }

    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        Debug.Log("Loaded new Scene");
        SoundControl.Instance.FadeVolume("CrystalChime", 0);
        Cursor.visible = false;
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        lvlWon = false;
        scriptArray = Object.FindObjectsOfType<lamp>();
        GameObject[] lamps = GameObject.FindGameObjectsWithTag("Lamp");
        Debug.Log(lamps);
        allLamps = new List<GameObject>();
        foreach (GameObject go in lamps)
            allLamps.Add(go);
        lvlWon = false;

        timer = 0;
        /*
        for (int i = 0; i < allLamps.Count; i++)
            print(" is an active object");
            */
        if (SceneManager.GetActiveScene().buildIndex == fourthMusic || SceneManager.GetActiveScene().buildIndex == fourthMusic+1 || SceneManager.GetActiveScene().buildIndex <= menuIndex)
        {

            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", 0.0f);
            SoundControl.Instance.FadeVolume("Credits", musicVolume);
        }
        else if (SceneManager.GetActiveScene().buildIndex < firstMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", musicVolume);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", 0.0f);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex < secondMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", musicVolume);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", 0.0f);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex < thirdMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", musicVolume);
            SoundControl.Instance.FadeVolume("BGMusic4", 0.0f);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex < fourthMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", musicVolume);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }
        else if (SceneManager.GetActiveScene().buildIndex < fifthMusic)
        {
            SoundControl.Instance.FadeVolume("BGMusic", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic2", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic3", 0.0f);
            SoundControl.Instance.FadeVolume("BGMusic4", musicVolume);
            SoundControl.Instance.FadeVolume("Credits", 0.0f);
        }

        //LevelChanger.Instance.FadeInLevel();
    }

    void Update()
    {
        if(NewInputDistributer.Instance.assignMode == false)
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown("o"))
                LoadPrevScene();
            else if (Input.GetKeyDown("p"))
                LoadNextScene();
            else if (Input.GetKeyDown("i"))
                AssignControlsEnter();
            else if (Input.GetKeyDown("u"))
                CompleteLevel();
#endif

            if (lvlWon)
            {
                //Debug.Log(lingeringSeconds);
                lingeringSeconds = lingeringSeconds - 1 * Time.deltaTime;

                if (lingeringSeconds <= 0)
                {
                    //LevelChanger.Instance.FadeToLevel();
                    lingeringSeconds = 2;

                    //LoadNextScene();
                    CompleteLevel();
                }
            }



            for (int i = 0; i < allLamps.Count; i++)
            {
                GameObject akt = allLamps[i];
                lamp lamp = akt.GetComponent(typeof(lamp)) as lamp;
                if (lamp == null || !lamp.isOn)
                    timer = 0;
                timer = timer + 1 * Time.deltaTime;
            }
            if (timer > duration)
            {
                Win();
            }




        }




        //if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton7))
        if (Input.GetButtonDown("Menu"))
        {
            Menu();
        }
        
    }
    private void LateUpdate()
    {
        

    }
    public void Win()
    {
        //Debug.Log("Yay");
        if (!lvlWon)
        {
            for (int i = 0; i < allLamps.Count; i++)
            {
                GameObject akt = allLamps[i];
                lamp lamp = akt.GetComponent(typeof(lamp)) as lamp;
                lamp.PlayFinal();
                
                
            }
            if (shake != null)
            {
                shake.TriggerShake();
            }
                
        }
        lvlWon = true;
        
        //text.SetActive(true);
    }
    
    public static int sceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }


    public void CompleteLevel()
    {

        Debug.Log(SceneManager.sceneCountInBuildSettings);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        bool levelWasClearedBefore = false;
        if (levelCleared[SceneManager.GetActiveScene().name] == 2)
            levelWasClearedBefore = true;
        levelCleared[SceneManager.GetActiveScene().name] = 2;

        SaveSystem.SaveFromLevelClearedDict();
        NextLevelToken next = FindObjectOfType<NextLevelToken>();
        bool backToLvl = false;
        for (int i = 0; i < BackToLevelSelect.Length; i++)
        {
            if(SceneManager.GetActiveScene().name == BackToLevelSelect[i])
            {
                if(next == null || (next != null && levelWasClearedBefore == false))
                {
                    backToLvl = true;
                    lvlJustCleared = SceneManager.GetActiveScene().name;
                    break;
                }
            }
        }
        if (backToLvl)
        {
            SceneManager.LoadScene(1);
            SoundControl.Instance.FadeVolume("CrystalChime", 0);
            return;
        }
       
        if(next != null)
        {
            SceneManager.LoadScene(next.NextLevelName);
            return;
        }
        

        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex+1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
            SceneManager.LoadScene(0);

    }
    public void LoseLevel()
    {
        SceneManager.LoadScene(1);
        Debug.Log("Oh no");
    }

    public void Menu()
    {
        if(SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 1 && NewInputDistributer.Instance.assignMode == false)
        {
            SoundControl.Instance.FadeVolume("CrystalChime", 0.0f);            
            SceneManager.LoadScene(1);
        }
        else if(SceneManager.GetActiveScene().name  == "MenuAndLevelSelect")
        {

        }
        AssignControlsExit();


    }

    public static void LoadSceneAtIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadSceneAtIndexNonStatic(int index)
    {
        SceneManager.LoadScene(index);
    }
    public static void LoadNextScene()
    {
        if(SceneManager.GetActiveScene().name == "Extra8")
        {
            SceneManager.LoadScene(1);
        }
        else
            SceneManager.LoadScene(nextSceneIndex);
    }

    public static void LoadPrevScene()
    {
        SceneManager.LoadScene(nextSceneIndex-2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void ResetController()
    {
        InputManagment.ResetControlsStatic();
        NewInputDistributer.Instance.ResetControls();
    }
    public void AssignControlsEnter()
    {
        if(NewInputDistributer.Instance.assignMode == false)
        {
            NewInputDistributer.Instance.GetComponent<Animator>().SetTrigger("Enter");

            NewInputDistributer.Instance.assignMode = true;
            NewInputDistributer.Instance.ResetControls();
        }
    }
    public void AssignControlsExit()
    {
        if (NewInputDistributer.Instance.assignMode == true)
        {
            NewInputDistributer.Instance.assignMode = false;
            NewInputDistributer.Instance.GetComponent<Animator>().SetTrigger("Exit");
        }
    }
}
