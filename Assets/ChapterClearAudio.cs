using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterClearAudio : MonoBehaviour
{
    public AudioSource soundChapterClear;

    public void OnClickClearSound()
    {
        soundChapterClear.Play();
    }
}
