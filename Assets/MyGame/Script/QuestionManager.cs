using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;

public class QuestionManager : MonoBehaviour
{
    public int questionAmount;
    public Question fragen;
    public List<Question> allQuestion;
    public List<Question> selectedQuestion;
    public int index = 0;

    public GameObject bild;
    public GameObject antwortButton1;
    public GameObject antwortButton2;
    public GameObject antwortButton3;
    public GameObject nextButton;
    public Button pressedbutton;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answer1Text;
    public TextMeshProUGUI answer2Text;
    public TextMeshProUGUI answer3Text;
    public TextMeshProUGUI countNumber;
    

    public bool isButtonPressed = false;

    public TextMeshProUGUI timerText;
    public float timerDuration = 10f;
    private float currentTimer;

    public AudioSource klingeln;


    void Start()
    {
        
        selectedQuestion = allQuestion.ToList();

        /*for (int i = 0; i < allQuestion.Count - questionAmount; i++)
        {
            int index = Random.Range(0, selectedQuestion.Count);
            selectedQuestion.RemoveAt(index);
        }*/

        selectedQuestion.Shuffle();
        SetQuestion();
        StartTimer();

        klingeln.loop = true;
        klingeln.Play();

    }

    public void SetQuestion()
    {
        Question currentQuestion = selectedQuestion[index];
        questionText.text = currentQuestion.frageText;
        answer1Text.text = currentQuestion.antwort1;
        answer2Text.text = currentQuestion.antwort2;
        answer3Text.text = currentQuestion.antwort3;


        StartTimer();
    }

    public void CheckAnswer(int number)
    {
        Question currentQuestion = selectedQuestion[index];

        if (number == currentQuestion.richtigeAntwort)
        {
            SetColorRightAnswer();
        }

        else
        {
            SetColorWrongAnswer();
        }
    }

    public void SetButton(Button myButton)
    {
        pressedbutton = myButton;
        isButtonPressed = true;
        if (myButton == true)
        {
            nextButton.GetComponent<Button>().interactable = true;
        }
    }

    public void SetColorRightAnswer()
    {
        Button myButton = pressedbutton;
        ColorBlock farbe = myButton.colors;
        farbe.selectedColor = Color.green;
        myButton.colors = farbe;
    }

    public void SetColorWrongAnswer()
    {
        Button myButton = pressedbutton;
        ColorBlock farbe = myButton.colors;
        farbe.selectedColor = Color.red;
        myButton.colors = farbe;
    }

    void StartTimer()
    {
        currentTimer = timerDuration;
    }

    void UpdateTimerDisplay()
    {
        timerText.text = Mathf.CeilToInt(currentTimer).ToString();
    }

    public void OnAnswerSelected()
    {
        CancelInvoke(nameof(SetQuestion));
        StartTimer();
    }

    public void NextButton ()
    {
        if (!isButtonPressed)
            return;
        index++;
        CheckIndex();

        isButtonPressed = false;
    }

    private void CheckIndex()
    {
        if (index < selectedQuestion.Count)
        {
            SetQuestion();
            Counter();
        }
    }


    public void Counter ()
    {
        countNumber.text = (index + 1).ToString() + "/"+ selectedQuestion.Count;
    }

    void Update()
    {
        currentTimer -= Time.deltaTime;
        if (index <= selectedQuestion.Count)
        {
            UpdateTimerDisplay();
        }
       

        if (currentTimer <= 0f)
        {
            index++;
            CheckIndex();
            if (index >= selectedQuestion.Count)
                return;
            Debug.Log(index + " " + selectedQuestion.Count);
            StartTimer();
        }
    }

    
}

public static class ShuffleHelper
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        System.Random rnd = new System.Random();
        while (n > 1)
        {
            int k = (rnd.Next(0, n) % n);
            n--;
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
