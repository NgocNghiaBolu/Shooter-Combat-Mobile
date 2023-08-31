using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AmmoCount : MonoBehaviour
{
    public Text ammoText;
    public Text magText;

    public static AmmoCount occurrence;

    public void Awake()
    {
        occurrence = this;
    }

    public void UpdateAmmoText(int presentAmmo)
    {
        ammoText.text = "Ammo: " + presentAmmo;
    }

    public void UpdateMagText(int Mag)
    {
        magText.text = "Mag:   " + Mag;
    }

}
