using UnityEngine;
using UnityEngine.UI;


public class ButtonValues : MonoBehaviour
{
    [SerializeField]
    private GameObject theGameManager;
    private GameManagerScript thGMScript;
    [SerializeField]
    private GameObject OKBadge;
    private Image theImage;

    private bool isButtonOn = false;

    public string buttonName;

    void Start()
    {
        thGMScript = theGameManager.GetComponent<GameManagerScript>();
        theImage = GetComponent<Image>();
    }
    

    public void itemIsChoosen()
    {        
        if (isButtonOn)
        {
            toUncooseTheItem();
        }
        else
        {
            if (canBeChosen())
            {
                toChooseTheItem();
            }
            else
            {
                Debug.LogWarning("There's no space for another object!");
            }
        }        
    }

    public void addItem(string theName, Sprite fruitImage)
    {
        buttonName = theName;
        if (fruitImage != null)
        {
            theImage.sprite = fruitImage;
        }
        else
        {
            Debug.LogWarning("The image is missing.");
        }
    }

    bool canBeChosen()
    {
        bool canChoose = thGMScript.isThereEnoughSpace();
        return canChoose;
    }

    private void toChooseTheItem()
    {
        OKBadge.SetActive(true);
        thGMScript.toChooseItem(buttonName);
        isButtonOn = true;
    }

    public void toUncooseTheItem()
    {
        OKBadge.SetActive(false);
        isButtonOn = false;
        thGMScript.removeItem(buttonName);
    }
}
