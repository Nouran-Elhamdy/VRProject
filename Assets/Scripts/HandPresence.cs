using System.Collections;
using System.Collections.Generic;
using UnityEngine.XR;
using UnityEngine;
public class HandPresence : MonoBehaviour
{
    [SerializeField] InputDeviceCharacteristics deviceCharacteristics;
    [SerializeField] List<GameObject> controllerPrefabs;
    [SerializeField] GameObject handModelPrefab;
    [SerializeField] bool showController = false;
    InputDevice targetDevice;

    GameObject spawnedController;
    GameObject spawnedHandPrefab;
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(deviceCharacteristics, devices);
        foreach (var item in devices)
        {
           Debug.Log(item.name + " " + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name); 

            if(prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("coudn't find a corrosponding model.");
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandPrefab = Instantiate(handModelPrefab, transform);
        }
    }
    void GetHardwareInputs()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primarayButtonValue) && primarayButtonValue)
        {
            Debug.Log("PrimaryButton Is Pressed + " + CommonUsages.primaryButton + " " + primarayButtonValue);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            Debug.Log("Trigger Is Pressed + " + CommonUsages.trigger + " " + triggerValue);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            Debug.Log("primary2DAxis Is Pressed + " + CommonUsages.primary2DAxis + " " + primary2DAxisValue);
        }
    }
    void Update()
    {
        GetHardwareInputs();

        if(showController)
        {
            spawnedHandPrefab.SetActive(false);
            spawnedController.SetActive(true);
        }
        else
        {
            spawnedHandPrefab.SetActive(true);
            spawnedController.SetActive(false);
        }
    }
}
