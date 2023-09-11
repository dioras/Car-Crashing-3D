using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Facebook.Unity.Example
{
	internal abstract class MenuBase : ConsoleBase
	{
		protected abstract void GetGui();

		protected virtual bool ShowDialogModeSelector()
		{
			return false;
		}

		protected virtual bool ShowBackButton()
		{
			return true;
		}

		protected void HandleResult(IResult result)
		{
			if (result == null)
			{
				base.LastResponse = "Null Response\n";
				LogView.AddLog(base.LastResponse);
				return;
			}
			base.LastResponseTexture = null;
			if (!string.IsNullOrEmpty(result.Error))
			{
				base.Status = "Error - Check log for details";
				base.LastResponse = "Error Response:\n" + result.Error;
			}
			else if (result.Cancelled)
			{
				base.Status = "Cancelled - Check log for details";
				base.LastResponse = "Cancelled Response:\n" + result.RawResult;
			}
			else if (!string.IsNullOrEmpty(result.RawResult))
			{
				base.Status = "Success - Check log for details";
				base.LastResponse = "Success Response:\n" + result.RawResult;
			}
			else
			{
				base.LastResponse = "Empty Response\n";
			}
			LogView.AddLog(result.ToString());
		}

		protected void OnGUI()
		{
			if (base.IsHorizontalLayout())
			{
				GUILayout.BeginHorizontal(new GUILayoutOption[0]);
				GUILayout.BeginVertical(new GUILayoutOption[0]);
			}
			GUILayout.Label(base.GetType().Name, base.LabelStyle, new GUILayoutOption[0]);
			this.AddStatus();
			if (UnityEngine.Input.touchCount > 0 && UnityEngine.Input.GetTouch(0).phase == TouchPhase.Moved)
			{
				Vector2 scrollPosition = base.ScrollPosition;
				scrollPosition.y += UnityEngine.Input.GetTouch(0).deltaPosition.y;
				base.ScrollPosition = scrollPosition;
			}
			base.ScrollPosition = GUILayout.BeginScrollView(base.ScrollPosition, new GUILayoutOption[]
			{
				GUILayout.MinWidth((float)ConsoleBase.MainWindowFullWidth)
			});
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			if (this.ShowBackButton())
			{
				this.AddBackButton();
			}
			this.AddLogButton();
			if (this.ShowBackButton())
			{
				GUILayout.Label(GUIContent.none, new GUILayoutOption[]
				{
					GUILayout.MinWidth((float)ConsoleBase.MarginFix)
				});
			}
			GUILayout.EndHorizontal();
			if (this.ShowDialogModeSelector())
			{
				this.AddDialogModeButtons();
			}
			GUILayout.BeginVertical(new GUILayoutOption[0]);
			this.GetGui();
			GUILayout.Space(10f);
			GUILayout.EndVertical();
			GUILayout.EndScrollView();
		}

		private void AddStatus()
		{
			GUILayout.Space(5f);
			GUILayout.Box("Status: " + base.Status, base.TextStyle, new GUILayoutOption[]
			{
				GUILayout.MinWidth((float)ConsoleBase.MainWindowWidth)
			});
		}

		private void AddBackButton()
		{
			GUI.enabled = ConsoleBase.MenuStack.Any<string>();
			if (base.Button("Back"))
			{
				base.GoBack();
			}
			GUI.enabled = true;
		}

		private void AddLogButton()
		{
			if (base.Button("Log"))
			{
				base.SwitchMenu(typeof(LogView));
			}
		}

		private void AddDialogModeButtons()
		{
			GUILayout.BeginHorizontal(new GUILayoutOption[0]);
			IEnumerator enumerator = Enum.GetValues(typeof(ShareDialogMode)).GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					this.AddDialogModeButton((ShareDialogMode)obj);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
			GUILayout.EndHorizontal();
		}

		private void AddDialogModeButton(ShareDialogMode mode)
		{
			bool enabled = GUI.enabled;
			GUI.enabled = (enabled && mode != MenuBase.shareDialogMode);
			if (base.Button(mode.ToString()))
			{
				MenuBase.shareDialogMode = mode;
				FB.Mobile.ShareDialogMode = mode;
			}
			GUI.enabled = enabled;
		}

		private static ShareDialogMode shareDialogMode;
	}
}
