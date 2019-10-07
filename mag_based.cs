using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SubClass
{
    public int mag_id;
    public int rounds_in_mag;
    public bool _mag_empty = false;
}
public class mag_based : MonoBehaviour
{
    public int curr_mag;
    public int curr_ammo_in_mag;
    [SerializeField]
    private SubClass[] myArray = new SubClass[3];//Statically defines how many magazines the player has
    //private SubClass[] myArray; //Can Define In The Editor How Many Magazines You Would Like The Player To Have
    public int mag_number; //Which Magazine the Player is currently shooting from
    public bool _mag_change; //Used to check if the player is changing magazines
    public bool _can_shoot; //Used to tell if the player is able to shoot or not
    public bool _out_of_ammo; //Used to tell if the player is out of ammo
    public float _reload_time = 1.0f; //Used To Define how long it takes for the player to reload magazines
    // Start is called before the first frame update
    void Start()
    {
        curr_ammo_in_mag = myArray[curr_mag].rounds_in_mag;
        mag_number = myArray.Length;
    }

    // Update is called once per frame
    void Update()
    {
        Magazine();
    }
    //This is The Function that Handles All Of The Shooting
    public void Magazine()
    {
        if (myArray[curr_mag].rounds_in_mag == 0)
        {
            if (curr_mag < (mag_number - 1))
            {
                curr_mag++;
            }
            else
            {
                curr_mag = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Replenish_Ammo();
        }
        //myArray[1].rounds_in_mag = 28;
        if (Input.GetKeyDown(KeyCode.P) && _can_shoot == true && _mag_change == false && _out_of_ammo == false)
        {
            if (curr_mag < (mag_number - 1))
            {
                if (myArray[curr_mag].rounds_in_mag == 0)
                {
                    //Debug.Log("Mag out Of Ammo -1");
                    Debug.Log("Magazine No Longer Has Any Ammo -1");
                }
                else
                {
                    if (myArray[curr_mag].rounds_in_mag >= 1)
                    {
                        Debug.Log("Reloaded New Mag With Ammo -2");
                    }
                    else
                    {
                        Debug.Log("Mag out Of Ammo -2");
                    }
                }
                curr_mag++;
            }
            else
            {
                curr_mag = 0;
            }
        }
        if (curr_mag < (mag_number - 1))
        {
            if ((Input.GetKeyDown(KeyCode.H) && _can_shoot == true && _out_of_ammo == false) && _mag_change == false)
            {


                if (myArray[curr_mag].rounds_in_mag > 1)
                {
                    //Debug.Log("Boom");
                    myArray[curr_mag].rounds_in_mag--;
                    curr_ammo_in_mag = myArray[curr_mag].rounds_in_mag;
                }
                else if (myArray[curr_mag].rounds_in_mag == 1)
                {
                    //Debug.Log("Boom");
                    myArray[curr_mag].rounds_in_mag = 0;
                    curr_ammo_in_mag = myArray[curr_mag].rounds_in_mag;
                    curr_mag++;
                    myArray[curr_mag - 1]._mag_empty = true;
                    curr_ammo_in_mag = myArray[curr_mag].rounds_in_mag;
                    StartCoroutine(_magChange());
                }
                else
                {
                    StartCoroutine(_magChange());
                    curr_mag++;
                    myArray[curr_mag - 1]._mag_empty = true;
                    curr_ammo_in_mag = myArray[curr_mag].rounds_in_mag;
                    //Debug.Log("Out of Ammo -3");
                    Debug.Log("Magazine No Longer Has Any Ammo -2");
                    Detect_Empty_Mags();
                }
            }
        }
        else
        {
            if ((Input.GetKeyDown(KeyCode.H) && _can_shoot == true && _out_of_ammo == false) && _mag_change == false)
            {
                if (myArray[curr_mag].rounds_in_mag > 1)
                {
                    //Debug.Log("Boom");
                    myArray[curr_mag].rounds_in_mag--;
                }
                else if (myArray[curr_mag].rounds_in_mag == 1)
                {
                    //Debug.Log("Boom");
                    myArray[curr_mag].rounds_in_mag = 0;
                    curr_ammo_in_mag = myArray[curr_mag].rounds_in_mag;
                    myArray[curr_mag]._mag_empty = true;
                    curr_ammo_in_mag = myArray[curr_mag].rounds_in_mag;
                    StartCoroutine(_magChange());
                }
                else
                {
                    StartCoroutine(_magChange());
                    //Debug.Log("Out of Ammo -4");
                    Debug.Log("Magazine No Longer Has Any Ammo -3");
                    _can_shoot = false;
                    myArray[curr_mag]._mag_empty = true;
                    Detect_Empty_Mags();
                }
                if (curr_ammo_in_mag > 1)
                {
                    curr_ammo_in_mag = myArray[curr_mag].rounds_in_mag;
                }
            }
        }
    }
    //THis is used to do the mag change, and call any functions such as a reload animation
    public IEnumerator _magChange()
    {
        Detect_Empty_Mags();
        _can_shoot = false;
        _mag_change = true;
        yield return new WaitForSeconds(_reload_time);
        _can_shoot = true;
        _mag_change = false;
        Empty_Mag_Check();
    }
    //Detect if all magazines are empty
    public void Detect_Empty_Mags()
    {
        Empty_Mag_Check();
    }
    //A Boolean to check if the player has any ammo left
    private bool Empty_Mag_Check()
    {
        for (int i = 0; i < myArray.Length; ++i)
        {
            if (myArray[i]._mag_empty == false)
            {
                return false;
            }
        }
        curr_mag = 0;
        _out_of_ammo = true;
        _can_shoot = false;
        return true;
    }
    /*************************************************************************
    A Function to replenish the players ammo supply
    This Can Be put in another script for a trigger or something similar
    Can Be Put On a Timer So It Takes Time To Reload All Magazines
    *************************************************************************/
    public void Replenish_Ammo()
    {
        for (int i = 0; i < myArray.Length; ++i)
        {
            if (myArray[i]._mag_empty == true)
            {
                myArray[i]._mag_empty = false;
                myArray[i].rounds_in_mag = 5;
                curr_mag = 0;
                _out_of_ammo = false;
                _can_shoot = true;
            }
        }
    }
}