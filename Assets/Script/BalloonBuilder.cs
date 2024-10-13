using UnityEngine;

namespace BalloonGame
{
    public class BalloonBuilder : MonoBehaviour
    {
        // The balloon prefab to be assigned from the Inspector
        [SerializeField] private GameObject balloon;

        // Time interval for creating balloons (1 second)
        private float ballonCreateTime = 1f;
        // A timer to keep track of time between balloon spawns
        private float timeCounter = 0f;

        void Update()
        {
            // Decrease the timer every frame
            timeCounter -= Time.deltaTime;

            // When the timer reaches below 0 and the game is not over, create a balloon
            if (timeCounter < 0 && !GameControl.instance.gameOver)
            {
                InstantiateBallon();
            }
        }

        // Method to instantiate a new balloon
        void InstantiateBallon()
        {
            // Instantiate a balloon at a random x position within the specified range
            GameObject go = Instantiate(balloon, new Vector3(Random.Range(-2.25f, 2.25f), -6, 0), Quaternion.Euler(0, 0, 0), transform);

            // Add the created balloon to the game control's balloon list
            GameControl.instance.AddBalloonToList(go);

            // Reset the timer to the balloon creation interval
            timeCounter = ballonCreateTime;
        }
    }
}
