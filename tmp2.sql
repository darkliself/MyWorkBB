--Sponge/hand pad type, Grade & Use    
IF $SP-18150$ LIKE "Scouring Pad" 
    THEN "Scouring pad offers gentle action and cleans the toughest places with ease" 
ELSE IF $SP-18150$ LIKE "Floor Pad" 
    THEN "Floor pad can be used for everyday cleaning, remove light soil, scuff marks, and black heel marks" 
ELSE IF $SP-18150$ LIKE "Steel Wool Pad" 
    THEN "Steel wool pad cuts through grease, rust and soil" 
ELSE IF $SP-18150$ LIKE "Stainless Steel Pad" 
    THEN "Steel wool pad cuts through grease, rust and soil" 
ELSE IF $SP-18150$ LIKE "Scrubber" 
    THEN "Scrubber breaks up of tough dirt with ease" 
ELSE IF $SP-18150$ LIKE "Set" 
    THEN "Set let you tackle the cleaning of large surfaces and tough messe" 
ELSE IF $SP-18150$ LIKE "Pad Holder" 
    THEN "Pad holder is designed to make clean up jobs faster, easier or safer" 
ELSE IF $SP-18150$ LIKE "Polishing Pad" 
    THEN "Polishing pad removes soil and scuff marks with minimal dulling of finish" 
ELSE IF $SP-18150$ LIKE "Stripper" 
    THEN "Stripper removes dirt, spills and scuffs leaving a clean surface ready for recoating " 
ELSE IF $SP-18150$ LIKE "Sponge" 
    THEN "Sponge quickly wipes up spills and messes and can carry cleaning solutions to the work surface" 
ELSE IF $SP-18150$ IS NOT NULL 
    THEN $SP-18150$_" makes your cleaning job easier and more effective" 
ELSE "@@";

--Material & True color (Include handle & bristles if applicable)    
IF $SP-22967$ NOT IN (NULL, "Assorted", "Multicolor") 
AND $SP-17447$ IS NOT NULL 
    THEN $SP-22967$_" and made of "_$SP-17447$_" for durability" 
ELSE IF $SP-17447$ IS NOT NULL 
    THEN "Made of "_$SP-17447$_" for durability" 
ELSE IF $SP-22967$ NOT IN (NULL, "Assorted", "Multicolor")
    THEN "Comes in "_$SP-22967$ 
ELSE IF $SP-22967$ NOT IN ("Assorted")
    THEN "Comes in assorted colors, so you can use a different color for each type of job" 
ELSE "@@";

--Level of use    
IF $SP-20642$ LIKE "Heavy" 
    THEN "Heavy-duty for tough cleaning jobs" 
ELSE IF $SP-20642$ LIKE "Medium" 
    THEN "Medium-duty for everyday general-purpose cleaning jobs" 
ELSE IF $SP-20642$ LIKE "Light" 
    THEN "Ideal for light duty jobs" 
ELSE "@@";

--Suitable surface    
IF A[6783].Values IS NOT NULL 
    THEN "Great for cleaning "_A[6783].Values.ToLower(true).FlattenWithAnd()
ELSE "@@";

--Dimensions (in Inches): H x W x D
--Dimensions (in Inches): H x W x D    
IF $SP-20654$ IS NOT NULL --H
AND $SP-21044$ IS NOT NULL --W
AND $SP-20657$ IS NOT NULL --D
    THEN "Dimensions: "_$SP-20654$_"""H x "_$SP-21044$_"""W x "_$SP-20657$_"""D" 
ELSE IF $SP-21044$ IS NOT NULL --W
AND COALESCE($SP-20657$, $SP-20654$) IS NOT NULL --L
    THEN "Dimensions: "_$SP-21044$_"""W x "_COALESCE($SP-20657$,$SP-20654$)_"""L" 
