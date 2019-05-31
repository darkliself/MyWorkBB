//§§5787384510798142735 "Craft Supplies" "Alex K."
CraftSupplyTypeUse();
CraftSuppliesDesign();
// Material
// Dimentions
// pack size
PackageContentCraftSupplies();
// Acid Free
CraftSuppliesArchivalSafe();
CraftSuppliesRubberBase();
CraftSuppliesSelfAdhesive();
// GlueGlueSticksNonToxic

// --[FEATURE #1]
// --Type of Craft Supply and Primary Use

void CraftSupplyTypeUse() {
    //var vortexType = A[6173];
    //Foil/Paper|Craft Kits/Sets|Craft Tools|Paint|Pencils|Fabric|Pens|Glitter/Powder|Crayons|Clay|Markers|Knives/Blades|Jewelry/Beads|Stamps|Craft Materials|Colored Pencils|Glues/Adhesives|Clothing|Button Complete Set
    var type = REQ.GetVariable("SP-17332").HasValue() ? REQ.GetVariable("SP-17332") : R("SP-17332").HasValue() ? R("SP-17332") : R("cnet_common_SP-17332");
    if (type.HasValue()) {
        if (type.HasValue("Clothing")) {
            Add($"CraftSupplyTypeUse⸮This cloth is perfect for arts and crafts projects");
        }
        else if (type.HasValue("Paint")) {
            Add($"CraftSupplyTypeUse⸮This paint makes your work unique");
        }
        else if (type.HasValue("Craft Materials")) {
            Add($"CraftSupplyTypeUse⸮These craft materials makes your work unique");
        }
        
        else if (type.HasValue("Clay")) {
            Add($"CraftSupplyTypeUse⸮This clay is perfect for creating shapes, structures and figures");
        }
        else if (type.HasValue("Jewelry/Beads")) {
            Add($"CraftSupplyTypeUse⸮Make crafting experiences more fun with these jewelry/beads");
        }
        else if (type.HasValue("Jewelry/Beads")) {
            Add($"CraftSupplyTypeUse⸮These glues/adhesives is great for Arts & Crafts projects");
        }
        else if (type.HasValue("Button Complete Set")) {
             Add($"CraftSupplyTypeUse⸮Button complete set is used to create buttons with your individual design");
        }
        else if (Coalesce(type).In("Pens", "%Pencils", "Markers", "Crayons")) {
            Add($"CraftSupplyTypeUse⸮{type} for everyday drawing tasks");
        } else {
             Add($"CraftSupplyTypeUse⸮{type.ToLower().ToUpperFirstChar().Pluralize()} are perfect for arts and crafts projects");
        }
        
    }
}

// --[FEATURE #2]
// --Design (functionality for tools, aesthetic value for decorative items)
void CraftSuppliesDesign() {
    if (A[7607].HasValue()) {
        Add($"CraftSuppliesDesign⸮Designed in the shape of {A[7607].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
    else if (A[7084].HasValue()) {
        Add($"CraftSuppliesDesign⸮{A[7084].Values.Select(o => o.Value()).FlattenWithAnd()} pattern design");
    }
}


// --[FEATURE #3]
// --Material (including True color/Finish)

// --[FEATURE #4]
// --Dimensions

// --[FEATURE #5]
// --Pack Size/Qty

// --[FEATURE #6]
// --If a Kit, detail contents

void PackageContentCraftSupplies() {
    if (A[6175].HasValue()) {
        Add($"PackageContentCraftSupplies⸮Package conteins: {A[6175].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
}

// --[FEATURE #7]
// --Additional Acid Free

// --[FEATURE #8]
// Additional Archival Safe 
void CraftSuppliesArchivalSafe() { 
    if (A[6189].HasValue("archival safe")) {
        Add($"CraftSuppliesArchivalSafe⸮Archival-safe for long-lasting protection");
    }
}

// --[FEATURE #9]
// Additional Rubber Base
void CraftSuppliesRubberBase() { 
    if (A[6189].HasValue("rubber base") || A[6189].HasValue("rubber feet")) {
        Add($"CraftSuppliesRubberBase⸮Rubber base keeps punches steady on your tabletop");
    }
}

// --[FEATURE #10]
// Additional Self Adhesive 
void CraftSuppliesSelfAdhesive() { 
    if (A[6189].HasValue("self-adhesive")) {
        Add($"CraftSuppliesSelfAdhesive⸮Self-adhesive for quick application");
    }
}

// --[FEATURE #11]
// --Additional GlueGlueSticksNonToxic

// --[FEATURE #12]
// --

//§§5787384510798142735 end of "Craft Supplies"