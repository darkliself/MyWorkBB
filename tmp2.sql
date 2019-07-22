-----Disposable plate type  
--Plates
IF $SP-22650$ LIKE "Plates" 
    THEN "Plates will save you from the hassle of washing dishes" 
--Trays
ELSE IF $SP-22650$ LIKE "trays" 
    THEN "Tray is the best serving tray for desserts" 
--Platters
ELSE IF $SP-22650$ LIKE "platters" 
    THEN "Enhance the food presentation at your next celebration with platters" 
--Lids
ELSE IF $SP-22650$ LIKE "Lids" 
    THEN "Lid ensures that spillage of food is prevented by fitting your dinnerware completely" 
ELSE "@@";
--Sets

-----Color family & Material    
IF $SP-22967$ LIKE "%assorted%" 
    THEN NULL
ELSE IF $SP-22967$ LIKE "%gray%" 
AND $SP-17447$ IS NOT NULL 
    THEN "Comes in gray and made of "_$SP-17447$
ELSE IF $SP-22967$ LIKE "%Multicolor%" 
AND $SP-17447$ IS NOT NULL 
    THEN "Multicolor "_$SP-23526$_" made of "_$SP-17447$
ELSE IF $SP-22967$ NOT IN (NULL, "Clear")
AND $SP-17447$ IS NOT NULL 
    THEN "Comes in "_$SP-22967$_" and made of "_$SP-17447$
ELSE IF $SP-22967$ IN ("Clear")
AND $SP-17447$ IS NOT NULL 
    THEN "Made of "_$SP-22967$_" "_$SP-17447$
ELSE IF $SP-22967$ IS NOT NULL
THEN "Color: "_$SP-22967$
ELSE IF $SP-17447$ IS NOT NULL
THEN "Material: "_$SP-17447$
ELSE "@@";

-----Capacity (oz.) 
IF A[6803].UnitUSM LIKE "%oz%" 
    THEN A[6803].ValueUSM.Prefix("Capacity: ").Postfix(" oz.")

ELSE IF A[6803].Unit LIKE "ml" 
    THEN A[6803].Value.MultiplyBy("0.033814").ToText("F2").RegexReplace("(?:(\.\d*?[1-9]+)|\.)0*$", "$1").Prefix("Capacity: ").Postfix(" oz.")
ELSE IF A[6803].Unit LIKE "l" 
    THEN A[6803].Value.MultiplyBy("33.814").ToText("F2").RegexReplace("(?:(\.\d*?[1-9]+)|\.)0*$", "$1").Prefix("Capacity: ").Postfix(" oz.")
ELSE IF A[6803].Unit LIKE "cl" 
    THEN A[6803].Value.MultiplyBy("0.33814").ToText("F2").RegexReplace("(?:(\.\d*?[1-9]+)|\.)0*$", "$1").Prefix("Capacity: ").Postfix(" oz.")
ELSE "@@";

-----Microwaveability   

IF A[10039].Where("Yes").Values IS NOT NULL 
    THEN "Microwave-safe for reheating" 
ELSE "@@";

-----Strength   

-------------oos

-----Number of disposable plates    

IF $SP-467$ IS NOT NULL THEN "This case of "_$SP-467$_" provides multi-event use" 
ELSE "@@";

-----Disposables pack size  

-----Specify if sectioned   
IF A[8740].Value IS NOT NULL THEN "Plate features "_A[8740].Value_" separate divided sections" 
ELSE "@@";

-----Use for additional product and/or manufacturer information relevant to the customer buying decision    
IF COALESCE ($SP-21044$,$SP-21453$) IS NOT NULL
    THEN "Dimensions: "_COALESCE ($SP-21044$,$SP-21453$)_"""";

-----Use for additional product and/or manufacturer information relevant to the customer buying decision    
IF A[7832].Value IS NOT NULL THEN "Freezer safe";

-----Use for additional product and/or manufacturer information relevant to the customer buying decision    

-----Use for additional product and/or manufacturer information relevant to the customer buying decision