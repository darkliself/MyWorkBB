//§§135293970167205 "Binders" "Alex K."

BinderTypeDurabilityColor();
// Dimentions
RingTypeClosure();
BinderCapacity();
InputSize();
MaterialOfItemFinish();
DesignIncludesPockets();
//pack size
//Recycled
IncludedLabels();
DuraHinge();
HandleStrap();

// --[FEATURE #1]
// --Binder Type, Durability & Color
void BinderTypeDurabilityColor() {
    var result = "";
    //Post Binders|Flexible Poly Binders|Hanging Binder|Binding Case|Mini Binders|Special Application Binder|Micro Binders|A4 Binders|Zipper Binders|View Binders|Fashion Binders|Non-View Binders|Ledger Binders|Legal Binders|Better Binders
    var binderType = R("SP-19663").HasValue() ? R("SP-19663").Replace("<NULL>", "").Text : R("cnet_common_SP-19663").HasValue() ? R("cnet_common_SP-19663").Replace("<NULL>", "").Text : "";
	var trueColor = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text :  "";
	//Standard|Heavy Duty|Economy
	var durabilityType = R("SP-21022").HasValue() ? R("SP-21022").Replace("<NULL>", "").Text : R("cnet_common_SP-21022").HasValue() ? R("cnet_common_SP-21022").Replace("<NULL>", "").Text : "";
	if (!String.IsNullOrEmpty(durabilityType)) {
	    durabilityType = $"with {durabilityType.ToLower()} durability ";
	}
	if (!String.IsNullOrEmpty(binderType)) {
	    if (!String.IsNullOrEmpty(trueColor) && binderType.ToLower().Equals("view binders")) {
	        var tmp = trueColor[0].ToString().ToUpper() + trueColor.Substring(1);
	        result = $"{tmp} view binder {durabilityType}is an excellent way to store spreadsheets";
	    }
	    else if (binderType.ToLower().Equals("ledger binders")) {
	        result = $"Ledger binders {durabilityType}is ideal for large spreadsheets, blueprints, engineering, mining, construction plans" ;
	    }
	    else if (binderType.ToLower().Equals("flexible poly binders")) {
	        result = $"Flexible poly construction {durabilityType}for easy carrying";
	    }
	    else if (!String.IsNullOrEmpty(trueColor)) {
	         var tmp = trueColor[0].ToString().ToUpper() + trueColor.Substring(1);
	         result = $"{tmp} {binderType.ToLower()} {durabilityType}protects and organizes work documents";
	    }
	    else {
	        var tmp = binderType[0].ToString().ToUpper() + binderType.Substring(1);
	        result = $"{tmp} {durabilityType}protects and organizes work documents";
	    }
	}
	if (!String.IsNullOrEmpty(result)) {
	    Add($"BinderTypeDurabilityColor⸮{result}");
	}
}

// --[FEATURE #2]
// --Dimensions: Height in Inches "x" Width in Inches "x" Depth in Inches

// --[FEATURE #3]
// --Closure (Ring type, Number of Rings & Locking Mechanism)

