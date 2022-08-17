using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndExitStore : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject store;

    public void OpenShop()
    {
        EventManager.ButtonClicked.Invoke();// вызываем звук нажати€ 
        mainMenu.SetActive(false);
        store.SetActive(true);

        Debug.Log("TransferQuantityCoin.transferQuantityCoin: " + TransferQuantityCoin.transferQuantityCoin);
    }

    public virtual void ExitMenu() // сделал доступным дл€ переопределени€ в классе ButtonController.
    {
        EventManager.ButtonClicked.Invoke();// вызываем звук нажати€ 
        mainMenu.SetActive(true);
        store.SetActive(false);
    }

    
}
