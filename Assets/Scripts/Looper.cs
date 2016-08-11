using UnityEngine;
using System.Collections.Generic;

public class Looper : MonoBehaviour
{

    public Transform Obstacle;

    int numOfObstacles = 6;
    float stepLength = 8f;
    Vector3 newPos;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < numOfObstacles; i++)
        {
            int randomNo = Random.Range(0, 2);

            //Insert Obstacles
            if (randomNo == 0)
                Instantiate(Obstacle, new Vector3(3f, -stepLength * i - 10.5f, 0), Quaternion.identity);
            else
                Instantiate(Obstacle, new Vector3(-3f, -stepLength * i - 10.5f, 0), Quaternion.identity);
        }
    }

    //On Collision detection with Trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        newPos = collider.transform.position;        

        if (collider.gameObject.tag == "Obstacle")
        {
            newPos.y -= stepLength * numOfObstacles;

            if (Random.Range(0, 2) == 0)
                newPos.x = 3f;
            else
                newPos.x = -3f;
        }

        collider.transform.position = newPos;
    }
}
