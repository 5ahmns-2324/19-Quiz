using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public QuestionManager QuestionManager;
    public GameObject ErgebnissCanvas;
    public GameObject Questioncanvas;
    
    void Start()
    {
        int endCount = QuestionManager.index;
        bool nextButtonpressed = QuestionManager.isButtonPressed;

        if (endCount == 5 && nextButtonpressed == true)
        {
            ErgebnissCanvas.SetActive(true);
            Questioncanvas.SetActive(false);
        }

    }

    
    void Update()
    {
        
    }
}
