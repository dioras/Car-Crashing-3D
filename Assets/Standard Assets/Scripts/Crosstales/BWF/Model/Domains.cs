using System;
using System.Collections.Generic;
using System.Text;
using Crosstales.BWF.Util;

namespace Crosstales.BWF.Model
{
	[Serializable]
	public class Domains
	{
		public Domains(Source source, List<string> domainList)
		{
			this.Source = source;
			this.DomainList = domainList;
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.GetType().Name);
			stringBuilder.Append(Constants.TEXT_TOSTRING_START);
			stringBuilder.Append("Source='");
			stringBuilder.Append(this.Source);
			stringBuilder.Append(Constants.TEXT_TOSTRING_DELIMITER);
			stringBuilder.Append("DomainList='");
			stringBuilder.Append(this.DomainList);
			stringBuilder.Append(Constants.TEXT_TOSTRING_DELIMITER_END);
			stringBuilder.Append(Constants.TEXT_TOSTRING_END);
			return stringBuilder.ToString();
		}

		public Source Source;

		public List<string> DomainList;
	}
}
