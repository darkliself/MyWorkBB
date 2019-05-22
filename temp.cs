// Dimensions as follows: If “Standup (3D)” use Height (floor/base to top) x Width (side to side) x Depth (front to back) in inches as H x W x D. If lay flat (2D such as paper) use Width (side to Side) x Length (top to bottom) in inches. If layflat has accountable Thickness (over .125”), add as Depth so use H x W x D. 
void Dimen_sions(){ 
    var Dimensions = ""; 
    var DimensionsWeight_Width = REQ.GetVariable("SP-21044").HasValue() ? REQ.GetVariable("SP-21044").Replace("<NULL>", "").Text : 
        R("SP-21044").HasValue() ? R("SP-21044").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21044").HasValue() ? R("cnet_common_SP-21044").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Width 
    var DimensionsWeight_Depth = REQ.GetVariable("SP-20657").HasValue() ? REQ.GetVariable("SP-20657").Replace("<NULL>", "").Text : 
        R("SP-20657").HasValue() ? R("SP-20657").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20657").HasValue() ? R("cnet_common_SP-20657").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Depth 
    var DimensionsWeight_Height = REQ.GetVariable("SP-20654").HasValue() ? REQ.GetVariable("SP-20654").Replace("<NULL>", "").Text : 
        R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Depth 
    var DimensionsWeight_Length = REQ.GetVariable("SP-20400").HasValue() ? REQ.GetVariable("SP-20400").Replace("<NULL>", "").Text : 
        R("SP-20400").HasValue() ? R("SP-20400").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20400").HasValue() ? R("cnet_common_SP-20400").Replace("<NULL>", "").Text : ""; //Dimensions & Weight 

    var CableLengthRef = REQ.GetVariable("SP-21212").HasValue() ? REQ.GetVariable("SP-21212").Replace("<NULL>", "").Text : 
        R("SP-21212").HasValue() ? R("SP-21212").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21212").HasValue() ? R("cnet_common_SP-21212").Replace("<NULL>", "").Text : ""; //Cable Length (ft.) 
    var CellPhoneCableOrConnectorTypeRef = REQ.GetVariable("SP-18037").HasValue() ? REQ.GetVariable("SP-18037").Replace("<NULL>", "").Text : 
        R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : ""; //Cable Length (ft.) 
    var DimensionsWeight_Diameter = Coalesce(R("SP-21045").Replace("<NULL>", "").Text, R("SP-21453").Replace("<NULL>", "").Text, R("SP-21629").Replace("<NULL>", "").Text); //Dimensions & Weight - Depth 
    var HeatersCoolers_HeatingCooling_SpecialFeatures_Details = Coalesce(A[4753], A[9898]); // Heaters & Coolers - Heating & Cooling - Details | Special Features 
    var FilingSystem_ProductSize = A[5924]; // Filing System/Product Size 

    if(!String.IsNullOrEmpty(DimensionsWeight_Width) 
    && !String.IsNullOrEmpty(DimensionsWeight_Depth) 
    && !String.IsNullOrEmpty(DimensionsWeight_Height)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""D" : 
        $@"Dimensions: {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""D"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Width) && !String.IsNullOrEmpty(DimensionsWeight_Depth)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""L" : 
        $@"Dimensions: {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""L"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Width)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""W x {DimensionsWeight_Width}""L" : 
        $@"Dimensions: {DimensionsWeight_Width}""W x {DimensionsWeight_Height}""L"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Depth)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""W x {DimensionsWeight_Depth}""L" : 
        $@"Dimensions: {DimensionsWeight_Height}""W x {DimensionsWeight_Depth}""L"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Length)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""W x {DimensionsWeight_Length}""L" : 
        $@"Dimensions: {DimensionsWeight_Height}""W x {DimensionsWeight_Length}""L"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Diameter)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""W x {DimensionsWeight_Diameter}""Dia." : 
        $@"Dimensions: {DimensionsWeight_Height}""W x {DimensionsWeight_Diameter}""Dia."; 
    } 
    else if(FilingSystem_ProductSize.HasValue() 
    && FilingSystem_ProductSize.Values.First().ValueUSM.ExtractNumbers().Any()){ 
    var numbers = FilingSystem_ProductSize.Values.First().ValueUSM.ExtractNumbers(); 
        Dimensions = $@"Dimensions: {numbers.First()}""W x {numbers.Last()}""L"; 
    } 
    else if(CableLengthRef.Equals("6")){ 
        Dimensions = "6' cable provides enough length to reach power sources"; 
    } 
    else if(!String.IsNullOrEmpty(CableLengthRef) 
    && CellPhoneCableOrConnectorTypeRef.ToLower().Contains("cable")){ 
        Dimensions = $"Cable has {CableLengthRef}' length"; 
    } 
    else if(!String.IsNullOrEmpty(CableLengthRef)){ 
        Dimensions = $"{CableLengthRef}' cable length"; 
    } 
    if(!String.IsNullOrEmpty(Dimensions)){ 
        Add($"Dimensions⸮{Dimensions}"); 
    } 
}