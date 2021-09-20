using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleBuyerController : MonoBehaviour
{
    private Image[] theImages;
    [SerializeField]
    private Sprite happySprite;
    [SerializeField]
    private Sprite unappySprite;


    // Start is called before the first frame update
    void Awake()
    {
        theImages = GetComponentsInChildren<Image>();
    }

    public void setImages(Sprite[] theImagesToSet)
    {
        for(int i = 0; i < theImagesToSet.Length; i++)
        {
            theImages[i].sprite = theImagesToSet[i];
            Color tmpCol = new Color(1, 1, 1, 1);
            theImages[i].color = tmpCol;
        }
    }

    public void normalizeAll()
    {
        for (int i = 0; i < theImages.Length; i++)
        {
            theImages[i].sprite = null;
            Color tmpCol = new Color(1, 1, 1, 0);
            theImages[i].color = tmpCol;
        }
    }

    public void happyOrNot(bool isHappy)
    {
        for(int i = 0; i < theImages.Length; i++)
        {
            theImages[i].sprite = null;
            Color tmpCol1 = new Color(1, 1, 1, 0);
            theImages[i].color = tmpCol1;
        }
        Color tmpCol = new Color(1, 1, 1, 1);
        theImages[1].color = tmpCol;
        if (isHappy)
        {
            theImages[1].sprite = happySprite;
        }
        else
        {
            theImages[1].sprite = unappySprite;
        }
    }
}
