using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ScrollGroup : MonoBehaviour
{
    Vector3 scrollTarget;
    Vector3 oldPosition;
    public Transform startPosition;
    public Vector3 stepLength;
    float scrollSpeed = 0.033f;
    float t = 0;
    public SpriteRenderer bigPic;
    public MenuComponent start;
    public int loadIndex = 0;
    private int levelOffset = 1;
    public bool autoArrange = true;

    public SpriteRenderer[] mistClouds;

    public ScrollObject[] scrollObjects;

    public MenuComponent[] objects;

    public void SetScrollTarget(Transform tf)
    {
        if(scrollTarget.y != -tf.localPosition.y)
        {
            scrollTarget = new Vector3(transform.position.x, -tf.localPosition.y, transform.position.z);
            t = 0;
            oldPosition = transform.position;
        }
        
        /*
        if (tf.gameObject.GetComponent<MenuComponent>().bigPic != null)
        {
            bigPic.sprite = tf.gameObject.GetComponent<MenuComponent>().bigPic;
        }
        else
            bigPic.sprite = tf.gameObject.GetComponent<SpriteRenderer>().sprite;
            */
    }
    void Start()
    {
        oldPosition = transform.position;
        scrollTarget = transform.position;
        objects = GetComponentsInChildren<MenuComponent>();
        
        //arrange();
        //bigPic = GetComponent<SpriteRenderer>();

        foreach (var item in mistClouds)
        {
            item.color = new Color(255,255,255, 0);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Menu"))
        {

        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(oldPosition, new Vector3(oldPosition.x, scrollTarget.y, oldPosition.z), t);
        t = t + scrollSpeed;
    }

    public void setStart(bool toggle)
    {
        //start.highlighted = toggle;
    }
    public void loadScene()
    {
        Management.LoadSceneAtIndex(levelOffset + loadIndex + Management.sceneIndex());
    }

    void arrange()
    {
        MenuComponent first = null;
        MenuComponent prior = null;
        Array.Sort(objects, delegate (MenuComponent user1, MenuComponent user2) {
            return user1.order.CompareTo(user2.order);
        });
        if (autoArrange)
            foreach (MenuComponent item in objects)
        {
            if(first == null)
            {

                first = item;
                prior = item;
                if (autoArrange)
                    item.gameObject.transform.SetPositionAndRotation(transform.position,startPosition.rotation);
            }
            else
            {
                item.up = prior;
                prior.down = item;
                if (autoArrange)
                    item.gameObject.transform.SetPositionAndRotation(prior.gameObject.transform.position, prior.gameObject.transform.rotation);
                if (autoArrange)
                    item.gameObject.transform.Translate(stepLength);
                prior = item;
            }
        }
        prior.down = first;
        first.up = prior;
    }
}
