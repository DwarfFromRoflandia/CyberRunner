using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndExitStore : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject store;
    [SerializeField] private Coin _coin;

    public void OpenShop()
    {
        EventManager.ButtonClicked.Invoke();// �������� ���� ������� 
        mainMenu.SetActive(false);
        store.SetActive(true);

        Debug.Log("TransferQuantityCoin.transferQuantityCoin: " + TransferQuantityCoin.transferQuantityCoin);
        Debug.Log("Coin: " + _coin.Coins);
    }

    public virtual void ExitMenu() // ������ ��������� ��� ��������������� � ������ ButtonController.
    {
        EventManager.ButtonClicked.Invoke();// �������� ���� ������� 
        mainMenu.SetActive(true);
        store.SetActive(false);
    }

    
}
