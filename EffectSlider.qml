import QtQuick
import QtQuick.Controls
import QtQuick.Layouts
import QtQuick.Controls.Universal

ColumnLayout {
	property alias name: effectName.text
	property alias min: slider.from
	property alias max: slider.to
	property alias step: slider.stepSize
	property alias value: slider.value
	
	property var format: v => v
	
	id: root
	Layout.fillHeight: parent
	
	Text {
		id: txtValue
		text: root.format(slider.value)
		Layout.alignment: Qt.AlignHCenter
		horizontalAlignment: Text.AlignHCenter
		verticalAlignment: Text.AlignVCenter
	}
	
	Slider {
		id: slider
		orientation: Qt.Vertical
		Layout.fillHeight: true
		Layout.alignment: Qt.AlignHCenter
		
		onMoved: txtValue.text = root.format(value)
	}
	
	Text {
		id: effectName
		Layout.alignment: Qt.AlignHCenter
		horizontalAlignment: Text.AlignHCenter
		verticalAlignment: Text.AlignVCenter
	}
}
