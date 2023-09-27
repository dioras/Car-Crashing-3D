using System;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

public class StoreListener : IStoreListener
{
	public void InitializeIAP()
	{
		ConfigurationBuilder configurationBuilder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(), new IPurchasingModule[0]);
		configurationBuilder.AddProduct("com.battlecreek.offroadoutlaws.100gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.battlecreek.offroadoutlaws.250gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.battlecreek.offroadoutlaws.500gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.battlecreek.offroadoutlaws.750gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.battlecreek.offroadoutlaws.4000gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.battlecreek.offroadoutlaws.10000gold", ProductType.Consumable);
		configurationBuilder.AddProduct("com.battlecreek.offroadoutlaws.monthlyvip", ProductType.Subscription);
		configurationBuilder.AddProduct("com.battlecreek.offroadoutlaws.timedvehiclepurchase", ProductType.Consumable);
		configurationBuilder.AddProduct("com.battlecreek.offroadoutlaws.premiumvehiclepurchase", ProductType.Consumable);
		UnityPurchasing.Initialize(this, configurationBuilder);
	}

	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
	{
		this.controller = controller;
		this.extensions = extensions;
	}

	public void OnInitializeFailed(InitializationFailureReason error)
	{
		UnityEngine.Debug.Log("Could not initialize UnityIAP");
	}

	public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
	{
		MenuManager.Instance.ShowMessage("Could not complete: " + p.ToString(), true);
	}

	public void RestoreIAP()
	{
	}

	public void PurchaseIAP(string product)
	{
		UnityEngine.Debug.Log("Purchasing: " + product);
		if (this.controller == null)
		{
			MenuManager.Instance.ShowMessage("Cannot purchase right now. Make sure you are online?", true);
			return;
		}
		this.controller.InitiatePurchase(product);
	}

	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
	{
		UnityEngine.Debug.Log("Processing purchase: " + purchaseEvent.purchasedProduct);
		if (purchaseEvent.purchasedProduct != null)
		{
			StatsData statsData = GameState.LoadStatsData();
			int num = 0;
			bool flag = false;
			bool flag2 = false;
			
			if (purchaseEvent.purchasedProduct.definition.id == "com.battlecreek.offroadoutlaws.monthlyvip" && statsData.IsMember)
			{
				statsData.IsMember = true;
				GameState.SaveStatsData(statsData);
				return PurchaseProcessingResult.Complete;
			}
			string id = purchaseEvent.purchasedProduct.definition.id;
			switch (id)
			{
			case "com.battlecreek.offroadoutlaws.100gold":
				num = 100;
				break;
			case "com.battlecreek.offroadoutlaws.250gold":
				num = 250;
				break;
			case "com.battlecreek.offroadoutlaws.500gold":
				num = 500;
				break;
			case "com.battlecreek.offroadoutlaws.750gold":
				num = 750;
				break;
			case "com.battlecreek.offroadoutlaws.4000gold":
				num = 4000;
				break;
			case "com.battlecreek.offroadoutlaws.10000gold":
				num = 10000;
				break;
			case "com.battlecreek.offroadoutlaws.monthlyvip":
				num = 200;
				flag = true;
				break;
			case "com.battlecreek.offroadoutlaws.timedvehiclepurchase":
				flag2 = true;
				break;
			case "com.battlecreek.offroadoutlaws.premiumvehiclepurchase":
				flag2 = true;
				break;
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
				UnityEngine.Debug.Log(MenuManager.Instance.LoadedVehiclesInGarage.Count);
				UnityEngine.Debug.Log(MenuManager.Instance.SelectedVehicleInGarageID);
				MenuManager.Instance.LoadMenu(MenuState.MainMenu, true, false);
			}
			MenuManager.Instance.UpdateScreen();
		}
		else
		{
			UnityEngine.Debug.Log("Product was null!");
		}
		return PurchaseProcessingResult.Complete;
	}

	private IStoreController controller;

	private IExtensionProvider extensions;
}
