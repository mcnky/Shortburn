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

        
        int minuteIndex = Mathf.RoundToInt(currentMinute / 5f) % 12;

        
        int hourIndex = currentHour % 12;

        
        if (minuteHand)
        {
            Transform targetMinutePoint = clockPoints[minuteIndex];
            Vector3 direction = targetMinutePoint.position - minuteHand.position;
            float angle = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg; 
            minuteHand.rotation = Quaternion.Euler(-angle, 0, 0); 
        }
        if (hourHand)
        {
            Transform targetHourPoint = clockPoints[hourIndex];
            Vector3 direction = targetHourPoint.position - hourHand.position;
            float angle = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg; 
            hourHand.rotation = Quaternion.Euler(-angle, 0, 0); 
        }
    }
}