ELSE IF $SP-21453$ IS NOT NULL 
    THEN "Dimensions: "_$SP-21453$_"""Dia." 
ELSE "@@";

--Construction/Design Features (Include handle & bristles)    
IF A[6786].Where("%heat-resistant bristles%").Values IS NOT NULL 
THEN "High heat resistant bristles offer long term economy" 
ELSE IF A[6786].Where("%angled bristle%").Values IS NOT NULL 
THEN "Angled bristle for efficient cleaning" 
ELSE IF A[6786].Where("%soft bristle brush%").Values IS NOT NULL 
THEN "Softer bristles for delicate cleaning of jars, bottles, and glasses" 
ELSE IF A[6786].Where("%stiff bristle%").Values IS NOT NULL OR A[6786].Where("%hard bristle brush%").Values IS NOT NULL
THEN "Stiff bristles provide aggressive cleaning" 
ELSE IF A[6786].Where("%long handle%").Values IS NOT NULL
THEN "Long handle is useful for cleaning needs" 
ELSE IF A[6786].Where("%heat-resistant handle%").Values IS NOT NULL 
THEN "Heat-resistant handle helps protect hands from burns, grease and grime" 
ELSE IF A[6786].Where("%molded ridges%").Values IS NOT NULL 
THEN "Comfortable molded ridges" 
ELSE IF A[6786].Where("%hanging hole%").Values IS NOT NULL AND A[6786].Where("%plastic handle%").Values IS NOT NULL THEN "Plastic handle with convenient hole for hanging" 
ELSE IF A[6786].Where("%flexible handle%").Values IS NOT NULL AND A[6786].Where("%plastic handle%").Values IS NOT NULL THEN "Flexible handle for getting into corners and around curves" 
ELSE "@@";

--Bristle trim length (If Applicable)--oos    

--Pack qty    
IF Request.Data["TX_UOM"] LIKE "%/%" 
THEN Request.Data["TX_UOM"].Split("/").First()_" per " 
    _Request.Data["TX_UOM"].Split("/").Last().ToLower()
ELSE "@@";

--Use for additional product and/or manufacturer information relevant to the customer buying decision
IF A[6786].Where("%BPA-free%").Values IS NOT NULL 
    THEN "BPA-free to address safety concerns";

--Add feauture
IF $SP-18150$ LIKE "Set" AND A[7094].Values IS NOT NULL
THEN "Set includes: "_A[7094].Values.Flatten(",");

--Water-activated micro-scrubber
IF DC.Ksp.Values.Where("%Water%activated micro%scrubber%") IS NOT NULL 
    THEN "Water-activated micro-scrubbers reach into surface grooves, lifting away the toughest soils";

-- built-in scourer 
IF A[6786].Values.Where("built-in scourer", "scouring pad")    IS NOT NULL
AND $SP-18150$ NOT LIKE "Scouring Pad" 
    THEN "One side is a scouring pad for scrubbing and cleaning";

--soap filling
IF A[6786].Values.Where("soap filling") IS NOT NULL
    THEN "Pre-loaded with soap to remove tough messes";

--sanitizing
IF A[7092].Values.Where("%dishwasher%") IS NOT NULL 
    THEN "Sanitize in the dishwasher" 
ELSE IF A[6786].Values.Where("washable%") IS NOT NULL 
AND A[7092].Values.Where("machine washable") IS NOT NULL
    THEN "Machine "_A[6786].Values.Where("washable%").First().ToUpperFirstChar()_", reducing waste and creating a more eco-friendly environmentn" 
ELSE IF A[6786].Values.Where("washable%") IS NOT NULL 
    THEN A[6786].Values.Where("washable%").First().ToUpperFirstChar()_", reducing waste and creating a more eco-friendly environment" 
ELSE IF A[7092].Values.Where("machine washable") IS NOT NULL
    THEN "Machine washable for multiple uses" 
ELSE IF A[7092].Values IS NOT NULL
    THEN "Washable for multiple uses";

--scent 
IF A[7356].Values IS NOT NULL 
    THEN "Leaves "_A[7356].Values.FlattenWithAnd()_" scent";

--Additional(Certifications & Standards)
IF A[380].Values IS NOT NULL 
THEN "Meets or exceeds "_A[380].Values.Flatten(",")_" standards";

--recycled content
IF A[6628].Value IS NOT NULL 
    THEN "Made with "_A[6628].Value_"% recycled materials"; 

--grip
IF A[6786].Values.Where("non-slip grip") IS NOT NULL 
OR DC.Ksp.Values.Where("%non%slip grip%") IS NOT NULL
    THEN "Non-slip grip for more comfortable to use";