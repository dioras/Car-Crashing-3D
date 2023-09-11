using System;
using UnityEngine;

namespace DigitalRuby.PyroParticles
{
	public class SingleLineAttribute : PropertyAttribute
	{
		public SingleLineAttribute(string tooltip)
		{
			this.Tooltip = tooltip;
		}

		public string Tooltip { get; private set; }
	}
}
