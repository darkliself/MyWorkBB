--              Bullet 1
----Pencil sharpener type & Use
    --SP-22593
    --Electric|Manual|Battery Powered

IF $SP-22593$ LIKE "Electric" 
    THEN "Electric pencil sharpener provides clean, precise sharpening" 
ELSE IF $SP-22593$ LIKE "Manual" 
    THEN "Manual pencil sharpener is quick and convenient for anywhere use" 
ELSE IF $SP-22593$ LIKE "Battery Powered" 
    THEN "Battery-powered pencil sharpener is ideal for high-quality clean line sharpening" 
ELSE "@@";

--              Bullet 2
----Workload    
    ---SP-23550
    ---Heavy-duty|Light-duty
IF $SP-23550$ LIKE "Light-duty" 
    THEN "Pencil sharpener is ideal for light-duty pencil sharpening" 
ELSE IF $SP-23550$ LIKE "Heavy-duty" 
    THEN "Heavy-duty versatile sharpener" 
ELSE IF $SP-23550$ LIKE "%Medium%Duty%" 
    THEN "Pencil sharpener usage type: Medium-duty" 
ELSE "@@";

--              Bullet 3
----Material of item & Product True color    
        --SP-22967 - Product True Color
        --SP-21408 - Material of Item

IF $SP-21408$ IS NOT NULL 
    AND $SP-22967$ IS NOT NULL
        THEN $SP-22967$_" sharpener is made of "_$SP-21408$_" and is ideal for pencils" 
ELSE IF $SP-21408$ IS NOT NULL 
        THEN "Made of "_$SP-21408$_" to help you achieve a sharp pencil tip" 
ELSE IF $SP-22967$ LIKE "available in different colors" OR  $SP-22967$ LIKE "assorted" 
        THEN "This item comes in assorted colors" 
ELSE IF $SP-22967$ IS NOT NULL 
        THEN "Comes in "_$SP-22967$
ELSE "@@";

--              Bullet 4
----Pencil sharpener mount type    
        --SP-16315
        --Desktop|Wall|Handheld

IF $SP-16315$ LIKE "Desktop" 
    THEN "Desktop pencil sharpener for your convenience" 
    --Сomes in desktop design for more convenience. 
    --Desktop pencil sharpening for your convenience.
ELSE IF $SP-16315$ LIKE "Wall" 
    THEN "Wall-mountable pencil sharpener"    
ELSE IF $SP-16315$ LIKE "Handheld" 
    THEN "Handheld pencil sharpener" 
ELSE "@@";

--              Bullet 5
--Pencil sharpener selector dial

--The Staples® Single-Hole Manual Pencil Sharpener is quick and convenient for use anywhere!
--https://www.staples.com/product_211672

IF $SP-18669$ LIKE "1%" 
    THEN "One-hole pencil sharpener is quick and convenient for use anywhere    " 

--6-hole dial accommodates various pencil widths 
--https://www.staples.com/product_356294

ELSE IF $SP-18669$ IN ("2%", "3%", "4%")
    THEN REF["SP-18669"].Replace(" holes", "-hole")_" dial accommodates various pencil widths" 

--Features a six-hole dial which conveniently sharpens assorted pencil widths
--https://www.staples.com/product_356294

ELSE IF $SP-18669$ IS NOT NULL
    THEN "Features a "_REF["SP-18669"].Replace(" holes", "-hole")_" dial which conveniently sharpens assorted pencil widths" 
ELSE "@@";

--              Bullet 6
--Pencil sharpener auto-shut off feature (If Applicable)    

--Auto shut off when pencil is completely sharpened 
--https://www.staples.com/product_185006

IF $SP-16316$ LIKE "Yes" 
    THEN "Auto shut off when pencil is completely sharpened" 
ELSE "@@";

--              Bullet 7
----Pack Size (If more than 1)    

IF $SP-12609$ NOT LIKE "each" 
    THEN "Available in a "_Request.Data["TX_UOM"].RegexReplace("(\S+)[\/](\S+)", "$2 of $1")
ELSE "@@";

--              Bullet 9
--Use for additional product and/or manufacturer information relevant to the customer buying decision

--Features antimicrobial protection for the life of the product 
--https://www.staples.com/product_891795

IF A[6189].Where("%antimicrobial%").Values IS NOT NULL
    THEN "Features antimicrobial protection for the life of the product";

--Hardened helical cutter for maximum precision and durability 
--https://www.staples.com/product_264898

IF A[6189].Where("%hardened helical cutter%").Values IS NOT NULL
    THEN "Features antimicrobial protection for the life of the product";

--Pencil saver technology prevents over-sharpening 
--https://www.staples.com/product_116392

IF A[6189].Where("%PencilSaver%").Values IS NOT NULL
    THEN "Pencil saver technology prevents oversharpening";

--Overheat protection prevents motor from overheating 
--https://www.staples.com/product_356332

IF A[6189].Where("overheat protection").Values IS NOT NULL
    THEN "Overheat protection prevents motor from overheating";

--SafeStart system prevents sharpening until receptacle is in place 
--https://www.staples.com/product_810743

IF A[6189].Where("SafeStart").Values IS NOT NULL
    THEN "SafeStart system prevents sharpening until receptacle is in place";

--              Bullet 
--Warranty Information (If Applicable)    

IF COALESCE(A[3574].Value, A[7241].Values) LIKE "limited lifetime warranty" 
THEN "Lifetime manufacturer limited warranty" 
ELSE IF COALESCE(A[3574].Value, A[7241].Values) IS NOT NULL 
    THEN COALESCE(A[3574].Value.ToLower(true).Replace(" years","-year").Replace(" months", "-month").Replace(" days","-day").Replace(" year","-year").Replace(" month", "-month").Replace(" day","-day").Erase(" warranty")
.Erase(" warranty"),
    A[7241].Values.ToLower(true).Replace(" years","-year").Replace(" months", "-month").Replace(" days","-day").Replace(" year","-year").Replace(" month", "-month").Replace(" day","-day").Erase(" warranty").Erase(" warranty"))
    _" manufacturer limited warranty" 
ELSE "@@";