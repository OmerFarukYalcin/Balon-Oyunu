using UnityEngine;

public class BalloonController : MonoBehaviour
{
    public AudioClip balloonBlustSound;
    public GameObject blust;
    GameControl gControl;
    
    void Start()
    {
        gControl = GameObject.Find("_Scripts").GetComponent<GameControl>();
    }

    private void Update()
    {
        if (this.gameObject.transform.position.y>=4.5 )
        {
            gControl.FugitiveBalloon();
            enabled = false;
        }
    }

    private void OnMouseDown()
    {
        if (transform.position.y <= 4.5)
        {
            GameObject go = Instantiate(blust, transform.position, transform.rotation);
            AudioSource.PlayClipAtPoint(balloonBlustSound, transform.position);
            Destroy(this.gameObject);
            Destroy(go, 0.267f);
            gControl.AddBalloon();
        }
        
    }
    

}