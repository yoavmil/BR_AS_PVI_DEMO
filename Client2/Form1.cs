using BR.AN.PviServices;
using System;
using System.CodeDom;
using System.Windows.Forms;

namespace Client2
{
    public partial class Form1 : Form
    {
        private Service service;
        private Cpu cpu;

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

                PviUtils.ReadGlobalVariables(cpu);

                Console.Write(PviUtils.GetGlobalVariabls());
                //cpu.Variables.Uploaded += Variables_Uploaded;
                //cpu.Variables.Upload();
            }
            else
            {
                statusLbl.Text = $"cpu not connected: {e.ErrorText}";
            }
        }

        private void Variables_Uploaded(object sender, PviEventArgs e)
        {
            // parse variables
            if (e.ErrorCode == 0)
            {
                foreach (Variable var in this.cpu.Variables.Values)
                {
                    //MemberCollection members = var.Members;
                    //if (members!=null)
                    //foreach (var member in members)
                    //{
                    //    Console.WriteLine($"{var.FullName}.{member.ToString()}");
                    //}
                    var v = AddGlobalVariable(var.Name);
                    v.Connected += GlovalVariable_Connected;
                    v.Connect();
                }
            }
            else
            {
                statusLbl.Text = "failed to upload variables";
            }
        }

        private void GlovalVariable_Connected(object sender, PviEventArgs e)
        {
            var v = (Variable)sender;
            var typeName = v.IECDataType.ToString();
            if (v.IECDataType == IECDataTypes.STRUCT)
            {
                Console.Write(PviUtils.ReadTypes(v));
                PviUtils.PrintEnums();
            }
            
            Console.WriteLine($"{v.Name} : {typeName}");
        }

        Variable AddGlobalVariable(string name)
        {
            if (cpu.Variables.ContainsKey(name) && cpu.Variables[name] != null)
                return cpu.Variables[name];
            return new Variable(cpu, name);
        }

        private void conectVarBtn_Click(object sender, EventArgs e)
        {
            string code = "";
            code += PviUtils.GetEnumDeclerations();
            code += PviUtils.GetStructDeclerations();
            code += PviUtils.GetGlobalVariabls();
            Console.Write(code);
        }
    }
}
