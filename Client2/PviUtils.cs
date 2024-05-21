using BR.AN.PviServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Client2
{
    public static class PviUtils
    {
        /*
         * Upload all the global varibals from the PLC and register their types and enum,
         * prepare the data for generating a C# like code that reflects them
         */
        public static void ReadGlobalVariables(Cpu cpu)
        {
            PviUtils.cpu = cpu;
            cpu.Variables.Uploaded += Variables_Uploaded;
            cpu.Variables.Upload();
        }

        static private Cpu cpu;
        private static void Variables_Uploaded(object sender, PviEventArgs e)
        {
            VariableCollection variables = (VariableCollection)sender;
            // parse variables
            if (e.ErrorCode == 0)
            {
                foreach (Variable var in variables.Values)
                {
                    var v = AddGlobalVariable(cpu, var.Name);
                    v.Connected += Variable_Connected;
                    v.Connect();
                }
            }
        }

        private static void Variable_Connected(object sender, PviEventArgs e)
        {
            var v = sender as Variable;
            if (v.IECDataType == IECDataTypes.STRUCT)
            {
                v.Uploaded += Struct_Uploaded;
                v.Upload();

                var typeName = v.StructName;
                foreach (Variable member in v.Members.Values)
                {
                    member.Connected += Member_Connected;
                    member.Connect();

                    structs[typeName].Add(member);
                }
            }
            else if (v.Value.IsEnum == 1)
            {
                globals.Add($"{v.Value.Enumerations.Name} {v.Name}");
                enums.Add(v.Value.Enumerations);
            }
            else
            {
                globals.Add($"{v.IECDataType.ToString()} {v.Name}");
            }
        }

        private static void Struct_Uploaded(object sender, PviEventArgs e)
        {
            Console.WriteLine($"{(sender as Variable).Name} struct uploaded");
            Variable variable = sender as Variable;
            var typeName = variable.StructName;
            globals.Add($"{typeName} {variable.Name}");
            structs[typeName] = new List<Variable>();
            foreach (Variable member in variable.Members.Values)
            {
                member.Connected += Member_Connected;
                member.Connect();

                structs[typeName].Add(member);
            }
        }

        private static void Member_Uploaded(object sender, PviEventArgs e)
        {
            Console.WriteLine($"{(sender as Variable).Name} member uploaded");
        }

        private static void Member_Connected(object sender, PviEventArgs e)
        {
            Variable member = sender as Variable;
            if (member.Value.IsEnum == 1)
            {
                enums.Add(member.Value.Enumerations);
            }
            else if (member.IECDataType == IECDataTypes.STRUCT)
            {
                var typeName = member.StructName;
                structs[typeName] = new List<Variable>();
                foreach (Variable innerMember in member.Members.Values)
                {
                    innerMember.Connected += Member_Connected;
                    innerMember.Connect();
                    structs[typeName].Add(innerMember);
                }
            }
        }

        private static Variable AddGlobalVariable(Cpu cpu, string name)
        {
            if (cpu.Variables.ContainsKey(name) && cpu.Variables[name] != null)
                return cpu.Variables[name];
            return new Variable(cpu, name);
        }

        public static string GetEnumDeclerations()
        {
            string str = "";
            foreach (var e in enums)
            {
                str += $"enum {e.Name} {{\n";
                for (var i = 0; i < e.Count; i++)
                {
                    str += $"\t{e.Names[i]} = {e.Values[i]},\n";
                }

                str += "}\n";
            }
            return str;
        }

        public static string GetStructDeclerations()
        {
            string result = "";

            foreach (var s in structs)
            {
                var strcutName = s.Key;
                
                result += $"class {strcutName} {{\n";
                foreach (var m in s.Value)
                {
                    result += $"\t{m.IECDataType} {m.Name};\n";
                }
                result += "}\n" ;
            }

            return result;
        }

        public static string GetGlobalVariabls()
        {
            string result = "";
            foreach (var g in globals)
            {
                result += $"{g};\n";
            }
            return result;
        }

        private static List<string> globals = new List<string>();
        private static Dictionary<string, List<Variable>> structs = new Dictionary<string, List<Variable>>();

        public static string ReadTypes(Variable variable)
        {
            if (variable == null) throw new ArgumentNullException("variable is null");
            if (!variable.IsConnected) throw new ArgumentException("variable is not connected");
            if (variable.IECDataType != IECDataTypes.STRUCT) throw new ArgumentException("variable isn't a struct");

            var typeName = variable.StructName;
            string str = "";
            str += $"class {typeName} {{\n";
            foreach (Variable member in variable.Members.Values)
            {
                member.Connect();
         
                if (member.Value.IsEnum == 1)
                {
                    enums.Add(member.Value.Enumerations);
                }
                str += $"\t{member.ToString()} {member.Name};\n";
            }

            str += "}\n";

            return "";
        }

        static List<EnumArray> enums = new List<EnumArray>();

        static public void PrintEnums()
        {
            string str ="";
            foreach (var e in enums)
            {
                str += $"enum {e.Name} {{\n";
                for (var i = 0; i < e.Count; i++)
                {
                    str += $"\t{e.Names[i]} = {e.Values[i]},\n";
                }

                str += "}\n";
            }

            Console.Write(str);
        }
    }
}
