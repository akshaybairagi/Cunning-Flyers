using UnityEngine;

public class CameraController : MonoBehaviour
{
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
