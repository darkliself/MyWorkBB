-- 1 - Paper type & Use

IF $SP-21737$ LIKE "Specialty" 
AND A[4759].Value LIKE "%carbonless paper%" THEN
"Specialty computer paper is excellent for making carbonless copies" 

ELSE IF $SP-21737$ IS NOT NULL
AND $SP-12560$ NOT IN (NULL, "Other")
    THEN "Reliable "_$SP-21737$_" paper is perfect for "_$SP-12560$_" use" 
ELSE IF $SP-21737$ IS NOT NULL
AND $SP-12560$ IN ("Other")
    THEN "Reliable "_$SP-21737$_" paper is perfect for various uses" 
ELSE IF $SP-21737$ IS NOT NULL THEN
    "Reliable "_$SP-21737$_" paper is perfect for everyday use" 
ELSE IF A[4763].Values IS NOT NULL THEN
    "Works great in your "_A[4763].Values.FlattenWithAnd().Replace("and", "or")_" printer" 
ELSE "@@";

-- 2 - Dimensions as follows:  If Standup (3D)  use Height (floor/base to top)  x Width (side to side)  x Depth (front to back)  in inches as H x W x D. If lay flat (2D such as paper) use Width (side to Side) x Length (top to bottom) in inches. If layflat has accountable Thickness (over .125”), add as Depth so use H x W x D.  If linear (rope, string, ribbon) use Width/Diameter in inches x Length in feet as W x L unless otherwise specified.  If liquid, dimensions should be appropriate content in ounces (oz.) or gallons (gal.)
-- https://www.staples.com/Staples-100-Recycled-Copy-Paper-8-1-2-x-11-Case/product_620014
--paper measuring 8 1/2 x 11 is ideal for bulletins, announcements, flyers, and memos. https://www.staples.com/product_733095

IF $SP-21044$ IS NOT NULL
AND $SP-20400$ IS NOT NULL THEN
    "Paper dimensions: "_$SP-21044$_"""W x "_$SP-20400$_"""L";

-- 3 - Paper Weight (lb.)
-- https://www.staples.com/Staples-Copy-Paper-8-1-2-x-11-Case/product_135848
IF $SP-18379$ IS NOT NULL THEN
    "Paper weight: "_$SP-18379$_" lbs." 
ELSE "@@";

