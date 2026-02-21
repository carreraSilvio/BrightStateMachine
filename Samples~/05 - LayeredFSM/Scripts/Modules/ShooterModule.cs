using UnityEngine;

namespace BrightLib.StateMachine.Samples.LayeredFSMSample
{
    public class ShooterModule : ActorModule
    {
        public bool IsOutOfAmmo => _currentAmmo == 0;
        public bool IsReloading { get; private set; }

        [SerializeField]
        private float _shootInterval = 0.1f;
        [SerializeField]
        private float _reloadDuration = 0.3f;
        [SerializeField]
        private int _totalAmmo = 10;
        [SerializeField]
        private GameObject _bulletPreafab;

        private float _shootTimer;
        private float _reloadTimer;
        private int _currentAmmo = 10;

        private enum State
        {
            Ready,
            Shooting,
            Reloading
        }
        private State _state;

        private void Update()
        {
            if(_state == State.Ready)
            {
            }
            else if(_state == State.Shooting)
            {
                if (Time.time - _shootTimer >= _shootInterval)
                {
                    return;
                }
            }
            else if(_state == State.Reloading)
            {
                if (Time.time - _reloadTimer >= _reloadDuration)
                {
                    _currentAmmo = _totalAmmo;
                    _state = State.Ready;
                }
            }
        }

        public void Shoot(Vector2 direction)
        {
            Shoot(direction.x, direction.y);
        }
        public void Shoot(float x, float y)
        {
            if(IsOutOfAmmo)
            {
                return;
            }
            if(Time.time - _shootTimer < _shootInterval)
            {
                return;
            }

            //spawn bullet
            GameObject bullet = GameObject.Instantiate(_bulletPreafab, transform.position, Quaternion.identity);
            bullet.GetComponent<Actor>().FaceDirection = Actor.FaceDirection;

            //decrease ammo
            _currentAmmo = Mathf.Max(0, _currentAmmo - 1);
            if(_currentAmmo == 0)
            {
                _state = State.Ready; 
            }
            else
            {
                _shootTimer = Time.time;
                _state = State.Shooting;
            }
        }

        public void Reload()
        {
            if(_state == State.Reloading)
            {
                return;
            }
            _reloadTimer = Time.time;
            _state = State.Reloading;
        }

    }
}
