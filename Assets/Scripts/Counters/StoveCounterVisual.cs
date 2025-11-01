using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private GameObject stoveOnGameObject ,  particalGameOnject;
    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChange;
    }

    private void StoveCounter_OnStateChange(object sender, StoveCounter.OnStateChnageEventArgs e)
    {

        bool showVisual = e.state == StoveCounter.State.Frying   || e.state == StoveCounter.State.Fried;
        stoveOnGameObject.SetActive(showVisual);
        particalGameOnject.SetActive(showVisual);

    }
}
