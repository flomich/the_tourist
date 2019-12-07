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

    private movePlayer move_script;
    private HealthScript health_script;

    private void Start()
    {
        move_script = GetComponent<movePlayer>();
        health_script = GetComponent<HealthScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Alpha1) && doener_count > 0)
        {
            //consume doener (increase damager)
            doener_count--;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && puntigamer_count > 0)
        {
            puntigamer_count--;
            move_script.activateBoost(5.0f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && frankfurter_count > 0 && !health_script.hasFullHealth())
        {
            frankfurter_count--;
            health_script.addHealth(health_script.getMaxHealth() * 0.25f);
        }
    }

    public void addDoenerCount(uint count)
    {
        Mathf.Min(doener_count + count, max_doener_count);
    }

    public void addPuntigamerCount(uint count)
    {
        Mathf.Min(puntigamer_count + count, max_puntigamer_count);
    }

    public void addFrankfurterCount(uint count)
    {
        Mathf.Min(frankfurter_count + count, max_frankfurter_count);
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
}
