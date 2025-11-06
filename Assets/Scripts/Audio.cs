using System;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour {
    [SerializeField] private AudioSource sourcePrefab;
    private Camera mainCamera;
    public AudioMixer mixer;
    
    private static Camera MainCamera => instance.mainCamera;
    private static AudioSource SourcePrefab => instance.sourcePrefab;
    public static AudioMixerGroup MasterGroup => instance.mixer.FindMatchingGroups("Master")[0];
    public static AudioMixerGroup SfxGroup => instance.mixer.FindMatchingGroups("SFX")[0];
    public static AudioMixerGroup MusicGroup => instance.mixer.FindMatchingGroups("Music")[0];
        
    private static Audio instance;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
            throw new Exception("SFXManager already exists");
        }
        instance = this;
        
        mainCamera = Camera.main;
    }

    public static void PlaySfxAtPos(AudioResource clip, Vector3 position, AudioMixerGroup group) {
        var source = Instantiate(SourcePrefab, instance.transform);
        source.resource = clip;
        source.outputAudioMixerGroup = group;
        source.transform.position = position;
        source.Play();
    }
    public static void PlaySfx(AudioResource clip, AudioMixerGroup group) {
        var source = Instantiate(SourcePrefab, MainCamera.transform);
        source.resource = clip;
        source.outputAudioMixerGroup = group;
        source.Play();
    }
}
