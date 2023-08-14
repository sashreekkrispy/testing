using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [Header("Floating Settings")]
    [SerializeField] private float floatingHeight = 5f;//height at which the object floats when activated
    [SerializeField] private float floatingSpeed = 2f;  //speed at which the object moves towards the floating height
    [SerializeField] private float flyingSpeed = 5f;//speed at which the object flies back to its initial position
    [SerializeField] private Transform cameraTransform; //camera's transform used for positioning

    [Header("Zoom Settings")]
    [SerializeField] private float cameraDistance = 3f;// default distance between the object and camera
    [SerializeField] private float zoomSpeed = 5f;   // speed of zoom in and out 
    [SerializeField] private float minCameraDistance = 2f; // closest zoom
    [SerializeField] private float maxCameraDistance = 10f;// farthest zoom

    [Header("Rotation Settings")]
    [SerializeField] private float lateralRotationSpeed = 200f;//speed of lateral rotation
    [SerializeField] private float longitudinalRotationSpeed = 100f;//speed of longitudinal rotation

    //private stuff
    private Vector3 initialPosition;
    private Vector3 targetFlyingPosition;
    private bool isFloating = false;
    private bool isRotatingLateral = false;
    private bool isRotatingLongitudinal = false;
    private Quaternion initialRotation;
    private Quaternion targetRotation;
    private Collider objectCollider;

    public bool IsFloating { get; private set; }
    public Collider ObjectCollider { get; private set; }

    void Start()
    {
        //store the position and rotation of the part before it moved
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        //get the collider component of the part
        objectCollider = GetComponent<Collider>();
    }

    void Update()
    {
        if (IsFloating)
        {
            //flying to cam movement
            Vector3 targetPosition = new Vector3(transform.position.x, floatingHeight, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, floatingSpeed * Time.deltaTime);
            //move the object towards the assigned object, usually camera 
            transform.position = Vector3.MoveTowards(transform.position, targetFlyingPosition, flyingSpeed * Time.deltaTime);
        }
        else
        {
            //flying back to initial position
            transform.position = Vector3.MoveTowards(transform.position, initialPosition, flyingSpeed * Time.deltaTime);

            //rotate back to the initial rotation (using Slerp for smooth rotation, smoooooth)
            transform.rotation = Quaternion.Slerp(transform.rotation, initialRotation, Time.deltaTime * 5f);
        }

        //toggle floating on right-click
        if (Input.GetMouseButtonDown(1))
        {
            if (IsFloating)
            {
                DeactivateFlyingObject();
            }
            else
            {
                //cast a ray from the camera to the mouse position on the screen
                Ray ray = cameraTransform.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit) && hit.collider == objectCollider)
                {
                    //check if the ray hits the object's collider
                    ActivateFlyingObject(cameraTransform.position);
                }
            }
        }

        if (IsFloating)
        {
            //rotation for object, given by arrow keys
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                isRotatingLateral = true;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                isRotatingLongitudinal = true;
            }

            if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
            {
                isRotatingLateral = false;
            }

            if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                isRotatingLongitudinal = false;
            }

            if (isRotatingLateral)
            {
                float rotationAmount = Input.GetKey(KeyCode.RightArrow) ? -1f : 1f;
                transform.Rotate(Vector3.up, rotationAmount * lateralRotationSpeed * Time.deltaTime, Space.World);
            }

            if (isRotatingLongitudinal)
            {
                float rotationAmount = Input.GetKey(KeyCode.DownArrow) ? -1f : 1f;
                transform.Rotate(Vector3.right, rotationAmount * longitudinalRotationSpeed * Time.deltaTime, Space.World);
            }

            // Zoom
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");
            cameraDistance = Mathf.Clamp(cameraDistance - scrollInput * zoomSpeed, minCameraDistance, maxCameraDistance);
            targetFlyingPosition = cameraTransform.position + cameraTransform.forward * cameraDistance;
        }
    }

    public void ActivateFlyingObject(Vector3 cameraPosition)
    {
        IsFloating = true;
        targetFlyingPosition = cameraPosition + cameraTransform.forward * cameraDistance;
        objectCollider.enabled = false;
    }

    public void DeactivateFlyingObject()
    {
        IsFloating = false;
        targetRotation = transform.rotation;
        objectCollider.enabled = true;
    }
}