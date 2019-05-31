//§§168337568405214671 "Floor mats" "Alex K."

FloorMatTypeAndUse();
// Dimentions
ProductColorAndFloorMatShape();
FloorMatMaterialAndBackingMaterial();
FloorMatShape();
FloorMatSuitableSetting();
// Standards
// --Post Consumer Content (%)
// --Recycled Content (%)
AdditionalGreaseResistant();
AdditionalTaperedEdges();
AdditionalRoundedCorners();
AdditionalDirtTrapping();
AdditionalGripperBack();
AdditionalMoistureStainResistant();
AdditionalGreentrax();
// Warranty

// --[FEATURE #1]
// --Floor mat type & Use
void FloorMatTypeAndUse() {
    // Indoor|Entrance|Dissipative/Anti-Static|Carpet|Anti-Static|Anti-Fatigue|Scraper|Safety|Indoor/Outdoor|Wiper
    var type = REQ.GetVariable("SP-17785").HasValue() ? REQ.GetVariable("SP-17785") : R("SP-17785").HasValue() ? R("SP-17785") : R("cnet_common_SP-17785");
    if (type.HasValue("Anti-Fatigue")) {
         Add($"FloorMatTypeAndUse⸮Anti-fatigue mat provides maximum comfort when you stand on it");
    }
    else if (type.HasValue("Scraper")) {
         Add($"FloorMatTypeAndUse⸮Protect your carpet or floor from accidental damage by using scraper mat");
    }
    else if (type.HasValue("Anti-Static")) {
         Add($"FloorMatTypeAndUse⸮Anti-static mat designed to absorb static electricity");
    }
    else if (type.HasValue("Entrance")) {
         Add($"FloorMatTypeAndUse⸮Entrance mat designed to help prevent dirt and moisure entering the home or office on footwear");
    } 
    else if (type.HasValue()) {
         Add($"FloorMatTypeAndUse⸮{type.ToLower(true).ToUpperFirstChar()} mat offers stylish comfort and support at a great value");
    } 
}
// --[FEATURE #2]
// --Dimensions (in Inches): L x W

// --[FEATURE #3]
// --Product Color & floor mat shape
void ProductColorAndFloorMatShape() {
    var typeColor = REQ.GetVariable("SP-22967").HasValue() ? REQ.GetVariable("SP-22967") : R("SP-22967").HasValue() ? R("SP-22967") : R("cnet_common_SP-22967");
    var shape = REQ.GetVariable("SP-21073").HasValue() ? REQ.GetVariable("SP-21073") : R("SP-21073").HasValue() ? R("SP-21073") : R("cnet_common_SP-21073");
    
    if (typeColor.HasValue() && shape.HasValue()) {
        if (typeColor.HasValue("assorted")) {
            Add($"ProductColorAndFloorMatShape⸮{shape.ToLower().ToUpperFirstChar()} shape, comes in assorted colors");  
        } else {
            Add($"ProductColorAndFloorMatShape⸮{shape.ToLower().ToUpperFirstChar()} shape, comes in {typeColor.ToLower()}"); 
        }
    }
    else if (typeColor.HasValue()) {
        if (typeColor.HasValue("assorted")) {
            Add($"ProductColorAndFloorMatShape⸮Coomes in assorted colors"); 
        } else {
            Add($"ProductColorAndFloorMatShape⸮Comes in {typeColor.ToLower()}");  
        }
    }
    else if (shape.HasValue()) {
        Add($"ProductColorAndFloorMatShape⸮{shape.ToLower().ToUpperFirstChar()} shape");
    }
}

// --[FEATURE #4]
// --Floor mat material & Backing material
void FloorMatMaterialAndBackingMaterial() {
    // Basketweave|Leather Grain|Raised Bar|Textured|Loop Pile|Striped|Waffle|Ivy Leaf|Solid|Cut Pile|Pebble|Raised Parquet|Extruded Loop|Raised waffle|Oblong|Ridge|Molded Bubble|Parquet|Bubble|Diamond|Rib|Raised Dome|Chevron
    var material = REQ.GetVariable("SP-21076").HasValue() ? REQ.GetVariable("SP-21076") : R("SP-21076").HasValue() ? R("SP-21076") : R("cnet_common_SP-21076");
    // Sponge|Foam|Foam Vinyl|Rubber|Vinyl|PVC|Recycled Rubber|Polyurethane|Closed Cell Foam|Woven Fiber|Nitrile Rubber|Polyester/Vinyl
    var backingMaterial = REQ.GetVariable("SP-21077").HasValue() ? REQ.GetVariable("SP-21077") : R("SP-21077").HasValue() ? R("SP-21077") : R("cnet_common_SP-21077");
  
    if (material.HasValue() && backingMaterial.HasValue()) {
        Add($"FloorMatMaterialAndBackingMaterial⸮Made of {material.ToLower(true)} with {backingMaterial.ToLower(true)} backing");
    }
    else if (material.HasValue()) {
        Add($"FloorMatMaterialAndBackingMaterial⸮Made of {material.ToLower(true)}");
    }
    else if (backingMaterial.HasValue()) {
        Add($"FloorMatMaterialAndBackingMaterial⸮{backingMaterial.ToUpperFirstChar()} backing");
    }
}

