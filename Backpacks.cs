//§§5884203981142745 "Backpacks" "Alex K." "Serhii.O" 
BackpackStyleAndUse();
TmpTrueColorMaterial();
LaptopCompatibleAndSize();
BackpackSize();
NumberOfCompartments();
BackpackGender();
BackpacksWeightCapacity();
IsWheeled();
AdditionalStraps();
ShockAbsorbingShoulderStraps();
AirFlowBackPadding();
QuikPocket();
BackpackFrontPocket();
BackpackOrganizer();
BackpacSidePocket();
CaseBaseStabilizingPlatform();
PaddedShoulderStrap();
AdjustableShoulderStrap();
TopCarryHandle();
BackpackHandGrip();

//warranty

// --[FEATURE #1]
// --Backpack style & use
void BackpackStyleAndUse() {
    var result = "";
    var backpackType = R("SP-18852").HasValue() ? R("SP-18852").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18852").HasValue() ? R("cnet_common_SP-18852").Replace("<NULL>", "").Text : "";
    if (SKU.ProductId.In("21863890")) {
        result = "Backpack is perfect for carrying all of your supplies";
    }
    else if (!String.IsNullOrEmpty(backpackType)) {
        if (backpackType.ToLower().Equals("backpack")) {
            result = "Backpack is perfect for carrying all of your supplies";
        }
        else if (backpackType.ToLower().Equals("camera backpack")) {
            result = "Camera backpack provides the optimal amount of customizable packing space";
        }
        else if (backpackType.ToLower().Equals("carrying case")) {
            result = "Carrying case provides protection to your device";
        }
        else if (backpackType.ToLower().Equals("frameless")) {
            result = "Frameless design ensures comfort";
        }
        else if (backpackType.ToLower().Equals("school backpack")) {
            result = "This backpack can carry all you child's school needs";
        }
        else if (backpackType.ToLower().Equals("shoulder/compression straps")) {
            result = "Comfort meets functionality with shoulder and compression straps";
        }
        else if (backpackType.ToLower().Equals("rolling backpack")) {
            result = "Rolling backpack is perfect to use every day or when you travel";
        }
        else if (backpackType.ToLower().Equals("tablet sleeve")) {
            result = "Tablet sleeve protect your device wherever you go";
        }
        else {
            result = $"{backpackType.ToLower().ToUpperFirstChar().Replace("backpack", "")} backpack";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BackpackStyleAndUse⸮{result}");
    }
}

// --[FEATURE #2]
// --Backpack True Color Material
void TmpTrueColorMaterial() {
    var result = "";
    //djustable shoulder straps for added comfort
    if (SKU.ProductId.In("21863890")) {
         result = "Comes in bleached denim color and made of polyester";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TmpTrueColorMaterial⸮{result}");
    }
}

// --[FEATURE #3]
// --Laptop compatible & compatibility size
void LaptopCompatibleAndSize() {
    var result = "";
    var laptopSizeCompatibility = R("SP-18870").HasValue() ? R("SP-18870").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18870").HasValue() ? R("cnet_common_SP-18870").Replace("<NULL>", "").Text : "";
    var laptopCompatible = R("SP-18898").HasValue() ? R("SP-18898").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18898").HasValue() ? R("cnet_common_SP-18898").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(laptopSizeCompatibility) && laptopSizeCompatibility.ToLower().Equals("not compatible")) {
        result = $"Designed to fit a range of laptops with screens {laptopSizeCompatibility.ToLower()}";
    }
    else if (!String.IsNullOrEmpty(laptopCompatible)) {
        result = "Accommodates laptops";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"LaptopCompatibleAndSize⸮{result}");
    }
}

// --[FEATURE #4]
// --Backpack size
void BackpackSize() {
    var result = "";
    var size = R("SP-22643").HasValue() ? R("SP-22643").Replace("<NULL>", "").Text :
            R("cnet_common_SP-22643").HasValue() ? R("cnet_common_SP-22643").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(size)) {
            result = $"Backpack size: {size.ToLower()}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BackpackSize⸮{result}");
    }
}

// --[FEATURE #5]
// --Number of compartments and their individual measurements
void NumberOfCompartments() {
    var result = "";
    var count = R("SP-18587").HasValue() ? R("SP-18587").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18587").HasValue() ? R("cnet_common_SP-18587").Replace("<NULL>", "").Text : "";
    var num = A[856];
    if (SKU.ProductId.In("21863890")) {
         result = $"Features front external pocket to keep you organized";
    }
    else if (!String.IsNullOrEmpty(count) && num.HasValue()) {
        result = $"Features {num.Values} compartments to keep you organized";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NumberOfCompartments⸮{result}");
    }
}

// --[FEATURE #6]
// --Gender
void BackpackGender() {
    var result = "";
    //Boys|Men|Unisex|Girls|Women
    var gender = R("SP-17445").HasValue() ? R("SP-17445").Replace("<NULL>", "").Text :
        R("cnet_common_SP-17445").HasValue() ? R("cnet_common_SP-17445").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(gender)) {
        if (gender.ToLower().Equals("inisex")) {
            result = "Backpack is appropriate for use by boys";
        }
        else {
            result = $"Backpack is appropriate for use by {gender.ToLower()}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BackpackGender⸮{result}");
    }
}

