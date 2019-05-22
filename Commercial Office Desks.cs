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
    var commercialOfficeDeskType = GetReference("SP-18204");
    
    if (!String.IsNullOrEmpty(commercialOfficeDeskType)) {
        if (commercialOfficeDeskType.ToLower().Equals("bow front")) {
            result = "The bow front desk expands your workspace";
        }
        else if (commercialOfficeDeskType.ToLower().Equals("corner")) {
            result = "Corner desk provides plenty of room to work but fits in compact office spaces";
        }
        else if (commercialOfficeDeskType.ToLower().Equals("double pedestal") 
        || commercialOfficeDeskType.ToLower().Equals("single pedestal")) {
            result = $"{commercialOfficeDeskType.ToLower().ToUpperFirstChar()} is desk ideal for multiple office solutions";
        }
        else if (commercialOfficeDeskType.ToLower().Equals("hutch")) {
            result = "Add more storage and organization to your workspace with the hutch";
        }
        else if (commercialOfficeDeskType.ToLower().Equals("l-shaped")) {
            result = "An 'L' configuration desk adds ample work space to your office";
        }
        else if (commercialOfficeDeskType.ToLower().Equals("U-shaped")) {
            result = "An 'U' configuration desk adds ample work space to your office";
        }
        else if (commercialOfficeDeskType.ToLower().Equals("workstations")
        || commercialOfficeDeskType.ToLower().Equals("returns")) {
            result = $"{commercialOfficeDeskType.ToLower().ToUpperFirstChar()} configuration desks add ample work space to your office";
        }
        else {
            result = $"{commercialOfficeDeskType.ToLower().ToUpperFirstChar()} configuration desk adds ample work space to your office";
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
    var commercialOfficeDeskType = GetReference("SP-18204");
    var furnishingMaterial = GetReference("SP-18963");
    var trueColor = GetReference("SP-22967");
    var harvest = Coalesce(A[7262], A[7209]).HasValue("harvest");
    var laminate = Coalesce(A[7263], A[7261]).HasValue("%laminate");
    if (harvest && laminate) {
        result = "Harvest finished laminate worksurface for a contemporary look";
    }
    else if (trueColor.ToLower().Equals("mahogany")
    && furnishingMaterial.ToLower().EndsWith("laminate")
    && commercialOfficeDeskType.ToLower().Equals("workstations")) {
        result = "Give your office a style overhaul with the addition of this mahogany-finished laminate workstation";
    }
    else if (!String.IsNullOrEmpty(trueColor) && !String.IsNullOrEmpty(furnishingMaterial)) {
        result = $"{trueColor.ToLower().ToUpperFirstChar()}-finished {furnishingMaterial.ToLower(true)} work surface for a contemporary look";
    }
    else if (!String.IsNullOrEmpty(trueColor)) {
        result = $"{trueColor.ToLower().ToUpperFirstChar()}-finished work surface for a contemporary look";
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
    var result = "";
    var commercialOfficeDeskType = GetReference("SP-18046");
    if (!String.IsNullOrEmpty(commercialOfficeDeskType)) {
        if (commercialOfficeDeskType.ToLower().Equals("contemporary")) {
            result = "Clean contemporary styling designed with your needs in mind to provide years of practical solutions";
        }
        else if (commercialOfficeDeskType.ToLower().Equals("industrial")) {
            result = "Brings a touch of the modern industrial style to your space";
        }
        else {
            result = $"{commercialOfficeDeskType} styling designed with your needs in mind to provide years of practical solutions";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"FurnishingStyle⸮{result}");
    }
}

// --[FEATURE #5]
// --# of drawers
void OfficeDesksNumOfDrawers() {
    var result = "";

    var numOfDrawers = GetReference("SP-12515");
    var numOfFileDrawers = GetReference("SP-1046");
    var drawer = "drawer";
    var fileDrawer = "drawer";
    
    if (!String.IsNullOrEmpty(numOfDrawers) && Coalesce(numOfDrawers).ExtractNumbers().First() > 1) {
        drawer = "drawers";
    }
    if (!String.IsNullOrEmpty(numOfFileDrawers) && Coalesce(numOfFileDrawers).ExtractNumbers().First() > 1) {
       fileDrawer = "drawers";
    }
    
    if (!String.IsNullOrEmpty(numOfDrawers) && !String.IsNullOrEmpty(numOfFileDrawers)) {
        result = $"Contains {numOfDrawers} {drawer} for office supplies and {numOfFileDrawers} file {fileDrawer} for documents";
    }
    else if (!String.IsNullOrEmpty(numOfDrawers)) {
        result = $"Contains {numOfDrawers} {drawer} for office supplies";
    }
    else if (!String.IsNullOrEmpty(numOfFileDrawers)) {
        result = $"Contains {numOfFileDrawers} file {fileDrawer} for documents";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"OfficeDesksNumOfDrawers⸮{result}");
    }
}


// --[FEATURE #6]
// --Drawer dimensions
void OfficeDesksDrawerDimensions() {
    var result = "";
    var drawerDimentions = GetReference("SP-4272");
    
    if (!String.IsNullOrEmpty(drawerDimentions)) {
        result = $"{drawerDimentions.Replace(", ", "; ")}";
    }
    
    if (!String.IsNullOrEmpty(result)) {
        Add($"OfficeDesksDrawerDimensions⸮{result}");
    }
}

// --[FEATURE #7]
// --If drawer holds files, what file size does it hold
// OOS


// --[FEATURE #8]
// --Weight capacity (lbs.)
void OfficeDesksWeightCapacity() {
    var result = "";
    var weight = A[7171];
    if (weight.HasValue()) {
        if (weight.Units.First().NameUSM.HasValue("lbs")) {
            result = $"Weight capacity: {weight.Values.First().ValueUSM} lbs";
        }
        else if (weight.Units.First().NameUSM.HasValue("oz")) {
            result = $"Weight capacity: {Math.Round(weight.Values.First().ValueUSM.ExtractNumbers().First() * 0.0625, 2)} lbs";
          
        }
        else if (weight.Units.First().Name.HasValue("g")) {
            result = $"Weight capacity: {Math.Round(weight.Values.First().ValueUSM.ExtractNumbers().First() * 0.00220462, 2)} lbs";
        }
        else if (weight.Units.First().Name.HasValue("kg")) {
            result = $"Weight capacity: {Math.Round(weight.Values.First().ValueUSM.ExtractNumbers().First() * 2.20462, 2)} lbs";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"OfficeDesksWeightCapacity⸮{result}");
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
    var result = ""; 
    var fullHeight = A[7278];
    var modestyPanel = A[7275];
    if (modestyPanel.HasValue("modesty panel") && fullHeight.HasValue("full-height")) {
        result = "Full-height modesty panel provides valuable privacy below the worksurface";
    }
    else if (modestyPanel.HasValue("modesty panel")) {
        result = "Modesty panel provides valuable privacy below the worksurface";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"AdditionalModestyPanel⸮{result}"); 
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