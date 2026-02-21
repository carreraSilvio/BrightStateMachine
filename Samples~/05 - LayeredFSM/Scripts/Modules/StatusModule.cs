using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class StatusModule : ActorModule
    {
        public float SlowSpeedModifier => _slowSpeedModifier;
        [SerializeField]
        private float _slowDuration = 3f; //in a real project would be kept somewhere else
        [SerializeField]
        private float _slowSpeedModifier = 0.3f;
        public bool IsSlowed { get; private set; }

        private float _slowTimer;

        private void Update()
        {
            if(IsSlowed)
            {
                if(Time.time - _slowTimer >= _slowDuration)
                {
                    IsSlowed = false;
                }
            }
        }

        public void ApplySlowStatus()
        {
            if(IsSlowed) 
            {
                return;
            }
            IsSlowed = true;
            _slowTimer = Time.time;
        }

        public void ClearSlowStatus()
        {
            IsSlowed = false;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            var actor = collision.GetComponentInParent<Actor>();
            if(actor != null) 
            {
                if(actor.name.Contains("StingingGrass"))
                {
                    ApplySlowStatus();
                }
            }
        }

    }
}
