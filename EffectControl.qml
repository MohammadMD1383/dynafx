import QtQuick
import QtQuick.Controls
import QtQuick.Controls.Universal
import QtQuick.Layouts

Rectangle {
	property string name
	
	property alias enabled: effectToggle.checked
	property alias controls: controls.model
	property alias switches: switches.model
	
	id: root
	width: rectBorder.width
	height: rectBorder.height + nameRect.height
	
	Rectangle {
		id: rectBorder
		width: grid.width + 20
		height: grid.height + nameRect.height
		anchors.centerIn: parent
		border.color: "gray"
		border.width: 1
		radius: 5
		
		Rectangle {
			id: nameRect
			anchors.top: parent.top
			anchors.left: parent.left
			anchors.topMargin: -height / 2
			anchors.leftMargin: 15
			width: effectToggle.width + 10
			height: effectToggle.height + 5
			color: "white"
			border.width: 1
			border.color: "gray"
			radius: 3
			
			CheckBox {
				id: effectToggle
				anchors.centerIn: parent
				text: root.name
			}
		}
		
		GridLayout {
			id: grid
			height: 300
			rows: 2
			columns: 4
			uniformCellWidths: true
			anchors.centerIn: parent
			anchors.verticalCenterOffset: nameRect.height / 4
			
			Repeater {
				id: controls
				
				EffectSlider {
					required property int index
					required property var modelData
					
					name: modelData.name
					min: modelData.min
					max: modelData.max
					step: modelData.step
					Layout.row: 0
					Layout.column: index
					format: modelData.format
				}
			}
			
			Column {
				Layout.row: 3
				Layout.column: 0
				Layout.columnSpan: 4
				Layout.alignment: Qt.AlignVCenter | Qt.AlignHCenter
				
				Repeater {
					id: switches
					
					CheckBox {
						required property var modelData
						
						text: modelData.name
					}
				}
			}
		}
	}
}
