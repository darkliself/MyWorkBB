--Accordion Folders

--Accordion Folders

--              Bullet 1
--Accordion File Type & Use
--Missed Accordion File Use in vortex

--Featuring 21 pockets, these heavy-duty expanding files make organizing important documents a breeze
--https://www.staples.com/product_595367

IF A[5921].Value LIKE "expanding file" 
    AND A[6546].Value IS NOT NULL
    AND A[5945].Where("heavy-duty").Values IS NOT NULL
        THEN "Featuring "_A[6546].Value_" pockets, these heavy-duty expanding files make organizing important documents a breeze" 

--Separate tabs are conveniently labeled January through December, which makes filing and finding your letter-sized documents a snap
--https://www.staples.com/product_595369
ELSE IF A[5921].Value IS NOT NULL
    AND A[5947].Where("A-Z").Values IS NOT NULL
    AND A[5947].Where("1-%").Values IS NOT NULL
        THEN A[5921].Value.ToUpperFirstChar()_" labeled "_A[5947].Where("A-Z", "1-%").Values.Flatten(", ")_" , which makes filing & finding your documents a snap"        

--Project Sorter is a great way to quickly consolidate clutter into organized categories
--https://www.staples.com/Pendaflex-PileSmart-Project-Sorter-Poly-10-Dividers-Blue-50995/product_581832
ELSE IF A[5921].Value LIKE "project sorter" 
    THEN "Project Sorter is a great way to quickly consolidate clutter into organized categories" 

--When you're on the go, wallets are the easy way to transport a large amount of paperwork
--https://www.staples.com/Smead-Expansion-Wallet-Redrope-Legal-Size-15-x-10/product_649335
ELSE IF A[5921].Value LIKE "expanding wallet" 
    THEN "When you're on the go, expanding wallets are an easy way to transport a large amount of paperwork" 

--Your new accordion expanding file includes 21 compartments and an A-to-Z index for organization and easy access to your files
--https://www.staples.com/product_119099
ELSE IF A[5921].Value IS NOT NULL
AND A[5947].Values.Count > 1
AND A[5947].Where("A-Z", "1-*", "jan*", "dec*").Values IS NOT NULL
    THEN A[5921].Value_" features an "_A[5947].Where("A-Z", "1-*", "jan*", "dec*").Values.Prefix("##").Flatten(", ").Postfix(" index for organization and easy access to your files").Shorten(100).Erase("[...]")

--Your new accordion expanding file includes 21 compartments and an A-to-Z index for organization and easy access to your files
--https://www.staples.com/product_119099

ELSE IF A[5921].Value LIKE "expanding file" 
AND A[6546].Value IS NOT NULL
AND A[5947].Where("A-Z").Values IS NOT NULL 
    THEN A[6546].Value.Prefix("Expanding file includes ").Postfix(" compartments and an ##A-Z index for organizing papers and documents").Shorten(100).Erase("[...]")

--Heavy-Duty Letter size expanding file features A-Z alphabetically indexed dividers for ease of organizing papers and documents.
--https://www.staples.com/product_595367
ELSE IF A[5921].Value LIKE "expanding file" 
    AND A[5947].Where("A-Z").Values IS NOT NULL 
        THEN "Expanding file features ##A-Z indexed dividers for ease of organizing papers and documents" 

--These expanding files feature 31 numbered pockets for easy organization of your papers
--https://www.staples.com/product_119107

ELSE IF A[5921].Value LIKE "expanding file" 
    AND A[5947].Where("1-%").Values IS NOT NULL
        THEN "These expanding files feature "_A[5947].Where("1-%").Values.First().ExtractDecimals().Last()_" numbered pockets for easy organization" 

--Staples expanding letter size document file features 26 pockets for keeping all your items neat, organized and secured. 
--https://www.staples.com/product_2757019

ELSE IF A[5921].Value IS NOT NULL 
    AND A[6546].Value IS NOT NULL
        THEN A[5921].Value.ToUpperFirstChar()_" features "_A[6546].Value_" pockets to keep items neat, organized and secured"  

