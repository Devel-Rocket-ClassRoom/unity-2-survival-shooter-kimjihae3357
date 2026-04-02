using UnityEngine;

public class CameraController : MonoBehaviour
{
    public  float cameraSpeed = 1.0f;
    public GameObject player;

    private float offsetX = 0.0f;
    private float offsetY = 4.5f;
    private float offsetZ = -5f;

    private float angleX = 30f;
    private float angleY = 0.0f;
    private float angleZ = 0.0f;
    Vector3 playerPos;


    void Start()
    {
        
    }

    void Update()
    {
        playerPos = new Vector3(
            player.transform.position.x + offsetX,
            player.transform.position.y + offsetY,
            player.transform.position.z + offsetZ );
        transform.position = Vector3.Lerp(transform.position, playerPos, cameraSpeed);

        transform.rotation = Quaternion.Euler(angleX, angleY, angleZ);
    }
}