// --[FEATURE #5]
// --Floor mat shape
void FloorMatShape() {
    // Basketweave|Leather Grain|Raised Bar|Textured|Loop Pile|Striped|Waffle|Ivy Leaf|Solid|Cut Pile|Pebble|Raised Parquet|Extruded Loop|Raised waffle|Oblong|Ridge|Molded Bubble|Parquet|Bubble|Diamond|Rib|Raised Dome|Chevron
    var pattern = REQ.GetVariable("SP-21076").HasValue() ? REQ.GetVariable("SP-21076") : R("SP-21076").HasValue() ? R("SP-21076") : R("cnet_common_SP-21076");
    if (pattern.HasValue()) {
        Add($"FloorMatShape⸮{pattern.ToLower().ToUpperFirstChar()} pettern");
    }
}

// --[FEATURE #6]
// --Suitable setting
void FloorMatSuitableSetting() {
    // -Indoor|Outdoor|Indoor and outdoor|Hot and cold environments 
    var suitableSetting = REQ.GetVariable("SP-23630").HasValue() ? REQ.GetVariable("SP-23630") : R("SP-23630").HasValue() ? R("SP-23630") : R("cnet_common_SP-23630");
  
    if (suitableSetting.HasValue("Indoor and outdoor")) {
        Add($"FloorMatMaterialAndBackingMaterial⸮Perfect for indoor and outdoor use");
    }
    else if (suitableSetting.HasValue("Indoor")) {
        Add($"FloorMatMaterialAndBackingMaterial⸮Indoor mat designed to help prevent dirt and moisture");
    }
    else if (suitableSetting.HasValue("Indoor")) {
        Add($"FloorMatMaterialAndBackingMaterial⸮Outdoor mat that provides excellent moisture and dirt removal");
    }
    else {
         Add($"FloorMatMaterialAndBackingMaterial⸮{suitableSetting.ToLower()}");
    }
}
// --[FEATURE #7]
// --NFSI Certification

// --[FEATURE #8]
// --Post Consumer Content (%)

// --[FEATURE #9]
// --Recycled Content (%)

// --[FEATURE #10]
// --Additional Grease-resistant material
void AdditionalGreaseResistant() {
    if (A[7383].HasValue("grease-resistant")) {
        Add($"AdditionalGreaseResistant⸮Grease-resistant material of this mat keeps it looking like new");
    }
}

// --[FEATURE #11]
// --Additional tapered edges
void AdditionalTaperedEdges() {
    if (A[7383].HasValue("tapered edges")) {
        Add($"AdditionalTaperedEdges⸮Tapered edges allow easier floor to mat transition");
    }
}

// --[FEATURE #12]
// --Additional rounded corners
void AdditionalRoundedCorners() {
    if (A[7383].HasValue("rounded corners")) {
        Add($"AdditionalRoundedCorners⸮Rounded Corners prevent tripping");
    }
}

// --[FEATURE #13]
// --Additional gripper back
void AdditionalGripperBack() {
    if (A[7383].HasValue("gripper back")) {
        Add($"AdditionalGripperBack⸮This mat stops the dirt, keeping your floors clean");
    }
}

// --[FEATURE #14]
// --Additional dirt trapping
void AdditionalDirtTrapping() {
    if (A[7383].HasValue("dirt trapping")) {
        Add($"AdditionalDirtTrapping⸮This mat stops the dirt, keeping your floors clean");
    }
}

// --[FEATURE #15]
// --Additional moisture-resistant stain-resistant
void AdditionalMoistureStainResistant() {
    if (A[7383].HasValue("moisture-resistant") && A[7383].HasValue("stain-resistant")) {
        Add($"AdditionalMoistureStainResistant⸮Moisture- ##and stain-resistant to cut cleaning and maintenance time by half");
    }
}

// --[FEATURE #16]
// --Additional Greentrax
void AdditionalGreentrax() {
    if (A[380].HasValue("Greentrax")) {
        Add($"AdditionalGreentrax⸮GreenTrax program recommended and approved as a part of green cleaning environments project");
    }
}
// --[FEATURE #17]
// --Warranty information

//§§168337568405214671 end of "Floor mats"