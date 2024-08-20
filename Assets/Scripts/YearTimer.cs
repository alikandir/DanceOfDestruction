using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class YearTimer : MonoBehaviour
{
    private TextMeshProUGUI yearTimerText;       // Reference to the Year Timer TextMeshPro component
    public float timeMultiplier = 0.1f;           // Controls how fast the timer increments
    private float elapsedYears = 2f;      // Starting at 2 million years
    public Vector3 popUpOffSet;
    Vector3 popUpPos;
    public TextMeshProUGUI popUpText;
    public float firstEventYear;
    public float secondEventYear;
    public float thirdEventYear;
    [TextAreaAttribute]
    public string firstEventPopUp;
    public string secondEventPopUp;
    public string thirdEventPopUp;
    bool isPopping=false;
    bool event1,event2,event3,event4;
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
        if (elapsedYears >= thirdEventYear && !isPopping && !event3)
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
        

        popUpText.gameObject.transform.DOMove(popUpPos + popUpOffSet, 5);
        
        StartCoroutine(HidePopUpText());
    }
    IEnumerator HidePopUpText()
    {
        yield return new WaitForSeconds(5);
        popUpText.gameObject.SetActive(false);
        popUpText.gameObject.transform.DOMove(popUpPos, 1);
    }
}

