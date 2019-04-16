//§§168328278181141928 "Trash Cans & Waste Receptacles" "Alex K."

TypeOfTrashCanAndUse();
TrashCanCapacity();
//Color & Material of item
//Dimensions
LidType();
HandleDetails();

// --[FEATURE #1]
// --Type of trash can & use
void TypeOfTrashCanAndUse() {
    var result = "";
    //Step Trash Cans|Swing Lid Trash Cans|Ash Urns|Lids|Sensor Trash Cans|Trash Cans w/Lid|Trash Cans w/ no Lid|Pop-Up Bin
    var type = R("SP-18159").HasValue() ? R("SP-18159").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18159").HasValue() ? R("cnet_common_SP-18159").Replace("<NULL>", "").Text : "";
    //Square|Pentagon|Round|Rectangular|Hexagon|Octagon|Semi-Round
    var receptacleShape = R("SP-24133").HasValue() ? R("SP-24133").Replace("<NULL>", "").Text :
		R("cnet_common_SP-24133").HasValue() ? R("cnet_common_SP-24133").Replace("<NULL>", "").Text : "";
    var cigaretteReceptacle = A[6599];
    
    if (!String.IsNullOrEmpty(type)) {
        if (type.ToLower().Equals("trash cans w/ no lid")) {
            if (!String.IsNullOrEmpty(receptacleShape)) {
                result = $"{receptacleShape.ToLower().ToUpperFirstChar()} trash can without lid ensures clean and litter-free space";
            } else {
                result = "Trash can without lid ensures clean and litter-free space";
            }
        }
        else if (type.ToLower().Equals("step trash cans")) {
            if (!String.IsNullOrEmpty(receptacleShape)) {
                result = $"{receptacleShape.ToLower().ToUpperFirstChar()} step trash can ensures clean and litter-free space and provides hands-free operation";
            } else {
                result = "Step trash can ensures clean and litter-free space and provides hands-free operation";
            }
        }
        else if (type.ToLower().Equals("swing lid trash cans")) {
            if (!String.IsNullOrEmpty(receptacleShape)) {
                result = $"{receptacleShape.ToLower().ToUpperFirstChar()} swing lid trash can allows easy trash disposal and conceals odors within the basket";
            } else {
                result = "Swing-lid trash can allows easy trash disposal and conceals odors within the basket";
            }
        }
        else if (type.ToLower().Equals("ash urns") && cigaretteReceptacle.HasValue("cigarette receptacle")) {
            if (!String.IsNullOrEmpty(receptacleShape)) {
                result = $"{receptacleShape.ToLower().ToUpperFirstChar()} cigarette receptacle extinguishes cigarettes without sand or water";
            } else {
                result = "Cigarette receptacle extinguishes cigarettes without sand or water";
            }
        }
        else if (type.ToLower().Equals("ash urns")) {
            if (!String.IsNullOrEmpty(receptacleShape)) {
                result = $"{receptacleShape.ToLower().ToUpperFirstChar()} ash urn fits perfectly anywhere you want to keep free of cigarette litter";
            } else {
                result = "Ash urn fits perfectly anywhere you want to keep free of cigarette litter";
            }
        }
        else if (type.ToLower().Equals("lids")) {
            if (!String.IsNullOrEmpty(receptacleShape)) {
                result = $"{receptacleShape.ToLower().ToUpperFirstChar()} lid keeps your waste out of sight and minimizes odors";
            } else {
                result = "Lid keeps your waste out of sight and minimizes odors";
            }
        }
        else if (type.ToLower().Equals("sensor trash cans")) {
            if (!String.IsNullOrEmpty(receptacleShape)) {
                result = $"{receptacleShape.ToLower().ToUpperFirstChar()} sensor trash can provides hands-free lid operation for a more hygienic environment";
            } else {
                result = "Sensor trash can provides hands-free lid operation for a more hygienic environment";
            }
        }
        else if (type.ToLower().Equals("trash cans w/lid")) {
            if (!String.IsNullOrEmpty(receptacleShape)) {
                result = $"{receptacleShape.ToLower().ToUpperFirstChar()} trash can with lid lets you dispose of waste items efficiently";
            } else {
                result = "Trash can with lid lets you dispose of waste items efficiently";
            }
        }
        else if (type.ToLower().Equals("pop-up bin")) {
            if (!String.IsNullOrEmpty(receptacleShape)) {
                result = $"{receptacleShape.ToLower().ToUpperFirstChar()} pop-up bin is exactly what you need for portability and utility";
            } else {
                result = "Pop-up bin is exactly what you need for portability and utility";
            }
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TypeOfTrashCanAndUse⸮{result}");
    }
}

// --[FEATURE #2]
// --Trash can & recycling bin capacity (Gallons)
void TrashCanCapacity() {
    var result = "";
    //Step Trash Cans|Swing Lid Trash Cans|Ash Urns|Lids|Sensor Trash Cans|Trash Cans w/Lid|Trash Cans w/ no Lid|Pop-Up Bin
    var capacity = R("SP-18118").HasValue() ? R("SP-18118").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18118").HasValue() ? R("cnet_common_SP-18118").Replace("<NULL>", "").Text : "";
    
    if (!String.IsNullOrEmpty(capacity)) {
        if (Coalesce(capacity).ExtractNumbers().Any()) {
            if(Coalesce(capacity).ExtractNumbers().First() < 15) {
                result = $"Storage capacity of {capacity} gal. to easily accommodate all your waste";
            }
            else {
                result = $"{capacity} gal. capacity for large amounts of trash";
            }
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TrashCanCapacity⸮{result}");
    }
}

// --[FEATURE #3]
// --Color & Material of item

// --[FEATURE #4]
// --Dimensions (in Inches): Height x Width x Depth

// --[FEATURE #5]
// --Lid type; including locking/fastening system
void LidType() {
    var result = "";
    //No Lid|Removable|Hinged|Automatic|Push-door|Flip-Top|Locking
    var binLidType = R("SP-21195").HasValue() ? R("SP-21195").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21195").HasValue() ? R("cnet_common_SP-21195").Replace("<NULL>", "").Text : "";
	var footPedal =  A[6603];
	var features = Coalesce(A[6609]);
	var closureType = A[6601];
    
    if (!String.IsNullOrEmpty(binLidType)) {
        if (binLidType.ToLower().Equals("flip-top")) {
            result = "Flip lid opens easily and gently swings back into place";
        }
        else if (binLidType.ToLower().Equals("push-door")) {
            result = "Push-door lid ensures zero spillage";
        }
        else if (binLidType.ToLower().Equals("automatic")) {
            result = "Motion-activated lid opens automatically with just the wave of your hand";
        }
        else if (binLidType.ToLower().Equals("hinged")) {
            if (footPedal.HasValue()) {
                result = "Hinged lid opens via a step-on foot pedal";
            } else {
                result = "Hinged lid ensures zero spillage";
            }
        }
        else if (binLidType.ToLower().Equals("removable") 
        || features.HasValue("% lid","% lid %","lid %")
        || closureType.HasValue()) {
            result = "Lid ensures zero spillage";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"LidType⸮{result}");
    }
}

// --[FEATURE #6]
// --Rim/Border Details
RimBorderDetails();
void RimBorderDetails() {
    var result = "";
	var features = A[6609];
    if (features.HasValue("rolled rims")) {
        result = "Rolled rims add durability and are a breeze to clean";
    }
    else if (Coalesce(features).HasValue("% rim", "% rims")) {
        result = $"{features.Where("% rim", "% rims").Select(o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} for added strength";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"RimBorderDetails⸮{result}");
    }
}

// --[FEATURE #7]
// --Handle details; style & material (If applicable)
void HandleDetails() {
    var result = "";
	var features = A[6609];
    var gripHandle = DC.KSP.GetString().ToLower();
    var handle = R("SP-299").HasValue() ? R("SP-299").Replace("<NULL>", "").Text :
		R("cnet_common_SP-299").HasValue() ? R("cnet_common_SP-299").Replace("<NULL>", "").Text : "";

    if (gripHandle.In("%side grip handle%") && gripHandle.In("%bottom handle%")) {
        result = "Bottom side handling grips for easy lifting, tilting, or pouring";
    }
    else if (features.HasValue("rounded handles")) {
        result = "Rounded handles reduce strain on hands while making lifting easier";
    }
    else if (features.HasValue() &&  Coalesce(features).HasValue("handling grips", "%handles", "%handle")) {
        result = $"{features.Where("handling grips", "%handles", "%handle").Select(o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} for easy lifting and movement";
    }
    else if (!String.IsNullOrEmpty(handle)) {
        result = "Handles for easy lifting";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"HandleDetails⸮{result}");
    }
}

// --[FEATURE #8]
// --Safety features (If applicable)

// --[FEATURE #9]
// --Certifications & Standards (If applicable)

// --[FEATURE #10]
// --Additional

// --[FEATURE #11]
// --Additional

// --[FEATURE #12]
// --Additional

//168328278181141928 end of "Trash Cans & Waste Receptacles" §§