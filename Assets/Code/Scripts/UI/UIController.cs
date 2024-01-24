using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public enum UIState
{
    win,
    lose,
    weapon

}
public class UIController : MonoBehaviour
{
    [SerializeField] GameObject weaponText;
    [SerializeField] GameObject winText;
    [SerializeField] GameObject loseText;

    public void ActiveWinText()
    {
        winText.SetActive(true);
        weaponText.SetActive(false);
        loseText.SetActive(false);
    }

    public void ActiveLoseText()
    {
        winText.SetActive(false);
        weaponText.SetActive(false);
        loseText.SetActive(true);
    }

    public void ActiveWeaponText()
    {
        winText.SetActive(false);
        weaponText.SetActive(true);
        loseText.SetActive(false);
    }
}
