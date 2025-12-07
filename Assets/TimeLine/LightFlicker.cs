using System.Collections;
using UnityEngine;

// 스크립트 파일 이름을 LightFlickerIntensity로 바꾸는 것을 권장합니다.
public class LightFlickerIntensity : MonoBehaviour
{
    private Light _light; // 조명 컴포넌트
    private float _originalIntensity; // 원래 빛의 밝기
    public float minStableTime = 0.2f;
    public float maxStableTime = 1.0f;
    public float minDimIntensity = 0.0f;
    public float maxDimIntensity = 0.1f;
    public int minFlickerCount = 1;
    public int maxFlickerCount = 5;
    public float minFlickerSpeed = 0.1f;
    public float maxFlickerSpeed = 0.2f;

    void Start()
    {
        // 내 오브젝트의 Light 컴포넌트를 가져옴
        _light = GetComponent<Light>();

        // 시작할 때의 밝기를 '원래 밝기'로 저장
        _originalIntensity = _light.intensity;

    }

    public void Flciker()
    {
        StartCoroutine(FlickerRoutine());
    }

    private IEnumerator FlickerRoutine()
    {
        // 게임이 끝날 때까지 무한 반복
        while (true)
        {
            // 1. 조명 밝기를 원래대로 (안정적인 상태)
            _light.intensity = _originalIntensity;

            // 2. 안정적인 시간(랜덤)만큼 기다린다
            float stableWaitTime = Random.Range(minStableTime, maxStableTime);
            yield return new WaitForSeconds(stableWaitTime);

            int flickerCount = Random.Range(minFlickerCount, maxFlickerCount);
            // 3. 정해진 횟수만큼 빠르게 밝기를 조절한다 (지지직-)
            for (int i = 0; i < flickerCount; i++)
            {
                // 어두워지기 (랜덤한 밝기로)
                _light.intensity = Random.Range(minDimIntensity, maxDimIntensity);

                // 랜덤한 속도로 기다리기
                float randomSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);
                yield return new WaitForSeconds(randomSpeed);

                // 다시 원래 밝기로 (짧게)
                _light.intensity = _originalIntensity;

                // 랜덤한 속도로 다시 기다리기
                randomSpeed = Random.Range(minFlickerSpeed, maxFlickerSpeed);
                yield return new WaitForSeconds(randomSpeed);
            }

            // (다시 1번으로 돌아가서 반복)
        }
    }
}