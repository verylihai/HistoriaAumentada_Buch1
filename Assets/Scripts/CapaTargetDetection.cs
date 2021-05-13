using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Vuforia;

namespace Assets.Scripts {

    public class CapaTargetDetection : MonoBehaviour {
        private TrackableBehaviour mTrackableBehaviour;
        public AudioSource audioSource;
        public PlayableDirector director;

        private bool initialPlay = true;

        void Start() {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
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
            }
            StopCoroutine("AudioSourceFadeOut");
            StartCoroutine("AudioSourceFadeIn");
        }
        private void OnTrackingLost() {
            StopCoroutine("AudioSourceFadeIn");
            StartCoroutine("AudioSourceFadeOut");
        }

        public IEnumerator AudioSourceFadeIn() {
            float startVolume = 0.333f;
            float FadeTime = .5f;

            audioSource.volume = 0;
            audioSource.Play();

            while (audioSource.volume < 1f) {
                audioSource.volume += startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }

        }

        public IEnumerator AudioSourceFadeOut() {
            float startVolume = audioSource.volume;
            float FadeTime = 1f;

            while (audioSource.volume > 0f) {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
        }

    }

}
