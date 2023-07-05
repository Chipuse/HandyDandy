using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class MenuComponent : MonoBehaviour
{
    public MenuComponent up;
    public MenuComponent down;
    public MenuComponent left;
    public MenuComponent right;
    public int order;
    public string sceneToLoadName = null;
    public int accessible = 1;

    public string[] levelNeedToBeCleared;

    public bool startButton = false;

    public Sprite bigPic;
    public SpriteRenderer sprite;
    public Color selected= new Color(255,255,255);
    public Color notSelected = new Color(200, 200, 200,200);

    public ScrollGroup sc;

    public bool entranceState = false; 
    [HideInInspector]
    public bool highlighted = false;
    [HideInInspector]
    public bool visible;

    public int pageNum = 1;
    bool stickReset = true;

    public ParticleSystem ps;
    public UnityEvent Action;
    public UnityEvent EnterAction;
    public UnityEvent ExitLeftAction;
    public UnityEvent ExitRightAction;

    public SpriteRenderer activateSprite;
    public SpriteRenderer activeButton;
    public ParticleSystem activateParticles;
    public SpriteRenderer lockedButton;

    private SpriteRenderer[] srComponent;
   
    private float coolDownCounter = 0;
    public bool enteredFirstTime = false;
    private float coolDownSec = 0.1f;
    private float coolDownSecFirst = 0.5f;

    public void SetVisible(bool toggle)
    {
        if(true)
        {
            if (!toggle)
            {
                sprite.enabled = false;
            }
            else
                sprite.enabled = true;
            visible = toggle;
        }
       
    }
    public void Enter()
    {        
        EnterAction.Invoke();
        stickReset = false;
        coolDownCounter = 0;
        if(sprite != null)
        {
            sprite.color = selected;
        }
    }
    public void Exit()
    {
        
        if (sprite != null)
        {
            sprite.color = notSelected;
        }
    }
    public void ActiveUpdate()
    {
        coolDownCounter += 1 * Time.deltaTime;

        if (Input.GetAxis("HorizontalAny") == 0 && Input.GetAxis("Vertical") == 0)        
        {
            stickReset = true;
        }

        if (Input.GetButtonDown("Submit"))
        {
            if (startButton && sc != null)
                sc.loadIndex = left.order;

            StartCoroutine(OnPress());            
        }

        if (Input.GetButtonDown("Menu") && MenuManager.Instance.activePage != 1)
        {
            MenuManager.Instance.ChangePageNonStatic(this);
        }
            

    }
    
    
    
    private void Change(MenuComponent next)
    {
        Exit();
        if (stickReset)
            next.enteredFirstTime = true;
        else
            next.enteredFirstTime = false;
        next.Enter();
    }

    public void Reason()
    {
       
        
        //if (stickReset)Debug.LogError(Input.GetAxis("Vertical"));
        if ( (!enteredFirstTime && coolDownCounter >= coolDownSec) || (enteredFirstTime && coolDownCounter >= coolDownSecFirst) || stickReset)
        {
            if (Input.GetAxis("Vertical") > 0)
            {
                if (up != null && up.accessible >= 1)
                {
                    highlighted = false;
                    Change(up);
                    up.highlighted = true;
                }

            }
            else if (Input.GetAxis("Vertical") < 0)
            {
                if (down != null && down.accessible >= 1)
                {
                    highlighted = false;
                    Change(down);
                    down.highlighted = true;
                }
            }
            else if (Input.GetAxis("HorizontalAny") > 0)
            {
                if (right != null && right.accessible >= 1)
                {
                    highlighted = false;
                    ExitRightAction.Invoke();
                    Change(right);
                    right.highlighted = true;
                }
            }
            else if (Input.GetAxis("HorizontalAny") < 0)
            {
                if (left != null && left.accessible >= 1)
                {
                    highlighted = false;
                    ExitLeftAction.Invoke();
                    Change(left);
                    left.highlighted = true;
                }
            }
        }
        
    }


    IEnumerator OnPress()
    {
        if(activateSprite != null)
            activateSprite.enabled = true;
        if(activeButton != null)
            activeButton.enabled = true;
        if (activateParticles != null)
            activateParticles.Play();
        
        yield return new WaitForSeconds(0.35f);
        
        if(activateSprite != null)
            activateSprite.enabled = false;
        if(activeButton != null)
            activeButton.enabled = false;
        Action.Invoke();
        
    }

    void Start()
    {
        if(GetComponent<SpriteRenderer>() != null)
        {
            sprite = GetComponent<SpriteRenderer>();
            sprite.color = notSelected;
            if(entranceState)
                sprite.color = selected;
        }

        if (activateSprite == null)
        {
            if (gameObject.transform.childCount > 2){
                GameObject go = transform.GetChild(1).gameObject;
                //Debug.Log(go.name);
                activateSprite = go.GetComponent<SpriteRenderer>();
            }
        }
        
        if (lockedButton == null)
        {
            if (gameObject.transform.childCount > 2){
                GameObject go = transform.GetChild(2).gameObject;
                //Debug.Log(go.name);
                lockedButton = go.GetComponent<SpriteRenderer>();
            }
        }


        srComponent = GetComponentsInChildren<SpriteRenderer>();
        if(sceneToLoadName != null && sceneToLoadName != "")
        {
            if (Management.Instance.levelCleared.ContainsKey(sceneToLoadName))
            {
                accessible = Management.Instance.levelCleared[sceneToLoadName];
            }
        }
        if(levelNeedToBeCleared.Length <= 0 || levelNeedToBeCleared == null)
        {
            accessible = 1;
        }
        else
        {
            foreach (var item in levelNeedToBeCleared)
            {
                if(item != null && item != "")
                {
                    if (Management.Instance.levelCleared.ContainsKey(item))
                    {
                        if (Management.Instance.levelCleared[item] >= 2 && accessible <= 1)
                        {
                            accessible = 1;
                            Management.Instance.levelCleared[sceneToLoadName] = accessible;
                        }
                    }
                    else
                    {
                        Debug.LogError("Level does not exist in build");
                        accessible = 0;
                    }
                }
                else
                {
                    accessible = 1;
                }
                
            }
        }

    }
    public void SetLeft(MenuComponent newLeft)
    {
        right = newLeft;
    }
    public void SetRight(MenuComponent newLeft)
    {
        left = newLeft;
    }


    void Update()
    {

        if (highlighted && NewInputDistributer.Instance.assignMode == false)
        {            
            ActiveUpdate();
            Reason();
        }
        else if(highlighted && NewInputDistributer.Instance.assignMode == true)
        {
            if (Input.GetButtonDown("Submit") )
            {
                Management.Instance.AssignControlsExit();

            }
        }
    }

    public void ChangeMaskInteraction(bool visibleInMask)
    {
        if (srComponent == null)
        {
            return;
        }
           
        foreach (SpriteRenderer element in srComponent)
            {
                if (visibleInMask)
                {
                    element.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                }
                        
                else if (!visibleInMask)
                {
                    element.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                    
                }
                        
            }

        if (accessible < 1 && lockedButton != null)
        {
            sprite.enabled = false;
            lockedButton.enabled = true;
        }
        
    }

    public void ButtonLoadLevel()
    {
        
        if (sceneToLoadName != null && sceneToLoadName != "")
        {
            if ( Management.Instance.levelCleared.ContainsKey(sceneToLoadName))
            {
                if (Management.Instance.levelCleared[sceneToLoadName] >= 1)
                {
                    Debug.Log(sceneToLoadName);
                    SceneManager.LoadScene(sceneToLoadName);
                }
                else
                {
                    Debug.Log("level not available yet");
                }
            }
        }
    }

    public void ButtonLoadLevelIgnore()
    {
        
        if (sceneToLoadName != null && sceneToLoadName != "")
        {
            if (Management.Instance.levelCleared.ContainsKey(sceneToLoadName))
            {
                SceneManager.LoadScene(sceneToLoadName);
            }
        }
    }

    public void QuitGame()
    {
        Management.Instance.QuitGame();
    }
}
