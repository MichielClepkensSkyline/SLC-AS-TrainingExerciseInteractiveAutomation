namespace Automation_1
{
	using System;
	using System.Collections.Generic;
	using System.Configuration;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using Skyline.DataMiner.Core.DataMinerSystem.Common;
	using Skyline.DataMiner.Net.ReportsAndDashboards;

	public class ElementSelectorModel : IElementSelector
	{
		private readonly IDms dms;
		private IDmsElement[] elements;
		private IDmsElement selectedElement;

		private int selectedParameterId;

		public ElementSelectorModel(IDms dms)
		{
			if (dms == null)
			{
				throw new ArgumentNullException(nameof(dms));
			}

			this.dms = dms;
		}

		public IReadOnlyCollection<IDmsElement> Elements
		{
			get
			{
				return elements ?? (elements = dms.GetElements().ToArray());
			}
		}

		public IDmsElement SelectedElement
		{
			get
			{
				return selectedElement ?? (selectedElement = Elements.First());
			}

			set
			{
				if (value == SelectedElement)
				{
					return;
				}

				selectedElement = value;
			}
		}

		public int SelectedParameterId
		{
			get
			{
				return selectedParameterId;
			}

			set
			{
				if (value == SelectedParameterId)
				{
					return;
				}

				selectedParameterId = value;
			}
		}
	}
}
