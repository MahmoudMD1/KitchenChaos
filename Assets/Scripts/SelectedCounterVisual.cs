using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjectArry;
    

    private void Start()
    {
        PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; 
        
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangeEventArgs e)
    {
        //Debug.Log(e.selectedCounter);///// not tregared
        //Debug.Log(clearCounter);

        if (e.selectedCounter == baseCounter)
        {
            Show();

        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        foreach (GameObject visualGameObject in visualGameObjectArry)
        {
            visualGameObject.SetActive(true);
        }
    }

    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArry)
        {
            visualGameObject.SetActive(false);
        }
    }

    
}
