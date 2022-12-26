using UnityEngine;
using UnityEngine.InputSystem;

public class DeviceDetector : MonoBehaviour
{
    [SerializeField] private GameObject keyboardKey;
    [SerializeField] private GameObject controllerKey;

    private string deviceName => Movement.Instance.deviceName;

    private void Update()
    {

        if (deviceName.Equals("Keyboard"))
        {
            if (keyboardKey.activeSelf && !controllerKey.activeSelf)
                return;

            controllerKey.SetActive(false);
            keyboardKey.SetActive(true);
        }
        else if (deviceName.Equals("Xbox Controller"))
        {
            if (controllerKey.activeSelf && !keyboardKey.activeSelf)
                return;

            keyboardKey.SetActive(false);
            controllerKey.SetActive(true);
        }
    }
}
