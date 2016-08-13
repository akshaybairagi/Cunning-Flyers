using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float smooth = 0.15f;         // The relative speed at which the camera will catch up.

    private Transform player;
    private Vector3 newPos;
    private float offsetY;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;       

        offsetY = transform.position.y - player.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (player != null)
        {
            newPos = transform.position;
            newPos.y = player.position.y + offsetY;

            transform.position = newPos;
        }
    }
}