--This 13-pocket accordion file is ideal for storing monthly statements, receipt and expense reports 
--https://www.staples.com/product_781633 

ELSE IF A[5921].Value IS NOT NULL 
    THEN "This "_A[5921].Value_" is ideal for storing monthly statements, receipts and expense reports" 
ELSE "@@";

--              Bullet 2
--Accordion File Closure Type
--An elastic enclosure keeps documents securely stored inside the letter expanding file
--https://www.staples.com/product_606603

IF A[5939].Where("%elastic closure%").Values IS NOT NULL 
    THEN "An elastic enclosure keeps documents securely stored inside the "_A[5921].Value
--Full flap with a corner closure, so that your papers always stay in place and you never have to worry about misplacing your work
--https://www.staples.com/product_2806372

ELSE IF A[5939].Where("%flap%").Values IS NOT NULL 
    AND A[5939].Where("%band%", "%closure%", "%holder%").Values.Count = 1

        THEN A[5939].Where("%flap%").Values
                .First().Replace("Full cover", "Full-cover").ToUpperFirstChar()
            _" with " 
            _A[5939].Where("%band%", "%closure%", "%holder%").Values
                .First()
            _" ensures your papers stay in place" 
ELSE IF A[5939].Values.Count > 1 
    THEN A[5939].Values
            .First()
            .ToUpperFirstChar()
        _" with " 
        _A[5939].Values
            .Last()
        _" ensures your papers stay in place" 

ELSE IF A[5939].Values.Count = 1 
    THEN A[5939].Values
            .First()
            .ToUpperFirstChar()
            .Erase("closure")

        _" closure ensures your papers stay in place" 
--Flap closure with twist lock for safe transportation of files
--https://www.staples.com/product_895843

ELSE IF A[5939].Where("%flap%").Values IS NOT NULL
    AND A[5939].Where("%twist lock%").Values IS NOT NULL
        THEN A[5939].Where("%flap%").Values
                .First()
                .Replace("Full cover", "Full-cover")
                .ToUppderCase()
            _" closure with twist lock for safe transportation of files" 

--Flap and elastic cord closure keep contents secure
--https://www.staples.com/product_452749

ELSE IF A[5939].Where("%elastic band").Values IS NOT NULL
    AND A[5939].Where("%Flap").Values IS NOT NULL
        THEN "Flap and elastic cord closure keep contents secure" 
ELSE "@@";

--              Bullet 3
--Folder Dimensions
IF A[354].UnitUSM LIKE "in" 
   AND A[355].UnitUSM LIKE "in" 
   AND A[356].UnitUSM LIKE "in" 
   THEN  "Dimensions: "_A[356].ValueUSM_"""H x "_A[355].ValueUSM_"""W x "_A[354].ValueUSM_"""D" 

ELSE IF A[354].UnitUSM LIKE "in" 
    AND A[356].UnitUSM LIKE "in" 
        THEN  "Dimensions: "_A[354].ValueUSM_"""W x "_A[356].ValueUSM_"""L" 

ELSE IF A[354].UnitUSM LIKE "in" 
    AND A[355].UnitUSM LIKE "in" 
        THEN  "Dimensions: "_A[354].ValueUSM_"""W x "_A[355].ValueUSM_"""L" 

