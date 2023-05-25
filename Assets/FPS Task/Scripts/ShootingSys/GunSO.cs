using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Shooting
{
    
    [CreateAssetMenu(fileName = "GunSO", menuName = "FPS/GunSO", order = 0)]
    public class GunSO : ScriptableObject 
    {
        public GameObject prefab;
        public Bullet bulletPrefab;
        public ParticleSystem muzzleEffect;

        public float damage;
        public int ammoNumb;
        public float fireRate;
        public float reloadTime;
        public bool isAutomatic;



    }
}
