using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*

 
TYPE
	Enum1 : 
		(
		Option1,
		Option2
		);
	Struct2 : 	STRUCT 
		usint : USINT;
	END_STRUCT;
	Struct1 : 	STRUCT 
		enum1 : Enum1;
		inner_struct : Struct2;
		float : REAL;
	END_STRUCT;
END_TYPE

 
 * 
 */

namespace Client2
{
    public enum Enum1 { Option1, Option2 };
	public class Struct2
	{
		public byte usint;
	}
    public class Struct1
    {
		public Enum1 enum1;
		public Struct2 inner_struct;
        public float Float;
    }
}
