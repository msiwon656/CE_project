using UnityEngine;
using UnityEngine.AI;

public class MazeTracker : MonoBehaviour
{
    public Transform target;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent 컴포넌트가 Game Object에 없습니다.");
            enabled = false; // 스크립트 비활성화
        }
    }

    // 여기에 각 패트롤 지점을 정해놓고 랜덤으로 목표를 정해서 가다가 플레이어가 소리를 내거나 만났을 경우 enum을 변경해서 추적하는 알고리즘으로 작성하면 좋을거 같다. 
    void Update()
    {
        if (target != null && agent != null)
        {
            agent.SetDestination(target.position);
        }
    
    }
}
/* 
using UnityEngine;
using UnityEngine.AI;

public class MazeTracker : MonoBehaviour
{
    // === 인스펙터에서 설정할 변수 ===
    // 플레이어 오브젝트 (현재는 null)
    public Transform target; 
    
    // 순찰 지점들을 담을 배열 (Unity에서 설정할 것임)
    public Transform[] patrolPoints; 
    
    // === 내부 변수 ===
    private NavMeshAgent agent;
    private int destPoint = 0; // 현재 순찰 목표 인덱스

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent 컴포넌트가 Game Object에 없습니다.");
            enabled = false;
            return;
        }

        // NavMeshAgent가 항상 목표를 향해 움직이도록 설정
        agent.autoBraking = false; 

        // 순찰 지점이 설정되어 있으면 즉시 순찰 시작
        if (patrolPoints.Length > 0)
        {
            GoToNextPoint();
        }
    }

    void Update()
    {
        // 1. 플레이어 (target) 추적 로직 (현재는 target이 null이므로 실행 안 됨)
        if (target != null)
        {
            // SetDestination 호출 빈도를 줄이는 로직 (이전에 제시한 해결책)
            if (!agent.pathPending && (agent.remainingDistance > agent.stoppingDistance || agent.isPathStale))
            {
                agent.SetDestination(target.position);
            }
        }
        // 2. 순찰 로직: 플레이어 target이 null이고, 순찰 지점이 설정되어 있을 때
        else if (patrolPoints.Length > 0)
        {
            // 목표(현재 순찰 지점)에 도착했는지 확인
            // 목표까지 남은 거리가 Stopping Distance보다 작으면 도착한 것으로 간주
            if (!agent.pathPending && agent.remainingDistance < agent.stoppingDistance + 1.0f) // 0.1f 여유 추가
            {
                GoToNextPoint();
            }
        }
    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 2) return;

        // 배열에서 다음 순찰 지점을 목표로 설정
        agent.SetDestination(patrolPoints[destPoint].position);

        // 다음 순찰 지점 인덱스 설정 (배열을 순환)
        destPoint = (destPoint + 1) % patrolPoints.Length;
    }
}
*/