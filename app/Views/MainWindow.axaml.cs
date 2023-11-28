using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Dynafx.Sound;
using Dynafx.ViewModels;

namespace Dynafx.Views;

public partial class MainWindow : Window {
	private readonly MainWindowViewModel _viewModel = new();
	private readonly SoundLooper         _looper    = new();

	public MainWindow() {
		DataContext = _viewModel;
		_viewModel.PropertyChanged += ViewModelOnPropertyChanged;
		InitializeComponent();
	}

	private void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e) {
		switch (e.PropertyName) {
			case nameof(MainWindowViewModel.Echo): {
				if (_viewModel.Echo) { } else { }

				break;
			}
		}
	}

	private void StartStopButtonOnClicked(object? sender, RoutedEventArgs e) {
		if (_viewModel.SwapStartStopButtonText()) {
			_looper.Start();
		} else {
			_looper.Stop();
		}
	}
}
