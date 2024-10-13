using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BalloonGame
{
    public class MainSceneButton : MonoBehaviour
    {
        // Reference to the Button component
        private Button button;

        private void Awake()
        {
            // Get the Button component attached to this GameObject
            button = GetComponent<Button>();
            // Add a listener to the button to call the MainScene method when clicked
            button.onClick.AddListener(MainScene);
        }

        // Method to load the main scene when the button is clicked
        private void MainScene()
        {
            // Load the scene named "SampleScene"
            SceneManager.LoadScene("SampleScene");
        }
    }
}
