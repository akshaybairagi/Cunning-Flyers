using UnityEngine;
using System.Collections;

public class ScrollerScript : MonoBehaviour {
    
    public float MinSpeed = 1f;
    public float MaxSpeed = 1f;

    public Sprite cloud1;
    public Sprite cloud2;
    public Sprite cloud3;

    private float[] speed;
    private Vector3 newPos;

    // Use this for initialization
    void Start () {

        speed = new float[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            speed[i] = Random.Range(MinSpeed, MaxSpeed);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        for (int i = 0; i < transform.childCount; i++)
        {
            newPos = transform.GetChild(i).position;
            newPos.x = newPos.x - speed[i] * Time.deltaTime;

            if (newPos.x < -10)
            {
                newPos.x = 10f;
                speed[i] = Random.Range(MinSpeed, MaxSpeed);
                ChangeCloudSprite(transform.GetChild(i).gameObject);
            }                

            transform.GetChild(i).position = newPos;
            
        }
    }

    void ChangeCloudSprite(GameObject cloud)
    {
        cloud.GetComponent<SpriteRenderer>().sprite = cloud1;

        switch (Random.Range(0, 3))
        {
            case 0:
                cloud.GetComponent<SpriteRenderer>().sprite = cloud1;
                break;
            case 1:
                cloud.GetComponent<SpriteRenderer>().sprite = cloud2;
                break;
            case 2:
                cloud.GetComponent<SpriteRenderer>().sprite = cloud3;
                break;
            default:
                break;
        }
    }
}
