-- Hanging File Folders

-- 1 - True Color and File Folder Material (including Stock/Weight #)

-- Durable 11 pt. folders in assorted bright colors (violet, yellow,red, blue, green) hold up to wear ##and tear

IF $SP-22967$ Like "Assorted" 
    THEN "Durable "_REF["SP-18596"].Replace("paper-stock", "paper stock")_" folders in assorted bright colors, hold up to wear ##and tear" 

-- Red 11 pt. folders hold up to wear and tear
ELSE IF $SP-22967$ IS NOT NULL
    THEN "Folders are made of "_$SP-22967$_" "_REF["SP-18596"].Replace("paper-stock", "paper stock") 
ELSE "@@";

-- 2 - Hanging File attachments details/Construction

-- Durable coated hangers for easy sliding
IF A[5945].Values.Where("coated hanger") IS NOT NULL
    THEN "Durable coated hangers for easy sliding" 

-- Long plastic hooks with built-in tension springs to grip rails
ELSE IF A[5945].Values.Where("built-in tension springs") IS NOT NULL
    THEN "Long plastic hooks with built-in tension springs to grip rails" 

-- Durable steel frame provides long-lasting use 
ELSE IF A[6547].Values.Where("%frame%") IS NOT NULL
    THEN A[6547].Values.Where("%frame%")_" provides long-lasting use" 

ELSE IF A[5945].Values.Where("lowered top rail") IS NOT NULL
    THEN "Lowered top rail edge eliminates need for separate file folder tabs" 

    --reinforced gusset
ELSE IF A[5945].Values.Where("reinforced gusset") IS NOT NULL 
    THEN "Reinforced gusset provides extra strength at critical points" 
ELSE "@@";

-- 3 - Number of Tabs/Location

IF A[5945].Where("5 tab positions").Values IS NOT NULL
AND A[5945].Where("%built%in%5%tab%").Values IS NOT NULL THEN
   "Tabs included for placement in 5 tab positions" 

-- Movable tabs go anywhere across the range of the folders for customization   
ELSE IF A[5945].Values.Where("Easy Slide tab") IS NOT NULL
    THEN "Movable tabs go anywhere across the range of the folders for customization " 

--tab positions
ELSE IF A[5945].Values.Where("%tab positions%") IS NOT NULL 
    THEN A[5945].Values.Where("%tab positions%").First()
    _" allow you to customize your files" 
ELSE "@@";

-- 4 - Paper Size

-- Accommodates letter sized papers and files
IF $SP-12517$ IS NOT NULL
AND $SP-12517$ Not Like "Other" 
    THEN "Accommodates "_$SP-12517$_"-size papers and files" 
ELSE "@@";

-- 5 - Scoring/File Folder Expansion Inches

-- Scored for 3/4 to hold several pages at once
IF $SP-23627$ > 0
    THEN "Scored for "_$SP-23627$_""" expansion to hold several pages at once" 
ELSE "@@";

-- 6 - Recycled Content % (If Applicable)
IF $SP-21623$ LIKE $SP-21624$
AND $SP-21623$ IS NOT NULL
    THEN "Made from "_$SP-21623$_"% post-consumer recycled material" 
ELSE IF $SP-21623$  IS NOT NULL
AND $SP-21624$ = 100
THEN "Made from "_$SP-21623$_"% recycled material with "_$SP-21624$_"% post-consumer material" 
ELSE IF $SP-21623$  IS NOT NULL
AND $SP-21624$ IS NOT NULL
    THEN "Made from "_$SP-21623$_"% recycled material with at least "_$SP-21624$_"% post-consumer material" 
ELSE IF $SP-21623$  IS NOT NULL
    THEN "Made from "_$SP-21623$_"% recycled material" 
ELSE IF $SP-21624$ = 100 
    THEN "Made from "_$SP-21624$_"% post-consumer material" 
ELSE IF $SP-21624$ IS NOT NULL 
    THEN "Made from at least "_$SP-21624$_"% post-consumer material" 
ELSE "@@";

-- 7 - Hanging File Folder Pack Size

-- Contains 25 folders per pack
IF Request.Data["TX_UOM"] IS NOT NULL
AND Request.Data["TX_UOM"] Not Like "Each" 
    THEN "Contains "_Request.Data["TX_UOM"].Replace("/", " folders per ")
ELSE "@@";

-- 8-12(5) - Use for additional product and/or manufacturer information relevant to the customer buying decision

-- Water resistant for durability
IF A[5945].Values.Where("waterproof") IS NOT NULL
    THEN "Water-resistant for durability";

IF A[6984].Value Like "No" 
    THEN "No assembly required";

-- Hanging File Pockets will fit in a file drawer or desktop filing system
IF A[5921].Value Like "%Hanging File%" 
    THEN "##"_A[5921].Value.ToUpperFirstChar()_" fits in a file drawer or desktop filing system" 
ELSE IF A[5922].Value Like "%Hanging File%" 
    THEN "##"_A[5922].Value.ToUpperFirstChar()_" fits in a file drawer or desktop filing system";

--swing hooks
IF A[5945].Values.Where("%Swing hooks") IS NOT NULL 
    THEN "Swing hooks fold in or swing out as needed for use as a hanging file or a regular expanding pocket";

--Tabs and inserts included for placement in 5 tab positions
IF A[5945].Where("%tab positions").Values IS NOT NULL
    THEN "Tabs and inserts are included for placement in "_A[5945].Where("%tab positions").Values.First();

-- Staples Reinforced Box-Bottom Hanging File Folders are ideal for large files
IF A[5945].Where("box bottom").Values IS NOT NULL
    THEN "Box-bottom hanging file folders are ideal for large files";

IF A[3574].Value IS NOT NULL 
    THEN "##"_A[3574].Value.Replace(" years", "-year").Replace(" months", "-month").Replace(" days", "-day").Replace(" year", "-year").Erase("limited").Erase("manufacturer").Erase("warranty").ToUpperFirstChar()_" manufacturer limited warranty" 
ELSE "@@";
