using UnityEngine;

public class PlateCounter : BaseCounter
{

    //[SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO platekitchenObjectSO;
    [SerializeField] private int plateNumber=0, maxPlate=5;

    private void Update()
    {
        if (plateNumber < maxPlate)
        {
            KitchenObject.SpwanKitchenObject(platekitchenObjectSO, this);
            plateNumber++;
        }
    }
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpwanKitchenObject(platekitchenObjectSO, this);
            plateNumber--;
        }
    }

    
}
