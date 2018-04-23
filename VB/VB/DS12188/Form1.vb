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
Imports DevExpress.XtraScheduler
Imports System.Data.OleDb
Imports DevExpress.Utils.Menu

Namespace DS12188
	Partial Public Class Form1
		Inherits Form
		Private deletedLabel As New AppointmentLabel(Color.Gray, "Deleted Label", "Deleted Label")

		Private Function CloneLabel(ByVal source As AppointmentLabel) As AppointmentLabel
			Dim clone As New AppointmentLabel()
			clone.Color = source.Color
			clone.DisplayName = source.DisplayName
			clone.MenuCaption = source.MenuCaption
			Return clone
		End Function


		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			InitializeLabels()
			' TODO: This line of code loads data into the 'carsDBLabelDataSet11.Labels' table. You can move, or remove it, as needed.
			'this.labelsTableAdapter1.Fill(this.carsDBLabelDataSet11.Labels);
			' TODO: This line of code loads data into the 'carsDBLabelDataSet.Cars' table. You can move, or remove it, as needed.
			Me.carsTableAdapter.Fill(Me.carsDBLabelDataSet.Cars)
			' TODO: This line of code loads data into the 'carsDBLabelDataSet.CarScheduling' table. You can move, or remove it, as needed.
			Me.carSchedulingTableAdapter.Fill(Me.carsDBLabelDataSet.CarScheduling)

			AddHandler carSchedulingTableAdapter.Adapter.RowUpdated, AddressOf carSchedulingTableAdapter_RowUpdated


		End Sub

		Private Sub InitializeLabels()
			Me.labelsTableAdapter1.Fill(Me.carsDBLabelDataSet11.Labels)

			Dim labels As DataTable = Me.carsDBLabelDataSet11.Labels

			If labels.Rows.Count = 0 Then
				Return
			End If
			schedulerControl1.Storage.Appointments.Labels.Clear()

			schedulerControl1.Storage.Appointments.Labels.BeginUpdate()
			For i As Integer = 0 To labels.Rows.Count - 1
				Dim color As Color = Color.FromArgb(Int32.Parse(labels.Rows(i).ItemArray(1).ToString()))
				Dim dislayName As String = labels.Rows(i).ItemArray(2).ToString()
				Dim menuCaption As String = labels.Rows(i).ItemArray(3).ToString()
				Dim aptLabel As New AppointmentLabel(color, dislayName, menuCaption)
				schedulerControl1.Storage.Appointments.Labels.Add(aptLabel)
			Next i
			schedulerControl1.Storage.Appointments.Labels.EndUpdate()
		End Sub

		Private Sub gridView1_RowUpdated(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Base.RowObjectEventArgs) Handles gridView1.RowUpdated
			labelsTableAdapter1.Update(carsDBLabelDataSet11)
			carsDBLabelDataSet11.AcceptChanges()
			InitializeLabels()
			schedulerControl1.Refresh()
		End Sub

		Private Sub OnApptChangedInsertedDeleted(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs) Handles schedulerStorage1.AppointmentsChanged, schedulerStorage1.AppointmentsInserted, schedulerStorage1.AppointmentsDeleted
			carSchedulingTableAdapter.Update(carsDBLabelDataSet)
			carsDBLabelDataSet.AcceptChanges()
		End Sub

		Private Sub carSchedulingTableAdapter_RowUpdated(ByVal sender As Object, ByVal e As OleDbRowUpdatedEventArgs)
			If e.Status = UpdateStatus.Continue AndAlso e.StatementType = StatementType.Insert Then
				Dim id As Integer = 0
				Using cmd As New OleDbCommand("SELECT @@IDENTITY", carSchedulingTableAdapter.Connection)
					id = CInt(Fix(cmd.ExecuteScalar()))
				End Using
				e.Row("ID") = id
			End If
		End Sub

		Private Sub simpleButton1_Click(ByVal sender As Object, ByVal e As EventArgs) Handles simpleButton1.Click
			If appointmentLabelEdit1.SelectedIndex = -1 Then
				MessageBox.Show("Please select a valid label")
				Return
			End If
			Dim index As Integer = appointmentLabelEdit1.SelectedIndex
			Dim selectedLabel As AppointmentLabel = schedulerStorage1.Appointments.Labels(index)
			DeleteLabelFromLabelCollection(index)
			DeleteLabelFromDataBase(selectedLabel)

			schedulerControl1.RefreshData()
		End Sub

		Private Sub DeleteLabelFromLabelCollection(ByVal index As Integer)
			schedulerStorage1.Appointments.Labels.BeginUpdate()
			schedulerStorage1.Appointments.Labels.RemoveAt(index)
			schedulerStorage1.Appointments.Labels.Insert(index, CloneLabel(Me.deletedLabel))
			schedulerStorage1.Appointments.Labels.EndUpdate()
		End Sub

		Private Sub DeleteLabelFromDataBase(ByVal selectedLabel As AppointmentLabel)

			Dim filterExpression As String = String.Format("Color = '{0}'", selectedLabel.Color.ToArgb())
			Dim labelRow() As DataRow = carsDBLabelDataSet11.Tables("Labels").Select(filterExpression)
			labelRow(0)("Color") = Me.deletedLabel.Color.ToArgb()
			labelRow(0)("DisplayName") = Me.deletedLabel.DisplayName
			labelRow(0)("MenuCaption") = Me.deletedLabel.MenuCaption
			labelsTableAdapter1.Update(Me.carsDBLabelDataSet11)
			carsDBLabelDataSet11.AcceptChanges()
		End Sub

		Private Sub schedulerControl1_AllowAppointmentConflicts(ByVal sender As Object, ByVal e As AppointmentConflictEventArgs) Handles schedulerControl1.AllowAppointmentConflicts
			'AppointmentBaseCollection abc = ((SchedulerControl)sender).Storage.Appointments.Items;
			e.Conflicts.Clear()
			Dim labelDisplayName As String = schedulerControl1.Storage.Appointments.Labels(e.AppointmentClone.LabelId).DisplayName
			Dim apt As Appointment = schedulerControl1.Storage.CreateAppointment(AppointmentType.Normal)
			If labelDisplayName = "Deleted Label" Then
				e.Conflicts.Add(apt)
			End If
		End Sub

		Private Sub schedulerControl1_EditAppointmentFormShowing(ByVal sender As Object, ByVal e As AppointmentFormEventArgs) Handles schedulerControl1.EditAppointmentFormShowing

			Dim apt As Appointment = e.Appointment

			' Required to open the recurrence form via context menu.
			Dim openRecurrenceForm As Boolean = apt.IsRecurring AndAlso schedulerStorage1.Appointments.IsNewAppointment(apt)

			' Create a custom form.
			Dim myForm As New MyAppointmentForm(CType(sender, SchedulerControl), apt, openRecurrenceForm)

			' Required for skins support.
			myForm.LookAndFeel.ParentLookAndFeel = schedulerControl1.LookAndFeel

			e.DialogResult = myForm.ShowDialog()
			schedulerControl1.Refresh()
			e.Handled = True

		End Sub

	

        Private Sub schedulerControl1_PopupMenuShowing(sender As System.Object, e As DevExpress.XtraScheduler.PopupMenuShowingEventArgs) Handles schedulerControl1.PopupMenuShowing
            If e.Menu.Id = SchedulerMenuItemId.AppointmentMenu Then

                Dim menu As SchedulerPopupMenu = e.Menu.GetPopupMenuById(SchedulerMenuItemId.LabelSubMenu, True)

                For Each item As DXMenuItem In menu.Items
                    If item.Caption = "Deleted Label" Then
                        item.Visible = False
                    End If
                Next item
            End If
        End Sub
    End Class
End Namespace