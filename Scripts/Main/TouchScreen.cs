using UnityEngine;
using System.Collections;

// Input.GetTouch example.
//
// Attach to an origin based cube.
// A screen touch moves the cube on an iPhone or iPad.
// A second screen touch reduces the cube size.

public class TouchScreen : MonoBehaviour
{
    public Main_Manager manager;
    private GameObject camera_GameObject;

    public float speedCam;
    Vector2 StartPosition;
    Vector2 DragStartPosition;
    Vector2 DragNewPosition;
    Vector2 Finger0Position;
    float DistanceBetweenFingers;
    bool isZooming;

    private void Start()
    {
        camera_GameObject = Camera.main.gameObject;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount == 0 && isZooming)
        {
            isZooming = false;
        }

        if (Input.touchCount == 1)
        {
            if (manager.target != null || manager.gameMode == Main_Manager.GameMode.Cloth 
                || manager.gameMode == Main_Manager.GameMode.Interact || manager.setting.working)
                return;
            
            if (!isZooming)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    Vector2 NewPosition = GetWorldPosition();
                    Vector2 PositionDifference = NewPosition - StartPosition;
                    float xPos = Mathf.Clamp(PositionDifference.x,-10,10);
                    PositionDifference = new Vector2(xPos, PositionDifference.y);
                    camera_GameObject.transform.Translate(-PositionDifference * speedCam);
                    camera_GameObject.transform.position = new Vector3(camera_GameObject.transform.position.x,-0.5f,-10);
                }
                StartPosition = GetWorldPosition();
            }
        }
        else if (Input.touchCount == 20)
        {
            if (Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                isZooming = true;

                DragNewPosition = GetWorldPositionOfFinger(1);
                Vector2 PositionDifference = DragNewPosition - DragStartPosition;

                if (Vector2.Distance(DragNewPosition, Finger0Position) < DistanceBetweenFingers)
                    camera_GameObject.GetComponent<Camera>().orthographicSize += (PositionDifference.magnitude);

                if (Vector2.Distance(DragNewPosition, Finger0Position) >= DistanceBetweenFingers)
                    camera_GameObject.GetComponent<Camera>().orthographicSize -= (PositionDifference.magnitude);

                DistanceBetweenFingers = Vector2.Distance(DragNewPosition, Finger0Position);
            }
            DragStartPosition = GetWorldPositionOfFinger(1);
            Finger0Position = GetWorldPositionOfFinger(0);
        }


    }

    Vector2 GetWorldPosition()
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
    }

    Vector2 GetWorldPositionOfFinger(int FingerIndex)
    {
        return camera_GameObject.GetComponent<Camera>().ScreenToWorldPoint(Input.GetTouch(FingerIndex).position);
    }
}