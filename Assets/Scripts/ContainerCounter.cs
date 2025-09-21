using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{

    public event EventHandler OnPlayerGrappingObject;
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpwanKitchenObject(kitchenObjectSO, player);

            OnPlayerGrappingObject?.Invoke(this, EventArgs.Empty);
        }
    }


   
}
