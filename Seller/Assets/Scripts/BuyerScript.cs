using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyerScript : MonoBehaviour
{
    [SerializeField]
    Transform strPos;
    [SerializeField]
    Transform endPos;
    [SerializeField]
    float speed;
    Animator theAnimContr;
    SpriteRenderer theBuyerRend;
    [SerializeField]
    GameObject theGMHolder;
    GameManagerScript theGMScript;

    // Start is called before the first frame update
    void Awake()
    {
        theGMScript = theGMHolder.GetComponent<GameManagerScript>();
        theAnimContr = GetComponentInChildren<Animator>();
        theBuyerRend = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {        
        StartCoroutine(theWalk(true, endPos.position));
    }

    public void walkOut()
    {
        StartCoroutine(theWalk(false, strPos.position));
    }

    IEnumerator theWalk(bool outOrIn, Vector2 theDistination)
    {
        Debug.Log("Start of Coroutine");
        if(outOrIn)
        {
            theBuyerRend.flipX = false;
            //Animation of fade in
        }
        else
        {
            theBuyerRend.flipX = true;
        }
        theAnimContr.SetBool("isWalking", true);
        while (Vector2.Distance(transform.position, theDistination) > 0.2f)
        {
            transform.position = Vector2.Lerp(transform.position, theDistination, speed/10);
            yield return null;
        }
        theAnimContr.SetBool("isWalking", false);
        if (outOrIn)
        {
            yield return new WaitForSeconds(2);
            theGMScript.startTheCycle();
        }
        
        /*if (!outOrIn)
        {
            //Animation of fade out
            gameObject.SetActive(false);
        }*/
        Debug.Log("End of Coroutine");
    }
}
