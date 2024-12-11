using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public Slider bonusHealthSlider;
    public float maxHealth;
    public float health;

    private PlayerHealth player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        maxHealth = player.maxHealth;
        health = maxHealth;
        bonusHealthSlider.value = 0f;
        healthSlider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        health = player.currentHealth; 
        if(health >= maxHealth){
            healthSlider.value = maxHealth;
            bonusHealthSlider.value = health - maxHealth;
        } else {
            healthSlider.value = health;
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
        if(health <= 0){
            health = 0;
        }
    }

    public void healLife(float heal)
    {
        health += heal;
        if(health >= maxHealth*2){
            health = maxHealth*2;
        }
    }
}
