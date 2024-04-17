using BR.AN.PviServices;
using System;
using System.Windows.Forms;

namespace Client2
{
    public partial class Form1 : Form
    {
        private Service service;
        private Cpu cpu;
        private Variable flag, counter;

        public Form1()
        {
            InitializeComponent();
        }

        private void connectControllerBtn_Click(object sender, EventArgs e)
        {
            service = new Service("service");
            service.Connected += Service_Connected;
            service.Connect();
        }

        private void Service_Connected(object sender, PviEventArgs e)
        {
            if (e.ErrorCode == 0)
            {
                statusLbl.Text = "Service connected";
                cpu = new Cpu(service, "cpu");
                cpu.Connected += Cpu_Connected;
                cpu.Connect();
            }
            else
            {
                statusLbl.Text = "Service not connected";
            }
        }

        private void Cpu_Connected(object sender, PviEventArgs e)
        {
            if (e.ErrorCode == 0)
            {
                statusLbl.Text = "cpu connected";
                connectVarBtn.Enabled = true;
            }
            else
            {
                statusLbl.Text = "cpu not connected";
            }
        }

        private void conectVarBtn_Click(object sender, EventArgs e)
        {
            if (flag == null)
            {
                flag = new Variable(cpu, "flag");
                flag.Connected += flagVariable_Connected;
                flag.Connect();

                counter = new Variable(cpu, "gCounter");
                counter.Connected += counterVariable_Connected;
                counter.Connect();
            }
            else
            {
                flag.Value = !flag.Value;
            }
        }

        private void flagVariable_Connected(object sender, PviEventArgs e)
        {
            if (e.ErrorCode == 0)
            {
                statusLbl.Text = "Variable connected: " + flag.Value.ToString();
                connectVarBtn.Text = "Toggle";
                flag.Active = true;
                flag.ValueChanged += Variable_ValueChanged;
            }
            else
            {
                statusLbl.Text = "Flag not connected";
            }
        }

        private void counterVariable_Connected(object sender, PviEventArgs e)
        {
            if (e.ErrorCode == 0)
            {
                counter.Active = true;
                counter.ValueChanged += Counter_ValueChanged;
            }
            else
            {
                statusLbl.Text = "Counter not connected";
            }
        }

        private void Counter_ValueChanged(object sender, VariableEventArgs e)
        {
            counterLbl.Text = counter.Value.ToString();
        }

        private void Variable_ValueChanged(object sender, VariableEventArgs e)
        {
            statusLbl.Text = "Flag changed: " + flag.Value.ToString();
        }
    }
}
