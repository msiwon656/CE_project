using UnityEngine;

// [System.Serializable]을 붙여야 인스펙터 창에서 편집할 수 있습니다.
[System.Serializable]
public class SubtitleClip
{
    public string subtitleText; // 자막 내용
    public float startTime;    // 시작 시간 (초)
    public float endTime;      // 종료 시간 (초)
}