using UnityEngine;

public class BalloonBuilder : MonoBehaviour
{
    //scripts
    GameControl gControl;
    
    //gameObject
    public GameObject balloon;
    
    //numeric
    public float ballonCreateTime = 1f;
    public float timeCounter = 0f;
    
    void Start()
    {
        gControl = this.gameObject.GetComponent<GameControl>();
    }

    void Update()
    {
        timeCounter -= Time.deltaTime;
        if(timeCounter < 0 && gControl.getTimeCounter() > 0)
        {
            InstantiateBallon(getMultiplyNumber());
        }
    }

    void InstantiateBallon(int _multiply)
    {
        GameObject go = Instantiate(balloon, new Vector3(Random.Range(-2.25f, 2.25f), -6, 0), Quaternion.Euler(0, 0, 0));
        go.GetComponent<Rigidbody2D>().AddForce(new Vector3(0, Random.Range(50f * _multiply, 100f * _multiply), 0));
        timeCounter = ballonCreateTime;
    }

    int getMultiplyNumber()
    {
        int multiply = (int)(gControl.getTimeCounter() / 10) - 6;
        multiply *= -1;
        return multiply;
    }
}
