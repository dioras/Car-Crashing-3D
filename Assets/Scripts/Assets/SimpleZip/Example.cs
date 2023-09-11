using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.SimpleZip
{
	public class Example : MonoBehaviour
	{
		public void Start()
		{
			try
			{
				string text = "El perro de San Roque no tiene rabo porque Ramón Rodríguez se lo ha robado.";
				text = string.Concat(new string[]
				{
					text,
					text,
					text,
					text,
					text
				});
				string text2 = Zip.CompressToString(text);
				string arg = Zip.Decompress(text2);
				this.Text.text = string.Format("Plain text: {0}\n\nCompressed: {1}\n\nDecompressed: {2}", text, text2, arg);
			}
			catch (Exception ex)
			{
				this.Text.text = ex.ToString();
			}
		}

		public Text Text;
	}
}
