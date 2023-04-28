using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    [SerializeField]
    private AudioClip[] footsteps;
    public AudioClip jump, dash, slide, land;
    private AudioSource source;
    private string lastPlayedClip;
    private Movement move;

    void Start()
    {
        source = GetComponent<AudioSource>();
        move = GetComponentInParent<Movement>();
    }
    
    public void PlaySound (string clip) {
        // Make sure sliding sound only play once in one slide event firing and stops when
        // received other events
        if (source.isPlaying && move.wallSlide){
            return;
        } else if (source.isPlaying && lastPlayedClip == "slide") {
            source.Stop();
        } 

        switch (clip) {
        case "jump":
            source.PlayOneShot(jump);
            print("jump");
            break;
        case "dash":
            source.PlayOneShot(dash);
            print("dash");
            break;
        case "slide":
            source.PlayOneShot(slide);
            print("slide");
            break;
        case "land":
            source.PlayOneShot(land);
            print("Landed");
            break;
        }
        lastPlayedClip = clip;
    }

    public void Step()
    {
        if (PlayerPrefs.GetInt("sfx") == 1)
        {
            AudioClip clip = GetRandomClip();
            source.PlayOneShot(clip);
        }
    }

    private AudioClip GetRandomClip()
    {
        return footsteps[Random.Range(0, footsteps.Length)];
    }
}
