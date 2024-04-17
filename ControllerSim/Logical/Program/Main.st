
PROGRAM _INIT
	(* Insert code here *)
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

