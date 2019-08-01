-- Bullet 1
-- Tape type & use

-- https://www.staples.com/product_169055
IF $SP-18701$ LIKE "Double Sided" 
THEN "Double Sided Tape holds things together without being seen" 
-- https://www.staples.com/product_2301886
ELSE IF $SP-18701$ LIKE "Masking" 
THEN "Masking Tape provides instant adhesion and resists lifting or curling" 
-- https://www.staples.com/product_772036
ELSE IF $SP-18701$ LIKE "Electrical" 
THEN "Electrical Tape provides excellent resistant to abrasion, moisture, alkalis, acids and corrosion" 
-- https://www.staples.com/product_738006
ELSE IF $SP-18701$ LIKE "Removable" 
THEN "Removable Tape is great for temporary posting" 
-- https://www.staples.com/product_487909
ELSE IF $SP-18701$ LIKE "Transparent" 
THEN "Transparent Tape is the perfect clear tape for wrapping, sealing, and label protection" 
-- Scotch® Magic™ Tape is the perfect invisible tape for sealing, mending, and labeling 
ELSE IF $SP-18701$ LIKE "Invisible" 
THEN "Invisible tape is the perfect option for sealing, mending, and labeling" 
ELSE "@@";

-- Bullet 2
-- Dimensions as W (in inches) x L (in yards); width is how wide the tape is & length is the total length of the roll

-- https://www.staples.com/product_DUC0021087
IF $SP-22046$ IS NOT NULL
AND $SP-22047$ IS NOT NULL
THEN "Tape with dimensions of "_ $SP-22046$_"""W x "_ $SP-22047$_" yds." 
ELSE "@@";

-- Bullet 3
-- Core diameter (Inches)

-- https://www.staples.com/product_368101
IF A[6447].ValueUSM LIKE "%in%" 
THEN A[6447].ValueUSM.Split(" ").First().Prefix("Core diameter: ").Postfix("""")
ELSE "@@";

-- Bullet 4
-- True color

-- https://www.staples.com/product_826131
IF A[6446].Values.Flatten(",").Split(",").Where("%tape%") IS NOT NULL
THEN A[6446].Values.Flatten(",").Split(",").Where("%tape%").Erase("tape").Prefix("Comes in ").Postfix(" color")

ELSE IF $SP-22967$ IS NOT NULL
    THEN "Comes in "_$SP-22967$

ELSE "@@";

-- Bullet 5
-- Tape Pack Size (If more than 1)

-- https://www.staples.com/product_518724?akamai-feo=off
IF Request.Data["TX_UOM"].ExtractDecimals().First() > 1 
THEN "They are sold as "_Request.Data["TX_UOM"].ExtractDecimals().First()_" per "_Request.Data["TX_UOM"].Split("/").Last().ToLower()
ELSE IF Request.Data["TX_UOM"] LIKE "Dozen" 
THEN "They are sold as 12 per pack" 
ELSE "@@";

-- Bullet 6
-- Dispenser included (If applicable)

-- https://www.staples.com/product_483534
IF A[6461].Value LIKE "dispenser" 
AND A[6462].Value LIKE "desktop" 
THEN "Desktop tape dispenser for keeping tape at hand atop your desk" 
-- https://www.staples.com/product_483534
ELSE IF A[6461].Value LIKE "dispenser" 
AND A[6462].Value LIKE "hand held" 
THEN "Dispenser is easy to hold and carry" 
-- https://www.staples.com/product_812056
ELSE IF A[6461].Value LIKE "dispenser" 
THEN "Includes dispenser for easy use" 
ELSE "@@";

-- Bullet 7
-- Tensile strength rating (If Applicable)

-- https://www.staples.com/product_425575?akamai-feo=off
IF A[6453].UnitUSM LIKE "lb/in" 
AND A[6453].ValueUSM = 1
THEN "Features a tensile strength of one lb. per inch" 
ELSE IF A[6453].UnitUSM LIKE "lb/in" 
THEN A[6453].ValueUSM.Prefix("Features a tensile strength of ").Postfix(" lbs. per inch")
ELSE "@@";

-- Bullet 8
-- Certifications

--https://www.staples.com/product_512320
IF A[380].Values IS NOT NULL
THEN "Meets or exceeds "_A[380].Values.FlattenWithAnd()_"  standards" 
ELSE "@@";

-- Additional bullet
-- Invisible 

-- https://www.staples.com/product_487908
IF A[6457].Values.Flatten() LIKE "%invisible after application%" 
THEN "Once applied, tape becomes invisible";

-- Additional bullet
-- Permanent adhesive

--https://www.staples.com/product_329504
IF A[6457].Values.Flatten() LIKE "%permanent adhesive%" 
THEN "Strong tape ideal for permanent, secure paper mending";

-- Additional bullet
-- Matte/glossy finish

-- https://www.staples.com/product_504753
IF A[6457].Value IS NOT NULL 
THEN A[6457].Values.Flatten().Split("; ").Where("%finish%").ToUpperFirstChar().Postfix(" for basic office use");

-- Additional bullet
-- Writable surface

-- https://www.staples.com/product_487908
IF A[6685].Value IS NOT NULL
THEN "Can be written on with pen, pencil, or marker";

-- Additional bullet 
-- Product Recycled Content

-- https://www.staples.com/product_329504?akamai-feo=off
IF A[6628].Value > 0
THEN "More environmentally-friendly, made from "_A[6628].Value_"##% recycled or plant-based material";

-- Lets you use both in indoor and outdoor places for maximum beneficiary (https://www.staples.com/3M-15-Dual-Lock-Re-closable-Fastener-System-Clear-2-Pack/product_IM1Y97725)
IF A[6189].Values.Where("indoor/outdoor use") IS NOT NULL
    THEN "You can use it both indoors and outdoors for maximum efficiency";