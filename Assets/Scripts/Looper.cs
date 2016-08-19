using UnityEngine;
using System.Collections.Generic;

public class Looper : MonoBehaviour
{

    public GameObject Obstacle;
    public GameObject Crate;
    public int numOfObstacles = 6;
    public float stepLength = 7f;

    int ObstacleLength;
    float crateLength;

    Vector3 newPos;

    public Queue<GameObject> cratesPool;

    // Use this for initialization
    void Start()
    {
        cratesPool = new Queue<GameObject>();

        //Init Obstacle Platforms
        GetObstacles();
        ObstacleLength = 5;
        crateLength = 0.25f;
    }

    //On Collision detection with Trigger
    void OnTriggerEnter2D(Collider2D collider)
    {
        newPos = collider.transform.position;

        if (collider.gameObject.tag == "Crate")
        {
            cratesPool.Enqueue(collider.gameObject);
            collider.gameObject.SetActive(false);
        }

        if (collider.gameObject.tag == "Obstacle")
        {
            newPos.y -= stepLength * numOfObstacles;

            if (Random.Range(0, 2) == 0)
                newPos.x = 3f;
            else
                newPos.x = -3f;

            //Place the powerUps
            if (cratesPool.Count > 0 && Random.Range(0, 3) > 0)
            {
                var obj = cratesPool.Dequeue();
                obj.SetActive(true);
                var objPos = obj.transform.position;

                if (newPos.x < 0)
                {
                    objPos.x = newPos.x + ObstacleLength - crateLength;
                    objPos.y = newPos.y + 1.5f;
                }
                else
                {
                    objPos.x = newPos.x;
                    objPos.y = newPos.y + 1.5f;
                }

                obj.transform.position = objPos;
            }
        }

        collider.transform.position = newPos;
    }

    private void GetObstacles()
    {
        for (int i = 0; i < numOfObstacles; i++)
        {
            int randomNo = Random.Range(0, 2);
            var pos = Vector3.zero;

            //Insert Obstacles
            if (randomNo == 0)
            {
                pos = new Vector3(3f, -stepLength * i - 10.5f, 0);
                Instantiate(Obstacle, pos, Quaternion.identity);
            }
            else
            {
                pos = new Vector3(-3f, -stepLength * i - 10.5f, 0);
                Instantiate(Obstacle, pos, Quaternion.identity);
            }


            //Insert PowerUp Crates
            GetCrates(pos);
        }
    }
    

    private void GetCrates(Vector3 pos)
    {
        var cratePos = Vector3.zero;

        if (pos.x > 0)
        {
            cratePos.x = pos.x - 1.25f;
            cratePos.y = pos.y + 3f;           
        }
        else
        {
            cratePos.x = pos.x + 1.25f;
            cratePos.y = pos.y + 3f;
        }

        cratePos.x = Random.Range(cratePos.x - 1f, cratePos.x + 1f);
        cratePos.y = Random.Range(cratePos.y - 1.5f, cratePos.y + 1.5f);

        Instantiate(Crate, cratePos, Quaternion.identity);
    }
}