-- 4 - Brightness
-- https://www.staples.com/Staples-Copy-Paper-8-1-2-x-11-Case/product_135848
--Brights colored paper is ideal for direct mail, flyers and office or school projects (https://www.staples.com/product_578341)
--This pastel paper is ideal for prints with moderate solid areas, graphics and saturated colors.

--Office paper
IF CAT.Main.Key LIKE "§1676385310823140679" 
     THEN NULL
ELSE IF $SP-18380$ IS NOT NULL THEN
    "Brightness rating of "_$SP-18380$_" for sharp, clear print results" 
ELSE "@@";

-- 5 - Paper Color & Finish
-- https://www.staples.com/Staples-Copy-Paper-8-1-2-x-11-Case/product_135848
--requested
--https://www.staples.com/Neenah-Paper-Astrobrights-Color-Cardstock-8-1-2-x-11-Eclipse-Black-100-Sheets-Pack-22024-01/product_581146
        --https://www.staples.com/Neenah-Paper-Astrobrights-Color-Cardstock-8-1-2-x-11-Eclipse-Black-100-Sheets-Pack-22024-01/product_581146
--https://www.staples.com/Boise-POLARIS-Premium-Color-Copy-Paper-11-x-17-White-500-Ream-BCP-2817/product_CASBCP2817

--Copy & Multipurpose Paper
IF CAT.Main.Key LIKE "§1676385310823140691" 
AND $SP-22967$ LIKE "Photo white" 
AND $SP-21738$ IS NOT NULL THEN
     "White photo paper with "_$SP-21738$_" finish" 
ELSE IF CAT.Main.Key LIKE "§1676385310823140691" 
AND $SP-14894$ IS NOT NULL 
AND $SP-21738$ IS NOT NULL THEN
    $SP-14894$_" paper with "_$SP-21738$_" finish" 
ELSE IF CAT.Main.Key LIKE "§1676385310823140691" 
AND $SP-22967$ NOT IN ("%bar%", NULL) 
AND $SP-22967$ LIKE "white" THEN 
    "Comes in white" 
ELSE IF CAT.Main.Key LIKE "§1676385310823140691" 
AND $SP-22967$ NOT IN ("%bar%", NULL) THEN 
    "Comes in "_$SP-22967$
ELSE "@@";

-- 6 - Sheet Quantity & Reams per case

--SP-23866

IF SKU.ProductId IN ("20503614", "20503652") THEN
    "Contains 10 reams per case, 40 cases per pallet" 
-- Sheet Quantity & Reams per case
ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "1-Ream" AND $SP-16747$ LIKE "1" AND $SP-23866$ LIKE "%Ream%" THEN
    "One ream, one sheet" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "1-Ream" AND $SP-16747$ LIKE "1" AND $SP-23866$ IS NOT NULL THEN
    "One ream per "_$SP-23866$_", one sheet" 

ELSE IF $SP-16747$ IS NULL
THEN NULL

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "1-Ream" AND $SP-23866$ LIKE "%Ream%" THEN
    "One ream, "_$SP-16747$_" sheets total" 



ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "1-Ream" AND $SP-23866$ IS NOT NULL THEN


    "One ream per "_$SP-23866$_", "_$SP-16747$_" sheets total" 

// here stoped
ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "3-Ream" AND $SP-23866$ LIKE "%Ream%" THEN
    "Three reams, "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "3-Ream" AND $SP-23866$ IS NOT NULL THEN
    "Three reams per "_$SP-23866$_", "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "4-Ream" AND $SP-23866$ LIKE "%Ream%" THEN
    "Four reams, "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "4-Ream" AND $SP-23866$ IS NOT NULL THEN
    "Four reams per "_$SP-23866$_", "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "5-Ream" AND $SP-23866$ LIKE "%Ream%" THEN
    "Five reams, "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "5-Ream" AND $SP-23866$ IS NOT NULL THEN
    "Five reams per "_$SP-23866$_", "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "6-Ream" AND $SP-23866$ LIKE "%Ream%" THEN
    "Six reams, "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "6-Ream" AND $SP-23866$ IS NOT NULL THEN
    "Six reams per "_$SP-23866$_", "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "8-Ream" AND $SP-23866$ LIKE "%Ream%" THEN
    "Eight reams, "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "8-Ream" AND $SP-23866$ IS NOT NULL THEN
    "Eight reams per "_$SP-23866$_", "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "10-Ream" AND $SP-23866$ LIKE "%Ream%" THEN
    "10 reams, "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "10-Ream" AND $SP-23866$ IS NOT NULL THEN
    "10 reams per "_$SP-23866$_", "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "20-Ream" AND $SP-23866$ LIKE "%Ream%" THEN
    "20 reams "_$SP-16747$_" sheets total" 

ELSE IF A[353].Value IS NOT NULL
AND $SP-22607$ LIKE "20-Ream" AND $SP-23866$ IS NOT NULL THEN
    "20 reams per "_$SP-23866$_", "_$SP-16747$_" sheets total"  

ELSE "@@";

-- 7 - Acid Free
-- https://www.staples.com/HammerMill-Copy-Plus-Copy-Paper-8-1-2-x-11-Case/product_122374
IF $SP-21736$ LIKE "Ye%" THEN
    "Paper is acid-free, which prevents it from crumbling or yellowing" 
ELSE "@@";

-- 8 - Recycled Content (%)
-- https://www.staples.com/Staples-30-Recycled-Copy-Paper-8-1-2-x-11-Case/product_492072
-- https://www.staples.com/Staples-100-Recycled-Copy-Paper-8-1-2-x-11-Case/product_620014
IF A[6628].Value > 0 THEN
    "Contains "_A[6628].Value_"% recycled postconsumer content for a sustainable choice" 
ELSE "@@";

-- 9 - Additional Bullet (if relevant)

IF A[6010].Where("yes").Values IS NOT NULL
AND A[380].Where("SFI").Values IS NOT NULL THEN 
        "Meets or exceeds FSC and SFI standards" 

ELSE IF A[6010].Where("yes").Values IS NOT NULL THEN
    "FSC certified - this product comes from responsbilty managed forests" 

ELSE IF A[380].Where("SFI").Values IS NOT NULL THEN
    "Meets or exceeds SFI standard";

-- 10 - Additional Bullet (if relevant)
IF CAT.Main.Key LIKE "§1676385310823140679" 
AND A[10618].Value LIKE "smooth" 
    THEN "Smooth surface for high-impact color graphics";

IF A[4783].Values.Where("%-hole punched").ExtractDecimals().First() > 1 
AND CAT.Main.Key LIKE "§1676385310823140691" THEN
    "Prepunched copy paper eliminates the need for a "_
    A[4783].Values.Where("%-hole punched").ExtractDecimals().First()
        .IfLike("2", "two")
        .IfLike("3", "three")
        .IfLike("4", "four")
        .IfLike("5", "five")
        .IfLike("6", "six")
        .IfLike("7", "seven")
        .IfLike("8", "eight")
        .IfLike("9", "nine")
    _"-hole punch";