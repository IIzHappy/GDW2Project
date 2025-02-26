using UnityEngine;

public class CamController : MonoBehaviour
{
    Transform target;
    Vector3 velocity = Vector3.zero;

    [Range(0f, 1f)] public float smoothTime;

    public Vector3 posOffset;

    public Transform topLeftBound;
    public Transform bottomRightBound;

    private Camera cam;

    private void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        cam = GetComponent<Camera>();
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + posOffset;

        float camHeight = cam.orthographicSize * 2;
        float camWidth = camHeight * cam.aspect;

        float minX = topLeftBound.position.x + camWidth / 2;
        float maxX = bottomRightBound.position.x - camWidth / 2;
        float minY = bottomRightBound.position.y + camHeight / 2;
        float maxY = topLeftBound.position.y - camHeight / 2;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void Zoom(float zoom)
    {
        cam.orthographicSize *= zoom;
    }
}
