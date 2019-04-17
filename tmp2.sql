--Type of trash can & use 
IF $SP-18159$ LIKE "Trash Cans w/ no Lid" 
AND $SP-24133$ LIKE "Semi-Round" 
    THEN "Semi-round trash can without lid ensures clean and litter-free space" 
ELSE IF $SP-18159$ LIKE "Trash Cans w/ no Lid" 
AND $SP-24133$ IS NOT NULL 
    THEN $SP-24133$_" trash can without lid ensures clean and litter-free space" 
ELSE IF $SP-18159$ LIKE "Trash Cans w/ no Lid" 
    THEN "Trash can without lid ensures clean and litter-free space" 

ELSE IF $SP-18159$ LIKE "Step Trash Cans" 
AND $SP-24133$ LIKE "Semi-Round" 
    THEN "Semi-round step trash can ensures clean and litter-free space and provides hands-free operation" 
ELSE IF $SP-18159$ LIKE "Step Trash Cans" 
AND $SP-24133$ IS NOT NULL 
    THEN $SP-24133$_" step trash can ensures clean and litter-free space and provides hands-free operation" 
ELSE IF $SP-18159$ LIKE "Step Trash Cans" 
    THEN "Step trash can ensures clean and litter-free space and provides hands-free operation" 

ELSE IF $SP-18159$ LIKE "Swing Lid Trash Cans" 
AND $SP-24133$ LIKE "Semi-Round" 
    THEN "Semi-round swing lid trash can allows easy trash disposal and conceals odors within the basket" 
ELSE IF $SP-18159$ LIKE "Swing Lid Trash Cans" 
AND $SP-24133$ IS NOT NULL
    THEN $SP-24133$_" swing lid trash can allows easy trash disposal and conceals odors within the basket" 
ELSE IF $SP-18159$ LIKE "Swing Lid Trash Cans" 
    THEN "Swing-lid trash can allows easy trash disposal and conceals odors within the basket" 

ELSE IF $SP-18159$ LIKE "Ash Urns" 
AND A[6599].Value LIKE "cigarette receptacle" 
AND $SP-24133$ LIKE "Semi-Round" 
    THEN "Semi-round cigarette receptacle extinguishes cigarettes without sand or water"     
ELSE IF $SP-18159$ LIKE "Ash Urns" 
AND A[6599].Value LIKE "cigarette receptacle" 
AND $SP-24133$ IS NOT NULL 
    THEN $SP-24133$_" cigarette receptacle extinguishes cigarettes without sand or water" 
ELSE IF $SP-18159$ LIKE "Ash Urns" 
AND A[6599].Value LIKE "cigarette receptacle" 
    THEN "Cigarette receptacle extinguishes cigarettes without sand or water" 

ELSE IF $SP-18159$ LIKE "Ash Urns" 
AND $SP-24133$ LIKE "Semi-Round" 
    THEN "Semi-round ash urn fits perfectly anywhere you want to keep free of cigarette litter"    
ELSE IF $SP-18159$ LIKE "Ash Urns" 
AND $SP-24133$ IS NOT NULL 
    THEN $SP-24133$_" ash urn fits perfectly anywhere you want to keep free of cigarette litter" 
ELSE IF $SP-18159$ LIKE "Ash Urns" 
    THEN "Ash urn fits perfectly anywhere you want to keep free of cigarette litter" 

ELSE IF $SP-18159$ LIKE "Lids" 
AND $SP-24133$ LIKE "Semi-Round" 
    THEN "Semi-round lid keeps your waste out of sight and minimizes odors"    
ELSE IF $SP-18159$ LIKE "Lids" 
AND $SP-24133$ IS NOT NULL 
    THEN $SP-24133$_" lid keeps your waste out of sight and minimizes odors" 
ELSE IF $SP-18159$ LIKE "Lids" 
    THEN "Lid keeps your waste out of sight and minimizes odors" 

