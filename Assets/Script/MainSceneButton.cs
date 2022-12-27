using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneButton : MonoBehaviour
{
   public void MainScene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
