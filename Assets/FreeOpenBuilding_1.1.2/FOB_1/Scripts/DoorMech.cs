using UnityEngine;

public class DoorMech : MonoBehaviour
{
    [Header("Rotation Settings")]
    public Vector3 OpenRotation, CloseRotation;

    [Header("Speed")]
    public float rotSpeed = 1f;

    [Header("State")]
    private bool doorBool = false;

    void Update()
    {
        if (doorBool)
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(OpenRotation), rotSpeed * Time.deltaTime);
        else
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(CloseRotation), rotSpeed * Time.deltaTime);
    }

    // ---------- Timeline 전용 함수 ----------
    public void OpenDoor()
    {
        doorBool = true;
    }

    public void CloseDoor()
    {
        doorBool = false;
    }

    public void ToggleDoor()
    {
        doorBool = !doorBool;
    }
}
