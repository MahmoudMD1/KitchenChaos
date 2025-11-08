using System.Collections.Generic;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
    public PlateCounter plateCounter;
    [SerializeField] private Transform counterTopPoint ,plateVisualPrefab;
    private List<GameObject> plateSpwanObjects;
    private void Awake()
    {
        plateSpwanObjects = new List<GameObject>();
    }
    private void Start()
    {
        plateCounter.OnPlateSpwan += PlateCounter_OnPlateSpwan;
        plateCounter.OnPlateRemove += PlateCounter_OnPlateRemove;
    }

    private void PlateCounter_OnPlateRemove(object sender, System.EventArgs e)
    {
        GameObject plateGameObject = plateSpwanObjects[plateSpwanObjects.Count - 1];
        plateSpwanObjects.Remove(plateGameObject);
        Destroy(plateGameObject);
    }

    private void PlateCounter_OnPlateSpwan(object sender, System.EventArgs e)
    {
        Transform plateVisualTransform = Instantiate(plateVisualPrefab, counterTopPoint);
        float plateOffsetY = .1f;
        plateVisualTransform.localPosition = new Vector3(0, plateOffsetY*plateSpwanObjects.Count, 0);
        plateSpwanObjects.Add(plateVisualTransform.gameObject);
    }
   
}
