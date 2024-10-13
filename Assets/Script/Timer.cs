using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BalloonGame
{
    public class Timer : MonoBehaviour
    {
        // Reference to the Text component that displays the timer
        private Text timerText;
        // Current time remaining on the timer
        private float time;
        // The target time for the timer (60 seconds)
        private float targetTime = 60f;
        // Event to notify when the timer reaches zero
        public event System.Action onTimeOut;

        void Awake()
        {
            // Initialize the timer with the target time
            time = targetTime;
            // Get the Text component attached to this GameObject
            timerText = GetComponent<Text>();
        }

        private IEnumerator Start()
        {
            // Wait for the target time duration
            yield return new WaitForSeconds(targetTime);
            // Invoke the onTimeOut event if there are any subscribers
            onTimeOut?.Invoke();
        }

        void Update()
        {
            // Update the timer if there is remaining time
            if (time > 0)
            {
                // Decrease the time by the time elapsed since the last frame
                time -= Time.deltaTime;
                // Update the displayed text to show the remaining time
                timerText.text = "Süre : " + (int)time;
            }
        }
    }
}
