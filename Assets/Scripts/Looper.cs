using UnityEngine;
using System.Collections.Generic;

public class Looper : MonoBehaviour
{

    public Transform Obstacle;

    int numOfObstacles = 6;
    float stepLength = 10f;
    float obstacleLength;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numOfObstacles; i++)
        {
            int randomNo = Random.Range(0, 2);

            //Insert Obstacles
            Instantiate(Obstacle, new Vector3(-5 + randomNo, -stepLength * i, 0), Quaternion.identity);
        }
    }

    //On Collision detection with Trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        Vector3 pos = collider.transform.position;        

        if (collider.gameObject.tag == "Obstacles")
        {
            pos.y -= stepLength * numOfObstacles;
            pos.x = -2 * Random.Range(0, 2);            
        }

        collider.transform.position = pos;
    }
}
