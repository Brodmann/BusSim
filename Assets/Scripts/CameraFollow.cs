using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    
    public Transform cameraTarget;
    public float cameraSpeed = 10.0f;
    public Vector3 dist;
    public Transform lookTarget;
 
    void Update() {
        Vector3 cameraPos = cameraTarget.position + dist;
        Vector3 lookPos = Vector3.Lerp(transform.position, cameraPos, cameraSpeed * Time.deltaTime);
        transform.position = lookPos;
        transform.LookAt(lookTarget.position);
    }
}
