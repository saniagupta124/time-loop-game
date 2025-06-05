using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ObjectOfInterestScript : MonoBehaviour
{
    [Header("Audio Settings")]
    [SerializeField]
    [Tooltip("Drag in your audio clip here!")]
    AudioClip Clip;
    [SerializeField]
    [Range(1f, 50f)]
    [Tooltip("How close the player has to be for audio to play")]
    float AudioPlayRange = 10f;
    float AudioPlayRange_Sqr;
    [SerializeField]
    [Tooltip("Whether audio plays from where it left off or restarts when a player returns")]
    bool RestartOnEnter = false;
    [SerializeField]
    [Tooltip("Check this box to make sure audio doesn't play again if player has already heard NoRepeatThreshold seconds of it.")]
    bool DisableAudioReturnAfterThreshold = false;
    [SerializeField]
    [Tooltip("Amount of seconds before audio on this object will not play upon a players return")]
    float NoRepeatThreshold = 5.0f;
    AudioSource Audio;
    bool AudioCanPlay = true;

    [Header("Image Settings")]
    [SerializeField]
    [Tooltip("Type the # images in, then drag all your images here in order!")]
    Texture[] Images;
    [SerializeField]
    [Range(1f, 10f)]
    [Tooltip("Size of the image -- press play and move slider to play with it!")]
    float ImageSize = 5;
    [SerializeField]
    [Tooltip("How far away from the picture a player is when it reaches full opacity")]
    [Range(10f, 100f)]
    float ImageFadeInDistance = 25.0f;
    float ImageFadeInDistance_Sqr;
    [SerializeField]
    [Tooltip("How far away from the picture a player is when it disappears")]
    [Range(10f, 100f)]
    float ImageGoneDist = 100.0f;
    RawImage ImageScript;
    float AnimTimer;
    [SerializeField]
    [Tooltip("Animation speed in FPS")]
    [Range(0, 60f)]
    float AnimationSpeed = 3f;
    float AnimFrameTime;
    int AnimIndex = 0;

    [SerializeField] bool facePlayerBool = true;
    private void Awake()
    {
        Audio = GetComponentInChildren<AudioSource>();
        ImageScript = GetComponentInChildren<RawImage>();
        AudioCanPlay = true;
    }

    void Start()
    {
        ImageScript.texture = Images[0];
        Audio.clip = Clip;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, AudioPlayRange);
    }

    // Update is called once per frame
    void Update()
    {
        // For live adjustment
        AnimFrameTime = 1.0f / AnimationSpeed;
        ImageScript.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, ImageSize);
        ImageScript.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, ImageSize);
        AudioPlayRange_Sqr = AudioPlayRange * AudioPlayRange;
        ImageFadeInDistance_Sqr = ImageFadeInDistance * ImageFadeInDistance;
        if (ImageFadeInDistance > ImageGoneDist) { float i = ImageGoneDist; ImageGoneDist = ImageFadeInDistance; ImageFadeInDistance = i; }
           
        if (facePlayerBool)
        {
            Quaternion targetRotation = Quaternion.LookRotation(PlayerScript.instance.transform.position - transform.position);
            float t = Mathf.Min(0.8f * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, t);
        }

        float _playerSqrDist = (PlayerScript.instance.transform.position - transform.position).sqrMagnitude;

        // Image fading
        float _playerDist = Mathf.Sqrt(_playerSqrDist);
        float _imageOpacity = (_playerDist + ImageFadeInDistance - ImageGoneDist) / (ImageFadeInDistance - ImageGoneDist);
        _imageOpacity = _imageOpacity < 0 ? 0 : _imageOpacity;
        _imageOpacity = _imageOpacity > 1 ? 1 : _imageOpacity;
        Color _imageNewColor = new Color(ImageScript.color.r, ImageScript.color.g, ImageScript.color.b, _imageOpacity);
        ImageScript.color = _imageNewColor;

        if (Images.Length > 1)
        {
            AnimTimer += Time.deltaTime;
            if (AnimTimer >= AnimFrameTime)
            {
                AnimIndex = (AnimIndex + 1) % Images.Length;
                ImageScript.texture = Images[AnimIndex];
                AnimTimer = 0;
            }
        }

        // Audio play
        if (!Audio.isPlaying && AudioCanPlay)
        {
            if (_playerSqrDist <= AudioPlayRange_Sqr)
            {
                Audio.Play();
            }
        }
        else
        {
            if (DisableAudioReturnAfterThreshold)
            {
                AudioCanPlay = Audio.time < NoRepeatThreshold;
            }
            if (_playerSqrDist > AudioPlayRange_Sqr)
            {
                if (RestartOnEnter) Audio.Stop();
                else Audio.Pause();
            }
        }
    }
}
