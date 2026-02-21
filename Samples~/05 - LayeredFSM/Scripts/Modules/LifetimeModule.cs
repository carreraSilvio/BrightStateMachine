using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class LifetimeModule : ActorModule
    {
        [SerializeField]
        private float _lifetime = 3.0f;

        private float _timer;

        private void OnEnable()
        {
            _timer = Time.time;
        }

        private void Update()
        {
            if(Time.time - _timer >= _lifetime)
            {
                Destroy(Actor.gameObject);
            }
        }
    }
}
