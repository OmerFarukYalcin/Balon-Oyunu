using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BalloonGame
{
    public class UiManager : MonoBehaviour
    {
        // List of panel templates to display game statistics
        [SerializeField] private List<PanelTemplate> panelTemplates = new();
        // Reference to the final panel that shows game over information
        private Transform finalPanel;
        // Reference to the retry button for restarting the game
        private Button retryBtn;

        private void Awake()
        {
            // Find the FinalPanel transform in the hierarchy
            finalPanel = transform.Find("FinalPanel");

            // Get the retry button component from the FinalPanel
            retryBtn = finalPanel.Find("retryButton").GetComponent<Button>();

            // Add a listener to the retry button to reload the current scene when clicked
            retryBtn.onClick.AddListener(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            });
        }

        private void Start()
        {
            // Iterate through the children of the final panel to find PanelTemplate components
            foreach (Transform item in finalPanel)
            {
                if (item.TryGetComponent(out PanelTemplate panelTemplate))
                {
                    // Add each found panel template to the list
                    panelTemplates.Add(panelTemplate);
                }
            }
        }

        // Method to be called when the game is over
        public void GameOver(Dictionary<string, int> keyValuePairs)
        {
            // Activate the final panel to show the game over UI
            finalPanel.gameObject.SetActive(true);

            // Update each panel template with the corresponding data from the key-value pairs
            foreach (PanelTemplate panelTemplate in panelTemplates)
            {
                panelTemplate.SetValue(keyValuePairs[panelTemplate.TemplateDataType.ToString()].ToString());
            }
        }
    }
}
