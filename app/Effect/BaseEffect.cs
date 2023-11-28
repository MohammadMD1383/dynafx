using Un4seen.Bass;
using static System.Console;

namespace Dynafx.Effect;

public class BaseEffect<TEffectProps>(TEffectProps props, int streamHandle, BASSFXType fxType, int priority) {
	protected readonly TEffectProps Props = props;
	private            int          _fxHandle;

	public bool Enabled { get; private set; }

	~BaseEffect() {
		if (Enabled) {
			Bass.BASS_ChannelRemoveFX(streamHandle, _fxHandle);
		}
	}

	public void TurnOn() {
		if (Enabled) {
			return;
		}

		_fxHandle = Bass.BASS_ChannelSetFX(streamHandle, fxType, priority);

		if (_fxHandle == 0) {
			WriteLine($"Error turning on effect ({GetType().FullName}): {Bass.BASS_ErrorGetCode()}");
			return;
		}

		Enabled = true;
	}

	public void TurnOff() {
		if (!Enabled) {
			return;
		}

		if (!Bass.BASS_ChannelRemoveFX(streamHandle, _fxHandle)) {
			WriteLine($"Error turning off effect ({GetType().FullName}): {Bass.BASS_ErrorGetCode()}");
			return;
		}

		_fxHandle = 0;
		Enabled = false;
	}

	public void Update() {
		if (!Enabled) {
			return;
		}

		if (!Bass.BASS_FXSetParameters(_fxHandle, Props)) {
			WriteLine($"Error updating effect ({GetType().FullName}): {Bass.BASS_ErrorGetCode()}");
		}
	}
}
