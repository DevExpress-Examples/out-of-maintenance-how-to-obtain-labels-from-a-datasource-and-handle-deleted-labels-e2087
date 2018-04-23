' Developer Express Code Central Example:
' How to obtain labels from a datasource and handle deleted labels
' 
' This example elaborates the approach illustrated in example
' http://www.devexpress.com/scid=E2028. Labels for appointments are obtained from
' a data source. When deleting, the label is not actually removed but its color
' and caption are changed to identify a "deleted label" entity which should be
' treated specifically. An appointment labeled with the "deleted label" could not
' be created or edited (moved, resized).
' Additionally, this example demonstrates
' how to hide "deleted" labels when editing or creating a new appointment, as well
' as hide them in the appointment context menu.
' This approach can help you to
' implement more complex scenario.
' 
' You can find sample updates and versions for different programming languages here:
' http://www.devexpress.com/example=E2087

Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports DevExpress.XtraScheduler.UI
Imports DevExpress.XtraScheduler

Namespace DS12188
	Partial Public Class MyAppointmentForm
		Inherits AppointmentForm
		Public Sub New()
			InitializeComponent()
		End Sub

		Private actualStorage As SchedulerStorage
		Private filteredStorage As SchedulerStorage

		Private Sub PrepareFilteredStorage(ByVal storage As SchedulerStorage)
			actualStorage = storage
			filteredStorage = New SchedulerStorage()
			filteredStorage.Appointments.Labels.Clear()
			For Each label As AppointmentLabel In actualStorage.Appointments.Labels
				If label.DisplayName <> "Deleted Label" Then
					filteredStorage.Appointments.Labels.Add(label)
				End If
			Next label
		End Sub

		Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment)
			MyBase.New(control, apt, False)
		End Sub

		Public Sub New(ByVal control As SchedulerControl, ByVal apt As Appointment, ByVal openRecurrenceForm As Boolean)
			MyBase.New(control, apt, openRecurrenceForm)
            AddHandler edtLabel.EditValueChanged, AddressOf edtLabel_EditValueChanged_1
            PrepareFilteredStorage(control.Storage)
            Dim lbl As AppointmentLabel = actualStorage.Appointments.Labels(Controller.LabelId)
            Dim index As Integer = GetFilteredLabelIndex(lbl)
            edtLabel.Storage = filteredStorage
            edtLabel.Label = filteredStorage.Appointments.Labels(index)
        End Sub

        Protected Overrides Sub OnLoad(e As System.EventArgs)
            MyBase.OnLoad(e)
           
        End Sub


		Private Function GetFilteredLabelIndex(ByVal lbl As AppointmentLabel) As Integer
			Return filteredStorage.Appointments.Labels.IndexOf(lbl)
		End Function

		Private Function GetActualLabelIndex(ByVal lbl As AppointmentLabel) As Integer
			Return actualStorage.Appointments.Labels.IndexOf(lbl)
		End Function

        Private Sub edtLabel_EditValueChanged_1(sender As System.Object, e As System.EventArgs)
            Dim index As Integer = GetActualLabelIndex(edtLabel.Label)
            Controller.SetLabel(actualStorage.Appointments.Labels(index))
        End Sub
    End Class
End Namespace