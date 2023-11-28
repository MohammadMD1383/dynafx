using System;
using Dynafx.Effect;
using Un4seen.Bass;
using static System.Console;

namespace Dynafx.Sound;

public class SoundLooper {
	private          int _recordHandle;
	private          int _streamHandle;
	private readonly int _frequency;
	private readonly int _channels;

	public bool IsPlaying { get; private set; }

	public EchoEffect? EchoEffect { get; private set; }

	public SoundLooper(
		int inDevice = -1,
		int outDevice = -1,
		int frequency = 44100,
		int channels = 2
	) {
		_channels = channels;
		_frequency = frequency;

		Bass.BASS_RecordInit(inDevice);
		Bass.BASS_Init(outDevice, frequency, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
	}

	~SoundLooper() {
		Stop();
		Bass.BASS_Free();
		Bass.BASS_RecordFree();
	}

	private void InitEffects() {
		EchoEffect = new EchoEffect(_streamHandle);
	}

	private void FreeEffects() {
		EchoEffect = null;
	}

	private int StreamProc(int handle, IntPtr buffer, int length, IntPtr user) {
		return Bass.BASS_ChannelGetData(_recordHandle, buffer, length);
	}

	private void FreeStreamHandle() {
		if (!Bass.BASS_ChannelFree(_streamHandle)) {
			WriteLine($"Error freeing stream: {Bass.BASS_ErrorGetCode()}");
		}
	}

	private void FreeRecordHandle() {
		if (!Bass.BASS_ChannelFree(_recordHandle)) {
			WriteLine($"Error freeing recording session: {Bass.BASS_ErrorGetCode()}");
		}
	}

	public void Start() {
		if (IsPlaying) {
			return;
		}

		_recordHandle = Bass.BASS_RecordStart(_frequency, _channels, BASSFlag.BASS_DEFAULT, null, IntPtr.Zero);
		if (_recordHandle == 0) {
			WriteLine($"Error starting record session: {Bass.BASS_ErrorGetCode()}");
			return;
		}

		_streamHandle = Bass.BASS_StreamCreate(_frequency, _channels, BASSFlag.BASS_DEFAULT, StreamProc, IntPtr.Zero);
		if (_streamHandle == 0) {
			WriteLine($"Error creating stream for recording: {Bass.BASS_ErrorGetCode()}");
			FreeRecordHandle();
			return;
		}

		if (!Bass.BASS_ChannelSetAttribute(_streamHandle, BASSAttribute.BASS_ATTRIB_BUFFER, 0)) {
			WriteLine($"Error disabling buffering on stream: {Bass.BASS_ErrorGetCode()}");
			FreeStreamHandle();
			FreeRecordHandle();
			return;
		}

		if (!Bass.BASS_ChannelStart(_streamHandle)) {
			WriteLine($"Error playing stream: {Bass.BASS_ErrorGetCode()}");
			FreeStreamHandle();
			FreeRecordHandle();
			return;
		}

		InitEffects();

		IsPlaying = true;
	}

	public void Stop() {
		if (!IsPlaying) {
			return;
		}

		FreeEffects();

		if (!Bass.BASS_ChannelStop(_streamHandle)) {
			WriteLine($"Error stopping stream: {Bass.BASS_ErrorGetCode()}");
			return;
		}

		FreeStreamHandle();
		FreeRecordHandle();

		IsPlaying = false;
	}
}
