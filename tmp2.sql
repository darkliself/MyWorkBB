--Pocket Folder Type, True Color & Pocket Folder Material

--SP-22708 - Type
--SP-22967 - True Color 
--SP-22709 - Material

--2-pocket folder with fastener is made of poly material in assorted colors (blue, green, yellow, red) 
IF $SP-22708$ IS NOT NULL
AND $SP-22967$ LIKE "Assorted" 
AND $SP-22709$ IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folders are made of " 
         _$SP-22709$_" material" 
         _" in assorted colors" 

ELSE IF $SP-22708$ IS NOT NULL AND $SP-22708$ LIKE "%pocket folder" 
AND $SP-22967$ LIKE "Multicolor" 
AND $SP-22709$ IS NOT NULL
    THEN "Multicolor " 
         _$SP-22708$
         _" is made of " 
         _$SP-22709$_" material" 

ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22967$ LIKE "Multicolor" 
AND $SP-22709$ IS NOT NULL
    THEN "Multicolor " 
         _REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folders are made of " 
         _$SP-22709$_" material" 

--Clear polypropylene for easy viewing and durability (https://www.staples.com/Report-Covers/cat_CL130501)
ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22967$ LIKE "Clear" 
AND $SP-22709$ IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folders are made of clear " 
         _$SP-22709$
         _" material for easy viewing and durability" 

--JAM Paper® Blue Plastic Two Pocket 3 Hole Punched Presentation School Folders are crafted from heavy duty plastic          
ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22709$ LIKE "Plastic" 
AND $SP-22967$ IS NOT NULL
    THEN $SP-22967$_" "_REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
_" folder is crafted from plastic" 

--Esselte Oxford 2-Pocket folder in yellow color made of sturdy leatherette stock resists tearing
ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22709$ LIKE "Leatherette Stock" 
AND $SP-22967$ IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folder comes in " 
         _$SP-22967$
         _" and made of sturdy leatherette stock resists tearing" 

--This blue Staples poly folder with prong fasteners is made of poly material for durability and is resistant to tearing as well as water. (https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=1213096&langId=-1&currentSKUNbr=431484&storeId=10101&itemType=1&addWE1ToCart=true&documentID=665429ac8ca3b64b1611200b5cc44499d565ada7)

ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22709$ LIKE "Poly" 
AND $SP-22967$ IS NOT NULL AND $SP-22708$ LIKE "%Pocket folder" 
    THEN $SP-22967$_" "_$SP-22708$_"s are made of poly material for durability" 

ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22709$ LIKE "Poly" 
AND $SP-22967$ IS NOT NULL
    THEN $SP-22967$_" "_REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
_" folders are made of poly material for durability" 

--Made of dark blue standard embossed paper stock for a professional appearance
ELSE IF A[5945].Where("embossed").Values IS NOT NULL
AND $SP-22708$ IS NOT NULL
AND $SP-22709$ LIKE "Paper Stock" 
AND $SP-22967$ IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folders are made of " 
         _$SP-22967$
         _" embossed paper stock" 

--Red Esselte Oxford recycled two-pocket folders made from high-quality stock paper for durability (https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=25987526&langId=-1&currentSKUNbr=479457&storeId=10101&itemType=1&addWE1ToCart=true&documentID=804ed4368e4d58e26f5a08de91d1fa1055fb13f7)    

ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22709$ LIKE "Paper Stock" 
AND $SP-22967$ IS NOT NULL  AND $SP-22708$ LIKE "%Pocket folder" 
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folder is made from high-quality " 
         _$SP-22967$
         _" paper stock" 

ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22709$ LIKE "Paper Stock" 
AND $SP-22967$ IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folders are made from high-quality " 
         _$SP-22967$
         _" paper stock" 

--These folders are made from smooth 100lb cardstock and have a shiny glossy finish
ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22709$ LIKE "Cardstock" 
AND $SP-22967$ IS NOT NULL
AND A[5945].Where("%gloss finish").Values IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folders are made from " 
         _$SP-22967$
         _" smooth cardstock and have a shiny finish" 

--These folders are made of 80lb cardstock so it is durable, yet still lightweight (https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=73158243&langId=-1&currentSKUNbr=263440&storeId=10101&itemType=1&addWE1ToCart=true&documentID=0739d3f585b585bdccbeec94ad706a9eac0bed41)         
ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22709$ LIKE "Cardstock" 
AND $SP-22967$ IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folders are made from " 
         _$SP-22967$
         _" cardstock so they are durable, yet still lightweight" 

ELSE IF $SP-22708$ IS NOT NULL
AND $SP-22709$ IS NOT NULL
AND $SP-22967$ IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folder in " 
         _$SP-22967$
         _" color is made of "_$SP-22709$_" material" 

ELSE IF $SP-22709$ IS NULL
AND $SP-22967$ LIKE "Clear" 
AND $SP-22708$ IS NOT NULL
    THEN "Clear "_REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")_" folder for easy viewing" 

ELSE IF $SP-22709$ IS NULL
AND $SP-22967$ IS NOT NULL
AND $SP-22708$ IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folder in " 
         _$SP-22967$

ELSE IF $SP-22709$ IS NOT NULL
AND $SP-22967$ IS NULL
AND $SP-22708$ IS NOT NULL
    THEN REF["SP-22708"].IfLike("Fasteners", "Fastener").IfLike("Heavy duty", "Heavy-duty")
         _" folders are made of " 
         _$SP-22709$_" material" 
ELSE "@@";

--# of Pockets

--Divide It Up folder with four pockets for easier organization of documents(https://www.staples.com/Oxford-Divide-It-Up-Poly-4-Pocket-Folder-Black/product_936907)  
IF $SP-22710$ LIKE "4 Pockets" 
    THEN "Folder with four pockets for easier organization of documents" 

ELSE IF $SP-22710$ LIKE "1 Pocket" 
    THEN "One interior pocket provides storage space for papers, brochures and more" 

ELSE IF $SP-22710$ IN ("2 Pockets", "3 Pockets")
    THEN "Feature "_$SP-22710$_" to store papers, brochures, and more" 

ELSE IF $SP-22710$ IS NOT NULL
    THEN $SP-22710$_" allow for detailed organization" 
ELSE "@@";

--Paper Size

--Comfortably holding standard letter-size paper (8 1/2'' x 11'')(https://www.quill.com/jam-paper-plastic-eco-two-pocket-clasp-school-folders-prong-clip-fasteners-black-6-pack-382ecbld/cbs/50835132.html?promoCode=&Effort_Code=901&Find_Number=1434009JAM&m=0&isSubscription=False)
IF $SP-12517$ LIKE "Letter" 
    THEN "Holds standard letter-size paper (8.5"" x 11"")" 

--These folders provide an efficient and durable way to collect and store all types of legal-sized documents (https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=52264825&langId=-1&currentSKUNbr=807787&storeId=10101&itemType=1&addWE1ToCart=true&documentID=e4ec4a56a00ea94fafc4273ad6326db63115ad37)
ELSE IF $SP-12517$ LIKE "Legal" 
     THEN "These folders provide an efficient and durable way to collect and store all types of legal-sized documents" 
ELSE "@@";

--Expandable Folder (If Applicable)

--Expands 3/4'' for greater storage capacity
IF A[8552].UnitUSM LIKE "in" 
    THEN "Expands " 
         _A[8552].ValueUSM.ExtractDecimals()
         .ToText("F2").RegexReplace("(?:(\.\d*?[1-9]+)|\.)0*$", "$1")
         _""" for greater storage capacity" 
ELSE IF A[8552].UnitUSM LIKE "ft" 
    THEN "Expands " 
         _A[8552].ValueUSM.ExtractDecimals().First().MultiplyBy(12).ToText("F2").RegexReplace("(?:(\.\d*?[1-9]+)|\.)0*$", "$1")
         _""" for greater storage capacity" 
ELSE IF A[8552].Unit LIKE "cm" 
    THEN "Expands " 
         _A[8552].Value.ExtractDecimals().First().MultiplyBy(0.393701).ToText("F2").RegexReplace("(?:(\.\d*?[1-9]+)|\.)0*$", "$1")
         _""" for greater storage capacity" 
ELSE IF A[8552].Unit LIKE "m" 
    THEN "Expands " 
         _A[8552].Value.ExtractDecimals().First().MultiplyBy(39.3701).ToText("F2").RegexReplace("(?:(\.\d*?[1-9]+)|\.)0*$", "$1")
         _""" for greater storage capacity" 
ELSE IF A[8552].Unit LIKE "mm" 
    THEN "Expands " 
         _A[8552].Value.ExtractDecimals().First().MultiplyBy(0.0393701).ToText("F2").RegexReplace("(?:(\.\d*?[1-9]+)|\.)0*$", "$1")
         _""" for greater storage capacity" 
ELSE "@@";

--Pocket Folder Pack Size (If more than 1)
IF Request.Data["TX_UOM"] LIKE "Each" THEN NULL
ELSE IF Request.Data["TX_UOM"].ExtractDecimals().First() < 11
    THEN Request.Data["TX_UOM"].ExtractDecimals().First()
         _" folders per "_Request.Data["TX_UOM"].Split("/").Last().ToLower() 
--25 folders per box is enough for almost any project (11-49)
ELSE IF Request.Data["TX_UOM"].ExtractDecimals().First() < 50
    THEN Request.Data["TX_UOM"].ExtractDecimals().First()
         _" folders per " 
         _Request.Data["TX_UOM"].Split("/").Last().ToLower() 
--50 pack size for economy and long use (> 50)
ELSE IF Request.Data["TX_UOM"].ExtractDecimals().First() IS NOT NULL
     THEN Request.Data["TX_UOM"].ExtractDecimals().First()
          _" folders per " 
         _Request.Data["TX_UOM"].Split("/").Last().ToLower()
ELSE "@@";

--Recycled Content % (If Applicable)

--60% total recycled content with 30% postconsumer content, making these environmentally friendly
IF $SP-21623$ > 0
AND $SP-21624$ > 0
    THEN $SP-21623$_"% total recycled content with " 
         _$SP-21624$_"% postconsumer content, making these environmentally friendly" 

--Made of 100% recycled content, these files are ideal for the environmentally conscious office
ELSE IF $SP-21623$ LIKE "100" 
    THEN "Made of 100% recycled content, these files are ideal for the environmentally conscious office" 

--Contains 50 percent postconsumer recycled content for environmental sustainability
ELSE IF $SP-21624$ > 0
    THEN "Contain "_$SP-21624$
         _" percent postconsumer recycled content for environmental sustainability" 
ELSE "@@";

--Additional Bullet (if relevant) (Business card holder)

--A business card holder adds an extra professional touch to each folder(https://www.staples.com/Avery-R-Two-Pocket-Folders-47985-Dark-Blue-Box-of-25/product_811820)
IF A[5965].Where("business card%").Values IS NOT NULL
    THEN "A business card holder adds an extra professional touch to each folder";

--Additional Bullet (if relevant) (Closed sides)

--Closed sides keep materials safe and secure(https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=73629&langId=-1&currentSKUNbr=757666&storeId=10101&itemType=1&addWE1ToCart=true&documentID=3122652577fdd888de9746fb232558420347d5e1)
IF A[5945].Where("closed sides").Values IS NOT NULL
    THEN "Closed sides keep materials safe and secure";

--Additional Bullet (if relevant) (Capacity)

--30 sheet capacity allows for convenient organization of loose papers (https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=52338241&langId=-1&currentSKUNbr=811822&storeId=10101&itemType=1&addWE1ToCart=true&documentID=4acada1aa6b2aed6b57772cb18c16e087ca072ae)
IF A[6576].Value IS NOT NULL
    THEN A[6576].Value
         _A[6576].Unit.IfLike("sheets", "sheet").IfLike("pages", "page").Prefix("-")
         _" capacity allows for convenient organization of loose papers";

--Additional Bullet (if relevant) (Tang fasteners)

--Include stitched-in gussets with three double-tang fasteners to bind punched sheets (https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=80438&langId=-1&currentSKUNbr=898320&storeId=10101&itemType=1&addWE1ToCart=true&documentID=d7e51d485eb299e56f6e7cff94bba37c4f7a082b)
IF A[5950].Value LIKE "3 double-tang fasteners" 
    THEN "Includes stitched-in gussets with three double-tang fasteners to bind punched sheets";

--Additional Bullet (if relevant) (CD slots)

--Some have slots for CDs too, making them great for presentations and press kits (https://www.staples.com/JAM-Paper-Plastic-Heavy-Duty-Two-Pocket-Folders-Clear-108-pack-3383HCLB/product_263474)
IF A[5965].Where("%CD pocket%").Values IS NOT NULL
    THEN "Has slots for CDs too, making it great for presentations and press kits";

--Additional Bullet (if relevant) (Hole punched)

--Folder is 3-hole punched for greater ease and is ideal for office use (https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=51811536&langId=-1&currentSKUNbr=513645&storeId=10101&itemType=1&addWE1ToCart=true&documentID=41e59a7fd2036e268e64c4fa83b6b98bc82bf9ec)
IF A[6548].Value = 3
OR A[5940].Value LIKE "3 holes" 
    THEN "Folder is 3-hole punched for greater ease and is ideal for office use";

--Additional Bullet (if relevant) (Cover clap)

--Get the extra capacity of a file pocket with the added security of a protective cover flap
IF A[5939].Where("%flap%").Values IS NOT NULL
    THEN "Get the extra capacity of a file pocket with the added security of a protective cover flap";