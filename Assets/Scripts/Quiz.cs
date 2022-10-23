using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour {

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;

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

    void Start() {
        timer = FindObjectOfType<Timer>();
        GetNextQuestion();
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
        if (index == question.GetCorrectAnswerIndex()) {
            questionText.text = "Correct";
            buttonImage.color = new Color32(0, 255, 47, 255);
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
    }

    void GetNextQuestion() {
        SetButtonState(true);
        SetDefaultButtonColors();
        DisplayQuestion();
    }

    public void DisplayQuestion() {
        questionText.text = question.GetQuestionText();
        TextMeshProUGUI buttonText;

        int index = 0;
        foreach (GameObject button in answerButtons) {
            buttonText = button.GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(index);
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
