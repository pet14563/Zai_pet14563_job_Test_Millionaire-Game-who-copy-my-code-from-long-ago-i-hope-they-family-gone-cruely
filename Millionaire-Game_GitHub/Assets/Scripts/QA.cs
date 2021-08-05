using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QA : MonoBehaviour
{
    public Animator anim;

    public Text scoreText;
    public Text scoreText2;
    public Text scoreText3;

    public GameObject conversationImg;
    public Text conversationText;
    public float conversationTime;

    public GameObject[] wrongAnswer;
    public GameObject[] correctAnswer;

    public GameObject[] wrongAnswerAsk;
    public GameObject[] correctAnswerAsk;


    public int score;

    public float timeLimit;
    public float maxtimeLimit;
    public Image timeLimitBar;

    public bool startQuestion;

    public int[] questionStore; // for store the question number
    private int questionNumber; // the question number that we will random put in the store

    public int currentQuestion; // the question that we are doing

    public GameObject[] QuestionText;

    public GameObject halperTimebutton;
    public GameObject halperWrongbutton;
    public GameObject helperAskbutton;


    public Menu menuScirpts;
    // Start is called before the first frame update
    void Start()
    {
        questionStore = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // we have 10 question
        maxtimeLimit = 120;
        timeLimitBar.GetComponent<Image>();
        ResetGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (conversationImg.activeSelf)
        {
            conversationTime -= Time.deltaTime;
            if (conversationTime <= 0)
            {
                conversationImg.SetActive(false);
            }
        }
        if (startQuestion == true)
        {
            timeLimitBar.fillAmount = timeLimit / maxtimeLimit;
            timeLimit -= Time.deltaTime;
            if (timeLimit <= 0)
            {
                WrongAnswer();
            }
        }
    }

    public void ConversationStart(float time , bool correctAnswerText , bool wrongAnswerText , bool welcomeText)
    {
        int randomchat = Random.Range(1,4); // 1 2 3
        conversationImg.SetActive(true);
        conversationTime = time; /// time for active conversation panel
        if (correctAnswerText == true)
        {
            if (score == 9)
            {
                randomchat = 0;
                conversationText.text = "YOU ARE THE WINNER!";
            }
            if (randomchat == 1)
            {
                conversationText.text = "Good job!! You are correct. Keep it up!";
            }
            if (randomchat == 2)
            {
                conversationText.text = "You are correct!. Excellent!!";
            }
            if (randomchat == 3)
            {
                conversationText.text = "Correct!. That nice";
            }
        }
        if (wrongAnswerText == true)
        {
            AnimationTrigger(4);
            if (randomchat == 1)
            {
                conversationText.text = "Ohww. you pick the wrong choice. But you can play again next time!";
            }
            if (randomchat == 2)
            {
                conversationText.text = "You are lose... Heheh. be more carefully next time. Okay?..";
            }
            if (randomchat == 3)
            {
                conversationText.text = "Don't give up!. Be better next time!!";
            }
        }
        if (welcomeText == true)
        {
            if (randomchat == 1)
            {
                conversationText.text = "Hello! My name is Emilly. I will be your guide here.";
            }
            if (randomchat == 2)
            {
                conversationText.text = "Hello! there i'm Emilly Feel free to ask me anything";
            }
            if (randomchat == 3)
            {
                conversationText.text = "Hi! i'm Emilly The game will begin sooner. you better be ready. okay?. Heheh...";
            }

        }
    }
    public void AnimationTrigger(int animNumber)
    {
        if (animNumber == 1)
        {
            anim.SetTrigger("IdleAnimation");
        }
        if (animNumber == 2)
        {
            anim.SetTrigger("closeeyeAnimation");
        }
        if (animNumber == 3)
        {
            anim.SetTrigger("smirkAnimation");
        }
        if (animNumber == 4)
        {
            anim.SetTrigger("sadAnimation");
        }
    }

    public void ResetGame()
    {
        score = 0;
        conversationTime = 0;
        //QuestionText[0].SetActive(false);
        //startQuestion = false;
        halperTimebutton.SetActive(true);
        halperWrongbutton.SetActive(true);
        helperAskbutton.SetActive(true);
        currentQuestion = 0; // make it reset to the first question in the store
        RandomQustion();
    }

    public void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        scoreText2.text = score + " / 10";
        scoreText3.text = score + " / 10";
    }

    public void HelperAddTime()
    {
        ConversationStart(20 , false , false , false);
        timeLimit += 60;
        halperTimebutton.SetActive(false);
        conversationText.text = "Aight! add time option. Right away!";
    }
    public void HelperAsk()
    {
        ConversationStart(20, false, false, false);
        int randomAnswer = Random.Range(1,6);
        print("you get randomAnswer = " + randomAnswer);
        helperAskbutton.SetActive(false);
        correctAnswerAsk[0] = GameObject.FindGameObjectWithTag("CorrectAnswer");
        wrongAnswerAsk[0] = GameObject.FindGameObjectWithTag("WrongAnswer");
        if (randomAnswer >= 1 && randomAnswer <= 2)
        {
            conversationText.text = "The correct Answer is ''" + correctAnswerAsk[0].name + "'' I'am very sure!!";
        }
        if (randomAnswer >= 3 && randomAnswer <= 4)
        {
            AnimationTrigger(2);
            conversationText.text = "Hmmm.... i'am not sure. but i think ''" + wrongAnswerAsk[0].name + "'' is correct... i guess..";
        }
        if (randomAnswer == 5)
        {
            AnimationTrigger(4);
            conversationText.text = "Sorry..... I don't know anything of this question. gomennasai!!!! >,<'";
        }

    }
    public void HelperDeleteWrong()
    {
        AnimationTrigger(3);
        ConversationStart(20, false, false, false);
        halperWrongbutton.SetActive(false);
        wrongAnswer = GameObject.FindGameObjectsWithTag("WrongAnswer");
        wrongAnswer[0].SetActive(false);
        wrongAnswer[1].SetActive(false);
        conversationText.text = "Hehe... Okay, I'll delete 2 wrong answers. Make sure you pick the right choice!";
    }

    public void RandomQustion() /// random question in the array that we already have 1-10 question in the store
    {
        print("you are doing random question");
        for (int arrayPosition = 0; arrayPosition < questionStore.Length; ++arrayPosition)
        {
            questionNumber = questionStore[arrayPosition];
            int randomlizeArray = Random.Range(0, arrayPosition);
            questionStore[arrayPosition] = questionStore[randomlizeArray];
            questionStore[randomlizeArray] = questionNumber;
        }
    }

    public void StartQuestion()
    {
        timeLimit = 60;
        if (wrongAnswer[0] != null && wrongAnswer[1] != null && wrongAnswer[2] != null)
        {
            wrongAnswer[0].SetActive(true);
            wrongAnswer[1].SetActive(true);
            wrongAnswer[2].SetActive(true);
        }
        /// reset delete -2
        startQuestion = true;
        if (score >= 0 && score <= 9)
        {
            if (questionStore[currentQuestion] == 0)
            {
                QuestionText[0].SetActive(true);
            }
            else
            {
                QuestionText[0].SetActive(false);
            }
            ///------------------------------------

            if (questionStore[currentQuestion] == 1)
            {
                QuestionText[1].SetActive(true);
            }
            else
            {
                QuestionText[1].SetActive(false);
            }
            ///------------------------------------
            
            if (questionStore[currentQuestion] == 2)
            {
                QuestionText[2].SetActive(true);
            }
            else
            {
                QuestionText[2].SetActive(false);
            }
            ///------------------------------------
            if (questionStore[currentQuestion] == 3)
            {
                QuestionText[3].SetActive(true);
            }
            else
            {
                QuestionText[3].SetActive(false);
            }
            ///------------------------------------
            if (questionStore[currentQuestion] == 4)
            {
                QuestionText[4].SetActive(true);
            }
            else
            {
                QuestionText[4].SetActive(false);
            }
            ///------------------------------------
            if (questionStore[currentQuestion] == 5)
            {
                QuestionText[5].SetActive(true);
            }
            else
            {
                QuestionText[5].SetActive(false);
            }
            ///------------------------------------
            if (questionStore[currentQuestion] == 6)
            {
                QuestionText[6].SetActive(true);
            }
            else
            {
                QuestionText[6].SetActive(false);
            }
            ///------------------------------------
            if (questionStore[currentQuestion] == 7)
            {
                QuestionText[7].SetActive(true);
            }
            else
            {
                QuestionText[7].SetActive(false);
            }
            ///------------------------------------
            if (questionStore[currentQuestion] == 8)
            {
                QuestionText[8].SetActive(true);
            }
            else
            {
                QuestionText[8].SetActive(false);
            }
            ///------------------------------------
            if (questionStore[currentQuestion] == 9)
            {
                QuestionText[9].SetActive(true);
            }
            else
            {
                QuestionText[9].SetActive(false);
            }
            ///------------------------------------
        }

    }

    public void CorrectAnswer()
    {
        ConversationStart(15 , true , false, false);
        score++;
        currentQuestion++;
        startQuestion = false;
        StartQuestion();
        if (score == 10)
        {
            menuScirpts.YouWin();
            startQuestion = false;
        }
        UpdateScore();
    }
    public void WrongAnswer()
    {
        AnimationTrigger(4);
        ConversationStart(15, false, true, false);
        menuScirpts.YouLose();
        startQuestion = false;
        UpdateScore();
    }

    //IEnumerator waitTenSeconds()
    //{
    //    yield return new WaitForSeconds(10);
    //    print("you are waitting 10 sec");
    //}
}
