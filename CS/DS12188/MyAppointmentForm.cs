using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.UI;
using DevExpress.XtraScheduler;

namespace DS12188 {
    public partial class MyAppointmentForm : AppointmentForm{
        public MyAppointmentForm() {
            InitializeComponent();
        }

        private SchedulerStorage actualStorage;
        private SchedulerStorage filteredStorage;

        private void PrepareFilteredStorage(SchedulerStorage storage) {
            actualStorage = storage;
            filteredStorage = new SchedulerStorage();
            filteredStorage.Appointments.Labels.Clear();
            foreach (AppointmentLabel label in actualStorage.Appointments.Labels) {
                if (label.DisplayName != "Deleted Label")
                    filteredStorage.Appointments.Labels.Add(label);
            }
        }

        public MyAppointmentForm(SchedulerControl control, Appointment apt)
			: base(control, apt, false) {
		}

        public MyAppointmentForm(SchedulerControl control, Appointment apt, bool openRecurrenceForm)
            : base(control, apt, openRecurrenceForm) 
        {            
            
        }

        protected override void UpdateForm() {
            base.UpdateForm();
            PrepareFilteredStorage(Control.Storage);
            AppointmentLabel lbl = actualStorage.Appointments.Labels[Controller.LabelId];
            int index = GetFilteredLabelIndex(lbl);
            edtLabel.Storage = filteredStorage;
            edtLabel.Label = filteredStorage.Appointments.Labels[index];
        }

        protected override void edtLabel_EditValueChanged(object sender, EventArgs e) {
            int index = GetActualLabelIndex(edtLabel.Label);
            Controller.SetLabel(actualStorage.Appointments.Labels[index]);
        }

        private int GetFilteredLabelIndex(AppointmentLabel lbl) {
            return filteredStorage.Appointments.Labels.IndexOf(lbl);
        }

        private int GetActualLabelIndex(AppointmentLabel lbl) {
            return actualStorage.Appointments.Labels.IndexOf(lbl);
        }
    }
}