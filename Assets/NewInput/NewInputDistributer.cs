using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewInputDistributer : MonoBehaviour
{
    public EntityController playerOne;
    public EntityController playerTwo;

    public List<EntityController> connectedController;
    private float stepSize = 3.5f;
    
    public bool assignMode = false;

    private static NewInputDistributer _instance;
    public static NewInputDistributer Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            assignMode = false;
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < connectedController.Count; i++)
        {
            if (assignMode)
            {
                if (connectedController[i].movedAssign)
                {
                    if (Mathf.Abs(connectedController[i].h) < 0.1)
                        connectedController[i].movedAssign = false;
                }
                else
                {
                    if (connectedController[i].playerNum == 1)
                    {
                        if(connectedController[i].h >= 0.1)
                        {
                            connectedController[i].playerNum = -1;
                            connectedController[i].movedAssign = true;
                            playerOne = null;
                        }
                    }
                    else if (connectedController[i].playerNum == 2)
                    {
                        if (connectedController[i].h <= -0.1)
                        {
                            connectedController[i].playerNum = -1;
                            connectedController[i].movedAssign = true;
                            playerTwo = null;
                        }
                    }
                    else
                    {
                        if (connectedController[i].h <= -0.1 && playerOne == null)
                        {
                            connectedController[i].playerNum = 1;
                            connectedController[i].movedAssign = true;
                            playerOne = connectedController[i];
                        }
                        else if (connectedController[i].h >= 0.1 && playerTwo == null)
                        {
                            connectedController[i].playerNum = 2;
                            connectedController[i].movedAssign = true;
                            playerTwo = connectedController[i];
                        }
                    }
                }
            }
            if(i <5 )
                connectedController[i].transform.localPosition = new Vector3(0, -stepSize * i + 3.5f, 0) ;
            else
                connectedController[i].transform.localPosition = new Vector3(0, -stepSize * 4 + 3.5f, 0);

            if (connectedController[i].playerNum == -1)
            {
                if(playerOne == null && playerTwo == null)
                {
                    connectedController[i].spriteRenderer.sprite = connectedController[i].middleBoth;
                }
                else if (playerOne == null && playerTwo != null)
                {
                    connectedController[i].spriteRenderer.sprite = connectedController[i].middleLeft;
                }
                else if (playerOne != null && playerTwo == null)
                {
                    connectedController[i].spriteRenderer.sprite = connectedController[i].middleRight;
                }
                else
                {
                    connectedController[i].spriteRenderer.sprite = connectedController[i].middleNone;
                }
            }
            else if (connectedController[i].playerNum == 1)
            {
                connectedController[i].spriteRenderer.sprite = connectedController[i].left;
            }
            else if (connectedController[i].playerNum == 2)
            {
                connectedController[i].spriteRenderer.sprite = connectedController[i].right;
            }
        }
    }

    public void ResetControls()
    {
        NewCharacterMovement[] playerMovementComponents;
        playerMovementComponents = FindObjectsOfType<NewCharacterMovement>();
        foreach (var item in playerMovementComponents)
        {
            item.EmptyControls();
        }
        if(playerOne != null)
        {
            playerOne.inUse = false;
            playerOne.playerNum = -1;
            playerOne = null;
        }
        if(playerTwo != null)
        {
            playerTwo.inUse = false;

            playerTwo.playerNum = -1;

            playerTwo = null;
        }
        
        
    }
    public void OnNewControllerRecognized(EntityController newController)
    {
        if (playerOne == null && newController.playerNum == -1)
        {
            playerOne = newController;
            newController.inUse = true;
            NewCharacterMovement[] playerMovementComponents;
            playerMovementComponents = FindObjectsOfType<NewCharacterMovement>();
            newController.playerNum = 1;
            foreach (var item in playerMovementComponents)
            {
                if(item.player == 1)
                {
                    //item.FillControl(playerOne);
                }
            }
        }            
        else if (playerTwo == null && newController.playerNum == -1)
        {
            newController.playerNum = 2;
            playerTwo = newController;
            newController.inUse = true;
            NewCharacterMovement[] playerMovementComponents;
            playerMovementComponents = FindObjectsOfType<NewCharacterMovement>();
            foreach (var item in playerMovementComponents)
            {
                if (item.player == 2)
                {
                    item.FillControl(playerTwo);
                }
            }
        }           
    }

}
