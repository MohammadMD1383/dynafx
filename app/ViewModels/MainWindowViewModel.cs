using ReactiveUI;

namespace Dynafx.ViewModels;

public class MainWindowViewModel : ViewModelBase {
	#pragma warning disable CA1822 // Mark members as static

	#region Echo
	private bool  _echo;
	private float _echoMix;
	private float _echoFeedback;
	private float _echoLeftDelay  = 1f;
	private float _echoRightDelay = 1f;
	private bool  _echoPanDelay;
	private bool  _echoLockDelay;

	public bool Echo {
		get => _echo;
		set => this.RaiseAndSetIfChanged(ref _echo, value);
	}

	public float EchoMix {
		get => _echoMix;
		set {
			_echoMix = value;
			this.RaisePropertyChanged();
			this.RaisePropertyChanged(nameof(EchoMixStr));
		}
	}

	public float EchoFeedback {
		get => _echoFeedback;
		set {
			_echoFeedback = value;
			this.RaisePropertyChanged();
			this.RaisePropertyChanged(nameof(EchoFeedbackStr));
		}
	}

	public float EchoLeftDelay {
		get => _echoLeftDelay;
		set {
			_echoLeftDelay = value;
			this.RaisePropertyChanged();
			this.RaisePropertyChanged(nameof(EchoLeftDelayStr));

			if (_echoLockDelay) {
				_echoRightDelay = value;
				this.RaisePropertyChanged(nameof(EchoRightDelay));
				this.RaisePropertyChanged(nameof(EchoRightDelayStr));
			}
		}
	}

	public float EchoRightDelay {
		get => _echoRightDelay;
		set {
			_echoRightDelay = value;
			this.RaisePropertyChanged();
			this.RaisePropertyChanged(nameof(EchoRightDelayStr));

			if (_echoLockDelay) {
				_echoLeftDelay = value;
				this.RaisePropertyChanged(nameof(EchoLeftDelay));
				this.RaisePropertyChanged(nameof(EchoLeftDelayStr));
			}
		}
	}

	public bool EchoPanDelay {
		get => _echoPanDelay;
		set => this.RaiseAndSetIfChanged(ref _echoPanDelay, value);
	}

	public bool EchoLockDelay {
		get => _echoLockDelay;
		set {
			_echoLockDelay = value;
			this.RaisePropertyChanged();

			if (_echoLockDelay) {
				_echoRightDelay = _echoLeftDelay;
				this.RaisePropertyChanged(nameof(EchoRightDelay));
				this.RaisePropertyChanged(nameof(EchoRightDelayStr));
			}
		}
	}

	public string EchoMixStr        => Percentage(_echoMix);
	public string EchoFeedbackStr   => Percentage(_echoFeedback);
	public string EchoLeftDelayStr  => MsTime(_echoLeftDelay);
	public string EchoRightDelayStr => MsTime(_echoRightDelay);
	#endregion

	private string _startStopButtonText = "Start";

	public string StartStopButtonText {
		get => _startStopButtonText;
		set => this.RaiseAndSetIfChanged(ref _startStopButtonText, value);
	}

	public bool SwapStartStopButtonText() {
		if (StartStopButtonText == "Start") {
			StartStopButtonText = "Stop";
			return true;
		}

		StartStopButtonText = "Start";
		return false;
	}

	#pragma warning restore CA1822 // Mark members as static
}
