using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Vuforia;

namespace Assets.Scripts {
    public class CapaTargetDetection : MonoBehaviour {
        private TrackableBehaviour mTrackableBehaviour;
        public AudioSource audioSource;
        public PlayableDirector director;
        private IEnumerator fadeInSound, fadeOutSound;

        private bool initialPlay = true;

        void Start() {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            fadeInSound = AudioSourceFadeIn(audioSource, .5f);
            fadeOutSound = AudioSourceFadeOut(audioSource, .1f);
            if (mTrackableBehaviour) {
                mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);
            }
        }
        void OnTrackableStatusChanged(TrackableBehaviour.StatusChangeResult statusChangeResult) {
            if (statusChangeResult.NewStatus == TrackableBehaviour.Status.DETECTED ||
                statusChangeResult.NewStatus == TrackableBehaviour.Status.TRACKED ||
                statusChangeResult.NewStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
                OnTrackingFound();
            } else {
                OnTrackingLost();
            }
        }
        private void OnTrackingFound() {
            if (initialPlay) {
                director.Play();
                audioSource.Play();
                StopCoroutine(fadeOutSound);
                StartCoroutine(fadeInSound);
                initialPlay = false;
            }
        }
        private void OnTrackingLost() {
            StopCoroutine(fadeInSound);
            StartCoroutine(fadeOutSound);
        }

        public static IEnumerator AudioSourceFadeIn(AudioSource audioSource, float FadeTime) {
            float startVolume = audioSource.volume;

            while (audioSource.volume < 1) {
                audioSource.volume += startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }

        }

        public static IEnumerator AudioSourceFadeOut(AudioSource audioSource, float FadeTime) {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0) {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }

        }

    }

}
