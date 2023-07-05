
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class MenuManager : MonoBehaviour
{
    private static MenuManager _instance;
    public static MenuManager Instance { get { return _instance; } }
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
    }
    Component[] menuComponents;
    public int activePage = 1;
     

    bool changing = false;
    bool leftSide = true;
    public static Animator animator;

    void SetActivePage(int pageNum, bool toggle)
    {
        foreach (MenuComponent element in menuComponents)
        {
            element.highlighted = false;
            
            if (element.pageNum == pageNum)
                element.SetVisible(toggle);
        }
    }
    public void UpdateVisibility()
    {
        SetActivePage(activePage, true);
        foreach (MenuComponent element in menuComponents)
        {
            if (element.visible == true)
                activePage = element.pageNum;
            if (element.pageNum == activePage && element.entranceState == true)
            {
                element.highlighted = true;
                element.Enter();
            }
                
        }
        //Debug.Log("Updated"+ activePage);
    }
    public void ChangeComplete()
    {
        foreach (MenuComponent element in menuComponents)
        {
            element.SetVisible(false);
        }
        UpdateVisibility();
        if (leftSide)
            leftSide = false;
        else
            leftSide = true;
        changing = false;
    }
    void Start()
    {
        activePage = 1;
        changing = false;
        leftSide = true;
        menuComponents = Object.FindObjectsOfType<MenuComponent>();

        SetActivePage(activePage, true);
        animator = gameObject.GetComponent<Animator>();
        foreach (MenuComponent element in menuComponents)
        {
            element.SetVisible(false);
        }
        UpdateVisibility();
    }
    public void ChangePage(int newPage)
    {
        //Debug.Log("try");
        if (!changing)
        {
            SetActivePage(newPage, true);
            if (!leftSide)
            {
                animator.SetTrigger("GoLeft");
                foreach (MenuComponent element in menuComponents)
                {
                    if (element.pageNum == newPage)
                    {
                        element.ChangeMaskInteraction(true);
                        element.sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    }

                    else if (element.pageNum == activePage)
                    {
                        element.ChangeMaskInteraction(false);
                        element.sprite.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                        element.Exit();
                    }

                }
            }
                
            else{
                animator.SetTrigger("GoRight");
                foreach (MenuComponent element in menuComponents)
                {
                    if (element.pageNum == newPage)
                    {
                        element.ChangeMaskInteraction(false);
                        element.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                    }

                    else if (element.pageNum == activePage)
                    {
                        element.ChangeMaskInteraction(true);
                        element.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                        element.Exit();
                    }

                }
            }
                
            changing = true;       }
    }
    
    public int returnOrderOfHighlighted()
    {
        foreach (MenuComponent element in menuComponents)
        {
            if (element.highlighted && element.order != 0)
            {
                return element.order;
            }

            
        }
        return 0;
    }
    
    public void ChangePageNonStatic(int newPage)
    {
        //Debug.Log("try");
        if (!changing)
        {
            SetActivePage(newPage, true);
            if (!leftSide)
            {
                animator.SetTrigger("GoLeft");
                foreach (MenuComponent element in menuComponents)
                {
                    if (element.pageNum == newPage)
                    {
                        element.ChangeMaskInteraction(true);
                        element.sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    }
                        
                    else if (element.pageNum == activePage)
                    {
                        element.ChangeMaskInteraction(false);
                        element.sprite.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                        element.Exit();
                    }
                        
                }
            }
            else
            {
                animator.SetTrigger("GoRight");
                foreach (MenuComponent element in menuComponents)
                {
                    if (element.pageNum == newPage)
                    {
                        element.ChangeMaskInteraction(false);
                        element.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                    }
                        
                    else if (element.pageNum == activePage)
                    {
                        element.ChangeMaskInteraction(true);
                        element.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                        element.Exit();
                    }
                        
                }
            }
            changing = true;
            activePage = newPage;
        }
    }
    
    public void ChangePageNonStatic(MenuComponent newEntrance)
    {
        int newPage;
        if (activePage == 1)
            newPage = 2;
        else
            newPage = 1;
        //Debug.Log("try");
        if (!changing)
        {
            SetActivePage(newPage, true);
            if (!leftSide)
            {
                animator.SetTrigger("GoLeft");
                foreach (MenuComponent element in menuComponents)
                {
                    if (element.pageNum == newPage)
                    {
                        element.ChangeMaskInteraction(true);
                        element.sprite.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                    }

                    else if (element.pageNum == activePage)
                    {
                        element.entranceState = false;
                        element.ChangeMaskInteraction(false);
                        element.sprite.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                        element.Exit();
                    }

                }
            }
            else
            {
                animator.SetTrigger("GoRight");
                foreach (MenuComponent element in menuComponents)
                {
                    if (element.pageNum == newPage)
                    {
                        element.ChangeMaskInteraction(false);
                        element.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;
                    }

                    else if (element.pageNum == activePage)
                    {
                        element.entranceState = false;
                        element.ChangeMaskInteraction(true);
                        element.gameObject.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
                        element.Exit();
                    }

                }
            }
            newEntrance.entranceState = true;
            changing = true;
            activePage = newPage;
        }
    }

    public void OnAssignControlsButton()
    {
        Management.Instance.AssignControlsEnter();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
