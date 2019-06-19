//§§135391203140599 "Hanging File Folders" "Alex K."

// true color and material
// FileFoldersTabPostion
// ??????
// Expansion
// FileFoldersPaperSize
HangingFileDetailsConstruction();

// --[FEATURE #1]
// --<Folder Material>, <True Color> & <Folder Durability (If Standard = Leave blank); Including stock & weight #

// --[FEATURE #2]
// --<Tab Position> & <Tabs>

// --[FEATURE #3]
// --<Folder Feature> (If Applicable)


// --[FEATURE #4]
// --<Folder Expansion (Inches)> (If Applicable)

// --[FEATURE #5]
// --<Paper Size>

// --[FEATURE #6]
// --Hanging File attachments details/Construction
void HangingFileDetailsConstruction() {
    if (A[5945].HasValue("coated hanger")) {
        Add($"HangingFileDetailsConstruction⸮Durable coated hangers for easy sliding");
    }
    else if (A[5945].HasValue("built-in tension springs")) {
        Add($"HangingFileDetailsConstruction⸮Long plastic hooks with built-in tension springs to grip rails");
    }
    else if (A[6547].HasValue("%frame%")) {
        Add($"HangingFileDetailsConstruction⸮{A[6547].Where("%frame%").Select( o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} provides long-lasting use");
    }
    else if (A[5945].HasValue("lowered top rail")) {
        Add($"HangingFileDetailsConstruction⸮Lowered top rail edge eliminates need for separate file folder tabs");
    }
    else if (A[5945].HasValue("reinforced gusset")) {
        Add($"HangingFileDetailsConstruction⸮Reinforced gusset provides extra strength at critical points");
    }
}
// --[FEATURE #7]
// --<Pack Qty (If more than 1)>

// --[FEATURE #8]
// --<Recycled Content (%)>

// --[FEATURE #9]
// --<Post Consumer Content (%)>

// --[FEATURE #10]
// --Additional

// --[FEATURE #11]
// --

// --[FEATURE #12]
// --

//§§135391203140599 end of "Hanging File Folders"