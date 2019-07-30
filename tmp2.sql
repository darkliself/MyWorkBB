-- Paper Trimmers

-- Paper trimmer type & Use (1)

-- All paper crafting enthusiasts know a rotary trimmer is a must-have (https://www.staples.com/product_942162)
IF $SP-14421$ IN ("guillotine", "rotary") 
    THEN "All paper crafting enthusiasts know that "_$SP-14421$_" trimmer is a must-have" 
ELSE IF $SP-14421$ LIKE "Cutting Mats" 
    THEN "All paper crafting enthusiasts know that cutting mat is a must-have" 

ELSE IF $SP-14421$ LIKE "Replacement Blades" 
    THEN "All paper crafting enthusiasts know that "_$SP-14421$_" are must-have" 

ELSE IF $SP-14421$ IS NOT NULL
    THEN "All paper crafting enthusiasts know that "_$SP-14421$_" is a must-have" 
ELSE "@@";

-- Paper trimmer size (inches) (2)

IF $SP-20654$ IS NOT NULL -- H
AND $SP-21044$ IS NOT NULL -- W
AND $SP-20657$ IS NOT NULL -- D
    THEN "Paper trimmer size: "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 

ELSE IF $SP-20654$ IS NOT NULL -- H
AND $SP-21044$ IS NOT NULL -- W
AND A[7520].Value Like "cutter cutting mat" 
    THEN "Cutting mat size: "_$SP-20654$_"""H x "_$SP-21044$_"""W" 

ELSE IF $SP-20654$ IS NOT NULL -- H
AND $SP-21044$ IS NOT NULL -- W
    THEN "Paper trimmer size: "_$SP-20654$_"""H x "_$SP-21044$_"""W" 

ELSE IF $SP-21044$ IS NOT NULL -- W
AND $SP-20657$ IS NOT NULL -- D
    THEN "Base size: "_$SP-21044$_"""W x "_$SP-20657$_"""D" 

ELSE IF $SP-20654$ IS NOT NULL -- H
AND $SP-21044$ IS NOT NULL -- W
AND A[7520].Value Like "cutter cutting mat" 
    THEN "Cutting mat size: "_$SP-20654$_"""H x "_$SP-21044$_"""W" 
ELSE "@@";

-- Maximum Paper Size (3)

-- Cuts up to 15 sheets of paper at a time (https://www.staples.com/product_103439)
IF A[4312].Value IS NOT NULL
    THEN "Cuts up to "_A[4312].Value_" sheets of paper at a time" 
ELSE "@@";

-- Blade length & Material (4)

-- Features stainless steel blades (https://www.staples.com/product_829909)
IF $SP-21137$ IS NOT NULL 
AND $SP-12951$ IS NOT NULL 
AND $SP-12951$ LIKE "%tripleTrack%" 
THEN $SP-21137$_""" "_REF["$SP-12951$"].Prefix("##").Replace(" ", " ##")_" blade" 
ELSE IF $SP-12951$ IS NOT NULL 
AND $SP-21137$ IS NOT NULL 
THEN $SP-21137$_""" "_$SP-12951$_" blade" 

ELSE IF $SP-21137$ IS NOT NULL 
THEN "Blade length: "_$SP-21137$_"""" 

ELSE IF $SP-12951$ IS NOT NULL 
THEN "Blade is made of "_$SP-12951$ 
ELSE "@@"; 

-- Features (Grids/Locks/Guards) (5)

IF A[4315].Values.Where("safety interlock") IS NOT NULL 
    THEN "Safety interlock secures blade when not in use" 

ELSE IF A[4315].Values.Where("automatic blade locking system") IS NOT NULL
AND A[6703].Value LIKE "Yes" 
    THEN "Safety features include finger guard ##and automatic blade locking system" 

ELSE IF A[4315].Values.Where("automatic blade locking system") IS NOT NULL
    THEN "Features automatic blade locking system" 
ELSE "@@";

-- True color (6)

IF $SP-22967$ NOT IN (NULL, "Assorted", "Multicolor")
    THEN "Comes in "_$SP-22967$

ELSE IF $SP-22967$ LIKE "Assorted" 
    THEN "Comes in assorted colors" 

ELSE IF $SP-22967$ LIKE "Multicolor" 
    THEN "Multicolor "_SKU.ProductType
ELSE "@@";

-- Pack Size (If more than 1) (7)

IF Request.Data["TX_UOM"] IS NOT NULL
AND Request.Data["TX_UOM"] Not Like "Each" 
    THEN "Contains "_Request.Data["TX_UOM"].Replace("/", " trimmers per ")
ELSE "@@";

-- Use for additional (8-12)

-- self-sharpening system keeps blade sharp (https://www.staples.com/product_264899)
IF A[4315].Values.Where("self-sharpening blade") IS NOT NULL
    THEN "Self-sharpening system keeps blade sharp";

IF A[4315].Values.Where("titanium coated blades") IS NOT NULL
    THEN "Titanium-bonded blades stay sharper longer";

IF A[4315].Values.Where("Auto Clamp system") IS NOT NULL
    THEN "Automatic clamp holds work securely and prevents shifting";

-- Used for cutting photos and small crafts  (https://www.staples.com/product_ACM15191)   
IF A[4309].Values IS NOT NULL
    THEN "Used for cutting "_A[4309].Values.FlattenWithAnd();

IF A[4315].Values.WhereNot("safety interlock", "self-sharpening blade", "automatic blade locking system", "titanium coated blades", "Auto Clamp system") IS NOT NULL
    THEN "Features "_A[4315].Values.WhereNot("safety interlock", "self-sharpening blade", "automatic blade locking system", "titanium coated blades", "Auto Clamp system").Prefix("##").Replace(" "," ##").Replace("##TripleTrack ##System", "##TripleTrack system").Replace("##Soft-touch ##handle", "soft-touch handle").FlattenWithAnd();

IF A[6628].Value > 0
    THEN "Made from "_A[6628].Value_"% recycled material";

IF A[3574].Value Like "%lifetime%" 
    THEN "Lifetime manufacturer limited warranty" 
ELSE IF A[3574].Value Like "%year%" 
    THEN A[3574].Value.Erase("Warranty").Erase("limited").ExtractDecimals().Postfix("-year manufacturer limited warranty");