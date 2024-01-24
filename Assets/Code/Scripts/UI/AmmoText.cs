using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoText : MonoBehaviour
{
    [SerializeField]private TMP_Text ammoInfo;
    private int maxAmmo;
    public void SetText(int currentAmmo, int maxAmmo)
    {
        this.maxAmmo = maxAmmo;
        ammoInfo.text = currentAmmo.ToString() + "/" + maxAmmo.ToString() + "Balas";
    }
    public void UpdateText(int currentAmmo)
    {
        ammoInfo.text = currentAmmo.ToString() + "/" + maxAmmo.ToString() + "Balas";
    }
}
