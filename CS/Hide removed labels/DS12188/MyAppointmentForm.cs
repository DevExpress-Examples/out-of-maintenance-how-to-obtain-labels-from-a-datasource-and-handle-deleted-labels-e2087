// Developer Express Code Central Example:
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

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PrepareFilteredStorage(Control.Storage);
            AppointmentLabel lbl = actualStorage.Appointments.Labels[Controller.LabelId];
            int index = GetFilteredLabelIndex(lbl);
            edtLabel.Storage = filteredStorage;
            edtLabel.Label = filteredStorage.Appointments.Labels[index];
        }

   

        private int GetFilteredLabelIndex(AppointmentLabel lbl) {
            return filteredStorage.Appointments.Labels.IndexOf(lbl);
        }

        private int GetActualLabelIndex(AppointmentLabel lbl) {
            return actualStorage.Appointments.Labels.IndexOf(lbl);
        }

        private void edtLabel_EditValueChanged_1(object sender, EventArgs e)
        {
            int index = GetActualLabelIndex(edtLabel.Label);
            Controller.SetLabel(actualStorage.Appointments.Labels[index]);
        }
    }
}