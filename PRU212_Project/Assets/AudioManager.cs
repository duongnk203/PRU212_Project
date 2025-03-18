using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Đảm bảo không bị null
    public AudioClip vacham;
    public AudioClip knife;
    public AudioClip dead;
    public AudioClip cung;
    public AudioClip laze;

    private void Awake()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>(); // Tạo mới nếu bị null
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("AudioClip không tồn tại!");
            return;
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource đã bị hủy! Đang tạo lại...");
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.PlayOneShot(clip);
    }
}

