using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour {

    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] QuestionSO question;
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    [SerializeField] Sprite defualtAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;

    void Start() {
        GetNextQuestion();
    }

    public void OnAnswerSelected(int index) {
        Image buttonImage = answerButtons[index].GetComponent<Image>();
        if (index == question.GetCorrectAnswerIndex()) {
            questionText.text = "Correct";
            buttonImage.color = new Color32(0, 255, 47, 255);
        } else {
            questionText.text = "Your fuckin ass got it wrong";
            buttonImage.color = new Color32(255, 0, 0, 255);
        }
        SetButtonState(false);
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