ELSE IF $SP-18159$ LIKE "Sensor Trash Cans" 
AND $SP-24133$ LIKE "Semi-Round" 
    THEN "Semi-round sensor trash can provides hands-free lid operation for a more hygienic environment"        
ELSE IF $SP-18159$ LIKE "Sensor Trash Cans" 
AND $SP-24133$ IS NOT NULL 
    THEN $SP-24133$_" sensor trash can provides hands-free lid operation for a more hygienic environment"    
ELSE IF $SP-18159$ LIKE "Sensor Trash Cans" 
    THEN "Sensor trash can provides hands-free lid operation for a more hygienic environment" 

ELSE IF $SP-18159$ LIKE "Trash Cans w/Lid" 
AND $SP-24133$ LIKE "Semi-Round" 
    THEN "Semi-round trash can with lid lets you dispose of waste items efficiently"     
ELSE IF $SP-18159$ LIKE "Trash Cans w/Lid" 
AND $SP-24133$ IS NOT NULL 
    THEN $SP-24133$_" trash can with lid lets you dispose of waste items efficiently" 
ELSE IF $SP-18159$ LIKE "Trash Cans w/Lid" 
    THEN "Trash can with lid lets you dispose of waste items efficiently" 

ELSE IF $SP-18159$ LIKE "Pop-Up Bin"    
AND $SP-24133$ LIKE "Semi-Round" 
    THEN "Semi-round pop-up bin is exactly what you need for portability and utility" 
ELSE IF $SP-18159$ LIKE "Pop-Up Bin"    
AND $SP-24133$ IS NOT NULL     
    THEN $SP-24133$_" pop-up bin is exactly what you need for portability and utility" 
ELSE IF $SP-18159$ LIKE "Pop-Up Bin" 
    THEN "Pop-up bin is exactly what you need for portability and utility" 

ELSE "@@"; 

--Trash can & recycling bin capacity (Gallons) 
IF $SP-18118$ IS NOT NULL 
AND $SP-18118$ < 15
    THEN "Storage capacity of "_$SP-18118$_" gal. to easily accommodate all your waste" 
ELSE IF $SP-18118$ >= 15    
    THEN $SP-18118$_" gal. capacity for large amounts of trash" 
ELSE "@@";

-- True Color & Material of item 
IF $SP-21408$ LIKE "%steel" 
AND $SP-22967$ LIKE "%steel" --true color
THEN "Made of "_$SP-22967$_" for long-lasting use" 
ELSE IF $SP-21408$ IS NOT NULL 
AND $SP-22967$ IS NOT NULL --true color
    THEN "Made of "_$SP-22967$_" "_$SP-21408$_" for long-lasting use" 
ELSE IF $SP-21408$ IS NOT NULL 
    THEN "Made from high-quality "_$SP-21408$_" for strength and durability" 
ELSE IF $SP-22967$ IS NOT NULL 
    THEN $SP-22967$_" color makes it the perfect fit for any decor" 
ELSE "@@";  

--Dimensions (in Inches): Height x Width x Depth 
IF $SP-24133$ LIKE "Round" 
AND $SP-20654$ IS NOT NULL --H 
AND COALESCE($SP-21044$,$SP-20657$) IS NOT NULL
    THEN "Dimensions: "_$SP-20654$_"""H x "_$SP-21044$_"""Dia." 
ELSE IF $SP-20654$ IS NOT NULL --H
AND $SP-21044$ IS NOT NULL --W
AND $SP-20657$ IS NOT NULL --D
    THEN "Dimensions: "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 
ELSE "@@"; 

--Lid type; including locking/fastening system
IF $SP-21195$ LIKE "flip-top" 
    THEN "Flip lid opens easily and gently swings back into place" 
ELSE IF $SP-21195$ LIKE "push-door" 
    THEN "Push-door lid ensures zero spillage" 
ELSE IF $SP-21195$ LIKE "Automatic" 
    THEN "Motion-activated lid opens automatically with just the wave of your hand" 
