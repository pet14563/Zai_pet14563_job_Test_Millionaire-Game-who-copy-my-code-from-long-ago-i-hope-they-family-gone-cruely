using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    public QA qaScripts;

    public GameObject Character;

    public Transform charMenuPosition;
    public Transform charGameplayPosition;

    public GameObject pressPlayButton;
    public GameObject pressPlayButtonRed;

    public GameObject menuPanel;
    public GameObject gameplayPanel;
    public GameObject winPopup;
    public GameObject losePopup;
    public GameObject introPanel;

    public bool canplay;


    // Start is called before the first frame update
    void Start()
    {
        qaScripts = GetComponent<QA>();
        Character.transform.position = charMenuPosition.transform.position;
        MenuActiveController(true, false, false, false , false);

    }

    // Update is called once per frame
    void Update()
    {
        if (canplay == true)
        {
            pressPlayButton.SetActive(true);
            pressPlayButtonRed.SetActive(false);
        }
        else
        {
            pressPlayButton.SetActive(false);
            pressPlayButtonRed.SetActive(true);
        }
    }

    public void MenuActiveController(bool activeMenuPanel, bool activeGameplayPanel, bool activeWinPanel, bool activeLosePanel , bool activeIntroPanel)
    {
        menuPanel.SetActive(activeMenuPanel);
        gameplayPanel.SetActive(activeGameplayPanel);
        winPopup.SetActive(activeWinPanel);
        losePopup.SetActive(activeLosePanel);
        introPanel.SetActive(activeIntroPanel);
    }

    public void PressPlay()
    {
        if (canplay == true)
        {
            Character.transform.position = charGameplayPosition.transform.position;// move girl to charGameplayPosition
            MenuActiveController(false, false, false, false, true);
            qaScripts.RandomQustion();
            qaScripts.ConversationStart(20, false, false, true);
        }
    }
    public void PressReady()
    {
        qaScripts.ConversationStart(20, false, false, false);
        MenuActiveController(false, true, false, false, false);
        qaScripts.StartQuestion(); // if press play timelimit to answer the question will start and question will pop up
        qaScripts.ConversationStart(20, false, false, false);
        qaScripts.conversationText.text = "The Game Begine!!!";
    }
    public void PressHowToPlay()
    {
        qaScripts.AnimationTrigger(2);
        qaScripts.ConversationStart(20, false, false, false);
        qaScripts.conversationText.text = "Simple. Pick the right choice 10 time and you will win this game";
    }
    public void PressHowCanYouHelp()
    {
        qaScripts.AnimationTrigger(2);
        qaScripts.ConversationStart(20, false, false, false);
        qaScripts.conversationText.text = "Added more time or Delete 2 wrong choice or Try to ask me. if i can.";
    }
    public void PressGiveMeMinute()
    {
        qaScripts.ConversationStart(20, false, false, false);
        qaScripts.conversationText.text = "Alright!.";
    }

    public void YouWin()
    {
        MenuActiveController(false, false, true, false ,false);
    }
    public void YouLose()
    {
        MenuActiveController(false, false, false, true, false);

    }
    public void PressNext()
    {
        MenuActiveController(true, false, false, false, false);
        Character.transform.position = charMenuPosition.transform.position;  // move girl to charMenuPosition
        qaScripts.ResetGame();


    }
}
