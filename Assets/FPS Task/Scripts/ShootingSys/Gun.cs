using FPS.Sounds;
using System;
using System.Drawing;
using UnityEngine;
using UnityEngine.Pool;

namespace FPS.Shooting
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] GunSO weapon;
        [SerializeField] Transform firePoint;

        //public
        public bool IsReloading { get; set; }
        public event Action<int> OnShoot;
        public event Action OnReload;


        // unity stuff
        InputManager _inputManager;
        Camera _mainCam;
        Animator _animator;
        ParticleSystem _muzzleEffect;
        IObjectPool<Bullet> bulletPool;

        // privates
        private CompatTarget _currentScanedTarget;
        private UnityEngine.Color _currentObjectColor;
        private int _maxMagNumb;
        private int _currentMagNumb;
        private float _fireDelta = 0;

        //animations id 
        private int _shootID;
        private int _reLoadID;

        private void Awake()
        {
            _inputManager = GetComponentInParent<InputManager>();
            _mainCam = Camera.main;
            _animator = GetComponentInParent<Animator>();
            SetAnimationIDS();
            bulletPool = new ObjectPool<Bullet>(CreatBullet,OnGetBullet,OnReleaseBullet
                ,OnDestroyBullet,maxSize:weapon.ammoNumb);

        }


        private void Start()
        {
            _maxMagNumb = weapon.ammoNumb;
            _currentMagNumb = _maxMagNumb;
            SetupMuzzleEffect();
            _fireDelta = Time.time + weapon.fireRate;

        }

        private void Update() 
        {
            HandleShooting();
            if (_inputManager.Reload)
            {
                Reload();
                _inputManager.Reload = false;
            }
           
        }

        private void HandleShooting()
        {
            Vector2 crosshair = new Vector2(Screen.width/2,Screen.height/2);
            Ray ray = _mainCam.ScreenPointToRay(crosshair);
            if(Physics.Raycast(ray.origin,ray.direction,out RaycastHit hitPoint,900))
            {
                /*CompatTarget target = hitPoint.collider.GetComponent<CompatTarget>();
                if(target != null)
                {
                    HighlightTarget(target);
                }
                else
                {
                    RestLastHighlightedObj();
                }*/

                if(!IsReloading) Fire(hitPoint.point);

            }

        }

        private void Fire(Vector3 point)
        {
            if (_inputManager.Shoot)
            {
                if (Time.time < _fireDelta) return;

                Bullet bullet = bulletPool.Get();
                bullet.Init(point,weapon.damage,bulletPool);

                SoundManager.Instannce.PlaySound(Sound.FireSingle,firePoint.position);

                if(_muzzleEffect != null)_muzzleEffect.Play();
                _animator.SetTrigger(_shootID);

                _currentMagNumb--;

                OnShoot?.Invoke(_currentMagNumb);

                if (!weapon.isAutomatic) _inputManager.Shoot = false;

                if (_currentMagNumb == 0)
                {
                    Reload();
                }
                _fireDelta = Time.time + weapon.fireRate;
            }
        }

        private void Reload()
        {
            IsReloading = true;
            _animator.SetTrigger(_reLoadID);
            OnReload?.Invoke();
            SoundManager.Instannce.PlaySound(Sound.Reload, firePoint.position);

        }

        public void ResetAmmo()
        {
            _currentMagNumb = _maxMagNumb;
        }

        private void HighlightTarget(CompatTarget target)
        {
            if (target == _currentScanedTarget) return;
            RestLastHighlightedObj();



            _currentScanedTarget = target;
            Renderer mr = _currentScanedTarget.GetComponentInChildren<Renderer>();
            _currentObjectColor = mr.materials[0].color;
            if (mr != null)
            {
                mr.materials[0].color = UnityEngine.Color.green;
            }

        }

        private void RestLastHighlightedObj()
        {
            if (_currentScanedTarget == null) return;
            _currentScanedTarget.GetComponentInChildren<Renderer>().materials[0].color = _currentObjectColor;
            _currentScanedTarget = null;
        }

        public int GetMaxMagNumb()
        {
            return weapon.ammoNumb;
        }

        private void SetupMuzzleEffect()
        {
            if(weapon.muzzleEffect == null) return;
            _muzzleEffect = Instantiate(weapon.muzzleEffect);
            _muzzleEffect.transform.parent = firePoint;
            _muzzleEffect.transform.localPosition = Vector3.zero;
            _muzzleEffect.transform.localRotation = Quaternion.identity;

        }

        private void SetAnimationIDS()
        {
            _shootID = Animator.StringToHash("Shoot");
            _reLoadID = Animator.StringToHash("Reload");
        }
     

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
            {
                SoundManager.Instannce.PlaySound(Sound.FootStep, transform.position);

            }
        }

        public Bullet CreatBullet()
        {
            Bullet bullet = Instantiate(weapon.bulletPrefab, firePoint.position, Quaternion.identity);
            return bullet;
        }
        private void OnGetBullet(Bullet bullet)
        {
            bullet.transform.position = firePoint.position;
            bullet.gameObject.SetActive(true);
        }

        private void OnReleaseBullet(Bullet bullet)
        {
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyBullet(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }
    }

    
}
