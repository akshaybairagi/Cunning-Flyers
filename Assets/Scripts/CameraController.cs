using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float smooth = 1.5f;         // The relative speed at which the camera will catch up.
    public bool smoothMove = true;

    private Transform player;
    private Vector3 newPos;
    private float offsetY;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;       

        offsetY = transform.position.y - player.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            newPos = transform.position;
            newPos.y = player.position.y + offsetY;

            if (smoothMove)
                transform.position = Vector3.Lerp(transform.position, newPos, smooth * Time.deltaTime);
            else
                transform.position = newPos;
        }
    }
}
