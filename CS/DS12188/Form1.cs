using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraScheduler;
using System.Data.OleDb;
using DevExpress.Utils.Menu;

namespace DS12188
{
    public partial class Form1 : Form
    {
        AppointmentLabel deletedLabel = new AppointmentLabel(Color.Gray, "Deleted Label", "Deleted Label");

        private AppointmentLabel CloneLabel(AppointmentLabel source) {
            AppointmentLabel clone = new AppointmentLabel();
            clone.Color = source.Color;
            clone.DisplayName = source.DisplayName;
            clone.MenuCaption = source.MenuCaption;
            return clone;
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeLabels();
            // TODO: This line of code loads data into the 'carsDBLabelDataSet11.Labels' table. You can move, or remove it, as needed.
            //this.labelsTableAdapter1.Fill(this.carsDBLabelDataSet11.Labels);
            // TODO: This line of code loads data into the 'carsDBLabelDataSet.Cars' table. You can move, or remove it, as needed.
            this.carsTableAdapter.Fill(this.carsDBLabelDataSet.Cars);
            // TODO: This line of code loads data into the 'carsDBLabelDataSet.CarScheduling' table. You can move, or remove it, as needed.
            this.carSchedulingTableAdapter.Fill(this.carsDBLabelDataSet.CarScheduling);

            carSchedulingTableAdapter.Adapter.RowUpdated += new OleDbRowUpdatedEventHandler (carSchedulingTableAdapter_RowUpdated);

            
        }

        private void InitializeLabels()
        {
            this.labelsTableAdapter1.Fill(this.carsDBLabelDataSet11.Labels);

            DataTable labels = this.carsDBLabelDataSet11.Labels;

            if (labels.Rows.Count == 0)
                return;
            schedulerControl1.Storage.Appointments.Labels.Clear();

            schedulerControl1.Storage.Appointments.Labels.BeginUpdate();
            for (int i = 0; i < labels.Rows.Count; i++)
            {
                Color color = Color.FromArgb(Int32.Parse(labels.Rows[i].ItemArray[1].ToString()));
                string dislayName = labels.Rows[i].ItemArray[2].ToString();
                string menuCaption = labels.Rows[i].ItemArray[3].ToString();
                AppointmentLabel aptLabel = new AppointmentLabel(color, dislayName, menuCaption);
                schedulerControl1.Storage.Appointments.Labels.Add(aptLabel);
            }
            schedulerControl1.Storage.Appointments.Labels.EndUpdate();
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            labelsTableAdapter1.Update(carsDBLabelDataSet11);
            carsDBLabelDataSet11.AcceptChanges();
            InitializeLabels();
            schedulerControl1.Refresh();
        }

        private void OnApptChangedInsertedDeleted(object sender, PersistentObjectsEventArgs e)
        {
            carSchedulingTableAdapter.Update(carsDBLabelDataSet);
            carsDBLabelDataSet.AcceptChanges();
        }

        private void carSchedulingTableAdapter_RowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        {
            if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
            {
                int id = 0;
                using (OleDbCommand cmd = new OleDbCommand("SELECT @@IDENTITY", carSchedulingTableAdapter.Connection))
                {
                    id = (int)cmd.ExecuteScalar();
                }
                e.Row["ID"] = id;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (appointmentLabelEdit1.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a valid label");
                return;
            }
            int index = appointmentLabelEdit1.SelectedIndex;
            AppointmentLabel selectedLabel = schedulerStorage1.Appointments.Labels[index];
            DeleteLabelFromLabelCollection(index);
            DeleteLabelFromDataBase(selectedLabel);
            
            schedulerControl1.RefreshData();
        }

        private void DeleteLabelFromLabelCollection(int index)
        {
            schedulerStorage1.Appointments.Labels.BeginUpdate();
            schedulerStorage1.Appointments.Labels.RemoveAt(index);
            schedulerStorage1.Appointments.Labels.Insert(index, CloneLabel(this.deletedLabel));
            schedulerStorage1.Appointments.Labels.EndUpdate();
        }

        private void DeleteLabelFromDataBase(AppointmentLabel selectedLabel)
        {

            string filterExpression = string.Format("Color = '{0}'", selectedLabel.Color.ToArgb());
            DataRow[] labelRow = carsDBLabelDataSet11.Tables["Labels"].Select(filterExpression);
            labelRow[0]["Color"] = this.deletedLabel.Color.ToArgb();
            labelRow[0]["DisplayName"] = this.deletedLabel.DisplayName;
            labelRow[0]["MenuCaption"] = this.deletedLabel.MenuCaption;
            labelsTableAdapter1.Update(this.carsDBLabelDataSet11);
            carsDBLabelDataSet11.AcceptChanges();
        }

        private void schedulerControl1_AllowAppointmentConflicts(object sender, AppointmentConflictEventArgs e)
        {
            //AppointmentBaseCollection abc = ((SchedulerControl)sender).Storage.Appointments.Items;
            e.Conflicts.Clear();
            string labelDisplayName = schedulerControl1.Storage.Appointments.Labels[e.AppointmentClone.LabelId].DisplayName;
            Appointment apt = schedulerControl1.Storage.CreateAppointment(AppointmentType.Normal);
            if(labelDisplayName == "Deleted Label")
                e.Conflicts.Add(apt);
        }

        private void schedulerControl1_EditAppointmentFormShowing(object sender, AppointmentFormEventArgs e) {

            Appointment apt = e.Appointment;

            // Required to open the recurrence form via context menu.
            bool openRecurrenceForm = apt.IsRecurring &&
               schedulerStorage1.Appointments.IsNewAppointment(apt);

            // Create a custom form.
            MyAppointmentForm myForm = new MyAppointmentForm((SchedulerControl)sender,
               apt, openRecurrenceForm);

            // Required for skins support.
            myForm.LookAndFeel.ParentLookAndFeel = schedulerControl1.LookAndFeel;

            e.DialogResult = myForm.ShowDialog();
            schedulerControl1.Refresh();
            e.Handled = true;

        }

        private void schedulerControl1_PreparePopupMenu(object sender, PreparePopupMenuEventArgs e) {
            if (e.Menu.Id == SchedulerMenuItemId.AppointmentMenu) {

                SchedulerPopupMenu menu = e.Menu.GetPopupMenuById(SchedulerMenuItemId.LabelSubMenu, true);

                foreach (DXMenuItem item in menu.Items) {
                    if (item.Caption == "Deleted Label")
                        item.Visible = false;
                }                
            }
        }

    }
}