using UnityEngine;
using UnityEngine.Playables;
using Vuforia;

namespace Assets.Scripts
{
    public class CapaTargetDetection : MonoBehaviour
    {
        //public new ParticleSystem particleSystem;
        private TrackableBehaviour mTrackableBehaviour;
        public PlayableDirector director;

        private bool initialPlay = true;

        // Use this for initialization
        void Start()
        {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour)
            {
                //particleSystem.Stop();
                mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);
            }
        }
        //private void Awake()
        //{
        //    if (director != null)
        //    {
        //        director.played += Director_Played;
        //        director.stopped += Director_Stopped;
        //    }
        //}

        //private void Director_Stopped(PlayableDirector obj)
        //{
        //    controlPanel.SetActive(true);
        //}

        //private void Director_Played(PlayableDirector obj)
        //{
        //    controlPanel.SetActive(false);
        //}

        void OnTrackableStatusChanged(TrackableBehaviour.StatusChangeResult statusChangeResult)
        {
            if (statusChangeResult.NewStatus == TrackableBehaviour.Status.DETECTED ||
                statusChangeResult.NewStatus == TrackableBehaviour.Status.TRACKED ||
                statusChangeResult.NewStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                Debug.Log("OnTrackableStatusChanged: " + statusChangeResult.NewStatus);
                OnTrackingFound();
            }
        }
        private void OnTrackingFound()
        {
            if (/*particleSystem != null && */initialPlay)
            {
                //particleSystem.Play();
                director.Play();
                initialPlay = false;
            }
        }
    }

}
