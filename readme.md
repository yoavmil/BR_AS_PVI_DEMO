# B&R Controller <=> dotnet app

This is a minimal example of B&R PLC simlutor running, and a PC host app connecting to it, reading and writing variables. The B&R IDE is called Automation Studio, in short **AS**.

## The PLC side

The PLC has 2 global variables: `flag` and `gCounter`. In C++ means, the controller does this:

```c++
bool flag;
uint8_t gCounter;
while (true) {
    if (flag) gCounter++;
}
```

In AS the global variables are defined at the logical view > Global.var:

```reStructuredText
VAR
	gCounter : USINT;
	flag : BOOL := TRUE;
END_VAR
```

And the program is called **Program** which has the code file `Main.st`.

```reStructuredText
PROGRAM _INIT
	gCounter:=0;
	flag :=TRUE;
END_PROGRAM

PROGRAM _CYCLIC
	IF flag = TRUE THEN
		gCounter := gCounter + 1;		
	END_IF	 
END_PROGRAM

PROGRAM _EXIT
	(* Insert code here *)
	 
END_PROGRAM
```

The code needs to compile and transfared to the simulator.

## The Host Side

The connection between a PLC and a dotnet PC app is by using `BR.AN.PviServices.dll`.  PVI stands for **P**rocess **V**isualization **I**nterface.

> Important note: it's supported only by dotnet framework < 5.0 and not by dotnet core.

The app connects to the PLC by 2 stages:

1. connecting a `Service`

2. connecting a `Cpu`

After ther'e connected, we can connect to the global variables `flag` and `gCounter` by using `Variable`. The connection is based on the variables names, which is unsafe.




