// --[FEATURE #7]
// --Weight capacity
void BackpacksWeightCapacity(){
    var result = "";
    if(SKU.ProductId.In("21863890")){
        var CarryingCase_Capacity = A[7822]; // Carrying Case - Capacity
        if(CarryingCase_Capacity.HasValue()
        && CarryingCase_Capacity.Units.First().Name.In("liters")){
            result = $"Weight Capacity of {Math.Round(CarryingCase_Capacity.FirstValue() * 2.2, 2)} lbs.";
        }
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"BackpacksWeightCapacity⸮{result}");
    }
}

// --[FEATURE #8]
// --If wheeled; state here
void IsWheeled() {
    var result = "";
    var wheeled = A[7246];
    
    if (wheeled.HasValue()) {
        result = "Wheeled for easy travel";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"IsWheeled⸮{result}");
    }
}

// --[FEATURE #9]
// --additional shock absorbing shoulder straps

void ShockAbsorbingShoulderStraps() {
    var result = "";
    var straps = A[860];
    
    if (Coalesce(straps).HasValue("shock absorbing shoulder straps")) {
        result = "Shock-absorbing Shoulder Straps for added comfort";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"ShockAbsorbingShoulderStraps⸮{result}");
    }
}

// --[FEATURE #10]
// --additional air-flow back padding

void AirFlowBackPadding() {
    var result = "";
    var straps = A[860];
    
    if (Coalesce(straps).HasValue("air-flow back padding")) {
        result = "Stay cool with the air-flow back padding";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AirFlowBackPadding⸮{result}");
    }
}

// --[FEATURE #11]
// --additional Quik Pocket
void QuikPocket() {
    var result = "";
    var straps = A[860];
    
    if (Coalesce(straps).HasValue("Quik Pocket")) {
        result = "Quik pocket provides easy access to items you need most frequently";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"QuikPocket⸮{result}");
    }
}

// --[FEATURE #12]
// --additional front pocket
void BackpackFrontPocket() {
    var result = "";
    var straps = A[2527];
    
    if (Coalesce(straps).HasValue("front pocket")) {
        result = "Backpack has front pocket to make organizing easy";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BackpackFrontPocket⸮{result}");
    }
}

// --[FEATURE #13]
// --additional backpack organizer
void BackpackOrganizer() {
    var result = "";
    var straps = A[2527];
    if (Coalesce(straps).HasValue("organizer")) {
        result = "Organize your small essentials in the organizer";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BackpackOrganizer⸮{result}");
    }
}

// --[FEATURE #14]
// --additional side pocket
void BackpacSidePocket() {
    var result = "";
    var straps = A[2527];
    
    if (Coalesce(straps).HasValue("side pocket")) {
        result = "Side pocket provides a convenient store space";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BackpacSidePocket⸮{result}");
    }
}


// --[FEATURE #15]
// --additional CaseBase stabilizing platform
void CaseBaseStabilizingPlatform() {
    var result = "";
    var straps = A[860];
    
    if (Coalesce(straps).HasValue("CaseBase stabilizing platform")) {
        result = "CaseBase Stabilizing Platform keeps the bag standing in an upright position when placed on the floor";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"CaseBaseStabilizingPlatform⸮{result}");
    }
}


// --[FEATURE #16]
// --additional padded shoulder strap
void PaddedShoulderStrap() {
    var result = "";
    var straps = A[860];  
    if (Coalesce(straps).HasValue("padded shoulder strap")) {
        result = "Padded shoulder straps eliminate friction and make the backpack comfortable";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"PaddedShoulderStrap⸮{result}");
    }
}

// --[FEATURE #17]
// --additional adjustable shoulder strap
void AdjustableShoulderStrap() {
    var result = "";
    var straps = A[860];
    
    if (Coalesce(straps).HasValue("adjustable shoulder strap")) {
        result = "Adjustable shoulder straps for added comfort and convenience";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdjustableShoulderStrap⸮{result}");
    }
}

// --[FEATURE #18]
// --additional top carry handle
void TopCarryHandle() {
    var result = "";
    var straps = A[859];
    
    if (Coalesce(straps).HasValue("top carry handle")) {
        result = "Top carry handle makes it easy to grab the backpack and go";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TopCarryHandle⸮{result}");
    }
}

// --[FEATURE #19]
// --additional hand grip
void BackpackHandGrip() {
    var result = "";
    var straps = A[859];
    
    if (Coalesce(straps).HasValue("hand grip")) {
        result = "Comes with hand grip for added convenience";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BackpackHandGrip⸮{result}");
    }
}

// --[FEATURE #20]
// --additional
void AdditionalStraps() {
    var result = "";
    //djustable shoulder straps for added comfort
    //https://www.staples.com/staples-pembroke-18-backpack-galaxy-pattern-6-88-w-x-18-11-h-x-12-20-d-52424/product_24275636
    if (SKU.ProductId.In("21863890")) {
         result = "Web haul handle ##and backpack straps for added comfort";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalStraps⸮{result}");
    }
}

// --[FEATURE #21]
// --Warranty

//5884203981142745 end of "Backpacks" "Alex K." "Serhii.O"§§ 