using System;
using CustomVP;
using UnityEngine;

namespace UpgradeScripts
{
    public class Show4WD : MonoBehaviour
    {
        public GameObject handPointer;
        private bool isShown;

        private CarController _carController;
        

        private void OnTriggerEnter(Collider other)
        {
            if(isShown) return;
            
            if (other.gameObject.layer.Equals(26))
            {
                _carController = VehicleLoader.Instance.playerCarController;
                isShown = true;
                CarUIControl.Instance.ShowMessage("Enabling 4WD will make it easy to go through Mud Areas!");
                handPointer.SetActive(true);
            }
                
        }

        private void Update()
        {
            if (isShown)
            {
                handPointer.SetActive(!(_carController.FWD && _carController.RWD));
                if (!handPointer.activeSelf)
                {
                    Destroy(gameObject);
                    Destroy(handPointer);
                }
            }
                
        }
    }
}