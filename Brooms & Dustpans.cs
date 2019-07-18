//§§1683392810982141837  "Brooms & Dustpans" "Alex K."

BroomDustpanTypeAndUse();

// --[FEATURE #1]
// -- Broom/dustpan type & Use
void BroomDustpanTypeAndUse(){
    var type = GetReferenceBase("SP-23349");
    if (type.HasValue("Handle Braces")){
        Add($"BroomDustpanTypeAndUse⸮Handle brace lends an added support to prevent handles from breaking under strenuous conditions");
    }
    else if (type.HasValue("Broom Heads")){
        Add($"BroomDustpanTypeAndUse⸮Broom head is designed to remove dirt and debris");
    }
    else if (type.HasValue("Push Brooms")){
        Add($"BroomDustpanTypeAndUse⸮Push broom offers a quick and efficient cleaning solution");
    }
    else if (type.HasValue("Kits")){
        Add($"BroomDustpanTypeAndUse⸮Kit offers a quick and efficient cleaning solution");
    }
    else if (type.HasValue("Angled Brooms")){
        Add($"BroomDustpanTypeAndUse⸮Angled broom is designed to reach into the tightest corners very easily");
    }
    else if (type.HasValue("Brooms w/Dustpan")){
        Add($"BroomDustpanTypeAndUse⸮Broom and dustpan set is great for all on-the-move applications for sweeping up and carrying debris");
    }
    else if (type.HasValue("Corn Brooms")){
        Add($"BroomDustpanTypeAndUse⸮Corn broom traps dust and fine dirt with ease");
    }
    else if (type.HasValue("Broom Handles")){
        Add($"BroomDustpanTypeAndUse⸮Broom handles");
    }
    else if (type.HasValue("Standard Brooms")){
        Add($"BroomDustpanTypeAndUse⸮Standard broom for easy and effective cleaning");
    }
    else if (type.HasValue("Dustpan")){
        Add($"BroomDustpanTypeAndUse⸮Dustpan for efficient pick-up of dust and dirt");
    }
    else if (type.HasValue("%tool holder%")){
        Add($"BroomDustpanTypeAndUse⸮{type.ToLower().ToUpperFirstChar()} provides a convenient storage solution");
    }
    else if (type.HasValue()){
        Add($"BroomDustpanTypeAndUse⸮${type.ToLower().ToUpperFirstChar()} for easy and effective cleaning");
    }
}
// --[FEATURE #2]
// -- Handle Information (Length and Material)

// --[FEATURE #3]
// -- Head Details (material and Size)

// --[FEATURE #4]
// -- Dimensions (in Inches): L x W

// --[FEATURE #5]
// -- Pack Size (If more than 1)

// --[FEATURE #6]
// -- Additional

// --[FEATURE #7]
// -- Additional

// --[FEATURE #8]
// -- Additional

// --[FEATURE #9]
// -- Additional

// --[FEATURE #10]
// -- Additional

// --[FEATURE #11]
// --Warranty information


//§§1683392810982141837  end of "Brooms & Dustpans"