using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace FileMonitor
{
    public partial class Form1 : Form
    {
        private static System.Timers.Timer updateTimer;
        public Form1()
        {
            InitializeComponent();
            updateTimer = new System.Timers.Timer(1000);
            updateTimer.Elapsed += new ElapsedEventHandler(handleUpdate);
        }

        private void handleUpdate(Object sender, ElapsedEventArgs args)
        {
            Dictionary<string, UInt32> list = Program.GetFullList();
            List<ListViewItem> items = new List<ListViewItem>();

            // Convert list to listview
            foreach (KeyValuePair<string, UInt32> pair in list)
            {
                ListViewItem item = new ListViewItem(pair.Value.ToString());
                ListViewItem.ListViewSubItem pathItem = new ListViewItem.ListViewSubItem();
                pathItem.Text = pair.Key;
                item.SubItems.Add(pathItem);
                items.Add(item);
            }

            items.Sort((x, y) => UInt32.Parse(y.Text).CompareTo(UInt32.Parse(x.Text)));

            if (listViewActivity.InvokeRequired)
            {
                listViewActivity.BeginInvoke((MethodInvoker)delegate
                {
                    listViewActivity.BeginUpdate();
                    listViewActivity.Items.Clear();
                    foreach (ListViewItem item in items)
                    {
                        ListViewItem current = null; // listViewActivity.FindItemWithText(item.SubItems[0].Text);
                        if (current != null)
                        {
                            current.Text = item.Text;
                        }
                        else
                        {
                            listViewActivity.Items.Add(item);
                        }
                    }
                    listViewActivity.EndUpdate();
                });
            }
            else
            {
                listViewActivity.Items.Clear();
                foreach (ListViewItem item in items)
                {
                    listViewActivity.Items.Add(item);
                }
            }
        }

        private void textBoxPath_DoubleClick(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                textBoxPath.Text = fbd.SelectedPath;
            }
        }

        bool monitoring = false;
        private void button_Click(object sender, EventArgs e)
        {
            if (!monitoring)
            {
                monitoring = true;
                string pathToMonitor = textBoxPath.Text;
                Program.CreateFSWatcher(pathToMonitor);
                button.Text = "Stop";
                textBoxPath.Enabled = false;
                updateTimer.Start();
            }
            else
            {
                // Stop!
                monitoring = false;
                Program.StopFSWatcher();
                button.Text = "Monitor";
                textBoxPath.Enabled = true;
                updateTimer.Stop();
            }
        }
    }
}
