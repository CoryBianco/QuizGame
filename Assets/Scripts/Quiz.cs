using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour {

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions;
    QuestionSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Buttons")]
    [SerializeField] Sprite defualtAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI score;
    ScoreKeeper scoreKeeper;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    public bool isComplete = false;

    void Start() {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = questions.Count;
        progressBar.value = 0;
    }

    void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion) {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        } else if (!hasAnsweredEarly && !timer.isAnsweringQuestion) {
            DisplayAnswer(-1);
            SetButtonState(false);
        }
    }

    void DisplayAnswer(int index) {
        Image buttonImage = answerButtons[index].GetComponent<Image>();
        if (index == currentQuestion.GetCorrectAnswerIndex()) {
            questionText.text = "Correct";
            buttonImage.color = new Color32(0, 255, 47, 255);
            scoreKeeper.IncrementCorrectAnswers();
        } else {
            questionText.text = "Your fuckin ass got it wrong";
            buttonImage.color = new Color32(255, 0, 0, 255);
        }
    }

    public void OnAnswerSelected(int index) {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.Canceltimer();
        score.text = "Score: " + scoreKeeper.CalculateScore() + "%";

        if (progressBar.value == progressBar.maxValue) {
            isComplete = true;
        }
    }

    void GetNextQuestion() {
        if (questions.Count > 0) {
            SetButtonState(true);
            SetDefaultButtonColors();
            GetRandomQuestion();
            DisplayQuestion();
            progressBar.value++;
            scoreKeeper.IncrementSeenQuestions();
        }
    }

    void GetRandomQuestion() {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if (questions.Contains(currentQuestion)) {
            questions.Remove(currentQuestion);
        }

    }

    public void DisplayQuestion() {
        questionText.text = currentQuestion.GetQuestionText();
        TextMeshProUGUI buttonText;

        int index = 0;
        foreach (GameObject button in answerButtons) {
            buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(index);
            index++;
        }
    }

    void SetButtonState(bool state) {
        foreach (GameObject button in answerButtons) {
            button.GetComponent<Button>().interactable = state;
        }
    }

    void SetDefaultButtonColors() {
        foreach (GameObject button in answerButtons) {
            button.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

}
