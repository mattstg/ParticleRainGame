using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    AudioSource audiosrc;
    ParticleSystem waterEffect;

    //Hose vars
    float currentHosePower;
    float hoseAcceleration = 40; //rate of hose power increase and decrease
    float maxHosePower = 150;  //max number of particles

    public override void Initialize()
    {
        base.Initialize();
        audiosrc = GetComponent<AudioSource>();
        waterEffect = GetComponentInChildren<ParticleSystem>();
        var em = waterEffect.emission;
        em.rateOverTime = 0;
    }

    public override void Refresh()
    {
        base.Refresh();
        UseHose(InputManager.Instance.updateInputPkg.useHosePressed);
    }
    public override void PhysicsRefresh()
    {
        base.PhysicsRefresh();
        if (InputManager.Instance.fixedUpdateInputPkg.jumpFirstPressed)
            Jump();
        Move(InputManager.Instance.fixedUpdateInputPkg.moveInput);
    }

    protected void UseHose(bool isPressed)
    {
        currentHosePower += ((isPressed) ? 1 : -1) * hoseAcceleration * Time.deltaTime;
        currentHosePower = Mathf.Clamp(currentHosePower, 0, maxHosePower);


        //does NOT work, just the strange way particle systems are coded
        //system.emission.rate = Random.Range (emmisionRateMin, emmisionRateMax);

        //works, you need to extract it first, then affect it
        //waterEffect.emission.rateOverTime = 0;
        var em = waterEffect.emission;
        
        if(currentHosePower != 0 && em.rateOverTime.Evaluate(0) != 0) //If not already zero and being set to 0
            em.rateOverTime = currentHosePower;                         //set it to the new value
    }


    public void Die()
    {
        Debug.Log("You lose");
    }
}
