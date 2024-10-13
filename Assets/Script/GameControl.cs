using System.Collections.Generic;
using UnityEngine;

namespace BalloonGame
{
    // Enum to define different data types used in the game
    public enum DataType
    {
        Blasted,
        Score,
        Fugutive,
    }

    public class GameControl : MonoBehaviour
    {
        // Singleton instance of the GameControl
        public static GameControl instance;

        // References to other components in the scene
        [SerializeField] private Timer timer;
        [SerializeField] private UiManager uiManager;

        // Dictionary to store game statistics (blasted balloons, score, fugitive balloons)
        private Dictionary<string, int> dataDictionary;

        // List to store all the balloons created in the game
        private List<GameObject> createdBalloons = new();

        // Property to manage the balloon speed with a clamp between minSpeed and maxSpeed
        public float Speed
        {
            get
            {
                return _speed;
            }
            set
            {
                _speed = Mathf.Clamp(value, minSpeed, maxSpeed);
            }
        }

        // Private fields for speed control
        private float _speed;
        private float minSpeed = 150f;
        private float maxSpeed = 250f;

        // Flag to indicate whether the game is over
        public bool gameOver { get; private set; }

        private void Awake()
        {
            // Singleton pattern to ensure only one GameControl instance exists
            if (instance == null)
                instance = this;
            else
                Destroy(this);

            // Initialize the speed to the minimum speed
            _speed = minSpeed;

            // Initialize the game data dictionary
            dataDictionary = new(){
                {DataType.Blasted.ToString(), 0},
                {DataType.Fugutive.ToString(), 0},
                {DataType.Score.ToString(), 0},
            };
        }

        private void OnEnable()
        {
            // Subscribe to the timer's timeout event
            timer.onTimeOut += HandleOnTimeOut;
        }

        private void OnDisable()
        {
            // Unsubscribe from the timer's timeout event
            timer.onTimeOut -= HandleOnTimeOut;
        }

        // Method called when the timer runs out
        private void HandleOnTimeOut()
        {
            gameOver = true;

            // Hide all the balloons when the game is over
            for (int i = 0; i < createdBalloons.Count; i++)
            {
                if (createdBalloons[i].TryGetComponent(out BalloonController balloonController))
                {
                    balloonController.HideGameObject();
                }
            }

            // Trigger the GameOver UI with the final game data
            uiManager.GameOver(dataDictionary);
        }

        // Method to add a balloon to the list of created balloons
        public void AddBalloonToList(GameObject go)
        {
            createdBalloons.Add(go);
        }

        // Method called when a balloon is blasted
        public void BlastedBalloon()
        {
            // Increase the blasted count and score
            AdjustDictionaryKeyValue(DataType.Blasted.ToString(), 1, true);
            AdjustDictionaryKeyValue(DataType.Score.ToString(), 100, true);

            // Increase the speed by 10% every 5 balloons blasted
            if (dataDictionary[DataType.Blasted.ToString()] % 5 == 0)
            {
                Speed += Speed * 0.1f;
            }
        }

        // Method called when a balloon escapes (fugitive balloon)
        public void FugitiveBalloon()
        {
            // Increase the fugitive count and decrease the score
            AdjustDictionaryKeyValue(DataType.Fugutive.ToString(), 1, true);
            AdjustDictionaryKeyValue(DataType.Score.ToString(), 100, false);

            // Decrease the speed by 10% every 5 balloons escape
            if (dataDictionary[DataType.Fugutive.ToString()] % 5 == 0)
            {
                Speed -= Speed * 0.1f;
            }
        }

        // Method to adjust the dictionary values based on the event (increase or decrease)
        private void AdjustDictionaryKeyValue(string key, int value, bool increase)
        {
            // Modify the value based on whether it's an increase or decrease
            int m_value = increase == true ? value * 1 : value * -1;

            // Update the dictionary with the new value
            dataDictionary[key] += m_value;

            // Print the updated values for debugging purposes
            foreach (KeyValuePair<string, int> entry in dataDictionary)
            {
                print($"key:{entry.Key},value:{entry.Value}");
            }
        }
    }
}
