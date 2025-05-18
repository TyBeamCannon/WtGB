using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;

using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
            return;
        ShopManager.instance.OpenShopUI();
    }

    void OnTriggerExit(Collider other)
    {
        ShopManager.instance.CloseShopUI();    
    }
}
