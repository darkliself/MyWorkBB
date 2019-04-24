--Type of Desktop Organizer and its use    
--Accessory Trays
--This medium accessory tray stacks and stores all your stuff. (https://www.staples.com/product_48955)
--Provides the extra space we all crave (https://www.staples.com/Poppin-Medium-Accessory-Tray-Pool-Blue-100238/product_48952)
IF $SP-18586$ LIKE "Accessory Trays" 
    THEN "Provides the extra space we all crave" 
--Mobile Device Stand/Holders
--The smart phone holder provides a removable ergonomic system to hold most mobile devices (https://www.staples.com/product_1610514)
ELSE IF $SP-18586$ LIKE "Mobile Device Stand/Holders" 
    THEN "The smart phone holder provides a removable ergonomic system to hold most mobile devices" 
--Pad Holders
--pad holder with pen is an excellent desk accessory (https://www.staples.com/product_155244)
ELSE IF $SP-18586$ LIKE "Pad Holders" 
    THEN "Pad holders are useful desk accessory" 
--Accessory Holders
ELSE IF $SP-18586$ LIKE "Accessory Holders" 
    THEN "Accessory holders are useful desk accessory" 
--Dispensers
ELSE IF A[6284].Value LIKE "paper clip dispenser" 
    THEN "Paper Clip Dispenser reduces clutter and adds convenience to your workspace" 
ELSE IF $SP-18586$ LIKE "Dispensers" 
    THEN "Dispensers are useful desk accessory" 

--Sets
--Storage/Document Boxes
--Document Box keeps important documents and file folders neatly tucked away. (https://www.staples.com/product_2566302)
ELSE IF $SP-18586$ LIKE "Storage/Document Boxes" 
    THEN "Document Box keeps important documents and file folders neatly tucked away" 
--Table Tops
ELSE IF $SP-18586$ LIKE "Table Tops" 
    THEN "Table Tops are useful desk accessory" 
--Stacking Supports
ELSE IF $SP-18586$ LIKE "Stacking Supports" 
    THEN "Stacking supports for trays provide solid support for stacking" 
--Letter Holder
ELSE IF $SP-18586$ LIKE "Letter Holder" 
    THEN "Letter holder helps organize cluttered desks" 
--Pen Cups
ELSE IF $SP-18586$ LIKE "Pen Cups" 
    THEN "Pen cup keeps pens and pencils organized" 
--Cabinets
ELSE IF $SP-18586$ LIKE "Cabinets" 
    THEN "Cabinets are useful desk accessory" 

-- Desk pad
ELSE IF $SP-18586$ LIKE "Desk pad" 
    THEN "Desk pad is an excellent desk protector" 

--Racks
ELSE IF $SP-18586$ LIKE "Racks" 
    THEN "Keep messages and other materials organized with this message rack" 
--Compartment Storage
ELSE IF $SP-18586$ LIKE "Compartment Storage" 
    THEN "Compartment storage is useful desk accessory for organizing your office, craft, or school supplies" 
--Copy Holders
ELSE IF $SP-18586$ LIKE "Copy Holders" 
    THEN "Copyholder makes reading at your desk an easier task" 
--Pencil Holders
ELSE IF $SP-18586$ LIKE "Pencil Holders" 
    THEN "Pencil holder provides plenty of room to store your writing instruments, rulers, and scissors" 
--Rotating Organizers
ELSE IF $SP-18586$ LIKE "Rotating Organizers" 
    THEN "Rotating desk organizer offers easy access to office supplies" 
--File Organizers
ELSE IF $SP-18586$ LIKE "File Organizers" 
    THEN "File organizer is great for keeping your desktop neat and tidy" 
---Storage Drawers
ELSE IF $SP-18586$ LIKE "Storage Drawers" 
    THEN "Storage Drawer keeps small supplies, writing instruments, and scissors organized" 
ELSE IF $SP-18586$ LIKE "Set" 
    THEN "This set is a useful desk accessory" 
ELSE IF $SP-18586$ IS NOT NULL
    THEN $SP-18586$_" are useful desk accessory" 
ELSE "@@";

--True color and Desktop Organizer Material   
IF $SP-22967$ LIKE "%clear%" 
AND $SP-18549$ IS NOT NULL
THEN "Made of clear "_$SP-18549$ 
ELSE IF $SP-22967$ LIKE "%black%" 
THEN "Comes in black" 
ELSE IF $SP-22967$ NOT IN (NULL, "Multicolor", "Clear")
AND A[5955].Where("%coated%").Values IS NOT NULL
    THEN "Comes in "_$SP-22967$_" and made of "_A[5955].Where("%coated%").Values.First() 
ELSE IF $SP-22967$ NOT IN (NULL, "Multicolor", "Clear")
AND $SP-18549$ IS NOT NULL 
    THEN "Comes in "_$SP-22967$_" and made of "_
         $SP-18549$_" for durability" 
ELSE IF $SP-22967$ LIKE "%assorted%" 
AND $SP-18586$ NOT LIKE "sets" 
    THEN $SP-18586$_" in bright, assorted colors stand out on your desk" 
ELSE IF $SP-22967$ LIKE "%assorted%" 
AND $SP-18586$ LIKE "sets" 
    THEN "Comes in bright, assorted colors stand out on your desk" 
ELSE IF $SP-22967$ LIKE "%multi%" 
    THEN "Organizer is multicolored" 
ELSE IF $SP-22967$ LIKE "%gray%" 
AND $SP-18549$ IS NOT NULL 
    THEN "Constructed with "_$SP-18549$_" material in a gray finish to match any workspace" 
ELSE IF $SP-22967$ IS NOT NULL 
AND $SP-18549$ IS NOT NULL 
    THEN "Constructed with "_$SP-18549$_" material in a "_$SP-22967$_" finish to match any workspace" 
ELSE IF $SP-22967$ LIKE "%gray%" 
    THEN "Gray color" 
ELSE IF $SP-22967$ LIKE "%gray%" 
AND $SP-18586$ NOT LIKE "sets" 
    THEN "Gray color" 
ELSE IF $SP-22967$ IS NOT NULL AND $SP-18586$ IS NOT NULL
    THEN $SP-22967$_" "_$SP-18586$
ELSE IF $SP-22967$ IS NOT NULL  
    THEN $SP-22967$_" construction" 
ELSE "@@";

--Dimensions in Inches: Height x Width x Depth    
IF A[1488].Value IS NOT NULL
AND $SP-20654$ IS NOT NULL
    THEN "Dimensions: ##"_$SP-20654$_"""H x "_$SP-21044$_"""Dia." 
ELSE IF $SP-20654$ IS NOT NULL
AND $SP-21044$ IS NOT NULL
AND $SP-20657$ IS NOT NULL
    THEN "Dimensions: ##"_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 
ELSE "@@";

--Feature/Benefit (Capacity as in Letter Size, 7-compartment tray, etc.) Use multiple bullets as necessary    
--Non-slip rubber feet reduce movement and protect your work surface from scratches and scuffs (https://www.staples.com/product_1587549)
--Desk Organizer provides an assortment of seven compartments that can easy store all your office supplies. !!!!!!!!!!!!
IF A[6289].Value NOT LIKE "1" 
AND A[6289].Value IS NOT NULL
    THEN "Desk Organizer provides "_A[6289].Value_" compartments, which can easily store all your supplies" 
ELSE IF A[6546].Value NOT LIKE "1" 
AND A[6546].Value IS NOT NULL
    THEN "Desk Organizer provides "_A[6546].Value_" compartments, which can easily store all your supplies" 
ELSE "@@";

IF A[6767].Value IS NOT NULL
OR A[6294].Where("%Non-slip%").Values IS NOT NULL
OR A[5945].Where("%Non-slip%").Values IS NOT NULL
    THEN "Non-slip rubber feet reduce movement and protect your work surface from scratches and scuffs" 
ELSE "@@";

IF A[6294].Where("%removable divider%").Values IS NOT NULL
    THEN "Removable divider panel for in-drawer organization" 
ELSE "@@";

IF A[6294].Where("%stackable%").Values IS NOT NULL
AND $SP-18586$ Like "Letter holder" 
    THEN $SP-18586$_" is stackable for customized storage" 
ELSE IF A[5936].Value IS NOT NULL
AND $SP-18586$ Like "Letter holder" 
    THEN $SP-18586$_" is stackable for customized storage" 
ELSE IF A[5945].Where("%stackable%").Values IS NOT NULL
AND $SP-18586$ Like "Letter holder" 
    THEN $SP-18586$_" is stackable for customized storage" 
ELSE IF A[6294].Where("%stackable%").Values IS NOT NULL
AND $SP-18586$ IS NOT NULL
    THEN $SP-18586$_" are stackable for customized storage" 
ELSE IF A[5936].Value IS NOT NULL
AND $SP-18586$ IS NOT NULL
    THEN REF["SP-18586"].IfLike("Accessory Tray", "Accessory trays")_" are stackable for customized storage" 
ELSE IF A[5945].Where("%stackable%").Values IS NOT NULL
AND $SP-18586$ IS NOT NULL
    THEN REF["SP-18586"].IfLike("Accessory Tray", "Accessory trays")_" are stackable for customized storage" 
ELSE "@@";

IF A[5945].Where("%vertical%horizontal%").Values IS NOT NULL
    THEN "Organizes your files on the desktop either vertically or horizontally" 
ELSE "@@";

IF COALESCE(A[374].Where("%tilt adjustment%").Values, A[6087].Where("%tilt%").Values) IS NOT NULL
    THEN "Adjustable tilt customizes viewing angle" 
ELSE "@@";

IF COALESCE(A[374].Where("%adjustable height%").Values, A[6087].Where("%height-adjustable%").Values) IS NOT NULL
    THEN "Adjustable height for comfortable viewing" 
ELSE "@@";

--Holds both A4 and letter size paper 
IF A[181].Where("%letter%", "%legal%").Values.Count = 2
    THEN "Holds both legal and letter-size paper" 
ELSE IF A[181].Where("%a4%", "%letter%").Values.Count = 2
    THEN "Holds both A4 and letter-size paper"    
ELSE IF A[181].Where("%legal%", "%letter%").Values.Count = 2
    THEN "Holds both A4 and legal-size paper"    
ELSE IF A[5925].Where("%letter%", "%legal%").Values.Count = 2
    THEN "Holds both legal and letter-size paper" 
ELSE IF A[5925].Where("%a4%", "%letter%").Values.Count = 2
    THEN "Holds both A4 and letter-size paper"    
ELSE IF A[5925].Where("%legal%", "%letter%").Values.Count = 2
    THEN "Holds both A4 and legal-size paper"    
ELSE IF A[181].Where("%letter%").Values IS NOT NULL
    THEN "Holds letter size-paper" 
ELSE IF A[181].Where("%legal%").Values IS NOT NULL
    THEN "Holds legal size-paper" 
ELSE IF A[181].Where("%A4%").Values IS NOT NULL
    THEN "Holds A4 size paper" 
ELSE IF A[5925].Where("%letter%").Values IS NOT NULL
    THEN "Holds letter-size paper" 
ELSE IF A[5925].Where("%legal%").Values IS NOT NULL
    THEN "Holds legal-size paper" 
ELSE IF A[5925].Where("%A4%").Values IS NOT NULL
    THEN "Holds A4 size paper" 
ELSE "@@";

IF A[5945].Where("%wall%mount%").Values IS NOT NULL
    THEN "Unit can be wall-mounted to save space" 
ELSE "@@";

--Use for additional product and/or manufacturer information relevant to the customer buying decision    
IF A[6294].Where("phone holder").Values IS NOT NULL
    THEN "Angled platform holds phone at just the right degree for quick and easy dialing and picking up";

--Use for additional product and/or manufacturer information relevant to the customer buying decision    

--Use for additional product and/or manufacturer information relevant to the customer buying decision    

--Use for additional product and/or manufacturer information relevant to the customer buying decision    

--Use for additional product and/or manufacturer information relevant to the customer buying decision

--Use for additional product and/or manufacturer information relevant to the customer buying decision    

--Use for additional product and/or manufacturer information relevant to the customer buying decision

--Warranty    
 IF A[3574].Value IS NOT NULL 
    THEN A[3574].Value
    .Replace(" years", "-year")
            .Replace("-years", "-year")
            .Replace(" year","-year")
            .Replace(" days","-day")            
            .Replace(" months", "-month")
            .Erase("limited")
            .Erase("manufacturer")
            .Replace("warranty", "")
            .Postfix(" manufacturer limited warranty")
            .ToUpperFirstChar()
ELSE "@@";