using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Answers : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager qM;
    public void Answer(){
        if(isCorrect){
            Debug.Log("Correct");
            qM.answeredCorrectly();
        }
                else{
            Debug.Log("Incorrect");
            qM.answeredCorrectly();
        }
    }
}
