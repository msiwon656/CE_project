using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Playables; // PlayableDirector를 사용하려면 필수!

public class SubtitleManager : MonoBehaviour
{
    [Header("연결할 컴포넌트")]
    // 기존 AudioSource 대신 또는 함께 사용
    public PlayableDirector playableDirector; // Timeline을 재생하는 디렉터
    public TextMeshProUGUI subtitleDisplay;

    [Header("자막 데이터")]
    public List<SubtitleClip> subtitleClips; // 자막 클립 리스트

    void Update()
    {
        // Timeline Director가 없으면 실행 중지
        if (playableDirector == null || subtitleDisplay == null) return;

        // Timeline이 재생 중이 아니면 텍스트 비우기
        if (playableDirector.state != PlayState.Playing)
        {
            subtitleDisplay.text = "";
            return;
        }

        // 🟢 핵심 변경: Timeline의 현재 시간 사용
        float currentTime = (float)playableDirector.time;
        string textToShow = "";

        // 모든 자막 클립을 확인 (이후 코드는 동일)
        foreach (var clip in subtitleClips)
        {
            if (currentTime >= clip.startTime && currentTime < clip.endTime)
            {
                textToShow = clip.subtitleText;
                break;
            }
        }

        subtitleDisplay.text = textToShow;
    }
}