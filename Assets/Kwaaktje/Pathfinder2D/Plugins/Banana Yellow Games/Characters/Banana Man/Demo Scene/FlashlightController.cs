using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class FlashlightController : MonoBehaviour
{   
    public KeyCode toggleKey = KeyCode.F;
    public Light spotlight;

    public bool isON = true;
    public bool isZooming = false;

    public float normalAngle = 90f;
    public float normalRange = 10f;
    public float normalIntensity = 15f;

    public float zoomAngle = 25f;
    public float zoomRange = 25f;
    public float zoomIntensity = 20f;

    public LayerMask detectionMask = ~0;
    public float checkRadiusPadding = 0.5f;
    public List<Transform> currentHits = new List<Transform>();

    Vector3 lastForward;    //  direction of light at World
    Vector3 lastPosition;   //  position of light at World

    private void Awake()
    {
        if (spotlight == null)
        {
            spotlight = GetComponent<Light>();
            if (spotlight == null )
            {
                spotlight = gameObject.AddComponent<Light>();
            }
        }
        spotlight.type = UnityEngine.LightType.Spot;
    }

    private void Update()
    {
        if (toggleKey != KeyCode.None && Input.GetKeyDown(toggleKey))
        {
            isON = !isON;
        }

        isZooming = Input.GetMouseButton(1);

        float targetRange;
        float targetIntensity;
        float targetOuterAngle;

        if (isZooming)
        {
            targetRange = zoomRange;
            targetIntensity = zoomIntensity;
            targetOuterAngle = zoomAngle;
        }
        else
        {
            targetRange = normalRange;
            targetIntensity = normalIntensity;
            targetOuterAngle = normalAngle;
        }

        if (!isON)
        {
            spotlight.intensity = 0f;
            spotlight.range = 0f;
            spotlight.spotAngle = targetOuterAngle;
        }
        else
        {
            spotlight.intensity = targetIntensity;
            spotlight.range = targetRange;
            spotlight.spotAngle = targetOuterAngle;
        }

        try
        {
            spotlight.innerSpotAngle = 0f;
        }
        catch
        {
            ;
        }

        lastPosition = spotlight.transform.position;
        lastForward = spotlight.transform.forward;

        UpdateDetection();
    }

    private void UpdateDetection()
    {
        currentHits.Clear();

        if (!isON || spotlight.range <= 0f || spotlight.intensity <= 0f)
        {
            return;
        }

        float range = spotlight.range;
        float halfAngle = spotlight.spotAngle * 0.5f;

        float sphereRadius = range + checkRadiusPadding;
        Collider[] cols = Physics.OverlapSphere(lastPosition, sphereRadius, detectionMask, QueryTriggerInteraction.Collide);

        for (int i = 0; i < cols.Length; i++)
        {
            Collider c = cols[i];
            if (c == null)
            {
                continue;
            }
            Transform t = c.transform;

            if (t.root == transform.root)
            {
                continue;
            }

            Vector3 dirToTarget = (t.position - lastPosition);
            float distToTarget = dirToTarget.magnitude;
            if (distToTarget > range)
            {
                continue;
            }

            if (distToTarget > 0.0001f)
            {
                dirToTarget /= distToTarget;
            }

            float angle = Vector3.Angle(lastForward, dirToTarget);
            if (angle > halfAngle)
            {
                continue;
            }

            // 중간에 빛을 가리는 오브젝트 처리. 빛이 막히면 no detection
            RaycastHit hit;
            if (Physics.Raycast(lastPosition, (t.position - lastPosition).normalized, out hit, distToTarget, detectionMask, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform != t)
                {
                    continue;
                }
            }

            if (!currentHits.Contains(t))
            {
                currentHits.Add(t);
            }
        }
    }

    private void OnValidate()
    {
        if (spotlight != null)
        {
            if (spotlight.type != UnityEngine.LightType.Spot)
            {
                spotlight.type = UnityEngine.LightType.Spot;
            }
        }
    }


    // 디버깅용 나중에 지울 것.
    void OnDrawGizmosSelected()
    {
        if (!spotlight) return;

        Gizmos.color = Color.yellow;

        Vector3 origin = spotlight.transform.position;
        Vector3 forward = spotlight.transform.forward;
        float range = Application.isPlaying ? spotlight.range : normalRange;
        float angle = Application.isPlaying ? spotlight.spotAngle : normalAngle;
        float halfAngle = angle * 0.5f;

        // 시야 원뿔 가장자리 대충 그림 (앞/좌/우/위/아래 방향선)
        // 방향 벡터들을 반각만큼 회전시키는 식으로 그려줄 수도 있지만
        // 간단하게는 range 지점의 원(반지름 = range * tan(halfAngle))만 찍자.
        float radiusAtEnd = Mathf.Tan(halfAngle * Mathf.Deg2Rad) * range;
        // 원 4분할만 찍어주자
        Vector3 up = Vector3.up;
        Vector3 right = Vector3.Cross(forward, up).normalized;
        if (right.sqrMagnitude < 0.001f)
        {
            // forward가 world up이랑 거의 평행하면 right 재계산
            right = Vector3.Cross(forward, Vector3.right).normalized;
            up = Vector3.Cross(right, forward).normalized;
        }

        Vector3 endCenter = origin + forward * range;
        Vector3 p1 = endCenter + up * radiusAtEnd;
        Vector3 p2 = endCenter - up * radiusAtEnd;
        Vector3 p3 = endCenter + right * radiusAtEnd;
        Vector3 p4 = endCenter - right * radiusAtEnd;

        Gizmos.DrawLine(origin, p1);
        Gizmos.DrawLine(origin, p2);
        Gizmos.DrawLine(origin, p3);
        Gizmos.DrawLine(origin, p4);

        Gizmos.DrawLine(p1, p3);
        Gizmos.DrawLine(p3, p2);
        Gizmos.DrawLine(p2, p4);
        Gizmos.DrawLine(p4, p1);
    }
}

