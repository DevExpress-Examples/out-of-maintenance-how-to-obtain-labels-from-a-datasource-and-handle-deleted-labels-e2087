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
Namespace DS12188
	Partial Public Class MyAppointmentForm
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
            CType(Me.chkAllDay.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtStartDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtEndDate.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtStartTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtEndTime.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtLabel.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtShowTimeAs.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.tbSubject.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtResource.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtResources.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.edtResources.ResourcesCheckedListBoxControl, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.chkReminder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.tbDescription.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.cbReminder.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.tbLocation.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.panel1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.panel1.SuspendLayout()
            Me.progressPanel.SuspendLayout()
            CType(Me.tbProgress, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.tbProgress.Properties, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'lblLabel
            '
            Me.lblLabel.Appearance.BackColor = System.Drawing.Color.Transparent
            '
            'chkAllDay
            '
            '
            'btnOk
            '
            Me.btnOk.Location = New System.Drawing.Point(16, 355)
            '
            'btnCancel
            '
            Me.btnCancel.Location = New System.Drawing.Point(104, 355)
            '
            'btnDelete
            '
            Me.btnDelete.Location = New System.Drawing.Point(192, 355)
            '
            'btnRecurrence
            '
            Me.btnRecurrence.Location = New System.Drawing.Point(280, 355)
            '
            'edtStartDate
            '
            Me.edtStartDate.EditValue = New Date(2005, 3, 31, 0, 0, 0, 0)
            Me.edtStartDate.Size = New System.Drawing.Size(126, 20)
            '
            'edtEndDate
            '
            Me.edtEndDate.EditValue = New Date(2005, 3, 31, 0, 0, 0, 0)
            Me.edtEndDate.Size = New System.Drawing.Size(126, 20)
            '
            'edtStartTime
            '
            Me.edtStartTime.EditValue = New Date(2005, 3, 31, 0, 0, 0, 0)
            Me.edtStartTime.Location = New System.Drawing.Point(230, 79)
            '
            'edtEndTime
            '
            Me.edtEndTime.EditValue = New Date(2005, 3, 31, 0, 0, 0, 0)
            Me.edtEndTime.Location = New System.Drawing.Point(230, 103)
            '
            'edtLabel
            '
            '
            'edtShowTimeAs
            '
            Me.edtShowTimeAs.Size = New System.Drawing.Size(222, 20)
            '
            'tbSubject
            '
            Me.tbSubject.Size = New System.Drawing.Size(422, 20)
            '
            'edtResource
            '
            '
            'edtResources
            '
            '
            '
            '
            Me.edtResources.ResourcesCheckedListBoxControl.CheckOnClick = True
            Me.edtResources.ResourcesCheckedListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill
            Me.edtResources.ResourcesCheckedListBoxControl.Location = New System.Drawing.Point(0, 0)
            Me.edtResources.ResourcesCheckedListBoxControl.Name = ""
            Me.edtResources.ResourcesCheckedListBoxControl.Size = New System.Drawing.Size(200, 100)
            Me.edtResources.ResourcesCheckedListBoxControl.TabIndex = 0
            '
            'chkReminder
            '
            '
            'tbDescription
            '
            Me.tbDescription.Size = New System.Drawing.Size(502, 144)
            '
            'cbReminder
            '
            '
            'tbLocation
            '
            Me.tbLocation.Size = New System.Drawing.Size(222, 20)
            '
            'panel1
            '
            Me.panel1.Location = New System.Drawing.Point(331, 41)
            '
            'progressPanel
            '
            Me.progressPanel.Size = New System.Drawing.Size(502, 34)
            '
            'tbProgress
            '
            Me.tbProgress.Properties.LabelAppearance.Options.UseTextOptions = True
            Me.tbProgress.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center
            Me.tbProgress.Size = New System.Drawing.Size(393, 31)
            '
            'lblPercentComplete
            '
            Me.lblPercentComplete.Appearance.BackColor = System.Drawing.Color.Transparent
            '
            'lblPercentCompleteValue
            '
            Me.lblPercentCompleteValue.Appearance.BackColor = System.Drawing.Color.Transparent
            Me.lblPercentCompleteValue.Location = New System.Drawing.Point(484, 10)
            '
            'MyAppointmentForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(534, 389)
            Me.MinimumSize = New System.Drawing.Size(506, 293)
            Me.Name = "MyAppointmentForm"
            Me.Text = "MyAppointmentForm"
            CType(Me.chkAllDay.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtStartDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtStartDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtEndDate.Properties.CalendarTimeProperties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtEndDate.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtStartTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtEndTime.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtLabel.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtShowTimeAs.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.tbSubject.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtResource.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtResources.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.edtResources.ResourcesCheckedListBoxControl, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.chkReminder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.tbDescription.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.cbReminder.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.tbLocation.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.panel1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.panel1.ResumeLayout(False)
            Me.panel1.PerformLayout()
            Me.progressPanel.ResumeLayout(False)
            Me.progressPanel.PerformLayout()
            CType(Me.tbProgress.Properties, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.tbProgress, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub

		#End Region
	End Class
End Namespace