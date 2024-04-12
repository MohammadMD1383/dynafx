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
		
		Connections {
			target: soundLooper
			
			function onStarted() {
				btn.text = "Stop";
			}
			
			function onStopped() {
				btn.text = "Start";
			}
		}
	}
}
