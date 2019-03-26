using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    public TMP_Text questionDisplayText;
    public TMP_Text scoreDisplayText;
    public TMP_Text timeRemainingDisplayText;
    public SimpleObjectPool answerButtonObjectPool;
    public Transform answerButtonParent;
    public GameObject questionDisplay;
    public GameObject roundEndDisplay;
    public GameObject prefab;
    public Transform spawnPoint;

    private GameObject ticket;
    private PlayerMovement playerMovement;
    private DataController dataController;
    private RoundData currentRoundData;
    private QuestionData[] questionPool;
    private bool isRoundActive;
    private float timeRemaining;
    private int questionIndex;
    private int playerScore;
    private List<GameObject> answerButtonGameObjects = new List<GameObject>();

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        scoreDisplayText.GetComponent<TextMeshProUGUI>().enabled = false;
        timeRemainingDisplayText.GetComponent<TextMeshProUGUI>().enabled = false;
        dataController = FindObjectOfType<DataController>();

        //timeRemaining = currentRoundData.timeLimitInSeconds;
    }

    void Update()
    {

        if (isRoundActive)
        {
            playerMovement.PlayerStartedQuestions(true);
            timeRemaining -= Time.deltaTime;
            UpdateTimeRemainingDisplay();
            if(timeRemaining <= 0f)
            {
                EndRound();
            }
        }
        playerMovement.PlayerStartedQuestions(false);
    }

    #region Functions
    public void StartTheGame()
    {
        //Reset the score and its text
        playerScore = 0;
        scoreDisplayText.text = "Questions Correctly Answered: " + playerScore.ToString();
        //Reset the question index so we start from the start again
        questionIndex = 0;
        currentRoundData = dataController.GetCurrentRoundData();
        questionPool = currentRoundData.questions;
        ShowQuestion();
        //Enable the GUI for score and timer.
        scoreDisplayText.GetComponent<TextMeshProUGUI>().enabled = true;
        timeRemainingDisplayText.GetComponent<TextMeshProUGUI>().enabled = true;

        //reset the time remaining and start the counter.
        timeRemaining = currentRoundData.timeLimitInSeconds;
        UpdateTimeRemainingDisplay();
        isRoundActive = true;
    }

    private void UpdateTimeRemainingDisplay()
    {
        timeRemainingDisplayText.text = "Time: " + Mathf.Round(timeRemaining).ToString();
    }

    private void ShowQuestion()
    {
        RemoveAnswerButtons(); //removing old answers so that we can display the new questions.
        QuestionData questionData = questionPool[questionIndex];
        questionDisplayText.text = questionData.questionText; //displaying the question

        for (int i = 0; i < questionData.answers.Length; i++) //displaying the buttons
        {
            GameObject answerButtonGameObject = answerButtonObjectPool.GetObject();
            answerButtonGameObjects.Add(answerButtonGameObject);
            answerButtonGameObject.transform.SetParent(answerButtonParent);

            AnswerButton answerButton = answerButtonGameObject.GetComponent<AnswerButton>();
            answerButton.Setup(questionData.answers[i]);
        }
    }

    //used to remove buttons
    private void RemoveAnswerButtons()
    {
        while (answerButtonGameObjects.Count > 0)
        {
            answerButtonObjectPool.ReturnObject(answerButtonGameObjects[0]);
            answerButtonGameObjects.RemoveAt(0);
        }
    }
    //checks whether the answer that is pressed is correct or not.
    public void AnswerButtonClicked(bool isCorrect)
    {
        if (isCorrect)
        {
            playerScore += currentRoundData.pointsAddedForCorrectAnswer;
            scoreDisplayText.text = "Questions Correctly Answered: " + playerScore.ToString();
        }

        if (questionPool.Length > questionIndex + 1)
        {
            questionIndex++;
            ShowQuestion();
        }
        else
        {
            EndRound();
        }

    }

    public void EndRound()
    {
        isRoundActive = false;

        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(true);

        if(playerScore >= 6) //give them the ticket.
        {
            ticket = Instantiate(prefab, spawnPoint) as GameObject;
            ticket.transform.position = spawnPoint.position;
        }

    }

    public void ReturnToGame()
    {
        questionDisplay.SetActive(false);
        roundEndDisplay.SetActive(false);
        scoreDisplayText.GetComponent<TextMeshProUGUI>().enabled = false;
        timeRemainingDisplayText.GetComponent<TextMeshProUGUI>().enabled = false;
    }
    #endregion
}
