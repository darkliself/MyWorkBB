--Broom/dustpan type & Use    
IF $SP-23349$ LIKE "Handle Braces" 
    THEN "Handle brace lends an added support to prevent handles from breaking under strenuous conditions" 
ELSE IF $SP-23349$ LIKE "Broom Heads" 
    THEN "Broom head is designed to remove dirt and debris" 
ELSE IF $SP-23349$ LIKE "Push Brooms" 
    THEN "Push broom offers a quick and efficient cleaning solution" 
ELSE IF $SP-23349$ LIKE "Kits" 
    THEN "Kit offers a quick and efficient cleaning solution" 
ELSE IF $SP-23349$ LIKE "Angled Brooms" 
    THEN "Angled broom is designed to reach into the tightest corners very easily" 
ELSE IF $SP-23349$ LIKE "Brooms w/Dustpan" 
    THEN "Broom and dustpan set is great for all on-the-move applications for sweeping up and carrying debris" 
ELSE IF $SP-23349$ LIKE "Corn Brooms" 
    THEN "Corn broom traps dust and fine dirt with ease" 
ELSE IF $SP-23349$ LIKE "Broom Handles" 
    THEN "Broom handle enables smooth, efficient and swift cleaning" 
ELSE IF $SP-23349$ LIKE "Standard Brooms" 
    THEN "Standard broom for easy and effective cleaning" 
ELSE IF $SP-23349$ LIKE "Dustpan" 
    THEN "Dustpan for efficient pick-up of dust and dirt" 
ELSE IF $SP-23349$ LIKE "%tool holder%" 
    THEN $SP-23349$_" provides a convenient storage solution" 
ELSE IF $SP-23349$ IS NOT NULL 
    THEN $SP-23349$_" for easy and effective cleaning" 
ELSE "@@";

--Handle Information (Length and Material)
IF $SP-23349$ LIKE "%handle%" AND $SP-20400$ IS NOT NULL AND $SP-21026$ IS NOT NULL
    THEN "Handle features "_$SP-20400$_""" length and made of "_$SP-21026$_" for durability" 
ELSE IF $SP-23349$ LIKE "%handle%" AND $SP-21026$ LIKE "%plastic%" 
    THEN "Handle features durable plastic construction" 
ELSE IF $SP-23349$ LIKE "%handle%" AND $SP-21026$ IS NOT NULL
    THEN "Handle made of "_$SP-21026$_" material for durability" 
ELSE IF $SP-23349$ LIKE "%handle%" AND $SP-20400$ IS NOT NULL
    THEN "It measures"_$SP-20400$_""" in length, allowing easy access in hard-to-reach areas" 
ELSE "@@";

--Head Details (material and Size)
IF $SP-23349$ LIKE "%Head%" AND $SP-20654$ IS NOT NULL
AND $SP-21044$ IS NOT NULL AND $SP-20657$ IS NOT NULL AND $SP-21026$ IS NOT NULL
    THEN "Head size is "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D and make from "_$SP-21026$_" material" 
ELSE IF $SP-23349$ LIKE "%Head%" AND $SP-21044$ IS NOT NULL AND $SP-21026$ IS NOT NULL
    THEN "Width head size is "_$SP-21044$_""" and made of "_$SP-21026$_" for durability" 
ELSE IF $SP-23349$ LIKE "%Head%" AND $SP-20654$ IS NOT NULL
AND $SP-21044$ IS NOT NULL AND $SP-20657$ IS NOT NULL
    THEN "Head size is "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 
ELSE IF $SP-23349$ LIKE "%Head%" AND $SP-21044$ IS NOT NULL
    THEN "It measures "_$SP-21044$_""" width for effective cleaning" 
ELSE IF $SP-23349$ LIKE "%Head%" AND $SP-21026$ LIKE "%plastic%" 
    THEN "Head have durable plastic construction" 
ELSE IF $SP-23349$ LIKE "%Head%" AND $SP-21026$ IS NOT NULL
    THEN "Head made of "_$SP-21026$_" for durability" 
ELSE "@@";

--Dimensions (in Inches): L x W
IF $SP-23349$ NOT LIKE "%Head%" AND $SP-23349$ NOT LIKE "%handle%" 
AND $SP-20400$ IS NOT NULL AND $SP-21044$ IS NOT NULL
THEN "Dimensions: "_$SP-20400$_"""L x"_$SP-21044$_"""W" 
ELSE IF $SP-23349$ NOT LIKE "%Head%" AND $SP-23349$ NOT LIKE "%handle%" 
AND $SP-20654$ IS NOT NULL
 AND $SP-21044$ IS NOT NULL AND $SP-20657$ IS NOT NULL
