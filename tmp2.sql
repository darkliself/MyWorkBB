-- [FEATURE #1]
-- Laminating machine type, Color, Compatibility (also for pouches/rolls) & Use

-- White thermal and cold personal laminator for occasional home and hobbyist use 
-- Thermal and cold laminating machine, thermal lamination for 3-mil or 5-mil and cold setting for self-adhesive pouches 
-- For use with thermal laminating pouches
--  Perfect for all personal lamination needs
-- 3M Scotch® 9'' Thermal laminator in gray color laminates item up to 5 mil thick and helps to protect documents.
-- The GBC Ultima 65 27-inch thermal roll laminator makes it easy to protect and preserve the important documents.

IF $SP-22967$ IS NOT NULL
AND $SP-18633$ IS NOT NULL
AND A[6899].Values.Flatten() IS NOT NULL
THEN $SP-22967$_" "_REF["SP-18633"].Replace("&","and")_" laminator for use with laminating "_A[6899].Values.Flatten().Replace("pouch", "pouches").Replace("roll", "rolls")_" to protect documents" 
ELSE IF $SP-22967$ IS NOT NULL
AND $SP-18633$ IS NOT NULL
THEN $SP-22967$_" "_$SP-18633$_" laminator makes it easy to protect and preserve the important documents" 
ELSE IF $SP-18633$ IS NOT NULL
AND A[6899].Values.Flatten() IS NOT NULL
THEN REF["SP-18633"].Replace("&","and")_" laminator for use with laminating "_A[6899].Values.Flatten().Replace("pouch", "pouches").Replace("roll", "rolls")_" to protect documents" 
ELSE IF $SP-18633$ IS NOT NULL 
THEN $SP-18633$_" laminator makes it easy to protect and preserve the important documents" 
ELSE "@@";

-- [FEATURE #2]
-- Construction (includes no. of rollers), Heat-up Time (includes special tech) & Laminating Speed

-- Fusion® 7000L Laminator provides faster-than-ever lamination with a 1-minute warm-up time and superior laminating speed.
-- -- It laminates at variable speeds of up to 10'' per minute. 

IF $SP-12512$ LIKE "1" 
AND $SP-12674$ IS NOT NULL
THEN "Provides lamination with "_$SP-12512$_" minute warm-up time and "_$SP-12674$_""" per minute laminating speed" 
ELSE IF $SP-12512$ IN ("2","3","4","5","6","7","8","9")   
AND $SP-12674$ IS NOT NULL
THEN "Provides lamination with "_$SP-12512$_" minutes warm-up time and "_$SP-12674$_""" per minute laminating speed" 
ELSE IF $SP-12512$ IS NOT NULL
AND $SP-12674$ IS NOT NULL
THEN "Provides lamination with "_$SP-12512$_"-minutes warm-up time and "_$SP-12674$_""" per minute laminating speed" 
ELSE IF $SP-12512$ LIKE "1" 
THEN "Laminator provides lamination with "_$SP-12512$_" minute warm-up time" 
ELSE IF $SP-12512$ IN ("2","3","4","5","6","7","8","9")   
THEN "Laminator provides lamination with "_$SP-12512$_" minutes warm-up time" 
ELSE IF $SP-12512$ IS NOT NULL
THEN "Laminator provides lamination with "_$SP-12512$_"-minutes warm-up time" 
ELSE IF $SP-12674$ IS NOT NULL
THEN "Laminates at variable speeds of up to "_$SP-12674$_""" per minute" 
ELSE "@@";

-- [FEATURE #3]
-- Throat Width & Input (Type & Size)

-- 27'' wide throat supports input up to 27'' wide or letter-sized documents 
IF $SP-22453$ IS NOT NULL
AND $SP-23995$ IS NOT NULL
THEN $SP-22453$_""" wide throat supports input up to "_$SP-23995$_""" wide" 
ELSE "@@";

-- [FEATURE #4]
-- Dimensions

-- Dimensions: 5.5''H x 16.8''W x 8.25''D 
IF $SP-20654$ IS NOT NULL
AND $SP-21044$ IS NOT NULL
AND $SP-20657$ IS NOT NULL
THEN "Dimensions: "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 
ELSE "@@";

-- [FEATURE #5]
-- Functionality (includes special tech & ADF) & Anti-jam features

-- Pouch Thickness Detection automatically determines optimal settings
-- Rapid 1-minute warm-up with InstaHeat Technology 
-- Auto Reverse mode reverses document flow for easy removal
-- Reverse jam prevention ensures smooth operation 
IF A[4282].Where("Flash Heat Technology").Values.Flatten()  IS NOT NULL
THEN "Rapid warm-up with Flash-Heat Technology" 
ELSE IF A[4282].Where("auto pouch thickness detection") IS NOT NULL
THEN "Pouch Thickness Detection automatically determines optimal settings" 
ELSE IF A[4282].Where("reverse button") IS NOT NULL
THEN "Reverses jam prevention ensures smooth operation" 
ELSE IF A[4282].Where("auto reverse function") IS NOT NULL
THEN "Auto-reverse mode reverses document flow for easy removal" 

ELSE IF A[6904].Value LIKE "Anti-jam system" 
THEN "Anti-jam system for efficient laminating" 
ELSE "@@";

-- [FEATURE #6]
--Controls/Settings (includes UI) & Indicators (includes display type)

IF $SP-1080$ IS NOT NULL
AND $SP-1080$ NOT LIKE "1" 
AND $CNET_MKT$ LIKE "%Premium design includes easy-to-use LED touch controls and button that turns green when machine is ready%" 
THEN REF["SP-1080"].Erase(" settings")_" temperature settings allow for customized use; premium design includes easy-to-use LED touch controls and button that turns green when machine is ready" 

ELSE IF $CNET_MKT$ LIKE "%Advanced temperature control ensures heat is carefully monitored for smooth, consistent results every time.%" 
THEN "Advanced temperature control ensures smooth, consistent results" 
ELSE "@@";

-- [FEATURE #7]
--Design (includes integrated features, storage options & carrier-free operation)
IF $CNET_MKT$ LIKE "%Stylish and ultra compact for storage, it laminates a single document from in under a minute%" 
THEN "Compact design for easy storage" 

ELSE IF $CNET_MKT$ LIKE "%Built-in carrying handle and cord wrap allows for neat storage no matter where you go%" 
THEN "Built-in carrying and cord-wrap handle for easy portability" 

ELSE IF $CNET_MKT$ LIKE "%Includes hidden built-in cord storage and foldable input tray%" 
THEN "Hidden built-in cord storage and foldable input tray for easy portability" 

ELSE IF $CNET_MKT$ LIKE "%Stylish and compact for storage, the Fusion 1000L laminates%" 
THEN "Compact design for easy storage" 
ELSE "@@";

-- [FEATURE #8]
--Safety Features
IF $SP-1128$ LIKE "Yes" 
THEN "For added safety, HeatGuard technology traps heat inside the laminator, so the outside is comfortable to touch" 
ELSE "@@";

-- [FEATURE #9]
--Power Management 
--Power: 100 - 240 VAC, 50/60 Hz nominal voltage, 55 W (maximum), 23 W (typical), less than 0.3 W (standby) power consumption

IF A[4236].Value IS NOT NULL 
AND A[3548].Value IS NOT NULL
THEN "Power: "_A[4236].Value.Erase(" V").Postfix("##V nominal voltage and ")_A[3548].Value.Postfix("##W maximum power consumption")

ELSE IF A[4236].Value IS NOT NULL 
THEN "Power: "_A[4236].Value.Erase(" V").Postfix("##V nominal voltage ") 

ELSE IF A[3548].Value IS NOT NULL
THEN "Power: "_A[3548].Value.Postfix("##W maximum power consumption")
ELSE "@@";

--[FEATURE #10]
--Maintenance - OOS

-- EXTRA
-- Auto Shut off save energy and prevents overheating 
IF $SP-736$ LIKE "Yes" 
THEN "Auto Shut off save energy and prevents overheating" 
ELSE "@@";

--Additional
IF A[9500].Values.WhereNot("laminator") IS NOT NULL
    THEN A[9500].Values.WhereNot("laminator").Flatten(", ").Prefix("Package includes: ");

--[FEATURE #11]
--Warranty Information
IF A[3574].Value LIKE "%lifetime%" 
    THEN "Lifetime manufacturer limited warranty" 
ELSE IF A[3574].Value IS NOT NULL 
    THEN A[3574].Value.Replace(" years","-year").Replace(" year","-year").Replace(" days","-day").Replace(" day","-day")
    .Replace(" months", "-month").Replace(" month", "-month").Postfix(" manufacturer limited warranty")
ELSE "@@";