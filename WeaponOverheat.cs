using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponOverheat : MonoBehaviour
{
    public float curr_weapon_temp;
    public float max_weapon_temp;
    public float _over_heat_rate;
    public float _cool_down_rate;
    public bool _shooting;
    public bool _over_heated;
    public bool _can_shoot;
    public bool _can_animate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Overheat();
        if (Input.GetKeyDown(KeyCode.J))
        {
            _shooting = true;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            _shooting = false;
        }
    }
    public void LateUpdate()
    {
        if(_over_heated == true)
        {
            StartCoroutine(waiter());
        }
    }
    public void Overheat()
    {
        if (_shooting == true && _over_heated == false)
        {
            if(curr_weapon_temp <= max_weapon_temp)
            {
                if(_can_shoot == true)
                {
                    curr_weapon_temp += _over_heat_rate * Time.deltaTime;
                }           
            }
            else
            {
                //Debug.Break();
                //StartCoroutine(waiter());
                _can_shoot = false;
                _over_heated = true;
            }
        }
        else
        {
            if(curr_weapon_temp < max_weapon_temp)
            {
                if(curr_weapon_temp > 0)
                {
                    curr_weapon_temp -= _cool_down_rate * Time.deltaTime;
                }
                else
                {
                    curr_weapon_temp = 0.0f;
                    if (_over_heated)
                    {
                        //StartCoroutine(waiter());
                        _over_heated = true;
                    }
                    _can_shoot = true;
                    _over_heated = false;
                }
            }
            else
            {
                if(curr_weapon_temp >= max_weapon_temp)
                {
                    curr_weapon_temp -= _cool_down_rate * Time.deltaTime;
                }
                else
                {
                    curr_weapon_temp = 0.0f;
                    //StartCoroutine(waiter());
                    _can_shoot = true;
                    _over_heated = false;
                }
            }
        }
    }
    
    public IEnumerator waiter()
    {
        _can_animate = true;
        yield return new WaitForSeconds(10f);
        _can_shoot = true;
        _over_heated = false;
        _can_animate = false;
    }
}
