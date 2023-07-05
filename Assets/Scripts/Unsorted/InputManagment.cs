using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManagment : MonoBehaviour
{
    public static string playerOne;
    public static string playerTwo;
    private static string e = "empty";

    private static InputManagment _instance;
    public static InputManagment Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }
        // Start is called before the first frame update
    void Start()
    {
        resetControls();
    }
    void resetControls()
    {
        playerOne = e;
        playerTwo = e;
    }
    public static void ResetControlsStatic()
    {
        playerOne = e;
        playerTwo = e;
    }

    public static string getPlayerOne()
    {
        return playerOne;
    }
    public static string getPlayerTwo()
    {
        return playerTwo;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            assignControls("1");
        }
        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            assignControls("2");
        }
        if (Input.GetKeyDown(KeyCode.Joystick3Button0))
        {
            assignControls("3");
        }
        if (Input.GetKeyDown(KeyCode.Joystick4Button0))
        {
            assignControls("4");
        }
        if (Input.GetKeyDown(KeyCode.Joystick5Button0))
        {
            assignControls("5");
        }
        if (Input.GetKeyDown(KeyCode.Joystick6Button0))
        {
            assignControls("6");
        }
        if (Input.GetKeyDown(KeyCode.Joystick7Button0))
        {
            assignControls("7");
        }
        if (Input.GetKeyDown(KeyCode.Joystick8Button0))
        {
            assignControls("8");
        }
        


    }
    void assignControls(string i)
    {

        if(playerOne == e)
        {
            playerOne = i;
        }
        else if(playerTwo == e && i != playerOne)
        {
            playerTwo = i;
        }
    }
}
