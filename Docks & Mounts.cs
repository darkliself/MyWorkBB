//§§530868304206054 "Docks & Mounts" "Alex K." "Serhii.O"


ConnectionOrHoldingDesign();
// Dimentions
DocksAndMountsPowerSource();
DocksAndMountsAdjustments();
DocksAndMountsDegreeViewingAngles();
DocksAndMountsScreenSizeCompatibility();


// --[FEATURE #1]
// --Product Compatibility (if it exceeds bullet size list in Extended Description)

// --[FEATURE #2]
// --Connection or Holding Design
void ConnectionOrHoldingDesign() {
    // Securely attaches to dash or window
    if (A[1031].HasValue()) {
        Add($"NotebookTypeAndUse⸮Securely attaches to {A[1031].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
}

// --[FEATURE #3]
// --Dimensions as HxWxD

// --[FEATURE #4]
// --Power Source
void DocksAndMountsPowerSource() {
    // https://www.staples.com/Lenovo-Proprietary-Interface-Docking-Station-for-Notebook-Tablet-PC-230-W-40A50230US/product_IM11X6392
    // Power: Power adapter 230 Watt AC 120/230 V ( 50/60 Hz )
    var type = REQ.GetVariable("SP-98").HasValue() ? REQ.GetVariable("SP-98") : R("SP-98").HasValue() ? R("SP-98") : R("cnet_common_SP-98");
    if (type.HasValue()) {
        Add($"DocksAndMountsPowerSource⸮Power: {type.ToLower(true)}");
    }
}

// --[FEATURE #5]
// --Additional adjustments
void DocksAndMountsAdjustments() {
    // Features tilt, vertically adjustable by 90 degrees, adjustable height
    if (A[9724].HasValue() && A[9724].Values.Count() == 1) {
        Add($"DocksAndMountsAdjustments⸮Features {A[9724].FirstValue()} adjustment");
    }
    else if (A[9724].HasValue()) {
        Add($"DocksAndMountsAdjustments⸮Features {A[9724].Values.Select(o => o.Value()).FlattenWithAnd()} adjustments");
    }
}


// --[FEATURE #6]
// --Additional Degree
void DocksAndMountsDegreeViewingAngles() {
    //  360-degree free rotation for optimal viewing angles
    if (A[9726].HasValue()) {
        Add($"DocksAndMountsDegreeViewingAngles⸮{A[9726].FirstValue().Replace("°", "")}-degree free rotation for optimal viewing angles");
    }
}

// --[FEATURE #7]
// --Additional Screen Size Compatibility
void DocksAndMountsScreenSizeCompatibility() {
    //   Fits most smartphones, up to 7"
    // screen size compatible 7522
    if ((A[10095].HasValue("up to%") || A[10095].HasValue("from %")) && A[7522].HasValue("cellular phone")) {
        Add($"DocksAndMountsScreenSizeCompatibility⸮Fits most smartphones, {A[10095].FirstValue()}");
    } 
    else if ((A[10095].HasValue("up to%") || A[10095].HasValue("from %")) && A[7522].HasValue("tablet")) {
        Add($"DocksAndMountsScreenSizeCompatibility⸮Fits most tablets, {A[10095].FirstValue()}");
    }
    else if (A[10095].HasValue() && A[7522].HasValue("cellular phone")) {
        Add($"DocksAndMountsScreenSizeCompatibility⸮Fits {A[10095].FirstValue()} smartphones");
    }
    else if (A[10095].HasValue() && A[7522].HasValue("tablet")) {
        Add($"DocksAndMountsScreenSizeCompatibility⸮Fits {A[10095].FirstValue()} tablets");
    }
    else if (A[10095].HasValue()) {
         Add($"DocksAndMountsScreenSizeCompatibility⸮Compatible with {A[10095].FirstValue()} screens");
    }
}

// --[FEATURE #8] 
// --Additional Package Content
void DocksAndMountsPackageContent() {
    //  Includes AC power adapter, power cord and publications
    if (A[7521].HasValue()) {
        Add($"DocksAndMountsPackageContent⸮Includes {A[7521].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
}

// --[FEATURE #9]
// --

// --[FEATURE #10]
// --

// --[FEATURE #11]
// --

// --[FEATURE #12]
// --Warranty

//§§530868304206054 "" ""