using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;

    public int CurrentQuestion;

    public TextMeshProUGUI QuestionTxt;

    public void answeredCorrectly(){
        QnA.RemoveAt(CurrentQuestion);
        generateQuestions();
    }
    private void Start(){
        generateQuestions();
    }

    void setAnswers(){
        for (int i = 0; i < options.Length; i++){
            options[i].GetComponent<Answers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[CurrentQuestion].answers[i];
            if(QnA[CurrentQuestion].correctAnswer == i+1){
                options[i].GetComponent<Answers>().isCorrect = true;
            }
        }
    }
    void generateQuestions(){
            if (QnA.Count == 0) {
        Debug.Log("No questions left!");
        return; // You might want to handle this scenario more gracefully
    }
        CurrentQuestion = Random.Range(0, QnA.Count);
        QuestionTxt.text = QnA[CurrentQuestion].question;
        setAnswers();
    }
}