void RingTypeClosure() {
    var result = "";
    var numberOfRings = R("SP-130").HasValue() ? R("SP-130").Replace("<NULL>", "").Text : R("cnet_common_SP-130").HasValue() ? R("cnet_common_SP-130").Replace("<NULL>", "").Text : "";
    //D-Ring|Round Ring|Slant Ring|Grip|Straight Post|One Touch
    var ringType = R("SP-19662").HasValue() ? R("SP-19662").Replace("<NULL>", "").Text : R("cnet_common_SP-19662").HasValue() ? R("cnet_common_SP-19662").Replace("<NULL>", "").Text : "";
    var gapless = A[5921].HasValue("%gapless%");
    if (!String.IsNullOrEmpty(ringType)) {
        var plural = false;
        if (!String.IsNullOrEmpty(numberOfRings)) {
            if (Coalesce(numberOfRings).ExtractNumbers().First() > 1) {
                plural = true;
            }
            if (ringType.ToLower().StartsWith("d-ring"))  {
                if (gapless) {
                    if (plural) {
                        result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} gapless D-rings keep pages secure";
                    } else {
                        result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} gapless D-ring keep pages secure";
                    }
                } else {
                    if (plural) {
                        result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} D-rings keep pages secure";
                    } else {
                        result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} D-ring keep pages secure";
                    }
                }
            }
            else if (ringType.ToLower().StartsWith("ring")) {
                if (plural) {
                    result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType.ToLower()).Pluralize()} keep pages secure";
                } else {
                    result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType.ToLower())} keep pages secure";
                }
            }
            else {
                if (plural) {
                    result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType).ToLower()} rings keep pages secure";
                } else {
                    result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType).ToLower()} ring keep pages secure";
                }
            }
        }
        else {
            if (ringType.ToLower().Equals("one touch")) {
                result = "One touch rings open and close with ease and keep pages secure";
            }  
            else if (ringType.ToLower().Equals("d-ring")) {
                result = "D-rings have a higher page capacity compared to same-size round rings";
            }
            else if (ringType.ToLower().Equals("round ring")) {
                result = "Equipped with round rings for easy page turning";
            }
            else if (ringType.ToLower().Equals("ring binder")) {
                result = "Binder with rings provides document protection and a professional appearance";
            }
            else {
                result = $"Binder with {ringType.ToLower().Replace("ring", "").Replace("rings", "")} rings provides document protection and a professional appearance";
            }
        }
    } 
    
	if (!String.IsNullOrEmpty(result)) {
	    Add($"RingTypeClosure⸮{result}");
	}
}

// --[FEATURE #4]
// --Capacity (Number of pages)

void BinderCapacity() {
    var result = "";
    var capacity = A[6576];
    var capacity2 = A[5928];
    
    if (capacity.HasValue()) {
        result = $"Holds up to {capacity.FirstValue()} {capacity.Units.First().Name}";
    }
    else if (capacity2.HasValue()) {
        var tmp = capacity2.Values.First().ValueUSM.Replace(" in", "\"");
        result = $"Holds up to {tmp} documents";
    }
	if (!String.IsNullOrEmpty(result)) {
	    Add($"BinderCapacity⸮{result}");
	}
}

// --[FEATURE #5]
// --Input Size
void InputSize() {
    var result = "";
    //8.5" x 11" (US letter)|8" x 11"|14.88" x 11" (Continuous)|3" x 4"|14.88" x 10.98"|Other|check|15" x 18"|8.5" x 14" (legal)|8" x 10"|5" x 7"|13" x 19"|5.5" x 8.5"|12" x 18"|8" x 5"|8" x 10.5"|11" x 9.0625"|6" x 9"|8.5" x 11.75"|12" x 8.5"|11" x 14"|4" x 6"|12" x 15"|9" x 12"|13" x 16.75"|12" x 12"|6.5" x 8.5"|9.5" x 11"|3.5" x 5.5"|7.75" x 5"|A4|3" x 5"|11" x 17"
    var folderOrPaperSize = R("SP-18288").HasValue() ? R("SP-18288").Replace("<NULL>", "").Text : R("cnet_common_SP-18288").HasValue() ? R("cnet_common_SP-18288").Replace("<NULL>", "").Text : "";
    var supportedFormat = A[5925]; // Multi Value
    var supportedFormatUSM = "";
    if (supportedFormat.HasValue() && supportedFormat.FirstValueUsm().ToString().Contains(" in")) {
            supportedFormatUSM = supportedFormat.FirstValueUsm().ToString();
    }
    if (!String.IsNullOrEmpty(folderOrPaperSize)) {
        if (folderOrPaperSize.ToLower().Contains("11\" x 17\"") 
        || supportedFormatUSM.Contains("Ledger") 
        || supportedFormatUSM.Contains("8.5 in x 11 in")) {
           result = "Accommodates ledger-size pages";
        }
        else if (folderOrPaperSize.ToLower().Contains("legal") 
        || supportedFormatUSM.Contains("8.5 in x 14 in") 
        || supportedFormatUSM.Contains("Legal")) {
           result = "Accommodates legal-size pages";
        }
        else if (folderOrPaperSize.ToLower().Contains("us letter") 
        || supportedFormatUSM.Contains("Letter") 
        || supportedFormatUSM.Contains("8.5 in x 11 in")) {
           result = "Accommodates letter-size pages";
        }
        else if (folderOrPaperSize.ToLower().Contains("check")) {
            result = "Protect checks and keep them organized";
        }
        
        else if (!String.IsNullOrEmpty(folderOrPaperSize) && !folderOrPaperSize.ToLower().Contains("other")) {
            result = $"Accommodates {folderOrPaperSize.Replace(" (Continuous)", "")} size pages";
        }
    }
    else if (!String.IsNullOrEmpty(supportedFormatUSM)) {
        var tmp = Coalesce(supportedFormatUSM.Split("(").Last()).Replace(" in", "\"").Replace(")", "");
        result = $"Accommodates {tmp} size pages";
    }
	if (!String.IsNullOrEmpty(result)) {
	    Add($"InputSize⸮{result}");
	}
}

