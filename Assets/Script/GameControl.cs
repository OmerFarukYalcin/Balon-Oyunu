using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    //UI
    [SerializeField] Text timeText;
    [SerializeField] Text balloonText;
    [SerializeField] Text pointText;
    [SerializeField] Text fugitiveText;
    [SerializeField] Text caseText;
    [SerializeField] Button retryButton;

    //numeric
    float timeCounter=60f;
    int balloonCount = 0;
    int fugitiveBallon = 0;
    int totalPoint = 0;
    
    //GameObject
    public GameObject blust;
    
    //bool
    bool gameComplete = false;
    void Start()
    {
        balloonText.text = "Balon : " + balloonCount;
        fugitiveText.text = "Kacan Balon : " + fugitiveBallon;
        pointText.text = "Puan : " + totalPoint;
    }

    void Update()
    {
        if (timeCounter > 0 && !gameComplete)
        {
            timeCounter -= Time.deltaTime;
            timeText.text = "Süre : " + (int)timeCounter;
        }
        else
        {
            GameObject[] go = GameObject.FindGameObjectsWithTag("Balon");
            for(int i =0; i < go.Length; i++)
            {
                Destroy(go[i]);
                Instantiate(blust,go[i].transform.position,go[i].transform.rotation);
            }
            gameComplete =true;
            caseText.text = "Oyun tamamlandý.Tebrikler puanýnýz " + totalPoint;
            retryButton.gameObject.SetActive(true);
        }
    }

    public float getTimeCounter() {
        return timeCounter;
    }

    public void AddBalloon()
    {
        balloonCount += 1;
        totalPoint += 100;
        balloonText.text = "Balon: " + balloonCount;
        pointText.text = "Puan : " + totalPoint;
    }

    public void FugitiveBalloon()
    {
        fugitiveBallon += 1;
        totalPoint -= 100;
        fugitiveText.text = "Kacan Balon : " + fugitiveBallon;
        pointText.text = "Puan : " + totalPoint;
    }
}
