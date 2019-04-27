using UnityEngine;

public class Theremin : MonoBehaviour
{
    [SerializeField]
    private Transform[] hands;
    [SerializeField]
    private AudioSource audioSource = null;
    [SerializeField]
    private Transform pitch = null;
    [SerializeField]
    private float pitchDistanceRange = 3.5f;
    [SerializeField]
    private float pitchHeight = 5f;
    [SerializeField]
    private Transform volume = null;
    [SerializeField]
    private float volumeRadius = 2f;

    float pitchVel;
    float volVel;

    void Update()
    {
        float nearestPitchProximity = 100f;
        float nearestVolProximity = 100f;
        
        foreach (Transform hand in hands)
        {
            if (Mathf.Abs(pitch.position.y - hand.position.y) < pitchHeight * .5f)
            {
                float thisDistance = Vector2.Distance(new Vector2(pitch.position.x, pitch.position.z), new Vector2(hand.position.x, hand.position.z)) * 2f;
                nearestPitchProximity = Mathf.Min(nearestPitchProximity, thisDistance);
            }
            nearestVolProximity = Mathf.Min(nearestVolProximity, Vector3.Distance(volume.transform.position, hand.transform.position));
        }
       
        audioSource.pitch = Mathf.SmoothDamp(audioSource.pitch, Wrj.Utils.Remap(nearestPitchProximity, 0, pitchDistanceRange, 6f, 3f), ref pitchVel, .1f);
        audioSource.volume = Mathf.SmoothDamp(audioSource.volume, Wrj.Utils.Remap(nearestVolProximity, 0, volumeRadius, 0f, 1f), ref volVel, .1f);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(pitch.position, new Vector3(pitchDistanceRange, pitchHeight, pitchDistanceRange));
        Gizmos.DrawWireSphere(volume.position, volumeRadius);
    }
}