THEN "Dimensions: "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 
ELSE IF $SP-23349$ NOT LIKE "%Head%" AND $SP-23349$ NOT LIKE "%handle%" 
AND $SP-20400$ IS NOT NULL 
THEN "It measures "_$SP-20400$_""" length for effective cleaning" 
ELSE "@@";

--Pack Size (If more than 1)
IF Request.Data["TX_UOM"] LIKE "%/Each" 
    THEN "Sold as "_Request.Data["TX_UOM"].ExtractDecimals().First()_" individualy packed" 
ELSE IF Request.Data["TX_UOM"] NOT LIKE "Each" 
    THEN "Sold as "_Request.Data["TX_UOM"].ExtractDecimals().First()_" per pack" 
ELSE "@@";

--Use for additional product and/or manufacturer information relevant to the customer buying decision

--material
IF $SP-23349$ NOT LIKE "%Head%" AND $SP-23349$ NOT LIKE "%handle%" 
AND $SP-21026$ IS NOT NULL AND $SP-21026$ NOT IN ("Corn Bristle")
    THEN "Made of "_$SP-21026$ _" for durability";

--color
IF $SP-21278$ IS NOT NULL AND $SP-21278$ NOT IN ("Assorted", "Multicolor", NULL, "clear")
    THEN "Comes in "_$SP-21278$
ELSE IF $SP-22967$ NOT IN ("Assorted", "Multicolor", NULL, "clear")
    THEN "Comes in "_$SP-22967$;

IF $SP-23349$ LIKE "Brooms w/Dustpan" 
AND A[6786].Values.Where("broom holder") IS NOT NULL
    THEN "Broom easily snaps into and stores inside the pan";

--hanger hole
IF A[6786].Values.Where("hanger hole", "hanging hole") IS NOT NULL 
    THEN A[6786].Values.Where("hanger hole", "hanging hole").Erase(" hole").FlattenWithAnd().ToUpperFirstChar()
    _" hole for easy storage" 
--wall mountable 
ELSE IF A[6786].Values.Where("wall mountable") IS NOT NULL 
    THEN "The hanging design helps prevent damage to bristles, handles, walls, and counters";

--molded riges
IF A[6786].Values.Where("molded ridges") IS NOT NULL 
OR DC.Ksp.Values.Where("%molded ridge%") IS NOT NULL
    THEN "Molded ridges for broom and counter brush cleaning" 
ELSE IF A[6786].Values.Where("molded teeth") IS NOT NULL 
OR DC.Ksp.Values.Where("%molded teeth%") IS NOT NULL 
THEN "Molded teeth to help clean bristles";

--Cleaning usage
IF $SP-15361$ IS NOT NULL 
    THEN "Perfect for "_$SP-15361$;

--adjustable handle
IF A[6786].Values.Where("Adjustable handle") IS NOT NULL 
    THEN "Adjustable handle enhances user comfort" 
ELSE IF A[6786].Values.Where("Ergonomic handle") IS NOT NULL 
    THEN "Ergonomic handle to prevent hand fatigue"; 

IF A[6784].Value LIKE "Yes" 
OR DC.Ksp.Values.Where("%Telescop%handle%") IS NOT NULL 
OR DC.MarketingText.Values.Where("%Telescop%handle%") IS NOT NULL THEN "Telescoping handle makes it easy to use";

--comfort grip
IF A[6786].Values.Where("comfort grip%") IS NOT NULL 
    THEN "Comfort grip for added convenience" 
ELSE IF A[6786].Values.Where("% grip", "% grip %") IS NOT NULL 
    THEN A[6786].Values.Where("% grip", "% grip %").First().Erase(" handle").ToUpperFirstChar() 
_" for added convenience";

IF A[6786].Values.Where("color-coded") IS NOT NULL 
    THEN "Color-coded feature makes it ideal for assigning it to a specific work area"; 

IF A[6786].Values.Where("% thread") IS NOT NULL 
    THEN A[6786].Values.Where("% thread").FlattenWithAnd().ToUpperFirstChar()
    _" for easy and secure attachment"; 

IF A[7091].Values.Where("wet", "dry").Count = 2 
    THEN "Can be used in dry or wet conditions";

--heavy-duty    
IF A[6786].Values.Where("heavy-duty") IS NOT NULL 
    THEN "Heavy-duty for tough cleaning jobs";

--bristles    
IF A[6786].Where("%angled bristle%").Values IS NOT NULL AND $SP-23349$ NOT LIKE "%angled%" 
    THEN "Angled bristle are great for sweeping in corners and other hard to sweep areas"    
ELSE IF A[6786].Where("%stiff bristle%").Values IS NOT NULL 
    THEN "Stiff bristles provide aggressive cleaning" 
ELSE IF A[6786].Where("%heat-resistant bristles%").Values IS NOT NULL 
    THEN "High heat resistant bristles offer long term economy" 
ELSE IF A[6786].Where("%stain-resistant bristles%").Values IS NOT NULL 
    THEN "Stain-resistant bristles for longer product life"; 

IF $SP-23349$ LIKE "%tool holder%" 
AND A[6786].Values.Where("%handle holders", "%brush holders").ExtractDecimals().Max() > 0 
THEN "Can hold up to "_A[6786].Values.Where("%brush holders").ExtractDecimals().Max()_" items";

--wheels
IF A[6786].Values.Where("wheels") IS NOT NULL 
    THEN "Wheels improve wear resistance";

--Warranty Information
IF A[3574].Value LIKE "%lifetime%" 
    THEN "Lifetime manufacturer limited warranty" 
ELSE IF A[3574].Value IS NOT NULL 
    THEN A[3574].Value.Replace(" years","-year").Replace(" year","-year").Replace(" days","-day").Replace(" day","-day")
    .Replace(" months", "-month").Replace(" month", "-month").Postfix(" manufacturer limited warranty")
ELSE "@@";