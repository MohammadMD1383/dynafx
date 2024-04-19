import QtQuick
import QtQuick.Controls
import QtQuick.Controls.Universal
import QtQuick.Layouts
import QtQuick.Window

ApplicationWindow {
	width: 1000
	height: 600
	visible: true
	
	Flow {
		anchors.top: parent.top
		anchors.bottom: btn.top
		anchors.left: parent.left
		anchors.right: parent.right
		anchors.bottomMargin: 10
		
		EffectControl {
			name: "Echo"
			controls: [
				{name: "Mix\n", min: 0, max: 100, step: 0.01, format: v => v.toFixed(2) + "%"},
				{name: "Feed\nBack", min: 0, max: 100, step: 0.01, format: v => v.toFixed(2) + "%"},
				{name: "Left\nDelay", min: 1, max: 2000, step: 1, format: v => v + "ms"},
				{name: "Right\nDelay", min: 1, max: 2000, step: 1, format: v => v + "ms"},
			]
			switches: [
				{name: "Lock left/right delay"},
			]
		}
	}
	
	Button {
		id: btn
		text: "Start"
		anchors.horizontalCenter: parent.horizontalCenter
		anchors.bottom: parent.bottom
		anchors.bottomMargin: 10
		
		onClicked: {
			soundLooper.toggle();
		}
	}
	
	Connections {
		target: soundLooper
		
		function onStarted() {
			btn.text = "Stop";
			// outputDeviceLabel.text = `Output Device: ${soundLooper.getOutputDeviceName()}`;
			// inputDeviceLabel.text = `Input Device: ${soundLooper.getInputDeviceName()}`;
		}
		
		function onStopped() {
			btn.text = "Start";
			// outputDeviceLabel.text = '';
			// inputDeviceLabel.text = '';
		}
	}
}
