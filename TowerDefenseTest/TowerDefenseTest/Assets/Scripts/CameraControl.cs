using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

    #region Fields
    public float maxZoom = 5;
    public float minZoom = 20;
    public float sensitivity = 1;
    public float zoomSpeed = 30;
    public float movSpeed = 50;
    float targetZoom;
    float limit;

    #endregion

    #region Unity methods
    private void LateUpdate()
    {
        Zoom();
        Pan();        
    }
    #endregion

    #region Private methods
    private void Zoom()
    {
        targetZoom -= Input.mouseScrollDelta.y * sensitivity;
        targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        float newSize = Mathf.MoveTowards(Camera.main.orthographicSize, targetZoom, zoomSpeed * Time.deltaTime);
        Camera.main.orthographicSize = newSize;
    }

    private void Pan()
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
    public void Init(int size)
    {
        limit = size / 2f;
        Camera.main.transform.position = new Vector3((size / 2f) - 0.5f, (size / 2f) - 0.5f, -10);
        Camera.main.orthographicSize = targetZoom = size / 2f;
        maxZoom = size / 6f;
        minZoom = size * 0.6f;
    }
    #endregion
}