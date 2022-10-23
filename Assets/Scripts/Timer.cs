using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

    [SerializeField] float questionTime = 30f;
    [SerializeField] float postAnswerTime = 5f;

    public bool loadNextQuestion;
    public bool isAnsweringQuestion = false;
    public float fillFraction;

    float timerValue;

    void Update() {
        UpdateTimer();
    }

    void UpdateTimer() {
        timerValue -= Time.deltaTime;

        if (isAnsweringQuestion) {
            if (timerValue > 0) {
                fillFraction = timerValue / questionTime;
            } else {
                isAnsweringQuestion = false;
                timerValue = postAnswerTime;
            }
        } else {
            if (timerValue > 0) {
                fillFraction = timerValue / postAnswerTime;
            } else {
                loadNextQuestion = true;
                isAnsweringQuestion = true;
                timerValue = questionTime;
            }
        }

        Debug.Log(isAnsweringQuestion + ": " + timerValue + " = " + fillFraction);
    }
}
