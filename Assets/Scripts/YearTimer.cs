using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class YearTimer : MonoBehaviour
{
    private TextMeshProUGUI yearTimerText;       // Reference to the Year Timer TextMeshPro component
    public float timeMultiplier = 0.1f;           // Controls how fast the timer increments
    private float elapsedYears = 0f;      // Starting at 2 million years
    public Vector3 popUpOffSet;
    public float popUpAppearTime;
    Vector3 popUpPos;
    public TextMeshProUGUI popUpText;
    public float firstEventYear;
    public float secondEventYear;
    public float thirdEventYear,forthEventYear,fifthEventYear,sixthEventYear,seventhEventYear,eightEventYear,ninethEvent,tenthEvent,eleEventYear,twelEventYear,thirteenEventYear;
    [TextAreaAttribute]
    public string firstEventPopUp;
    public string secondEventPopUp;
    public string thirdEventPopUp,forth,fifth,sixth,seventh,eighth,nineth,tenth,eleventh,twelth,thirteen;
    bool isPopping=false;
    bool event1,event2,event3,event4,event5,event6,event7,event8,event9,event10,event11,event12,event13=false;
    void Start()
    {
        if (yearTimerText == null)
        {
            yearTimerText = GetComponent<TextMeshProUGUI>(); // Auto-assign the TextMeshPro component
        }
        popUpPos = popUpText.gameObject.transform.position;
        // Initialize the text display
        UpdateYearDisplay();
        popUpText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Increment the timer based on the time multiplier and deltaTime
        elapsedYears += timeMultiplier * Time.deltaTime;

        // Update the display text
        UpdateYearDisplay();
        UpdatePopUpText();
    }

    void UpdateYearDisplay()
    {
        if (elapsedYears >= 1000) { yearTimerText.text = (elapsedYears / 1000).ToString("0.0") + " Billion Years"; }
        // Update the text to show the years in a readable format
        else
            yearTimerText.text = elapsedYears.ToString("0.0") + " Million Years";

        // Position the text at the desired location (if needed, you can add more offset logic here)
        RectTransform rectTransform = yearTimerText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
    }

    void UpdatePopUpText()
    {
        if (elapsedYears >= thirdEventYear && !isPopping && !event13)
        {
            popUpText.text = thirteen;
            ShowPopUpText();
            event13 = true;
        }
        else if (elapsedYears >= twelEventYear && !isPopping && !event12)
        {
            popUpText.text = twelth;
            ShowPopUpText();
            event12 = true;
        }
        else if (elapsedYears >= eleEventYear && !isPopping && !event11)
        {
            popUpText.text = eleventh;
            ShowPopUpText();
            event11 = true;
        }
        else if (elapsedYears >= tenthEvent && !isPopping && !event10)
        {
            popUpText.text = tenth;
            ShowPopUpText();
            event10 = true;
        }
        else if (elapsedYears >= ninethEvent && !isPopping && !event9)
        {
            popUpText.text = nineth;
            ShowPopUpText();
            event9 = true;
        }
        else if (elapsedYears >= eightEventYear && !isPopping && !event8)
        {
            popUpText.text = eighth;
            ShowPopUpText();
            event8 = true;
        }
        else if (elapsedYears >= seventhEventYear && !isPopping && !event7)
        {
            popUpText.text = seventh;
            ShowPopUpText();
            event7 = true;
        }
        else if (elapsedYears >= sixthEventYear && !isPopping && !event6)
        {
            popUpText.text = sixth;
            ShowPopUpText();
            event6 = true;
        }
        else if (elapsedYears >= fifthEventYear && !isPopping && !event5)
        {
            popUpText.text = fifth;
            ShowPopUpText();
            event5 = true;
        }
        else if (elapsedYears >= forthEventYear && !isPopping && !event4)
        {
            popUpText.text = forth;
            ShowPopUpText();
            event4 = true;
        }
        else if (elapsedYears >= thirdEventYear && !isPopping && !event3)
        {
            popUpText.text = thirdEventPopUp;
            ShowPopUpText();
            event3 = true;
        }
        else if (elapsedYears >= secondEventYear && !isPopping && !event2)
        {
            popUpText.text = secondEventPopUp;
            ShowPopUpText();
            event2 = true;
        }
        else if (elapsedYears >= firstEventYear && !isPopping && !event1 )
        {
            popUpText.text = firstEventPopUp;
            ShowPopUpText();
            event1 = true;
        }
    }
    void ShowPopUpText()
    {
        popUpText.gameObject.SetActive(true);
        popUpText.gameObject.GetComponent<AudioSource>().Play();
        

        popUpText.gameObject.transform.DOMove(popUpPos + popUpOffSet, popUpAppearTime);
        
        StartCoroutine(HidePopUpText());
    }
    IEnumerator HidePopUpText()
    {
        yield return new WaitForSeconds(popUpAppearTime);
        popUpText.gameObject.SetActive(false);
        popUpText.gameObject.transform.DOMove(popUpPos, 1);
    }
}

