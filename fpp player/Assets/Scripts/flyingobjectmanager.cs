using UnityEngine;

public class FlyingObjectManager : MonoBehaviour
{
    [SerializeField] private FlyingObject[] flyingObjects;

    private FlyingObject currentActiveObject;

    void Update()
    {
        // Check if there's an active object
        if (currentActiveObject != null)
        {
            // Check if the currently active object is back to its initial position (inactive)
            if (!currentActiveObject.IsFloating)
            {
                // Deactivate the current active object
                currentActiveObject.DeactivateFlyingObject();
                currentActiveObject = null;
            }
        }
        else
        {
            // Check for right-click to activate a flying object
            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                foreach (FlyingObject flyingObject in flyingObjects)
                {
                    if (Physics.Raycast(ray, out hit) && hit.collider == flyingObject.ObjectCollider)
                    {
                        // Activate the selected object
                        flyingObject.ActivateFlyingObject(Camera.main.transform.position);
                        currentActiveObject = flyingObject;
                        break;
                    }
                }
            }
        }
    }
}