ELSE IF $SP-21195$ LIKE "Hinged" 
AND A[6603].Value LIKE "yes" 
    THEN "Hinged lid opens via a step-on foot pedal" 
ELSE IF $SP-21195$ LIKE "Hinged" 
    THEN "Hinged lid ensures zero spillage" 
ELSE IF $SP-21195$ LIKE "Removable" 
OR COALESCE(A[6609].Where("% lid","% lid %","lid %").Values,A[6601].Value) IS NOT NULL
    THEN "Lid ensures zero spillage" 
ELSE "@@";

--Rim/Border Details
IF A[6609].Where("rolled rims").Values IS NOT NULL 
    THEN "Rolled rims add durability and are a breeze to clean" 
ELSE IF A[6609].Values.Where("% rim", "% rims") IS NOT NULL 
    THEN A[6609].Values.Where("% rim", "% rims").First()_" for added strength" 
ELSE "@@";

--Handle details; style & material (If applicable)
IF DC.Ksp.Values.Where("%side grip handle%") IS NOT NULL 
AND DC.Ksp.Values.Where("%Bottom handle%") IS NOT NULL 
    THEN "Bottom side handling grips for easy lifting, tilting, or pouring" 

ELSE IF A[6609].Where("rounded handles").Values IS NOT NULL 
    THEN "Rounded handles reduce strain on hands while making lifting easier" 
ELSE IF A[6609].Values.Where("handling grips", "%handles", "%handle") IS NOT NULL 
    THEN A[6609].Values.Where("handling grips", "%handles", "%handle").ToLower(true).FlattenWithAnd().ToUpperFirstChar()
    _" for easy lifting and movement" 
ELSE IF $SP-299$ IS NOT NULL 
    THEN "Handles for easy lifting" 
ELSE "@@";

--Safety features (If applicable)
IF COALESCE(A[6609].Where("%fire-resistant%").Values, 
    A[6817].Value) IS NOT NULL 
AND A[6609].Values.Where("%resist%","%proof%").WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Count > 1 
    THEN "Resistant to fire, "_A[6609].Values.Where("%resist%","%proof%").WhereNot("% lid %", "% lid", "%fingerprint%","%fire%").Erase("-resistant").Erase("proof").FlattenWithAnd()_" to provide you with years of service" 
ELSE IF COALESCE(A[6609].Where("%fire-resistant%").Values, 
    A[6817].Value) IS NOT NULL 
AND A[6609].Values.Where("%resist%","%proof%").WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Count = 1
    THEN "Resistant to fire and "_A[6609].Values.Where("%resist%","%proof%").WhereNot("% lid %", "% lid", "%fingerprint%","%fire%").Erase("-resistant").Erase("proof").FlattenWithAnd()_" to provide you with years of service" 
ELSE IF COALESCE(A[6609].Where("%fire-resistant%").Values, 
    A[6817].Value) IS NOT NULL 
    THEN "Firesafe construction will not burn, melt, or emit toxic fumes" 
ELSE IF A[6609].Values.Where("%resist%","%proof%").WhereNot("% lid %", "% lid","%fingerprint%").Count > 1 
    THEN "Resistant to "_A[6609].Values.Where("%resist%","%proof%").WhereNot("% lid %", "% lid", "%fingerprint%").Erase("-resistant").Erase("proof").FlattenWithAnd()_" to provide you with years of service" 
ELSE IF A[6609].Where("%weather-resistant%").Values IS NOT NULL 
    THEN "Weather-resistant construction for year-round use" 
ELSE IF A[6609].Where("%UV-resistant%").Values IS NOT NULL 
    THEN "UV-resistant to limit fading and provide you with years of service" 
ELSE IF A[6609].Values.Where("%resist%","%proof%").WhereNot("% lid %", "% lid","%fingerprint%") IS NOT NULL
    THEN "Resistant to "_A[6609].Values.Where("%resist%","%proof%").WhereNot("% lid %", "% lid", "%fingerprint%").Erase("-resistant").Erase("proof").FlattenWithAnd()_" to provide you with years of service" 
