
----Floor mat type & Use    
    --Anti-Fatigue|Anti-Static|Carpet|Dissipative/Anti-Static|Entrance|Indoor|Indoor/Outdoor|Safety|Scraper
IF $SP-17785$ LIKE "Anti-Fatigue" 
THEN "Anti-fatigue mat provides maximum comfort when you stand on it" 

ELSE IF $SP-17785$ LIKE "Scraper" 
THEN "Protect your carpet or floor from accidental damage by using scraper mat" 

ELSE IF $SP-17785$ LIKE "Anti-Static" 
THEN "Anti-static mat designed to absorb static electricity" 

ELSE IF $SP-17785$ LIKE "Entrance" 
THEN "Entrance mat designed to help prevent dirt and moisure entering the home or office on footwear" 

ELSE IF $SP-17785$ LIKE "Floor mat" 
THEN "Floor mat offers stylish comfort and support at a great value" 

ELSE IF $SP-17785$ IS NOT NULL
THEN $SP-17785$_" mat offers stylish comfort and support at a great value" 

ELSE "@@";

----Dimensions (in Inches): L x W    

--SP-20400 - L
--SP-21044 - W

IF $SP-20400$ IS NOT NULL
    AND $SP-21044$ IS NOT NULL
        THEN "Dimensions: "_$SP-20400$_"""L x "_$SP-21044$_"""W" 
ELSE IF $SP-20400$ IS NOT NULL
        THEN "Dimensions: "_$SP-20400$_"""L" 
ELSE IF $SP-21044$ IS NOT NULL
        THEN "Dimensions: "_$SP-21044$_"""W" 
ELSE "@@";

----Product true color & floor mat shape        
    ---Rectangle|Square|Semi-Circle
    IF $SP-21073$ LIKE "Rectangle" 
    AND $SP-22967$ LIKE "%black%" 
        THEN $SP-22967$_" rectangular design is unobtrusive and masks dirt and foot prints" 
    ELSE IF $SP-21073$ IS NOT NULL
    AND $SP-22967$ IS NOT NULL
        THEN $SP-22967$_" "_$SP-21073$_" design" 
    ELSE "@@"; 

----Floor mat material & Backing material        

IF $SP-21074$ IS NOT NULL 
AND $SP-21077$ IS NOT NULL 
    THEN "Made of "_$SP-21074$_" with "_$SP-21077$_" backing" 
ELSE IF $SP-21074$ IS NOT NULL 
    THEN "Made of "_$SP-21074$
ELSE "@@";

----Floor mat shape    ---OOS?

----Suitable setting        
        --Indoor|Outdoor|Indoor and outdoor|Hot and cold environments    

IF $SP-23630$ LIKE "Indoor and outdoor" 
    THEN"Perfect for indoor and outdoor use"    
ELSE IF $SP-23630$ LIKE "Indoor" 
    THEN"Indoor mat designed to help prevent dirt and moisture" 
ELSE IF $SP-23630$ LIKE "Outdoor" 
    THEN"Outdoor mat that provides excellent moisture and dirt removal" 
ELSE "@@";

----NFSI Certification    ---OOS

----Post Consumer Content (%)        

----Recycled Content (%)        
        --Typically contains 50% or more recycled resin
IF $SP-21623$ IS NOT NULL
AND $SP-21623$>0
    THEN "Typically contains "_$SP-21623$_"% or more recycled material" 
ELSE "@@";
----Use for additional product and/or manufacturer information relevant to the customer buying decision        

IF A[7383].Where("grease-resistant").Values IS NOT NULL 
    THEN "Grease-resistant material of this mat keeps it looking like new";
IF A[7383].Where("tapered edges").Values IS NOT NULL 
    THEN "Tapered edges allow easier floor to mat transition";
IF A[7383].Where("rounded corners").Values IS NOT NULL 
    THEN "Rounded Corners prevent tripping";
    ----rounded corners help keep mat flat
IF A[7383].Where("gripper back").Values IS NOT NULL 
    THEN "Gripper backing reduces mat movement";
    --- Gripper back for use on carpets.
    --- gripper back for stability on carpeted flooring
    --- Gripper back to protect flooring from general wear
IF A[7383].Where("dirt trapping").Values IS NOT NULL 
    THEN "This mat stops the dirt, keeping your floors clean";

IF A[7383].Where("moisture-resistant").Values IS NOT NULL 
AND A[7383].Where("stain-resistant").Values IS NOT NULL
    THEN "Moisture- and stain-resistant to cut cleaning and maintenance time by half";

----Use for additional product and/or manufacturer information relevant to the customer buying decision

IF A[380].Values.Where("Greentrax") IS NOT NULL THEN
    "GreenTrax program recommended and approved as a part of ""green cleaning environments"" project";

----Warranty information        
    IF $SP-21932$ LIKE "%lifetime%" 
        THEN "Lifetime manufacturer limited warranty" 
    ELSE IF A[7241].Value IS NOT NULL 
        THEN A[7241].Value
            .Replace(" year","-year")
            .Replace(" days","-day")
            .Replace(" years", "-year")
            .Replace("-years", "-year")
            .Replace(" months", "-month")
            .Erase("limited")
            .Erase("manufacturer")
            .Replace("warranty", "manufacturer limited warranty")
            .ToUpperFirstChar()
    ELSE "@@";