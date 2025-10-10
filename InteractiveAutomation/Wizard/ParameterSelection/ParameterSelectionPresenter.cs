using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InteractiveAutomation.Wizard.ParameterSelection
{
	internal class ParameterSelectionPresenter
	{
		private readonly IParameterSelectionView parameterSelectionView;

		public ParameterSelectionPresenter(IParameterSelectionView view)
		{
			parameterSelectionView = view ?? throw new ArgumentNullException(nameof(view));

		}

	}
}
