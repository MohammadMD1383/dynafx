import QtQuick
import QtQuick.Controls
import QtQuick.Controls.Material
import QtQuick.Layouts
import QtQuick.Window

ApplicationWindow {
	width: 700
	height: 450
	visible: true
	
	Button {
		text: soundLooper.isRunning() ? "Stop" : "Start"
		anchors.centerIn: parent
		onClicked: {
			if (soundLooper.isRunning())
				soundLooper.stop();
			else
				soundLooper.start();
		}
	}
}
