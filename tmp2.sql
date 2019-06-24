--Computer cleaning type & use
IF $SP-18424$ LIKE "Cleaning Kit" 
   THEN "Cleaning kit provides safe and gentle cleaning for all your mobile and electronic devices" 
ELSE IF $SP-18424$ LIKE "Laser Lens Cleaners" 
   THEN "Cleaning kit allow each brush to retract into a recess to avoid knocking laser lens out of alignment" 
ELSE IF $SP-18424$ LIKE "Repair Kit" 
   THEN "Tool kit is ideal for electronics, computer maintenance and home repairs" 
ELSE IF $SP-18424$ LIKE "CD/DVD Cleaners" 
   THEN "CD/DVD cleaner cleans up to 99% of all scratched ##DVDs, ##CDs, game discs, ##VCDs" 
ELSE IF $SP-18424$ LIKE "Vacuum" 
   THEN "Vacuum computer cleaning system is designed to remove dust, dirt and debris from computer systems" 
ELSE IF $SP-18424$ LIKE "Air Duster" 
   THEN "Air duster is perfect for removing dust or debris from sensitive electronic devices" 
ELSE IF $SP-18424$ LIKE "Screen Cleaner Sprays" 
   THEN "Cleaning spray is suitable for cleaning notebook ##PCs, touchscreens, keyless pads and phones" 
ELSE IF $SP-18424$ LIKE "Wipes/Cloths" 
   THEN "Screen wipes provide safe cleaning to any tablet, laptop, monitor, or cell phone screen" 
ELSE IF $SP-18424$ LIKE "Wipes/Duster Combo" 
   THEN "Duster is ideal for cleaning keyboard or CPU and wipes are safe to use on notebooks" 
ELSE IF $SP-18424$ LIKE "Cleaning Card" 
   THEN "Cleans card reader with one easy swipe" 
ELSE IF $SP-18424$ LIKE "Keyboard Guard" 
   THEN "Guard protect your keyboard from dust and spills" 
ELSE IF $SP-18424$ LIKE "Printer Cleaning Kit" 
   THEN "Printer cleaning kit offers a safe, easy way to clean printers, copiers and fax machines" 
ELSE "@@";

--Odor or scent--OOS

CASE $SP-4886$
WHEN "Bitterant" THEN "Contains bitterant to help discourage inhalant abuse" 
WHEN "Slight Ethereal" THEN "Slightly ethereal scent" 
WHEN "Clean scent" THEN "Clean scent for pleasant effect" 
WHEN "Unscented" THEN "Unscented to cause less irritation" 
ELSE "@@" 
END;

--Capacity
IF $SP-21132$ IS NOT NULL
THEN "Has a "_$SP-21132$_" oz. capacity" 
ELSE "@@";

--Key ingredient
IF $SP-24202$ LIKE "%Ozone Safe%" THEN "The 100% ozone-safe formula" 
ELSE IF $SP-24202$ LIKE "%microfiber%" THEN "Manufactured using microfiber for added safety" 
ELSE IF $SP-24202$ LIKE "%streak free%" AND $SP-24202$ LIKE "%alcohol-free%" THEN "Streak free and alcohol free" 
ELSE IF $SP-24202$ LIKE "%antimicrobial finish%" THEN "Antimicrobial protection keeps product cleaner" 
ELSE IF $SP-24202$ LIKE "%, %" THEN "Key ingredients: "_$SP-24202$
ELSE IF $SP-24202$ IS NOT NULL THEN "Key ingredient: "_$SP-24202$
ELSE "@@";

--Computer cleaning pack size
IF $SP-22606$ LIKE "Each" THEN NULL
    ELSE IF $SP-22606$ > 1 
THEN "Contains "_$cnet_cleaning_pack$_" "_COALESCE(A[371].Value.Pluralize().IfLike("s",""), A[7520].Value.Pluralize().IfLike("s",""),
A[6780].Value.Pluralize().IfLike("s",""), A[7520].Value.Pluralize().IfLike("s",""),
A[1047].Values.First().Pluralize().IfLike("s",""))_" per "_Request.Data["TX_UOM"].Split("/").Last().ToLower().Replace("each","pack")
    ELSE "@@";

