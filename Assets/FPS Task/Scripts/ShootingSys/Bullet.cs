using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace FPS.Shooting
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] float speed;
        [SerializeField] ParticleSystem[] hitEffects;



        IObjectPool<Bullet> _bulletPool;
        Rigidbody _rb;

        private float _damage;
        private float _delta;

        private const float DestroyTime = 10;


       
        private void Awake() 
        {
            _rb = GetComponent<Rigidbody>();
        }
        

        private void Update() 
        {
            
            _delta += Time.deltaTime;
            if(_delta > DestroyTime && gameObject.activeSelf) _bulletPool.Release(this);

        }

        private void OnCollisionEnter(Collision other) 
        {
            // TODO add blood effects if enemy
            if(other.collider.tag != "Enemy" && hitEffects.Length > 0 )
            {
                CreateBulletEffects(other);
            }
            else
            {
                DamageTarget(other);
            }


            if (gameObject.activeSelf) _bulletPool.Release(this);
        }

        private void DamageTarget(Collision other)
        {
            Health enemyHealth = other.gameObject.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(_damage, gameObject);
            }
        }

        private void CreateBulletEffects(Collision other)
        {
            ContactPoint contactPoint = other.contacts[0];
            foreach (var effect in hitEffects)
            {
                // TODO pooling effects
                GameObject obj = Instantiate(effect, contactPoint.point, Quaternion.LookRotation(contactPoint.normal)).gameObject;
                obj.transform.parent = other.transform;
                Destroy(obj, effect.duration);
            }
        }



        public void Init(Vector3 target,float damage, IObjectPool<Bullet> pool)
       {
          _damage = damage;
          _bulletPool = pool;

          Vector3 targetDirection = (target - transform.position).normalized;
          transform.forward = targetDirection;
          _rb.AddForce(targetDirection * speed, ForceMode.Impulse);
        }
    }
}
