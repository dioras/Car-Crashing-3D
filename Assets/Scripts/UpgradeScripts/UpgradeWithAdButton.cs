using System;
using CustomVP;
using UnityEngine;
using UnityEngine.UI;

namespace UpgradeScripts
{
    public class UpgradeWithAdButton : MonoBehaviour
    {
        public GameObject upgradeButton;
        public GameObject adUpgradeButton;
        private Button _button;
        private float lastTime;
        private float timeInterval = 1;
        private MenuManager _menuManager;
        public int amount = 0;

        private void Start()
        {
            _button = adUpgradeButton.GetComponent<Button>();
            _button.onClick.AddListener(RequestRewarded);
            _menuManager = MenuManager.Instance;
        }

        private void Update()
        {
	        //return;
            if (Time.time - lastTime > timeInterval)
            {
	            amount = 0;
	            bool flag2 = false;
	            if (_menuManager.menuState.Equals(MenuState.Power))
	            {
	                PowerPart part = PowerParts.GetPart(_menuManager.CurrentVehicle.vehicleType, _menuManager.SelectedSubPowerPartType, _menuManager.CurrentPowerPartStage + 1);

	                if (part == null)
	                {
	                    upgradeButton.SetActive(true);
	                    adUpgradeButton.SetActive(false);
	                    return;
	                }
	                
		            amount = part.partCost;
		            flag2 = CheckSpecialParts();
	            }
	            else if (_menuManager.menuState == MenuState.TuneWheels)
	            {
		            	int stage = _menuManager.SelectedWheelsControls.Stage + 1;

			            if (Suspensions.GetWheelsUpgrade(stage) == null)
			            {
				            upgradeButton.SetActive(true); 
				            adUpgradeButton.SetActive(false); 
				            return;
			            }
			            
						amount = Suspensions.GetWheelsUpgrade(stage).upgradeCost;
	            }
	            else if(_menuManager.menuState == MenuState.TuneSuspension)
	            {
		            int stage = _menuManager.SelectedSuspension.UpgradeStage + 1;

		            if (Suspensions.GetSuspensionUpgrade(_menuManager.SelectedSuspension.gameObject.name, stage) == null)
		            { 
			            upgradeButton.SetActive(true); 
			            adUpgradeButton.SetActive(false); 
			            return;
		            }
					
					amount = Suspensions.GetSuspensionUpgrade(_menuManager.SelectedSuspension.gameObject.name, stage).upgradeCost;
					
	            }
	            else if (_menuManager.menuState == MenuState.SwitchSuspension)
	            {
		            SuspensionPart suspension = Suspensions.GetSuspension(_menuManager.CurrentVehicle.vehicleType, _menuManager.SelectedSuspension.gameObject.name);
		            
		             if (suspension == null)
					 { 
						 upgradeButton.SetActive(true);
						 adUpgradeButton.SetActive(false);
						 return;
					 }
		             
		             amount = (!_menuManager.CurrentVehicle.PurchasedPartsList.Contains(_menuManager.SelectedSuspension.gameObject.name)) ? suspension.partCost : 0;
	            }
	            
                StatsData statsData = GameState.LoadStatsData();
                bool flag;
                flag = (statsData.Money >= amount) || flag2;

                upgradeButton.SetActive(flag);
                adUpgradeButton.SetActive(!flag);
                _button.interactable = Advertisements.Instance.IsRewardVideoAvailable();
                
                lastTime = Time.time;
            }
        }

        public bool CheckSpecialParts()
        {
	        var menu = _menuManager;
	        bool flag = false;
	        var _type = menu.SelectedSubPowerPartType;

	        switch (_type)
	        {
		        case PowerPartType.Diesel:
			        if (menu.CurrentCarController.BlowerStage > 0)
				        flag = true;

			        if (menu.CurrentCarController.DieselPurchased)
				        flag = true;
			        break;

		        case PowerPartType.Gearbox:
			        if (menu.CurrentCarController.ManualTransmissionPurchased)
				        flag = true;
			        break;
		        
		        case PowerPartType.TankTracks:
			        if (menu.CurrentCarController.TankTracksPurchased)
				        flag = true;
			        break;
		        case PowerPartType.Blower:
			        if (menu.CurrentCarController.DieselStage == 4 || menu.CurrentCarController.TurboStage > 0)
				        flag = true;
			        if (menu.CurrentCarController.PurchasedBlowerStage == 4)
				        flag = true;
			        break;
		        case PowerPartType.Turbo:
			        if (menu.CurrentCarController.BlowerStage > 0 && menu.CurrentCarController.PurchasedBlowerStage > 0)
				        flag = true;
			        if (menu.CurrentCarController.PurchasedTurboStage == 4)
				        flag = true;
			        break;
				default:  flag = false;
					break;
	        }

	        return flag;
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }

        private void RequestRewarded()
        {
            Advertisements.Instance.ShowRewardedVideo(RewardedCompleteMethod);
        }

        private void RewardedCompleteMethod(bool isCompleted)
        {
	        if (isCompleted) return;
	        
	        switch (_menuManager.menuState)
	        {
		        case MenuState.Power:
			        _menuManager.UpgradePowerAds();
			        break;
		        case MenuState.SwitchSuspension:
			        _menuManager.InstallSuspensionAds();
			        break;
		        case MenuState.TuneSuspension:
			        _menuManager.UpgradeSuspensionAds();
			        break;
		        case MenuState.TuneWheels:
			        _menuManager.UpgradeWheelsAds();
			        break;
	        }
        }
    }
}