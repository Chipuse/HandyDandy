using UnityEngine;


public class LevelChanger : MonoBehaviour
{
    private static LevelChanger _instanceLC;
    public static LevelChanger Instance { get { return _instanceLC; } }
    

    public Animator animator;
    public Management management;
    // Update is called once per frame
    void Awake()
    {
        if (_instanceLC != null && _instanceLC != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instanceLC = this;
        }

        animator = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    public void AnimFinished()
    {
        Management.LoadNextScene();
    }

    public void FadeToLevel()
    {
        
        animator.SetTrigger("FadeOut");
        animator.ResetTrigger("FadeIn");
    }
    public void FadeInLevel()
    {
        animator.SetTrigger("FadeIn");
        animator.ResetTrigger("FadeOut");
    }
    public void FadeToLevelTwo()
    {
        animator.SetTrigger("FadeOut");
    }
    public void FadeInLevelTwo()
    {
        animator.SetTrigger("FadeIn");
    }
}
