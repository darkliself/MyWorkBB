//§§2979753164550 "Commercial Office Desks" "Alex K."

CommercialOfficeDeskTypeAndUse();
FurnishingMaterialAndFurnishingTrueColor();
// Dimentions
FurnishingStyle();
OfficeDesksNumOfDrawers();
OfficeDesksDrawerDimensions();
// [FEATURE #7] -- OOS
OfficeDesksWeightCapacity();
// using "WhiteboardsAssemblyInformation" 
// Certifications
AdditionalModestyPanel();
AdditionalLaminateResistant();
AdditionalLockCore();

// --[FEATURE #1]
// --Commercial office desk type & Use
void CommercialOfficeDeskTypeAndUse() {
    var result = "";
    //Workstations|Corner|Executive|U-Shaped|Returns|Hutch|Double Pedestal|Single Pedestal|File Drawer Pedestal|L-Shaped|Straight Base|Bow Front|Table Desk
    var commercialOfficeDeskType = REQ.GetVariable("SP-18204").HasValue() ? REQ.GetVariable("SP-18204") : R("SP-18204").HasValue() ? R("SP-18204") : R("cnet_common_SP-18204");
    
    if (!String.IsNullOrEmpty(commercialOfficeDeskType)) {
        if (commercialOfficeDeskType.HasValue("bow front")) {
            Add($"CommercialOfficeDeskTypeAndUse⸮The bow front desk expands your workspace");
        }
        else if (commercialOfficeDeskType.HasValue("corner")) {
            Add($"CommercialOfficeDeskTypeAndUse⸮Corner desk provides plenty of room to work but fits in compact office spaces");
        }
        else if (commercialOfficeDeskType.HasValue("double pedestal") 
        || commercialOfficeDeskType.HasValue("single pedestal")) {
            Add($"CommercialOfficeDeskTypeAndUse⸮{commercialOfficeDeskType.ToLower().ToUpperFirstChar()} is desk ideal for multiple office solutions");
        }
        else if (commercialOfficeDeskType.HasValue("hutch")) {
            Add($"CommercialOfficeDeskTypeAndUse⸮Add more storage and organization to your workspace with the hutch");
        }
        else if (commercialOfficeDeskType.HasValue("l-shaped")) {
            Add($"CommercialOfficeDeskTypeAndUse⸮An 'L' configuration desk adds ample work space to your office");
        }
        else if (commercialOfficeDeskType.HasValue("U-shaped")) {
           Add($"CommercialOfficeDeskTypeAndUse⸮An 'U' configuration desk adds ample work space to your office");
        }
        else if (commercialOfficeDeskType.HasValue("workstations")
        || commercialOfficeDeskType.HasValue("returns")) {
           Add($"CommercialOfficeDeskTypeAndUse⸮{commercialOfficeDeskType.ToLower().ToUpperFirstChar()} configuration desks add ample work space to your office");
        }
        else {
            Add($"CommercialOfficeDeskTypeAndUse⸮{commercialOfficeDeskType.ToLower().ToUpperFirstChar()} configuration desk adds ample work space to your office");
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"CommercialOfficeDeskTypeAndUse⸮{result}");
    }
}

// --[FEATURE #2]
// --Furnishing material & Furnishing True color
void FurnishingMaterialAndFurnishingTrueColor() {
    var result = "";
    //Workstations|Corner|Executive|U-Shaped|Returns|Hutch|Double Pedestal|Single Pedestal|File Drawer Pedestal|L-Shaped|Straight Base|Bow Front|Table Desk
    var commercialOfficeDeskType = REQ.GetVariable("SP-18204").HasValue() ? REQ.GetVariable("SP-18204") : R("SP-18204").HasValue() ? R("SP-18204") : R("cnet_common_SP-18204");
    var furnishingMaterial = REQ.GetVariable("SP-18963").HasValue() ? REQ.GetVariable("SP-18963") : R("SP-18963").HasValue() ? R("SP-18963") : R("cnet_common_SP-18963");
    var trueColor = REQ.GetVariable("SP-22967").HasValue() ? REQ.GetVariable("SP-22967") : R("SP-22967").HasValue() ? R("SP-22967") : R("cnet_common_SP-22967");
    var harvest = Coalesce(A[7262], A[7209]).HasValue("harvest");
    var laminate = Coalesce(A[7263], A[7261]).HasValue("%laminate");
    if (harvest && laminate) {
        result = "Harvest finished laminate worksurface for a contemporary look";
    }
    else if (trueColor.HasValue("mahogany")
    && furnishingMaterial.HasValue("%laminate")
    && commercialOfficeDeskType.HasValue("workstations")) {
         Add("FurnishingMaterialAndFurnishingTrueColor⸮Give your office a style overhaul with the addition of this mahogany-finished laminate workstation");
    }
    else if (trueColor.HasValue() && furnishingMaterial.HasValue()) {
         Add($"FurnishingMaterialAndFurnishingTrueColor⸮{trueColor.ToLower().ToUpperFirstChar()}-finished {furnishingMaterial.ToLower(true)} work surface for a contemporary look");
    }
    else if (!String.IsNullOrEmpty(trueColor)) {
         Add($"FurnishingMaterialAndFurnishingTrueColor⸮{trueColor.ToLower().ToUpperFirstChar()}-finished work surface for a contemporary look");
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"FurnishingMaterialAndFurnishingTrueColor⸮{result}");
    }
}

// --[FEATURE #3]
// --Dimensions (in Inches): H x W x D

