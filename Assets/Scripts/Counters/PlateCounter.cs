using System;
using UnityEngine;

public class PlateCounter : BaseCounter
{
    public event EventHandler OnPlateSpwan,OnPlateRemove;
    //[SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectSO platekitchenObjectSO;
    [SerializeField] private int plateSwapedNumber=0, maxPlateSwaped=4;
    [SerializeField] private float spwanPlateTimer, spwanPlateTimerMax = 4f;

    private void Update()
    {
        spwanPlateTimer += Time.deltaTime;
        if (spwanPlateTimer > spwanPlateTimerMax)
        {
            spwanPlateTimer = 0f;
            if (plateSwapedNumber < maxPlateSwaped)
            {
                plateSwapedNumber++;
                OnPlateSpwan?.Invoke(this, EventArgs.Empty);
            }
        }
    }
    public override void Interact(PlayerController player)
    {
        if (!player.HasKitchenObject()) {
            {
                plateSwapedNumber --;
                KitchenObject.SpwanKitchenObject(platekitchenObjectSO, player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);

            }
        }

    }
}
