using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recoil : MonoBehaviour
{
    public float recoil = 0.0f;
    public float maxRecoil_x = -20f;
    public float maxRecoil_y = 20f;
    public float recoilSpeedUp = 2f;
    public bool _is_recoiling;
    public float recoilSpeedDown = 1f;
    public int recoil_dir = 1;
    public float temp_recoil_x;
    public void StartRecoil(float recoilParam, float maxRecoil_xParam, float recoilSpeedParam)
    {
        // in seconds
        recoil = recoilParam;
        maxRecoil_x = maxRecoil_xParam;
        recoilSpeedUp = recoilSpeedParam;
        maxRecoil_y = Random.Range(-maxRecoil_xParam, maxRecoil_xParam);
    }

    void recoiling()
    {
        if (recoil > 0f)
        {
            if(recoil_dir == 1)
            {
                temp_recoil_x = maxRecoil_x;
            }
            else
            {
                temp_recoil_x = Mathf.Abs(maxRecoil_x);
            }
            //Quaternion maxRecoil = Quaternion.Euler(-maxRecoil_y, 0f, maxRecoil_x);
            Quaternion maxRecoil = Quaternion.Euler(-maxRecoil_y, 0f, temp_recoil_x);
            // Dampen towards the target rotation
            transform.localRotation = Quaternion.Slerp(transform.localRotation, maxRecoil, Time.deltaTime * recoilSpeedUp);
            _is_recoiling = true;
            recoil -= Time.deltaTime;
        }
        else
        {
            _is_recoiling = false;
            recoil = 0f;
            // Dampen towards the target rotation
            //transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeedDown / 2);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.identity, Time.deltaTime * recoilSpeedDown);
        }
     }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.G))
        {
            recoil += 0.015f;
        }
        if (Input.GetKeyUp(KeyCode.G))
        {
            if (recoil_dir == 1)
            {
                recoil_dir = 2;
            }
            else
            {
                recoil_dir = 1;
            }
        }
        recoiling();
    }
}
