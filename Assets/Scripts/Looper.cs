using UnityEngine;
using System.Collections.Generic;

public class Looper : MonoBehaviour
{

    public GameObject Obstacle;
    public GameObject Crate;
    public int numOfObstacles = 6;
    public float stepLength = 7f;

    Vector3 newPos;

    public Queue<GameObject> cratesPool;

    // Use this for initialization
    void Start()
    {
        cratesPool = new Queue<GameObject>();

        //Init Obstacle Platforms
        GetObstacles();
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
                newPos.x = 2f;
            else
                newPos.x = -2f;

            //Place the powerUps
            if (cratesPool.Count > 0 && Random.Range(0, 3) > 0)
            {
                var obj = cratesPool.Dequeue();
                obj.SetActive(true);

                obj.transform.position = GetCratePosition(newPos);
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
                pos = new Vector3(2f, -stepLength * i - 10.5f, 0);
                Instantiate(Obstacle, pos, Quaternion.identity);
            }
            else
            {
                pos = new Vector3(-2f, -stepLength * i - 10.5f, 0);
                Instantiate(Obstacle, pos, Quaternion.identity);
            }

            //Insert PowerUp Crates
            createCrate(pos);
        }
    }
    

    private void createCrate(Vector3 pos)
    {
        var position = GetCratePosition(pos);

        Instantiate(Crate, position, Quaternion.identity);
    }

    private Vector3 GetCratePosition(Vector3 pos)
    {
        var newPos = Vector3.zero;

        if (pos.x > 0)
        {
            newPos.x = pos.x - 1.25f;
            newPos.y = pos.y + 3f;           
        }
        else
        {
            newPos.x = pos.x + 1.25f;
            newPos.y = pos.y + 3f;
        }

        newPos.x = Random.Range(newPos.x - 1f, newPos.x + 1f);
        newPos.y = Random.Range(newPos.y - 1.5f, newPos.y + 1.5f);

        return newPos;
    }
}
