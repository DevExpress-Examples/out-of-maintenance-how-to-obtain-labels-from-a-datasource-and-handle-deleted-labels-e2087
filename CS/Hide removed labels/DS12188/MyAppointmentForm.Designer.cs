﻿// Developer Express Code Central Example:
// How to obtain labels from a datasource and handle deleted labels
// 
// This example elaborates the approach illustrated in example
// http://www.devexpress.com/scid=E2028. Labels for appointments are obtained from
// a data source. When deleting, the label is not actually removed but its color
// and caption are changed to identify a "deleted label" entity which should be
// treated specifically. An appointment labeled with the "deleted label" could not
// be created or edited (moved, resized).
// Additionally, this example demonstrates
// how to hide "deleted" labels when editing or creating a new appointment, as well
// as hide them in the appointment context menu.
// This approach can help you to
// implement more complex scenario.
// 
// You can find sample updates and versions for different programming languages here:
// http://www.devexpress.com/example=E2087

namespace DS12188 {
    partial class MyAppointmentForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            ((System.ComponentModel.ISupportInitialize)(this.chkAllDay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtStartDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtStartDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEndDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEndDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtStartTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEndTime.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLabel.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtShowTimeAs.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSubject.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtResource.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtResources.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtResources.ResourcesCheckedListBoxControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReminder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbReminder.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLocation.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
            this.panel1.SuspendLayout();
            this.progressPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblLabel
            // 
            this.lblLabel.Appearance.BackColor = System.Drawing.Color.Transparent;
            // 
            // chkAllDay
            // 
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(16, 355);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(104, 355);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(192, 355);
            // 
            // btnRecurrence
            // 
            this.btnRecurrence.Location = new System.Drawing.Point(280, 355);
            // 
            // edtStartDate
            // 
            this.edtStartDate.EditValue = new System.DateTime(2005, 3, 31, 0, 0, 0, 0);
            this.edtStartDate.Size = new System.Drawing.Size(126, 20);
            // 
            // edtEndDate
            // 
            this.edtEndDate.EditValue = new System.DateTime(2005, 3, 31, 0, 0, 0, 0);
            this.edtEndDate.Size = new System.Drawing.Size(126, 20);
            // 
            // edtStartTime
            // 
            this.edtStartTime.EditValue = new System.DateTime(2005, 3, 31, 0, 0, 0, 0);
            this.edtStartTime.Location = new System.Drawing.Point(230, 79);
            // 
            // edtEndTime
            // 
            this.edtEndTime.EditValue = new System.DateTime(2005, 3, 31, 0, 0, 0, 0);
            this.edtEndTime.Location = new System.Drawing.Point(230, 103);
            // 
            // edtLabel
            // 
            this.edtLabel.EditValueChanged += new System.EventHandler(this.edtLabel_EditValueChanged_1);
            // 
            // edtShowTimeAs
            // 
            this.edtShowTimeAs.Size = new System.Drawing.Size(222, 20);
            // 
            // tbSubject
            // 
            this.tbSubject.Size = new System.Drawing.Size(422, 20);
            // 
            // edtResource
            // 
            // 
            // edtResources
            // 
            // 
            // 
            // 
            this.edtResources.ResourcesCheckedListBoxControl.CheckOnClick = true;
            this.edtResources.ResourcesCheckedListBoxControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edtResources.ResourcesCheckedListBoxControl.Location = new System.Drawing.Point(0, 0);
            this.edtResources.ResourcesCheckedListBoxControl.Name = "";
            this.edtResources.ResourcesCheckedListBoxControl.Size = new System.Drawing.Size(200, 100);
            this.edtResources.ResourcesCheckedListBoxControl.TabIndex = 0;
            // 
            // chkReminder
            // 
            // 
            // tbDescription
            // 
            this.tbDescription.Size = new System.Drawing.Size(502, 144);
            // 
            // cbReminder
            // 
            // 
            // tbLocation
            // 
            this.tbLocation.Size = new System.Drawing.Size(222, 20);
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(331, 41);
            // 
            // progressPanel
            // 
            this.progressPanel.Size = new System.Drawing.Size(502, 34);
            // 
            // tbProgress
            // 
            this.tbProgress.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.tbProgress.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.tbProgress.Size = new System.Drawing.Size(393, 31);
            // 
            // lblPercentComplete
            // 
            this.lblPercentComplete.Appearance.BackColor = System.Drawing.Color.Transparent;
            // 
            // lblPercentCompleteValue
            // 
            this.lblPercentCompleteValue.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.lblPercentCompleteValue.Location = new System.Drawing.Point(484, 10);
            // 
            // MyAppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(534, 389);
            this.MinimumSize = new System.Drawing.Size(506, 293);
            this.Name = "MyAppointmentForm";
            this.Text = "MyAppointmentForm";
            ((System.ComponentModel.ISupportInitialize)(this.chkAllDay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtStartDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtStartDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEndDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEndDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtStartTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtEndTime.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLabel.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtShowTimeAs.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbSubject.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtResource.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtResources.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtResources.ResourcesCheckedListBoxControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkReminder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbReminder.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbLocation.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.progressPanel.ResumeLayout(false);
            this.progressPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
    }
}