using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public List<Transform> clockPoints;
    public Transform hourHand;
    public Transform minuteHand;
    public float timeSpeed = 1f;

    private int currentMinute = 0;
    private int currentHour = 0;
    private float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime * timeSpeed;

        if (elapsedTime >= 1f)
        {
            elapsedTime = 0f;
            currentMinute++;

            if (currentMinute >= 60)
            {
                currentMinute = 0;
                currentHour++;

                if (currentHour >= 12)
                {
                    currentHour = 0;
                }
            }

            UpdateClockHands();
        }
    }

    void UpdateClockHands()
    {
        if (clockPoints.Count < 12) return;

        int minuteIndex = currentMinute / 5; 
        int hourIndex = currentHour % 12; 

        if (minuteHand && clockPoints[minuteIndex])
        {
            Vector3 direction = clockPoints[minuteIndex].position - minuteHand.position;
            minuteHand.up = direction;
        }

        if (hourHand && clockPoints[hourIndex])
        {
            Vector3 direction = clockPoints[hourIndex].position - hourHand.position;
            hourHand.up = direction;
        }
    }
}