--Name tag/badge type & Use

--Fasten this ID badge holder to clothing, bags and more to keep your hands free (https://www.staples.com/product_511100) - ID Badge Holder
IF $SP-18516$ LIKE "ID Badge Holder" 
    THEN "Fasten this ##ID badge holder to clothing, bags and more to keep your hands free" 

--ID reel connects easily to any ID card or name badge (not included) with clear badge holder (https://www.staples.com/Swivel-Back-Clip-On-Retractable-ID-Reel-with-Badge-Holder-Black-12-PK/product_890413) - ID reel
ELSE IF $SP-18516$ LIKE "Badge Reels" 
    THEN "Badge reel connects easily to any ID card or name badge" 

--Lanyard to hang identification badge around your neck (https://www.staples.com/product_662776?akamai-feo=off)
ELSE IF $SP-18516$ LIKE "Lanyards" 
    THEN "Lanyard to hang identification badge around your neck" 

--Name Tag is ideal for meetings, seminars, special events, conventions and more (https://www.staples.com/Avery-Self-Laminating-Name-Tags-with-Clips-2-x-3-1-4/product_612648)
ELSE IF $SP-18516$ LIKE "Sticker Name Tags/Labels" 
    THEN "Name tag is ideal for meetings, seminars, special events, conventions and more" 

ELSE IF $SP-18516$ LIKE "ID Cards" 
    THEN "ID Cards for everyday use" 

--Plastic ID badge holders for everyday use (https://www.staples.com/Avery-2921-Horizontal-Name-Tag-Holders-Clear-50-Pack/product_576409)
ELSE IF $SP-18516$ LIKE "Plastic Name Tags/ID Cards" 
    THEN "Plastic Name Tags for everyday use" 
ELSE "@@";

--Attachment style

--Pin securely holds badge in place  (https://www.staples.com/product_SS941254)
IF $SP-21608$ LIKE "Pin" 
    THEN "Pin securely holds badge in place" 

--Secures to clothing with metal clip (https://www.staples.com/Staples-Vinyl-Straps-with-Two-Hole-Clips-Clear-100-Pack-18914-1122897/product_573105)
ELSE IF A[6086].Where("metal clip").Values IS NOT NULL
OR A[6121].Where("metal clip").Values IS NOT NULL
    THEN "Secures to clothing with metal clip" 

--Clip secures badge to clothing (https://www.staples.com/C-line-Reusable-Clear-Plastic-Name-Tag-Holders/product_76611)    
ELSE IF A[6086].Where("clip").Values IS NOT NULL
OR A[6121].Where("clip").Values IS NOT NULL
    THEN "Clip secures badge to clothing" 

--Belt clip model hooks on pants belt     (https://www.staples.com/product_458940)    
ELSE IF A[6121].Where("belt carabiner", "belt clip").Values IS NOT NULL
    THEN "Belt clip model hooks on pants belt" 

--Bulldog clip fastens to clothing, bags and more (https://www.staples.com/Swingline-GBC-1122897-ID-Badge-Clip-Clear-Strap-Silver-Clip/product_857505)
ELSE IF $SP-21608$ LIKE "Bulldog Clip" 
    THEN "Bulldog clip fastens to clothing, bags and more" 

--Integrated swivel clip helps prevent damage to garments (https://www.staples.com/Avery-74536-Garment-Friendly-Clip-Style-Name-Badges-White-3-x-4-50-Pack/product_461274)    
ELSE IF $SP-21608$ LIKE "Swivel Clip" 
    THEN "Integrated swivel clip helps prevent damage to garments" 

--Swivel hook to attach ID cards securely (https://www.staples.com/1344043BAC31-36-Visitor-Pre-Designed-Lanyards-with-Breakaway-Release-Red-10-Pack/product_206367)
ELSE IF $SP-21608$ LIKE "Swivel Hook" 
    THEN "Swivel hook to attach ID card securely" 
ELSE "@@";

--Badge loading style
--Top-loading design prevents inserts from falling out (https://www.staples.com/product_440726)
IF A[6087].Where("top side open").Values IS NOT NULL
    THEN "Top-loading design prevents inserts from falling out" 
--Self-adhesive badges adhere firmly and remove easily (https://www.staples.com/Avery-Print-or-Write-Name-Tags-Red-Border-2-11-32-x-3-3-8/product_404301)
ELSE IF $SP-21603$ LIKE "Self Adhesive" 
    THEN "Self-adhesive badges adhere firmly and remove easily" 
ELSE "@@";

--Name tag & Badge size
--Name tag & Badge size
IF $SP-20400$ IS NOT NULL --length
AND $SP-21044$ IS NOT NULL --width
    THEN "Size: " 
         _$SP-21044$_"""W x " 
         _$SP-20400$_"""L" 

ELSE IF $SP-20400$ IS NOT NULL
AND $SP-18516$ LIKE "Lanyards" 
    THEN "Lanyard length: "_$SP-20400$_"""" 
ELSE "@@";

--Printer type used (If applicable)

--Designed for use with both inkjet and laser printers for optimal versatility (https://www.staples.com/Avery-74536-Garment-Friendly-Clip-Style-Name-Badges-White-3-x-4-50-Pack/product_461274)
IF $SP-21605$ LIKE "Inkjet/Laser" 
    THEN "Designed for use with both inkjet and laser printers for optimal versatility" 

--Allows creating badges with laser printer, eliminating extra costs (https://www.staples.com/The-Mighty-Badge-901856-Insert-Sheet-Refill-Kit-for-Laser-Printer-Bright-White-60-Pack/product_1106903)    
ELSE IF $SP-21605$ LIKE "Laser" 
    THEN "Allows creating badges with laser printers, eliminating extra costs" 
ELSE IF $SP-21605$ LIKE "Inkjet" 
    THEN "Allows creating badges with inkjet printers, eliminating extra costs" 
ELSE "@@";

--Pack Size (# of badges)

IF Request.Data["TX_UOM"] LIKE "%/%" 
    THEN Request.Data["TX_UOM"].Replace("/", " per ")
ELSE "@@";

--Recycled Content (%)

--10 percent post-consumer recycled content for an eco-friendly option (https://www.staples.com/Staples-Reinforced-Fastener-Folder-2-Fasteners-Legal-Manila-50-Box/product_831057)
IF A[6628].Value > 0
    THEN A[6628].Value
         _"% post-consumer recycled content for an eco-friendly option" 
ELSE "@@";

--Additional Bullet (Orientation)

--Use in horizontal or vertical orientation  (https://www.staples.com/product_357520)
IF A[6087].Where("landscape/portrait orientation").Values IS NOT NULL
    THEN "Use in horizontal or vertical orientation" 
ELSE IF COALESCE(
                A[6121].Where("portrait orientation", "vertical cards orientation").Values,
                A[6087].Where("portrait orientation", "vertical cards orientation").Values,
                A[4783].Where("portrait orientation", "vertical cards orientation").Values
                ) IS NOT NULL
AND COALESCE(
                A[6121].Where("landscape orientation", "horizontal cards orientation").Values,
                A[6087].Where("landscape orientation", "horizontal cards orientation").Values,
                A[4783].Where("landscape orientation", "horizontal cards orientation").Values,
                A[5944].Value.IsEmpty.IfLike("Yes", "").IfLike("No", "Not empty")
                ) IS NOT NULL
    THEN "Use in horizontal or vertical orientation" 

--Portrait-orientation for easy loading of the badge (https://www.staples.com/134664931-Vertical-ID-Badge-Holders-Clear-50-Pack/product_206467)    
ELSE IF COALESCE(
                A[6121].Where("portrait orientation", "vertical cards orientation").Values,
                A[6087].Where("portrait orientation", "vertical cards orientation").Values,
                A[4783].Where("portrait orientation", "vertical cards orientation").Values
                ) IS NOT NULL
    THEN "Portrait orientation for easy loading of the badge" 

--Landscape orientation to slip badge easily into the holder (https://www.staples.com/134647931-Horizontal-ID-Badge-Holders-Clear-50-Pack/product_206466)
ELSE IF COALESCE(
                A[6121].Where("landscape orientation", "horizontal cards orientation").Values,
                A[6087].Where("landscape orientation", "horizontal cards orientation").Values,
                A[4783].Where("landscape orientation", "horizontal cards orientation").Values,
                A[5944].Value.IsEmpty.IfLike("Yes", "").IfLike("No", "Not empty")
                ) IS NOT NULL
    THEN "Landscape orientation to slip badge easily into the holder";

--Additional Bullet (PVC-free material)

--Made from soft, PVC Free plastic (https://www.staples.com/product_SS941254)
IF COALESCE(
            A[6087].Where("PVC-free").Values,
            A[4783].Where("PVC-free").Values
            ) IS NOT NULL
AND COALESCE(
            A[372].Values,
            A[6073].Values
            ) IS NOT NULL
    THEN "Made of PVC-free " 
         _COALESCE(A[372].Values.First().ToLower(), A[6073].Values.First().ToLower())

--material
ELSE IF $SP-17447$ IS NOT NULL 
    THEN "Made of "_$SP-17447$;

IF $SP-22967$ LIKE "Assorted" 
     THEN "Comes in assorted colors" 
ELSE IF $SP-22967$ IS NOT NULL
     THEN "Comes in "_$SP-22967$;

--Additional Bullet (Prepunched)  

--Prepunched for use with strap clips, lanyards, reels, chains and more (https://www.staples.com/C-Line-Horizontal-ID-Badge-Holders-12-Pack/product_975994)
IF COALESCE(
            A[6087].Where("pre-punched holes", "pre-drilled holes").Values,
            A[6121].Where("%pre-punched%").Values
            ) IS NOT NULL
    THEN "Pre-punched for use with strap clips, lanyards, reels, chains and more" 
    ELSE "@@";

--Additional Bullet (Garment Friendly clip)

--Garment-friendly clip helps to protect clothing (https://www.staples.com/Avery-Top-Loading-Clip-Style-Name-Tags-2-1-4-x-3-1-2/product_461148)
IF A[6087].Where("Garment Friendly clip").Values IS NOT NULL
    THEN "Garment-friendly clip helps protect clothing";

--Additional Bulltet (Magnet Attachment)

--Magnet attaches easily and won't mar clothing (https://www.staples.com/C-Line-Name-Badge-Holder-Kits-Magnetic-Top-Load-3-x-4-20-Bx/product_438493)
IF A[6121].Where("lacquered magnetic side").Values IS NOT NULL
OR A[6086].Where("magnet").Values IS NOT NULL    
    THEN "Magnet attaches easily and won't mar clothing";

--Additional Bulltet (Removable Adhesive)

--Name badge labels come with removable adhesive so they will not damage your clothing (https://www.staples.com/Blanks-USA-3-1-2-x-2-1-4-Name-Tag-Label-White-100-Pack/product_195253)
IF $SP-18516$ LIKE "Sticker Name Tags/Labels" 
AND A[4783].Where("removable adhesive").Values IS NOT NULL
    THEN "Name badge labels come with removable adhesive so they will not damage your clothing";
