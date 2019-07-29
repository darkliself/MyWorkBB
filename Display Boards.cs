//§§11012087140934  "Display Boards" "Alex K."
DisplayBoardTypeAndUse();
// WhiteboardsFrameConstruction - All
// Dimensions - All
// WhiteboardsDesign - All
DisplayBoardRecyclable();
DisplayBoardSmoothSurface();
DisplayBoardDoubleSided();
// True color
DisplayBoardResistant();
DisplayBoardIncluded();

// --[FEATURE #1]
// -- Display board type & use
void DisplayBoardTypeAndUse() {
    // Poster Board|Tagboard|Presentation Board|Display
    var type = GetReferenceBase("SP-17482");
    if (type.HasValue("Poster Board")) {
        Add($"DisplayBoardTypeAndUse⸮Poster board is great for signs, posters and mounting");
    }
    else if (type.HasValue("Display")) {
        Add($"DisplayBoardTypeAndUse⸮Display board is designed for projects, presentations and general display purposes");
    }
    else if (type.HasValue("Presentation Board")) {
        Add($"DisplayBoardTypeAndUse⸮Presentation board is great for sales presentations, exhibits and displays");
    }
    else if (type.HasValue()) {
        Add($"DisplayBoardTypeAndUse⸮{type.ToLower().ToUpperFirstChar()} can be used for hundreds of arts/crafts projects");
    }
}
// --[FEATURE #2]
// -- Overall Dimensions: H x W

// --[FEATURE #3]
// -- Frame construction; includes material, properties & mechanism.

// --[FEATURE #4]
// -- Design; include integrated features

// --[FEATURE #5]
// -- Additional recyclable
void DisplayBoardRecyclable() {
    if (A[6189].HasValue("recyclable")) {
        Add($"DisplayBoardRecyclable⸮Recyclable for an environmentally-friendly choice");
    }
}

// --[FEATURE #6]
// -- Additional smooth surface
void DisplayBoardSmoothSurface() {
    if (A[6189].HasValue("smooth surface")) {
        Add($"DisplayBoardSmoothSurface⸮Smooth surface works with wide range of mediums including markers and paint");
    }
}
// --[FEATURE #7]
// -- Additional double-sided
void DisplayBoardDoubleSided() {
    if (A[6189].HasValue("%double-sided%")) {
        Add($"DisplayBoardDoubleSided⸮Double-sided board allows front and back display");
    }
    else if (A[6087].HasValue("%double-sided presentation%")) {
        Add($"DisplayBoardDoubleSided⸮Double-sided board allows front and back display");
    }
}

// --[FEATURE #8]
// -- Additional recyclable

// --[FEATURE #9] 
// -- Additional resistant
void DisplayBoardResistant() {
    if (A[6189].HasValue("%-resistant%")) {
        Add($"DisplayBoardResistant⸮{A[6189].Where("%-resistant%").Select(o => o.Value()).FlattenWithAnd().Replace("resistant", "").ToUpperFirstChar()} resistant");
    }
}
// --[FEATURE #10]
// -- Carrying bag included
void DisplayBoardIncluded() {
    if (A[6108].HasValue()) {
        Add($"DisplayBoardIncluded⸮{A[6108].Values.Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} included");
    }
}
// --[FEATURE #11]
// -- HookStripWeatherproofResistant

// --[FEATURE #12]
// -- Additional

//§§11012087140934  end of "Display Boards"