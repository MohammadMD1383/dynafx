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
		id: btn
		text: "Start"
		anchors.centerIn: parent
		
		onClicked: {
			soundLooper.toggle();
		}
	}
	
	Label {
		id: outputDeviceLabel
		anchors.horizontalCenter: parent.horizontalCenter
		anchors.top: btn.bottom
	}
	
	Label {
		id: inputDeviceLabel
		anchors.horizontalCenter: parent.horizontalCenter
		anchors.top: outputDeviceLabel.bottom
	}
	
	Connections {
		target: soundLooper
		
		function onStarted() {
			btn.text = "Stop";
			outputDeviceLabel.text = `Output Device: ${soundLooper.getOutputDeviceName()}`;
			inputDeviceLabel.text = `Input Device: ${soundLooper.getInputDeviceName()}`;
		}
		
		function onStopped() {
			btn.text = "Start";
			outputDeviceLabel.text = '';
			inputDeviceLabel.text = '';
		}
	}
}