ELSE IF A[5924].ValueUSM LIKE "%in%" 
    AND A[5924].ValueUSM.ExtractDecimals().Count = 2
    THEN "Dimensions: "_A[5924].ValueUSM
            .ExtractDecimals()
            .Postfix("""")
            .Flatten(" x ")

ELSE IF A[5924].ValueUSM IS NOT NULL
    OR A[354].ValueUSM IS NOT NULL
    OR A[355].ValueUSM IS NOT NULL
    OR A[356].ValueUSM IS NOT NULL
        THEN THROW
ELSE "@@";

--              Bullet 4
--True color & Material of Item
IF A[373].Values.Where("assorted") IS NOT NULL
OR A[1618].Value IS NOT NULL
   THEN A[5921].Value.ToUpperFirstChar()
   _" available in assorted colors" 
   _COALESCE(A[5955].WhereNot("kraft board").Values.First().Prefix(" ##and made of "))

--[color] color with [material] material to organize and protect your documents
--https://www.staples.com/Staples-Poly-Expanding-Hanging-File-Jackets-Letter-Assorted-5-Pack/product_706811
ELSE IF A[5955].Values IS NOT NULL
AND A[373].Values.Count > 1
    THEN "Multicolor with " 
         _A[5955].WhereNot("kraft board").Values.First().IfLike("", "kraft board")
            .Erase("(PP)")
            .Erase("(PU)")
            .Erase("(TPE)")
            .Erase("(TPU)")
         _" material helps to organize and protect your documents" 

ELSE IF A[5955].Values IS NOT NULL
AND A[5811].Values.Count > 1
    THEN "Multicolor "_A[5921].Value_" with " 
         _A[5955].WhereNot("kraft board").Values.First().IfLike("", "kraft board")
            .Erase("(PP)")
            .Erase("(PU)")
            .Erase("(TPE)")
            .Erase("(TPU)")
         _" material helps to organize and protect your documents" 
ELSE IF A[5955].Values.Flatten() LIKE "Kraft paper" 
AND A[373].Values.Flatten() LIKE "kraft"   
    THEN "Made of kraft paper, these files are great for organizing and protecting document" 

ELSE IF A[5955].Values IN ("kraft%", "%redrope%")
AND A[373].Values IN ("kraft", "redrope")
    THEN A[5955].WhereNot("kraft board").Values
            .First()
            .IfLike("", "kraft board")
            .IfLike("card stock", "cardstock")
            .Erase("(PP)")
            .Erase("(PU)")
            .Erase("(TPE)")
            .Erase("(TPU)")
            .ToUpperFirstChar()
        _" files help organize and protect your documents" 

ELSE IF A[5955].Values IS NOT NULL
AND A[373].Values IS NOT NULL
    THEN A[373].Values.First().ToUpperFirstChar().Replace("available in different colors","Available in different colors,").ToUpperFirstChar()_" "_A[5955].WhereNot("kraft board").Values
            .First()
            .IfLike("", "kraft board")
            .IfLike("card stock", "cardstock")
            .Erase("(PP)")
            .Erase("(PU)")
            .Erase("(TPE)")
            .Erase("(TPU)")
        _" files help organize and protect your documents" 

ELSE IF A[5955].Values IS NOT NULL
AND A[5811].Values IS NOT NULL
    THEN A[5811].Values.First().ToUpperFirstChar().Replace("available in different colors","Available in different colors,").ToUpperFirstChar()
         _" "_A[5955].WhereNot("kraft board").Values.First().IfLike("", "kraft board").Erase("(PP)").Replace("card stock", "cardstock").Replace("card-stock","cardstock")
         _" files help organize and protect your documents" 

--Poly Colors expanding files offer protection and organization for your documents.
--https://www.staples.com/product_498249
ELSE IF A[373].Values IS NOT NULL 
    AND A[5955].Value IS NULL 
        THEN A[373].Values.Flatten(", ").ToUpperFirstChar().Replace("available in different colors","Available in different colors,")_" "_A[5921].Value_"  offers protection and organization for your documents" 

ELSE IF A[5811].Values IS NOT NULL 
    AND A[5955].Value IS NULL 
        THEN A[5811].Values.Flatten(", ").ToUpperFirstChar().Replace("available in different colors","Available in different colors,")_" "_A[5921].Value_"  offers protection and organization for your documents" 
ELSE "@@";     

--              Bullet 5
--Folder or Paper Size
--Holds letter-size documents for easy storage and access 
--https://www.staples.com/product_SS985803

IF A[5925].ValuesUSM.Where("%in%").Count = 1 
    THEN "Holds "_A[5925].ValuesUSM.Replace(" in", """").Replace(" A ", " ##A ").Replace("legal ", "legal size ").Erase("size size ")_" documents" 

--Accommodates letter sized documents 
--https://www.staples.com/product_2757002    
ELSE IF A[5925].ValuesUSM.Where("%in%").Count > 1
    THEN "Accommodates "_A[5925].ValuesUSM.Where("%in%").FlattenWithAnd(10, ", ").Replace(" in", """").Erase("a size")_" sized documents" 
ELSE IF A[5925].Values IS NULL THEN NULL
ELSE "@@";

--              Bullet 6
--Pack Qty (If more than 1)
IF Request.Data["TX_UOM"] LIKE "each" THEN "@@" 

-- for <<dozen>> mixed feature
--Contains 12 per pack for bulk purchasing
ELSE IF Request.Data["TX_UOM"] LIKE "dozen" 
    THEN "Contains 12 per pack" 

--Contains 10 per pack for bulk purchasing (2-10)
ELSE IF Request.Data["TX_UOM"].ExtractDecimals().First() < 11
    THEN Request.Data["TX_UOM"]
            .ExtractDecimals()
            .First()
        _" per "_
        Request.Data["TX_UOM"]
            .Split("/")
            .Last()
            .ToLower()

--25 per box provides enough for almost any project (11-49)
ELSE IF Request.Data["TX_UOM"].ExtractDecimals().First() < 50
    THEN Request.Data["TX_UOM"]
            .ExtractDecimals()
            .First()
        _" per " 
        _Request.Data["TX_UOM"]
            .Split("/")
            .Last()
            .ToLower()
        _" provides enough for almost any project" 

--50 pack size for economy and long use (> 50)
ELSE IF Request.Data["TX_UOM"].ExtractDecimals().First() >= 50
     THEN Request.Data["TX_UOM"]
            .ExtractDecimals()
            .First()

        _" per " 
        _Request.Data["TX_UOM"]
            .Split("/")
            .Last()
            .ToLower()
        _" for economy and long use" 
ELSE "@@";

--              Bullet 7
--Post Consumer Content (%)
--Minimum 10-percent post-consumer content reduces your carbon footprint 
--https://www.staples.com/product_810352
IF A[11306].Value < 31 THEN 
    "Minimum "_A[11306].Value_"% post-consumer content reduces your carbon footprint" 

--Made of 100% post-consumer content reducing environmental impact 
--https://www.staples.com/product_482504
ELSE IF A[11306].Value > 30 THEN 
    "Made of "_A[11306].Value_"% post-consumer content reducing environmental impact" 
ELSE "@@";

--              Bullet 8
--Recycled Content (%)
--Minimum 10 percent recycled content for an environmentally friendly product 
--https://www.staples.com/product_459684
IF A[6628].Value < 31
    THEN "Minimum "_A[6628].Value_"% recycled content" 

--10 percent post-consumer content for an eco-friendly choice 
--https://www.staples.com/product_807791
ELSE IF A[6628].Value > 30
    THEN A[6628].Value_"% recycled content for an eco-friendly choice " 
ELSE "@@";

--              Bullet 9
--expands to 1'', allotting you plenty of space to hold all of your important letter-sized
--https://www.staples.com/Staples-Manila-Expanding-File-Jacket-1-Letter-50-Box/product_541092
IF A[8552].UnitUSM LIKE "in" 
    THEN "Expands up to "_A[8552].ValueUSM_""" for extra space to hold your important papers" 

--Six expanding pockets can hold letter-size papers and organize your desk. 
--https://www.staples.com/product_24142056

ELSE IF A[6552].Value IS NOT NULL 
    AND A[6546].Value IS NOT NULL
        THEN A[6546].Value_" expanding pockets can hold letter-size papers assisting in desk organization";

--              Bullet 10
--Water-resistant material protects papers and documents from the elements
--https://www.staples.com/Avery-Protect-and-Store-1-Inch-Slant-D-3-Ring-View-Binder-White-23000/product_894667

IF A[5945].Where("waterproof").Values IS NOT NULL
    THEN "Water-resistant material protects papers and documents";

--              Bullet 11
-- opening
--Open top offers easy access to stored documents 
--https://www.staples.com/product_293118
IF A[5945].Where("top side open").Values IS NOT NULL
    THEN "Top opening offers easy access to stored documents";

--              Bullet 12
--antimicrobial
--antimicrobial material inhibits the growth of harmful bacteria, mold and mildew
--https://www.staples.com/product_654239
IF A[5945].Where("%antimicrobial%").Values IS NOT NULL
    THEN "Antimicrobial material inhibits the growth of harmful bacteria, mold and mildew";

--              Bullet 13
--Durable
--Use as wear-resistant
--wear-resistant fabric for extended use
--https://www.staples.com/product_689122
IF A[5945].Where("wear-resistant").Values IS NOT NULL
    THEN "Wear-resistant material for extended use";

--              Bullet 14
--reinforced
--Tyvek reinforced at top of gusset for extra durability 
--https://www.staples.com/product_517144

IF A[5945].Where("Tyvek%gusset", "reinforced tabs").Values IS NOT NULL
    THEN A[5945].Where("Tyvek%gusset").Values.First().ToUpperFirstChar()_" for extra durability" 

--Reinforced sides help to prevent rips and tears even when filled to capacity
ELSE IF  A[5945].Where("reinforced sides").Values IS NOT NULL
    THEN "Reinforced sides help to prevent rips and tears even when filled to capacity" 

--Reinforced gussets ensure the durability of this product - perfect for all your filing and organization needs. 
--https://www.staples.com/product_454257

ELSE IF A[5945].Where("%Reinforced%gussets%").Values IS NOT NULL
    THEN A[5945].Where("%Reinforced%gussets%").Values.Replace(" reinforced", "-reinforced").Replace(" strip", "-strip")
            .First()
            .ToUpperFirstChar()
        _" for durability" 

ELSE IF A[5945].Where("%Reinforced%gusset%").Values IS NOT NULL
    THEN A[5945].Where("%Reinforced%gusset%").Values.Replace(" reinforced", "-reinforced").Replace(" strip", "-strip")
            .First()
            .ToUpperFirstChar()
        _" for durability";

-- Includes labels for alphabetic (A to Z), monthly (Jan. to Dec.), daily (1 to 31), household subjects and blank indexing for easy labeling 
--  Use the 21 A-to-Z indexed pockets to sort your paperwork in alphabetical order, or apply custom labels for personalized organization.
IF A[5947].Values IS NOT NULL
THEN "Includes "_A[5947].Values.FlattenWithAnd().Replace("January-December", "monthly (Jan to ##Dec)").Replace("Jan-Dec", "monthly (Jan to ##Dec)").Replace("A-Z", "alphabetic (A to ##Z)").Replace("1-31", "daily (1 to 31)")_" labels for personalized organization" 
ELSE IF A[5923].Where("% labels", "% inserts").Values.Count = 1
THEN "Includes "_A[5923].Where("% labels", "% inserts").Values.Flatten()_" for personalized organization" 
ELSE IF A[5923].Where("% labels").Values.Count > 1
THEN "Includes "_A[5923].Where("% labels").Values.FlattenWithAnd().Replace("A-Z labels", "alphabetic (A to ##Z)").Replace("Jan.-Dec. labels", "monthly (Jan to ##Dec)").Replace("blank labels", "blank indexing").Replace("household subject labels", "household subjects").Replace("1-31 labels", "daily (1 to 31)").Erase("labels")_" labels for personalized organization" 
ELSE IF A[5923].Where("% inserts").Values.Count > 1
THEN "Includes "_A[5923].Where("% inserts").Values.FlattenWithAnd().Replace("A-Z inserts", "alphabetic (A to ##Z)").Replace("Jan.-Dec. inserts", "monthly (Jan to ##Dec)").Replace("blank inserts", "blank indexing").Replace("1-31 inserts", "daily (1 to 31)").Erase("inserts")_" inserts for personalized organization";
