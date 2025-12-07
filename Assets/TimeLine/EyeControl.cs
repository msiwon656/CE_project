using UnityEngine;

public class EyeContorl : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveAmount = 10f;     // 1씩 움직임
    public float moveSpeed = 3f;      // 부드러운 이동 속도

    private Vector3 targetPosition;   // 목표 위치
    private Vector3 originalPosition; // 시작 위치 저장

    void Start()
    {
        originalPosition = transform.localPosition;
        targetPosition = originalPosition;
    }

    void Update()
    {
        // 부드럽게 이동
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, moveSpeed * Time.deltaTime);
    }

    // -------- Timeline에서 호출할 함수들 --------

    public void ResetToOrigin()
    {
        targetPosition = originalPosition;
    }

    public void MoveXUp()
    {
        targetPosition += new Vector3(moveAmount, 0, 0);
    }

    public void MoveXDown()
    {
        targetPosition += new Vector3(-moveAmount, 0, 0);
    }

    public void MoveYUp()
    {
        targetPosition += new Vector3(0, moveAmount, 0);
    }

    public void MoveYDown()
    {
        targetPosition += new Vector3(0, -moveAmount, 0);
    }
}
