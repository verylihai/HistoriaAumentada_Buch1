using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using Vuforia;

namespace Assets.Scripts {

    public class CapaTargetDetection : MonoBehaviour {
        private TrackableBehaviour mTrackableBehaviour;
        public AudioSource audioSource;
        public AudioSource audioSourceTrackingLost;
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
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
            } else {
                OnTrackingLost();
                GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>().clearFlags = CameraClearFlags.Color;
            }
        }
        private void OnTrackingFound() {
            director.Play();
            if (initialPlay) {
                StartCoroutine("AudioSourceFadeOutTrackingLost");
                StopCoroutine("AudioSourceFadeInTrackingLost");
            }
            initialPlay = false;

            StopCoroutine("AudioSourceFadeOut");
            StartCoroutine("AudioSourceFadeIn");
        }
        private void OnTrackingLost() {
            StopCoroutine("AudioSourceFadeIn");
            StartCoroutine("AudioSourceFadeOut");

            StopCoroutine("AudioSourceFadeOutTrackingLost");
            StartCoroutine("AudioSourceFadeInTrackingLost");
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
            yield return new WaitForSeconds(.5f);
        }

        public IEnumerator AudioSourceFadeOut() {
            float startVolume = audioSource.volume;
            float FadeTime = 1f;

            while (audioSource.volume > 0f) {
                audioSource.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }

        public IEnumerator AudioSourceFadeInTrackingLost() {
            float startVolume = 0.333f;
            float FadeTime = .5f;

            audioSourceTrackingLost.volume = 0;
            audioSourceTrackingLost.Play();

            while (audioSourceTrackingLost.volume < .5f) {
                audioSourceTrackingLost.volume += startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            yield return new WaitForSeconds(.5f);
        }

        public IEnumerator AudioSourceFadeOutTrackingLost() {
            float startVolume = audioSourceTrackingLost.volume;
            float FadeTime = 1f;

            while (audioSourceTrackingLost.volume > 0f) {
                audioSourceTrackingLost.volume -= startVolume * Time.deltaTime / FadeTime;
                yield return null;
            }
            yield return new WaitForSeconds(1f);
        }

    }

}
