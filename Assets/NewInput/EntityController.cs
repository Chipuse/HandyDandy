using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityController : MonoBehaviour
{
    public bool inUse;
    public float h;
    public bool jumpButton;
    public bool jumpButtonDown;
    public bool emoteButton;
    public int playerNum = -1;

    public Sprite middleBoth;
    public Sprite middleNone;
    public Sprite middleRight;
    public Sprite middleLeft;
    public Sprite left;
    public Sprite right;
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public bool movedAssign = false;

    // Start is called before the first frame update
    void Start()
    {
        inUse = false;
        playerNum = -1;playerNum = -1;
        DontDestroyOnLoad(gameObject);
        transform.SetParent(NewInputDistributer.Instance.gameObject.transform);
        NewInputDistributer.Instance.connectedController.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void LateUpdate()
    {
        jumpButtonDown = false;
        //emoteButton = false;
    }

    private void OnMove(InputValue input)
    {
        //if (!inUse)
        //    NewInputDistributer.Instance.OnNewControllerRecognized(this);
        //Debug.Log("im moving" + input.Get<Vector2>());
        h = input.Get<Vector2>().x;
    }

    private void OnJump(InputValue input)
    {
        
        jumpButton = true;
        jumpButtonDown = true;

    }
    private void OnJumpRelease(InputValue input)
    {
        jumpButton = false;
        
    }
    private void OnEmote(InputValue input)
    {
        emoteButton = true;
    }

    private void OnEmoteRelease(InputValue input)
    {
        emoteButton = false;
    }






}
