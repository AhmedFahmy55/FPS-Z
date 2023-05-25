using FPS.Shooting;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace FPS.UI.Shooting
{
    public class AmmoUI : MonoBehaviour
    {

        [SerializeField] TextMeshProUGUI maxMag;
        [SerializeField] TextMeshProUGUI currentMag;

        Gun playerGun;


        private void Awake()
        {
            playerGun = GameObject.FindWithTag("Player").GetComponentInChildren<Gun>();
        }

        private void OnEnable()
        {
            playerGun.OnShoot += UpdateAmmo;
            playerGun.OnReload += ResetAmmo;
        }
        private void Start()
        {
            maxMag.text = $"{playerGun.GetMaxMagNumb()}";
            currentMag.text = maxMag.text;
        }

        private void ResetAmmo()
        {
            currentMag.text = maxMag.text;
        }

        private void OnDisable()
        {
            playerGun.OnShoot -= UpdateAmmo;
            playerGun.OnReload -= ResetAmmo;


        }

        void UpdateAmmo(int numb)
        {
            currentMag.text = $"{numb}";
        }
    }
}
