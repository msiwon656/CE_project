using UnityEngine;
using System.Collections.Generic;
using TMPro; // TextMeshPro를 사용하려면 이 줄이 필수입니다!

public class SubtitleManager : MonoBehaviour
{
    [Header("연결할 컴포넌트")]
    public AudioSource audioSource;         // AI 음성 오디오 소스
    public TextMeshProUGUI subtitleDisplay; // 자막을 표시할 Text (TMPro)

    [Header("자막 데이터")]
    public List<SubtitleClip> subtitleClips; // 자막 클립 리스트

    void Start()
    {
        if (subtitleDisplay != null)
        {
            subtitleDisplay.text = ""; // 시작할 때 텍스트 비우기
        }
    }

    void Update()
    {
        // 오디오나 자막창이 없으면 실행 중지
        if (audioSource == null || subtitleDisplay == null) return;

        // 오디오가 재생 중이 아니면 텍스트 비우기
        if (!audioSource.isPlaying)
        {
            subtitleDisplay.text = "";
            return;
        }

        // 현재 오디오 재생 시간
        float currentTime = audioSource.time;
        string textToShow = ""; // 이번 프레임에 표시할 텍스트

        // 모든 자막 클립을 확인
        foreach (var clip in subtitleClips)
        {
            // 현재 시간이 이 클립의 시작과 끝 시간 사이에 있는지 확인
            if (currentTime >= clip.startTime && currentTime <= clip.endTime)
            {
                textToShow = clip.subtitleText;
                break; // 알맞은 자막을 찾았으면 루프 종료
            }
        }

        // 찾은 텍스트를 UI에 표시
        subtitleDisplay.text = textToShow;
    }
}