// --[FEATURE #6]
// --Material of Item & Finish
void MaterialOfItemFinish() {
    var result = "";
    var material = "Some(pvc) ";//R("SP-21408");
    var surface = A[5945];
    if (!String.IsNullOrEmpty(material)) {
        var tmp = material.ToLower().Replace("(abs)", "(ABS)").Replace("(pvc)", "(PVC)");
        if (surface.HasValue("%non%stick%")) {
            result = $"Made of {tmp} with non-stick surface";
        }
        else if (surface.HasValue("%surface%")) {
            var tmp1 = surface.Where("%surface%").First().Value();
            result = $"Made of {material.ToLower()} with {tmp1}";
        }
        else {
            result = $"Made of {tmp}";
        }
    }     
	if (!String.IsNullOrEmpty(result)) {
	    Add($"MaterialOfItemFinish⸮{result}");
	}
}

// --[FEATURE #7]
// --Design (includes pockets)

void DesignIncludesPockets() {
    var result = "";
    var designFeature = A[5945];
    var design = A[5965];
    
    if (designFeature.HasValue("retractable hooks")) {
        result = "Retractable storage hooks for single-point or drop-file systems";
    }
    else if (design.HasValue()) {
        var tmp  = design.WhereNot("%spine%","front cover full size pocket", "back cover full size pocket", "front cover pocket", "back cover pocket").Match(5971, 5965).Values("x").Where(o => o.In("%inner%pocket%")).Flatten(" ").Replace("<NULL>", "1");
        //Add(tmp);
        if (tmp.ExtractNumbers().Any() 
        && tmp.ExtractNumbers().Sum() == 1) {
            result = "One interior pocket for added organization";
        }
        else if (tmp.ExtractNumbers().Any()
        && tmp.ExtractNumbers().Sum() > 1) {
            result = $"{tmp.ExtractNumbers().Sum()} interior pockets for added organization"; 
        }
        else if (design.Where("%inner%pocket%").Count() > 1
        && !Coalesce(design.WhereNot("%inner%pocket%","front cover full size pocket", "back cover full size pocket", "front cover pocket", "back cover pocket", "%spine%")).HasValue()) {
            result = $"{design.Where("%inner%pocket%").Count()} interior pockets for added organization";
        }
        else if (design.Where("%pocket%").Count() == 1 
        && !Coalesce(design).HasValue("%spine%")
        && design.WhereNot("%spine%").Match(5971, 5965).Values("x").Where(o => o.In("%pocket%")).Flatten().Replace("<NULL>", "1").ExtractNumbers().Any()
        && design.WhereNot("%spine%").Match(5971, 5965).Values("x").Where(o => o.In("%pocket%")).Flatten().Replace("<NULL>", "1").ExtractNumbers().Max() < 2) {
            var tmp1 = design.WhereNot("%spine%").Match(5971, 5965).Values("x") // get items where not "spine"
                .Where(o => o.In("%pocket%")) // checked item for pockets
                    .Take(3)
                    .Distinct()
                    .FlattenWithAnd(3, " ")
                    .Replace("4", "Four")
                    .Replace("full size", "full-size")
                    .Replace(" pockets", "")
                    .Replace(" pocket", "")
                    .Replace("<NULL>", "")
                    .ToString();
            tmp1 = tmp1[0].ToString().ToUpper() + tmp1.Substring(1).ToLower();
            result = $"{tmp1}  pocket for added organization";
        }
        else if (design.WhereNot("%spine%").Match(5971, 5965).Values("x").Where(o => o.In("%pocket%")).Flatten().Replace("<NULL>", "1").ExtractNumbers().Any()
        && design.WhereNot("%spine%").Match(5971, 5965).Values("x").Where(o => o.In("%pocket%")).Flatten().Replace("<NULL>", "1").ExtractNumbers().Sum() > 0) {
            var tmp1 = design.WhereNot("%spine%").Match(5971, 5965).Values("x") // get items where not "spine"
                .Where(o => o.In("%pocket%")) // checked item for pockets
                .Flatten()
                .Replace("<NULL>", "1")
                .ExtractNumbers()
                .Sum()
                .ToString();
            tmp1 = tmp1.Equals("1") ? "One pocket" : $"{tmp1} pockets";
            result = $"{tmp1} for added organization";
        }
        else if (design.Where("%pocket%").Count() > 0) {
            var tmp1 = design.Where("%pocket%").Count().ToString();
            tmp1 = tmp1.Equals("1") ? "One pocket" : $"{tmp1} pockets";
            result = $"{tmp1} for added organization";
        }

    }
	if (!String.IsNullOrEmpty(result)) {
	    Add($"DesignIncludesPockets⸮{result}");
	}
}

