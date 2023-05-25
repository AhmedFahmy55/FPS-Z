using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] Slider healthSlider;

    Health playerHealth;


    float playerMaxHealth;
    float playercurrentHealth;

    private void Awake()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();
    }
    private void OnEnable()
    {
        playerHealth.OnTakeDamage.AddListener(UpdatePlayerHealth);
    }

    private void Start()
    {
        playerMaxHealth = playerHealth.GetMaxHeath();
        playercurrentHealth = playerHealth.GetCurrentHealth();
        healthSlider.value = playerMaxHealth / playercurrentHealth;
    }

    public void UpdatePlayerHealth(float value)
    {
        playercurrentHealth -= value;
        healthSlider.value  = playercurrentHealth / playerMaxHealth ;
    }
}
