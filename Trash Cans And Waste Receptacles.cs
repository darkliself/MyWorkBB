//§§168328278181141928 "Trash Cans & Waste Receptacles" "Alex K."

TypeOfTrashCanAndUse();
TrashCanCapacity();
//Color & Material of item
//Dimensions
LidType();
HandleDetails();
//Safety features
// Standards
SlideLockSecurity();
HandsFreeOperation();
TrashCansPedal();
FingerPrintResistantCoating();
CarbonFilterGate();
TrashCanBase();
StayOpenFunction();
LinerPocket();
RemovableInnerBucket();
ShoxSilentLid();
VentingChannels();
InternalHinge();
TrashCanWheels();

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
// --Additional Slide Lock Security
void SlideLockSecurity() {
    var result = "";
    var features = A[6609];
    var lidLock = R("SP-21194").HasValue() ? R("SP-21194").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21194").HasValue() ? R("cnet_common_SP-21194").Replace("<NULL>", "").Text : "";

    if (features.HasValue("slide lock")) {
        result = "A slide lock securely locks the lid to help keep pets and curious children from getting into the trash";
    }
    else if (!String.IsNullOrEmpty(lidLock) && lidLock.ToLower().Equals("yes")) {
        result = "Locks for secure use";
    }
    else if (Coalesce(features).HasValue("screw closure", "tie-down rings")) {
        result = $"{features.Where("screw closure", "tie-down rings").Select(o=>o.Value()).FlattenWithAnd().ToUpperFirstChar()} help make security simple";
    }
    
    if (!String.IsNullOrEmpty(result)) {
        Add($"SlideLockSecurity⸮{result}");
    }
}

// --[FEATURE #11]
// --Additional
void HandsFreeOperation() {
    var result = "";
    var features = A[6609];
    var type = R("SP-18159").HasValue() ? R("SP-18159").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18159").HasValue() ? R("cnet_common_SP-18159").Replace("<NULL>", "").Text : "";

    if (!String.IsNullOrEmpty(type)) {
        if (features.HasValue("odorless") && type.ToLower().Equals("sensor trash cans")) {
            
            result = "100% touch-free, odor-free and eliminates cross-contamination of germs";
        }
        else if (type.ToLower().Equals("step trash cans") || type.ToLower().Equals("sensor trash cans")) {
            result = "Touch-free feature prevents the cross-contamination of germs";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"HandsFreeOperation⸮{result}");
    }
}
// --[FEATURE #12]
// --Additional Trash Cans Pedal
void TrashCansPedal() {
    var result = "";
    var features = A[6609];
    if (features.HasValue("features")) {
        result = "Strong steel pedal is engineered for a smooth and easy step";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TrashCansPedal⸮{result}");
    }
}

// --[FEATURE #13]
// --Additional Finger-print Resistant Coating

void FingerPrintResistantCoating() {
    var result = "";
    var features = A[6609];
   
    if (features.HasValue("fingerprint-resistant coating")) {
        result = "Fingerprint-resistant coating is easy to clean";
    }
    else if (features.HasValue("dent-proof plastic lid")) {
        result = "Dent-proof plastic lid won't show dirt or fingerprints";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"FingerPrintResistantCoating⸮{result}");
    }
}

// --[FEATURE #14]
// --Additional Carbon Filter Gate
void CarbonFilterGate() {
    var result = "";
    var features = A[6609];
   
    if (Coalesce(features).HasValue("Carbon filter gate%")) {
        result = "Fingerprint-resistant coating is easy to clean";
    }
    else if (features.HasValue("dent-proof plastic lid")) {
        result = "Dent-proof plastic lid won't show dirt or fingerprints";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"CarbonFilterGate⸮{result}");
    }
}

// --[FEATURE #15]
// --Additional

void TrashCanBase() {
    var result = "";
    var features = A[6609];
   
    if (features.HasValue("anti-slip base")) {
        result = "Non-skid base to prevent the bin from sliding";
    }
    else if (features.HasValue("solid base")) {
        result = "Solid base keeps potential liquid spills contained";
    }
    else if (features.HasValue("reinforced base")) {
        result = "Reinforced base to reduce wear and tear from dragging";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TrashCanBase⸮{result}");
    }
}

// --[FEATURE #16]
// --Additional Stay Open Function

void StayOpenFunction() {
    var result = "";
    var features = A[6609];
   
    if (features.HasValue("stay-open function")) {
        result = "Non-skid base to prevent the bin from sliding";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"StayOpenFunction⸮{result}");
    }
}

// --[FEATURE #17]
// --Additional Liner Pocket
void LinerPocket() {
    var result = "";
    var features = A[6609];
   
    if (features.HasValue("liner pocket")) {
        result = "Liner pocket keeps liners where you need them and dispenses them one by one from inside the can for a faster liner change";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"LinerPocket⸮{result}");
    }
}


// --[FEATURE #18]
// --Additional Removable Inner Bucket
void RemovableInnerBucket() {
    var result = "";
    var features = A[6609];
    if (features.HasValue("removable inner bucket")) {
        result = "Inner trash bucket is fully removable for easy emptying and cleaning";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"RemovableInnerBucket⸮{result}");
    }
}

// --[FEATURE #19]
// --Additional lid shox technology/silent lid
void ShoxSilentLid () {
    var result = "";
    var features = A[6609];
    if (features.HasValue("lid shox technology")) {
        result = "Lid shox technology controls the motion of the lid for a slow, silent close";
    }
    else if (Coalesce(features).HasValue("silent%lid")) {
        result = "Lid closes slowly with whisper quiet operation";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"ShoxSilentLid⸮{result}");
    }
}

// --[FEATURE #20]
// --Additional venting channels
void VentingChannels () {
    var result = "";
    var features = A[6609];
    if (features.HasValue("venting channels")) {
        result = "Venting channels reduce the force needed to lift each trash bin";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"VentingChannels⸮{result}");
    }
}

// --[FEATURE #21]
// --Additional internal hinge
void InternalHinge () {
    var result = "";
    var features = A[6609];
    if (features.HasValue("internal hinge")) {
        result = "Internal hinge prevents the lid from bumping the wall";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"InternalHinge⸮{result}");
    }
}

// --[FEATURE #22]
// --Additional wheels
void TrashCanWheels () {
    var result = "";
    var features = A[6609];
    if (features.HasValue("wheels")) {
        result = "Wheels make the can easy to move";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TrashCanWheels⸮{result}");
    }
}

// --[FEATURE #23]
// --Additional powered
TrashCanPowered();
void TrashCanPowered () {
    var result = "";
    var batteryFormFactor = A[335];
    var batteryNum = A[861];
    if (batteryFormFactor.HasValue() && batteryNum.HasValue()) {
        result = "Powered by {batteryNum.Values} {batteryFormFactor.Values} batteries";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TrashCanPowered⸮{result}");
    }
}

//168328278181141928 end of "Trash Cans & Waste Receptacles" §§