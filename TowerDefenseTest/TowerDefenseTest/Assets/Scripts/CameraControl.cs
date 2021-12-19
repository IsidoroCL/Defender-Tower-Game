using UnityEngine;

public class CameraControl : MonoBehaviour
{

    #region Fields
    [SerializeField]
    float maxZoom = 5;
    [SerializeField]
    float minZoom = 20;
    [SerializeField]
    float sensitivity = 1;
    [SerializeField]
    float zoomSpeed = 30;
    [SerializeField]
    float movSpeed = 50;
    float targetZoom;
    #endregion

    #region Unity methods
    private void LateUpdate()
    {
        ZoomWithMouse();
        PanWithMouse();
    }
    #endregion

    #region Private methods
    private void ZoomWithMouse()
    {
        targetZoom -= Input.mouseScrollDelta.y * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        float newSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
        Camera.main.orthographicSize = newSize;
    }

    private void PanWithMouse()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position += new Vector3(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * movSpeed,
                                           -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * movSpeed, 0.0f);
            }

            else if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position += new Vector3(-Input.GetAxisRaw("Mouse X") * Time.deltaTime * movSpeed,
                                           -Input.GetAxisRaw("Mouse Y") * Time.deltaTime * movSpeed, 0.0f);
            }
        }
    }
    #endregion

    #region Public / Protected methods
    public void Initialize(int size)
    {
        Camera.main.transform.position = new Vector3((size / 2f) - 0.5f, (size / 2f) - 0.5f, -10);
        Camera.main.orthographicSize = targetZoom = size / 2f;
        maxZoom = size / 6f;
        minZoom = size * 0.6f;
    }
    #endregion
}