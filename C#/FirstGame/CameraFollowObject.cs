using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    public float mouseSensitivity = 10;
    public Transform target;
    public float distFromTarget = 2;
    //the max and min on how low and high the camera can turn
    public Vector2 pitchMinMax = new Vector2(-35, 60);

    public float rotationSmoothTime = 8f;
    Vector3 rotationSmoothVelocity;
    Vector3 currentRotation;

    float yaw;
    float pitch;

    public float moveSpeed = 5;
    public float returnSpeed = 9;
    public float wallPush = 0.7f;

    public LayerMask collisionMask;

    private bool pitchlock = false;


    

    // Start is called before the first frame update
    void Start()
    {
        //hides the cursor and makes so the cursor can't escape the game screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void LateUpdate()
    {
        //checks if there is something between the camera and player
        collisionCheck(target.position - transform.forward * distFromTarget);
        //checks if the player is close to a wall
        wallCheck();
        //if the player is not against a wall the camera can move freely
        if (!pitchlock)
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);
        }
        //if the player is againts a wall the camera will lock to from above view
        else
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch = pitchMinMax.y;
            currentRotation = Vector3.Lerp(currentRotation, new Vector3(pitch, yaw), rotationSmoothTime * Time.deltaTime);
        }
        transform.eulerAngles = currentRotation;

        Vector3 e = transform.eulerAngles;
        e.x = 0;

        target.eulerAngles = e;



   
    }
    //checks if there is something between player and the camera
    private void collisionCheck(Vector3 retPoint)
    {
        //the camera casts a ray to see if it sees the player
        RaycastHit hit;
        if (Physics.Linecast(target.position, retPoint, out hit, collisionMask))
        {
            Vector3 norm = hit.normal * wallPush;
            Vector3 p = hit.point + norm;
            
            
            transform.position = Vector3.Lerp(transform.position, p, moveSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, retPoint, returnSpeed * Time.deltaTime);
            pitchlock = false;
        }
        transform.position = Vector3.Lerp(transform.position, retPoint, returnSpeed * Time.deltaTime);
        pitchlock = false;
    }
    //checks if the player is against a wall
    private void wallCheck()
    {
        Ray ray = new Ray(target.position, -target.forward);
        RaycastHit hit;
        //cast a sphere to see if the player is against a wall
        if (Physics.SphereCast(ray, 0.5f, out hit, 0.7f, collisionMask))
        {
            pitchlock = true;
        }
        else
        {
            pitchlock = false;
        }
    }

 
}
