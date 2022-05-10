using UnityEngine;

public class MedKitActivator : MonoBehaviour
{
    [SerializeField] private GameObject _medKitStation;

    private void OnDisable()
    {
        DeactivateStation();
    }

    public void ActivateStation()
    {
        _medKitStation.SetActive(true);
    }

    public void DeactivateStation()
    {
        _medKitStation.SetActive(false);
    }
}