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

		End Sub

		Protected Overrides Sub UpdateForm()
			MyBase.UpdateForm()
			PrepareFilteredStorage(Control.Storage)
			Dim lbl As AppointmentLabel = actualStorage.Appointments.Labels(Controller.LabelId)
			Dim index As Integer = GetFilteredLabelIndex(lbl)
			edtLabel.Storage = filteredStorage
			edtLabel.Label = filteredStorage.Appointments.Labels(index)
		End Sub

		Protected Overrides Sub edtLabel_EditValueChanged(ByVal sender As Object, ByVal e As EventArgs)
			Dim index As Integer = GetActualLabelIndex(edtLabel.Label)
			Controller.SetLabel(actualStorage.Appointments.Labels(index))
		End Sub

		Private Function GetFilteredLabelIndex(ByVal lbl As AppointmentLabel) As Integer
			Return filteredStorage.Appointments.Labels.IndexOf(lbl)
		End Function

		Private Function GetActualLabelIndex(ByVal lbl As AppointmentLabel) As Integer
			Return actualStorage.Appointments.Labels.IndexOf(lbl)
		End Function
	End Class
End Namespace