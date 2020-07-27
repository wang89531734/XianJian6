using System;

namespace SoftStar
{
	public interface IEditComponentHelper
	{
		string[] AvailableComponentNames
		{
			get;
		}

		void AddComponentByName(string componentName);

		void RemoveComponentByName(string componentName);
	}
}