// --[FEATURE #4]
// --Furnishing style
void FurnishingStyle() {
    var commercialOfficeDeskType = REQ.GetVariable("SP-18046").HasValue() ? REQ.GetVariable("SP-18046") : R("SP-18046").HasValue() ? R("SP-18046") : R("cnet_common_SP-18046");
    if (commercialOfficeDeskType.HasValue()) {
        if (commercialOfficeDeskType.HasValue("contemporary")) {
             Add("FurnishingStyle⸮Clean contemporary styling designed with your needs in mind to provide years of practical solutions");
        }
        else if (commercialOfficeDeskType.HasValue("industrial")) {
             Add("FurnishingStyle⸮Brings a touch of the modern industrial style to your space");
        }
        else {
             Add($"FurnishingStyle⸮{commercialOfficeDeskType.ToLower().ToUpperFirstChar()} styling designed with your needs in mind to provide years of practical solutions");
        }
    }
}

// --[FEATURE #5]
// --# of drawers
void OfficeDesksNumOfDrawers() {
    var numOfDrawers = REQ.GetVariable("SP-12515").HasValue() ? REQ.GetVariable("SP-12515") : R("SP-12515").HasValue() ? R("SP-12515") : R("cnet_common_SP-12515");
    var numOfFileDrawers = REQ.GetVariable("SP-1046").HasValue() ? REQ.GetVariable("SP-1046") : R("SP-1046").HasValue() ? R("SP-1046") : R("cnet_common_SP-1046");
    var drawer = "drawer";
    var fileDrawer = "drawer";
    
    if (numOfDrawers.HasValue() && numOfDrawers.ExtractNumbers().First() > 1) {
        drawer = "drawers";
    }
    if (numOfFileDrawers.HasValue() && numOfFileDrawers.ExtractNumbers().First() > 1) {
       fileDrawer = "drawers";
    }
    
    if (numOfDrawers.HasValue() && numOfFileDrawers.HasValue()) {
        Add($"OfficeDesksNumOfDrawers⸮Contains {numOfDrawers} {drawer} for office supplies and {numOfFileDrawers} file {fileDrawer} for documents");
    }
    else if (numOfDrawers.HasValue()) {
        Add($"OfficeDesksNumOfDrawers⸮Contains {numOfDrawers} {drawer} for office supplies");
    }
    else if (numOfFileDrawers.HasValue()) {
        Add($"OfficeDesksNumOfDrawers⸮Contains {numOfFileDrawers} file {fileDrawer} for documents");
    }
}

// --[FEATURE #6]
// --Drawer dimensions
void OfficeDesksDrawerDimensions() {
    var drawerDimentions = REQ.GetVariable("SP-4272").HasValue() ? REQ.GetVariable("SP-4272") : R("SP-4272").HasValue() ? R("SP-4272") : R("cnet_common_SP-4272");
    if (drawerDimentions.HasValue()) {
        Add($"OfficeDesksDrawerDimensions⸮{drawerDimentions.Replace(", ", "; ")}");
    }
}

// --[FEATURE #7]
// --If drawer holds files, what file size does it hold
// OOS

// --[FEATURE #8]
// --Weight capacity (lbs.)
void OfficeDesksWeightCapacity() {
    if (A[7171].HasValue()) {
        if (A[7171].Units.First().NameUSM.HasValue("lbs")) {
            Add($"OfficeDesksWeightCapacity⸮Weight capacity: {A[7171].Values.First().ValueUSM} lbs");
        }
        else if (A[7171].Units.First().NameUSM.HasValue("oz")) {
            Add($"OfficeDesksWeightCapacity⸮Weight capacity: {Math.Round(A[7171].Values.First().ValueUSM.ExtractNumbers().First() * 0.0625, 2)} lbs");
          
        }
        else if (A[7171].Units.First().Name.HasValue("g")) {
           Add($"OfficeDesksWeightCapacity⸮Weight capacity: {Math.Round(A[7171].Values.First().ValueUSM.ExtractNumbers().First() * 0.00220462, 2)} lbs");
        }
        else if (A[7171].Units.First().Name.HasValue("kg")) {
            Add($"OfficeDesksWeightCapacity⸮Weight capacity: {Math.Round(A[7171].Values.First().ValueUSM.ExtractNumbers().First() * 2.20462, 2)} lbs");
        }
    }
}

// --[FEATURE #9]
// --Assembly information
// using WhiteboardsAssemblyInformation

// --[FEATURE #10]
// --Certifications

// --[FEATURE #11]
// --Additional Modesty panel
void AdditionalModestyPanel(){ 
    if (A[7275].HasValue("modesty panel") && A[7278].HasValue("full-height")) {
        Add($"AdditionalModestyPanel⸮Full-height modesty panel provides valuable privacy below the worksurface");
    }
    else if (A[7275].HasValue("modesty panel")) {
         Add($"AdditionalModestyPanel⸮Modesty panel provides valuable privacy below the worksurface");
    }
}

// --[FEATURE #12]
// --Additional Laminate's resistant
void AdditionalLaminateResistant(){ 
    if (A[7261].HasValue("%laminate%") && A[7259].HasValue("%resistant%")) {
        Add($"AdditionalLaminateResistant⸮Laminates are {A[7259].Where("%resistant%").Select(o => o.Value().Replace("-resistant", "")).FlattenWithAnd()}-resistant");
    }
}
// --[FEATURE #13]
// --Additional Lock Core
void AdditionalLockCore(){ 
    if (A[7259].HasValue("%HON One Key core removable locks%")) {
        Add($"AdditionalLockCore⸮All drawers lock by securing the center drawer");
    }
}
// --[FEATURE #14]
// --Warranty

//§§ "2979753164550" end of "Commercial Office Desks" "Alex K."