ELSE "@@"; 

--Certifications & Standards (If applicable)    
IF $SP-21659$ IS NOT NULL
    THEN "Meets or exceeds "_REF["SP-21659"].Erase(" compliant")_" standards" 
ELSE "@@";

--Use for additional product and/or manufacturer information relevant to the customer buying decision 

--security
IF A[6609].Values.Where("slide lock") IS NOT NULL 
    THEN "A slide lock securely locks the lid to help keep pets and curious children from getting into the trash" 
ELSE IF $SP-21194$ LIKE "Yes" 
    THEN "Locks for secure use" 
ELSE IF A[6609].Values.Where("screw closure","tie-down rings") IS NOT NULL 
    THEN A[6609].Values.Where("screw closure","tie-down rings").FlattenWithAnd().ToUpperFirstChar()
    _" help make security simple";

--hands-free operation
IF $SP-18159$ IN ("Sensor Trash Cans")
AND A[6609].Values.Where("odorless") IS NOT NULL 
    THEN "100% touch-free, odor-free and eliminates cross-contamination of germs" 
ELSE IF $SP-18159$ IN ("Step Trash Cans", "Sensor Trash Cans") 
    THEN "Touch-free feature prevents the cross-contamination of germs";

--pedal
IF A[6609].Values.Where("steel pedal") IS NOT NULL 
    THEN "Strong steel pedal is engineered for a smooth and easy step";

IF A[6609].Values.Where("fingerprint-resistant coating") IS NOT NULL 
    THEN "Fingerprint-resistant coating is easy to clean" 
ELSE IF A[6609].Values.Where("dent-proof plastic lid") IS NOT NULL
    THEN "Dent-proof plastic lid won't show dirt or fingerprints";

    --cfg
IF A[6609].Values.Where("Carbon filter gate%") IS NOT NULL     
    THEN "Equipped with Carbon Filter Gate (CFG) to neutralize the toughest odors";

--base 
IF A[6609].Values.Where("anti-slip base") IS NOT NULL 
    THEN "Non-skid base to prevent the bin from sliding" 
ELSE IF A[6609].Values.Where("solid base") IS NOT NULL 
    THEN "Solid base keeps potential liquid spills contained" 
ELSE IF A[6609].Values.Where("Reinforced base") IS NOT NULL 
    THEN "Reinforced base to reduce wear and tear from dragging";

--stay-open function
IF A[6609].Values.Where("stay-open function") IS NOT NULL 
    THEN "The lid stays open for as long as you like — perfect for longer chores";

--liner pocket    
IF A[6609].Values.Where("liner pocket") IS NOT NULL 
    THEN "Liner pocket keeps liners where you need them and dispenses them one by one from inside the can for a faster liner change";

--removable inner bucket
IF A[6609].Values.Where("removable inner bucket") IS NOT NULL     
    THEN "Inner trash bucket is fully removable for easy emptying and cleaning";

--lid shox technology/silent lid
IF A[6609].Values.Where("lid shox technology") IS NOT NULL 
    THEN "Lid shox technology controls the motion of the lid for a slow, silent close" 
ELSE IF A[6609].Values.Where("silent%lid") IS NOT NULL 
    THEN "Lid closes slowly with whisper quiet operation";

--venting channels
IF A[6609].Values.Where("Venting channels") IS NOT NULL 
    THEN "Venting channels reduce the force needed to lift each trash bin";

--internal hinge
IF A[6609].Values.Where("internal hinge") IS NOT NULL 
    THEN "Internal hinge prevents the lid from bumping the wall";

--wheels    
IF A[6604].Values.Where("wheels") IS NOT NULL 
    THEN "Wheels make the can easy to move";

--power
IF A[335].Value IS NOT NULL 
AND A[861].Value IS NOT NULL 
    THEN "Powered by "_A[861].Value.Postfix(" ")_A[335].Value_" batteries";