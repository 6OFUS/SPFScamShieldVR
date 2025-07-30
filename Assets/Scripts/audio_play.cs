using UnityEngine;

public class AnimationAudioEvent : MonoBehaviour
{
    public AudioClip audioClip;

    public void PlayAudio()
    {
        // Create a temporary AudioSource if one doesn't exist
        AudioSource audioSource = gameObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Play the clip
        audioSource.PlayOneShot(audioClip);
    }
}
