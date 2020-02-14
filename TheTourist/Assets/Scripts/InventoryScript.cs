using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public uint doener_count = 1;
    public uint puntigamer_count = 1;
    public uint frankfurter_count = 1;

    public uint max_doener_count = 10;
    public uint max_puntigamer_count = 10;
    public uint max_frankfurter_count = 10;

    public float effect_duration = 8.0f;

    private movePlayer move_script;
    private HealthScript health_script;
    private CombatScript combat_script;

    private bool consume_doener;
    private bool consume_frankfurter;
    private bool consume_puntigamer;

    private void Start()
    {
        move_script = GetComponent<movePlayer>();
        health_script = GetComponent<HealthScript>();
        combat_script = GetComponent<CombatScript>();

        consume_doener = false;
        consume_frankfurter = false;
        consume_puntigamer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (consume_doener && doener_count > 0)
        {
            //consume doener (increase damage)
            SoundEffectScript.Instance.playConsumeDoener(gameObject.transform.position);
            ParticleEffectsScript.Instance.damageBoostEffect(gameObject.transform.position, gameObject, effect_duration);

            doener_count--;
            combat_script.activateDoubleDamage(effect_duration);
            consume_doener = false;
        }

        if (consume_puntigamer && puntigamer_count > 0)
        {
            // increase speed
            SoundEffectScript.Instance.playConsumePuntigamer(gameObject.transform.position);
            ParticleEffectsScript.Instance.speedBoostEffect(gameObject.transform.position, gameObject, effect_duration);

            puntigamer_count--;
            move_script.activateBoost(effect_duration);
            combat_script.activateDoubleSpeed(effect_duration);
            consume_puntigamer = false;
        }

        if (consume_frankfurter && frankfurter_count > 0 && !health_script.hasFullHealth())
        {
            // increase health

            SoundEffectScript.Instance.playConsumeFrankfurter(gameObject.transform.position);
            ParticleEffectsScript.Instance.healthBoostEffect(gameObject.transform.position, gameObject);

            frankfurter_count--;
            health_script.addHealth(health_script.getMaxHealth() * 0.25f);
            consume_frankfurter = false;
        }
    }

    public void addDoenerCount(uint count)
    {
        if(doener_count + count <= max_doener_count)
        {
            doener_count += count;
        }
    }

    public void addPuntigamerCount(uint count)
    {
        if (puntigamer_count + count <= max_puntigamer_count)
        {
            puntigamer_count += count;
        }
    }

    public void addFrankfurterCount(uint count)
    {
        if (frankfurter_count + count <= max_frankfurter_count)
        {
            frankfurter_count += count;
        }
    }

    public uint getDoenerCount()
    {
        return doener_count;
    }

    public uint getPuntigamerCount()
    {
        return puntigamer_count;
    }

    public uint getFrankfurterCount()
    {
        return frankfurter_count;
    }

    public uint getMaxDoenerCount()
    {
        return max_doener_count;
    }

    public uint getMaxPuntigamerCount()
    {
        return max_puntigamer_count;
    }

    public uint getMaxFrankfurterCount()
    {
        return max_frankfurter_count;
    }

    public void setConsumePuntigamer(bool state)
    {
        consume_puntigamer = state;
    }

    public void setConsumeDoener(bool state)
    {
        consume_doener = state;
    }
    public void setConsumeFrankfurter(bool state)
    {
        consume_frankfurter = state;
    }
}
