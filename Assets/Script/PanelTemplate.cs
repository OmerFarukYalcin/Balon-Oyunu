using UnityEngine;
using UnityEngine.UI;

namespace BalloonGame
{
    public class PanelTemplate : MonoBehaviour
    {
        // The type of data this panel represents (e.g., score, blasted balloons)
        [SerializeField] private DataType dataType;
        // Reference to the Text component used to display the value
        private Text valueText;

        private void Awake()
        {
            // Find the child GameObject named "value" and get its Text component
            valueText = transform.Find("value").GetChild(0).GetComponent<Text>();
        }

        // Method to set the displayed value in the panel
        public void SetValue(string str)
        {
            valueText.text = str; // Update the text with the provided string
        }

        // Property to get the DataType of this panel
        public DataType TemplateDataType => dataType;
    }
}
