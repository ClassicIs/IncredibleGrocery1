using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    int money;
    [SerializeField]
    Text theMoney;
    [SerializeField]
    GameObject menu;

    [SerializeField]
    Sprite ButtonOn;
    [SerializeField]
    Sprite ButtonOff;
    [SerializeField]
    GameObject MusicButton;
    [SerializeField]
    GameObject SFXButton;
    bool isButtonSFXOn;
    bool isButtonMusicOn;
    List <string> choosenItems;
    int quantOfItems;
    List <string> sellerItems;
    [SerializeField]
    GameObject theStorage;
    Animator theStorageAnim;

    [SerializeField]
    GameObject thePlayerBubble;
    
    BubbleBuyerController playerBubbleController;
    [SerializeField]
    GameObject theBuyer;

    BuyerScript theBuyerScript;
    ButtonValues[] allSlots;

    [SerializeField]
    GameObject sellerBubble;
    ScriptForSellBubble[] sellerBubbleIMG;

    [SerializeField]
    Fruit[] theFruitsOne;
    Fruit[] theFruitsBuyer;
    [SerializeField]
    GameObject AudioScriptHolder;
    AudioManagerScript theAudioScript;

    private bool hasFillBasket;
    [SerializeField]
    Button sellButton;
    [SerializeField]
    int correctMoney;

    void Start()
    {
        correctMoney = 10;
        //quantOfItems = 3;
        money = 0;
        theMoney.text = "$ " + money.ToString();
        isButtonSFXOn = true;
        isButtonMusicOn = true;
        hasFillBasket = false;
        

    }

    private void Awake()
    {
        theAudioScript = AudioScriptHolder.GetComponent<AudioManagerScript>();
        theBuyerScript = theBuyer.GetComponentInChildren<BuyerScript>();
        theStorageAnim = theStorage.GetComponentInChildren<Animator>();
        
        sellerItems = new List<string>();
        choosenItems = new List<string>();
        playerBubbleController = thePlayerBubble.GetComponent<BubbleBuyerController>();
        allSlots = theStorage.GetComponentsInChildren<ButtonValues>();
        
    }

    public void menuFunc()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }        
    }

    public void changeMoney(int moneyToAdd)
    {
        theAudioScript.playSound("moneySound");
        money = money + moneyToAdd;
        theMoney.text = "$ " + money.ToString();
    }

    public void musicButtonFunc()
    {
        theAudioScript.playSound("buttonClicked");
        isButtonMusicOn = !isButtonMusicOn;
        buttonSwitch(isButtonMusicOn, MusicButton);
        theAudioScript.turnMusic(isButtonMusicOn);
        //Tell AudioListener to turn off SFX
    }

    public void SfxButtonFunc()
    {
        theAudioScript.playSound("buttonClicked");
        isButtonSFXOn = !isButtonSFXOn;
        buttonSwitch(isButtonSFXOn, SFXButton);
        theAudioScript.turnSFX(isButtonSFXOn);

        //Tell AudioListener to turn off SFX
    }

    void fillInStorage()
    {        
        for (int i = 0; i < allSlots.Length; i++)
        {
            allSlots[i].addItem(theFruitsOne[i].nameOfTheFruit, theFruitsOne[i].theFruit);
        }

    }

    void buttonSwitch(bool isItOn, GameObject buttonToChange)
    {
        theAudioScript.playSound("buttonClicked");
        if (isItOn)
        {
            buttonToChange.GetComponent<Image>().sprite = ButtonOn;
        }
        else
        {
            buttonToChange.GetComponent<Image>().sprite = ButtonOff;
        }
    }

    public void settingsButton()
    {
        if (menu.activeSelf)
        {
            menu.SetActive(false);
        }
        else
        {
            menu.SetActive(true);
        }
    }

    public bool isThereEnoughSpace()
    {
        if((sellerItems == null) || (quantOfItems > sellerItems.Count))
        {
            theAudioScript.playSound("productSelected");
            return true;
        }
        else
        {            
            return false;
        }
    }

    public void toChooseItem(string itemToAdd)
    {
        Debug.Log("An item: " + itemToAdd);
        sellerItems.Add(itemToAdd);
        if (sellerItems != null)
        {
            if (quantOfItems == sellerItems.Count)
            {
                hasFillBasket = true;
                sellButton.interactable = true;
            }
        }
        Debug.Log("Added an item: " + sellerItems);
    }
    public void removeItem(string itemToRemove)
    {
        sellerItems.Remove(itemToRemove);
        if(hasFillBasket)
        {
            hasFillBasket = false;
            sellButton.interactable = false;
        }
        Debug.Log("Removed an item: " + sellerItems);
    }

    void buyerToChoose()
    {
        int randVal = Random.Range(1, 4);

        Debug.Log("Random value equals: " + randVal);
        theFruitsBuyer = new Fruit[randVal];
        quantOfItems = randVal;
        int[] whatBuyerChoosen = new int [randVal];
        for(int i = 0; i < randVal; i++)
        {
            whatBuyerChoosen[i] = Random.Range(0, 16);

            for (int j = 0; j < i; j++)
            {
                if(whatBuyerChoosen[i] == whatBuyerChoosen[j])
                {
                    i--;
                    continue;
                }
            }
        }
        Sprite[] fruitsBuyerBasket = new Sprite[randVal];
        for (int i = 0; i < randVal; i++)
        {
            theFruitsBuyer[i] = theFruitsOne[whatBuyerChoosen[i]];
            Debug.Log(i + " fruit is " + theFruitsBuyer[i].nameOfTheFruit);
            fruitsBuyerBasket[i] = theFruitsBuyer[i].theFruit;
        }
        theAudioScript.playSound("bubbleOn");
        playerBubbleController.setImages(fruitsBuyerBasket);
    }

    public void startTheCycle()
    {
        fillInStorage();
        thePlayerBubble.GetComponentInParent<Image>().enabled = true;
        thePlayerBubble.SetActive(true);

        buyerToChoose();
        
        menuAnimation(true);
    }

    public void endOfCycle()
    {
        quantOfItems = 0;
        //theFruitsBuyer

        choosenItems.Clear();
        
        sellerItems.Clear();
        //sellerItems.Add(itemToAdd);
        playerBubbleController.normalizeAll();
        for(int i = 0; i < sellerBubbleIMG.Length; i++)
        {
            sellerBubbleIMG[i].normalizeAll();
        }
        for (int i = 0; i < allSlots.Length; i++)
        {
            allSlots[i].toUncooseTheItem();
        }

        hasFillBasket = false;

        theAudioScript.playSound("bubbleOff");

        thePlayerBubble.GetComponentInParent<Image>().enabled = false;
        thePlayerBubble.SetActive(false);

        sellerBubble.GetComponentInParent<Image>().enabled = false;
        sellerBubble.SetActive(false);
    }
    private void menuAnimation(bool goInOrOut)
    {
        theStorageAnim.SetBool("GoIn", goInOrOut);
    }

    public void sellTheProducts()
    {
        theAudioScript.playSound("buttonClicked");
        sellButton.interactable = false;
        menuAnimation(false);
        bool[] hasChoosenRight = new bool [quantOfItems];
        for(int i = 0; i < quantOfItems; i++)
        {
            for (int j = 0; j < quantOfItems; j++)
            {
                if (theFruitsBuyer[j].nameOfTheFruit == sellerItems[i])
                {
                    hasChoosenRight[i] = true;
                    break;
                }
                else
                {
                    hasChoosenRight[i] = false;
                }
            }
        }
        Debug.Log("All answers: \n" + hasChoosenRight);

        bool allRight = true;
        for(int i = 0; i < hasChoosenRight.Length; i++)
        {
            if(!hasChoosenRight[i])
            {
                allRight = false;
                break;
            }
        }

        if(allRight)
        {
            changeMoney(correctMoney);
            Debug.Log("All answers are correct");
        }
        else
        {
            Debug.LogWarning("All answers are not correct!");
        }

        Sprite[] theSellerChoiceSprites = new Sprite[sellerItems.Count];

        for (int k = 0; k < sellerItems.Count; k++)
        {
            for (int i = 0; i < 16; i++)
            {
                if (theFruitsOne[i].nameOfTheFruit == sellerItems[k])
                {
                    theSellerChoiceSprites[k] = theFruitsOne[i].theFruit;
                    break;
                }
            }
        }

        sellerBubble.GetComponentInParent<Image>().enabled = true;
        sellerBubble.SetActive(true);
        theAudioScript.playSound("bubbleOn");

        sellerBubbleIMG = sellerBubble.GetComponentsInChildren<ScriptForSellBubble>();

        
        for (int i = 0; i < hasChoosenRight.Length; i++)
        {
            sellerBubbleIMG[i].DrawSellerChoice(hasChoosenRight[i], theSellerChoiceSprites[i]);
        }
        StartCoroutine(reaction(allRight));

    }

    IEnumerator reaction(bool boolForReact)
    {
        playerBubbleController.happyOrNot(boolForReact);
        yield return new WaitForSeconds(5);
        theBuyerScript.walkOut();
        StartCoroutine(resetTheCycle());
    }
    IEnumerator resetTheCycle()
    {
        endOfCycle();
        yield return new WaitForSeconds(5);

        theBuyer.SetActive(false);
        yield return new WaitForSeconds(3);
        theBuyer.SetActive(true);
    }

}
