using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class YearTimer : MonoBehaviour
{
    private TextMeshProUGUI yearTimerText;       // Reference to the Year Timer TextMeshPro component
    public float timeMultiplier = 0.1f;           // Controls how fast the timer increments
    private float elapsedYears = 2f;      // Starting at 2 million years

    void Start()
    {
        if (yearTimerText == null)
        {
            yearTimerText = GetComponent<TextMeshProUGUI>(); // Auto-assign the TextMeshPro component
        }

        // Initialize the text display
        UpdateYearDisplay();
    }

    void Update()
    {
        // Increment the timer based on the time multiplier and deltaTime
        elapsedYears += timeMultiplier * Time.deltaTime;

        // Update the display text
        UpdateYearDisplay();
    }

    void UpdateYearDisplay()
    {
        if (elapsedYears >= 1000) { yearTimerText.text = (elapsedYears/100).ToString("0.0") + " Billion Years"; }
        // Update the text to show the years in a readable format
        else
            yearTimerText.text = elapsedYears.ToString("0.0") + " Million Years";

        // Position the text at the desired location (if needed, you can add more offset logic here)
        RectTransform rectTransform = yearTimerText.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, rectTransform.anchoredPosition.y);
    }
}

