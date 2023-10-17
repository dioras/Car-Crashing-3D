using System;
using System.Drawing.Printing;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class StoreListener : IStoreListener
{
	public void InitializeIAP()
	{
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.100gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.250gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.500gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.750gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.4000gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.10000gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.monthly", ProductType.Subscription);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.unlimitedfuel", ProductType.NonConsumable);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.timedvehiclepurchase", ProductType.Consumable);
		configurationBuilder.AddProduct("com.carcrash.carcrashinggames.premiumvehiclepurchase", ProductType.Consumable);
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		this.controller = controller;
		this.extensions = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		Debug.Log("Could not initialize UnityIAP");
	}

	public void OnInitializeFailed(InitializationFailureReason error, string s)
	{
		Debug.Log("Could not initialize UnityIAP");
	}

	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
		if(MenuManager.Instance != null)
			MenuManager.Instance.ShowMessage("Could not complete: " + p.ToString(), true);
		else
			CarUIControl.Instance.ShowMessage("Could not complete: " + p.ToString());
	}

	public void ShowOnlineMessage()
	{
		if(MenuManager.Instance != null){
			MenuManager.Instance.ShowMessage("Cannot purchase right now. Make sure you are online?", true);
		}
		else{
			CarUIControl.Instance.ShowMessage("Cannot purchase right now. Make sure you are online?");
		}
	}

	public void RestoreIAP()
	{
		
	}

	public void PurchaseIAP(string product)
	{
		Debug.Log("Purchasing: " + product);
		if (this.controller == null)
		{
			ShowOnlineMessage();
			return;
		}
		this.controller.InitiatePurchase(product);
	}

	public void PurchaseFromMap(string product)
	{
		Debug.Log("Purchasing: " + product);
		if (this.controller == null)
		{
			CarUIControl.Instance.ShowMessage("Cannot purchase right now. Make sure you are online?");
			return;
		}
		this.controller.InitiatePurchase(product);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
	{
		Debug.Log("Processing purchase: " + purchaseEvent.purchasedProduct.ToString());
		if (purchaseEvent.purchasedProduct != null)
		{
			StatsData statsData = GameState.LoadStatsData();
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			
			if (purchaseEvent.purchasedProduct.definition.id == "com.carcrash.carcrashinggames.monthlyvip" && statsData.IsMember)
			{
				statsData.IsMember = true;
				GameState.SaveStatsData(statsData);
				return PurchaseProcessingResult.Complete;
			}
			else if(purchaseEvent.purchasedProduct.definition.id == "com.carcrash.carcrashinggames.unlimitedfuel" && statsData.HasUnlimitedFuel)
			{
				statsData.HasUnlimitedFuel = true;
				GameState.SaveStatsData(statsData);
				CarUIControl.Instance.SetUnlimitedFuel(true);
				return PurchaseProcessingResult.Complete;
			}
			string id = purchaseEvent.purchasedProduct.definition.id;
			switch (id)
			{
			case "com.carcrash.carcrashinggames.100gold":
				num = 100;
				break;
			case "com.carcrash.carcrashinggames.250gold":
				num = 250;
				break;
			case "com.carcrash.carcrashinggames.500gold":
				num = 500;
				break;
			case "com.carcrash.carcrashinggames.750gold":
				num = 750;
				break;
			case "com.carcrash.carcrashinggames.4000gold":
				num = 4000;
				break;
			case "com.carcrash.carcrashinggames.10000gold":
				num = 10000;
				break;
			case "com.carcrash.carcrashinggames.monthly":
				num = 200;
				flag = true;
				break;
			case "com.carcrash.carcrashinggames.unlimitedfuel":
				flag3 = true;
				break;
			case "com.carcrash.carcrashinggames.timedvehiclepurchase":
				flag2 = true;
				break;
			case "com.carcrash.carcrashinggames.premiumvehiclepurchase":
				flag2 = true;
				break;
			}

			if(!statsData.HasUnlimitedFuel && flag3)
			{
				GameState.SetUnlimitedFuel(true);
				CarUIControl.Instance.SetUnlimitedFuel(true);
				return PurchaseProcessingResult.Complete;
			}

			string text = "Thanks! You now have " + num + " more gold";
			if (flag)
			{
				text += " and are a member";
			}
			text += "!";
			if (!flag2)
			{
				MenuManager.Instance.ShowMessage(text, true);
			}
			else
			{
				MenuManager.Instance.HideMessage();
				MenuManager.Instance.StopStoreCallbackTimer();
				MenuManager.Instance.BuyVehicle(Currency.Cash, true);
			}
			GameState.AddCurrency(num, Currency.Gold);
			if (!statsData.IsMember && flag)
			{
				GameState.SetMembership(true);
				MenuManager.Instance.HideBecomeMember();
				Debug.Log(MenuManager.Instance.LoadedVehiclesInGarage.Count);
				Debug.Log(MenuManager.Instance.SelectedVehicleInGarageID);
				MenuManager.Instance.LoadMenu(MenuState.MainMenu, true, false);
			}
			MenuManager.Instance.UpdateScreen();
		}
		else
		{
			Debug.Log("Product was null!");
		}
		return PurchaseProcessingResult.Complete;
	}

	private IStoreController controller;

	private IExtensionProvider extensions;
}