// --[FEATURE #8]
// --Binder Pack Size (If more than 1)

// --[FEATURE #9]
// --Additional Recycled

// --[FEATURE #10]
// --Additional Lables

void IncludedLabels() {
    var result = "";
    var labels = Coalesce(A[5942]);
    var spineLabelHolder = A[6577];
    if (labels.HasValue()) {
        var tmp = labels.Values.Select(o => o.Value().Replace(" label", "")).FlattenWithAnd(10, ", ");
        if (labels.Values.Count() > 1) {
            result = $"Insert custom {tmp} labels for easy identification";
        }
        else {
            result = $"Insert custom {tmp} labelsfor easy identification";
        }
    }
    else if (spineLabelHolder.HasValue("Yes")) {
        result = "Insert a custom label into the spine pocket for easy identification";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"IncludedLabels⸮{result}");
    }
}

// --[FEATURE #11]
// --Additional DuraHinge

void DuraHinge() {
    var result = "";
    var duraEdgeHinge = Coalesce(A[5945]);
    if (duraEdgeHinge.Where("%Dura%").Count() > 1) {
        result = "Binder features a ##DuraHinge design that's stronger, lasts longer, and resists tearing and a ##DuraEdge that makes the sides and top more pliable to resist splitting";
    }
    else if (duraEdgeHinge.HasValue("DuraHinge")) {
        result = "Binder features a ##DuraHinge design that's stronger, lasts longer, and resists tearing"; 
    }
    else if (duraEdgeHinge.HasValue("DuraEdge")) {
        result = "##DuraEdge makes the sides and top more pliable to resist splitting";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"DuraHinge⸮{result}");
    }
}

// --[FEATURE #12]
// --Additional Handle $ Strap

void HandleStrap() {
    var result = "";
    var shoulderStrap = A[5923];
    var handles = A[7065];
    if (shoulderStrap.HasValue("%shoulder%strap%") && handles.HasValue()) {
        result = "Comes with a handle and shoulder strap, so it is more comfortable to tote this binder around";
    }
    else if (shoulderStrap.HasValue("%shoulder%strap%")) {
        result = "Comes with shoulder strap, so it is more comfortable to tote this binder around without a backpack or a case"; 
    }
    else if (handles.HasValue()) {
        result = "Comes with a handle, so it is more comfortable to tote this binder around without a backpack or a case";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"HandleStrap⸮{result}");
    }
}

//§§135293970167205 and of "Binders"
