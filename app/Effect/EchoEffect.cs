using Un4seen.Bass;

namespace Dynafx.Effect;

public class EchoEffect(
	int streamHandle,
	float mix = 0f,
	float feedback = 0f,
	float leftDelay = 1f,
	float rightDelay = 1f,
	bool panDelay = false
) : BaseEffect<BASS_DX8_ECHO>(
	new BASS_DX8_ECHO(mix, feedback, leftDelay, rightDelay, panDelay),
	streamHandle,
	BASSFXType.BASS_FX_DX8_ECHO,
	90
) {
	public float Mix {
		get => Props.fWetDryMix;
		set => Props.fWetDryMix = value;
	}

	public float Feedback {
		get => Props.fFeedback;
		set => Props.fFeedback = value;
	}

	public float LeftDelay {
		get => Props.fLeftDelay;
		set => Props.fLeftDelay = value;
	}

	public float RightDelay {
		get => Props.fRightDelay;
		set => Props.fRightDelay = value;
	}

	public bool PanDelay {
		get => Props.lPanDelay;
		set => Props.lPanDelay = value;
	}
}
