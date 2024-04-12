#ifndef SOUND_LOOPER_HPP
#define SOUND_LOOPER_HPP

#include <QObject>
#include <bass.h>

class SoundLooper : public QObject {
	Q_OBJECT

	bool running{};
	HRECORD recording{};
	HSTREAM stream{};

public:
	[[nodiscard]]
	Q_INVOKABLE bool isRunning() const {
		return running;
	}

	static DWORD streamRecordProc(HSTREAM handle, void* buffer, DWORD length, void* user) {
		const auto _this = static_cast<SoundLooper *>(user);
		return BASS_ChannelGetData(_this->recording, buffer, length);
	}

	Q_INVOKABLE void start() {
		if (not BASS_Init(-1, 48'000, BASS_DEVICE_DEFAULT, nullptr, nullptr))
			exit(-10);

		if (not BASS_RecordInit(0))
			exit(-11);

		recording = BASS_RecordStart(48'000, 2, 0, nullptr, nullptr);
		if (not recording)
			exit(-12);

		stream = BASS_StreamCreate(48'000, 2, 0, &streamRecordProc, this);
		if (not stream)
			exit(-13);

		BASS_ChannelSetAttribute(stream, BASS_ATTRIB_BUFFER, 0);
		BASS_ChannelStart(stream);

		constexpr BASS_DX8_ECHO echoParams{50, 20, 565, 565, false};
		const auto echoEffect = BASS_ChannelSetFX(stream, BASS_FX_DX8_ECHO, 0);
		BASS_FXSetParameters(echoEffect, &echoParams);
		running = true;
	}

	Q_INVOKABLE void stop() {
		BASS_ChannelFree(stream);
		BASS_RecordFree();
		BASS_Free();
		running = false;
	}
};

#endif
