using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ScriptForSellBubble : MonoBehaviour
{
    [SerializeField]
    GameObject okSign;
    [SerializeField]
    GameObject notOkSign;
    Image theImage;

    private void OnEnable()
    {
        theImage = GetComponent<Image>();
    }

    public void normalizeAll()
    {
        if(notOkSign.activeSelf)
        {
            notOkSign.SetActive(false);
        }
        if (okSign.activeSelf)
        {
            okSign.SetActive(false);
        }
        theImage.sprite = null;

    }

    public void DrawSellerChoice(bool rightOrWrong, Sprite theSprites)
    {
        Debug.Log("It is " + rightOrWrong);
        if (rightOrWrong)
        {
            okSign.SetActive(true);
        }
        else
        {
            notOkSign.SetActive(true);
        }

        theImage.sprite = theSprites;
    }

}
