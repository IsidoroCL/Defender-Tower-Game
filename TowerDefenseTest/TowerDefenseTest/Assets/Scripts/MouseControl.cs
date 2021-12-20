using UnityEngine;

public class MouseControl : MonoBehaviour
{

    #region Fields

    #endregion

    #region Unity methods
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                HandleTouchAlt();
            }
            else
            {
                HandleTouch();
            }
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
            objectTouch.GetComponent<Tile>().ToggleContent(TileType.LaserTurret);
        }
    }

    private void HandleTouchAlt()
    {
        GameObject objectTouch = SelectedObjectByMouse();
        if (objectTouch != null &&
            objectTouch.GetComponent<Tile>())
        {
            objectTouch.GetComponent<Tile>().ToggleContent(TileType.CannonTurret);
        }
    }

    private static GameObject SelectedObjectByMouse()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
        if (hit.collider != null)
        {
            return hit.collider.transform.gameObject;
        }
        return null;
    }
    #endregion

    #region Public / Protected methods

    #endregion
}