--Certification & standards
IF $SP-21659$ IS NOT NULL
THEN "Meets or exceeds "_$SP-21659$_" standards" 
ELSE "@@";

--Use for additional product and/or manufacturer information relevant to the customer buying decision--Dimensions/Size
IF $SP-20654$ IS NOT NULL --H
AND $SP-21044$ IS NOT NULL --W
AND $SP-20657$ IS NOT NULL --D
    THEN "Dimensions: "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 
ELSE IF $SP-21044$ IS NOT NULL --W
AND COALESCE($SP-20657$, $SP-20654$) IS NOT NULL --L
    THEN "Dimensions: "_$SP-21044$_"""W x "_COALESCE($SP-20657$,$SP-20654$)_"""L" 
ELSE IF A[6185].ValueUSM IS NOT NULL THEN A[6185].ValueUSM.Replace(" in", """");

--Use for additional product and/or manufacturer information relevant to the customer buying decision
IF A[374].Values.Flatten() LIKE "%anti-static%" THEN "Anti-static formula cleans and protects" 
ELSE IF A[5810].Values.Flatten() LIKE "%anti-static%" THEN "Anti-static formula cleans and protects" 
ELSE IF A[6786].Values.Flatten() LIKE "%anti-static%" THEN "Anti-static formula cleans and protects";

IF COALESCE(A[374].Values.Where("%alcohol%free%"), 
A[6970].Values.Where("%alcohol%free%")) IS NOT NULL 
AND COALESCE(A[374].Values.Where("%streak%free%"), 
A[6970].Values.Where("%streak%free%")) IS NOT NULL 
    THEN "Streak- and alcohol-free" 
ELSE IF COALESCE(A[374].Values.Where("%alcohol%free%"), 
A[6970].Values.Where("%alcohol%free%")) IS NOT NULL 
    THEN "Alcohol-free solution is safe for all screens" 
ELSE IF COALESCE(A[374].Values.Where("%streak%free%"), 
A[6970].Values.Where("%streak%free%")) IS NOT NULL 
    THEN "Streak-free";

IF A[374].Where("%antimicrobial finish%").Values IS NOT NULL THEN "Antimicrobial protection keeps product cleaner";

IF A[374].Where("%dust-resistant%spill-resistant").Values IS NOT NULL THEN "Protect from dust and spills";

--Use for additional product and/or manufacturer information relevant to the customer buying decision
IF A[5795].Value LIKE "%transparent%" THEN "Clear color";

--Use for additional product and/or manufacturer information relevant to the customer buying decision
CASE COALESCE(A[371].Value, A[7520].Value)
    WHEN NULL THEN NULL
    WHEN "%air%" THEN "Canned air is perfect for getting crumbs out of your keyboard";

--Use for additional--package content(if each)
IF $SP-22606$ LIKE "Each" AND COALESCE(A[4868].Values, A[4846].Values, A[7521].Values, A[7094].Values, A[6958].Values) IS NOT NULL
    THEN "Package contains "_COALESCE(A[4868].Values.Replace(" oz "," oz. ").EraseTextSurroundedBy("()").FlattenWithAnd(), 
A[4846].Values.Replace(" oz "," oz. ").EraseTextSurroundedBy("()").FlattenWithAnd(), 
A[7521].Values.Replace(" oz "," oz. ").EraseTextSurroundedBy("()").FlattenWithAnd(), 
A[6958].Values.Replace(" oz "," oz. ").EraseTextSurroundedBy("()").FlattenWithAnd(), 
A[7094].Values.Replace(" oz "," oz. ").EraseTextSurroundedBy("()").FlattenWithAnd()); 

--Use for additional product and/or manufacturer information relevant to the customer buying decision
IF COALESCE(A[4846].Where("%extension tube%").Values, A[4925].Where("%extension tube%").Values, A[4868].Where("%extension tube%").Values, A[7521].Where("%extension tube%").Values) 
IS NOT NULL THEN "Includes an extension tube for pin point cleaning";

--Use for additional product and/or manufacturer information relevant to the customer buying decision
IF A[374].Values.Flatten() LIKE "%washable cloth%" THEN "Cleaning cloth comes in washable design for simple cleanup and cleans without causing any damages to delicate surfaces";

IF A[374].Values.Where("anti-scratch") IS NOT NULL THEN
    "Devices will not scratch";