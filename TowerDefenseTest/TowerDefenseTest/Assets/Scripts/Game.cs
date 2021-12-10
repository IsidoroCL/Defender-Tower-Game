using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    #region Fields
    public GridScenario scenario;
    #endregion

    #region Unity methods
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            HandleTouch();
        }
    }
    #endregion

    #region Private methods
    private void HandleTouch()
    {
        GameObject objectTouch = SelectedObjectByMouse();
        if (objectTouch != null &&
            objectTouch.GetComponent<Tile>())
        {
            scenario.ToggleContent(objectTouch.GetComponent<Tile>(), TileType.LaserTurret);
        }
    }
    #endregion

    #region Public / Protected methods
    public static GameObject SelectedObjectByMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            Debug.Log("Objeto pulsado: " + hit.collider.transform.gameObject.name);
            return hit.collider.transform.gameObject;
        }
        return null;
    }
    #endregion
}