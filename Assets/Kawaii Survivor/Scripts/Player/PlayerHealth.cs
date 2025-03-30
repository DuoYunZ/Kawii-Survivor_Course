using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
public class PlayerHealth : MonoBehaviour
{

    [Header("Setting")]
    [SerializeField] private int maxHealth;
    private int health;

    [Header("Elements")]
    [SerializeField] private Slider healthSliders;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header(" Actions ")]
    public static Action<Vector2> onAttackDodged;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;

        UpdateUI();
        
    }



    public void TakeDamage(int damage)
    {
        int realDamage = Mathf.Min(damage, health);
        health -= realDamage;

        UpdateUI();

       

        if(health <=0)        
            PassAway();
        
    }
    private void  PassAway()
    {
        Debug.Log("Ded");
        SceneManager.LoadScene(0);
    }
    private void UpdateUI()
    {
        float healthBarvalue = health / maxHealth;
        healthSliders.value = healthBarvalue;
        healthText.text = (int)health + "/" + maxHealth;
    }
}
    