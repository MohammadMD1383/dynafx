using ReactiveUI;

namespace Dynafx.ViewModels;

public class ViewModelBase : ReactiveObject {
	protected static string Percentage(float percent) => percent.ToString("0.00") + "%";
	protected static string MsTime(float num) => num.ToString("0.0") + "ms";
}
