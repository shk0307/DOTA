﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

abstract public class Monster : MonoBehaviour {
    public GameObject target;
    public NavMeshAgent agent;
    protected Animator anim;

    public float MonsterStartHealth = 100;
    private float MonsterHealth;

    public int MonsterAttackDamage = 10;
    //public float speed = 25f;

    public Vector3 translation = Vector3.zero;

    [Header("Unity Stuff")]
    public Image HealthBar;

    private bool dead = false;

    protected PlayerHealthController playerHealthController;

    // Use this for initialization
    protected void Start()
    {
        agent = GetComponent <NavMeshAgent> ();
        monsterInit();
        InitializeRotation();
        playerHealthController = intitializePlayerHealthController();
        MonsterHealth = MonsterStartHealth;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (isDead()) return; // so that the object does not move

        //Moves monster towards target
        monsterMovement();

        //Destroys monster once it reaches player for memory management purposes
        if (playerDamageCriteria())
        {
            damagePlayer();
        }
    }

    protected PlayerHealthController intitializePlayerHealthController ()
    {
        return FindObjectOfType<PlayerHealthController>();
    }

    protected bool isDead()
    {
        return dead;
    }

    protected void playDead()
    {
        // TODO: change this so that it works for not just ghosts
        //anim.Play("ghost_die", 0);
    }


    protected void InitializeRotation()
    {
        transform.rotation = new Quaternion(0, 180, 0, 0);
    }

    protected void LookAt (Vector3 direction)
    {
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), 0.1f);
    }

    protected void destroySelf()
    {
        StartCoroutine(playDeathAnimation());
    }

    IEnumerator playDeathAnimation()
    {
        playDead();
        yield return new WaitForSeconds(1.5f);
        Destroy(this.gameObject);
    }

    protected void checkDead()
    {
        if (MonsterHealth <= 0)
        {
            dead = true;
            destroySelf();
        }
    }

    public void takeDamage(int damage)
    {
        MonsterHealth -= damage;

        HealthBar.fillAmount = MonsterHealth / MonsterStartHealth;

        checkDead();
    }

    protected void damagePlayer()
    {
        // Damage the player.
        playerHealthController.TakeDamage(MonsterAttackDamage);
        // Destroy the monster.
        Destroy(this.gameObject);
    }

    protected abstract void monsterInit();

    protected abstract void monsterMovement();

    protected abstract bool playerDamageCriteria();

}
