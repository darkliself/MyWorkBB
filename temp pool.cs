
//Get reference from item or from common

String GetReference(String referenceCode){
    return R(referenceCode).HasValue() ? R(referenceCode).Text : 
           R($"cnet_common_{referenceCode}").HasValue() ? R($"cnet_common_{referenceCode}").Text : 
           "";
}

//Get list of numbers from string

List<double> extNumbers(string str){
    var numbers = new List<double>();
    int a = 0;
    string tmp = "";
    foreach(char c in str){
        if(Char.IsDigit(c) || c == '.'){
            tmp += c;
        }
        else{
            if(!String.IsNullOrEmpty(tmp)){
                numbers.Add(double.Parse(tmp));
            }
            tmp = "";
        }
        if(a == str.Length - 1){
            if(!String.IsNullOrEmpty(tmp)){
                numbers.Add(double.Parse(tmp));
            }
        }
        a++;
    }
    return numbers;
}

//"Features for all Category BEGIN" 

Recycled_Post_Consumer_Content(); 
Pack_Size(); 
Compliant_Standards(); 
Warr_anty(); 
Dimen_sions(); 
CaffeineFree(); 
GlutenFree(); 
Kosher(); 
Acid_Free();
SugarFree(); 
TrueColor_Material();
Processor_Type_All();
Operating_System();
RAM_Type();
Wireless_Connectivity();
Graphics_All();
Weight_All();

// Recycled Content % (If Applicable)
void Recycled_Post_Consumer_Content(){
    var RecycledContent = "";
    var PostConsumerContent = "";
    var RecycledPostConsumerContent = "";
    var RecycledContentRef = R("SP-21623").HasValue() ? R("SP-21623").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21623").HasValue() ? R("cnet_common_SP-21623").Replace("<NULL>", "").Text : "";
    var PostConsumerContentRef = R("SP-21624").HasValue() ? R("SP-21624").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21624").HasValue() ? R("cnet_common_SP-21624").Replace("<NULL>", "").Text : "";

    if (!String.IsNullOrEmpty(RecycledContentRef) && !String.IsNullOrEmpty(PostConsumerContentRef)) {
        RecycledPostConsumerContent = $"Contains {RecycledContentRef}% total recycled content with {PostConsumerContentRef}% post-consumer content";
    }
    if (!String.IsNullOrEmpty(RecycledContentRef)) {
        RecycledContent = $"Contains {RecycledContentRef}% total recycled content";
    }
    if (!String.IsNullOrEmpty(PostConsumerContentRef)) {
        PostConsumerContent = $"Contains {PostConsumerContentRef}% post-consumer content";
    }
    if(!String.IsNullOrEmpty(RecycledContent)){
        Add($"RecycledContent⸮{RecycledContent}");
    }
    if(!String.IsNullOrEmpty(PostConsumerContent)) {
        Add($"PostConsumerContent⸮{PostConsumerContent}");
    }
    if (!String.IsNullOrEmpty(RecycledPostConsumerContent)) {
        Add($"RecycledPostConsumerContent⸮{RecycledPostConsumerContent}");
    }
    else if (!String.IsNullOrEmpty(RecycledContent)) {
        Add($"RecycledPostConsumerContent⸮{RecycledContent}");
    }
    else if (!String.IsNullOrEmpty(PostConsumerContent)) {
        Add($"RecycledPostConsumerContent⸮{PostConsumerContent}");
    }
}

//  Pack Size for all
void Pack_Size() {
    var result = "";
    var containerTypeResult = "";
    // for juices use container type
    var containerType = !(R("SP-24477") is null) || !R("SP-24477").Text.Equals("<NULL>") ? R("SP-24477").Text : 
    !(R("cnet_common_SP-24477") is null) || !R("cnet_common_SP-24477").Text.Equals("<NULL>") ? R("cnet_common_SP-24477").Text : "";
    var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM")); //Filing System - Features
    var Size = TX_UOM.ExtractNumbers().Any() ? TX_UOM.ExtractNumbers().First() : 0;
    var Pack = TX_UOM.Text.Split("/").Count() > 1 ? TX_UOM.Text.Split("/").Last().ToLower() : "";
    if (TX_UOM.HasValue("Set")) {
        result = "Sold as set";
        containerTypeResult = "Sold as set";
    }
    else if (TX_UOM.HasValue() && TX_UOM.Text.Split('/').Count() < 3) {
        if(TX_UOM.Text.Contains("Dozen")){
            result = "12 per pack";
            if (!String.IsNullOrEmpty(containerType)) {
                containerTypeResult = $"12 {Coalesce(containerType.ToLower()).Pluralize()} per pack";
            } else {
                containerTypeResult = "12 per pack";
            }
        }
        else if(Size != 0 && !String.IsNullOrEmpty(Pack)){
            result = $"{Size} per {Pack}";
            if (!String.IsNullOrEmpty(containerType)) {
                containerTypeResult = $"{Size} {Coalesce(containerType.ToLower()).Pluralize()} per {Pack}";;
            } else {
                containerTypeResult = $"{Size} per {Pack}";
            }

        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"PackSize⸮{result}");
    }
    if (!String.IsNullOrEmpty(containerTypeResult)) {
        Add($"DrinkPackSize⸮{containerTypeResult}");
    }
}
// Compliant Standards
void Compliant_Standards(){
    var CompliantStandards = "";
    var CompliantStandardsRef = R("SP-21659").HasValue() ? R("SP-21659").Replace("<NULL>", "").Text : 
    R("cnet_common_SP-21659").HasValue() ? R("cnet_common_SP-21659").Replace("<NULL>", "").Text : "";

    if(!String.IsNullOrEmpty(CompliantStandardsRef)){
        switch(CompliantStandardsRef.Split(',').Count()){
            case 1:
            CompliantStandards = $"Meets or exceeds {CompliantStandardsRef}".TrimEnd(',');
            break;
            case var a when a > 1:
            CompliantStandards = $"Meets or exceeds {CompliantStandardsRef.Split(", ").FlattenWithAnd(10, ", ")}";
            break;
        }
    }
    if(!String.IsNullOrEmpty(CompliantStandards)){
        Add($"CompliantStandards⸮{CompliantStandards}");
    }
}

// Warranty information
void Warr_anty(){
    var result = "";
    var referList = new List<string>(){"SP-16", "SP-5675", "SP-21932"};
    var Warranty = Coalesce(A[430]); // Remote Control - Features

    foreach(var refer in referList){
        var temp = R(refer).HasValue() ? R(refer).Replace("<NULL>", "").Text : 
    R($"cnet_common_{refer}").HasValue() ? R($"cnet_common_{refer}").Replace("<NULL>", "").Text : "";
        if(!String.IsNullOrEmpty(temp)){
                // Warranty = temp;
        var numbers = Coalesce(temp).ExtractNumbers();
        if(numbers.Any()){
            switch(temp.ToLower()){
                case var str when str.Contains("year"):
                result = $"{numbers.First()}-year manufacturer limited warranty";
                break;
                case var str when str.Contains("month"):
                result = $"{numbers.First()}-month manufacturer limited warranty";
                break;
                case var str when str.Contains("day"):
                result = $"{numbers.First()}-day manufacturer limited warranty";
                break;
                }
            }
        }
        else if(refer.Contains("replacement")){
            result = "Lifetime manufacturer limited replacement  warranty";
            break;
        }
        else if(refer.Contains("ifetime manufacturer")){
            result = "Lifetime manufacturer limited warranty";
            break;
        }
        else if(Warranty.HasValue()){
            var numbers = Warranty.Values.ExtractNumbers();
            if(numbers.Any()){
               switch(Warranty.FirstValue()){
                   case var str when str.In("%year%"):
                   result = $"{numbers.First()}-year manufacturer limited warranty";
                   break;
                   case var str when str.In("%month%"):
                   result = $"{numbers.First()}-month manufacturer limited warranty";
                   break;
                   case var str when str.In("%day%"):
                   result = $"{numbers.First()}-day manufacturer limited warranty";
                   break;
               }
            }
            else if(Warranty.HasValue("%replacement%")){
                result = "Lifetime manufacturer limited replacement  warranty";
                break;
            }
            else if(Warranty.HasValue("%ifetime manufacturer%")){
                result = "Lifetime manufacturer limited warranty";
                break;
            }
        }
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"Warranty⸮{result}");
    }
}

// Dimensions as follows: If “Standup (3D)” use Height (floor/base to top) x Width (side to side) x Depth (front to back) in inches as H x W x D. If lay flat (2D such as paper) use Width (side to Side) x Length (top to bottom) in inches. If layflat has accountable Thickness (over .125”), add as Depth so use H x W x D.
void Dimen_sions(){
    var Dimensions = "";
    var DimensionsWeight_Width = R("SP-21044").HasValue() ? R("SP-21044").Replace("<NULL>", "").Text : 
    R("cnet_common_SP-21044").HasValue() ? R("cnet_common_SP-21044").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Width
    var DimensionsWeight_Depth = R("SP-20657").HasValue() ? R("SP-20657").Replace("<NULL>", "").Text : 
    R("cnet_common_SP-20657").HasValue() ? R("cnet_common_SP-20657").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Depth
    var DimensionsWeight_Height = R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
    R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Depth
    var DimensionsWeight_Diameter = Coalesce(R("SP-21045").Replace("<NULL>", "").Text, R("SP-21453").Replace("<NULL>", "").Text, R("SP-21629").Replace("<NULL>", "").Text); //Dimensions & Weight - Depth
    var HeatersCoolers_HeatingCooling_SpecialFeatures_Details = Coalesce(A[4753], A[9898]); // Heaters & Coolers - Heating & Cooling - Details | Special Features

    if(!String.IsNullOrEmpty(DimensionsWeight_Width) 
        && !String.IsNullOrEmpty(DimensionsWeight_Depth)
        && !String.IsNullOrEmpty(DimensionsWeight_Height)){
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""D" :
        $@"Dimensions: {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""D";
    }
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Width)){
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""W x {DimensionsWeight_Width}""L" :
        $@"Dimensions: {DimensionsWeight_Height}""W x {DimensionsWeight_Width}""L";
    }
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Depth)){
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""W x {DimensionsWeight_Depth}""L" :
        $@"Dimensions: {DimensionsWeight_Height}""W x {DimensionsWeight_Depth}""L";
    }
    else if(!String.IsNullOrEmpty(DimensionsWeight_Width) && !String.IsNullOrEmpty(DimensionsWeight_Depth)){
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""L" :
        $@"Dimensions: {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""L";
    }
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Diameter)){
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""W x {DimensionsWeight_Diameter}""Dia." :
        $@"Dimensions: {DimensionsWeight_Height}""W x {DimensionsWeight_Diameter}""Dia.";
    }
    if(!String.IsNullOrEmpty(Dimensions)){
        Add($"Dimensions⸮{Dimensions}");
    }
}

// "Caffeine Free" 
void CaffeineFree() {
    var result = "";
    var cafFree = Coalesce(A[6733].HasValue());
    if (cafFree) {
        result = "This drink is caffeine free";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"CafFree⸮{result}");
    }
}

// "Additional Kosher" 
void Kosher() {
    var result = "";
    var attrKosher = Coalesce(A[6739]);
    var productLine = !(SKU.ProductLineName is null) ? SKU.ProductLineName.Text.ToLower() : "";
    var modelName = !(SKU.ModelName is null) ? SKU.ModelName.Text.ToLower() : "";
    var description = !(SKU.Description is null) ? SKU.Description.Text.ToLower() : "";
    var isKosher = false;
    if (Coalesce(productLine).In("%kosher%", "%orthodox union kosher%") || Coalesce(modelName).In("%kosher%", "%orthodox union kosher%") || Coalesce(description).In("%kosher%", "%orthodox union kosher%")) {
        isKosher = true;
    }
    if (attrKosher.HasValue("Orthodox Union Kosher")
        || attrKosher.HasValue("kosher")
        || DC.KSP.GetString().In("%kosher%", "%orthodox union kosher%") 
        || DC.MKT.GetString().In("%kosher%", "%orthodox union kosher%")
        || DC.WIB.GetString().In("%kosher%", "%orthodox union kosher%")
        || DC.FEAT.GetString().In("%kosher%", "%orthodox union kosher%")
        || isKosher) {
       result = "The product is kosher";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"Kosher⸮{result}");
    }
}

// "Additional Acid Free" 
 void Acid_Free(){
    var AcidFree = "";
    var Features = Coalesce(A[5945], A[6031], A[6037]);
    var itemInfo = R("SP-21736").HasValue() ? R("SP-21736").Replace("<NULL>", "").Text : R("cnet_common_SP-21736").HasValue() ?  R("cnet_common_SP-21736").Replace("<NULL>", "").Text : "";
    if(Features.HasValue("acid free")
    || DC.KSP.GetString().In("%acid free%") 
    || DC.MKT.GetString().In("%acid free%")
    || DC.WIB.GetString().In("%acid free%")
    || DC.FEAT.GetString().In("%acid free%")
    || (!String.IsNullOrEmpty(itemInfo) && itemInfo.ToLower().Equals("Yes"))) {
        AcidFree = "This product is acid free";
    }
    if(!String.IsNullOrEmpty(AcidFree)){
        Add($"AcidFree⸮{AcidFree}");
    }
}

// "Additional Gluten Free" 
void GlutenFree() {
     var result = "";
     var attrGlutenFree = Coalesce(A[6739]);
     var productLine = !(SKU.ProductLineName is null) ? SKU.ProductLineName.Text.ToLower() : "";
    var modelName = !(SKU.ModelName is null) ? SKU.ModelName.Text.ToLower() : "";
    var description = !(SKU.Description is null) ? SKU.Description.Text.ToLower() : "";

     var isGlutenFree = false;
     if (Coalesce(productLine).In("%gluten%free%") || Coalesce(modelName).In("%gluten%free%") || Coalesce(description).In("%gluten%free%")) {
         isGlutenFree = true;
    }
    if (attrGlutenFree.HasValue("gluten-free")
        || DC.KSP.GetString().In("%gluten%free%") 
        || DC.MKT.GetString().In("%gluten%free%")
        || DC.WIB.GetString().In("%gluten%free%")
        || DC.FEAT.GetString().In("%gluten%free%")
        || isGlutenFree) {
        result = "Gluten-free";
    }
     if (!String.IsNullOrEmpty(result)) {
         Add($"GlutenFree⸮{result}");
    }
}

// "Sugar free" 
void SugarFree() {
    var sugarFreeResult = "";
    var sugarFree = Coalesce(A[6739].HasValue("sugar-free"));
    if (sugarFree) {
        sugarFreeResult = "No sugar added";
    }
    if (!String.IsNullOrEmpty(sugarFreeResult)) {
        Add($"SugarFree⸮{sugarFreeResult}");
    }
}

// "True color & Label Material" 
void TrueColor_Material(){
    var TrueColorMaterial = "";
    var resultTrueColor = "";
    var resultMaterial = "";
    var TrueColor = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : 
        R("SP-21278").HasValue() ? R("SP-21278").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21278").HasValue() ? R("cnet_common_SP-21278").Replace("<NULL>", "").Text : ""; // True Color
    var Material = R("SP-22251").HasValue() ? R("SP-22251").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22251").HasValue() ? R("cnet_common_SP-22251").Replace("<NULL>", "").Text : 
        R("SP-21408").HasValue() ? R("SP-21408").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21408").HasValue() ? R("cnet_common_SP-21408").Replace("<NULL>", "").Text : ""; // Label Material

    if(!String.IsNullOrEmpty(TrueColor)){
        switch(TrueColor.ToLower()){
            case var a when !String.IsNullOrEmpty(a) && !String.IsNullOrEmpty(Material):
            // Made from sticker paper and come in white color
            var Mat = "";
            Mat = Coalesce(Material).RegexReplace(@"(^[A-Z]{2,})|.*", "$1");
            var Mat2 = !string.IsNullOrEmpty(Mat) ? Mat : Material.ToLower(true);
            TrueColorMaterial = $"Comes in {TrueColor.ToLower()} and made of {Mat2}";
            break;
            case var a when a.Equals("assorted"):
            TrueColorMaterial = $"{TrueColor.ToUpperFirstChar()} colors";
            resultTrueColor = $"{TrueColor.ToUpperFirstChar()} colors";
            break;
            case var a when !a.Equals("multicolor"):
            TrueColorMaterial = $"Comes in {TrueColor.ToLower().Replace("meteorite with black stand", "meteorite color with black stand")}";
            resultTrueColor = $"Comes in {TrueColor.ToLower().Replace("meteorite with black stand", "meteorite color with black stand")}";
            break;
        }
    }else if(!(SPEC["MS"].GetLine("Color") is null)){
        var color = SPEC["MS"].GetLine("Color").Body;
        switch(color){
            case var temp when temp.ToLower().In("transparent", "clear"):
            TrueColorMaterial = "Comes in сlear";
            resultTrueColor = "Comes in сlear";
            break;
            case var temp when temp.ToString().Split(", ").Count() > 0:
            TrueColorMaterial = $"Comes in {color.ToLower().Replace("meteorite with black stand", "meteorite color with black stand")}";
            resultTrueColor =  $"Comes in {color.ToLower().Replace("meteorite with black stand", "meteorite color with black stand")}";
            break;

        }
    }
    if(!String.IsNullOrEmpty(Material)){
        resultMaterial = $"Made of {Material}";
    }
    if(!String.IsNullOrEmpty(TrueColorMaterial)){
        Add($"TrueColorMaterial⸮{TrueColorMaterial}");
    }
    if (!String.IsNullOrEmpty(resultTrueColor)) {
        Add($"TrueColor⸮{resultTrueColor}");
    }
    if (!String.IsNullOrEmpty(resultMaterial)) {
        Add($"Material⸮{resultMaterial}");
    }
}

// "Processor Type" 
void Processor_Type_All(){
    var ProcessorType = "";
    var Processor_Manufacturer = Coalesce(A[39]); //P rocessor - Manufacturer
    var Processor_ClockSpeed = Coalesce(A[40]); // Processor - Clock Speed
    var Processor_Type = Coalesce(A[35]); // Processor - Type
    var Processor = Coalesce(A[39], A[40], A[35]);
    var Processor_MaxTurboSpeed = Coalesce(A[5424]); // Processor - Max Turbo Speed
    var CacheMemory_InstalledSize = Coalesce(A[400]); // Cache Memory - Installed Size
    var Processor_ProcessorNumber = Coalesce(A[3267]); // Processor - Processor Number
    var Processor_NumberOfCores = Coalesce(A[3483]); // Processor - Number of Cores

    if(!Processor.HasValue()){
        ProcessorType = "";
    }
    else if(Processor_MaxTurboSpeed.HasValue()
        && CacheMemory_InstalledSize.HasValue()
        && Processor_Manufacturer.HasValue()){
        var Type = ""; 
        var NumberOfCores = "";
        var MaxTurboSpeed = "";
        var UnitMaxTurboSpeed = "";
        var InstalledSize = "";
        var UnitInstalledSize = "";
        var ClockSpeed = Processor_ClockSpeed.HasValue() ? Processor_ClockSpeed.FirstValue() : "";
        var Unit = Processor_ClockSpeed.HasValue() ? $"{Processor_ClockSpeed.Units.First().Name} " : "";
        var Manufacturer = Processor_Manufacturer.HasValue() ? $"{Processor_Manufacturer.FirstValue()} " : "";
        var ProcessorNumber = Processor_ProcessorNumber.HasValue() ? $"{Processor_ProcessorNumber.FirstValue()} " : "";

        // S21607123 - 2.8GHz Intel Core i5-8400 hexa-core processor with up to 4GHz speed and 9MB cache memory
        if(Processor_Manufacturer.HasValue("Intel")){
            Type = Processor_Type.HasValue() ? $"{Processor_Type.FirstValue().RegexReplace(@"(Core i).*", "Core")} " : "";
            NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower().Replace("6-core", "hexa-core")} " : "";
            MaxTurboSpeed = Processor_MaxTurboSpeed.HasValue() ? $" up to {Processor_MaxTurboSpeed.FirstValue()}" : "";
            UnitMaxTurboSpeed = Processor_MaxTurboSpeed.HasValue() ? $"{Processor_MaxTurboSpeed.Units.First().Name} speed" : "";
            InstalledSize = CacheMemory_InstalledSize.HasValue() ? $" and {CacheMemory_InstalledSize.FirstValue()}" : "";
            UnitInstalledSize = CacheMemory_InstalledSize.HasValue() ? $"{CacheMemory_InstalledSize.Units.First().Name} cache memory" : "";

            ProcessorType = $"{ClockSpeed}{Unit}{Manufacturer}{Type}{ProcessorNumber}{NumberOfCores}processor with{MaxTurboSpeed}{UnitMaxTurboSpeed}{InstalledSize}{UnitInstalledSize}";
        }
        // S21056776 - Features 2.5GHz (up to 2.9GHz) AMD A-series A6-9220 dual-core processor with 1MB cache memory
        else if(Processor_Manufacturer.HasValue("AMD")){
            MaxTurboSpeed = Processor_MaxTurboSpeed.HasValue() ? $"(up to {Processor_MaxTurboSpeed.FirstValue()}" : "";
            UnitMaxTurboSpeed = Processor_MaxTurboSpeed.HasValue() ? $"{Processor_MaxTurboSpeed.Units.First().Name}) " : "";
            Type = Processor_Type.HasValue() ? $"{Processor_Type.FirstValue().ToString().Split(' ').Where(s => Coalesce(s).In("A%")).Flatten().IfLongerThan(0, "A-series")} " : "";
            NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower()} " : "";
            InstalledSize = CacheMemory_InstalledSize.HasValue() ? $" {CacheMemory_InstalledSize.FirstValue()}" : "";
            UnitInstalledSize = CacheMemory_InstalledSize.HasValue() ? $"{CacheMemory_InstalledSize.Units.First().Name} cache memory" : "";

            ProcessorType = $"Features {ClockSpeed}{Unit}{MaxTurboSpeed}{UnitMaxTurboSpeed}{Manufacturer}{Type}{ProcessorNumber}{NumberOfCores}processor with{InstalledSize}{UnitInstalledSize}";
        }
    }
    // S21133725 - Experience responsive performance, power, adaptability and fun with Intel Celeron 2GHz processor
    else if(Processor_Type.HasValue("Celeron")){
        var ClockSpeed = Processor_ClockSpeed.HasValue() ? Processor_ClockSpeed.FirstValue() : "";
        var Unit = Processor_ClockSpeed.HasValue() ? $"{Processor_ClockSpeed.Units.First().Name} " : "";

        ProcessorType = $"Experience responsive performance, power, adaptability and fun with Intel Celeron {ClockSpeed}{Unit}processor";
    }
    else if(CAT.Alt.Count() > 0 && CAT.MainAlt.Key.In("3364010221167289")){
        var Manufacturer = Processor_Manufacturer.HasValue() ? $"{Processor_Manufacturer.FirstValue()} " : "";
        var NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower()} " : "";
        var ClockSpeed = Processor_ClockSpeed.HasValue() ? Processor_ClockSpeed.FirstValue() : "";
        var Unit = Processor_ClockSpeed.HasValue() ? $"{Processor_ClockSpeed.Units.First().Name} " : "";
        var ProcessorNumberOrType = Coalesce($"{Processor_ProcessorNumber.FirstValue()} ", $"{Processor_Type.FirstValue()} ");

        ProcessorType = $"{Manufacturer}{ProcessorNumberOrType}{NumberOfCores}{ClockSpeed}{Unit}processor delivers quality performance";
    }
    // S21124162 - 3.7GHz AMD 2700X 8-core processor for ultimate performance
    else{
        var ClockSpeed = Processor_ClockSpeed.HasValue() ? Processor_ClockSpeed.FirstValue() : "";
        var Unit = Processor_ClockSpeed.HasValue() ? $"{Processor_ClockSpeed.Units.First().Name} " : "";
        var Manufacturer = Processor_Manufacturer.HasValue() ? $"{Processor_Manufacturer.FirstValue()} " : "";
        var ProcessorNumberOrType = Coalesce($"{Processor_ProcessorNumber.FirstValue().Replace("i3","Core i3")} ", $"{Processor_Type.FirstValue()} ");
        var NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower()} " : "";

        ProcessorType = $"{ClockSpeed}{Unit}{Manufacturer}{ProcessorNumberOrType}{NumberOfCores}processor for ultimate performance";
    }
    if(!String.IsNullOrEmpty(ProcessorType)){
        Add($"ProcessorType⸮{ProcessorType}");
    }
}

// --Operating System
void Operating_System(){
    var OperatingSystem = "";
    var OSProvided_Type = Coalesce(A[16]); // OS Provided - Type

    if(OSProvided_Type.HasValue()){
        switch(OSProvided_Type){
            case var a when a.HasValue("%Windows 10 pro 64-bit%"):
            OperatingSystem = "Works on Windows 10 Pro, 64-bit operating system for an intuitive and user-friendly interface";
            break;
            // S21492722 - Windows 10 Home gives you the familiar feel of Windows with enhanced capabilities
            case var a when a.HasValue("%Windows 10 Home%"):
            OperatingSystem = "Windows 10 Home gives you the familiar feel of Windows with enhanced capabilities";
            break;
            // S13586477 - Google Chrome operating system enables effortless Internet browsing
            case var a when a.HasValue("%Google Chrome%"):
            OperatingSystem = "Google Chrome operating system enables effortless Internet browsing";
            break;
            // S21492718 - Windows 10 Pro operating system provides an intuitive and user-friendly interface
            case var a when a.HasValue("%indows%"):
            var windows = OSProvided_Type.FirstValue().Replace(" Windows", "Windows").Replace(" Edition", "").Replace("Microsoft", "").Replace(" for Thin Clients", "").Replace("Workstations", "workstations");
            OperatingSystem = $"{windows} operating system provides an intuitive and user-friendly interface";
            break;
            // S3651629 - Linux OS smartly manages your hardware and runs routine applications efficiently
            case var a when a.HasValue("%inux%"):
            OperatingSystem = "Linux OS smartly manages your hardware and runs routine applications efficiently";
            break;
            // S20631523 - HP ThinPro operating system offers an intuitive and user-friendly interface
            case var a when a.HasValue("%HP%ThinPro%"):
            OperatingSystem = "HP ThinPro operating system offers an intuitive and user-friendly interface";
            break;
        }
    }
    if(!String.IsNullOrEmpty(OperatingSystem)){
        Add($"OperatingSystem⸮{OperatingSystem}");
    }
}

// --RAM type
void RAM_Type(){
    var RAMType = "";
    var RAM_InstalledSize = Coalesce(A[53]); // RAM - Installed Size
    var RAM_Technology = Coalesce(A[56]); // RAM - Technology
    bool isNotGb = RAM_InstalledSize.HasValue() && RAM_InstalledSize.Units.First().Name.NotIn("GB") ? true : false;

    if(RAM_InstalledSize.HasValue()){
        var UnitRam = $"{RAM_InstalledSize.Units.First().Name} ";
        var RAM = "";

        switch(RAM_InstalledSize.FirstValue()){
            case var a when a == 0 || isNotGb:
            RAMType = "";
            break;
            // S4464584 - 2GB DDR2 memory makes you productive with its smooth performance (https://www.staples.com/product_2800417?akamai-feo=off)
            case var a when a < 4 && RAM_Technology.HasValue():
            RAM = $" {RAM_Technology.FirstValue().Replace(" SDRAM", "")}";
            RAMType = $"{RAM_InstalledSize.FirstValue()}{UnitRam}{RAM} memory makes you productive with its smooth performance";
            break;
            // S21340202 - 16GB SDRAM memory to run multiple programs (https://www.staples.com/HP-EliteBook-x360-1030-G2-13-3-Touchscreen-LCD-2-in-1-Notebook-Intel-Core-i7-i7-7600U-Dual-core-2-8GHZ-16GB-DDR4-SDRAM/product_IM19Y3295)
            case var a when a >= 16 && RAM_Technology.HasValue():
            RAM = $"{RAM_Technology.FirstValue().Replace("DDR4", "")}";
            RAMType = $"{RAM_InstalledSize.FirstValue()}{UnitRam}{RAM} memory to run multiple programs";
            break;
            // S21280151 - 8GB DDR4 SDRAM smoothly run your games, photo and video editing applications (https://www.staples.com/Dell-Latitude-7480-14-Laptop-LCD-Core-i5-7300U-256GB-SSD-8GB-RAM-WIN-10-Pro-Black/product_24122677)
            case var a when a >= 8 && RAM_Technology.HasValue():
            RAM = $" {RAM_Technology.FirstValue()}";
            RAMType = $"{RAM_InstalledSize.FirstValue()}{UnitRam}{RAM} smoothly run your games, photo and video editing applications";
            break;
            // S21580875 - With 8GB memory, you can multitask between various applications without issue (https://www.staples.com/acer-aspire-5-a517-51-33q4-17-3-lcd-notebook-intel-core-i3-6th-gen-i3-6006u-2-core-2-ghz-8gb-ddr4-sdram-1-tb-hdd/product_IM12GP105)
            case var a when a == 8:
            RAMType = $"With {RAM_InstalledSize.FirstValue()}{UnitRam} memory, you can multitask between various applications without issue";
            break;
            // 21251351 - 2GB memory seamlessly handles multiple programs together (https://www.staples.com/asus-vivobook-e203na-dh02-11-6-lcd-netbook-intel-celeron-n3350-dual-core-2-core-1-10-ghz-4-gb-ddr3-sdram/product_IM12ML083)
            default:
            RAMType = $"{RAM_InstalledSize.FirstValue()}{UnitRam} memory seamlessly handles multiple programs together";
            break;
        }
    }
    if(!String.IsNullOrEmpty(RAMType)){
        Add($"RAMType⸮{RAMType}");
    }
}

// --Connectivity (wireless)
void Wireless_Connectivity(){
    var WirelessConnectivity = "";
    var Networking_DataLinkProtocol = Coalesce(A[306]); // Networking - Data Link Protocol
    var Networking_WirelessProtocol = Coalesce(A[4645]); // Networking - Wireless Protocol
    var Networking_WirelessNIC = Coalesce(A[3266]); // Networking - Wireless NIC
    var Notebooks_Networking_Features = Coalesce(A[2873]); // Notebooks - Networking - Features

    // The improved 802.11 ac 2x2 Wi-Fi antenna delivers a stronger, more reliable Internet connection than before. (https://www.staples.com/HP-Stream-Laptop-14-ax069st--Office-365-Personal-included-/product_2802786)
    if(Notebooks_Networking_Features.HasValue("%2x2%")){
        WirelessConnectivity = Networking_WirelessProtocol.HasValue("802.11%") ?
        $"The improved {Networking_WirelessProtocol.Where("802.11%").First().Value()} 2x2 Wi-Fi antenna delivers a stronger, more reliable Internet connection than before" : "";
    }
    // S21498853 - 802.11ac wireless connectivity for easy internet access (https://www.staples.com/product_2634349?akamai-feo=off)
    else if(Networking_DataLinkProtocol.Where("ieee %").Count() == 1){
        WirelessConnectivity = Networking_WirelessProtocol.HasValue("802.11%") ?
        $"{Networking_WirelessProtocol.Where("802.11%").First().Value()} wireless connectivity for easy internet access" : "";
    }
    // S21213908 - Supports 802.11a/b/g/n/ac and Bluetooth 5.0 wireless connectivity for easy internet access
    else if(Networking_DataLinkProtocol.Where("ieee %").Count() > 1){
        var WirelessNIC = Networking_WirelessNIC.HasValue() ? Networking_WirelessNIC.FirstValue() : "";
        var Bluetooth = Networking_WirelessProtocol.HasValue("bluetooth%") ? $" and {Networking_WirelessProtocol.Where("bluetooth%").First().Value().ToUpperFirstChar()}" : "";

        WirelessConnectivity = Networking_WirelessProtocol.HasValue("802.11%") ?
        $"Supports {WirelessNIC}{Networking_WirelessProtocol.Where("802.11%").First().Value()}{Bluetooth} wireless connectivity for easy internet access" : "";
    }
    if(!String.IsNullOrEmpty(WirelessConnectivity)){
        Add($"WirelessConnectivity⸮{WirelessConnectivity}");
    }
}

// --Graphics
void Graphics_All(){
    var Graphics = "";
    var VideoMemory_InstalledSize = Coalesce(A[241]); //Video Memory - Installed Size
    var VideoMemory_Technology = Coalesce(A[244]); // Video Memory - Technology
    var VideoOutput_GraphicsProcessor = Coalesce(A[247]); // Video Output - Graphics Processor

    if(VideoOutput_GraphicsProcessor.HasValue()){
        var Technology = VideoMemory_Technology.HasValue() ? $" {VideoMemory_Technology.FirstValue().Replace("SDRAM", "")}" : "";
        var Size = VideoMemory_InstalledSize.HasValue() ? $" {VideoMemory_InstalledSize.FirstValue()}" : "";
        var UnitsSize = VideoMemory_InstalledSize.HasValue() ? VideoMemory_InstalledSize.Units.First().Name : "";
        var GraphicsProcessor = VideoOutput_GraphicsProcessor.FirstValue().ToString().Split('/').First();

        switch(VideoOutput_GraphicsProcessor){
            // S21531860 - Experience smooth, lag-free performance with AMD Radeon Vega 6 graphic card (https://www.staples.com/HP-ZBook-14u-G4-14-Touchscreen-LCD-Mobile-Workstation-Intel-Core-i7-7500U-Dual-core-2-7GHZ-8GB-DDR4-SDRAM-256GB-SSD/product_IM18W3275?akamai-feo=off)
            case var a when a.HasValue("%AMD%"):
            Graphics = $"Experience smooth, lag-free performance with {GraphicsProcessor}{Size}{UnitsSize} graphic card";
            break;
            // S21204883 - NVIDIA Quadro P600 2GB discrete graphic card provides excellent ability in a variety of multimedia applications and user experiences (https://www.staples.com/lenovo-thinkpad-p52s-20lb0022us-15-6-inch-laptop-computer-core-i7-16-gb-windows-10-pro-english/product_IM13BQ311)
            case var a when a.HasValue("%Quadro%"):
            Graphics = $"{GraphicsProcessor}{Size}{UnitsSize} discrete graphic card provides excellent ability in a variety of multimedia applications and user experiences";
            break;
            case var a when a.HasValue("%intel%Nvidia%"):
            Graphics = $"{VideoOutput_GraphicsProcessor.FirstValue().ToString().Split('/').Last() + " graphics and"}{Size}{UnitsSize}{Technology} dedicated graphics memory ensure ultimate gaming experience";
            break;
            // S21174772 - NVIDIA GeForce GTX 1050 graphics and 2GB GDDR5  dedicated graphics memory ensure ultimate gaming experience (https://www.staples.com/hp-omen-x-17-ap020nr-17-3-laptop-intel-core-i7-7820hk-1tb-hdd-256gb-ssd-16gb-ram-win-10-home-geforce-gtx-1080/product_24298705)
            case var a when a.HasValue("%Nvidia%"):
            Graphics = $"{GraphicsProcessor + " graphics and"}{Size}{UnitsSize}{Technology} dedicated graphics memory ensure ultimate gaming experience";
            break;
            // Intel HD Graphics 620 provides everyday image quality for internet usage, basic photo editing and casual gaming (https://www.staples.com/Dell-Latitude-7480-14-Laptop-LCD-Core-i5-7300U-256GB-SSD-8GB-RAM-WIN-10-Pro-Black/product_24122677)
            case var a when a.HasValue("Intel% 6%", "Intel% 5%", "Intel% P6%", "Intel% P5%", "Intel%HD%Graphics%"):
            Graphics = $"{GraphicsProcessor} shared graphic card for lag-free and smooth performance";
            break;
            // Intel HD Graphics render all the visuals on screen with smooth, vivid quality (https://www.staples.com/HP-15-bs061st-15-6-Laptop-Computer-Intel-Pentium-500GB-SATA-HD-8GB-DDR3L-Windows-10-Intel-HD-Graphics-405/product_2720612)
            case var a when a.HasValue("Intel HD Graphics%"):
            Graphics = "Intel HD Graphics: render all the visuals on screen with smooth, vivid quality";
            break;
        }
    }
    if(!String.IsNullOrEmpty(Graphics)){
        Add($"Graphics⸮{Graphics}");
    }
}

// return value in XX"W x YY"H format
string cnet_sheet_dimension_notebooks_notepads() {
    var result = "";
    var sheetDimention = R("SP-18229").HasValue() ? R("SP-18229").Replace("<NULL>", "").Text : 
    R("cnet_common_SP-18229").HasValue() ? R("cnet_common_SP-18229").Replace("<NULL>", "").Text : "";
    var paperFormat = Coalesce(A[6018], A[6069]);
    if (!String.IsNullOrEmpty(sheetDimention) && !sheetDimention.ToLower().Equals("other")) {
        var tmp = Coalesce(sheetDimention).RegexReplace(@"(\d*"")(\sx\s)(\d*"")", "$1H$2$3L" );
        result = tmp;
    }
    else if (paperFormat.HasValue()) {
        if (paperFormat.FirstValueUsm().ExtractNumbers().Count() == 2) {
            var num1 = Math.Round((double)paperFormat
                .FirstValueUsm()
                .Replace("0.02", "")
                .Replace(".51", ".5")
                .ExtractNumbers()[0], 2);
            var num2 = Math.Round((double)paperFormat
                .FirstValueUsm()
                .Replace("0.02", "")
                .Replace(".51", ".5")
                .ExtractNumbers()[1], 2);
            result = $"{num1}\"W x {num2}\"H";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        return result;
    } else {
        return "";
    }
}

// --Weight (lbs.)
void Weight_All(){
    var result = "";
    var WeightRef = R("SP-20402").HasValue() ? R("SP-20402").Replace("<NULL>", "").Text : 
    R("cnet_common_SP-20402").HasValue() ? R("cnet_common_SP-20402").Replace("<NULL>", "").Text : "";

    if(!String.IsNullOrEmpty(WeightRef)){
        result = $"Weighs {WeightRef} lbs.";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"WeightAll⸮{result}");
    }
}

//"Features for all Category END" 

//§§2324927166253 "Office Chairs BEGIN" "Serhii.O" 

Type_of_Chair_Use();
Office_Chairs_True_Color_Upholstery_Material();
Type_Of_Support_And_Ergonomic();
Office_Chairs_Overall_Dimensions();
Seat_Dimension();
Back_Dimension();
Arm_Type();
Tilt_Mechanism_Type();
Office_Chairs_Capacity();
Office_Chairs_Assembly_Required();
Certifications_Standards_ANSI_BIFMA();
// Warranty - All

// --[FEATURE #1]
// --Type of Chair & Use (Include specialty data such as Extended Use 24/7; Big & Tall; etc. as applicable)
void Type_of_Chair_Use(){
    var result = ""; 
    // No|Ergonomic
    var ergonomicRef = R("SP-21540").HasValue() ? R("SP-21540").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21540").HasValue() ? R("cnet_common_SP-21540").Replace("<NULL>", "").Text : "";
    // Bankers|Kneeling|Manager|Ball|Conference|Task|Executive|Computer and Desk
    var chairTypeRef = R("SP-21546").HasValue() ? R("SP-21546").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21546").HasValue() ? R("cnet_common_SP-21546").Replace("<NULL>", "").Text : "";
    var general_ProductType = Coalesce(A[7156]); // General - Product Type
    var general_Class = Coalesce(A[7157]); // General - Class
    var general_DesignedFor = Coalesce(A[7158]); // General - Designed For
    var general_Casters = Coalesce(A[7164]); /// General - Casters
    var general_Swivel = Coalesce(A[7165]); // General - Swivel
    var general_HoursADay = Coalesce(A[7170]); // General - Hours a Day
    var general_Features = Coalesce(A[7175]); // General - Features
    var seat_Features = Coalesce(A[7176]); // Seat - Features
    var backrest_Features = Coalesce(A[7205]); // Backrest - Features
    var base_CastersQty = Coalesce(A[7210]); // Base - Casters Qty
    var dimensionsWeight_BackrestHeight = Coalesce(A[7237]); // Dimensions & Weight - Backrest Height
    var armrests_PaddedArmrests = Coalesce(A[9499]); // Armrests - Padded Armrests
    var general_Style = Coalesce(A[10997]); // General - Style

    // -- Ergonomic kneeling chair for therapeutic comfort at work
    if(general_ProductType.HasValue("kneeling chair")){
        result = "Ergonomic kneeling chair for therapeutic comfort at work";
    }
    // -- Guest chair to enhance the look of your waiting areak
    else if(general_Class.HasValue("visitor")
        && (!general_Casters.HasValue() || !base_CastersQty.HasValue())){
        result = "Guest chair to enhance the look of your waiting area";
    }
    // -- Conference chair for a reception area, library, or study room  
    else if(general_Class.HasValue("visitor")
        || general_DesignedFor.HasValue() && general_DesignedFor.Where("conference room","reception", "library").Any()){
        result = "Conference chair for a reception area, library, or study room";
    }
    // -- Elegant side chair perfect for use in waiting rooms, reception areas and conference rooms
    else if(general_Class.HasValue("club chair")
        || general_ProductType.HasValue("armchair")){
        result = "Elegant side chair perfect for use in waiting rooms, reception areas and conference rooms";
    }
    // -- Gaming chair will take your gaming experience to next level
    else if(general_ProductType.HasValue("%gaming%")
        || general_DesignedFor.HasValue("%gaming%")){
        result = "Elegant side chair perfect for use in waiting rooms, reception areas and conference rooms";
    }
    // -- Contemporary office chair with padded arms for comfort
    else if(general_Style.HasValue("Contemporary")
        || armrests_PaddedArmrests.HasValue("Yes")){
        result = "Contemporary office chair with padded arms for comfort";
    }
    // -- High back ergonomic chair with pneumatic seat height adjustment for customization
    else if(dimensionsWeight_BackrestHeight.HasValue()
        && dimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
        && dimensionsWeight_BackrestHeight.Values.First().ValueUSM >= 22
        && (ergonomicRef.Equals("ergonomic") || general_Class.HasValue("ergonomic"))
        && general_Features.HasValue("pneumatic seat height adjustment")){
        result = "High-back ergonomic chair with pneumatic seat height adjustment for customization";
    }
    // -- Ergonomic office chair with molded back for daily office work
    else if((ergonomicRef.Equals("ergonomic") || general_Class.HasValue("ergonomic"))
        && backrest_Features.HasValue("molded")){
        result = "Ergonomic office chair with molded back for daily office work";
    }
    // -- Executive office chair with a swivel seat for maximum workspace use 
    else if(general_Swivel.HasValue("Yes")
        && general_Class.HasValue("executive")){
        result = "Executive office chair with a swivel seat for maximum workspace use";
    }
    // -- Executive high-back chair brings style and comfort to your office
    else if(dimensionsWeight_BackrestHeight.HasValue()
        && dimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
        && dimensionsWeight_BackrestHeight.Values.First().ValueUSM >= 22
        && general_Class.HasValue("executive")){
        result = "Executive high-back chair brings style and comfort to your office";
    }
    // -- Mesh managers chair gives breathable support
    else if(general_Class.HasValue("manager")){
        result = "Manager chair offers comfort and complements a contemporary look";
    }
    // -- Padded office chair with caster wheels for easy mobility
    else if((seat_Features.HasValue("padded") || backrest_Features.HasValue("padded"))
        && (base_CastersQty.HasValue() || general_Casters.HasValue("%Yes%"))){
        result = "Padded office chair with caster wheels for easy mobility";
    }
    // -- Lightweight chairs are easy to move, fold, and stack
    else if(general_Features.HasValue("foldable")){
        result = "Lightweight chairs are easy to move, fold, and stack";
    }
    // -- Office chair is a smart addition to any office space
    else if(!String.IsNullOrEmpty(chairTypeRef)){
        result = chairTypeRef.Equals("Computer and Desk") ? "Computer and desk chair is a smart addition to any office space" : $"{chairTypeRef} chair is a smart addition to any office space";
    }
    else if(chairTypeRef.Equals("Computer and Desk")){
        result = "Computer and desk chair is a smart addition to any office space";
    }
    // -- Task chair offers comfort and complements a contemporary look
    else if(general_Class.HasValue("task")
        && general_Style.HasValue("Contemporary")){
        result = "Task chair offers comfort and complements a contemporary look";
    }
    // -- Task chair ideal for time-intensive tasks
    else if(general_Class.HasValue("task")
        && general_HoursADay.HasValue() && general_HoursADay.FirstValue() >= 6){
        result = "Task chair ideal for time-intensive tasks";
    }
    // -- Task chair for comfortable office work
    else if(general_Class.HasValue("task")){
        result = "Task chair for comfortable office work";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"TypeofChairUse⸮{result}"); 
    }
}

// --[FEATURE #2]
// --True Color & Upholstery material (Note: if back upholstery and seat upholstery differ include both within this bullet)
void Office_Chairs_True_Color_Upholstery_Material(){
    var result = ""; 
    // Mesh|Luxura|Crepe|Leather|Vellum|Foam|Polyester|Plastic|Nylon|Metal|PVC|Polyurethane|Polypropylene|Polymer|Wood|Vinyl|Spun-elastic|Bungee Cord|LeatherSoft|Fabric|Faux Leather|Acrylic|Rattan
    var officeChairBackMaterialRef = R("SP-22290").HasValue() ? R("SP-22290").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22290").HasValue() ? R("cnet_common_SP-22290").Replace("<NULL>", "").Text : "";
    var seatMaterialRef = R("SP-522").HasValue() ? R("SP-522").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-522").HasValue() ? R("cnet_common_SP-522").Replace("<NULL>", "").Text : "";
    // Mesh|Faux Leather|Fabric|Bungee Cord|Metal|Leather|Wood|Plastic|Polyester|Acrylic|Vinyl|Nylon|Polyurethane
    var chairMaterialRef = R("SP-21525").HasValue() ? R("SP-21525").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21525").HasValue() ? R("cnet_common_SP-21525").Replace("<NULL>", "").Text : "";
    // Multicolor|Kraft|Orange|Manila|Green|Natural Kraft|Clear|Neon|Gold|Gray/Silver|Yellow|Blue|Brown|Assorted|Beige|Red|White|Black|Pink|Purple|Ivory|Metallic|Rose Gold
    var colorFamilyRef = R("SP-17441").HasValue() ? R("SP-17441").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-17441").HasValue() ? R("cnet_common_SP-17441").Replace("<NULL>", "").Text : "";
    var trueColorRef = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : "";
    var miscellaneous_ProductMaterial = Coalesce(A[372]); // Miscellaneous - Product Material
    var miscellaneous_Color = Coalesce(A[373]); // Miscellaneous - Color
    var miscellaneous_ColorCategory = Coalesce(A[5811]); // Miscellaneous - Color Category
    var general_Class = Coalesce(A[7157]); // General - Class
    var seatFeatures = Coalesce(A[7176]); // Seat - Features
    var seat_Color = Coalesce(A[7177]); // Seat - Color
    var seat_Material = Coalesce(A[7178]); // Seat - Material
    var backrest_Material = Coalesce(A[7203]); // Backrest - Material
    var backrest_Color = Coalesce(A[7204]); // Backrest - Color
    var base_Color = Coalesce(A[7209]); // Base - Color

    // -- Chair material and color (Bullet 2)
    if(Coalesce(officeChairBackMaterialRef.ToLower()).NotIn($"%{seatMaterialRef.ToLower()}%")
        && Coalesce(seatMaterialRef.ToLower()).NotIn($"%{officeChairBackMaterialRef.ToLower()}%")
        && !String.IsNullOrEmpty(officeChairBackMaterialRef)
        && !String.IsNullOrEmpty(seatMaterialRef)
        && !String.IsNullOrEmpty(colorFamilyRef)
        && backrest_Color.HasValue() && seat_Color.HasValue()
        && backrest_Color.FirstValue().In(seat_Color.FirstValue())){
        var backrestColor = $"{backrest_Color.FirstValue().ToUpperFirstChar()} ";
        result = $"{backrestColor}{officeChairBackMaterialRef.ToLower()} back and {colorFamilyRef.ToLower()} {seatMaterialRef.ToLower()} seat";
    }
    else if(Coalesce(officeChairBackMaterialRef.ToLower()).NotIn($"%{seatMaterialRef.ToLower()}%")
        && Coalesce(seatMaterialRef.ToLower()).NotIn($"%{officeChairBackMaterialRef.ToLower()}%")
        && !String.IsNullOrEmpty(officeChairBackMaterialRef)
        && !String.IsNullOrEmpty(seatMaterialRef)
        && backrest_Color.HasValue() && seat_Color.HasValue()
        && backrest_Color.FirstValue().In(seat_Color.FirstValue())){
        var backrestColor = $"{backrest_Color.FirstValue().ToUpperFirstChar()} ";
        result = $"{backrestColor}{officeChairBackMaterialRef.ToLower()} back and {seatMaterialRef.ToLower()} seat";
    }
    else if(Coalesce(officeChairBackMaterialRef.ToLower()).NotIn($"%{seatMaterialRef.ToLower()}%")
        && Coalesce(seatMaterialRef.ToLower()).NotIn($"%{officeChairBackMaterialRef.ToLower()}%")
        && !String.IsNullOrEmpty(officeChairBackMaterialRef)
        && !String.IsNullOrEmpty(seatMaterialRef)
        && backrest_Color.HasValue() && seat_Color.HasValue()
        && backrest_Color.FirstValue().NotIn(seat_Color.FirstValue())){
        var backrestColor = $"{backrest_Color.FirstValue().ToUpperFirstChar()} ";
        var seatColor = $"{seat_Color.FirstValue().ToLower()} ";
        result = $"{backrestColor}{officeChairBackMaterialRef.ToLower()} back and {seatColor}{seatMaterialRef.ToLower()} seat";
    }
    else if(chairMaterialRef.ToLower().Contains("leather")
        && trueColorRef.ToLower().Contains("burgundy")){
        result = $"Burgundy {chairMaterialRef.ToLower()} upholstery adds style and comfort";
    }
    // --These plastic back task chairs in choice of vibrant colors are the perfect addition to your office
    else if(backrest_Color.HasValue("%vibrant%")
        && !String.IsNullOrEmpty(officeChairBackMaterialRef)
        && general_Class.HasValue("task")){
        result = $"These {officeChairBackMaterialRef.ToLower()} back task chairs in choice of vibrant colors are the perfect addition to your office";
    }
    // -- Breathable black mesh back with padded mesh seat
    else if(backrest_Color.HasValue()
        && backrest_Material.HasValue("mesh")
        && seat_Material.HasValue("mesh")
        && seatFeatures.HasValue("%padded%")
        && base_Color.HasValue() && backrest_Color.HasValue()
        && base_Color.FirstValue().NotIn(backrest_Color.FirstValue())){
        var backrestColor = $" {backrest_Color.FirstValue().ToUpperFirstChar()}";
        result = $"Breathable{backrestColor} mesh back with padded mesh seat and {base_Color.FirstValue()} base";
    }
    else if(backrest_Color.HasValue()
        && backrest_Material.HasValue("mesh")
        && seat_Material.HasValue("mesh")
        && seatFeatures.HasValue("%padded%")){
        var backrestColor = $" {backrest_Color.FirstValue().ToUpperFirstChar()}";
        result = $"Breathable{backrestColor} mesh back with padded mesh seat";
    }
    // -- Black vinyl seat with breathable mesh back for comfort
    else if(seat_Color.HasValue()
        && !String.IsNullOrEmpty(chairMaterialRef)
        && backrest_Material.HasValue("mesh")){
        result = $"{seat_Color.FirstValue().ToUpperFirstChar()} {chairMaterialRef.ToLower()} seat with breathable mesh back for comfort";
    }
    // -- Black bonded leather and mesh provide breathable durability
    else if(miscellaneous_ColorCategory.HasValue()
        && chairMaterialRef.Contains("bonded leather")
        && (miscellaneous_ProductMaterial.HasValue("mesh") || seat_Material.HasValue("mesh") || backrest_Material.HasValue("mesh"))){
        var tmp = miscellaneous_Color.HasValue() ? miscellaneous_Color.FirstValue().ToUpperFirstChar() : 
        miscellaneous_ColorCategory.FirstValue().ToUpperFirstChar();
        result = $"{tmp} bonded leather and mesh provide breathable durability";
    }
    // -- Black leather-like cushioned seat is easy to clean with a damp cloth
    else if(chairMaterialRef.ToLower().Equals("leather")
        && seat_Color.HasValue()){
        result = $"{seat_Color.FirstValue().ToUpperFirstChar()} {chairMaterialRef.ToLower()} cushioned seat is easy to clean with a damp cloth";
    }
    // --Black LeatherSoft upholstery is soft and durable
    else if(miscellaneous_ColorCategory.HasValue()
        && (miscellaneous_ProductMaterial.HasValue("LeatherSoft") || seat_Material.HasValue("LeatherSoft") || backrest_Material.HasValue("LeatherSoft"))){
        var tmp = miscellaneous_Color.HasValue() ? miscellaneous_Color.FirstValue().ToUpperFirstChar() : miscellaneous_ColorCategory.FirstValue().ToUpperFirstChar();
        result = $"{tmp} LeatherSoft upholstery is soft and durable";
    }
    // -- Black mesh upholstery allows air to circulate
    else if(!String.IsNullOrEmpty(colorFamilyRef)
        && chairMaterialRef.ToLower().Equals("mesh")){
        var tmp = !String.IsNullOrEmpty(trueColorRef) ? trueColorRef.ToUpperFirstChar() : 
        colorFamilyRef.ToUpperFirstChar();
        result = $"{tmp} {chairMaterialRef.ToLower()} upholstery allows air to circulate";
    }
    else if(!String.IsNullOrEmpty(colorFamilyRef)
        && !chairMaterialRef.ToLower().Equals("mesh")
        && !String.IsNullOrEmpty(chairMaterialRef)
        && (miscellaneous_ProductMaterial.HasValue("mesh", "mesh fabric") || seat_Material.HasValue("mesh", "mesh fabric") || backrest_Material.HasValue("mesh", "mesh fabric"))){
        var tmp = !String.IsNullOrEmpty(trueColorRef) ? trueColorRef.ToUpperFirstChar() : 
        colorFamilyRef.ToUpperFirstChar();
        result = $"{tmp} {chairMaterialRef.ToLower()} upholstery allows air to circulate";
    }
    // -- Black vinyl upholstery provides damage resistance
    else if(seat_Color.HasValue()
        && (miscellaneous_ProductMaterial.HasValue("%vinyl%") || seat_Material.HasValue("%vinyl%") || backrest_Material.HasValue("%vinyl%"))){
        result = $"{seat_Color.FirstValue().ToUpperFirstChar()} vinyl upholstery provides damage resistance";
    }
    // -- Black fabric chair for softness and a professional look
    else if(!String.IsNullOrEmpty(chairMaterialRef)
        && !chairMaterialRef.ToLower().Equals("wood")
        && miscellaneous_ColorCategory.HasValue()
        && miscellaneous_ColorCategory.WhereNot("silver").Any()){
        var tmp = miscellaneous_Color.HasValue() ? $"Comes in {miscellaneous_Color.WhereNot("silver").First().Value().ToLower()}" : 
        $"Comes in {miscellaneous_ColorCategory.WhereNot("silver").First().Value().ToLower()}";
        result = tmp;
        // -- color and offers softness and a professional look
    }
    // -- Upholstered with ultra soft, durable and breathable Black CaressoftPlus upholstery
    else if(backrest_Material.HasValue("CaressoftPlus")
        && backrest_Color.HasValue("Silver", "Chrome")){
        result = $"Upholstered with ultra soft, durable and breathable {backrest_Color.FirstValue().ToLower()} CaressoftPlus upholstery";
    }
    else if(miscellaneous_ProductMaterial.HasValue("CaressoftPlus")
        && miscellaneous_Color.HasValue() 
        && miscellaneous_Color.WhereNot("Silver", "Chrome").Any()){
        result = $"Upholstered with ultra soft, durable and breathable {miscellaneous_Color.WhereNot("Silver", "Chrome").First().Value().ToLower()} CaressoftPlus upholstery";
    }
    else if(miscellaneous_ProductMaterial.HasValue("%fabric%")
        && miscellaneous_Color.HasValue("available in different colors")){
        result = $"Sturdy {miscellaneous_ProductMaterial.Where("%fabric%").First().Value().ToLower()} material endures intense daily use";
    }
    // -- Navy blue fabric upholstery is easy to maintain
    else if(miscellaneous_ProductMaterial.HasValue()
        && miscellaneous_ProductMaterial.WhereNot("%mesh%", "%plastic%", "%metal%", "%steel%").Any()
        && miscellaneous_Color.HasValue("available in different colors")
        && miscellaneous_Color.WhereNot("Silver", "Chrome").Any()){
        result = $"Available in different colors {miscellaneous_ProductMaterial.WhereNot("%mesh%", "%plastic%", "%metal%", "%steel%").Flatten(", ").ToLower()} upholstery is easy to maintain";
    }
    else if(seat_Color.HasValue()
        && seat_Material.HasValue()
        && seat_Material.WhereNot("%mesh%", "%plastic%", "%metal%", "%steel%").Any()){
        result = $"{seat_Color.FirstValue().ToUpperFirstChar()} {seat_Material.WhereNot("%mesh%", "%plastic%", "%metal%", "%steel%").First().Value().ToLower()} upholstery is easy to maintain";
    }
    else if(backrest_Color.HasValue()
        && backrest_Material.HasValue()
        && backrest_Material.WhereNot("%mesh%", "%plastic%", "%metal%", "%steel%").Any()){
        result = $"{backrest_Color.FirstValue().ToUpperFirstChar()} {backrest_Material.WhereNot("%mesh%", "%plastic%", "%metal%", "%steel%").First().Value().ToLower()} upholstery is easy to maintain";
    }
    else if(miscellaneous_ProductMaterial.HasValue()
        && miscellaneous_ProductMaterial.WhereNot("%mesh%", "%plastic%", "%metal%", "%steel%").Any()
        && Coalesce(trueColorRef.ToLower()).NotIn("silver", "chrome")){
        result = $"{trueColorRef.ToUpperFirstChar()} {miscellaneous_ProductMaterial.WhereNot("%mesh%", "%plastic%", "%metal%", "%steel%").First().Value().ToLower()} upholstery is easy to maintain";
    }
    // -- Available in black and is made using plastic, wood and steel
    else if(seat_Color.HasValue()
        && seat_Material.HasValue()){
        result = $"Available in {seat_Color.FirstValue().ToLower()} and is made using {seat_Material.FirstValue().ToLower()}";
    }
    else if(backrest_Color.HasValue()
        && backrest_Material.HasValue()){
        result = $"Available in {backrest_Color.FirstValue().ToLower()} and is made using {backrest_Material.FirstValue().ToLower()}";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"OfficeChairsTrueColorUpholsteryMaterial⸮{result}"); 
    }
}

// --[FEATURE #3]
// --Type of support and Ergonomic Construction/Design
void Type_Of_Support_And_Ergonomic(){
    var result = ""; 
    var LumbarSupportRef = R("SP-1096").HasValue() ? R("SP-1096").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-1096").HasValue() ? R("cnet_common_SP-1096").Replace("<NULL>", "").Text : "";
    // Lumbar Support|Headrest|Footrest
    var TypeOfSupportRef = R("SP-24164").HasValue() ? R("SP-24164").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-24164").HasValue() ? R("cnet_common_SP-24164").Replace("<NULL>", "").Text : "";
    var Backrest_LumbarSupport = Coalesce(A[7199]); // Backrest - Lumbar Support
    var Seat_AdjustableHeight = Coalesce(A[7188]); // Seat - Adjustable Height
    var General_TiltTensionAdjustment = Coalesce(A[7168]); // General - Tilt Tension Adjustment
    var General_TiltLock = Coalesce(A[7167]); // General - Tilt Lock
    var General_Features = Coalesce(A[7175]); // General - Features
    var General_Swivel = Coalesce(A[7165]); // General - Swivel
    var Armrests_Features = Coalesce(A[7223]); // Armrests - Features
    var Base_CastersQty = Coalesce(A[7210]); // Base - Casters Qty
    var Base_WheelType = Coalesce(A[7212]); // Base - Wheel Type
    var General_Mechanism = Coalesce(A[7169]); // General - Mechanism
    var Seat_Features = Coalesce(A[7176]); // Seat - Features
    var DimensionsWeight_BackrestHeight = Coalesce(A[7237]); // Dimensions & Weight - Backrest Height
    var Backrest_MaxHeight = Coalesce(A[7193]); // Backrest - Max Height

    // -- Lumbar support, seat height adjustment with tilt tension and tilt lock offer customized comfort
    if(!String.IsNullOrEmpty(LumbarSupportRef)
    && TypeOfSupportRef.ToLower().Contains("headrest")){
        result = "This chair provides lumbar and head support";
    }
    else if(!String.IsNullOrEmpty(LumbarSupportRef)
        && TypeOfSupportRef.ToLower().Contains("footrest")){
        result = "This chair provides lumbar and foot support";
    }
    else if(!String.IsNullOrEmpty(LumbarSupportRef)
        && TypeOfSupportRef.ToLower().Contains("lumbar")){
        result = "This chair provides lumbar support";
    }
    else if(TypeOfSupportRef.ToLower().Equals("lumbar support")){
        result = "Built-in lumbar support encourages proper posture";
    }
    else if(TypeOfSupportRef.ToLower().Equals("footrest")){
        result = "Built in footrest";
    }
    else if(TypeOfSupportRef.ToLower().Equals("headrest")){
        result = "Built-in headrest provides additional upper body support";
    }
    else if(Backrest_LumbarSupport.HasValue("Yes")
        && Seat_AdjustableHeight.HasValue("Yes")
        && General_TiltTensionAdjustment.HasValue("Yes")
        && General_TiltLock.HasValue("Yes")){
        result = "Lumbar support, seat height adjustment with tilt tension and tilt lock offer customized comfort";
    }
    // -- Gas-lift seat height adjustment and 360-degree swivel for customized ergonomic comfort
    else if(General_Features.HasValue("gas-lift height adjustment")
        && General_Swivel.HasValue("Yes")){
        result = "Gas-lift seat height adjustment and 360-degree swivel for customized ergonomic comfort";
    }
    // -- Pneumatic seat height adjustment and sculpted arms provide ergonomic support
    else if(General_Features.HasValue("pneumatic seat height adjustment")
        && Armrests_Features.HasValue("sculpted")){
        result = "Pneumatic seat height adjustment and sculpted arms provide ergonomic support";
    }
    // -- Pneumatic gas-lift height adjustment allows you to customize the chair to your specific needs
    else if(General_Features.HasValue("pneumatic seat height adjustment")){
        result = "Pneumatic gas-lift height adjustment allows you to customize the chair to your specific needs";
    }
    // -- Five-star base with carpet casters for stability
    else if(Base_CastersQty.HasValue("5")
        && Base_WheelType.HasValue("carpet")){
        result = "Five-star base with carpet casters for stability";
    }
    // -- Butterfly mechanism for easy adjustment of height and tilt
    else if(General_Mechanism.HasValue("butterfly")){
        result = "Butterfly mechanism for easy adjustment of height and tilt";
    }
    // -- Waterfall seat edge promotes better leg circulation
    else if(Seat_Features.HasValue("waterfall edge")){
        result = "Waterfall seat helps reduce leg strain";
    }
    // -- High-back design offers adequate support to neck and head
    else if(DimensionsWeight_BackrestHeight.HasValue() 
        || Backrest_MaxHeight.HasValue()){
        if((DimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
        && DimensionsWeight_BackrestHeight.Values.First().ValueUSM >= 22)
        || (Backrest_MaxHeight.Units.First().NameUSM.In("in")
        && Backrest_MaxHeight.Values.First().ValueUSM >= 22)){
            result = "High-back design offers adequate support to neck and head";
        }
    }
    // -- Mid-back design minimizes stress and strain
    else if(DimensionsWeight_BackrestHeight.HasValue() 
        || Backrest_MaxHeight.HasValue()){
        if((DimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
        && DimensionsWeight_BackrestHeight.Values.First().ValueUSM >= 15
        && DimensionsWeight_BackrestHeight.Values.First().ValueUSM <= 21)
        || (Backrest_MaxHeight.Units.First().NameUSM.In("in")
        && Backrest_MaxHeight.Values.First().ValueUSM >= 15
        && Backrest_MaxHeight.Values.First().ValueUSM <= 21)){
            result = "Mid-back design minimizes stress and strain";
        }
    }
    // -- Low-back design relieves lower back strain
    else if(DimensionsWeight_BackrestHeight.HasValue() 
        || Backrest_MaxHeight.HasValue()){
        if((DimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
        && DimensionsWeight_BackrestHeight.Values.First().ValueUSM < 15)
        || (Backrest_MaxHeight.Units.First().NameUSM.In("in")
        && Backrest_MaxHeight.Values.First().ValueUSM < 15)){
            result = "Low-back design may help relieve lower back strain";
        }
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"TypeOfSupportAndErgonomic⸮{result}"); 
    }
}

// --[FEATURE #4]
// --Overall Dimensions (in Inches): Height (should include range) x Width x Depth
void Office_Chairs_Overall_Dimensions(){
    var result = ""; 
    var AdjustableHeightMinimumRef = R("SP-21870").HasValue() ? R("SP-21870").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21870").HasValue() ? R("cnet_common_SP-21870").Replace("<NULL>", "").Text : "";
    var HeightRef = R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : "";
    var DepthRef = R("SP-20657").HasValue() ? R("SP-20657").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20657").HasValue() ? R("cnet_common_SP-20657").Replace("<NULL>", "").Text : "";
    var WidthRef = R("SP-21044").HasValue() ? R("SP-21044").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21044").HasValue() ? R("cnet_common_SP-21044").Replace("<NULL>", "").Text : "";
    var DimensionsWeight_MinWidth = Coalesce(A[7232]); // Dimensions & Weight - Min Width

    if(!String.IsNullOrEmpty(AdjustableHeightMinimumRef)
    && !String.IsNullOrEmpty(HeightRef)
    && !String.IsNullOrEmpty(WidthRef)
    && !String.IsNullOrEmpty(DepthRef)){
        result = $@"Overall dimensions: {AdjustableHeightMinimumRef}-{HeightRef}""H x {WidthRef}""W x {DepthRef}""D";
    }
    else if(!String.IsNullOrEmpty(HeightRef)
        && !String.IsNullOrEmpty(WidthRef)
        && !String.IsNullOrEmpty(DepthRef)){
        result = $@"Overall dimensions: {HeightRef}""H x {WidthRef}""W x {DepthRef}""D";
    }
    else if(DimensionsWeight_MinWidth.HasValue()){
        if(!String.IsNullOrEmpty(HeightRef)
        && !String.IsNullOrEmpty(DepthRef)
        && DimensionsWeight_MinWidth.Units.First().NameUSM.In("in")){
            result = $@"Overall dimensions: {HeightRef}""H x {DimensionsWeight_MinWidth.Values.First().ValueUSM}""W x {DepthRef}""D";
        }
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"OfficeChairsOverallDimensions⸮{result}"); 
    }
}

// --[FEATURE #5]
// --Seat Dimension (in Inches): Height (should include range) x Width x Depth
void Seat_Dimension(){
    var result = ""; 
    var seatWidthRef = R("SP-21885").HasValue() ? R("SP-21885").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21885").HasValue() ? R("cnet_common_SP-21885").Replace("<NULL>", "").Text : "";
    var seatDepthRef = R("SP-21886").HasValue() ? R("SP-21886").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21886").HasValue() ? R("cnet_common_SP-21886").Replace("<NULL>", "").Text : "";

    if(!String.IsNullOrEmpty(seatWidthRef)
    && !String.IsNullOrEmpty(seatDepthRef)){
        result = $@"Seat dimensions: {seatWidthRef}""W x {seatDepthRef}""D";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"SeatDimension⸮{result}"); 
    }
}

// --[FEATURE #6]
// --Back Dimensions (in Inches): Width x Depth
void Back_Dimension(){
    var result = ""; 
    var backWidthRef = R("SP-21937").HasValue() ? R("SP-21937").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21937").HasValue() ? R("cnet_common_SP-21937").Replace("<NULL>", "").Text : "";
    var backDepthRef = R("SP-21938").HasValue() ? R("SP-21938").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21938").HasValue() ? R("cnet_common_SP-21938").Replace("<NULL>", "").Text : "";

    if(!String.IsNullOrEmpty(backWidthRef)
    && !String.IsNullOrEmpty(backDepthRef)){
        result = $@"Back dimensions: {backWidthRef}""W x {backDepthRef}""D";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"BackDimension⸮{result}"); 
    }
}

// --[FEATURE #7]
// --Arm Type (include padding, measurements and adjustability)
void Arm_Type(){
    var result = ""; 
    var General_Armrests = Coalesce(A[7161]); // General - Armrests
    var Armrests_AdjustableArmrests = Coalesce(A[7214]); // Armrests - Adjustable Armrests
    var Armrests_Features = Coalesce(A[7223]); // Armrests - Features
    var Armrests_Shape = Coalesce(A[7625]); // Armrests - Shape
    var General_PiecesIncluded = Coalesce(A[10460]); // General - Pieces Included

    // -- Armless design allows for easy movement and versatility
    if(General_PiecesIncluded.HasValue("armless chair")
    || General_Armrests.HasValue("No")){
        result = "Armless design allows for easy movement and versatility";
    }
    // -- Padded adjustable arms allow you to set the height, width and angle for custom ergonomics
    else if(Armrests_Features.HasValue("padded")
        && Armrests_AdjustableArmrests.HasValue()
        && Armrests_AdjustableArmrests.Where("height", "width", "tilt").Any()){
        result = "Width and height adjustable arms allow you to rest your forearms comfortably";
    }
    // -- Width and height adjustable arms allow you to rest your forearms comfortably
    else if(Armrests_AdjustableArmrests.HasValue()
        && Armrests_AdjustableArmrests.Where("height", "width").Any()){
        result = "Padded adjustable arms allow you to set the height, width and angle for custom ergonomics";
    }
    // -- Height-adjustable arms to support the shoulders and upper body
    else if(Armrests_AdjustableArmrests.HasValue()
        && Armrests_AdjustableArmrests.Where("height").Any()){
        result = "Height-adjustable arms to support the shoulders and upper body";
    }
    // -- Adjustable arms for supporting your forearms
    else if(!Armrests_Features.HasValue("No")){
        result = "Adjustable arms for supporting your forearms";
    }
    else if(Armrests_Shape.HasValue()
        && Armrests_AdjustableArmrests.HasValue("No")){
        result = $"Fixed {Armrests_Shape.FirstValue()} arms provide added comfort";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"ArmType⸮{result}"); 
    }
}

// --[FEATURE #8]
// --Tilt Mechanism type
void Tilt_Mechanism_Type(){
    var result = ""; 
    // Back-Tilt|Synchro-Tilt|Spring-Tilt|Seat-Tilt|Pivot-Tilt|Knee-Tilt|Forward-Tilt|Center-Tilt/Knee-Tilt/Synchro-Tilt|Center-Tilt|Synchro-Tilt/ Pivot-Tilt|Swivel-Tilt|Multi-Tilter|Locking-Tilt|Knee-Tilt/Synchro-Tilt
    var ChairTiltTypeRef = R("SP-22280").HasValue() ? R("SP-22280").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22280").HasValue() ? R("cnet_common_SP-22280").Replace("<NULL>", "").Text : "";
    var General_TiltLock = Coalesce(A[7167]); // General - Tilt Lock
    var General_TiltTensionAdjustment = Coalesce(A[7168]); // General - Tilt Tension Adjustment
    var General_Mechanism = Coalesce(A[7169]); // General - Mechanism

    // -- Synchro-tilt mechanism for a smooth recline, with tilt tension and tilt lock for secure support
    if(General_TiltLock.HasValue("Yes")
    && General_TiltTensionAdjustment.HasValue("Yes")
    && General_Mechanism.HasValue("synchro")){
        result = "Synchro-tilt mechanism for a smooth recline with tilt tension and tilt lock for secure support";
    }
    // -- Center-tilt mechanism rotates the seat from a point at the center to comfortably recline
    else if(ChairTiltTypeRef.Equals("Center-Tilt")){
        result = "Center-tilt mechanism rotates the seat from a point at the center to comfortably recline";
    }
    // -- High-performance asynchronous tilt lets you fine-tune your posture for any task
    else if(General_Mechanism.HasValue("synchro")){
        result = "High-performance asynchronous tilt lets you fine-tune your posture for any task";
    }
    // -- Tilt with adjustable tension control for comfort
    else if(General_TiltTensionAdjustment.HasValue("Yes")
        && !String.IsNullOrEmpty(ChairTiltTypeRef)){
        result = $"{ChairTiltTypeRef} with adjustable tension control for comfort";
    }
    else if(!String.IsNullOrEmpty(ChairTiltTypeRef)){
        result = $"{ChairTiltTypeRef} for comfort";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"TiltMechanismType⸮{result}"); 
    }
}

// --[FEATURE #9]
// --Capacity in lbs. as well as time (if available) detailed as: "Rated for up to 250 lbs. based on an 8 hour work day" or "Rated for up to 350 lbs. based on continuous use" 
void Office_Chairs_Capacity(){
    var result = ""; 
    var MaximumWeightCapacityRef = R("SP-17524").HasValue() ? R("SP-17524").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-17524").HasValue() ? R("cnet_common_SP-17524").Replace("<NULL>", "").Text : "";
    var general_HoursADay = Coalesce(A[7170]); // General - Hours a Day

    switch(MaximumWeightCapacityRef){
        // -- The strong chair can safely support the weight of a user who weighs up to 250 pounds for a full work day
        case var CapacityRef when Coalesce(CapacityRef).HasValue()
        && Coalesce(MaximumWeightCapacityRef).ExtractNumbers().First() >= 250
        && general_HoursADay.HasValue() && general_HoursADay.FirstValue() > 8:
        result = $"This chair can safely support a user who weighs up to {CapacityRef} lbs. for a full work day";
        break;
        // -- Weight rated up to 250 pounds for up to 5 hours of use per day
        case var CapacityRef when !String.IsNullOrEmpty(CapacityRef)
        && general_HoursADay.HasValue():
        result = $"Weight is rated up to {CapacityRef} lbs. for up to {general_HoursADay.FirstValue()} hours of use per day";
        break;
        case var CapacityRef when !String.IsNullOrEmpty(CapacityRef):
        result = $"Weight is rated up to {CapacityRef} lbs.";
        break;
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"OfficeChairsCapacity⸮{result}"); 
    }
}

// --[FEATURE #10]
// --Assembly required
void Office_Chairs_Assembly_Required(){
    var result = ""; 
    var General_AssemblyRequired = Coalesce(A[7173]); // General - Assembly Required
    var General_Features = Coalesce(A[7175]); // General - Features

    if(General_Features.HasValue("partial assembly required")){
        result = "Partial Assembly Required";
    }
    else if(General_AssemblyRequired.HasValue()){
        result = General_AssemblyRequired.HasValue("Yes") ? "Assembly Required" :
        General_AssemblyRequired.HasValue("No") ? "Comes fully assembled for immediate use" : "";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"OfficeChairsAssemblyRequired⸮{result}"); 
    }
}

// --[FEATURE #11]
// --Certifications & Standards (ANSI/BIFMA)
void Certifications_Standards_ANSI_BIFMA(){
    var result = ""; 
    var Miscellaneous_CompliantStandards = Coalesce(A[380]); // Miscellaneous - Compliant Standards

    if(Miscellaneous_CompliantStandards.HasValue("%ANSI%")
    && Miscellaneous_CompliantStandards.HasValue("%BIFMA%")){
        result = "Meets or exceeds ANSI/BIFMA standards";
    }
    else{
        result = Miscellaneous_CompliantStandards.HasValue("%ANSI%") ? "Meets or exceeds ANSI standard" :
        Miscellaneous_CompliantStandards.HasValue("%BIFMA%") ? "Meets or exceeds BIFMA standard" : "";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"CertificationsStandardsANSIBIFMA⸮{result}"); 
    }
}

// --[FEATURE #12] - All
// --Warranty

//§§2324927166253 "Office Chairs END" "Serhii.O" 

//§§590434034623141470 "Monitor Mounts & Stands BEGIN" "Serhii.O" 

Monitor_Mount_Stand_Type_Use();
Monitor_Size_Supported();
Adjustable_Minimum_Maximum_Height();
Monitor_Mount_Stand_Articulation();
Monitor_Mount_Stand_Rotation();
Monitor_Mount_Stand_Storage_Options();
// Weight supported (lbs.) - All
// Dimensions - All
Mount_Bracket_Or_VESA_Pattern();
// Certifications & Standards - All
// Color - All
Rubber_Feet();
// Warranty - All

// --[FEATURE #1]
// --Monitor mount/stand type & use
void Monitor_Mount_Stand_Type_Use(){
    var result = "";
    // Brackets|Racks|Adapters|Mounts|Extenders|Cart|Track|Mounting Kit|Stands|Arms|Risers
    var MonitorMountStandTypeRef = R("SP-18605").HasValue() ? R("SP-18605").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18605").HasValue() ? R("cnet_common_SP-18605").Replace("<NULL>", "").Text : "";

    // -- Raise your monitor to a comfortable viewing height (https://www.staples.com/Mind-Reader-Harmonize-Adjustable-Plastic-Monitor-Stand-with-Drawer-Black/product_2599043)
    if(!String.IsNullOrEmpty(MonitorMountStandTypeRef)){
        result = $"Raise your monitor to a comfortable viewing height with this monitor {MonitorMountStandTypeRef}";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"MonitorMountStandTypeUse⸮{result}");
    }
}

// --[FEATURE #2]
// --Monitor size supported
void Monitor_Size_Supported(){
    var result = "";
    // Up to 22" Monitor|Up to 65" Monitor|Up to 56" Monitor|Up to 47" Monitor|Up to 23" Monitor|Up to 34" Monitor|Up to 55" Monitor|Up to 42" Monitor|Up to 35" Monitor|Up to 20" Monitor|Up to 71" Monitor|Up to 63" Monitor|Up to 75" Monitor|Up to 30" Monitor|Up to 32" Monitor|Up to 17" Monitor|Up to 25" Monitor|Up to 21" Monitor|Up to 40" Monitor|Up to 28" Monitor|Up to 82" Monitor|Up to 46" Monitor|Up to 37" Monitor|Up to 70" Monitor|Up to 24" Monitor|Up to 26" Monitor|Up to 90" Monitor|Up to 80" Monitor|Up to 27" Monitor|Up to 82" Monitor
    var MonitorSizeSupportedRef = R("SP-22184").HasValue() ? R("SP-22184").Replace("<NULL>", "").Text :
        R("cnet_common_SP-22184").HasValue() ? R("cnet_common_SP-22184").Replace("<NULL>", "").Text : "";

    if(!String.IsNullOrEmpty(MonitorSizeSupportedRef)){
        result = $"Supports {MonitorSizeSupportedRef}s for versatile use";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"MonitorSizeSupported⸮{result}");
    }
}

// --[FEATURE #3]
// --Adjustable minimum & maximum height (Inches)
void Adjustable_Minimum_Maximum_Height(){
    var result = "";
    var AdjustableMaximumHeightRef = R("SP-22194").HasValue() ? R("SP-22194").Replace("<NULL>", "").Text :
        R("cnet_common_SP-22194").HasValue() ? R("cnet_common_SP-22194").Replace("<NULL>", "").Text : "";
    var AdjustableMinimumHeightRef = R("SP-22195").HasValue() ? R("SP-22195").Replace("<NULL>", "").Text :
        R("cnet_common_SP-22195").HasValue() ? R("cnet_common_SP-22195").Replace("<NULL>", "").Text : "";

    if(!String.IsNullOrEmpty(AdjustableMinimumHeightRef)
    && !String.IsNullOrEmpty(AdjustableMaximumHeightRef)){
        result = $@"Height adjusts from {AdjustableMinimumHeightRef}"" to {AdjustableMaximumHeightRef}"" for comfortable viewing";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AdjustableMinimumMaximumHeight⸮{result}");
    }
}

// --[FEATURE #4] - All
// --Mount & stand material

// --[FEATURE #5]
// --Articulation; pivot angle (Degrees), tilt angle (Degrees) & viewing Angles (If applicable)
void Monitor_Mount_Stand_Articulation(){
    var result = ""; 
    var AVFurniture_Tilt = Coalesce(A[8508]); // AV Furniture - Tilt
    var AVFurniture_Pan = Coalesce(A[8509]); // AV Furniture - Pan
    var AVFurniture_Rotation = Coalesce(A[8511]); // AV Furniture - Rotation
    var AVFurniture_Adjustments = Coalesce(A[8621]); // AV Furniture - Adjustments
    var Miscellaneous_Features = Coalesce(A[374]); // Miscellaneous - Features

    if(AVFurniture_Tilt.HasValue()
    && AVFurniture_Pan.HasValue()
    && AVFurniture_Rotation.HasValue()){
        result = $"{AVFurniture_Tilt.FirstValue().Replace("°", "")}-degree tilt, {AVFurniture_Tilt.FirstValue().Replace("°", "")}-degree pan and {AVFurniture_Tilt.FirstValue().Replace("°", "")}-degree rotation adjustment options enable optimal viewing";
    }
    else if(AVFurniture_Tilt.HasValue()
        && AVFurniture_Pan.HasValue()){
        result = $"{AVFurniture_Tilt.FirstValue().Replace("°", "")}-degree tilt and {AVFurniture_Tilt.FirstValue().Replace("°", "")}-degree pan adjustment options enable optimal viewing";
    }
    else if((AVFurniture_Tilt.HasValue()
        || AVFurniture_Pan.HasValue())
        && AVFurniture_Rotation.HasValue()){
        var TiltOrPan = AVFurniture_Tilt.HasValue() ? $"{AVFurniture_Tilt.FirstValue().Replace("°", "")}-degree tilt" :
        AVFurniture_Pan.HasValue() ? $"{AVFurniture_Tilt.FirstValue().Replace("°", "")}-degree pan" : "";

        result = $"{TiltOrPan} and {AVFurniture_Rotation.FirstValue().Replace("°", "")}-degree rotation adjustment options enable optimal viewing";
    }
    // S21545725 - Tilt functions enable adjustments for comfortable viewing
    else if(AVFurniture_Adjustments.HasValue()
        && AVFurniture_Adjustments.HasValue("pan", "tilt", "rotation", "swivel")){
        result = $"{AVFurniture_Adjustments.Where("pan", "tilt", "rotation", "swivel").Select(s => s.Value().ToLower(true).ToUpperFirstChar()).FlattenWithAnd()} functions enable adjustments for comfortable viewing";
    }
    else if(AVFurniture_Adjustments.HasValue()
        && AVFurniture_Adjustments.HasValue("%tilt%","%swivel%","%pan rotation%", "%pivot%","%rotation%")){
        result = $"{AVFurniture_Adjustments.Where("%tilt%","%swivel%","%pan rotation%", "%pivot%","%rotation%").Select(s => s.Value().NotIn("%cable%") && s.Value().ToLower(true).ToUpperFirstChar()).FlattenWithAnd()} functions enable adjustments for comfortable viewing";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"MonitorMountStandArticulation⸮{result}");
    }
}

// --[FEATURE #6]
// --Rotation
void Monitor_Mount_Stand_Rotation(){
    var result = ""; 
    var AVFurniture_Tilt = Coalesce(A[8508]); // AV Furniture - Tilt
    var AVFurniture_Pan = Coalesce(A[8509]); // AV Furniture - Pan
    var AVFurniture_Rotation = Coalesce(A[8511]); // AV Furniture - Rotation
    var AVFurniture_Features = Coalesce(A[889]); // AV Furniture - Features

    if((!AVFurniture_Tilt.HasValue()
    || !AVFurniture_Pan.HasValue())
    && AVFurniture_Rotation.HasValue()
    && AVFurniture_Rotation.FirstValue().ExtractNumbers().Any() && AVFurniture_Rotation.FirstValue().ExtractNumbers().Max() > 89 ){
        result = $"{AVFurniture_Rotation.FirstValue().Replace("°", "")}-degree rotation allows monitors to be oriented vertically or horizontally";
    }
    else if(AVFurniture_Features.HasValue("%portrait to landscape%")){
        result = "Supports landscape to portrait display rotation for viewing longer pages without scrolling";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"MonitorMountStandRotation⸮{result}");
    }
}

// --[FEATURE #7]
// --Storage options; number of drawers, shelf count (If applicable)
void Monitor_Mount_Stand_Storage_Options(){
    var result = ""; 
    var Miscellaneous_Details = Coalesce(A[9546]); // Miscellaneous - Details
    // 6|5|8|7|10+|9|0|2|1|4|3
    var NumberOfDrawersRef = R("SP-18205").HasValue() ? R("SP-18205").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18205").HasValue() ? R("cnet_common_SP-18205").Replace("<NULL>", "").Text : "";
    var ShelfCountRef = R("SP-4256").HasValue() ? R("SP-4256").Replace("<NULL>", "").Text :
        R("cnet_common_SP-4256").HasValue() ? R("cnet_common_SP-4256").Replace("<NULL>", "").Text : "";

    if(Miscellaneous_Details.HasValue("integrated drawer")){
        result = "Integrated drawer organizes essentials such as pens, clips, etc.";
    }
    else if(NumberOfDrawersRef.Equals("1")){
        result = "Storage drawer organizes essentials such as pens, clips, etc.";
    }
    else if(!String.IsNullOrEmpty(NumberOfDrawersRef)){
        result = $"{NumberOfDrawersRef} drawers organize essentials such as pens, clips, etc.";
    }
    else if(ShelfCountRef.Equals("1")){
        result = "Storage shelf is ideal for managing important papers or files";
    }
    else if(!String.IsNullOrEmpty(ShelfCountRef)){
        result = $"{ShelfCountRef} shelves are ideal for managing important papers or files";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"MonitorMountStandStorageOptions⸮{result}");
    }
}

// --[FEATURE #8] - All
// --Weight supported (lbs.)

// --[FEATURE #9] - All
// --Dimensions (in Inches): Height x Width x Depth; if a range, note here

// --[FEATURE #10]
// --Mount bracket or VESA pattern (If applicable)
void Mount_Bracket_Or_VESA_Pattern(){
    var result = ""; 
    // 51|Yes|50 x 50, 75 x 75, 100 x 100|400 x 400, 447 x 405|100 x 100 mm|75 x 75, 100 x 100, 200 x 100, 200 x 200|100 x 100, 75 x 75|822 x 405, 800 x 400|200 x 200, 760 x 505|75 x 75, 100 x 100, 200 x 200|75, 100|100 x 100, 200 x 100|100 x 100, 400 x 400|75 x 75, 100 x 100, 100 x 200|200 x 100, 600 x 400|200 x 200, 862 x 517|200 x 200, 600 x 400|100 x 100, 723 x 400|100, 200 x 100, 200, 400 x 200, 400, 800
    var MountBracketorVESAPatternRef = R("SP-22196").HasValue() ? R("SP-22196").Replace("<NULL>", "").Text :
        R("cnet_common_SP-22196").HasValue() ? R("cnet_common_SP-22196").Replace("<NULL>", "").Text : "";
    var FlatPanelMountInterface = Coalesce(A[2368]); // Flat Panel Mount Interface

    if(!String.IsNullOrEmpty(MountBracketorVESAPatternRef)
    && !MountBracketorVESAPatternRef.Equals("Yes")){
        result = $"Compatible with: standard VESA {MountBracketorVESAPatternRef} mounting patterns";
    }
    else if(!FlatPanelMountInterface.HasValue("VESA%", "Yes")){
        result = $"Compatible with: standard VESA {FlatPanelMountInterface.WhereNot("VESA%", "Yes").Select(s => s.Value().Replace(" mm", "mm")).FlattenWithAnd()} mounting patterns";
    }
    else if(FlatPanelMountInterface.HasValue("VESA%")
        || !String.IsNullOrEmpty(MountBracketorVESAPatternRef)){
        result = "Meets or exceed VESA standard";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"MountBracketOrVESAPattern⸮{result}");
    }
}

// --[FEATURE #11] - All
// --VESA mounting interface Certifications & Standards

// --[FEATURE #12] - All
// --Use for additional product and/or manufacturer information relevant to the customer buying decision (Color)

// --[FEATURE #13]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Rubber_Feet(){
    var result = ""; 
    var Miscellaneous_PackageContent = Coalesce(A[9501]); // Miscellaneous - Package Content
    var Miscellaneous_Features = Coalesce(A[374]); // Miscellaneous - Features

    if(Miscellaneous_PackageContent.HasValue()
    || Miscellaneous_Features.HasValue()){
        result = "Rubber feet protect desk surface";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"RubberFeet⸮{result}");
    }
}

// --[FEATURE #14] - All
// --Warranty information

//§§590434034623141470 "Monitor Mounts & Stands END" "Serhii.O" 

//§§135391203158855 "Filing Accessories BEGIN" "Serhii.O" 

Filing_Accessory_Type();
// True color & Material of Item - All
// Size - All
Paper_Size_Filing_Accessory();
// Filing Accesory Pack Size - All
Folder_Expands();
Type_Of_Filing_Accessory();
Adhesive_Filing_Accessory();
Heavy_Duty_Filing_Accessory();
Laminated_Surface_Filing_Accessory();
Cut_Tab_Filing_Accessory();
Tear_Resistant_Filing_Accessory();
Heavy_Weight_Filing_Accessory();
Blank_Inserts_Filing_Accessory();
Printing_Technology_Filing_Accessory();
Self_Adhesiveness_Filing_Accessory();
Alphabetical_Order_Filing_Accessory();

// --"[FEATURE #1]" 
// --"Filing Accessory Type" 
void Filing_Accessory_Type(){
    var result = "";
    // Labels|Hanging File Frames|Tabs|Fasteners
    var FilingAccessoryTypeRef = R("SP-18605").HasValue() ? R("SP-18605").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18605").HasValue() ? R("cnet_common_SP-18605").Replace("<NULL>", "").Text : "";
    var FilingSystem_ProductType = Coalesce(A[5922]); // Filing System - Product Type

    if(!String.IsNullOrEmpty(FilingAccessoryTypeRef)){
        switch(FilingAccessoryTypeRef){
            case var FilingAccessory when FilingAccessory.Equals("Labels")
            && FilingSystem_ProductType.HasValue("self-adhesive label", "self-adhesive color-coded label"):
            result = $"{FilingSystem_ProductType.FirstValue().ToUpperFirstChar()}s make locating your files an easy task";
            break;
            case var FilingAccessory when FilingAccessory.Equals("Labels"):
            result = "Labels help you easily identify and remove inactive records, making room for new chart space";
            break;
            case var FilingAccessory when FilingAccessory.Equals("Tabs"):
            result = "Tabs make organizing files quick and easy";
            break;
            case var FilingAccessory when FilingAccessory.Equals("Folder Frames"):
            result = "Make the most of your file drawers and keep your documents organized with this folder frame";
            break;
            case var FilingAccessory when FilingAccessory.Equals("Index Guides"):
            result = "Break folders into smaller groups for quick identification with index guides";
            break;
            case var FilingAccessory when FilingAccessory.Equals("Label Protectors"):
            result = "Label protector for protecting labels against smudging and water";
            break;
            // --3 Interior dividers are ideal for sectioning large files or projects 
            case var FilingAccessory when FilingAccessory.Equals("Filing Dividers"):
            result = $"{FilingAccessory} are ideal for sectioning large files or projects";
            break;
            // https://www.staples.com/OIC-Hanging-Folder-Frame-Letter-Size-Twin-Pack-Adjustable-24-to-27/product_177311?akamai-feo=off
            // --Make the most of your file drawers and keep your documents organized with this folder frame
            case var FilingAccessory when FilingAccessory.Equals("Hanging File Frames"):
            result = "Keep your documents organized with this helding files frame";
            break;
            // --Fasteners are made to secure top-punched documents inside a file folder.
            case var FilingAccessory when FilingAccessory.Equals("Fasteners"):
            result = "Fasteners are made to secure documents inside a file folder";
            break;
            default:
            result = $"Keep your documents organized with these {Coalesce(FilingAccessoryTypeRef.ToLower()).Pluralize()}";
            break;
        }
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"FilingAccessoryType⸮{result}");
    }
}

// --[FEATURE #2] - All
// --True color & Material of Item

// --[FEATURE #3] - ALL
// --Size (File size, tab count/cut, etc.)

// --[FEATURE #4]
// --Paper Size (If not mentioned within Bullet 3)
void Paper_Size_Filing_Accessory(){
    var result = "";
    // Labels|Hanging File Frames|Tabs|Fasteners
    var FilingAccessoryTypeRef = R("SP-18605").HasValue() ? R("SP-18605").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18605").HasValue() ? R("cnet_common_SP-18605").Replace("<NULL>", "").Text : "";
    var PaperSizeRef = R("SP-12517").HasValue() ? R("SP-12517").Replace("<NULL>", "").Text :
        R("cnet_common_SP-12517").HasValue() ? R("cnet_common_SP-12517").Replace("<NULL>", "").Text : "";
    var FilingSystem_SupportedFormat = Coalesce(A[5925]); // Filing System - Supported Format

    if(FilingAccessoryTypeRef.Equals("Folder Frames")
    && !String.IsNullOrEmpty(PaperSizeRef)
    && !PaperSizeRef.Equals("Other")){
        result = $"This frame works with {PaperSizeRef}-size paper and smaller documents, making it a versatile storage solution";
    }
    else if(FilingSystem_SupportedFormat.HasValue()){
        result = $"For use with {FilingSystem_SupportedFormat.FirstValueUsm().Replace(@" in", @"""")} sheets";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"PaperSizeFilingAccessory⸮{result}");
    }
}

// --[FEATURE #5] - All
// --Filing Accesory Pack Size

// --[FEATURE #6]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Folder_Expands(){
    var result = "";
    // 2"|1 1/4"|3"|1/2"|4"|5"|1 1/2"|1"|3 1/2"|5 1/4"|3/4"|7" 
    var FolderExpansionRef = R("SP-18595").HasValue() ? R("SP-18595").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18595").HasValue() ? R("cnet_common_SP-18595").Replace("<NULL>", "").Text : "";

    if(!String.IsNullOrEmpty(FolderExpansionRef)){
        result = $"File folder expands up to {FolderExpansionRef} for maximum storage";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"FolderExpands⸮{result}");
    }
}

// --[FEATURE #7]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Type_Of_Filing_Accessory(){
    var result = "";
    // Card Pocket|Folder Tabs and Labels|Fasteners|Retention Jacket|Folder Insertable Tabs|Printable Tabs|File Backs|Untabbed File Dividers|Outguides|Folder Frames|Sheet Lifters|Folder Tabs|Folder Labels|File Case Wrap|File Dividers|File Dividers with Preprinted Tabs - Monthly|File Dividers with Preprinted Tabs - A-Z|File Dividers with Preprinted Tabs - Numeric|File Dividers with Write-On Tabs
    var TypeOfFilingAccessoryRef = R("SP-24087").HasValue() ? R("SP-24087").Replace("<NULL>", "").Text :
        R("cnet_common_SP-24087").HasValue() ? R("cnet_common_SP-24087").Replace("<NULL>", "").Text : "";
    var FilingSystem_ProductType = Coalesce(A[5922]); // Filing System - Product Type
    var FilingSystem_ManufacturerProductType = Coalesce(A[5921]); // Filing System - Manufacturers Product Type

    if(!String.IsNullOrEmpty(TypeOfFilingAccessoryRef)){
        switch(TypeOfFilingAccessoryRef){
            case var a when a.Equals("File Dividers with Preprinted Tabs - Numeric")
            && FilingSystem_ProductType.HasValue("%color-coded%") || FilingSystem_ManufacturerProductType.HasValue("%color-coded%"):
            result = "Break your shelf filing system into easily spotted smaller groups with these color-coded numeric labels";
            break;
            case var a when a.Equals("File Dividers with Preprinted Tabs - A-Z")
            && FilingSystem_ProductType.HasValue("%color-coded%") || FilingSystem_ManufacturerProductType.HasValue("%color-coded%"):
            result = "Use alphabetic color-coded labels to put an end to misfiled records and speed file retrieval time";
            break;
            case var a when a.Equals("File Dividers with Preprinted Tabs - Monthly")
            && FilingSystem_ProductType.HasValue("%color-coded%") || FilingSystem_ManufacturerProductType.HasValue("%color-coded%"):
            result = "Month label has color coding that makes it easy to locate all folders with the same date";
            break;
            default:
            if(FilingSystem_ManufacturerProductType.HasValue("%color-coded%")){
                result = "Assign separate color-code to different categories within your file system";
            }
            break;
        }
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"TypeOfFilingAccessory⸮{result}");
    }
}

// --[FEATURE #8]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Adhesive_Filing_Accessory(){
    var result = "";
    var FilingSystem_ProductType = Coalesce(A[5922]); // Filing System - Product Type
    var FilingSystem_Features = Coalesce(A[5945]); // Filing System - Features
    var FilingSystem_Printable = Coalesce(A[6543]); // Filing System - Printable
    var FilingSystem_PackageContent = Coalesce(A[5923]); // Filing System - Package Content
    var PaperSupplies_Features = Coalesce(A[6031]); // Paper Supplies - Features
    // Labels|Hanging File Frames|Tabs|Fasteners
    var FilingAccessoryTypeRef = R("SP-18605").HasValue() ? R("SP-18605").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18605").HasValue() ? R("cnet_common_SP-18605").Replace("<NULL>", "").Text : "";

    if(FilingSystem_Features.HasValue("permanent adhesive")
    && FilingSystem_ProductType.HasValue("%self-adhesive%")
    && FilingSystem_Printable.HasValue("Yes")){
        result = $"Permanent {FilingSystem_ProductType.FirstValue().ToLower()} are easy to use and can be printed on";
    }
    else if(FilingSystem_Features.HasValue("permanent adhesive")
        && FilingSystem_ProductType.HasValue("%self-adhesive%")
        && FilingSystem_PackageContent.HasValue("printable tab inserts")){
        result = $"Permanent {FilingSystem_ProductType.FirstValue().ToLower()} are easy to use with printable tab inserts";
    }
    else if(FilingSystem_Features.HasValue("permanent adhesive")
        && FilingSystem_ProductType.HasValue("%self-adhesive%")){
        result = $"Permanent {FilingSystem_ProductType.FirstValue().ToLower()} are easy to use";
    }
    else if( PaperSupplies_Features.HasValue("%self-adhesive%")
        && !String.IsNullOrEmpty(FilingAccessoryTypeRef)){
        result = $"Self-adhesive {FilingAccessoryTypeRef.ToLower()} don't need glue or tape";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AdhesiveFilingAccessory⸮{result}");
    }
}

// --[FEATURE #9]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Heavy_Duty_Filing_Accessory(){
    var result = "";
    var FilingSystem_Features = Coalesce(A[5945]); // Filing System - Features

    if(FilingSystem_Features.HasValue("heavy-duty")){
        result = "Heavy-duty construction to hold up to years of use";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"HeavyDutyFilingAccessory⸮{result}");
    }
}

// --[FEATURE #10]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Laminated_Surface_Filing_Accessory(){
    var result = "";
    var FilingSystem_Features = Coalesce(A[5945]); // Filing System - Features

    if(FilingSystem_Features.HasValue("laminated surface")){
        result = "They are laminated to prevent them from cracking and smudging";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"LaminatedSurfaceFilingAccessory⸮{result}");
    }
}

// --[FEATURE #11]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Cut_Tab_Filing_Accessory(){
    var result = "";
    var FilingSystem_Features = Coalesce(A[5945]); // Filing System - Features
    var PaperSupplies_Features = Coalesce(A[6059]); // Paper Supplies - Features

    if(FilingSystem_Features.HasValue("1/5 cut tab")
    && FilingSystem_Features.HasValue("angled")){
        result = "The 1/5-cut design makes wide enough to read easily, which is also helped by angled construction, so you can flip through dividers quickly, and find the contents you need to access";
    }
    else if(FilingSystem_Features.HasValue("1/5 cut tab")
        || PaperSupplies_Features.HasValue("1/5 cut tab")){
        result = "The 1/5-cut design makes them wide enough to read easily so you can flip through dividers quickly and find the contents you need to access";
    }
    // --Angled for easy viewing from above file drawers 
    else if(FilingSystem_Features.HasValue("angled")){
        result = "Angled for easy viewing from above file drawers";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"CutTabFilingAccessory⸮{result}");
    }
}

// --[FEATURE #12]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Tear_Resistant_Filing_Accessory(){
    var result = "";
    var FilingSystem_Features = Coalesce(A[5945]); // Filing System - Features

    if(FilingSystem_Features.HasValue("tear-resistant")){
        result = "Tear resistant material is strong and durable";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"TearResistantFilingAccessory⸮{result}");
    }
}

// --[FEATURE #13]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Blank_Inserts_Filing_Accessory(){
    var result = "";
    var FilingSystem_PackageContent = Coalesce(A[5923]); // Filing System - Package Content

    if(FilingSystem_PackageContent.HasValue("blank inserts")){
        result = "Available with blank tabs for your own indexing or as alphabetic sets";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"BlankInsertsFilingAccessory⸮{result}");
    }
}

// --[FEATURE #14]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Printing_Technology_Filing_Accessory(){
    var result = "";
    var Media_PrintingTechnology = Coalesce(A[4763]); // Media - Printing Technology

    if(Media_PrintingTechnology.HasValue()){
        result = $"For use with {Media_PrintingTechnology.Values.Select(s => s.Value()).FlattenWithAnd()} printers";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"PrintingTechnologyFilingAccessory⸮{result}");
    }
}

// --[FEATURE #15]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
// --Made from heavyweight 25 pt. pressboard for superior durability
void Heavy_Weight_Filing_Accessory(){
    var result = "";
    var PaperSupplies_Features = Coalesce(A[6059]); // Paper Supplies - Features

    if(PaperSupplies_Features.HasValue("heavyweight")){
        result = "Made from heavyweight material for superior durability";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"HeavyWeightFilingAccessory⸮{result}");
    }
}

// --[FEATURE #16]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Self_Adhesiveness_Filing_Accessory(){
    var result = "";
    var FilingSystem_Features = Coalesce(A[5945]); // Filing System - Features

    if(FilingSystem_Features.HasValue("self-adhesive")){
        result = "Self-adhesiveness for rigid pasting and quick reference";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"SelfAdhesivenessFilingAccessory⸮{result}");
    }
}

// --[FEATURE #17]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Alphabetical_Order_Filing_Accessory(){
    var result = "";
    var FilingSystem_Printable = Coalesce(A[4759]); // Filing System - Printable

    if(FilingSystem_Printable.HasValue("bar-index color-coded labels (alphabetic)")){
        result = "They allow users to effectively identify and organize items by alphabetical order";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AlphabeticalOrderFilingAccessory⸮{result}");
    }
}

//§§135391203158855 "Filing Accessories END" "Serhii.O" 

//§§168130738704140478 "Headsets & Microphones BEGIN" "Serhii.O" 

Headset_Microphone_Type_Use();
Cord_Headset_Microphone();
Headset_Design_Color();
Material_Of_Item_Coating();
Microphone_And_Headset_Technology();
Interface_Headset_Microphone();
Sensitivity_Headset_Microphone();
Microphones_Frequency();
Impedance_Headset_Microphone();
Power_Source();
Push_To_Talk();
// Warranty - All

// --[FEATURE #1]
// --Headset/microphone type & Use
void Headset_Microphone_Type_Use(){
    var HeadsetMicrophoneTypeUse = "";
    // Gooseneck Microphone|Headset Microphone|Ear Cushion|Ear Loops|Shotgun Microphone|Lapel Microphone|Microphone Holder|Desktop Microphone|Condenser Microphone|Handheld Microphone|Desktop Microphone|Computer Headset|Phone Headset
    var HeadsetMicrophoneTypeRef = R("SP-18064").HasValue() ? R("SP-18064") : 
        R("cnet_common_SP-18064").HasValue() ? R("cnet_common_SP-18064") : Coalesce("");

    // --Plantronics® EncorePro HW510V® Black monaural voice tube headset is suitable for customer service centers and offices. (https://www.staples.com/product_IM1XU0898)
    // --headset is perfect for everyday use with compatible electronic devices (https://www.staples.com/product_24274917)
    if(HeadsetMicrophoneTypeRef.ToLower().In("%headset%")){
        HeadsetMicrophoneTypeUse = $"{HeadsetMicrophoneTypeRef.Text} is perfect for everyday use with compatible electronic devices";
    }
    if (!String.IsNullOrEmpty(HeadsetMicrophoneTypeUse)) {
        Add($"HeadsetMicrophoneTypeUse⸮{HeadsetMicrophoneTypeUse}");
    }
}

// --[FEATURE #2]
// --Cord (length and type)
void Cord_Headset_Microphone(){
    var CordHeadsetMicrophone = "";
    var CableLength_ftRef = R("SP-21212").HasValue() ? R("SP-21212").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21212").HasValue() ? R("cnet_common_SP-21212").Replace("<NULL>", "").Text : "";
    var AudioOutput_CableLength = Coalesce(A[8577]); // Audio Output - Cable Length
    var CableDetails_Length = Coalesce(A[317]); // Cable Details - Length

    // --This microphone features 3.5 mm audio jack to connect to audio sources and 6.5' cable for extra reach
    // --Provides wired connectivity with 0.39' cable length (https://www.staples.com/product_IM12MZ909)
    // --Plantronics® Male patch cord has 18 of length and right angle plug for easy connection. (https://www.staples.com/product_IM1F35659)
    // --patch cord has 18 of length and right angle plug for easy connection. (https://www.staples.com/product_IM1F35659)
    // --The headphones with cord length of 2.5 m, has nickel-plated plug and ferrite magnet. (https://www.staples.com/product_IM14T7276)
    if(!String.IsNullOrEmpty(CableLength_ftRef)){
        CordHeadsetMicrophone = $"Provides wired connectivity with {CableLength_ftRef}'L cable";
    }
    else if(AudioOutput_CableLength.HasValue()){
        switch(AudioOutput_CableLength.Units.First().NameUSM){
            case var Unit when Unit.In("%in%"):
            CordHeadsetMicrophone = $@"Provides wired connectivity with {AudioOutput_CableLength.Values.First().ValueUSM}""L cable";
            break;
            case var Unit when Unit.In("%ft%"):
            CordHeadsetMicrophone = $"Provides wired connectivity with {AudioOutput_CableLength.Values.First().ValueUSM}'L cable";
            break;
        }
    }
    else if(CableDetails_Length.HasValue()){
        switch(CableDetails_Length.Units.First().NameUSM){
            case var Unit when Unit.In("%in%"):
            CordHeadsetMicrophone = $@"Provides wired connectivity with {CableDetails_Length.Values.First().ValueUSM}""L cable";
            break;
            // S21308633 - Provides wired connectivity with 8L cable
            case var Unit when Unit.In("%ft%"):
            CordHeadsetMicrophone = $"Provides wired connectivity with {CableDetails_Length.Values.First().ValueUSM}'L cable";
            break;
        }
    }
    if (!String.IsNullOrEmpty(CordHeadsetMicrophone)) {
        Add($"CordHeadsetMicrophone⸮{CordHeadsetMicrophone}");
    }
}

// --[FEATURE #3]
// --Headset design & Color
void Headset_Design_Color(){
    var HeadsetDesignColor = "";
    // Gooseneck Microphone|Headset Microphone|Ear Cushion|Ear Loops|Shotgun Microphone|Lapel Microphone|Microphone Holder|Desktop Microphone|Condenser Microphone|Handheld Microphone|Desktop Microphone|Computer Headset|Phone Headset
    var HeadsetMicrophoneTypeRef = R("SP-18064").HasValue() ? R("SP-18064").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18064").HasValue() ? R("cnet_common_SP-18064").Replace("<NULL>", "").Text : "";
    // Behind-the-Neck|Earbuds|Over-the-Ear|Over-the-Head
    var HeadsetDesignRef = R("SP-18066").HasValue() ? R("SP-18066").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18066").HasValue() ? R("cnet_common_SP-18066").Replace("<NULL>", "").Text : "";
    var TrueColorRef = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : "";
    var AudioOutput_Foldable = Coalesce(A[8570]); // Audio Output - Foldable
    var AudioOutput_HeadphonesEarPartsType = Coalesce(A[7425]); // Audio Output - Headphones Ear-Parts Type
    var AudioOutput_HeadphonesMount = Coalesce(A[7426]); // Audio Output - Headphones Mount
    var Miscellaneous_Color_ColorCategory = Coalesce(A[373], A[5811]); // Miscellaneous - Color | Color Category
    var Miscellaneous_EarpadMaterial = Coalesce(A[8468]); // Miscellaneous - Earpad Material

    // ----------------------------------------------------------Behind-the-Neck
    // --Headset with foldable design ensures comfortable fit behind neck design.  (https://www.staples.com/product_788621)
    // --Headset features comfortable ear cushions and behind the neck foldable design for added comfort. (https://www.staples.com/product_788621)
    // --Headset can be worn with two ear buds in a behind-the-neck style (https://www.staples.com/product_IM1KU7672)
    if(HeadsetDesignRef.Equals("Behind-the-Neck")
    && AudioOutput_Foldable.HasValue()
    && !String.IsNullOrEmpty(TrueColorRef)){
        HeadsetDesignColor = $"{TrueColorRef.ToLower().ToUpperFirstChar()} headset with foldable design ensures comfortable fit behind neck design";
    }
    else if(HeadsetDesignRef.Equals("Behind-the-Neck")
        && AudioOutput_Foldable.HasValue()){
        HeadsetDesignColor = "Headset with foldable design ensures comfortable fit behind neck design";
    }
    else if(HeadsetDesignRef.Equals("Behind-the-Neck")
        && !String.IsNullOrEmpty(TrueColorRef)){
        HeadsetDesignColor = $"{TrueColorRef.ToLower().ToUpperFirstChar()} headset can be worn with two ear buds in a behind-the-neck style";
    }
    else if(HeadsetDesignRef.Equals("Behind-the-Neck")){
        HeadsetDesignColor = "Headset can be worn with two ear buds in a behind-the-neck style";
    }
    else if(!String.IsNullOrEmpty(TrueColorRef)
        && AudioOutput_HeadphonesEarPartsType.HasValue("%on-ear%", "%full size%")){
        HeadsetDesignColor = $"{TrueColorRef.Replace(" color highlights", " accent").Replace(" ring", " accent").ToLower().ToUpperFirstChar()} on-ear design with high-comfort ear pads";
    }
    else if(AudioOutput_HeadphonesEarPartsType.HasValue("%on-ear%", "%full size%")){
        HeadsetDesignColor = "On-ear design with high-comfort ear pads";
    }
    // ----------------------------------------------------------Earbuds
    else if(AudioOutput_HeadphonesEarPartsType.HasValue("ear-bud")
        && Miscellaneous_Color_ColorCategory.HasValue()){
        HeadsetDesignColor = $"{Miscellaneous_Color_ColorCategory.FirstValue().ToUpperFirstChar()} earbuds deliver optimum sound quality";
    }
    else if(AudioOutput_HeadphonesEarPartsType.HasValue("ear-bud")){
        HeadsetDesignColor = "Earbuds deliver optimum sound quality and comfortable wearing for all day use";
    }
    else if(AudioOutput_HeadphonesEarPartsType.HasValue("ear-bud")
        && !String.IsNullOrEmpty(TrueColorRef)){
        HeadsetDesignColor = $"Comfortable wearing {TrueColorRef.ToLower()} earbuds for all day use";
    }
    // ----------------------------------------------------------Over-the-Ear
    else if(AudioOutput_HeadphonesMount.HasValue("over-the-ear mount")
        && Miscellaneous_Color_ColorCategory.HasValue()){
        HeadsetDesignColor = $"{Miscellaneous_Color_ColorCategory.FirstValue().ToUpperFirstChar()} over-ear headphones for listening to music on-the-go";
    }
    else if(AudioOutput_HeadphonesMount.HasValue("over-the-ear mount")){
        HeadsetDesignColor = "Over-the-ear design makes these headphones comfortable";
    }
    // ----------------------------------------------------------Over-the-Head
    // --headset has over-the-head design with soft ear cushions for all-day wearing comfort. (https://www.staples.com/product_IM1XU0898)
    // --Comes in over-the-head style for added comfort (https://www.staples.com/product_IM1XU0898)
    else if(HeadsetDesignRef.Equals("Over-the-Head")
        && Miscellaneous_EarpadMaterial.HasValue("%soft%")
        && !String.IsNullOrEmpty(TrueColorRef)){
        HeadsetDesignColor = $"{TrueColorRef.ToLower().ToUpperFirstChar()} headset has over-the-head design with soft ear cushions for all-day wearing comfort";
    }
    else if(HeadsetDesignRef.Equals("Over-the-Head")
        && Miscellaneous_EarpadMaterial.HasValue("%soft%")){
        HeadsetDesignColor = "Headset has over-the-head design with soft ear cushions for all-day wearing comfort";
    }
    else if(HeadsetDesignRef.Equals("Over-the-Head")
        && !String.IsNullOrEmpty(TrueColorRef)
        && !String.IsNullOrEmpty(HeadsetMicrophoneTypeRef)){
        HeadsetDesignColor = $"{TrueColorRef.ToLower().ToUpperFirstChar()} {HeadsetMicrophoneTypeRef.ToLower()} comes in over-the-head style for added comfort";
    }
    else if(HeadsetDesignRef.Equals("Over-the-Head")
        && !String.IsNullOrEmpty(HeadsetMicrophoneTypeRef)){
        HeadsetDesignColor = $"{HeadsetMicrophoneTypeRef.ToLower().ToUpperFirstChar()} comes in over-the-head style for added comfort";
    }
    if (!String.IsNullOrEmpty(HeadsetDesignColor)) {
        Add($"HeadsetDesignColor⸮{HeadsetDesignColor}");
    }
}

// --[FEATURE #4]
// --Material of item & Coating
void Material_Of_Item_Coating(){
    var MaterialOfItemCoating = "";
    var Miscellaneous_BodyMaterial = Coalesce(A[830]); // Miscellaneous - Body Material
    var Miscellaneous_HeadbandMaterial = Coalesce(A[8632]); // Miscellaneous - Headband Material

    // --Rugged ABS plastic headstrap and earcups resist breakage in high-use situations, replaceable leatherette ear cushions (https://www.staples.com/product_IM19X9323)
    // --Plastic and metal construction provides style and durability (Plastic and metal construction provides style and durability )
    // --The laser-welded construction of this headset, creates more robust design to increase long term reliability(https://www.staples.com/product_IM1XU0899)
    // --Laser-welded construction and metal joints are great to provide reliability where it matters (https://www.staples.com/product_IM1XU0899)
    if(Miscellaneous_BodyMaterial.HasValue()
        && Miscellaneous_BodyMaterial.Where("%metal%", "%aluminum%", "%steel%").Count() == 3){
        MaterialOfItemCoating = $"{Miscellaneous_BodyMaterial.Where("%metal%", "%aluminum%", "%steel%").Select(s => s).FlattenWithAnd().ToUpperFirstChar()} construction creates more robust design to increase long-term reliability";
    }
    else if(Miscellaneous_HeadbandMaterial.HasValue()
        && Miscellaneous_HeadbandMaterial.Where("%metal%", "%aluminum%", "%steel%").Count() == 3){
        MaterialOfItemCoating = $"{Miscellaneous_BodyMaterial.Where("%metal%", "%aluminum%", "%steel%").Select(s => s).FlattenWithAnd().ToUpperFirstChar()} construction provides durability";
    }
    //  S21308633 - ABS plastic construction provides reliability where it matters
    else if(Coalesce(A[830], A[8632]).HasValue()){
        MaterialOfItemCoating = $"{Coalesce(A[830], A[8632]).FirstValue().ToUpperFirstChar()} construction provides reliability where it matters";
    }
    if (!String.IsNullOrEmpty(MaterialOfItemCoating)) {
        Add($"MaterialOfItemCoating⸮{MaterialOfItemCoating}");
    }
}

// --[FEATURE #5]
// --Microphone and headset technology
void Microphone_And_Headset_Technology(){
    var MicrophoneAndHeadsetTechnology = "";
    var AudioOutput_Details = Coalesce(A[9410]); //  Audio Output - Details

    // --SoundGuard technology protects from noise spikes and listening fatigue (https://www.staples.com/product_1095659)
    if(AudioOutput_Details.HasValue("%SoundGuard%")){
        MicrophoneAndHeadsetTechnology = "SoundGuard technology protects from noise spikes and listening fatigue";
    }
    if (!String.IsNullOrEmpty(MicrophoneAndHeadsetTechnology)) {
        Add($"MicrophoneAndHeadsetTechnology⸮{MicrophoneAndHeadsetTechnology}");
    }
}

// --[FEATURE #6]
// --Interface
void Interface_Headset_Microphone(){
    var InterfaceHeadsetMicrophone = "";
    // Gooseneck Microphone|Headset Microphone|Ear Cushion|Ear Loops|Shotgun Microphone|Lapel Microphone|Microphone Holder|Desktop Microphone|Condenser Microphone|Handheld Microphone|Desktop Microphone|Computer Headset|Phone Headset
    var HeadsetMicrophoneTypeRef = R("SP-18064").HasValue() ? R("SP-18064") : 
        R("cnet_common_SP-18064").HasValue() ? R("cnet_common_SP-18064") : Coalesce("");
    var Connections_ConnectorType = Coalesce(A[1416]); // Connections - Connector Type
    var AudioOutput_BluetoothVersion = Coalesce(A[8461]); // Audio Output - Bluetooth Version
    var AudioSystem_MaxOperatingDistance = Coalesce(A[10022]); // Audio System - Max Operating Distance
    var AudioOutput_WirelessTechnology = Coalesce(A[1102]); //Audio Output - Wireless Technology
    var AudioOutput_ConnectivityTechnology = Coalesce(A[444]); // Audio Output - Connectivity Technology

    // --It connects to PC via USB/USB-C, connects to mobile devices and tablets via 3.5 mm jack and devices that support USB-C. (https://www.staples.com/product_IM12TW534)
    // --Mini-phone host interface headphone offers high performance (https://www.staples.com/product_IM1J77664)
    // --It comes with USB Bluetooth adapter for plug-and-play computer connectivity and USB cable for convenient charging. (https://www.staples.com/product_IM11DM463)
    // --USB Host interface for user convenience (https://www.staples.com/product_IM1VR5678)
    // --USB Microphone can be used across many different devices without hassle. (https://www.staples.com/product_962397)
    // --Quick Disconnect feature provides walkaway convenience (https://www.staples.com/product_1095659)
    // --Wired connectivity allows connection to PC or desk phone (https://www.staples.com/product_IM1XU0897)
    if(Connections_ConnectorType.HasValue("%Quick Disconnect%")){
        InterfaceHeadsetMicrophone = "Quick Disconnect feature provides walkaway convenience";
    }
    else if(AudioOutput_BluetoothVersion.HasValue("bluetooth 4.1%")
        && AudioSystem_MaxOperatingDistance.HasValue()
        && AudioSystem_MaxOperatingDistance.Units.First().NameUSM.In("ft")){
        InterfaceHeadsetMicrophone = $"Bluetooth 4.1 prolongs battery life and allowing you to listen clearly up to {AudioSystem_MaxOperatingDistance.Values.First().ValueUSM}' from your device";
    }
    else if(AudioOutput_WirelessTechnology.HasValue("%bluetooth%")){
        InterfaceHeadsetMicrophone = "Bluetooth connectivity for connecting external Bluetooth-enabled devices";
    }
    else if(AudioOutput_WirelessTechnology.HasValue("%Dect%")){
        InterfaceHeadsetMicrophone = "DECT frequency band transmits your voice and gives transcendent voice and sound quality";
    }
    else if(Connections_ConnectorType.HasValue("%3.5%")
        && Connections_ConnectorType.HasValue("%USB%")){
        InterfaceHeadsetMicrophone = "Connects to mobile devices and tablets via 3.5 mm jack and devices that support USB";
    }
    else if(Connections_ConnectorType.HasValue("%3.5%")){
        InterfaceHeadsetMicrophone = "Mini-phone host interface headphone offers high performance";
    }
    else if(Connections_ConnectorType.HasValue("%USB%")
        && HeadsetMicrophoneTypeRef.HasValue()){
        InterfaceHeadsetMicrophone = $"USB {HeadsetMicrophoneTypeRef} can be used across many different devices without hassle";
    }
    else if(AudioOutput_ConnectivityTechnology.HasValue("%wired%")){
        InterfaceHeadsetMicrophone = "Wired connectivity allows connection to PC or desk phone";
    }
    if (!String.IsNullOrEmpty(InterfaceHeadsetMicrophone)) {
        Add($"InterfaceHeadsetMicrophone⸮{InterfaceHeadsetMicrophone}");
    }
}

// --[FEATURE #7]
// --Sensitivity (for microphone/earpiece)
void Sensitivity_Headset_Microphone(){
    var SensitivityHeadsetMicrophone = "";
    var MicrophoneSensitivityRef = R("SP-22015").HasValue() ? R("SP-22015").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22015").HasValue() ? R("cnet_common_SP-22015").Replace("<NULL>", "").Text : "";

    // --Enables safe listening experience with maximum 104 dB limited sensitivity by ActiveGard™ SPL (https://www.staples.com/product_IM1RG6774)
    // --The 113 dB/1 mW sensitivity ensures noiseless clear audio (https://www.staples.com/product_IM1TW9698)
    // --105 dB/1 mW sensitivity for better audio fidelity (https://www.staples.com/product_208098)
    if(!String.IsNullOrEmpty(MicrophoneSensitivityRef)){
        SensitivityHeadsetMicrophone = $"{MicrophoneSensitivityRef} microphone sensitivity ensures noiseless clear audio";
    }
    if (!String.IsNullOrEmpty(SensitivityHeadsetMicrophone)) {
        Add($"SensitivityHeadsetMicrophone⸮{SensitivityHeadsetMicrophone}");
    }
}

// --[FEATURE #8]
// --Microphones frequency
void Microphones_Frequency(){
    var MicrophonesFrequency = "";
    // Gooseneck Microphone|Headset Microphone|Ear Cushion|Ear Loops|Shotgun Microphone|Lapel Microphone|Microphone Holder|Desktop Microphone|Condenser Microphone|Handheld Microphone|Desktop Microphone|Computer Headset|Phone Headset
    var HeadsetMicrophoneTypeRef = R("SP-18064").HasValue() ? R("SP-18064") : 
        R("cnet_common_SP-18064").HasValue() ? R("cnet_common_SP-18064") : Coalesce("");
    var AudioInput_FrequencyResponse = Coalesce(A[279]); // Audio Input - Frequency Response
    var AudioInput_ActiveNoiseCanceling = Coalesce(A[9894]); // Audio Input - Active Noise Canceling

    // --Headset in hot pink color has an operating frequency range of 20 Hz - 20 kHz and offers noise cancellation to decrease unwanted ambient sounds. (https://www.staples.com/product_1289674)
    // --It has mini-phone interface for connectivity and works well at 20 - 20000 Hz frequency band. (https://www.staples.com/product_IM12R5018)
    // --captures audio cleanly at a frequency response of 40 Hz to 18 kHz(https://www.staples.com/product_962397)
    if(HeadsetMicrophoneTypeRef.In("Ear Cushion", "Ear Loops", "Microphone Holder")){
        MicrophonesFrequency = "";
    }
    else if(HeadsetMicrophoneTypeRef.HasValue()
        && AudioInput_FrequencyResponse.HasValue()
        && AudioInput_ActiveNoiseCanceling.HasValue()){
        MicrophonesFrequency = $"{HeadsetMicrophoneTypeRef.Text.ToLower().ToUpperFirstChar()} has an operating frequency range of {AudioInput_FrequencyResponse.FirstValue().Replace(" - ", "-")}{AudioInput_FrequencyResponse.Units.First().Name} and offers noise cancellation to decrease unwanted ambient sounds";
    }
    else if(HeadsetMicrophoneTypeRef.HasValue()
        && AudioInput_FrequencyResponse.HasValue("%-%")){
        MicrophonesFrequency = $"{HeadsetMicrophoneTypeRef.Text.ToLower().ToUpperFirstChar()} captures audio cleanly at frequencies of {AudioInput_FrequencyResponse.FirstValue().ExtractNumbers().Select(s => $"{s}Hz").Flatten("-")}";
    }
    else if(AudioInput_FrequencyResponse.HasValue()){
        MicrophonesFrequency = $"{HeadsetMicrophoneTypeRef.Text.ToLower().ToUpperFirstChar()} captures audio cleanly at frequency of {AudioInput_FrequencyResponse.FirstValue().ExtractNumbers().Select(s => $"{s}Hz").Flatten("-")}";
    }
    if (!String.IsNullOrEmpty(MicrophonesFrequency)) {
        Add($"MicrophonesFrequency⸮{MicrophonesFrequency}");
    }
}

// --[FEATURE #9]
// --Impedance
void Impedance_Headset_Microphone(){
    var ImpedanceHeadsetMicrophone = "";
    var AudioInput_Impedance = Coalesce(A[341]); // Audio Input - Impedance

    // --16 Ohms impedance to support large scale conferences (https://www.staples.com/product_962398)
    // --2000 Ohms impendence and 62 dBV sensitivity for superior performance (https://www.staples.com/product_IM1E92884)
    if(AudioInput_Impedance.HasValue()){
        switch(AudioInput_Impedance.FirstValue()){
            case var Impedance when Impedance < 100:
            ImpedanceHeadsetMicrophone = $"##{Impedance} ohm impedance to support large scale conferences";
            break;
            case var Impedance when Impedance > 99:
            ImpedanceHeadsetMicrophone = $"##{Impedance} ohm impendence for superior performance";
            break;
        }
    }
    if (!String.IsNullOrEmpty(ImpedanceHeadsetMicrophone)) {
        Add($"ImpedanceHeadsetMicrophone⸮{ImpedanceHeadsetMicrophone}");
    }
}

// --[FEATURE #10]
// --Power source
void Power_Source(){
    var PowerSource = "";
    var Battery_RunTime = Coalesce(A[341]); // Battery - Run Time (Up To)

    if(Battery_RunTime.HasValue()
    && Battery_RunTime.Units.First().Name.In("hour%")){
        PowerSource = $"Rechargeable battery provides up to {Battery_RunTime.FirstValue()} hours of working time";
    }
    if (!String.IsNullOrEmpty(PowerSource)) {
        Add($"PowerSource⸮{PowerSource}");
    }
}

// --[FEATURE #11]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Push_To_Talk(){
    var PushToTalk = "";
    var AudioOutput_Controls = Coalesce(A[9410]); // Audio Output - Controls

    if(AudioOutput_Controls.HasValue("Push to Talk (PTT)")){
        PushToTalk = "Push-to-talk";
    }
    if (!String.IsNullOrEmpty(PushToTalk)) {
        Add($"PushToTalk⸮{PushToTalk}");
    }
}

// --[FEATURE #12] - All
// --Warranty information

//§§168130738704140478 "Headsets & Microphones END" "Serhii.O" 

//§§5904340310149141983 "Computer Mice BEGIN" "Serhii.O" 

Mouse_Tracking_Method();
Wireless_Or_Wired();
Mouse_Tracking_Method_Operation();
Type_Of_Battery_Number();
Multi_Device();
// Certification & Standards - All
Number_Of_Buttons();
Ergonomic_Mice();
Battery_Indicator();
Adjustable_Trackball();
BlueTrack_Technology();
Gesture_Function();
Optical_Sensor();
USB_Compatibility();
Control_Cursor();
// Warranty - All

// --[FEATURE #1]
// --Mouse tracking method and/or Gaming & use
void Mouse_Tracking_Method(){
    var MouseTrackingMethod = "";
    // Advanced Optical|Trackball|Bluetrack|Bluetooth|Rollerbar|Darkfield|Laser|Optical
    var MouseTrackingMethodRef = R("SP-18605").HasValue() ? R("SP-18605").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18605").HasValue() ? R("cnet_common_SP-18605").Replace("<NULL>", "").Text : "";
    // Gaming|Non Gaming
    var GamingRef = R("SP-13567").HasValue() ? R("SP-13567").Replace("<NULL>", "").Text :
        R("cnet_common_SP-13567").HasValue() ? R("cnet_common_SP-13567").Replace("<NULL>", "").Text : "";

    if(!String.IsNullOrEmpty(MouseTrackingMethodRef)){
        if(!String.IsNullOrEmpty(GamingRef)){
            GamingRef = GamingRef.Replace("Gaming", ". Elevate your multiplayer gaming with the gaming mouse").Replace("Non Gaming", "");
        }
        switch(MouseTrackingMethodRef){
            case var TrackingMethod when TrackingMethod.Equals("Advanced Optical"):
            MouseTrackingMethod = $"Advanced Optical Mouse delivers reliable connectivity along with the responsive performance of a high-resolution optical sensor{GamingRef}";
            break;
            // --BlueTrack mouse for improved precision 
            // --BlueTrack technology combines laser precision and optical power
            case var TrackingMethod when TrackingMethod.Equals("Bluetrack"):
            MouseTrackingMethod = $"BlueTrack technology combines laser precision and optical power for improved precision{GamingRef}";
            break;
            // --Bluetooth connectivity supports a clutter-free workplace
            case var TrackingMethod when TrackingMethod.Equals("Bluetooth"):
            MouseTrackingMethod = $"Bluetooth connectivity supports a clutter-free workplace{GamingRef}";
            break;
            // --Darkfield technology for improved precision 
            case var TrackingMethod when TrackingMethod.Equals("Darkfield"):
            MouseTrackingMethod = $"Darkfield technology for improved precision{GamingRef}";
            break;
            // --Precision laser tracking works on most surfaces 
            case var TrackingMethod when TrackingMethod.Equals("Laser"):
            MouseTrackingMethod = $"Precision laser tracking works on most surfaces{GamingRef}";
            break;
            case var TrackingMethod when TrackingMethod.Equals("Optical"):
            MouseTrackingMethod = $"Optical mouse allows you to have complete control over cursor movement{GamingRef}";
            break;
            default:
            MouseTrackingMethod = GamingRef;
            break;
        }
    }
    if(!String.IsNullOrEmpty(MouseTrackingMethod)){
        Add($"MouseTrackingMethod⸮{MouseTrackingMethod}");
    }
}

// --[FEATURE #2]
// --Wireless or Wired
void Wireless_Or_Wired(){
    var WirelessOrWired = "";
    // Advanced Optical|Trackball|Bluetrack|Bluetooth|Rollerbar|Darkfield|Laser|Optical
    var MouseTrackingMethodRef = R("SP-13644").HasValue() ? R("SP-13644").Replace("<NULL>", "") :
        R("cnet_common_SP-13644").HasValue() ? R("cnet_common_SP-13644").Replace("<NULL>", "") : Coalesce("");
    // Wireless|Wired\Wireless|Wired
    var WirelessOrWiredRef = R("SP-18444").HasValue() ? R("SP-18444").Replace("<NULL>", "") :
        R("cnet_common_SP-18444").HasValue() ? R("cnet_common_SP-18444").Replace("<NULL>", "") : Coalesce("");

    // -- Take your Staples wireless mouse anywhere, and rely on it when you need it(https://www.staples.com/product_1181110)
    if(MouseTrackingMethodRef.In("Trackball", "Rollerbar")
    && WirelessOrWiredRef.In("Wireless")){
        WirelessOrWired = $"Take your wireless {MouseTrackingMethodRef.Text} anywhere and rely on it when you need it";
    }
    else if(WirelessOrWiredRef.In("Wireless")){
        WirelessOrWired = "Take this reliable wireless mouse anywhere";
    }
    // -- Wired connectivity for accuracy and dependability (https://www.staples.com/product_911687)
    else if(WirelessOrWiredRef.In("Wired")){
        WirelessOrWired = "Wired connectivity for accuracy and dependability";
    }
    if(!String.IsNullOrEmpty(WirelessOrWired)){
        Add($"WirelessOrWired⸮{WirelessOrWired}");
    }
}

// --[FEATURE #3]
// --Mouse tracking method & operation
void Mouse_Tracking_Method_Operation(){
    var MouseTrackingMethodOperation = "";
    // Advanced Optical|Trackball|Bluetrack|Bluetooth|Rollerbar|Darkfield|Laser|Optical
    var MouseTrackingMethodRef = R("SP-13644").HasValue() ? R("SP-13644").Replace("<NULL>", "") :
        R("cnet_common_SP-13644").HasValue() ? R("cnet_common_SP-13644").Replace("<NULL>", "") : Coalesce("");

    // -- Optical mouse allows you to have complete control over cursor movement(https://www.staples.com/product_1980581)
    if(MouseTrackingMethodRef.In("%Optical%")){
        MouseTrackingMethodOperation = $"{MouseTrackingMethodRef.Text} mouse allows you to have complete control over cursor movement";
    }
    // -- Optical mouse for improved precision(https://www.staples.com/product_24293721)
    else if(MouseTrackingMethodRef.In("Trackball", "Rollerbar")){
        MouseTrackingMethodOperation = $"{MouseTrackingMethodRef.Text} technology for improved precision";
    }
    if(!String.IsNullOrEmpty(MouseTrackingMethodOperation)){
        Add($"MouseTrackingMethodOperation⸮{MouseTrackingMethodOperation}");
    }
}

// --[FEATURE #4]
// --Type of battery & number of batteries required
void Type_Of_Battery_Number(){
    var TypeOfBatteryNumber = "";
    // 1|2|4|2/1|3
    var NumberOfBatteriesRequiredRef = R("SP-22167").HasValue() ? R("SP-22167").Replace("<NULL>", "") :
        R("cnet_common_SP-22167").HasValue() ? R("cnet_common_SP-22167").Replace("<NULL>", "") : Coalesce("");
    // AAA|NiCd|Alkaline|Lithium|AA|Rechargeable|NiCd/Ni-MH/Lithium-ion|Lithium-Ion
    var TypeOfBatteryRef = R("SP-13512").HasValue() ? R("SP-13512").Replace("<NULL>", "") :
        R("cnet_common_SP-13512").HasValue() ? R("cnet_common_SP-13512").Replace("<NULL>", "") : Coalesce("");

    // -- Powered by 2 AAA batteries(https://www.staples.com/product_2805659)
    if(NumberOfBatteriesRequiredRef.HasValue("1")
    && TypeOfBatteryRef.HasValue()){
        TypeOfBatteryNumber = $"Powered by one {TypeOfBatteryRef.Text} battery";
    }
    else if(NumberOfBatteriesRequiredRef.HasValue()
    && TypeOfBatteryRef.HasValue()){
        TypeOfBatteryNumber = $"Powered by {NumberOfBatteriesRequiredRef.Text} {TypeOfBatteryRef.Text} batteries";
    }
    else if(TypeOfBatteryRef.In("%AA%")){
        TypeOfBatteryNumber = $"Powered by {TypeOfBatteryRef.Text} batteries";
    }
    else if(TypeOfBatteryRef.HasValue()){
        TypeOfBatteryNumber = $"Powered by {TypeOfBatteryRef.Text} battery";
    }
    if(!String.IsNullOrEmpty(TypeOfBatteryNumber)){
        Add($"TypeOfBatteryNumber⸮{TypeOfBatteryNumber}");
    }
}

// --[FEATURE #5]
// --Multi device
void Multi_Device(){
    var MultiDevice = "";
    // No|Yes
    var MultiDeviceRef = R("SP-24727").HasValue() ? R("SP-24727").Replace("<NULL>", "").Text :
        R("cnet_common_SP-24727").HasValue() ? R("cnet_common_SP-24727").Replace("<NULL>", "").Text : "";

    // -- Multi-device mouse for convenience (https://www.staples.com/Logitech-M720-Triathlon-Multi-device-Wireless-Mouse-910-004790/product_2392528)
    if(MultiDeviceRef.Equals("Yes")){
        MultiDevice = "Multi-device mouse for convenience";
    }
    if(!String.IsNullOrEmpty(MultiDevice)){
        Add($"MultiDevice⸮{MultiDevice}");
    }
}

// --[FEATURE #6] - All
// --Certification & Standards

// --[FEATURE #7]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Number_Of_Buttons(){
    var NumberOfButtons = "";
    var NumberOfButtonsRef = R("SP-22165").HasValue() ? R("SP-22165").Replace("<NULL>", "") :
        R("cnet_common_SP-22165").HasValue() ? R("cnet_common_SP-22165").Replace("<NULL>", "") : Coalesce("");
    var InputDevice_Features = Coalesce(A[587]); // Input Device - Features

    switch(NumberOfButtonsRef){
        // -- 3-button design for speed and enhanced functionality  (https://www.staples.com/product_784641)
        case var Buttons when Buttons.In("2", "3")
        && InputDevice_Features.HasValue("programmable buttons"):
        NumberOfButtons = $"Features {Buttons.Text} programmable buttons for quick response and personalized setup";
        break;
        case var Buttons when Buttons.HasValue()
        && InputDevice_Features.HasValue("programmable buttons"):
        NumberOfButtons = $"{Buttons.Text} programmable buttons for personalized setup, speed, and enhanced functionality";
        break;
        case var Buttons when Buttons.In("2", "3"):
        NumberOfButtons = $"{Buttons.Text} buttons for easy navigatio";
        break;
        case var Buttons when Buttons.HasValue():
        NumberOfButtons = $"{Buttons.Text}-button design for speed and enhanced functionality";
        break;
    }
    if(!String.IsNullOrEmpty(NumberOfButtons)){
        Add($"NumberOfButtons⸮{NumberOfButtons}");
    }
}

// --[FEATURE #8]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Ergonomic_Mice(){
    var Ergonomic = "";
    var ErgonomicRef = R("SP-22165").HasValue() ? R("SP-22165").Replace("<NULL>", "").Text :
        R("cnet_common_SP-22165").HasValue() ? R("cnet_common_SP-22165").Replace("<NULL>", "").Text : "";

    // -- Ergonomic shape for comfortable prolonged use(https://www.staples.com/product_1634578) 
    if(ErgonomicRef.Equals("Ergonomic")){
        Ergonomic = "Ergonomic shape for comfortable prolonged use";
    }
    if(!String.IsNullOrEmpty(Ergonomic)){
        Add($"Ergonomic⸮{Ergonomic}");
    }
}

// --[FEATURE #9]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Battery_Indicator(){
    var BatteryIndicator = "";
    var InputDevice_Features = Coalesce(A[587]); // Input Device - Features

    // -- Battery indicator display informs user of battery life(https://www.staples.com/product_306226) 
    if(InputDevice_Features.HasValue("battery indicator")){
        BatteryIndicator = "Battery indicator display informs user of battery life";
    }
    if(!String.IsNullOrEmpty(BatteryIndicator)){
        Add($"BatteryIndicator⸮{BatteryIndicator}");
    }
}

// --[FEATURE #10]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Adjustable_Trackball(){
    var AdjustableTrackball = "";
    var InputDevice_Features = Coalesce(A[587]); // Input Device - Features

    // -- Adjustable trackball for comfort grip(https://www.staples.com/product_2764655)
    if(InputDevice_Features.HasValue("trackball")){
        AdjustableTrackball = "Adjustable trackball for comfort grip";
    }
    if(!String.IsNullOrEmpty(AdjustableTrackball)){
        Add($"AdjustableTrackball⸮{AdjustableTrackball}");
    }
}

// --[FEATURE #11]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void BlueTrack_Technology(){
    var BlueTrackTechnology = "";
    var InputDevice_Features = Coalesce(A[587]); // Input Device - Features
    // Advanced Optical|Trackball|Bluetrack|Bluetooth|Rollerbar|Darkfield|Laser|Optical
    var MouseTrackingMethodRef = R("SP-13644").HasValue() ? R("SP-13644").Replace("<NULL>", "").Text :
        R("cnet_common_SP-13644").HasValue() ? R("cnet_common_SP-13644").Replace("<NULL>", "").Text : "";

    // -- Includes programmable buttons for personalized setup and use (https://www.staples.com/product_886654)
    // -- BlueTrack technology built for the rigors of daily use(https://www.staples.com/product_811870)
    if(InputDevice_Features.HasValue("Microsoft BlueTrack Technology")
    && !MouseTrackingMethodRef.Equals("Bluetrack")){
        BlueTrackTechnology = "BlueTrack technology built for the rigors of daily use";
    }
    if(!String.IsNullOrEmpty(BlueTrackTechnology)){
        Add($"BlueTrackTechnology⸮{BlueTrackTechnology}");
    }
}

// --[FEATURE #12]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Gesture_Function(){
    var GestureFunction = "";
    var InputDevice_Features = Coalesce(A[587]); // Input Device - Features
    var InputDevice_MultiTouch = Coalesce(A[7684]); // Input Device - Multi-Touch
    var ScrollWheelRef = R("SP-12767").HasValue() ? R("SP-12767").Replace("<NULL>", "").Text :
        R("cnet_common_SP-12767").HasValue() ? R("cnet_common_SP-12767").Replace("<NULL>", "").Text : "";

    // -- Includes a scroll wheel for easy movement through documents and pages (https://www.staples.com/product_393202)
    if(InputDevice_Features.HasValue("gesture function")){
        GestureFunction = "Includes gesture function for easy movement through documents and pages";
    }
    else if(InputDevice_MultiTouch.HasValue("Yes")){
        GestureFunction = "Includes Multi-Touch for easy movement through documents and pages";
    }
    else if(!String.IsNullOrEmpty(ScrollWheelRef)){
        GestureFunction = $"Includes {ScrollWheelRef} for easy movement through documents and pages";
    }
    if(!String.IsNullOrEmpty(GestureFunction)){
        Add($"GestureFunction⸮{GestureFunction}");
    }
}

// --[FEATURE #13]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Optical_Sensor(){
    var OpticalSensor = "";
    var MovementResolutionRef = R("SP-350955").HasValue() ? R("SP-350955").Replace("<NULL>", "").Text :
        R("cnet_common_SP-350955").HasValue() ? R("cnet_common_SP-350955").Replace("<NULL>", "").Text : "";

    // -- 1,000 dpi resolution optical sensor ensures reliable tracking (https://www.staples.com/product_142312)
    if(!String.IsNullOrEmpty(MovementResolutionRef)){
        OpticalSensor = $"Optical sensor with {MovementResolutionRef} dpi resolution ensures reliable tracking";
    }
    if(!String.IsNullOrEmpty(OpticalSensor)){
        Add($"OpticalSensor⸮{OpticalSensor}");
    }
}

// --[FEATURE #14]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void USB_Compatibility(){
    var USBCompatibility = "";
    // Bluetooth|PS/2|Direct Connect|USB & PS/2|USB|2.4 GHz|Radio Frequency
    var InterfaceTypeRef = R("SP-18442").HasValue() ? R("SP-18442").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18442").HasValue() ? R("cnet_common_SP-18442").Replace("<NULL>", "").Text : "";

    // -- USB compatibility may be used with a variety of devices (https://www.staples.com/product_366208)
    if(InterfaceTypeRef.Contains("USB")){
        USBCompatibility = "USB compatibility, may be used with a variety of devices";
    }
    if(!String.IsNullOrEmpty(USBCompatibility)){
        Add($"USBCompatibility⸮{USBCompatibility}");
    }
}

// --[FEATURE #15]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void Control_Cursor(){
    var ControlCursor = "";
    var InputDevice_MaxOperatingDistance = Coalesce(A[3019]); // Input Device - Max Operating Distance

    if(SKU.ProductId.In("6834263")){
        ControlCursor = "Signal lets you control your cursor from up to 15' away";
    }
    //  S2133532 - Signal lets you control your cursor from up to 3.3 away
    else if(InputDevice_MaxOperatingDistance.HasValue()){
        ControlCursor = $"Signal lets you control your cursor from up to {InputDevice_MaxOperatingDistance.Values.First().ValueUSM}{InputDevice_MaxOperatingDistance.Units.First().NameUSM.Replace("ft", "\'").Replace("in", "\"")} away";
    }
    if(!String.IsNullOrEmpty(ControlCursor)){
        Add($"ControlCursor⸮{ControlCursor}");
    }
}

// --[FEATURE #16] - All
// --Warranty information

//§§5904340310149141983 "Computer Mice END" "Serhii.O" 

//§§590442000142219 "Computer Monitors BEGIN" "Serhii.O" 

Monitor_Screen_Size(); 
Display_Technology(); 
Monitor_Resolution(); 
Interface_Of_Monitor(); 
Monitor_Aspect_Ratio(); 
Monitor_Color(); 
Max_Viewing_Angle(); 
Overall_Dimensions(); 
Speakers_Included(); 
Monitor_Weight(); 
// Certification & standards - All 
// Warranty - All 

// --[FEATURE #1] 
// --Monitor screen size in Inches 
void Monitor_Screen_Size(){ 
    var result = ""; 
    var MonitorScreenSizeInInchesRef = R("SP-350407").HasValue() ? R("SP-350407").Replace("<NULL>", "").Text : 
R("cnet_common_SP-350407").HasValue() ? R("cnet_common_SP-350407").Replace("<NULL>", "").Text : ""; // Monitor Screen Size in Inches 
var CurvedRef = R("SP-21788").HasValue() ? R("SP-21788").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21788").HasValue() ? R("cnet_common_SP-21788").Replace("<NULL>", "").Text : ""; // Curved 
var MonitorResolutionRef = R("SP-21063").HasValue() ? R("SP-21063").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21063").HasValue() ? R("cnet_common_SP-21063").Replace("<NULL>", "").Text : ""; // Monitor Resolution 
var UltraHD4KRef = R("SP-21063").HasValue() ? R("SP-21063").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21063").HasValue() ? R("cnet_common_SP-21063").Replace("<NULL>", "").Text : ""; // 4K Ultra HD 

if(!String.IsNullOrEmpty(MonitorScreenSizeInInchesRef)){ 
    switch(Coalesce(MonitorScreenSizeInInchesRef).ExtractNumbers().First()){ 
// -- The HP 27 Curved Monitor draws you in and won't set you back (https://www.staples.com/product_2720259) 
        case var Size when Size != 0 && CurvedRef.ToLower().Equals("curved"): 
        result = $@"The {Size}"" Curved Monitor draws you in and won't set you back"; 
        break; 
// -- This large format Full HD monitor boasts a diagonal of stunning 27 (https://www.staples.com/product_2708012) 
        case var Size when Size >= 27 && MonitorResolutionRef.Contains("1080"): 
        result = $@"This large-format Full HD monitor has a diagonal measurement of {MonitorScreenSizeInInchesRef}"""; 
        break; 
        case var Size when Size >= 27 && UltraHD4KRef.Equals("4K Ultra HD"): 
        result = $@"This large-format 4K Ultra HD monitor has a diagonal measurement of {Size}"""; 
        break; 
        case var Size when Size >= 27 && MonitorResolutionRef.Contains("2160"): 
        result = $@"This large-format 4K monitor has a diagonal measurement of {MonitorScreenSizeInInchesRef}"""; 
        break; 
        case var Size when Size >= 27: 
        result = $@"This large-format monitor has a diagonal measurement of {MonitorScreenSizeInInchesRef}"""; 
        break; 
// -- 17 monitor for your viewing pleasure (https://www.staples.com/NEC-AS172-BK-17-Black-LED-Backlit-LCD-Monitor-DVI/product_IM1TX1513?akamai-feo=off) 
        case var Size when Size != 0: 
        result = $@"{MonitorScreenSizeInInchesRef}"" screen for your viewing pleasure"; 
        break; 
    } 
} 
if(!String.IsNullOrEmpty(result)){ 
    Add($"MonitorScreenSize⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Display technology 
void Display_Technology(){ 
    var result = ""; 
    var DisplayTechnologyRef = R("SP-21049").HasValue() ? R("SP-21049").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21049").HasValue() ? R("cnet_common_SP-21049").Replace("<NULL>", "").Text : ""; // Display Technology 
var PanelTypeRef = R("SP-21051").HasValue() ? R("SP-21051").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21051").HasValue() ? R("cnet_common_SP-21051").Replace("<NULL>", "").Text : ""; // Display Technolo 

// -- TFT active matrix display technology for better clarity(https://www.staples.com/product_278695) 
if(DisplayTechnologyRef.Equals("TFT Active Matrix")){ 
    result = "TFT active matrix display technology for better clarity"; 
} 
// -- ASUS® LCD Monitor with 3.5 mm audio jack offers a 1920 x 1080 resolution and has LED backlight to reduce power consumption (https://www.staples.com/product_IM1KW3626) 
else if(DisplayTechnologyRef.Equals("LED Backlight")){ 
    result = "Monitor has LED backlight to reduce power consumption"; 
} 
// -- LCD panel for efficient color reproduction (https://www.staples.com/product_1137520) 
else if(PanelTypeRef.Contains("LCD")){ 
    result = "LCD panel for efficient color reproduction"; 
} 
// -- Step up to the best: IPS technology brings you clear views from almost any angle(https://www.staples.com/product_2529692) 
else if(PanelTypeRef.Contains("IPS")){ 
    result = "Step up to the best: IPS technology brings you clear views from almost any angle"; 
} 
if(!String.IsNullOrEmpty(result)){ 
    Add($"DisplayTechnology⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Monitor resolution 
void Monitor_Resolution(){ 
    var result = ""; 
    var MonitorResolutionRef = R("SP-21063").HasValue() ? R("SP-21063").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21063").HasValue() ? R("cnet_common_SP-21063").Replace("<NULL>", "").Text : ""; /// Monitor Resolution 

// -- Screen Resolution: 1920 x 1080 resolution delivers excellent detail (https://www.staples.com/product_24298470) 
if(!String.IsNullOrEmpty(MonitorResolutionRef)){ 
    result = $"{MonitorResolutionRef} screen resolution delivers excellent detail"; 
} 
if(!String.IsNullOrEmpty(result)){ 
    Add($"MonitorResolution⸮{result}"); 
} 
} 

// --[FEATURE #4] 
// --Interface 
void Interface_Of_Monitor(){ 
    var result = ""; 
    var MonitorVGAInputRef = R("SP-21048").HasValue() ? R("SP-21048").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21048").HasValue() ? R("cnet_common_SP-21048").Replace("<NULL>", "").Text : ""; // Monitor VGA Input 
var HDMIPortsRef = R("SP-14079").HasValue() ? R("SP-14079").Replace("<NULL>", "").Text : 
R("cnet_common_SP-14079").HasValue() ? R("cnet_common_SP-14079").Replace("<NULL>", "").Text : ""; // HDMI Ports 
var HDCPSupportedRef = R("SP-21055").HasValue() ? R("SP-21055").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21055").HasValue() ? R("cnet_common_SP-21055").Replace("<NULL>", "").Text : ""; // HDCP Supported 
var DVIInputRef = R("SP-21069").HasValue() ? R("SP-21069").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21069").HasValue() ? R("cnet_common_SP-21069").Replace("<NULL>", "").Text : ""; // DVI Input 

// -- VGA input and HDMI with HDCP support offers better device compatibility and support (https://www.staples.com/Asus-VE278H-27-Black-LED-Backlit-LCD-Monitor-2-HDMI-DVI/product_IM1PY3733) 
if(MonitorVGAInputRef.ToLower().Equals("yes") 
    && HDMIPortsRef.ToLower().Equals("yes") 
    && HDCPSupportedRef.ToLower().Equals("yes")){ 
    result = "VGA and HDMI with HDCP support offer better device compatibility and support"; 
} 
// -- VGA/HDMI: Ready to connect with both VGA and HDMI ports for HD quality (https://www.staples.com/product_2465802) 
else if(MonitorVGAInputRef.ToLower().Equals("yes") 
    && HDMIPortsRef.ToLower().Equals("yes")){ 
    result = "VGA/HDMI: Ready to connect with both VGA and HDMI ports"; 
} 
// -- DVI & VGA inputs so you can easily power and extend the enjoyment from your smartphone or tablet(https://www.staples.com/Acer-K242HYL-Abd-23-8-Widescreen-LCD-Monitor/product_2738924) 
else if(MonitorVGAInputRef.ToLower().Equals("yes") 
    && DVIInputRef.ToLower().Equals("yes")){ 
    result = "DVI and VGA inputs allow you to display the contents of your smartphone or tablet screen onto the monitor"; 
} 
else if(!MonitorVGAInputRef.ToLower().Equals("no")){ 
    result = $"VGA interface: {MonitorVGAInputRef}"; 
} 
else if(!HDMIPortsRef.ToLower().Equals("no")){ 
    result = $"HDMI interface: {HDMIPortsRef}"; 
} 
else if(!HDCPSupportedRef.ToLower().Equals("no")){ 
    result = $"HDMI interface: {HDCPSupportedRef}"; 
} 
if(!String.IsNullOrEmpty(result)){ 
    Add($"InterfaceOfMonitor⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Monitor aspect ratio 
void Monitor_Aspect_Ratio(){ 
    var result = ""; 
    var MonitorAspectRatioRef = R("SP-21064").HasValue() ? R("SP-21064").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21064").HasValue() ? R("cnet_common_SP-21064").Replace("<NULL>", "").Text : ""; // Monitor Aspect Ratio 

if(!String.IsNullOrEmpty(MonitorAspectRatioRef)){ 
    result = $"{MonitorAspectRatioRef} aspect ratio enables widescreen viewing"; 
} 
if(!String.IsNullOrEmpty(result)){ 
    Add($"MonitorAspectRatio⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Color Depth/Color Support 
void Monitor_Color(){ 
    var result = ""; 
var Display_ColorSupport = Coalesce(A[5580]); // Display - Color Support 

// -- supports 16.7 million colors for vivid visuals (https://www.staples.com/product_IM1QY7874) 
if(Display_ColorSupport.HasValue()){ 
    result = $"Supports {Display_ColorSupport.FirstValue()} for vivid visuals"; 
} 
if(!String.IsNullOrEmpty(result)){ 
    Add($"MonitorColor⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Max viewing angle21059 
void Max_Viewing_Angle(){ 
    var result = ""; 
    var MaxViewingAngle_HorizontalRef = R("SP-21067").HasValue() ? R("SP-21067").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21067").HasValue() ? R("cnet_common_SP-21067").Replace("<NULL>", "").Text : ""; // Max Viewing Angle - Horizontal (Degrees) 
var MaxViewingAngle_VerticalRef = R("SP-21068").HasValue() ? R("SP-21068").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21068").HasValue() ? R("cnet_common_SP-21068").Replace("<NULL>", "").Text : ""; // Max Viewing Angle - Vertical (Degrees) 

if(!String.IsNullOrEmpty(MaxViewingAngle_HorizontalRef) 
    && !String.IsNullOrEmpty(MaxViewingAngle_VerticalRef)){ 
    result = $"Viewing Angle: {MaxViewingAngle_HorizontalRef} degrees horizontal, {MaxViewingAngle_VerticalRef} degrees vertical"; 
} 
if(!String.IsNullOrEmpty(result)){ 
    Add($"MaxViewingAngle⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Overall dimensions 
void Overall_Dimensions(){ 
    var result = ""; 
    var Height_WithStandRef = R("SP-21058").HasValue() ? R("SP-21058").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21058").HasValue() ? R("cnet_common_SP-21058").Replace("<NULL>", "").Text : ""; // Height - With Stand (Inches) 
var Width_WithStandRef = R("SP-21059").HasValue() ? R("SP-21059").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21059").HasValue() ? R("cnet_common_SP-21059").Replace("<NULL>", "").Text : ""; // Width - With Stand (Inches) 
var Depth_WithStandRef = R("SP-21060").HasValue() ? R("SP-21060").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21060").HasValue() ? R("cnet_common_SP-21060").Replace("<NULL>", "").Text : ""; // Depth - With Stand (Inches) 

if(!String.IsNullOrEmpty(Height_WithStandRef) 
    && !String.IsNullOrEmpty(Width_WithStandRef) 
    && !String.IsNullOrEmpty(Depth_WithStandRef)){ 
    result = $@"Overall dimensions: {Height_WithStandRef}""H x {Width_WithStandRef}""W x {Depth_WithStandRef}""D"; 
} 
if(!String.IsNullOrEmpty(result)){ 
    Add($"OverallDimensions⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Speakers included (If Applicable) 
void Speakers_Included(){ 
    var result = ""; 
    var MonitorSpeakersRef = R("SP-14558").HasValue() ? R("SP-14558").Replace("<NULL>", "").Text : 
R("cnet_common_SP-14558").HasValue() ? R("cnet_common_SP-14558").Replace("<NULL>", "").Text : ""; // Monitor Speakers 

// -- Fully integrated sound: Get powerful built-in audio without cluttering your desk with external speakers (https://www.staples.com/product_2465802) 
if(MonitorSpeakersRef.Equals("Yes")){ 
    result = "Fully integrated sound: Get powerful built-in audio without cluttering your desk with external speakers"; 
} 
if(!String.IsNullOrEmpty(result)){ 
    Add($"SpeakersIncluded⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Weight (lbs.) 
void Monitor_Weight(){ 
    var result = ""; 
    var WeightRef = R("SP-20402").HasValue() ? R("SP-20402").Replace("<NULL>", "").Text : 
R("cnet_common_SP-20402").HasValue() ? R("cnet_common_SP-20402").Replace("<NULL>", "").Text : ""; // Weight (lbs.) 

if(!String.IsNullOrEmpty(WeightRef)){ 
    result = $"Weighs {WeightRef} lbs. with stand"; 
} 

if(!String.IsNullOrEmpty(result)){ 
    Add($"MonitorWeight⸮{result}"); 
} 
} 

// --[FEATURE #11] - All 
// --Certification & standards 

// --[FEATURE #12] - All 
// --Warranty 

//§§590442000142219 "Computer Monitors END" "Serhii.O" 

//§§590434031426140452 "Audio/Video Cables BEGIN" "Serhii.O" 

Type_Of_Cable_Interface_Use();
Dimensions_Audio_Video_Cables();
Connectors_Plating();
Cable_Jacket_Material();
Specific_Audio_Visual_Feature();
Connector_Finish_And_Or_Connector_Material();
// Certification & standards - All
Serial_ATA();
Provides_Protection();
HDCP_Compliant();
Latched_Connectors();
Strain_Relief();
// Warranty Information - All

// --[FEATURE #1]
// --Type of cable, Interface & Use
void Type_Of_Cable_Interface_Use(){
    var TypeOfCableInterfaceUse = "";
    var VideoCableTypeRef = !R("SP-18471").Text.Equals("<NULL>") ? R("SP-18471").Text :
    !R("cnet_common_SP-18471").Text.Equals("<NULL>") ? R("cnet_common_SP-18471").Text : "";// Video Cable Type
    var InterfaceRef = !R("SP-19209").Text.Equals("<NULL>") ? R("SP-19209").Text :
    !R("cnet_common_SP-19209").Text.Equals("<NULL>") ? R("cnet_common_SP-19209").Text : ""; // Interface

    if(!String.IsNullOrEmpty(VideoCableTypeRef)
    && !String.IsNullOrEmpty(InterfaceRef)){
        TypeOfCableInterfaceUse = $"This {VideoCableTypeRef} is ideal to connect your {InterfaceRef}-compatible devices";
    }
    if(!String.IsNullOrEmpty(TypeOfCableInterfaceUse)){
        Add($"TypeOfCableInterfaceUse⸮{TypeOfCableInterfaceUse}");
    }
}

// --[FEATURE #2]
// --Dimensions (in Inches) if less than 3 feet. If more than 3 feet dimensions (in Feet). Include gauge of wire (If Applicable)
void Dimensions_Audio_Video_Cables(){
    var DimensionsAudioVideoCables = "";
    var CableLength_Inches_Ref = !R("SP-21325").Text.Equals("<NULL>") ? R("SP-21325").Text :
    !R("cnet_common_SP-21325").Text.Equals("<NULL>") ? R("cnet_common_SP-21325").Text : ""; // Cable Length (Inches)
    var CableLength_ft_Ref = !R("SP-21877").Text.Equals("<NULL>") ? R("SP-21877").Text :
    !R("cnet_common_SP-21877").Text.Equals("<NULL>") ? R("cnet_common_SP-21877").Text : ""; // Cable Length (ft)
    var CableGaugeAWGRef = !R("SP-24322").Text.Equals("<NULL>") ? R("SP-24322").Text :
    !R("cnet_common_SP-24322").Text.Equals("<NULL>") ? R("cnet_common_SP-24322").Text : ""; // Cable Gauge (AWG)
    var CableLength_Inches = 0;
    var CableLength_ft = 0;

    if(!String.IsNullOrEmpty(CableLength_Inches_Ref)){
        CableLength_Inches = Coalesce(CableLength_Inches_Ref).ExtractNumbers().First();
    }
    if(!String.IsNullOrEmpty(CableLength_ft_Ref)){
        CableLength_ft = Coalesce(CableLength_ft_Ref).ExtractNumbers().First();
    }
    if(CableLength_Inches != 0
    && CableLength_ft < 3.0
    && !String.IsNullOrEmpty(CableGaugeAWGRef)){
        DimensionsAudioVideoCables = $@"{CableLength_Inches}"" length {CableGaugeAWGRef} AWG cable";
    }
    else if(CableLength_ft > 2.9
        && !String.IsNullOrEmpty(CableGaugeAWGRef)){
        DimensionsAudioVideoCables = $@"{CableLength_ft}"" length {CableGaugeAWGRef} AWG cable";
    }
    else if(CableLength_ft < 3.0
        && CableLength_Inches < 36.0){
        DimensionsAudioVideoCables = $@"Cable Length {CableLength_Inches}""";
    }
    else if(CableLength_ft > 2.9){
        DimensionsAudioVideoCables = $@"{CableLength_ft}"" length cable";
    }
    if(!String.IsNullOrEmpty(DimensionsAudioVideoCables)){
        Add($"DimensionsAudioVideoCables⸮{DimensionsAudioVideoCables}");
    }
}

// --[FEATURE #3]
// --Connectors & Plating
void Connectors_Plating(){
    var ConnectorsPlating = "";
    var End1ConnectorTypeRef = !R("SP-21232").Text.Equals("<NULL>") ? R("SP-21232").Text :
    !R("cnet_common_SP-21232").Text.Equals("<NULL>") ? R("cnet_common_SP-21232").Text : ""; // End 1 Connector Type
    var End2ConnectorTypeRef = !R("SP-21233").Text.Equals("<NULL>") ? R("SP-21233").Text :
    !R("cnet_common_SP-21233").Text.Equals("<NULL>") ? R("cnet_common_SP-21233").Text : ""; // End 1 Connector Type
    var ConnectorGenderRef = !R("SP-18423").Text.Equals("<NULL>") ? R("SP-18423").Text :
    !R("cnet_common_SP-18423").Text.Equals("<NULL>") ? R("cnet_common_SP-18423").Text : "";// Connector Gender
    var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features
    var SystemPowerCables_Cable_CableFeatures = Coalesce(A[1979]); /// System & Power Cables - Cable - Cable Features

    if(!String.IsNullOrEmpty(End1ConnectorTypeRef)
    && !String.IsNullOrEmpty(End2ConnectorTypeRef)
    && !String.IsNullOrEmpty(ConnectorGenderRef)){
        var AdditionalFeatures = Cables_Cable_AdditionalFeatures.HasValue() ? $" {Cables_Cable_AdditionalFeatures.Where("corrosion-resistant connectors").First().Value().Replace("yes"," with corrosion resistance").Replace("No","")}" : "";
        var CableFeatures = SystemPowerCables_Cable_CableFeatures.HasValue() ? SystemPowerCables_Cable_CableFeatures.Where("corrosion-resistant connectors").First().Value().Replace("yes"," with corrosion resistance").Replace("No","") : "";
        ConnectorsPlating = $"{End1ConnectorTypeRef} to {End2ConnectorTypeRef} ({ConnectorGenderRef.Split(" to ").First()} to {ConnectorGenderRef.Split(" to ").Last()}) connectors{AdditionalFeatures}{CableFeatures}";
    }
    if(!String.IsNullOrEmpty(ConnectorsPlating)){
        Add($"ConnectorsPlating⸮{ConnectorsPlating}");
    }
}

// --[FEATURE #4]
// --Cable jacket material
void Cable_Jacket_Material(){
    var CableJacketMaterial = "";
    var CableJacketMaterialRef = !R("SP-21209").Text.Equals("<NULL>") ? R("SP-21209").Text :
    !R("cnet_common_SP-21209").Text.Equals("<NULL>") ? R("cnet_common_SP-21209").Text : ""; // Cable Jacket Material

    switch(CableJacketMaterialRef){
        case var JacketMaterial when JacketMaterial.Equals("PVC"):
        CableJacketMaterial = "PVC jacket protects the conductor from environmental factors";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Aluminum"):
        CableJacketMaterial = "Cable Jacket Material: Aluminum";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Plastic"):
        CableJacketMaterial = "To enhance durability this cable is enclosed in a plastic jacket";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Rubber"):
        CableJacketMaterial = "Cable Jacket Material: Rubber";
        break;
        case var JacketMaterial when JacketMaterial.Equals("SVT"):
        CableJacketMaterial = "Cable Jacket Material: SVT";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Nylon/Cotton/Kevlar Polymer"):
        CableJacketMaterial = "Cable Jacket Material: Nylon/Cotton/Kevlar Polymer";
        break;
        case var JacketMaterial when JacketMaterial.Equals("CMG"):
        CableJacketMaterial = "It features CMG rated jacket for in-wall applications";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Polypropylene"):
        CableJacketMaterial = "Cable Jacket Material: Polypropylene";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Nylon"):
        CableJacketMaterial = "Cable Jacket Material: Nylon";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Rubberized Plastic"):
        CableJacketMaterial = "Cable Jacket Material: Rubberized Plastic";
        break;
        case var JacketMaterial when JacketMaterial.Equals("CL2"):
        CableJacketMaterial = "CL2 rating ensures that your cable installation complies with fire safety codes and insurance requirements";
        break;
        case var JacketMaterial when JacketMaterial.Equals("TPE"):
        CableJacketMaterial = "Cable Jacket Material: TPE";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Plenum"):
        CableJacketMaterial = "Cable Jacket Material: Plenum";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Xtra-Flex PVC"):
        CableJacketMaterial = "Xtra flexible PVC jacket offers double shielding against outside noise";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Silver Braid Shielding"):
        CableJacketMaterial = "Cable Jacket Material: Silver Braid Shielding";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Duraflex"):
        CableJacketMaterial = "Duraflex jacket for easy routing and installation in tight spaces";
        break;
        case var JacketMaterial when JacketMaterial.Equals("Polyethylene"):
        CableJacketMaterial = "Cable Jacket Material: Polyethylene";
        break;
    }
    if(!String.IsNullOrEmpty(CableJacketMaterial)){
        Add($"CableJacketMaterial⸮{CableJacketMaterial}");
    }
}

// --[FEATURE #5]
// --Specific Audio/Visual Feature (if applicable)
void Specific_Audio_Visual_Feature(){
        var SpecificAudioVisualFeature = "";
    var Cables_Cable_CableKeyFeatures = Coalesce(A[10424]); // Cables - Cable - Cable Key Features
    var SystemPowerCables_Cable_CableFeatures = Coalesce(A[1979]); // System & Power Cables - Cable - Cable Features
    var Cables_Cable_TechnologyFeatures = Coalesce(A[2478]); // Cables - Cable - Technology Features

    if(Cables_Cable_CableKeyFeatures.HasValue()
    && SystemPowerCables_Cable_CableFeatures.HasValue()){
        SpecificAudioVisualFeature = $"Features: {Cables_Cable_CableKeyFeatures.Values.Flatten(", ")}, {SystemPowerCables_Cable_CableFeatures.Values.Select(s => s.Value()).FlattenWithAnd()}";
    }
    //  S21590273 - Features: plenum, 4K support, active cable, HDMI CEC support (Consumer Electronics Control)
    else if(Cables_Cable_CableKeyFeatures.HasValue()
    && Cables_Cable_TechnologyFeatures.HasValue()){
        SpecificAudioVisualFeature = $"Features: {Cables_Cable_CableKeyFeatures.Values.Flatten(", ")}, {Cables_Cable_TechnologyFeatures.Values.Select(s => s.Value()).FlattenWithAnd()}";
    }
    else if(Cables_Cable_CableKeyFeatures.HasValue()){
        SpecificAudioVisualFeature = $"Features: {Cables_Cable_CableKeyFeatures.Values.Flatten(", ")}";
    }
    if(!String.IsNullOrEmpty(SpecificAudioVisualFeature)){
        Add($"SpecificAudioVisualFeature⸮{SpecificAudioVisualFeature}");
    }
}

// --[FEATURE #6]
// --Connector finish and/or Connector material
void Connector_Finish_And_Or_Connector_Material(){
    var ConnectorFinishAndOrConnectorMaterial = "";
    var ConnectorFinishRef = !R("SP-21208").Text.Equals("<NULL>") ? R("SP-21208").Text :
    !R("cnet_common_SP-21208").Text.Equals("<NULL>") ? R("cnet_common_SP-21208").Text : ""; // Connector Finish
    var InterfaceRef = !R("SP-19209").Text.Equals("<NULL>") ? R("SP-19209").Text :
    !R("cnet_common_SP-19209").Text.Equals("<NULL>") ? R("cnet_common_SP-19209").Text : ""; // Interface

    switch(ConnectorFinishRef){
        case var ConnectorFinish when ConnectorFinish.Equals("Gold-Plated")
        && InterfaceRef.Contains("HDMI"):
        ConnectorFinishAndOrConnectorMaterial = "Gold-plated connectors for the highest signal transfer rate";
        break;
        case var ConnectorFinish when ConnectorFinish.Equals("Gold-Plated"):
        ConnectorFinishAndOrConnectorMaterial = "Gold-plated connectors for maximum conductivity";
        break;
        case var ConnectorFinish when ConnectorFinish.Equals("Nickel-Plated"):
        ConnectorFinishAndOrConnectorMaterial = "Nickel-plated connectors that are rustproof";
        break;
        case var ConnectorFinish when ConnectorFinish.Equals("Silver-Plated"):
        ConnectorFinishAndOrConnectorMaterial = "Silver connector plating for good conductivity";
        break;
        case var ConnectorFinish when ConnectorFinish.Equals("Tinned Copper"):
        ConnectorFinishAndOrConnectorMaterial = "Tinned copper conductor for better conductivity";
        break;
    }
    if(!String.IsNullOrEmpty(ConnectorFinishAndOrConnectorMaterial)){
        Add($"ConnectorFinishAndOrConnectorMaterial⸮{ConnectorFinishAndOrConnectorMaterial}");
    }
}

// --[FEATURE #7] - All
// --Certification & standards

// --[FEATURE #8]
// --Serial ATA (SATA)
void Serial_ATA(){
    var SerialATA = "";
    var Cables_Cable_LeftConnectorType = Coalesce(A[588]); // Cables - Cable - Left Connector Type
    var Cables_Cable_RightConnectorType = Coalesce(A[589]); // Cables - Cable - Right Connector Type

    if(Cables_Cable_LeftConnectorType.HasValue("%serial%ata%")
    || Cables_Cable_RightConnectorType.HasValue("%serial%ata%")){
        SerialATA = "Serial ATA (SATA) Yes" ;
    }
    if(!String.IsNullOrEmpty(SerialATA)){
        Add($"SerialATA⸮{SerialATA}");
    }
}

// --[FEATURE #9]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void Provides_Protection(){
    var ProvidesProtection = "";
    var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features

    if(Cables_Cable_AdditionalFeatures.HasValue("EMI/RFI protection")){
        ProvidesProtection = "Provides protection against RF and EM";
    }
    if(!String.IsNullOrEmpty(ProvidesProtection)){
        Add($"ProvidesProtection⸮{ProvidesProtection}");
    }
}

// --[FEATURE #10]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HDCP_Compliant(){
    var HDCPCompliant = "";
    var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features

    if(Cables_Cable_AdditionalFeatures.HasValue("HDCP compliant")){
        HDCPCompliant = "HDCP compliant to provide the highest level of signal quality";
    }
    if(!String.IsNullOrEmpty(HDCPCompliant)){
        Add($"HDCPCompliant⸮{HDCPCompliant}");
    }
}

// --[FEATURE #11]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void Latched_Connectors(){
    var LatchedConnectors = "";
    var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features

    if(Cables_Cable_AdditionalFeatures.HasValue("latched")){
        LatchedConnectors = "Latched connectors avoid accidental disconnections";
    }
    if(!String.IsNullOrEmpty(LatchedConnectors)){
        Add($"LatchedConnectors⸮{LatchedConnectors}");
    }
}

// --[FEATURE #12]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void Strain_Relief(){
    var StrainRelief = "";
    var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features

    if(Cables_Cable_AdditionalFeatures.HasValue("strain relief")){
        StrainRelief = "Strain relief minimizes wear and tear";
    }
    if(!String.IsNullOrEmpty(StrainRelief)){
        Add($"StrainRelief⸮{StrainRelief}");
    }
}

// --[FEATURE #13] - All
// --Warranty Information

//§§590434031426140452 "Audio/Video Cables END" "Serhii.O" 

//§§591714025317164870 "TVs BEGIN" "Serhii.O" 

TV_Size_Use();
TV_HD_QualityTV_Type();
Enabled_3D();
TV_Display_Resolution();
Intergrated_IO_Features();
Audio_Video_Enhancement_Technology();
Ports_Interfaces();
Weight_Pounds();
VESA_Mounting_Standard();
// Certifications & Standards - All
Remote_Control_Details();
// Warranty - All

// -- [FEATURE #1]
// -- TV size & Use
void TV_Size_Use(){
    var TVSizeUse = "";
    var TVSize = !R("SP-18763").Text.Equals("<NULL>") ? R("SP-18763").Text :
    !R("cnet_common_SP-18763").Text.Equals("<NULL>") ? R("cnet_common_SP-18763").Text : "";
    var Television_DiagonalClass = Coalesce(A[963]); // Television - Diagonal Class

    if(Television_DiagonalClass.HasValue()){
        switch(TVSize){
            // -- Ideal for home, office, cars, trucks, RVs and boats
            case var Size when Size.Equals(@"Under 20"""):
            TVSizeUse = $@"{Television_DiagonalClass.FirstValue()}"" TV is ideal for home, office, cars, trucks, RVs and boats";
            break;
            // -- The Westinghouse 24'' LED 1080p full HDTV features a sleek and slim look in a perfect size for a dorm room, kitchen or office.
            case var Size when Size.Equals(@"20"" to 29"""):
            TVSizeUse = $@"{Television_DiagonalClass.FirstValue()}"" TV in a perfect size for a dorm room, kitchen or office";
            break;
            // -- The 31.5'' screen size is suitable for a small-sized dorm room
            case var Size when Size.Equals(@"30"" to 39"""):
            TVSizeUse = $@"{Television_DiagonalClass.FirstValue()}"" TV is suitable for a small-sized dorm room";
            break;
            // -- The 50'' screen size is suitable for a living room
            case var Size when Size.Equals(@"40"" to 49""") || Size.Equals(@"50"" to 59"""):
            TVSizeUse = $@"{Television_DiagonalClass.FirstValue()}"" TV is suitable for a living room";
            break;
            // -- Comes with 80'' LED backlit front panel display to offer better picture view
            default:
            TVSizeUse = $@"{Television_DiagonalClass.FirstValue()}"" TV to offer better picture view";
            break;
        }
    }
    if(!String.IsNullOrEmpty(TVSizeUse)){
        Add($"TVSizeUse⸮{TVSizeUse}");
    }
}

// -- [FEATURE #2]
// -- TV HD quality & TV type
void TV_HD_QualityTV_Type(){
    var TVHDQualityTVType = "";
    var TVType = !R("SP-18760").Text.Equals("<NULL>") ? R("SP-18760").Text :
    !R("cnet_common_SP-18760").Text.Equals("<NULL>") ? R("cnet_common_SP-18760").Text : "";
    var TVHDQuality = !R("SP-18761").Text.Equals("<NULL>") ? R("SP-18761").Text :
    !R("cnet_common_SP-18761").Text.Equals("<NULL>") ? R("cnet_common_SP-18761").Text : "";

    // -- Experience your favorite shows and movies in full 1080p quality with this refurbished LED HDTV from Westinghouse Electronics.
    // --4K Ultra HD creates a new level of lifelike clarity.
    if(!string.IsNullOrEmpty(TVType) && !string.IsNullOrEmpty(TVHDQuality)){
        TVHDQualityTVType = $"Experience your favorite shows and movies in {TVHDQuality} quality with this {TVType} TV";
    }
    else if(!string.IsNullOrEmpty(TVType)){
        TVHDQualityTVType = $"Experience your favorite shows and movies with this {TVType} TV";
    }
    else if(TVHDQuality.Equals("4K Ultra")){
        TVHDQualityTVType = "4K Ultra HD creates a new level of lifelike clarity";
    }
    else if(!string.IsNullOrEmpty(TVHDQuality)){
        TVHDQualityTVType = $"Experience your favorite shows and movies in {TVHDQuality} quality";
    }
    if(!String.IsNullOrEmpty(TVHDQualityTVType)){
        Add($"TVHDQualityTVType⸮{TVHDQualityTVType}");
    }
}

// -- [FEATURE #3]
// -- 3D enabled
void Enabled_3D(){
    var Enabled3D = "";
    var Enabled3DRef = !R("SP-18762").Text.Equals("<NULL>") ? R("SP-18762").Text :
    !R("cnet_common_SP-18762").Text.Equals("<NULL>") ? R("cnet_common_SP-18762").Text : "";

    // -- TV when 3D de-interlaced gives better viewing experience. 
    if(Enabled3DRef.Equals("Yes")){
        Enabled3D = "3D gives better viewing experience" ;
    }
    if(!String.IsNullOrEmpty(Enabled3D)){
        Add($"Enabled3D⸮{Enabled3D}");
    }
}

// -- [FEATURE #4]
// -- TV display resolution
void TV_Display_Resolution(){
    var TVDisplayResolution = "";
    var TelevisionsDisplayResolution = !R("SP-14438").Text.Equals("<NULL>") ? R("SP-14438").Text :
    !R("cnet_common_SP-14438").Text.Equals("<NULL>") ? R("cnet_common_SP-14438").Text : "";

    // -- TV when 3D de-interlaced gives better viewing experience. 
    if(!String.IsNullOrEmpty(TelevisionsDisplayResolution)){
        TVDisplayResolution = $"{TelevisionsDisplayResolution} display resolution for enhanced picture quality";
    }
    if(!String.IsNullOrEmpty(TVDisplayResolution)){
        Add($"TVDisplayResolution⸮{TVDisplayResolution}");
    }
}

// -- [FEATURE #5]
// -- Intergrated IO Features
void Intergrated_IO_Features(){
    var IntergratedIOFeatures = "";
    var IntergratedIOFeaturesRef = !R("SP-23601").Text.Equals("<NULL>") ? R("SP-23601").Text :
    !R("cnet_common_SP-23601").Text.Equals("<NULL>") ? R("cnet_common_SP-23601").Text : "";
    // --Has two 10 W built-in speakers and tuner enhance the experience with superior sound
    // --Built-in 2 x 10 W speaker for enhanced sound

    if(!String.IsNullOrEmpty(IntergratedIOFeaturesRef)){
        IntergratedIOFeatures = $"Features {IntergratedIOFeatures} for enhanced sound";
    }
    if(!String.IsNullOrEmpty(IntergratedIOFeatures)){
        Add($"IntergratedIOFeatures⸮{IntergratedIOFeatures}");
    }
}

// -- [FEATURE #6]
// -- Audio/Video Enhancement Technology
void Audio_Video_Enhancement_Technology(){
    var AudioVideoEnhancementTechnology = "";
    var AudioSystem_DigitalAudioFormat = Coalesce(A[1166]); // Audio System - Digital Audio Format
    var Television_MotionEnhancementTechnology = Coalesce(A[5354]); // Television - Motion Enhancement Technology

    // -- DTS digital output;Dolby Digital Plus output;Dolby Pulse output; DTS Studio Sound output
    if(AudioSystem_DigitalAudioFormat.HasValue()
    || Television_MotionEnhancementTechnology.HasValue()){
        var AudioFormat = AudioSystem_DigitalAudioFormat.HasValue() ? $" {AudioSystem_DigitalAudioFormat.Values.FlattenWithAnd(10, ", ")}" : "";
        var MotionEnhancementTechnology = Television_MotionEnhancementTechnology.HasValue() ? $" motion enhancement technology at {Television_MotionEnhancementTechnology.FirstValue().ToLower()}" : "";
        AudioVideoEnhancementTechnology = $"Features:{AudioFormat}{MotionEnhancementTechnology}";
    }
    if(!String.IsNullOrEmpty(AudioVideoEnhancementTechnology)){
        Add($"AudioVideoEnhancementTechnology⸮{AudioVideoEnhancementTechnology}");
    }
}

// --[Feature #7]
// --Ports/Interfaces
void Ports_Interfaces(){
    var PortsInterfaces = "";
    var Connections_ConnectorType = Coalesce(A[1416]); // Connections - Connector Type
    var Television_VideoInterface = Coalesce(A[1887]); // Television - Video Interface

    // ----This Tv has four HDMI, three USB, one RS232C, one RF in and one audio return channel support interface for reliable connectivity.
    // --HDMI Ports Qty: 2; USB Ports Qty: 1; Video Interface: Component, HDMI 
    // --This Tv has four HDMI, three USB, one RS232C, one RF in and one audio return channel support interface for reliable connectivity.
    // --EXPECTED
    // S21209607 - 2 HDMI, 1 USB, component, composite and HDMI video interface for reliable connectivity
    if(Connections_ConnectorType.HasValue()){
        var HDMI = Connections_ConnectorType.Where("%HDMI%").Match(1415).Values().Count() > 0 ? $"{Connections_ConnectorType.Where("%HDMI%").Match(1415).Values().Count()} x HDMI, " : "";
        var USB = Connections_ConnectorType.Where("%USB%").Match(1415).Values().Count() > 0  ? $"{Connections_ConnectorType.Where("%USB%").Match(1415).Values().Count()} x USB, " : "";
        var Other = Television_VideoInterface.HasValue() ? $"{Television_VideoInterface.Values.Select(s => s.Value()).FlattenWithAnd()} video " : "";

        PortsInterfaces = $"{HDMI}{USB}{Other}interface for reliable connectivity";
    }
    if(!String.IsNullOrEmpty(PortsInterfaces)){
        Add($"PortsInterfaces⸮{PortsInterfaces}");
    }
}

// -- [FEATURE #8]
// -- Weight pounds
void Weight_Pounds(){
    var Weight = "";
    var DimensionsWeightDetails_Details = Coalesce(A[4651]); // Dimensions & Weight Details - Details
    var DimensionsWeight_Weight = Coalesce(A[357]); // Dimensions & Weight - Weight

    // S21209607 - Weight: 75 lbs. (with stand), 63.5 lbs. (without stand)
    if(DimensionsWeightDetails_Details.HasValue()){
        var panelWithStand = DimensionsWeightDetails_Details.Where("panel with stand").Match(519).HasValue() ? $"{Math.Round(DimensionsWeightDetails_Details.Where("panel with stand").Match(519).Values().ExtractNumbers().First() * 2.20462, 1)} lbs. (with stand), " : "";
        var panelWithoutStand = DimensionsWeightDetails_Details.Where("panel without stand").Match(519).HasValue() ? $"{Math.Round(DimensionsWeightDetails_Details.Where("panel without stand").Match(519).Values().ExtractNumbers().First() * 2.20462, 1)} lbs. (without stand)" : "";
        Weight = $"Weight: {panelWithStand}{panelWithoutStand}";
    }
    else if(DimensionsWeight_Weight.HasValue()
        && DimensionsWeight_Weight.Units.First().NameUSM.In("lbs")){
        var lb_lbs = DimensionsWeight_Weight.Values.First().ValueUSM == 1 ? " lb." : " lbs.";
        Weight = $"Weight: {DimensionsWeight_Weight.Values.First().ValueUSM}{lb_lbs}";
    }
    if(!String.IsNullOrEmpty(Weight)){
        Add($"Weight⸮{Weight}");
    }
}

// -- [FEATURE #9]
// -- VESA mounting standard
void VESA_Mounting_Standard(){
    var VESAMountingStandard = "";
    var VESAMountingDimensionsRef = !R("SP-23599").Text.Equals("<NULL>") ? R("SP-23599").Text :
    !R("cnet_common_SP-23599").Text.Equals("<NULL>") ? R("cnet_common_SP-23599").Text : "";
    var Miscellaneous_FlatPanelMountInterface = Coalesce(A[2368]); // Miscellaneous - Flat Panel Mount Interface

    // S21209607 - TV is compatible with 300 x 300mm VESA mount 
    if(!String.IsNullOrEmpty(VESAMountingDimensionsRef)){
        VESAMountingStandard = $"TV is compatible with {VESAMountingDimensionsRef} VESA mount";
    }
    else if(Miscellaneous_FlatPanelMountInterface.HasValue("%Yes%")){
        VESAMountingStandard = "TV is VESA mount compatible";
    }
    if(!String.IsNullOrEmpty(VESAMountingStandard)){
        Add($"VESAMountingStandard⸮{VESAMountingStandard}");
    }
}

// -- [FEATURE #10] - All
// -- Certifications & Standards

// --[Feature #11]
// --Remote control details
void Remote_Control_Details(){
    var RemoteControlDetails = "";
    var RemoteControl_Type = Coalesce(A[815]); // Remote Control - Type
    var RemoteControl_RemoteControlModel = Coalesce(A[4957]); // Remote Control - Remote Control Model
    var RemoteControl_Features = Coalesce(A[820]); // Remote Control - Features
    // --815 - Remote Control - Type
    // --4957 Remote Control Model
    // --820 Features
    if(RemoteControl_Type.HasValue("%remote control")
    && RemoteControl_RemoteControlModel.HasValue()
    && RemoteControl_Features.HasValue() && RemoteControl_Features.Where("Multi-Code Remote").Count() == 1){
        RemoteControlDetails = $"It comes with a Multi-Code {RemoteControl_RemoteControlModel.FirstValue()} {RemoteControl_Type.FirstValue()}ler that lets you operate the TV in a convenient way";
    }
    // S19709747 - It comes with VIZIO XRT136 remote controller that lets you operate the TV in a convenient way
    else if(RemoteControl_Type.HasValue("%remote control")
        && RemoteControl_RemoteControlModel.HasValue()){
        RemoteControlDetails = $"It comes with {RemoteControl_RemoteControlModel.FirstValue()} {RemoteControl_Type.FirstValue()}ler that lets you operate the TV in a convenient way";
    }
    if(!String.IsNullOrEmpty(RemoteControlDetails)){
        Add($"RemoteControlDetails⸮{RemoteControlDetails}");
    }
}

// -- [FEATURE #12] - All
// -- Warranty information

//§§591714025317164870 "TVs END" "Serhii.O" 

//§§3364010221167289 "Laptops BEGIN" "Serhii.O" 

// ProcessorType - All
Hard_Drive_Type();
// Operating System - All
// Installed RAM - All
Laptop_Memory_Type();
Screen_Resolution();
Screen_Size();
// Graphics - All
Backlit_Keyboard();
// Wireless Connectivity - All
Battery_Life();
// Warranty - All

// --[FEATURE #1] - All
// --Processor type

// --[FEATURE #2]
// --Hard Drive Type
void Hard_Drive_Type(){
    var HardDriveType = "";
    var HardDrive_Capacity = Coalesce(A[76]); // Hard Drive - Capacity
    var HardDrive_SSDFormFactor = Coalesce(A[7705]); // Hard Drive - SSD Form Factor
    var HardDrive_Type = Coalesce(A[5780]); // Hard Drive - Type
    var HardDrive_SpindleSpeed = Coalesce(A[84]); // Hard Drive - Spindle Speed

    if(HardDrive_Capacity.HasValue()){
        if(HardDrive_Capacity.Units.First().Name.NotIn("GB", "TB")){
            HardDriveType = "";
        }
        else if(HardDrive_SSDFormFactor.HasValue("eMMC")){
            HardDriveType = HardDrive_Capacity.FirstValue() >= 32 ?
            // S21486289 - 128GB eMMC hard drive provides ample photo storage
            $"{HardDrive_Capacity.FirstValue()}{HardDrive_Capacity.Units.First().Name} eMMC hard drive provides ample photo storage" : // S16715349 - 16GB eMMC flash hard drive offers adequate internal storage space
            $"{HardDrive_Capacity.FirstValue()}{HardDrive_Capacity.Units.First().Name} eMMC flash hard drive offers adequate internal storage space";
        }
        else if(HardDrive_Type.HasValue("SSD")){
            HardDriveType = HardDrive_Capacity.FirstValue() >= 256 || HardDrive_Capacity.Units.First().Name.In("TB") ? 
            // S21549833 - 256GB SSD hard drive enables you to store thousands of files
            $"{HardDrive_Capacity.FirstValue()}{HardDrive_Capacity.Units.First().Name} SSD hard drive enables you to store thousands of files" :
            // S21549832 - 128GB SSD keeps your running programs active while your computer resumes from suspension in seconds
            $"{HardDrive_Capacity.FirstValue()}{HardDrive_Capacity.Units.First().Name} SSD keeps your running programs active while your computer resumes from suspension in seconds" ;
        }
        else if(HardDrive_Type.HasValue("HDD")){
            if(HardDrive_Capacity.Units.First().Name.In("TB")){
                HardDriveType = HardDrive_SpindleSpeed.HasValue() && HardDrive_SpindleSpeed.Units.First().Name.In("rpm") ? 
                // S21126352 - 1TB HDD SATA 5400rpm hard drive for storage
                $"{HardDrive_Capacity.FirstValue()}{HardDrive_Capacity.Units.First().Name} HDD SATA {HardDrive_SpindleSpeed.FirstValue()}{HardDrive_SpindleSpeed.Units.First().Name} hard drive for storage" :
                // --1TB HDD gives you tons of storage space
                $"{HardDrive_Capacity.FirstValue()}{HardDrive_Capacity.Units.First().Name} HDD gives you tons of storage space";
            }
            else{
                HardDriveType = HardDrive_SpindleSpeed.HasValue() && HardDrive_SpindleSpeed.Units.First().Name.In("rpm") ?
                // S4481330 - 160GB HDD storage drive with 5400rpm spindle speed provides adequate internal storage space
                $"{HardDrive_Capacity.FirstValue()}{HardDrive_Capacity.Units.First().Name} HDD storage drive with {HardDrive_SpindleSpeed.FirstValue()}{HardDrive_SpindleSpeed.Units.First().Name} spindle speed provides adequate internal storage space" :
                // --500GB HDD provides enough space to store all your documents and multimedia content
                $"{HardDrive_Capacity.FirstValue()}{HardDrive_Capacity.Units.First().Name} HDD provides enough space to store all your documents and multimedia content" ;
            }
        }
    }
    if(!String.IsNullOrEmpty(HardDriveType)){
            Add($"HardDriveType⸮{HardDriveType}");
    }
}

// --[FEATURE #3] - All
// --Computer Operating System

// --[FEATURE #4] - All
// --Installed RAM

// --[FEATURE #5]
// --Laptop Memory Type
void Laptop_Memory_Type(){
    var LaptopMemoryType = "";
    var RAM_Technology = Coalesce(A[56]); // RAM - Technology

    switch(RAM_Technology){
        // S18674103 - DDR4 RAM: With its higher bandwidth, everything from multi-tasking to playing games gets a performance boost
        case var a when a.HasValue("DDR4%"):
        LaptopMemoryType = "DDR4 RAM: With its higher bandwidth, everything from multi-tasking to playing games gets a performance boost";
        break;
        // S21500408 - DDR3L SDRAM memory technology for high-performance solution
        case var a when a.HasValue():
        LaptopMemoryType = $"{RAM_Technology.FirstValue()} memory technology for high-performance solution";
        break;
    }
    if(!String.IsNullOrEmpty(LaptopMemoryType)){
        Add($"LaptopMemoryType⸮{LaptopMemoryType}");
    }
}

// --[FEATURE #6]
// --Screen Resolution
void Screen_Resolution(){
    var ScreenResolution = "";
    var Display_DisplayResolutionAbbreviation = Coalesce(A[4803]); // Display - Display Resolution Abbreviation
    var Display_NativeResolution = Coalesce(A[149]); // Display - Native Resolution
    var Display_WidescreenDisplay = Coalesce(A[4804]); // Display - Widescreen Display

    if(Display_DisplayResolutionAbbreviation.HasValue()){
        var HD = string.Empty;
        switch(Display_DisplayResolutionAbbreviation){
            // S18674103 - Features Full HD 1920 x 1080 resolution for unbelievable pixel-by-pixel image performance
            case var ResolutionAbbreviation when ResolutionAbbreviation.HasValue("Full HD"):
            ScreenResolution = "Features Full HD 1920 x 1080 resolution for unbelievable pixel-by-pixel image performance";
            break;
            // S8099732 - HD+ 1600 x 900 display: Enjoy your photos, movies, and games with the crisp quality of 1.4 million pixels
            case var ResolutionAbbreviation when ResolutionAbbreviation.HasValue("HD+"):
            HD = Display_NativeResolution.HasValue() ? $"{Display_NativeResolution.FirstValue().ToLower()} " : "";
            ScreenResolution = $"HD+ {HD}display: enjoy your photos, movies, and games with the crisp quality of 1.4 million pixels" ;
            break;
            // S11318171 - High-definition 1366 x 768 display: Enjoy your entertainment in great quality and high-definition detail of 1 million pixels
            case var ResolutionAbbreviation when ResolutionAbbreviation.HasValue("HD"):
            HD = Display_NativeResolution.HasValue() ? $"{Display_NativeResolution.FirstValue().ToLower()} " : "";
            ScreenResolution = $"High-definition {HD}display: enjoy your entertainment in great quality and high-definition detail of 1 million pixels" ;
            break;
            // S20727310 - Stunning 2560 x 1440 resolution WQHD results in a much sharper image, with no distortion or stretching, all while maintaining the same 16:9 aspect ratio
            case var ResolutionAbbreviation when ResolutionAbbreviation.HasValue("WQHD"):
            ScreenResolution = "Stunning 2560 x 1440 resolution WQHD results in a much sharper image, with no distortion or stretching, all while maintaining the same 16:9 aspect ratio";
            break;
            // S5712728 - Widescreen 1080p+ / WUXGA resolution for outstanding image quality
            case var ResolutionAbbreviation when ResolutionAbbreviation.HasValue("WUXGA%"):
            ScreenResolution = $"Widescreen 1080p+ / {ResolutionAbbreviation.FirstValue()} resolution for outstanding image quality";
            break;
            // S2934789 - WXGA resolution (1280 x 800) for widescreen HD performance
            case var ResolutionAbbreviation when ResolutionAbbreviation.HasValue("WXGA"):
            ScreenResolution = "WXGA resolution (1280 x 800) for widescreen HD performance";
            break;
            // S15418865 - QHD+ 3200 x 1800 resolution gives you 5.7 million pixels for the ultimate screen performance with precise clarity and eye-popping details
            case var ResolutionAbbreviation when ResolutionAbbreviation.HasValue("QHD+"):
            HD = Display_NativeResolution.HasValue() ? $"{Display_NativeResolution.FirstValue().ToLower()} " : "";
            ScreenResolution = $"QHD+ {HD}resolution gives you 5.7 million pixels for the ultimate screen performance with precise clarity and eye-popping details" ;
            break;
            // S20398128 - Enjoy every little detail with 3840 x 2160 Ultra HD 4K resolution. That’s four times the resolution of Full HD
            case var ResolutionAbbreviation when ResolutionAbbreviation.HasValue("Ultra HD 4K"):
            ScreenResolution = "Enjoy every little detail with 3840 x 2160 Ultra HD 4K resolution. That’s four times the resolution of Full HD";
            break;
            // S17143994 - Presents pictures, presentations, movies and more in stunning Full HD plus resolution
            case var ResolutionAbbreviation when ResolutionAbbreviation.HasValue("Full HD Plus"):
            ScreenResolution = "Presents pictures, presentations, movies and more in stunning Full HD plus resolution";
            break;
            default:{
                if(Display_NativeResolution.HasValue()
                    && Display_NativeResolution.Values.ExtractNumbers().Last() > 720
                    && Display_NativeResolution.Values.ExtractNumbers().Last() == Display_NativeResolution.Values.ExtractNumbers().First()){
                    ScreenResolution = $"16:9 widescreen {Display_DisplayResolutionAbbreviation.FirstValue()} resolution perfect for watching movies, playing video games and getting on with office work";
                }
                // S17855952 - Widescreen 2880 x 1920 resolution for outstanding image quality
                else if(Display_WidescreenDisplay.HasValue("Yes") && Display_NativeResolution.HasValue()){
                    ScreenResolution = $"Widescreen {Display_NativeResolution.FirstValue()} resolution for outstanding image quality";
                }
            }
            break;
        }
    }
    if(!String.IsNullOrEmpty(ScreenResolution)){
        Add($"ScreenResolution⸮{ScreenResolution}");
    }
}

// --[FEATURE #7]
// --Screen Size (Inches)
void Screen_Size(){
    var ScreenSize = "";
    var Display_DiagonalSize = Coalesce(A[140]); // Display - Diagonal Size

    if(Display_DiagonalSize.HasValue()){
        switch(Display_DiagonalSize.FirstValue()){
            // S21351019 - 14 screen provides a great movie watching experience
            case var DiagonalSize when DiagonalSize > 13.0 
            && DiagonalSize < 16.0
            && DiagonalSize != 15.6:
            ScreenSize = $@"{DiagonalSize}"" screen provides a great movie watching experience";
            break;
            // S14117028 - Get comfortable with an expansive, 17.3 screen that makes streaming, scrolling, and multitasking easy and enjoyable
            case var DiagonalSize when DiagonalSize >= 16.0:
            ScreenSize = $@"Get comfortable with an expansive, {DiagonalSize}"" screen that makes streaming, scrolling, and multitasking easy and enjoyable";
            break;
            // S16468657 - Its 15.6 display offers 40% more viewable area than a 13.3 screen, so you have more room to spread out your work, and more space to enjoy your movie
            case var DiagonalSize when DiagonalSize == 15.6:
            ScreenSize = @"Its 15.6"" display offers 40% more viewable area than a 13.3"" screen, so you have more room to spread out your work, and more space to enjoy your movie";
            break;
            // S17855952 - 12.6 screen is large enough to watch movies and TV shows and small enough to carry easily
            case var DiagonalSize when DiagonalSize <= 13.0 && DiagonalSize > 0:
            ScreenSize = $@"{DiagonalSize}"" screen is large enough to watch movies and TV shows and small enough to carry easily";
            break;
        }
    }
    if(!String.IsNullOrEmpty(ScreenSize)){
        Add($"ScreenSize⸮{ScreenSize}");
    }
}

// --[FEATURE #8] - All
// --Graphics Card Brand

// --[FEATURE #9]
// --Backlit Keyboard
void Backlit_Keyboard(){
    var BacklitKeyboard = "";
    var InputDevice_Backlight = Coalesce(A[10622]); // Input Device - Backlight

    // S21481628 - A backlit keyboard lets you play day or night
    if(InputDevice_Backlight.HasValue()){
        BacklitKeyboard = "A backlit keyboard lets you play day or night";
    }
    if(!String.IsNullOrEmpty(BacklitKeyboard)){
        Add($"BacklitKeyboard⸮{BacklitKeyboard}");
    }
}

// --[FEATURE #10] - All
// --Wireless Connectivity

// --[FEATURE #11]
// --Battery Life (up to hours)
void Battery_Life(){
    var BatteryLife = "";
    var Battery_RunTime = Coalesce(A[341]); // Battery - Run Time (Up To)
    var Battery_Capacity = Coalesce(A[338]); // Battery - Capacity
    var Battery_Technology = Coalesce(A[332]); // Battery - Technology

    if(Battery_RunTime.HasValue()){
        if(Battery_RunTime.Units.First().Name.In("hours", "hour(s)")
        && (Battery_RunTime.FirstValue() >= 12 && Battery_RunTime.FirstValue() < 15)){
            BatteryLife = $"{Battery_RunTime.FirstValue()} hours of battery operation";
        }
        else if(Battery_RunTime.Units.First().Name.In("hours", "hour(s)")
        && (Battery_RunTime.FirstValue() >= 15 && Battery_RunTime.FirstValue() < 24)){
            BatteryLife = $"{Battery_RunTime.FirstValue()}-hour battery run time helps you stay unwired and work longer nonstop";
        }
        else if((Battery_RunTime.Units.First().Name.In("hours", "hour(s)")
        && Battery_RunTime.FirstValue() >= 24) ||
        (Battery_RunTime.Units.First().Name.In("day", "days")
        && Battery_RunTime.FirstValue() >= 1)){
            BatteryLife = "Liberating battery life: Take on the day without worrying about recharging";
        }
        else if(Battery_Capacity.HasValue()
            && Battery_Technology.HasValue("lithium ion")){
            BatteryLife = $"{Battery_Capacity.FirstValue()}{Battery_Capacity.Units.First().Name} lithium-ion battery with a run time of up to {Battery_RunTime.FirstValue()} {Battery_RunTime.Units.First().Name.Replace("(s)", "s")}";
        }
        else if(!Battery_Capacity.HasValue()
            && Battery_Technology.HasValue("lithium ion")){
            BatteryLife = $"Lithium-ion battery with a run time of up to {Battery_RunTime.FirstValue()} {Battery_RunTime.Units.First().Name.Replace("(s)", "s")}";
        }
        // S21481628 - 5400mAh lithium-polymer battery with a run time of up to 8 hours
        else if(Battery_Capacity.HasValue()
            && Battery_Technology.HasValue("lithium polymer")){
            BatteryLife = $"{Battery_Capacity.FirstValue()}{Battery_Capacity.Units.First().Name} lithium-polymer battery with a run time of up to {Battery_RunTime.FirstValue()} {Battery_RunTime.Units.First().Name.Replace("(s)", "s")}";
        }
        else if(!Battery_Capacity.HasValue()
            && Battery_Technology.HasValue("lithium polymer")){
            BatteryLife = $"Lithium-polymer battery with a run time of up to {Battery_RunTime.FirstValue()} {Battery_RunTime.Units.First().Name.Replace("(s)", "s")}";
        }
        else if(Battery_Capacity.HasValue()
            && Battery_Technology.HasValue()){
            BatteryLife = $"{Battery_Capacity.FirstValue()}{Battery_Capacity.Units.First().Name} {Battery_Technology.FirstValue()} battery with a run time of up to {Battery_RunTime.Units.First().Name.Replace("(s)", "s")}";
        }
        else if(!Battery_Capacity.HasValue()
            && Battery_Technology.HasValue()){
            BatteryLife = $"{Battery_Technology.FirstValue()} battery with a run time of up to {Battery_RunTime.FirstValue()} {Battery_RunTime.Units.First().Name.Replace("(s)", "s")}";
        }
    }
    if(!String.IsNullOrEmpty(BatteryLife)){
        Add($"BatteryLife⸮{BatteryLife}");
    }
}

// --[FEATURE #12] - All
// --Warranty *NEEDS TO BE LAST BULLET* If REFURBISHED, then “Warranty Length Non-mfg. warranty through Vendor. This product is NOT warrantied through Brand Name”

//§§3364010221167289 "Laptops END" "Serhii.O" 

//§§3364110222167288 "Desktop Computers BEGIN" "Serhii.O" 

// ProcessorType - All
// RAM Type - All
HardDrive_MemoryType_Capacity();
// Operating System - All
// Graphics - All
// WirelessConnectivity - All
Optical_Drive_Of_Desktop();
USB_Ports_Of_Desktop();
Additional_PortsInterface();
// Dimensions - All
Box_Contents();
// Warranty - All

// --[FEATURE #1] - All
// --Processor type

// --[FEATURE #2] - All
// --RAM type

// --[FEATURE #3]
// --Hard Drive Memory Type and Capacity
void HardDrive_MemoryType_Capacity(){
    var HardDriveMemoryTypeCapacityOfDesktop = "";
    var SSDCapacity = !R("SP-18331").Text.Equals("<NULL>") ? R("SP-18331").Text :
    !R("cnet_common_SP-18331").Text.Equals("<NULL>") ? R("cnet_common_SP-18331").Text : ""; // SSD Capacity - 200GB|180GB|1TB|250GB|900GB|240GB|300GB|800GB|600GB|256GB|96GB|4TB|3TB|2TB|1.6TB|700GB|960GB|480GB|400GB|512GB|500GB|160GB|4GB|16GB|36GB|32GB|30GB|525GB|64GB|60GB|90GB|450GB|100GB|80GB|128GB|120GB|146GB|8GB|5TB+
    var HDDCapacity = !R("SP-18431").Text.Equals("<NULL>") ? R("SP-18431").Text :
    !R("cnet_common_SP-18431").Text.Equals("<NULL>") ? R("cnet_common_SP-18431").Text : ""; // HDD Capacity - Under 320GB|512GB|1.2TB|320GB|5TB|4.5TB|640GB|450GB|1.5TB|1TB|750GB|6TB +|5.5TB|500GB|3.5TB|3TB|2.5TB|2TB|4TB

    if(!String.IsNullOrEmpty(HDDCapacity)
        && !String.IsNullOrEmpty(SSDCapacity)){
        HardDriveMemoryTypeCapacityOfDesktop = $"{HDDCapacity} HDD and {SSDCapacity} SDD hard drives enables you to store thousands of files";
    }
    else if(Coalesce(HDDCapacity).In("16GB", "32GB")){
        HardDriveMemoryTypeCapacityOfDesktop = $"{HDDCapacity} HDD hard drive for ample storage";
    }
    else if(Coalesce(SSDCapacity).In("16GB", "32GB")){
        HardDriveMemoryTypeCapacityOfDesktop = $"{SSDCapacity} SSD hard drive for ample storage";
    }
    else if(!String.IsNullOrEmpty(HDDCapacity)){
        HardDriveMemoryTypeCapacityOfDesktop = $"{HDDCapacity} HDD hard drive enables you to store thousands of files";
    }
    else if(!String.IsNullOrEmpty(SSDCapacity)){
        HardDriveMemoryTypeCapacityOfDesktop = $"{SSDCapacity} SSD hard drive enables you to store thousands of files";
    }
    if(!String.IsNullOrEmpty(HardDriveMemoryTypeCapacityOfDesktop)){
        Add($"HardDriveMemoryTypeCapacityOfDesktop⸮{HardDriveMemoryTypeCapacityOfDesktop}");
    }
}

// --[FEATURE #4] - All
// --Operating System

// --[FEATURE #5] - All
// --Graphics

// --[FEATURE #6] - All
// --Connectivity (wireless)

// --[FEATURE #7]
// --Desktop optical drive
void Optical_Drive_Of_Desktop(){
    var OpticalDriveOfDesktop = "";
    var DesktopOpticalDriveRef = !R("SP-3999").Text.Equals("<NULL>") ? R("SP-3999").Text :
    !R("cnet_common_SP-3999").Text.Equals("<NULL>") ? R("cnet_common_SP-3999").Text : ""; // Desktop Optical Drive

    switch(Coalesce(DesktopOpticalDriveRef)){
        case var a when a.In("%DVD%SuperMulti%"):
        OpticalDriveOfDesktop = "Super multi-format DVD drive for all your optical media and video recording needs; plays and burns DVDs and CDs";
        break;
        case var a when a.In("%DVD%WRITER%"):
        OpticalDriveOfDesktop = "With DVD-Writer instantly access your favorite music, movies, data, or other content on the discs";
        break;
        case var a when a.In("%DVD%"):
        OpticalDriveOfDesktop = "The built-in DVD drive lets you play DVDs for a richer entertainment experience";
        break;
    }
    if(!String.IsNullOrEmpty(OpticalDriveOfDesktop)){
        Add($"OpticalDriveOfDesktop⸮{OpticalDriveOfDesktop}");
    }
}

// --[FEATURE #8]
// --Desktop USB ports
void USB_Ports_Of_Desktop(){
    var USBPortsOfDesktop = "";
    var InterfaceProvided_Type = Coalesce(A[6216]); // Interface Provided - Type | (8965) - Interface Provided - Comment, (10) - Interface Provided - Qty

    if(InterfaceProvided_Type.HasValue()){
        var a = InterfaceProvided_Type.Where("USB%") // Find all with USB
        .Match(8965, 10).Values("&") // Match with Comment and Qty
        .Where(s => !s.HasValue("%charging%")) // Find all without charging
        .Select(s => s.RegexReplace(@".*&(\d*).*", "$1")) // Get only Qty
        .ExtractNumbers()
        .Sum();
        // S21058085 - Use 5 x USB 3.1 Gen 1, 2 x USB 2.0, 1 x USB-C 3.1 Gen 2 and 2 x USB 2.0 ports for lightning data movement speed
        if(a > 1){
            USBPortsOfDesktop = $"Use {InterfaceProvided_Type.Where("USB%").Match(10).Values(" x ").Where(s => !s.HasValue("%charging%")).Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "$2 x $1")).FlattenWithAnd()} ports for lightning data movement speed";
        }
        else if(a == 1){
            USBPortsOfDesktop = $"Use {InterfaceProvided_Type.Where("USB%").Match(10).Values(" x ").Where(s => !s.HasValue("%charging%")).Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "$2 x $1")).FlattenWithAnd()} port for lightning data movement speed";
        }
    }
    if(!String.IsNullOrEmpty(USBPortsOfDesktop)){
        Add($"USBPortsOfDesktop⸮{USBPortsOfDesktop}");
    }
}

// --[FEATURE #9]
// --Additional Ports/Interface (If Applicable)
void Additional_PortsInterface(){
    var AdditionalPortsInterface = ""; // Additional Ports/Interface
    var InterfaceProvided_Type = Coalesce(A[6216]); // Interface Provided - Type

    if(InterfaceProvided_Type.HasValue()
        && InterfaceProvided_Type.WhereNot("%input%").Where(d => d.Value().In("%VGA%", "%HDMI%", "%DisplayPort%")).Count() > 1){
        AdditionalPortsInterface = $"Built-in {InterfaceProvided_Type.WhereNot("%input%").Where(d => d.Value().In("%VGA%", "%HDMI%", "%DisplayPort%")).Select(c => c.Value().RegexReplace("(DisplayPort).*", "DisplayPort").Replace("output", "")).Distinct().FlattenWithAnd()} output to connect to TVs or multiple displays for stunning HD entertainment";
    }
    if(!String.IsNullOrEmpty(AdditionalPortsInterface)){
        Add($"AdditionalPortsInterface⸮{AdditionalPortsInterface}");
    }
}

// --[FEATURE #10] - All Features
// --Dimensions (in Inches): Height x Width x Depth

// --[FEATURE #11]
// --Detail any peripherals such as Keyboard, mouse, monitor, etc.
void Box_Contents(){
     var BoxContentsOfDesktop = "";
    if(SPEC["WIB"].GetLines().Where(l => l.Body.In("%keyboard%", "%mouse%")).Any()){
        BoxContentsOfDesktop = $"Includes {SPEC["WIB"].GetLines().Where(l => l.Body.In("%keyboard%", "%mouse%")).Select(l => l.Body.ToLower(true)).Flatten(',').Split(", ").FlattenWithAnd().Replace("(on selected markets only) ", "")} for immediate setup";
    }
    if(!String.IsNullOrEmpty(BoxContentsOfDesktop)){
        Add($"BoxContentsOfDesktop⸮{BoxContentsOfDesktop}");
    }
}

// --[FEATURE #12] - All Features
// --Warranty information

//§§3364110222167288 "Desktop Computers END" "Serhii.O" 

//§§12375510567216950 "MICR Ink for All Units BEGIN" "Serhii.O" 

Type_Of_Cartridges();
Color_Of_Cartridges();
// CartridgeYieldType - Remanufactured Laser Printer Ink
Page_Yield_Per_Package();
Ink_Or_Toner_Special_Type();
// Cartridge_Compatible - Remanufactured Laser Printer Ink
// Wide_Format_Compatibility();
Ink_Or_Toner_Pack_Size();

// --[FEATURE #1]
// --Type of cartridge & Use
// --MICR Black toner cartridge is designed to protect your important documents and offers high bank acceptance rates as well as printer compatibility
void Type_Of_Cartridges(){
    var result = "";
    // Ink Tank|Photo Ink|Waste Toner|Fax Cartridge|Waste Toner Bottle|Photo Developer Kit|Solid Ink|Fuser Kit|Transfer Kit|Waste Toner Box|Maintenance Kit|Photo Conductor Kit|Oil Bottle|Glossy Photo Print Pack|Refill Kit|Drum|MICR|Other|Printhead/Cleaner|Ink/Printhead/Cleaner|Ink|Usage kit|Imaging Unit|Toner|Printhead|Rolls
    var supplyTypeRef = R("SP-1035").HasValue() ? R("SP-1035").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-1035").HasValue() ? R("cnet_common_SP-1035").Replace("<NULL>", "").Text : "";

    if(supplyTypeRef.ToLower().Equals("micr")){
        result = "MICR toner cartridge is designed to protect your documents and offers high bank acceptance rates";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"TypeOfCartridges⸮{result}");
    }
}

// --[FEATURE #2]
// --Ink or toner True color
void Color_Of_Cartridges(){
    var result = "";
    // Photo Black|Black Matte|Pigment Black|Chroma Optimizer|Photo Blue|Magenta|Photo Cyan|Yellow|Black/Color|Photo Value Pack|Cyan|Photo Gray|Photo Ink|Dark Gray|Other|Color Combination|Light Grey|Blue|Clear|Light Magenta|Light Yellow|Black|Red|Orange|Gray|Green|Light Cyan|Photo Magenta|Color|Light Black|Not Applicable
    var inkOrTonerColorRef = R("SP-1036").HasValue() ? R("SP-1036").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-1036").HasValue() ? R("cnet_common_SP-1036").Replace("<NULL>", "").Text : "";

    if(inkOrTonerColorRef.ToLower().Equals("black")){
        result = "Black ink";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"ColorOfCartridges⸮{result}");
    }
}

// --[FEATURE #3] - CartridgeYieldType
// --Cartridge yield type
// Cartridge_Yield_Type - In §5226058, §12366910285216553, §1212164913166147, §5226157, §12393811005220973, §5226006, §5226054, §12378610641217427

// --[FEATURE #4]
// --Page yield per package; Up to
void Page_Yield_Per_Package(){
    var result = "";
    var ConsumablesIncluded_Type = Coalesce(A[2293]); // Consumables Included - Type
    var ConsumablesIncluded_Color = Coalesce(A[2294]); // Consumables Included - Color (A[2295] - Consumables Included - Included Qty)
    var ConsumablesIncluded_DutyCycle = Coalesce(A[2297]); // Consumables Included - Duty Cycle
    var Consumable_DutyCycleCoverage = Coalesce(A[3210]); // Consumable - Duty Cycle Coverage
    var Consumable_ConsumableType = Coalesce(A[4760]); // Consumable - Consumable Type
    var Consumable_DutyCycle = Coalesce(A[4785]); // Consumable - Duty Cycle

    if(Consumable_DutyCycle.HasValue()){
        // --Delivers excellent print quality, sharp images and text - up to a 3000-page yield
        if(Consumable_DutyCycle.HasValue("%page%")
        && Consumable_ConsumableType.HasValue("%cartridge", "ink tank", "solid inks", "ink tank / paper kit")){
            result = $"Delivers excellent print quality, sharp images and text up to a {Consumable_DutyCycle.FirstValue().ExtractNumbers().First()}-page yield";
        }
        else if(Consumable_DutyCycleCoverage.HasValue("at%coverage")
        && Consumable_ConsumableType.HasValue("%cartridge", "ink tank", "solid inks", "ink tank / paper kit")){
            result = $"This {Consumable_ConsumableType.FirstValue().Replace(" / paper kit", "")} delivers up to {Consumable_DutyCycle.FirstValue().ExtractNumbers().First()} pages at {Consumable_DutyCycleCoverage.FirstValue().ExtractNumbers().First()}% coverage";
        }
        // --Page yield footnote: Tested in HP deskjet 2050 all-in-one series - J510, approximate average based on ISO/IEC 24711 or HP testing methodology and continuous printing, actual yield varies considerably based on content of printed pages and other factors
        // --Approximate page yield based on ISO/IEC 24711 testing is 170 pages
        else if(Consumable_DutyCycleCoverage.HasValue("%ISO/IEC%") //--%ISO/IEC 
        && Consumable_ConsumableType.HasValue("%cartridge", "ink tank", "solid inks", "ink pack")){
            result = $"This {Consumable_ConsumableType.FirstValue()} delivers up to {Consumable_DutyCycle.FirstValue().ExtractNumbers().First()} pages based on {Consumable_DutyCycleCoverage.FirstValue()} testing methodology";
        }
    }
    // --Tested to provide the best image quality and most reliable printing you can count on page after page
    else if(Consumable_ConsumableType.HasValue("%cartridge", "ink tank", "solid inks", "ink tank / paper kit")){
        result = "Tested to provide the best image quality and most reliable printing you can count on page after page";
    }
    // --Color 2294
    // --DUTY 2297
    // --qty 2295
    // --Yields up to 2,300 pages black/ 700 pages each color
    // --Maximize your page yield - up to 300 pages cyan, 300 pages magenta, and 300 pages yellow.
    // --Yields up to 480 pages for black and 165 pages for tricolor
    else if(ConsumablesIncluded_DutyCycle.HasValue()){
        if(ConsumablesIncluded_DutyCycle.Match(2295, 2294).Values(" x ").Flatten().In("%1 x black%")
        && ConsumablesIncluded_Color.HasValue("color", "tricolor")){
            result = $"Yields up to {ConsumablesIncluded_DutyCycle.Match(2297, 2294).Values(" for ").Where(s => s.NotIn("%color%")).Flatten("; ")} and {ConsumablesIncluded_DutyCycle.Match(2297, 2294).Values(" for ").Where(s => s.In("%color%")).Flatten("; ")}";
        }
        else if(!ConsumablesIncluded_Color.HasValue("color", "tricolor")){
            result = ConsumablesIncluded_Type.HasValue("drum") ? $"Maximize your page yield - up to {ConsumablesIncluded_Type.WhereNot("drum").Match(2297, 2294).Values(" ").Where(s => s.NotIn("%color%")).Flatten(", ")} and {ConsumablesIncluded_Type.Where("drum").Match(2297).Values(" ").Where(s => s.NotIn("%color%")).Flatten(", ") + " drum"}" :
            $"Maximize your page yield - up to {ConsumablesIncluded_DutyCycle.Match(2297, 2294).Values(" ").Where(s => s.NotIn("%color%")).Flatten(", ")}";
        }
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"PageYieldPerPackage⸮{result}");
    }
}

// --[FEATURE #5]
// --Ink or toner special type
void Ink_Or_Toner_Special_Type(){
    var result = "";
    // Ink or Toner Special Type - Use and Return|Remanufactured
    var inkOrTonerSpecialTypeRef = R("SP-22914").HasValue() ? R("SP-22914").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22914").HasValue() ? R("cnet_common_SP-22914").Replace("<NULL>", "").Text : ""; 

    if(inkOrTonerSpecialTypeRef.ToLower().Equals("use and return")){
        result = "Use and Return toner cartridges are specially priced for customers to use once, and are not to be refilled or remanufactured";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"InkOrTonerSpecialType⸮{result}");
    }
}

// --[FEATURE #6] - Cartridge_Compatible
// --Ink & Toner compatibility

// --[FEATURE #7]
// --Wide format compatibility (If Applicable)
/*void Wide_Format_Compatibility(){
    var result = "";

    if(!String.IsNullOrEmpty(result)){
        Add($"WideFormatCompatibility⸮{result}");
    }
}*/

// --[FEATURE #8]
// --Ink or toner pack size
// --COPY_PASTE
// --Package contents (even if 1) including counts and color
// --Package includes one black toner cartridge for use with a compatible laser printer and fax machines
// --Four ink sticks are included in each package so you can enjoy a high-yield product
// --Dual pack includes 1 magenta printhead and 1 cyan printhead
// --Package includes one black toner cartridge for use with a compatible laser printer and fax machines
// --Get double the print volume at a lower price with 2 Original HP cartridges in one package.
void Ink_Or_Toner_Pack_Size(){
    var result = "";
    // Photo Black|Black Matte|Pigment Black|Chroma Optimizer|Photo Blue|Magenta|Photo Cyan|Yellow|Black/Color|Photo Value Pack|Cyan|Photo Gray|Photo Ink|Dark Gray|Other|Color Combination|Light Grey|Blue|Clear|Light Magenta|Light Yellow|Black|Red|Orange|Gray|Green|Light Cyan|Photo Magenta|Color|Light Black|Not Applicable
    var inkOrTonerColorRef = R("SP-1036").HasValue() ? R("SP-1036").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-1036").HasValue() ? R("cnet_common_SP-1036").Replace("<NULL>", "").Text : "";
    // 4/Pack|5/Pack|2/Pack|3/Pack|Photo Paper Value Pk|Business Value Pk|Other|6/Pack|1/Pack
    var inkOrTonerPackSizeRef = R("SP-2884").HasValue() ? R("SP-2884").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-2884").HasValue() ? R("cnet_common_SP-2884").Replace("<NULL>", "").Text : "";
    var consumable_Color = Coalesce(A[494]); // Consumable - Color
    var consumablesIncluded_Color = Coalesce(A[2294]); // Consumables Included - Color
    var consumable_ConsumableType = Coalesce(A[4760]); // Consumable - Consumable Type
    var consumable_PrintingTechnology = Coalesce(A[4764]); //Consumable - Printing Technology
    var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM")); //Filing System - Features

    if(inkOrTonerColorRef.ToLower().Equals("not applicable")){
        result = "Cartridge produces richer, deeper colors and delivers superior results for all your printing needs";
    }
    else if(inkOrTonerPackSizeRef.Equals("1/pack")
    && consumable_ConsumableType.HasValue("%cartridge", "ink tank", "ink pack")
    && consumable_PrintingTechnology.HasValue()
    && consumable_Color.HasValue() && consumable_Color.Values.Count == 1){
        result = $"Package includes one {consumable_Color.Values.Select(s => s.Value().RegexReplace(@"color \((.+)\)", "tricolor")).Flatten()} {consumable_ConsumableType.FirstValue()} for use with a compatible {consumable_PrintingTechnology.FirstValue()} printer";
    }
    // S11199185
    else if(inkOrTonerPackSizeRef.Equals("1/pack")
    && consumable_ConsumableType.HasValue("%cartridge", "ink pack")
    && consumable_PrintingTechnology.HasValue()
    && consumable_Color.HasValue() && consumable_Color.Values.Count == 3){
        result = $"Package includes one tricolor {consumable_ConsumableType.FirstValue()} for use with a compatible {consumable_PrintingTechnology.FirstValue()} printer";
    }
    else if(consumable_Color.HasValue() && consumable_Color.Values.Count == 2
    && consumable_Color.HasValue("black")
    && consumable_Color.HasValue("%tricolor%", "color (dye-based cyan, dye-based magenta, dye-based yellow)")){
        result = "Get double the print volume at a lower price with black and tricolor cartridges in one package";
    }
    else if(!inkOrTonerPackSizeRef.Equals("1/pack")
    && consumable_Color.HasValue() && consumable_Color.FirstValue().ToString().Length < 37){
        result = $"Convenient multi-pack containing individual color cartridges ({consumable_Color.Values.Select(s => s.Value().RegexReplace(@"color \((.+)\)", "tricolor")).FlattenWithAnd()})";
    }
    else if(!inkOrTonerPackSizeRef.Equals("1/pack")
    && consumablesIncluded_Color.HasValue() && consumablesIncluded_Color.FirstValue().ToString().Length < 37){
        result = $"Convenient multi-pack containing individual color cartridges ({consumablesIncluded_Color.Values.Select(s => s.Value().RegexReplace(@"color \((.+)\)", "tricolor")).FlattenWithAnd()})";
    }
    else if(!inkOrTonerPackSizeRef.Equals("1/pack")
    && consumable_Color.HasValue() && consumable_Color.FirstValue().ToString().Length > 36
    && TX_UOM.ExtractNumbers().Any() && TX_UOM.ExtractNumbers().First() == consumable_Color.Values.Count){
        result = $"Includes one {consumable_Color.Values.Select(s => s.Value().RegexReplace(@"color \((.+)\)", "tricolor")).FlattenWithAnd()} cartridge";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"InkOrTonerPackSize⸮{result}");
    }
}

//§§12375510567216950 "MICR Ink for All Units END" "Serhii.O" 

//§§5226058, §12366910285216553, §1212164913166147, §5226157, §12393811005220973, §5226006, §5226054, §12378610641217427 "Remanufactured Laser Printer Ink, Toner & Drum Units, InkJet Printer Ink, Toner & Drum Uni BEGIN" "Serhii.O" 

Cartridge_Yield_Type();
Cartridge_Package_Contents();
Cartridge_Vendor_Specific_Information();
Cartridge_SavingsMessage();
Cartridge_Compatible();
// Recycled Post Consumer Content - All
Cartridge_Media_Included();
Cartridge_Features();
Cartridge_Remanufactured();

// --1.Cartridge yield type, per Cartridge. If cartridge yield type doesn't apply, use generic messaging 'Maximize you page yield while you save
// --2.Package contents (even if 1) including counts and color
// --3.Vendor specific information/tagline
// --4.If cartridge is twin pack or combo pack, bullet should be a savings message    
// --5.Use for additional product and/or manufacturer information relevant to the customer buying decision

// --[FEATURE #1]
// --LEFT: Not Applicable
// --Cartridge yield type, per Cartridge. If cartridge yield type doesn't apply, use generic messaging 'Maximize you page yield while you save' 
// --High yield produces the maximum number or prints
// --Save time and money with this high-yield toner cartridge compared to standard one
// --Innobella™ high yield (XL Series) magenta ink cartridge
// --1.Cartridge yield type, per Cartridge. If cartridge yield type doesn't apply, use generic messaging 'Maximize you page yield while you save
// --2.Package contents (even if 1) including counts and color
// --3.Vendor specific information/tagline
// --4.If cartridge is twin pack or combo pack, bullet should be a savings message    
// --5.Use for additional product and/or manufacturer information relevant to the customer buying decision
void Cartridge_Yield_Type(){
    var result = "";
    // Not Applicable|Mega|Ultra High Yield|Super High Yield|Economy|Standard|High Yield|High Yield/Standard|Extra High Yield|Other
    var CartridgeYieldTypeRef = R("SP-1037").HasValue() ? R("SP-1037").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-1037").HasValue() ? R("cnet_common_SP-1037").Replace("<NULL>", "").Text : "";
    var SupplyType = R("SP-1035").HasValue() ? R("SP-1035").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-1035").HasValue() ? R("cnet_common_SP-1035").Replace("<NULL>", "").Text : ""; // Supply Type - Ink Tank|Photo Ink|Waste Toner|Fax Cartridge|Waste Toner Bottle|Photo Developer Kit|Solid Ink|Fuser Kit|Transfer Kit|Waste Toner Box|Maintenance Kit|Photo Conductor Kit|Oil Bottle|Glossy Photo Print Pack|Refill Kit|Drum|MICR|Other|Printhead/Cleaner|Ink/Printhead/Cleaner|Ink|Usage kit|Imaging Unit|Toner|Printhead|Rolls
    var PageYieldPerPackage = R("SP-1041").HasValue() ? R("SP-1041").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-1041").HasValue() ? R("cnet_common_SP-1041").Replace("<NULL>", "").Text : ""; // Page Yield Per Package (Up to)
    var InkOrTonerPackSize = R("SP-2884").HasValue() ? R("SP-2884").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-2884").HasValue() ? R("cnet_common_SP-2884").Replace("<NULL>", "").Text : ""; // Ink or Toner Pack Size - 4/Pack|5/Pack|2/Pack|3/Pack|Photo Paper Value Pk|Business Value Pk|Other|6/Pack|1/Pack
    var Consumable_CartridgeYield = Coalesce(A[3531]); // Consumable - Cartridge Yield
    var Consumable_ConsumableType = Coalesce(A[4760]); // Consumable - Consumable Type
    var Header_PackagedQuantity = Coalesce(A[694]); // Header - Packaged Quantity

    if(!String.IsNullOrEmpty(CartridgeYieldTypeRef)){
        if(!CartridgeYieldTypeRef.Equals("Other")
        && !String.IsNullOrEmpty(SupplyType)
        && !String.IsNullOrEmpty(PageYieldPerPackage)){
            result = SupplyType.Equals("Ink") ? 
            $"{CartridgeYieldTypeRef} ink cartridge, up to {PageYieldPerPackage} pages" :
            $"{CartridgeYieldTypeRef} {SupplyType}, up to {PageYieldPerPackage} pages";
        }
        else if(CartridgeYieldTypeRef.Equals("High Yield")){
            if(Consumable_CartridgeYield.HasValue("XL")
            && Consumable_ConsumableType.HasValue("%cartridge", "ink tank")){
                result = !String.IsNullOrEmpty(InkOrTonerPackSize) && !InkOrTonerPackSize.Equals("1/Pack") && !InkOrTonerPackSize.Equals("Other") ?
                $"Save money when you buy these high-yield (XL Series) {Consumable_ConsumableType.FirstValue().Pluralize().ToString().Split(" ").Last()} instead of the standard ones" :
                $"Save time and money with this high-yield (XL Series) {Consumable_ConsumableType.FirstValue().ToString().Split(" ").Last()} compared to standard ones";
            }
            else if(Consumable_ConsumableType.HasValue("%cartridge", "ink tank", "ink tank / paper kit", "ink pack")){
                result = !String.IsNullOrEmpty(InkOrTonerPackSize) && !InkOrTonerPackSize.Equals("1/Pack") && !InkOrTonerPackSize.Equals("Other") ?
                $"Save time and money with this high-yield {Consumable_ConsumableType.FirstValue().Replace(" / paper kit", "").Pluralize()} compared to standard ones" :
                $"Save time and money with this high-yield {Consumable_ConsumableType.FirstValue().Replace(" / paper kit", "")} compared to standard ones";
            }
        }
        else if(CartridgeYieldTypeRef.Equals("Extra High Yield")
            && Consumable_ConsumableType.HasValue("%cartridge", "ink tank")){
            // --Designed for frequent printing, these high-capacity ink cartridges yield more pages and need fewer replacements than standard HP ink cartridges
            result = $"Designed for frequent printing, this {Consumable_ConsumableType.FirstValue()} yields more pages and needs fewer replacements";
        }
        else if(CartridgeYieldTypeRef.Equals("High Yield/Standard")
            && Consumable_ConsumableType.HasValue()){
            // --Combo pack includes: one black 786XL high-yield cartridge and one each of 786 cyan, magenta and yellow standard yield cartridges
            // --This HP 902XL/902 ink cartridge 4-pack provides all the ink you need to print, so you can create stunning prints at 
            result = $"This {Consumable_ConsumableType.FirstValue().RegexReplace(".+cartridge", "cartridge").RegexReplace(".+tank", "tank")} provides all the ink you need to print, so you can create stunning prints at home";
        }
        else if(CartridgeYieldTypeRef.Equals("Standard")
            && Consumable_ConsumableType.HasValue("%cartridge", "ink tank")){
            // --Get outstanding yield capacity per cartridge while enjoying cost savings
            result = $"Get standard-yield capacity per {Consumable_ConsumableType.FirstValue()} while enjoying cost savings";
        }
        else if(CartridgeYieldTypeRef.Equals("Economy")){
            // --Economy-sized and priced for occasional printing
            result = "Economy-sized and priced for occasional printing";
        }
        else if(CartridgeYieldTypeRef.Equals("Other")
            && (Consumable_CartridgeYield.HasValue("New Max Yield", "Extra High Capacity", "Ultra High Capacity", "Large Capacity", "Ultra High Capacity", "XXL size", "Double Extra Large", "Ultra High Yield")
                || Coalesce(DC.KSP.GetString()).In("Ultra high yields for affordable, high-volume printing"))){
            // --Change toner cartridges less often and minimize interruptions. High-capacity cartridges are designed for frequent printing.
            var ConsumableType = Consumable_ConsumableType.HasValue() ? $" {Consumable_ConsumableType.FirstValue()}" : "";
            result = $"{Consumable_CartridgeYield.FirstValue()}{ConsumableType}s are designed for frequent printing";
        }
    }
    else if(!String.IsNullOrEmpty(PageYieldPerPackage)){
        result = $"Yields up to {PageYieldPerPackage} pages";
    }
    else if(Header_PackagedQuantity.HasValue("1")
        && Coalesce(SKU.Brand).HasValue()
        && Coalesce(SKU.ModelName).HasValue()){
        // --Standard
        if(SKU.Brand.In("HP Inc.")){
            switch(SKU.ModelName){
                case var a when a.In("%A") && a.NotIn("%/%") && Consumable_ConsumableType.HasValue("%cartridge","ink tank"):
                result = $"Get standard-yield capacity per {Consumable_ConsumableType.FirstValue()} while enjoying cost savings";
                break;
                case var a when a.In("%X") && a.NotIn("%/%"):
                result = $"High-yield{" " + Consumable_ConsumableType.FirstValue()}s are designed for frequent printing";
                break;
                case var a when a.In("%Y") && a.NotIn("%/%"):
                result = $"Extra high-yield{" " + Consumable_ConsumableType.FirstValue()}s are designed for frequent printing";
                break;
            }
        }
        else if(SKU.Brand.In("Canon")){
            switch(SKU.ModelName){
                case var a when a.In("%XL") && a.NotIn("%/%"):
                result = $"High-yield{" " + Consumable_ConsumableType.FirstValue()}s are designed for frequent printing";
                break;
                case var a when a.In("%XXL") && a.NotIn("%/%"):
                result = $"Extra high-yield{" " + Consumable_ConsumableType.FirstValue()}s are designed for frequent printing";
                break;
                case var a when a.In("%H") && a.NotIn("%/%"):
                result = $"Extra high-yield{" " + Consumable_ConsumableType.FirstValue()}s are designed for frequent printing";
                break;
            }
        }
        else if(SKU.Brand.In("Epson")){
            if(SKU.ModelName.In("%XL")
            && SKU.ModelName.NotIn("%/%")){
                result = $"High-yield{" " + Consumable_ConsumableType.FirstValue()}s are designed for frequent printing";
            }
        }
    }
    else result = "Maximize your page yield while you save";
    if(!String.IsNullOrEmpty(result)){
        Add($"CartridgeYieldType⸮{result}");
    }
}

// --[FEATURE #2]
// --Package contents (even if 1) including counts and True color
// --Package includes one black toner cartridge for use with a compatible laser printer and fax machines
// --Four ink sticks are included in each package so you can enjoy a high-yield product
// --Dual pack includes 1 magenta printhead and 1 cyan printhead
// --Package includes one black toner cartridge for use with a compatible laser printer and fax machines
// --Get double the print volume at a lower price with 2 Original HP cartridges in one package.
// --1/Pack|2/Pack|3/Pack|4/Pack|5/Pack|Photo Paper Value Pk|Business Value Pk|Other
void Cartridge_Package_Contents(){
    var CartridgePackageContents = "";
    var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM"));
    var InkOrTonerColor = R("SP-1036").Text; // Ink or Toner Color - Photo Black|Black Matte|Pigment Black|Chroma Optimizer|Photo Blue|Magenta|Photo Cyan|Yellow|Black/Color|Photo Value Pack|Cyan|Photo Gray|Photo Ink|Dark Gray|Other|Color Combination|Light Grey|Blue|Clear|Light Magenta|Light Yellow|Black|Red|Orange|Gray|Green|Light Cyan|Photo Magenta|Color|Light Black|Not Applicable
    var SupplyType = R("SP-1035").Text; // Supply Type - Ink Tank|Photo Ink|Waste Toner|Fax Cartridge|Waste Toner Bottle|Photo Developer Kit|Solid Ink|Fuser Kit|Transfer Kit|Waste Toner Box|Maintenance Kit|Photo Conductor Kit|Oil Bottle|Glossy Photo Print Pack|Refill Kit|Drum|MICR|Other|Printhead/Cleaner|Ink/Printhead/Cleaner|Ink|Usage kit|Imaging Unit|Toner|Printhead|Rolls
    var InkOrTonerPackSize = R("SP-2884").Text; // Ink or Toner Pack Size - 4/Pack|5/Pack|2/Pack|3/Pack|Photo Paper Value Pk|Business Value Pk|Other|6/Pack|1/Pack
    var Consumable_ConsumableType = Coalesce(A[4760]); // Consumable - Consumable Type
    var Consumable_PrintingTechnology = Coalesce(A[4764]); // Consumable - Printing Technology
    var Consumable_Color = Coalesce(A[494]); // Consumable - Color
    var ConsumablesIncluded_Color = Coalesce(A[2294]); // Consumables Included - Color
    var ConsumablesIncluded_Type = Coalesce(A[2293]); // Consumables Included - Type
    var General_Technology = Coalesce(A[4782]); // General - Technology

    if(TX_UOM.HasValue() 
        && TX_UOM.ExtractNumbers().Any()
        && TX_UOM.ExtractNumbers().First() > 0
        && !String.IsNullOrEmpty(InkOrTonerColor) && Coalesce(InkOrTonerColor).NotIn("Black/Color", "Color Combination", "Photo Ink", "Other", "Not Applicable")){
        switch(Coalesce(SupplyType)){
            case var a when a.HasValue() && a.In("Ink"):
            CartridgePackageContents = $"Includes {TX_UOM.ExtractNumbers().First()} {InkOrTonerColor} ink cartridges";
            break;
            case var a when a.HasValue() && a.NotIn("Other"):
            CartridgePackageContents = $"The package includes {TX_UOM.ExtractNumbers().First()} {InkOrTonerColor.ToLower()} {SupplyType.ToLower()}s";
            break;
        }
    }
    else if(InkOrTonerColor.Equals("Not Applicable")){
        CartridgePackageContents = "Cartridge produces richer, deeper colors and delivers superior results for all your printing needs";
    }
    else if(InkOrTonerPackSize.Equals("1/pack")){
        if(Consumable_PrintingTechnology.HasValue() //--technology
            && Consumable_Color.HasValue()){
            switch(Consumable_Color.Values.Count){ //--color
                case var a when a == 1 && Consumable_ConsumableType.HasValue("%cartridge","ink tank", "ink pack"):
                CartridgePackageContents = $"The package includes one {Consumable_Color.Values.Select(s => s.Value().RegexReplace(@"color \((.+)\)", "tricolor")).Flatten()} {Consumable_ConsumableType.FirstValue()}";
                break;
                case var a when a == 3 && Consumable_ConsumableType.HasValue("%cartridge","ink tank"):
                CartridgePackageContents = $"The package includes one tricolor {Consumable_ConsumableType.FirstValue()}";
                break;
            }
        }
    }
    else if(Consumable_Color.HasValue() //--Includes: Black and tricolor print cartridge
        && Consumable_Color.Values.Count == 2
        && Consumable_ConsumableType.HasValue("%cartridge","ink tank")
        && Consumable_Color.HasValue("black", "%tricolor%", "color (dye-based cyan, dye-based magenta, dye-based yellow)")){
        CartridgePackageContents = "Get double the print volume at a lower price with black and tricolor cartridges in one package";
    }
    else if(!InkOrTonerPackSize.Equals("1/pack")){
        if(!InkOrTonerPackSize.Equals("Other") 
            && Consumable_Color.HasValue()
            && Consumable_ConsumableType.HasValue("%cartridge","ink tank")){
            var Colors = Consumable_Color.Values.Select(s => s.Value()).FlattenWithAnd().RegexReplace(@"color \((.+)\)", "tricolor");
            if(Colors.ToString().Length < 37){
                CartridgePackageContents = $"This convenient multipack contains individual color cartridges ({Colors})";
            }
            if(Consumable_Color.Values.Count > 1 && Colors.ToString().Length < 69){
                CartridgePackageContents = $"This package includes {Colors} cartridges";
            }
        } //--Colors of print cartridges: Matte black, photo black, cyan, magenta, yellow, gray
        else if(Consumable_Color.HasValue()
            && Consumable_ConsumableType.HasValue("%cartridge","ink tank")
            && TX_UOM.HasValue()
            && TX_UOM.ExtractNumbers().Any()){
            var Colors = Consumable_Color.Values.Flatten();
            var ColorsCount = Consumable_Color.Values.Count;
            if(Colors.ToString().Length > 36
                && ColorsCount == TX_UOM.ExtractNumbers().First()){
                CartridgePackageContents = $"Includes {Consumable_Color.Values.Select(s => s.Value()).FlattenWithAnd()} cartridges";
            }
            else if(Colors.ToString().Length > 36
                && ColorsCount == TX_UOM.ExtractNumbers().First()){
                CartridgePackageContents = $"Includes {Consumable_Color.Values.Select(s => s.Value()).FlattenWithAnd().RegexReplace(@"color \((.+)\)", "tricolor")} cartridges";
            }
            // ELSE IF $SP-2884$ NOT IN ("1/pack") 
            // AND A[4760].Value IN ("%cartridge","ink tank")
            // AND A[494].Values.Length >36
            // AND A[494].Values.Count LIKE Request.Data["TX_UOM"].ExtractDecimals().First()
            // THEN "Includes "_A[494].Values.FlattenWithAnd()_" cartridges"  
        }
        else if(ConsumablesIncluded_Color.HasValue()
            && Consumable_ConsumableType.HasValue("%cartridge","ink tank")){
            var Colors = ConsumablesIncluded_Color.Values.Select(s => s.Value()).FlattenWithAnd().RegexReplace(@"color \((.+)\)", "tricolor");
            if(Colors.ToString().Length < 37){
                CartridgePackageContents = $"This convenient multipack contains individual color cartridges ({Colors})";
            }
        }
    } //--This package includes cyan, magenta and yellow ink cartridges
    else if(Consumable_ConsumableType.HasValue("%with drum unit kit")
        && ConsumablesIncluded_Type.Where("toner cartridge").Match(2295).HasValue()){
        var tonerCartridge = ConsumablesIncluded_Type.Where("toner cartridge").Match(2295).Values().First() + " ";
        var tonerCartridgePlur = ConsumablesIncluded_Type.Where("toner cartridge").Select(s => s.Value()).First().Pluralize();
        var drumMatch = ConsumablesIncluded_Type.Where("drum").Match(2295).HasValue() ? " and " + ConsumablesIncluded_Type.Where("drum").Match(2295).Values().First() + " " : "";
        var drum = ConsumablesIncluded_Type.HasValue("drum") ? ConsumablesIncluded_Type.Where("drum").First() + " cartridge" : "";
        CartridgePackageContents = $"This package includes {tonerCartridge}{tonerCartridgePlur}{drumMatch}{drum}";
    } //--This package includes two toner cartridges and one drum cartridge
    else if(TX_UOM.HasValue()){ //-- TO AVOID This package includes six print cartridge / paper kits
        if(!TX_UOM.HasValue("Each")){
            if(TX_UOM.ExtractNumbers().Any()){
                if(Consumable_ConsumableType.HasValue()){
                    switch(Consumable_ConsumableType){
                        case var a when a.HasValue("%/ paper kit%"):
                        CartridgePackageContents = $"This package includes {TX_UOM.ExtractNumbers().First()} {Consumable_ConsumableType.FirstValue().Replace(" / paper kit", "").Pluralize() + " and paper kit"}";
                        break;
                        case var a when a.HasValue("%/ waste bag kit%"):
                        CartridgePackageContents = $"This package includes {TX_UOM.ExtractNumbers().First()} {Consumable_ConsumableType.FirstValue().Replace(" / ","/").Replace("/waste bag kit", "").Pluralize() + " and waste bag kit"}";
                        break;
                    }
                }
                else if(Consumable_ConsumableType.HasValue()){
                    CartridgePackageContents = $"This package includes {TX_UOM.ExtractNumbers().First()} {Consumable_ConsumableType.FirstValue().Pluralize()}";
                }
            }
        }
        else if(TX_UOM.HasValue("Each")){
            if(Consumable_ConsumableType.HasValue()){
                var technology = General_Technology.HasValue() ? General_Technology.FirstValue().Replace(" / ","/") + " " : "";
                switch(Consumable_ConsumableType){
                    case var a when a.HasValue("%/ paper kit%"):
                    CartridgePackageContents = $"This package includes one {technology}{Consumable_ConsumableType.FirstValue().Replace(" / ","/").Replace("/paper kit", "") + " and paper kit"}";
                    break;
                    case var a when a.HasValue("%/ waste bag kit%"):
                    CartridgePackageContents = $"This package includes one {technology}{Consumable_ConsumableType.FirstValue().Replace(" / ","/").Replace("/waste bag kit", "") + " and waste bag kit"}";
                    break;
                }
            }
            else if(Consumable_ConsumableType.HasValue()
                && General_Technology.HasValue()){
                CartridgePackageContents = $"This package includes one {General_Technology.FirstValue().Replace(" / ","/") + " "}{Consumable_ConsumableType.FirstValue().Replace(" / ","/")}";
            }
        }
    }
    if(!String.IsNullOrEmpty(CartridgePackageContents)){
        Add($"CartridgePackageContents⸮{CartridgePackageContents}");
    }
}

// --[FEATURE #3]
// --Vendor specific information/tagline
// --Clover Remanufactured Black Toner Cartridge, OKI B4400/B4600 (CTGB4600) offers vibrant print quality, reliability and performance for your photos and documents.
void Cartridge_Vendor_Specific_Information(){
    var VendorSpecificInformation = "";
    var CartridgeYieldTypeRef = R("SP-1037").Text; // Cartridge Yield Type - Not Applicable|Mega|Ultra High Yield|Super High Yield|Economy|Standard|High Yield|High Yield/Standard|Extra High Yield|Other
    var InkOrTonerPackSize = R("SP-2884").Text; // Ink or Toner Pack Size - 4/Pack|5/Pack|2/Pack|3/Pack|Photo Paper Value Pk|Business Value Pk|Other|6/Pack|1/Pack
    var InkOrTonerCartridgeType = R("SP-2886").Text; // Ink or Toner Cartridge Type - Original|Remanufactured|Compatible|Refilled
    var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM"));
    var Consumable_ConsumableType = Coalesce(A[4760]); // Consumable - Consumable Type
    var Consumable_CartridgeFeatures = Coalesce(A[4418]); // Consumable - Cartridge Features
    var Brand = Coalesce(SKU.Brand);
    var Manufacturer = Coalesce(SKU.ManufacturerName);

    if(Brand.HasValue("Samsung")){
        VendorSpecificInformation = "Maximize printing performance with genuine Samsung supplies";
    }
    else if(Manufacturer.HasValue()){
        switch(Manufacturer){
            case var a when a.HasValue("Clover Technologies"):
            //--Clover Remanufactured Black Toner Cartridge, OKI B4400/B4600 (CTGB4600) offers vibrant print quality, reliability and performance for your photos and documents.
            VendorSpecificInformation = "Clover offers vibrant print quality, reliability and performance for your photos and documents";
            break;
            case var a when a.HasValue("Lexmark")
            && Consumable_ConsumableType.HasValue("toner cartridge", "printer imaging unit"):
            // --Trust genuine Lexmark W850H21G Toner Cartridge to provide outstanding print quality for all your important photos and documents
            // --Original Lexmark 80C1SY0 toner cartridges are specifically designed to deliver professional quality results every time you print
            VendorSpecificInformation = $"Lexmark {Consumable_ConsumableType.FirstValue().Replace("printer imaging unit", "imaging unit").Pluralize().ToTitleCase()} provide outstanding print quality for all your documents";
            break;
            case var a when a.HasValue("Sustainable Earth by Staples")
            && InkOrTonerCartridgeType.Equals("Remanufactured"):
            VendorSpecificInformation = "Environmentally responsible remanufacturing and end of life process";
            break;
            case var a when a.HasValue("Staples")
            && Consumable_ConsumableType.HasValue("toner cartridge")
            && InkOrTonerCartridgeType.Equals("Remanufactured")
            && TX_UOM.NotIn("Each"):
            // --Staples remanufactured ink cartridge is designed to meet standards of the original manufacturer's equipment (OEM - Canon PG-210XL)
            VendorSpecificInformation = "Remanufactured cartridges meet standards of the original manufacturer's equipment";
            break;
            case var a when a.HasValue("Staples")
            && Consumable_ConsumableType.HasValue("toner cartridge")
            && InkOrTonerCartridgeType.Equals("Remanufactured"):
            VendorSpecificInformation = "Remanufactured cartridge meets standards of the original manufacturer's equipment";
            break;
            case var a when a.HasValue("Xerox")
            && Consumable_ConsumableType.HasValue("%cartridge", "ink tank", "solid inks")
            && !CartridgeYieldTypeRef.Equals("Other")
            && TX_UOM.NotIn("Each"):
            // --Xerox high-yield printer cartridges from Staples offer excellent value and reliability
            VendorSpecificInformation = $"Xerox {CartridgeYieldTypeRef}{" " + Consumable_ConsumableType.FirstValue().Replace("solid inks", "solid ink").Pluralize()} offer excellent value and reliability";
            break;
            case var a when a.HasValue("Xerox")
            && Consumable_ConsumableType.HasValue("%cartridge", "ink tank", "solid inks")
            && !CartridgeYieldTypeRef.Equals("Other"):
            VendorSpecificInformation = $"Xerox {CartridgeYieldTypeRef}{" " + Consumable_ConsumableType.FirstValue().Replace("solid inks", "solid ink")}s offer excellent value and reliability";
            break;
            case var a when a.HasValue("HP Inc.")
            && TX_UOM.NotIn("Each"):
            // --The unmatched reliability of original HP ink cartridges means consistent convenience and better value
            VendorSpecificInformation = $"The unmatched reliability of {InkOrTonerCartridgeType}{" " + Brand + " "}{Consumable_ConsumableType.FirstValue().Replace(" / paper kit", "").Replace("toner cartridge", "cartridge").Replace("print cartridge", "cartridge").Pluralize()} means consistent convenience and better value";
            break;
            case var a when a.HasValue("Brother")
            && Consumable_ConsumableType.HasValue("%cartridge"):
              // --Brother cartridges are designed for high performance, reliable and fast work
            // --Unlike bargain inks, original Brother LC75CL Ink Cartridges are products of a deep commitment to research and development which produces richer colors
            // --Brother genuine ink is the result of extensive research and development of over 100 different elements
            // --Brothers Technology is the guarantee of continuously accurate printing in any situation
            VendorSpecificInformation = "Brother's Technology is the guarantee of continuously accurate printing in any situation";
            break;
            case var a when a.HasValue("Canon")
            && Consumable_ConsumableType.HasValue("%cartridge","ink tank", "ink tank / paper kit")
            && !String.IsNullOrEmpty(InkOrTonerCartridgeType):
            // --Original Canon 131 Toner Cartridges deliver long-lasting quality to all your photos and documents
            VendorSpecificInformation = $"{InkOrTonerCartridgeType} Canon {Consumable_ConsumableType.FirstValue().Replace(" / paper kit", "").ToTitleCase()}s deliver long-lasting quality to all your photos and documents";
            break;
            case var a when a.HasValue("Canon")
            && Consumable_ConsumableType.HasValue("%cartridge","ink tank", "ink tank / paper kit"):
            VendorSpecificInformation = $"Canon {Consumable_ConsumableType.FirstValue().Replace(" / paper kit", "").ToTitleCase()}s deliver long-lasting quality to all your photos and documents";
            break;
            case var a when a.HasValue("Epson")
            && Consumable_ConsumableType.HasValue("%cartridge")
            && Consumable_CartridgeFeatures.HasValue("Epson DURABrite Ultra"):
            // --Epsons revolutionary DURABrite Ultra Ink produces smudge, fade and water resistant prints that look brilliant on both plain and glossy photo paper
            VendorSpecificInformation = "Epson's DURABrite Ultra Ink produces smudge-, fade-, and water-resistant prints that look brilliant";
            break;
            case var a when a.HasValue("Epson")
            && Consumable_ConsumableType.HasValue("%cartridge", "ink pack", "ink tank", "solid inks", "print cartridge / paper kit"):
            // --Epson 200 Ink Cartridges are specially formulated to deliver photographic-quality prints with true-to-life color
            VendorSpecificInformation = $"Epson {Consumable_ConsumableType.FirstValue().Replace("solid inks", "solid ink").Replace("print cartridge / paper kit", "cartridge").Replace("ink optimizer cartridge", "ink cartridge")}s are formulated to deliver photographic-quality prints with true-to-life color";
            break;
            case var a when a.HasValue("Dell")
            && Consumable_ConsumableType.HasValue("%cartridge"):
            // -- By purchasing Dell NF555 you save time and total cost of printing due to reliability and consistently brilliant results
            VendorSpecificInformation = $"Dell {Consumable_ConsumableType.FirstValue().Replace(" cartridge", "").ToUpperFirstChar()} cartridges save time and total cost of printing";
            break;
            case var a when a.HasValue("Dell")
            && Consumable_ConsumableType.HasValue("%drum%"):
            VendorSpecificInformation = $"Dell {Consumable_ConsumableType.FirstValue().ToUpperFirstChar()}s save time and total cost of printing due to reliability and brilliant results";
            break;
            case var a when a.HasValue("Lexmark")
            && Consumable_ConsumableType.HasValue("%cartridge")
            && !InkOrTonerPackSize.Equals("1/Pack"):
            VendorSpecificInformation = "This Lexmark cartridges deliver excellent print quality from the first page to the last";
            break;
            case var a when a.HasValue("Lexmark")
            && Consumable_ConsumableType.HasValue("%cartridge")
            && InkOrTonerPackSize.Equals("1/Pack"):
            VendorSpecificInformation = "This Lexmark cartridge delivers excellent print quality from the first page to the last";
            break;
            case var a when a.HasValue("Sharp")
            && Consumable_ConsumableType.HasValue("developer kit"):
            // --Sharp Toner/developer cartridge in black color is designed to meet the highest standards of quality, reliability and exceptional yields that meet or exceed OEM.
            VendorSpecificInformation = "Sharp developer kit is designed to meet the highest standards of quality";
            break;
            case var a when a.HasValue()
            && Consumable_ConsumableType.HasValue("%cartridge"):
            // --This Dell toner cartridge delivers high-quality cyan toner to pages, producing sharply defined text and images
            VendorSpecificInformation = $"This {Manufacturer.Text.Split(" ").Select(s => s.Replace("Kyocera","KYOCERA")).Flatten(" ")} {Consumable_ConsumableType.FirstValue().Replace(" cartridge", "")} cartridge delivers high-quality printing";
            break;
            case var a when a.HasValue()
            && Consumable_ConsumableType.HasValue("toner cartridge / waste toner collector", "waste toner collector"):
              // --EXPECTED AFTER THIS POINT
            // --drum kit
            // --print ribbon cassette and paper kit
            // --print ribbon / paper kit
            // --printer imaging unit
            // --photoconductor unit
            // --toner cartridge for label applications
            // --toner carrier
            // --printer imaging unit
            // --toner refill
            // --toner kit
            // --waste toner collector
            // --MICR toner cartridge
            VendorSpecificInformation = $"{Manufacturer} {Consumable_ConsumableType.FirstValue().Replace("toner cartridge / waste toner collector", "toner cartridge and waste toner collector")} for exceptional reliability and performance";
            break;
            case var a when a.HasValue()
            && Consumable_ConsumableType.HasValue():
            VendorSpecificInformation = $"{Manufacturer} {Consumable_ConsumableType.FirstValue().Replace("print ribbon / paper kit", "print ribbon and paper kit").Replace(" and paper kit", "").Replace(" for label applications", "").Replace("toner cartridge with drum unit", "toner and drum cartridges").Replace("print ribbon", "print ribbons")} produces superior quality for a wide array of printing needs";
            break;
        }
    }
    if(!String.IsNullOrEmpty(VendorSpecificInformation)){
        Add($"VendorSpecificInformation⸮{VendorSpecificInformation}");
    }
}

// --[FEATURE #4]
// --If cartridge is twin pack or combo pack, bullet should be a savings message
// --Twin package saves money
// --Multi pack saves you money
void Cartridge_SavingsMessage(){
    var result = "";
    var Consumable_ConsumableType = Coalesce(A[4760]); // Consumable - Consumable Type
    var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM"));

    if(Consumable_ConsumableType.HasValue("%with drum unit kit")){
        result = "Bundle pack provides better value";
    }
    else if(TX_UOM.HasValue()){
        switch(TX_UOM){
            case var a when a.HasValue("2/Pack"):
            result = "Comes in two cartridges per pack to save your money";
            break;
            case var a when !a.HasValue("Each"):
            result = "Multi-pack saves you money";
            break;
        }
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"CartridgeSavingsMessage⸮{result}");
    }
}

// --[FEATURE #5]
// --Compatible with: HP ThinkJet, QuietJet & QuietJet plus
void Cartridge_Compatible(){
     var result = "";
    var Miscellaneous_CompatibleCartridge = Coalesce(A[3302]); // Miscellaneous - Compatible Cartridge
    var Miscellaneous_TargetCompany = Coalesce(A[4898]); // Miscellaneous - Target Company
    var Miscellaneous_Refurbished = Coalesce(A[5312]); // Miscellaneous - Refurbished
    var CompatibleProducts = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body);

    if(CompatibleProducts.HasValue()){
        result = Miscellaneous_CompatibleCartridge.HasValue() ? 
        $"Compatible with: {Miscellaneous_CompatibleCartridge.Values.Select(s => s.Value().Replace("Troy", "TROY").Replace("plus", "Plus").Replace("Envy", "ENVY").Replace("Deskjet", "DeskJet").Replace("Officejet", "OfficeJet").Replace("ImageCLASS","imageCLASS").Replace("Kyocera", "KYOCERA").Replace("Copycentre","CopyCentre").Replace("B405/z","B405/Z").Replace("Troy", "TROY")).FlattenWithAnd().ToString().Split(' ').Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).Flatten(" ")}" :
        $"Compatible with: {CompatibleProducts.Text.Replace("plus", "Plus").Replace("Envy", "ENVY").Replace("Deskjet", "DeskJet").Replace("Officejet", "OfficeJet").Replace("ImageCLASS","imageCLASS").Replace("Kyocera", "KYOCERA").Replace("Copycentre","CopyCentre").Replace("B405/z","B405/Z").Replace("Troy", "TROY").Replace(";", ",").Split(' ').Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).Flatten(" ")}";
    }
    else if(Miscellaneous_TargetCompany.HasValue()
        && Miscellaneous_Refurbished.HasValue("remanufactured")){
        result = $"Manufactured specifically for {Miscellaneous_TargetCompany.FirstValue()}, remanufactured toner cartridges offer fast, hassle-free printing";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"CartridgeCompatible⸮{result}");
    }
}

// --[FEATURE #6] - RecycledContent - All
// --HP helps you reduce your environmental impact with free and easy cartridge recycling, and with cartridges made with recycled content
// --Contains 89% post-consumer recycled content and packaging contains 100% recycled content

// --[FEATURE #8]
// --Print beautiful photos with the included glossy photo paper and included inks
// --Includes PP-201 Canon 4 x 6 Glossy Photo Paper (50-sheets)
void Cartridge_Media_Included(){
    var result = "";
    var MediaIncluded_Type = Coalesce(A[2322]); // Media Included - Type
    var MediaIncluded_Size = Coalesce(A[2323]); // Media Included - Size
    var MediaIncluded_IncludedQty = Coalesce(A[2326]); // Media Included - Included Qty

    if(MediaIncluded_Type.HasValue()){
        if(MediaIncluded_Type.Values.Flatten().In("glossy photo paper", "matte photo paper", "photo paper")
            && MediaIncluded_IncludedQty.HasValue()
            && MediaIncluded_Size.HasValue() && MediaIncluded_Size.Values.First().ValueUSM.In("%in%")){ //--Media Included - Size
            result = $"Print beautiful {MediaIncluded_Size.Values.First().ValueUSM.Replace(" in", "")} photos with the included {MediaIncluded_Type.FirstValue()} ({MediaIncluded_IncludedQty.FirstValue()}-sheet) and included inks";
        }
        else if(MediaIncluded_Type.Values.Flatten().ToString().Equals("glossy photo paper")
            && MediaIncluded_IncludedQty.HasValue()){ //--Print beautiful photos with the included glossy photo paper and included inks.
            //--Print beautiful photos and documents with an improved ink formulation that gives you a wider range of colors
            result = $"Print beautiful photos with the included {MediaIncluded_Type.FirstValue()} ({MediaIncluded_IncludedQty.FirstValue()}-sheets) and improved ink formulation that gives you a wider range of colors";
        }
    }
    else if(Coalesce(SKU.ProductId).HasValue("14795486")){
        result = "Print beautiful photos with the included 20 sheets 5 x 7 HP Advanced Photo Paper, 50 sheets 4 x 6 HP Advanced Photo Paper, 15 5 x 7 envelopes, creative booklet with photo and card project ideas";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"CartridgeMediaIncluded⸮{result}");
    }
}

// --[FEATURE #9] 
void Cartridge_Features(){
    var result = "";
    var SupplyType = R("SP-1035").HasValue() ? R("SP-1035").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-1035").HasValue() ? R("cnet_common_SP-1035").Replace("<NULL>", "").Text : ""; // Supply Type - Ink Tank|Photo Ink|Waste Toner|Fax Cartridge|Waste Toner Bottle|Photo Developer Kit|Solid Ink|Fuser Kit|Transfer Kit|Waste Toner Box|Maintenance Kit|Photo Conductor Kit|Oil Bottle|Glossy Photo Print Pack|Refill Kit|Drum|MICR|Other|Printhead/Cleaner|Ink/Printhead/Cleaner|Ink|Usage kit|Imaging Unit|Toner|Printhead|Rolls
    var Consumable_CartridgeFeatures = Coalesce(A[4418]); //Consumable - Cartridge Features
    var Consumable_ConsumableType = Coalesce(A[4760]); // Consumable - Consumable Type

    if(Consumable_CartridgeFeatures.HasValue()){
        switch(Consumable_CartridgeFeatures){
            case var a when a.HasValue("HP Vivera"): //--HP Vivera Ink offers superior ingredients and unique formulations, enabling laser-quality black text and graphics
            result = "HP Vivera Ink offers superior ingredients and unique formulations, enabling laser-quality black text and graphics";
            break;
            case var a when a.HasValue("Canon Full-Photolithography Inkjet Nozzle Engineering (FINE)"): //--Designed with FINE (Full-Photolithography Inkjet Nozzle Engineering) technology, your prints will realize added sharpness, detail and quality 
            result = "Designed with FINE (Full-Photolithography Inkjet Nozzle Engineering) technology, your prints will realize added sharpness, detail and quality";
            break;
            case var a when a.HasValue("Canon ChromaLife100+"): //--ChromaLife100 ink technology creates long-lasting beautiful photos when used with genuine Canon photo paper 
            result = "ChromaLife100+ ink technology creates long-lasting beautiful photos when used with genuine Canon photo paper";
            break;
            case var a when a.HasValue("Canon ChromaLife100"):
            result = "ChromaLife100 ink technology creates long-lasting beautiful photos when used with genuine Canon photo paper";
            break;
            case var a when a.HasValue("Brother Innobella"): //--Innobella™ ink technology provides high quality print with vivid colors and better definition Designed as part of an entire printing system to provide a superior degree of quality 
            result = "Innobella™ ink technology provides high-quality print with vivid colors and better definition; designed as part of an entire printing system to provide a superior degree of quality";
            break;
            case var a when a.HasValue("Epson Claria Premium Ink"): //-- Claria Premium 4-dye color (Photo Black,Cyan, Magenta, Yellow) ink technology delivers stunning photos and sharp text that last for more than 200 years  
            result = "Claria Premium Ink technology delivers stunning photos and sharp texts that last for more than 200 years";
            break;
            case var a when a.HasValue("Epson Claria Ink"): //--Quick drying claria inks make handling photos, worry free for sharing
            result = "Quick-drying Claria Inks make handling photos worry-free for sharing";
            break;
            case var a when a.HasValue("Lexmark Vizix 2.0 print technology"): //--New Vizix 2.0 print technology that delivers vibrant, crisp prints from the first print to the last
            result = "New Vizix 2.0 print technology that delivers vibrant, crisp prints from the first print to the last one";
            break;
            case var a when a.HasValue("Brother INKvestment"): //--INKvestment cartridges provide less than $0.05 color cost per page
            result = "INKvestment cartridges provide less color cost per page";
            break;
            case var a when a.HasValue("Dell Ink Management System"): //--Cartridge supports Dell Ink Management System™ for low ink detection
            result = "Cartridge supports Dell Ink Management System™ for low ink detection";
            break;
            case var a when a.HasValue("Epson UltraChrome Hi-Gloss2 Ink"): //--UltraChrome Hi-Gloss® 2 photo black ink cartridge provides superior resistance to water, fading and smudging
            result = "UltraChrome Hi-Gloss ink provides superior resistance to water, fading and smudging";
            break;
            case var a when a.HasValue("water-resistant"): //--Prints are fade and water resistant, so they're sure to last
            result = "Prints are water resistant, so they're sure to last";
            break;
            case var a when a.HasValue("Epson UltraChrome K3 Ink"): //--Ultrachrome K3 ink can produce archival prints with amazing color fidelity, gloss level and scratch and water-resistance
            result = "UltraChrome K3 ink can produce archival prints with amazing color fidelity, gloss level, scratch, and water resistance";
            break;
            case var a when a.HasValue("Dual Resistant High Density (DRHD)"): //--Clean and sharp printing with ReCP technology | --Dual resistant high-density ink system provides clear, smudge-resistant text and images
            result = "Dual resistant high-density ink system provides clear, smudge-resistant text and images";
            break;
            case var a when a.HasValue("Epson Claria Photo HD Ink"): //--Claria® Hi-Definition Inks provide true-to-life colors for printing your best shots
            result = "Claria® Hi-Definition Inks provide true-to-life colors for printing your best shots";
            break;
            case var a when a.HasValue("Canon Full-Photolithography Inkjet Nozzle Engineering (FINE)/Canon ChromaLife100"):
            result = "Designed with FINE (Full-Photolithography Inkjet Nozzle Engineering) technology, your prints will realize added sharpness, detail and quality|ChromaLife100 ink technology creates long-lasting beautiful photos when used with genuine Canon photo paper";
            break;
            case var a when a.HasValue("HP Vivera / Smart printing"): //--HP Smart Printing technology makes automatic adjustments to optimize print quality and enhance reliability, receive automatic alerts when a cartridge is low or out, enjoy low maintenance, trouble-free printing
            result = "HP vivera Ink offers superior ingredients and unique formulations, enabling laser-quality black text and graphics|HP Smart Printing technology makes automatic adjustments to optimize print quality and enhance reliability";
            break;
            case var a when a.HasValue("HP Smart printing"): //--HP Smart Printing technology makes automatic adjustments to optimize print quality and enhance reliability, receive automatic alerts when a cartridge is low or out, enjoy low maintenance, trouble-free printing
            result = "HP Smart Printing technology makes automatic adjustments to optimize print quality and enhance reliability";
            break;
            case var a when a.HasValue("Dell Toner Management System") && SupplyType.Equals("Toner"): //--Supports Dell Toner Management System™ for low toner detection
            result = "Supports Dell Toner Management system™ for low toner detection";
            break;
            case var a when a.HasValue("HP ColorSphere") && Consumable_ConsumableType.HasValue("toner cartridge"): //--HP ColorSphere toner and machine intelligence built into the cartridge enable fast, high-quality, and consistent results
            result = "HP ColorSphere toner and machine intelligence built into the cartridge enable fast, high-quality, and consistent results";
            break;
            case var a when a.HasValue("HP SureSupply") && Consumable_ConsumableType.HasValue("%cartridge"): //--Use convenient ink alerts to easily identify and shop for original HP cartridges using HP SureSupply
            result = "Use convenient ink alerts to easily identify and shop for original HP cartridges using HP SureSupply";
            break;
            case var a when a.HasValue("HP Smart printing / SureSupply"): //--Shop for supplies hassle-free with HP SureSupply
            result = "HP Smart Printing technology makes automatic adjustments to optimize print quality and enhance reliability. Shop for supplies hassle-free with HP SureSupply";
            break;
            case var a when a.HasValue("Unison Toner") && Consumable_ConsumableType.HasValue("%cartridge"): //--Unison toners unique formulation consistently delivers outstanding image quality, ensures long-life print system reliability and promotes superior sustainability
            result = "Unison toner's unique formulation consistently delivers outstanding image quality, ensures long-life print system reliability and promotes superior sustainability";
            break;
            case var a when a.HasValue("Dell Emulsion Aggregation Toner Technology/Dell Toner Management System"): //--Clean and sharp print with ReCP technology
            result = "Clean and sharp print with Dell Emulsion Aggregation Toner Technology and Dell Toner Management System";
            break;
            case var a when !a.HasValue("Xerox US/XCL/XE sold plan"):
            result = $"Clean and sharp print with {Consumable_CartridgeFeatures.FirstValue().ToTitleCase().Replace(" technology", "").Replace(" / ", "/")} Technology";
            break;
        }
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"CartridgeFeatures⸮{result}");
    }
}

// --[FEATURE #10]
void Cartridge_Remanufactured(){
    var result = "";
    var inkOrTonerSpecialTypeRef = R("SP-22914").HasValue() ? R("SP-22914").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22914").HasValue() ? R("cnet_common_SP-22914").Replace("<NULL>", "").Text : ""; // Ink or Toner Special Type - Use and Return|Remanufactured

    if(inkOrTonerSpecialTypeRef.ToLower().Equals("use and return")){
        result = "Use and Return toner cartridges are specially priced for customers to use once, and are not to be refilled or remanufactured";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"CartridgeRemanufactured⸮{result}");
    }
}

//§§5226058, §12366910285216553, §1212164913166147, §5226157, §12393811005220973, §5226006, §5226054, §12378610641217427 "Remanufactured Laser Printer Ink, Toner & Drum Units, InkJet Printer Ink, Toner & Drum Uni END" "Serhii.O" 

//§§168339291098380001 "Heaters BEGIN" "Serhii.O" 

// Space heater power type, Type of space heater & Usage    
    Type_Of_Heater();
    Speed_Settings();
    Maximum_Wattage_HeaterBTU();
    Estimated_Coverage();
    Featur_es();
    Special_Buttons_And_Controls();
    Adjus_table(); //Aditional
    // 8 - Dimensions - All
    // 9 - Compliant Standards - All
    // 10 - Warranty - All

    // Bullet 1
    // Type of Heater
    void Type_Of_Heater(){
        var TypeOfHeater = "";
        var SpaceHeaterType = R("SP-19100").Text; //Space Heater Type
        var SpaceHeaterPowerType = R("SP-19099").Text; //Space Heater Power Type
        var HeatingCooling_AirFlowControl = Coalesce(A[5668]); //Heating & Cooling - Air Flow Control
        var HeatingCooling_HeatingCapacitykW = Coalesce(A[4135]); //Heating & Cooling - Heating Capacity (kW)
        var HeatingCooling_FormFactor = Coalesce(A[4745]); //Heating & Cooling - Form Factor

        // The ceramic heater oscillates smoothly on a wide arc to heat a large room quickly and evenly
        // https://www.staples.com/product_205774
        if(SpaceHeaterType.ToLower().Contains("ceramic") 
            && HeatingCooling_AirFlowControl.HasValue("%oscillati%")
            && HeatingCooling_HeatingCapacitykW.HasValue() 
            && (HeatingCooling_HeatingCapacitykW.FirstValue() > 500) 
            && HeatingCooling_HeatingCapacitykW.Units.First().Name.HasValue("W")){
            TypeOfHeater = $"The ceramic {HeatingCooling_FormFactor.Where("tower").Flatten(", ")} heater oscillates smoothly on a wide arc to heat a large room quickly and evenly";
            // electric forced air heater quickly heats up the space and provides you with the much-needed warmth
            // https://www.staples.com/product_2111415
        }
        else if(SpaceHeaterPowerType.ToLower().Contains("electric") 
            && SpaceHeaterType.ToLower().Contains("fan-forced")){
            TypeOfHeater = "Electric forced air heater quickly heats up the space and provides you with the much-needed warmth";   
            // Tower heater quickly heats up the room and provides you with the much-needed warmth
            // https://www.staples.com/product_1364793 
        }
        else if(SpaceHeaterType.ToLower().Contains("ceramic") 
            && HeatingCooling_FormFactor.HasValue("tower")){
            TypeOfHeater = "Ceramic tower heater quickly heats up the room and provides you with the much-needed warmth";        
        }
        else if(!String.IsNullOrEmpty(SpaceHeaterType) 
            && HeatingCooling_FormFactor.HasValue("tower")){
            TypeOfHeater = $"{SpaceHeaterType.ToLower().ToUpperFirstChar()} tower heater quickly heats up the room and provides you with the much-needed warmth"; 
            // Ceramic tower heater is featured with 1500 W of warmth offering comfort
            // https://www.staples.com/product_205779
        }
        else if(SpaceHeaterType.ToLower().Contains("ceramic") 
            && HeatingCooling_HeatingCapacitykW.HasValue()){
            TypeOfHeater = $"Ceramic {HeatingCooling_FormFactor.Where("tower").Flatten(", ")} heater is featured with {HeatingCooling_HeatingCapacitykW.FirstValue()}{HeatingCooling_HeatingCapacitykW.Units.First().Name} of warmth, offering comfort"; 
            // The oscil fan heater directs warm air around the entire room for consistent heating
            // https://www.staples.com/product_520127
        }
        else if(SpaceHeaterType.ToLower().Contains("fan-forced") 
            && HeatingCooling_AirFlowControl.HasValue("%oscillati%")){
            TypeOfHeater = "The oscil fan heater directs warm air around the entire room for consistent heating";        
        }
        else if(!String.IsNullOrEmpty(SpaceHeaterType)){
            TypeOfHeater = $"{SpaceHeaterType.ToLower().ToUpperFirstChar()} heater quickly heats up the room and provides you with the much-needed warmth";
        }
        else if(SpaceHeaterPowerType.ToLower().Contains("electric")){
            TypeOfHeater = "Electric heater quickly heats up the room and provides you with the much-needed warmth";
        }
        if(!String.IsNullOrEmpty(TypeOfHeater)){
            Add($"TypeOfHeater⸮{TypeOfHeater}");
        }
    }

    // Bullet 2
    // Speed settings       
    void Speed_Settings(){
        var SpeedSettingsOfHeater = "";
        var SettingsControlsIndicators_HeatSettingsQty = Coalesce(A[4752]); //Settings, Controls & Indicators - Heat Settings Qty
        var HeatingCooling_ProductType = Coalesce(A[3646]); //Heating & Cooling - Product Type

        // Ceramic Heater has been designed with six customized heat settings to give you control of your comfort
        // https://www.staples.com/product_24344682     
        if(SettingsControlsIndicators_HeatSettingsQty.HasValue()){
            switch(SettingsControlsIndicators_HeatSettingsQty.FirstValue()){
                case var a when a > 3:
                SpeedSettingsOfHeater = $"{HeatingCooling_ProductType.FirstValue().ToUpperFirstChar()} has been designed with {SettingsControlsIndicators_HeatSettingsQty.FirstValue()} customized heat settings to give you control of your comfort";
        // Easy-to-use dial controls allow for efficient adjustment of the heater with two fan speeds 
        // https://www.staples.com/product_519877
                break;
                case var a when a > 1:
                SpeedSettingsOfHeater = $"Easy-to-use dial controls allow for efficient adjustment of the {HeatingCooling_ProductType.FirstValue()} with {SettingsControlsIndicators_HeatSettingsQty.FirstValue()} speeds";
                break;
            }
        }
        if(!String.IsNullOrEmpty(SpeedSettingsOfHeater)){
            Add($"SpeedSettingsOfHeater⸮{SpeedSettingsOfHeater}");
        }
    }

    // Bullet 3
    // Maximum wattage & Heater BTU    
    void Maximum_Wattage_HeaterBTU(){
        var MaximumWattageHeaterBTUOfHeater = "";
        var MaximumWattage = R("SP-15338").Text; //Maximum Wattage
        var MaximumHeaterBTU = R("SP-21509").Text; //Maximum Heater BTU

        // Output power: 1500 W of comforting warmth, maximum BTUs: 5118 
        // https://www.staples.com/product_1364793
        if(!String.IsNullOrEmpty(MaximumWattage) 
            && !String.IsNullOrEmpty(MaximumHeaterBTU)){
            MaximumWattageHeaterBTUOfHeater = $"Output power: {MaximumWattage}W of comforting warmth; maximum BTUs: {MaximumHeaterBTU}BTU/h";
        }else if(!String.IsNullOrEmpty(MaximumWattage)){
            MaximumWattageHeaterBTUOfHeater = $"Output power: {MaximumWattage}W of comforting warmth";
        }else if(!String.IsNullOrEmpty(MaximumHeaterBTU)){
            MaximumWattageHeaterBTUOfHeater = $"Maximum Heater BTU: {MaximumHeaterBTU}BTU/h";
        }
        if(!String.IsNullOrEmpty(MaximumWattageHeaterBTUOfHeater)){
            Add($"MaximumWattageHeaterBTUOfHeater⸮{MaximumWattageHeaterBTUOfHeater}");
        }
    }

    // Bullet 4
    // Estimated Coverage (If Available)      
    void Estimated_Coverage(){
        var EstimatedCoverageOfHeater = "";
        var HeatingCooling_RecommendedRoomArea = Coalesce(A[4755]); //Heating & Cooling - Recommended Room Area
        var HeatersCoolers_SettingsControlsIndicators_ControlType = Coalesce(A[4505]); //Heaters & Coolers - Settings, Controls & Indicators - Control Type
        var HeatingCooling_ProductType = Coalesce(A[3646]); //Heating & Cooling - Product Type

        // The 23'' infrared quartz fireplace is capable of providing supplemental zone heating in a room up to 1,000 square feet
        // https://www.staples.com/product_24178305
        if(HeatingCooling_RecommendedRoomArea.HasValue() 
            && HeatingCooling_ProductType.HasValue()){
            EstimatedCoverageOfHeater = HeatersCoolers_SettingsControlsIndicators_ControlType.HasValue("electronic") ? 
            $"The electronic {HeatingCooling_ProductType.FirstValue()} is capable of providing supplemental zone heating in a room up to {Math.Round(HeatingCooling_RecommendedRoomArea.FirstValue().ExtractNumbers().First() * 10.7639, 0)} sq. ft." : 
            $"{HeatingCooling_ProductType.FirstValue().ToUpperFirstChar()} is capable of providing supplemental zone heating in a room up to {Math.Round(HeatingCooling_RecommendedRoomArea.FirstValue().ExtractNumbers().First() * 10.7639, 0)} sq. ft.";
        }
        if(!String.IsNullOrEmpty(EstimatedCoverageOfHeater)){
            Add($"EstimatedCoverageOfHeater⸮{EstimatedCoverageOfHeater}");
        }
    }

    // Bullet 5
    // Features (timer shut-off, portable, overheating protection, etc.) *Use multiple bullets as needed.    
    void Featur_es(){
        var FeaturesOfHeater = "";
        var HeatingCooling_SafetyFeatures = Coalesce(A[4754]); //Heating & Cooling - Safety Features
        var SettingsControlsIndicators_Timer = Coalesce(A[4538]); //Settings, Controls & Indicators - Timer

        // Overheat protection, heater will instantly shut off to prevent overheating 
        // https://www.staples.com/product_24178354
        if(HeatingCooling_SafetyFeatures.HasValue("overheating protection")){
            FeaturesOfHeater = "Overheat protection: heater will instantly shut off to prevent overheating";
        // 8-hour timer for easy operation
        // https://www.staples.com/Lasko-1500-W-Digital-Ceramic-Tower-Heater-with-Remote-Control-Gray-5160/product_2109806    
        }
        else if(SettingsControlsIndicators_Timer.HasValue()){
            FeaturesOfHeater = $"{SettingsControlsIndicators_Timer.FirstValue()}-{SettingsControlsIndicators_Timer.Units.First().Name.Replace("hrs", "hour")} timer for easy operation";
        }
        if(!String.IsNullOrEmpty(FeaturesOfHeater)){
            Add($"FeaturesOfHeater⸮{FeaturesOfHeater}");
        }
    }

    // Bullet 6
    // Special Buttons and Controls (If Applicable)
    void Special_Buttons_And_Controls(){
        var SpecialButtonsAndControlsOfHeater = "";
        var HeatingCooling_FormFactor = Coalesce(A[4745]); //Heating & Cooling - Form Factor
        var HeatingCooling_SafetyFeatures = Coalesce(A[4754]); //Heating & Cooling - Safety Features
        var Miscellaneous_IncludedAccessories = Coalesce(A[5669]); //Miscellaneous - Included Accessories
        var SettingsControlsIndicators_RemoteControl = Coalesce(A[3647]); //Settings, Controls & Indicators - Remote Control
        var HeatingCooling_ProductType = Coalesce(A[3646]); //Heating & Cooling - Product Type

        // Tip-over switch - heater turns off automatically if tipped over 
        // https://www.staples.com/product_2472431     
        if(HeatingCooling_SafetyFeatures.HasValue("tip-over switch")){
            SpecialButtonsAndControlsOfHeater = "Tip-over switch: heater turns off automatically if tipped over";
        // heater comes with an unique safety motion sensor that shuts the heater off when any objects are too close
        // https://www.staples.com/SpaceHeaters/cat_CL80001   
        }
        else if(HeatingCooling_SafetyFeatures.HasValue("safety motion sensor")){
            SpecialButtonsAndControlsOfHeater = "Heater comes with a unique safety motion sensor that shuts the heater off when any objects are too close";
        // Warm a room with the click of a remote with this Lasko tower heater
        // https://www.staples.com/product_205774  
        }
        else if(Miscellaneous_IncludedAccessories.HasValue("remote control") 
            || SettingsControlsIndicators_RemoteControl.HasValue()){
            SpecialButtonsAndControlsOfHeater = $"Warm a room with the click of a remote with this {HeatingCooling_FormFactor.Where("tower").Select(s => s.Value().Replace("Yes", "tower ").Replace("No", "")).Flatten(", ")}{HeatingCooling_ProductType.FirstValue()}";
        }
        if(!String.IsNullOrEmpty(SpecialButtonsAndControlsOfHeater)){
            Add($"SpecialButtonsAndControlsOfHeater⸮{SpecialButtonsAndControlsOfHeater}");
        }
    }

    // Bullet 7 Aditional
    // Adjustable thermostat control helps provide personalized comfort 
    void Adjus_table(){
        var AdjustableOfHeater = "";
        var HeatingCooling_SpecialFeatures = Coalesce(A[4753]); //Heating & Cooling - Safety Features

        // https://www.staples.com/product_409749    
        if(HeatingCooling_SpecialFeatures.HasValue("%adjustable thermostat%")){
            AdjustableOfHeater = "Adjustable thermostat control helps provide personalized comfort";
        }
        if(!String.IsNullOrEmpty(AdjustableOfHeater)){
            Add($"AdjustableOfHeater⸮{AdjustableOfHeater}");
        }
    }

//§§168339291098380001 "Heaters END" "Serhii.O" 

//§§135391203142831 "Classification Folders BEGIN" "Serhii.O" 

First_Main();
    Faste_ners();
    SizeLetter_And_legal();
    TabStyle_And_Location();
    // 5 - Recycled Content - All
    // 6 - Pack Size - All
    Additional_Bullets();

    // --[FEATURE #1]
    // --FirstBullet
    void First_Main(){
        var TrueColorMaterialOfClassificationFolders = "";
        var TrueColor = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : ""; //True Color
        var CardFileHolderMaterial = R("SP-18556").HasValue() ? R("SP-18556").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18556").HasValue() ? R("cnet_common_SP-18556").Replace("<NULL>", "").Text : ""; //Card File & Holder Material
        var PaperStockThicknessPoints = R("SP-23621").HasValue() ? R("SP-23621").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-23621").HasValue() ? R("cnet_common_SP-23621").Replace("<NULL>", "").Text : ""; //Paper Stock Thickness (Points)
        var NumberOfDividers = R("SP-18560").HasValue() ? R("SP-18560").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18560").HasValue() ? R("cnet_common_SP-18560").Replace("<NULL>", "").Text : ""; //Number of Dividers
        var Colors = "";

        if(TrueColor.Equals("Assorted") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            TrueColorMaterialOfClassificationFolders = $"Folders are made of {PaperStockThicknessPoints} pt. {CardFileHolderMaterial.ToLower()} in assorted colors";
        }
        else if(TrueColor.Equals("Multicolor") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            // [color] color with [material] material to organize and protect your documents (https://www.staples.com/Staples-Poly-Expanding-Hanging-File-Jackets-Letter-Assorted-5-Pack/product_706811)  
            TrueColorMaterialOfClassificationFolders = $"{Colors} with {PaperStockThicknessPoints} pt. {CardFileHolderMaterial.ToLower()} material to organize and protect your documents";
        }
        else if(TrueColor.Equals("Multicolor") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial)){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            TrueColorMaterialOfClassificationFolders = $"{Colors} with {CardFileHolderMaterial.ToLower()} material to organize and protect your documents";
        }
        else if(TrueColor.Equals("Black") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)
            && Coalesce(NumberOfDividers).HasValue("1")){
            // Black plastic to complement office decor (https://www.staples.com/3M-Desktop-Document-Holder-Black-12-H-x-9-3-8-W-x-2-D-150-Sheet-Capacity/product_811716)  
            TrueColorMaterialOfClassificationFolders = $"Black {PaperStockThicknessPoints} pt. {CardFileHolderMaterial.ToLower()} with 1 partition to complement office decor";
        }
        else if(TrueColor.Equals("Black") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && Coalesce(NumberOfDividers).HasValue("1")){
            // Black plastic to complement office decor (https://www.staples.com/3M-Desktop-Document-Holder-Black-12-H-x-9-3-8-W-x-2-D-150-Sheet-Capacity/product_811716)  
            TrueColorMaterialOfClassificationFolders = $"Black {CardFileHolderMaterial.ToLower()} with 1 partition to complement office decor";
        }
        else if(TrueColor.Equals("Black") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && Coalesce(NumberOfDividers).ExtractNumbers().Any() 
            && Coalesce(NumberOfDividers).ExtractNumbers().First() > 1
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            // Black plastic to complement office decor (https://www.staples.com/3M-Desktop-Document-Holder-Black-12-H-x-9-3-8-W-x-2-D-150-Sheet-Capacity/product_811716)  
            TrueColorMaterialOfClassificationFolders = $"Black {PaperStockThicknessPoints} pt. {CardFileHolderMaterial.ToLower()} with {NumberOfDividers} partitions to complement office decor";
        }
        else if(TrueColor.Equals("Black") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && Coalesce(NumberOfDividers).ExtractNumbers().Any() 
            && Coalesce(NumberOfDividers).ExtractNumbers().First() > 1){
            // Black plastic to complement office decor (https://www.staples.com/3M-Desktop-Document-Holder-Black-12-H-x-9-3-8-W-x-2-D-150-Sheet-Capacity/product_811716)  
            TrueColorMaterialOfClassificationFolders = $"Black {CardFileHolderMaterial.ToLower()} with {NumberOfDividers} partitions to complement office decor";
        }
        else if(TrueColor.Equals("Black") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && String.IsNullOrEmpty(NumberOfDividers) 
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            // Black plastic to complement office decor (https://www.staples.com/3M-Desktop-Document-Holder-Black-12-H-x-9-3-8-W-x-2-D-150-Sheet-Capacity/product_811716)  
            TrueColorMaterialOfClassificationFolders = $"Black {PaperStockThicknessPoints} pt. {CardFileHolderMaterial.ToLower()} to complement office decor";
        }
        else if(TrueColor.Equals("Black") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && String.IsNullOrEmpty(NumberOfDividers)){
            TrueColorMaterialOfClassificationFolders = $"Black {CardFileHolderMaterial.ToLower()} to complement office decor";
        }
        else if(Coalesce(CardFileHolderMaterial).HasValue("Pressboard") 
            && !String.IsNullOrEmpty(TrueColor) 
            && NumberOfDividers.Equals("1") 
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            // Blue 25 pt. pressboard to hold your documents securely (https://www.staples.com/Staples-Pressboard-Fastener-Folders-Legal-2-Expansion-25-Box/product_384870)
            TrueColorMaterialOfClassificationFolders = $"{Colors} {PaperStockThicknessPoints} pt. pressboard with 1 partition to hold your documents securely";
        }
        else if(Coalesce(CardFileHolderMaterial).HasValue("Pressboard") 
            && !String.IsNullOrEmpty(TrueColor) 
            && NumberOfDividers.Equals("1")){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            TrueColorMaterialOfClassificationFolders = $"{Colors} pressboard with 1 partition to hold your documents securely";
        }
        else if(Coalesce(CardFileHolderMaterial).HasValue("Pressboard") 
            && !String.IsNullOrEmpty(TrueColor) 
            && Coalesce(NumberOfDividers).ExtractNumbers().Any() 
            && Coalesce(NumberOfDividers).ExtractNumbers().First() > 1
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            TrueColorMaterialOfClassificationFolders = $"{Colors} {PaperStockThicknessPoints} pt. pressboard with {NumberOfDividers} partitions to hold your documents securely";
        }
        else if(Coalesce(CardFileHolderMaterial).HasValue("Pressboard") 
            && !String.IsNullOrEmpty(TrueColor) 
            && Coalesce(NumberOfDividers).ExtractNumbers().Any() 
            && Coalesce(NumberOfDividers).ExtractNumbers().First() > 1){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            TrueColorMaterialOfClassificationFolders = $"{Colors} pressboard with {NumberOfDividers} partitions to hold your documents securely";
        }
        else if(Coalesce(CardFileHolderMaterial).HasValue("Pressboard") 
            && !String.IsNullOrEmpty(TrueColor) 
            && String.IsNullOrEmpty(NumberOfDividers) 
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            TrueColorMaterialOfClassificationFolders = $"{Colors} {PaperStockThicknessPoints} pt. pressboard to hold your documents securely";
        }
        else if(Coalesce(CardFileHolderMaterial).HasValue("Pressboard") 
            && !String.IsNullOrEmpty(TrueColor) 
            && String.IsNullOrEmpty(NumberOfDividers)){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            TrueColorMaterialOfClassificationFolders = $"{Colors} pressboard to hold your documents securely";
        }
        else if(!String.IsNullOrEmpty(TrueColor) 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && NumberOfDividers.Equals("1") 
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            //  Red 25 pt. durable pressboard covers with 2 partitions (17 pt. Kraft) (https://www.staples.com/Staples-Top-Tab-Pressboard-Classification-Folders-Letter-Red-2-5-Cut-Tab-2-Partitions-10-Box-18334/product_807772)
            TrueColorMaterialOfClassificationFolders = $"{Colors} {PaperStockThicknessPoints} pt. {CardFileHolderMaterial.ToLower()} covers with 1 partition";
        }
        else if(!String.IsNullOrEmpty(TrueColor) 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && NumberOfDividers.Equals("1")){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            TrueColorMaterialOfClassificationFolders = $"{Colors} {CardFileHolderMaterial.ToLower()} covers with 1 partition";
        }
        else if(!String.IsNullOrEmpty(TrueColor) 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && Coalesce(NumberOfDividers).ExtractNumbers().Any() 
            && Coalesce(NumberOfDividers).ExtractNumbers().First() > 1
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            TrueColorMaterialOfClassificationFolders = $"{Colors} {PaperStockThicknessPoints} pt. {CardFileHolderMaterial.ToLower()} covers with {NumberOfDividers} partitions";
        }
        else if(!String.IsNullOrEmpty(TrueColor) 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && Coalesce(NumberOfDividers).ExtractNumbers().Any() 
            && Coalesce(NumberOfDividers).ExtractNumbers().First() > 1){
            Colors = TrueColor.Split(", ").Count() > 1 ? TrueColor.Split(", ")[0].ToLower().ToUpperFirstChar() + ", " + TrueColor.Split(", ").Skip(1).Select(s => s.ToLower()).Flatten(", ") : TrueColor.ToLower().ToUpperFirstChar();
            TrueColorMaterialOfClassificationFolders = $"{Colors} {CardFileHolderMaterial.ToLower()} covers with {NumberOfDividers} partitions";
        }
        else if(!String.IsNullOrEmpty(TrueColor) 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && String.IsNullOrEmpty(NumberOfDividers) 
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            TrueColorMaterialOfClassificationFolders = $"Comes in {TrueColor.ToLower()} and made of {PaperStockThicknessPoints} pt. {CardFileHolderMaterial.ToLower()} for durability";
        }
        else if(!String.IsNullOrEmpty(TrueColor) 
            && !String.IsNullOrEmpty(CardFileHolderMaterial) 
            && String.IsNullOrEmpty(NumberOfDividers)){
            TrueColorMaterialOfClassificationFolders = $"Comes in {TrueColor.ToLower()} and made of {CardFileHolderMaterial.ToLower()} for durability";
        }
        else if(Coalesce(TrueColor).HasValue("Assorted") 
            && !String.IsNullOrEmpty(CardFileHolderMaterial)){
            TrueColorMaterialOfClassificationFolders = "Assorted colors";
        }
        else if(!String.IsNullOrEmpty(TrueColor) 
            && !String.IsNullOrEmpty(CardFileHolderMaterial)){
            TrueColorMaterialOfClassificationFolders = $"Comes in {TrueColor.ToLower()}";
        }
        if(!String.IsNullOrEmpty(TrueColorMaterialOfClassificationFolders)){
            Add($"TrueColorMaterialOfClassificationFolders⸮{TrueColorMaterialOfClassificationFolders}");
        }
    }

    // --[FEATURE #2]
    // --Fasteners (Size,Position and Capacity)
    void Faste_ners(){
        var FastenersOfClassificationFolders = "";
        var NumberOfFasteners = R("SP-18559").HasValue() ? R("SP-18559").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18559").HasValue() ? R("cnet_common_SP-18559").Replace("<NULL>", "").Text : ""; //Number of Fasteners
        var FastenerCapacityInches = R("SP-23624").HasValue() ? R("SP-23624").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-23624").HasValue() ? R("cnet_common_SP-23624").Replace("<NULL>", "").Text : ""; //Fastener Capacity (Inches)

        if(Coalesce(NumberOfFasteners).ExtractNumbers().Any()){
            switch(Coalesce(NumberOfFasteners).ExtractNumbers().First()){
                case var a when a > 1:
                FastenersOfClassificationFolders = !String.IsNullOrEmpty(FastenerCapacityInches) ? 
                $"{NumberOfFasteners} {FastenerCapacityInches}\" fasteners allow you to subdivide papers within the folder" :
                $"{NumberOfFasteners} fasteners allow you to subdivide papers within the folder";
                break;
                case var a when a == 1:
                FastenersOfClassificationFolders = !String.IsNullOrEmpty(FastenerCapacityInches) ? 
                $"One {FastenerCapacityInches}\" fastener keeps papers secure and in order" :
                "One fastener keeps papers secure and in order";
                break;
            }
        }
        if(!String.IsNullOrEmpty(FastenersOfClassificationFolders)){
            Add($"FastenersOfClassificationFolders⸮{FastenersOfClassificationFolders}");
        }
    }

    // --[FEATURE #3]
    // --Size (letter and legal) with ''up to'' statement for expandable
    void SizeLetter_And_legal(){
        var SizeLetterAndlegalOfClassificationFolders = "";
        var FilingSystem_SupportedFormat = Coalesce(A[5925]); //Filing System - Supported Format
        var FilingSystem_Features = Coalesce(A[5945]); //Filing System - Features
        var FilingSystem_Expansion = Coalesce(A[8552]); //Filing System - Expansion

        if(!FilingSystem_SupportedFormat.HasValue("%Letter%", "%Legal%", "%216 x 279 mm%", "%216 x 356 mm%", "%215.9 x 279.4 mm%")){
            SizeLetterAndlegalOfClassificationFolders = "";
        }
        else if(FilingSystem_Features.HasValue("Tyvek tape%") 
            && FilingSystem_Expansion.HasValue() 
            && FilingSystem_SupportedFormat.HasValue()){
            // Letter size with Tyvek tape on spine for extra durability and up to 2'' capacity (changed) (https://www.staples.com/Staples-Top-Tab-Pressboard-Classification-Folders-Letter-Gray-Green-2-5-Cut-Tab-2-Partitions-10-Box-18335/product_811394)
            SizeLetterAndlegalOfClassificationFolders = $@"{FilingSystem_SupportedFormat.Where("%Letter%", "%Legal%", "%216 x 279 mm%", "%216 x 356 mm%", "%215.9 x 279.4 mm%").First().Value().RegexReplace(".*(Letter).*", "Letter").RegexReplace(".*(Legal).*", "Legal").RegexReplace(".*(216 x 279 mm).*", "Letter").RegexReplace(".*(216 x 356 mm).*", "Legal").RegexReplace(".*(215.9 x 279.4 mm).*", "Letter") + " size with Tyvek tape on spine for extra durability and "}{"up to " + FilingSystem_Expansion.Values.First().ValueUSM.ToLower() + @"\"" capacity"}";
        }
        else if(FilingSystem_Expansion.HasValue() 
            && FilingSystem_SupportedFormat.HasValue()){
            // expands to 1'', allotting you plenty of space to hold all of your important letter-sized (https://www.staples.com/Staples-Manila-Expanding-File-Jacket-1-Letter-50-Box/product_541092)
            SizeLetterAndlegalOfClassificationFolders = $@"Expands up to ##{FilingSystem_Expansion.Values.First().ValueUSM.ToLower()}"" providing plenty of space to hold all of your important {FilingSystem_SupportedFormat.Where("%Letter%", "%Legal%", "%216 x 279 mm%", "%216 x 356 mm%", "%215.9 x 279.4 mm%").First().Value().RegexReplace(".*(Letter).*", "letter").RegexReplace(".*(Legal).*", "legal").RegexReplace(".*(216 x 279 mm).*", "letter").RegexReplace(".*(216 x 356 mm).*", "legal").RegexReplace(".*(215.9 x 279.4 mm).*", "letter").ToLower() + "-size documents"}";
        }
        else if(FilingSystem_SupportedFormat.HasValue("%Letter%", "%216 x 279 mm%")){
            // Letter-size compatible with a wide range of paper (https://www.staples.com/Staples-Pressboard-Fastener-Folders-Letter-2-Expansion-25-Box/product_384868)
            SizeLetterAndlegalOfClassificationFolders = "Letter-size, compatible with a wide range of paper";
        }
        else if(FilingSystem_SupportedFormat.HasValue("%Legal%", "%216 x 356 mm%")){
            // These folders provide an efficient and durable way to collect and store all types of legal-sized documents (https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=52264825&langId=-1&currentSKUNbr=807787&storeId=10101&itemType=1&addWE1ToCart=true&documentID=e4ec4a56a00ea94fafc4273ad6326db63115ad37)
            SizeLetterAndlegalOfClassificationFolders = "Folders provide an efficient and durable way to collect and store all types of legal-size documents";
        }
        if(!String.IsNullOrEmpty(SizeLetterAndlegalOfClassificationFolders)){
            Add($"SizeLetterAndlegalOfClassificationFolders⸮{SizeLetterAndlegalOfClassificationFolders}");
        }
    }

    // --[FEATURE #4]
    // --Tab Style and Location
     void TabStyle_And_Location(){
        var TabStyleAndLocationOfClassificationFolders = "";
        var FilingSystem_Features = Coalesce(A[5945]); //Filing System - Features

        if(FilingSystem_Features.HasValue()){
            switch(FilingSystem_Features){
                case var Features when Features.HasValue("assorted position tabs") 
                || Features.Where("end tab", "left side tab", "right of center tab", "right side tab", "top side tab", "2/5-tab right position").Count() > 1:
                // 3 tabs in mixed locations so its easy to see whats in each folder
                TabStyleAndLocationOfClassificationFolders = "Tabs in mixed locations so it's easy to see what's in each folder";
                break;
                case var Features when Features.HasValue("extra long tab") 
                && Features.HasValue("top side tab") 
                && Features.HasValue("%cut tab"):
                // Extra-long 2/5-cut top tab for labels and titles
                TabStyleAndLocationOfClassificationFolders = $"Extra-long {Features.Where("%cut tab").First().Value().Replace(" cut", "-cut").Replace(" tab", "")} top tab for labels and titles";
                break;
                case var Features when Features.HasValue("2/5-tab right position"):
                // 2/5-сut top tab provides plenty of room for labels and titles
                var position = Features.Where("2/5-tab right position").Flatten(", ");
                TabStyleAndLocationOfClassificationFolders = $@"{position.Replace(" cut", "-cut").Replace("2/5-tab right position", "2/5-cut").Replace(" tab", "").ToUpperFirstChar()} {position.Replace("2/5-tab right position", "right")} tab provides plenty of room for labels and titles";
                break;
                case var Features when Features.HasValue("%cut tab"):
                var cutTab = Features.Where("%cut tab").Flatten(", ").ToLower().Replace(" cut", "-cut").Replace(" tab", "");
                var Tab = Features.Where("end tab", "left side tab", "right of center tab", "right side tab", "top side tab").Count() > 0 ? $" {Features.Where("end tab", "left side tab", "right of center tab", "right side tab", "top side tab").Flatten(", ").ToLower().Replace("end tab", "end").Replace("left side tab", "left").Replace("right of center tab", "right of center").Replace("right side tab", "right").Replace("top side tab", "top")} " : "";

                TabStyleAndLocationOfClassificationFolders = $"Extra-long {cutTab}{Tab}tab provides plenty of room for labels and titles";
                break;
            }
        }
        if(!String.IsNullOrEmpty(TabStyleAndLocationOfClassificationFolders)){
            Add($"TabStyleAndLocationOfClassificationFolders⸮{TabStyleAndLocationOfClassificationFolders}");
        }
    }

    // Additional Bullet (if relevant) (Dividers with integrated double-sided paper holders)
    void Additional_Bullets(){  
        var FilingSystem_Features = Coalesce(A[5945]); //Filing System - Features

        SafeSHIELD();
        TyvekGusset();
        ReinforcedTabs();
        Antimicrobial();
        Dividers();
        HeavyDuty();
        SFIcertified();
        Multicolor();

        // Additional Bullet (if relevant) (SafeSHIELD)
        void SafeSHIELD(){
            var AdditionalSafeSHIELDOfClassificationFolders = "";
            if(FilingSystem_Features.HasValue("SafeSHIELD")){
                AdditionalSafeSHIELDOfClassificationFolders = "SafeSHIELD fastener protects papers and fingers while also reducing fastener crinkling";
            }
            if(!String.IsNullOrEmpty(AdditionalSafeSHIELDOfClassificationFolders)){
                Add($"AdditionalSafeSHIELDOfClassificationFolders⸮{AdditionalSafeSHIELDOfClassificationFolders}");
            }
        }
        // Additional Bullet (if relevant) (Tyvek gusset)
        void TyvekGusset(){
            var AdditionalTyvekGussetOfClassificationFolders = "";
            if(FilingSystem_Features.HasValue("%Tyvek%gusset%")){
                AdditionalTyvekGussetOfClassificationFolders = "Tyvek expandable file gusset offers increased durability";
            }
            if(!String.IsNullOrEmpty(AdditionalTyvekGussetOfClassificationFolders)){
                Add($"AdditionalTyvekGussetOfClassificationFolders⸮{AdditionalTyvekGussetOfClassificationFolders}");
            }
        }
        // Additional Bullet (if relevant) (Reinforced tabs)
        void ReinforcedTabs(){
            var AdditionalReinforcedTabsOfClassificationFolders = "";
            if(FilingSystem_Features.HasValue("reinforced tab%")){
                AdditionalReinforcedTabsOfClassificationFolders = $"Reinforced extra-strength {FilingSystem_Features.Where("reinforced tab%").First().Value().ToString().Split(" ").Last()} for frequently handled files";
            }
            if(!String.IsNullOrEmpty(AdditionalReinforcedTabsOfClassificationFolders)){
                Add($"AdditionalReinforcedTabsOfClassificationFolders⸮{AdditionalReinforcedTabsOfClassificationFolders}");
            }
        }
        // Additional Bullet (if relevant) (Antimicrobial protection)
        void Antimicrobial(){
            var AdditionalAntimicrobialOfClassificationFolders = "";
            if(FilingSystem_Features.HasValue("antimicrobial protection")){
                AdditionalAntimicrobialOfClassificationFolders = "Antimicrobial folders guard against the growth of bacteria, odors, algae, mold, fungus and mildew";
            }
            if(!String.IsNullOrEmpty(AdditionalAntimicrobialOfClassificationFolders)){
                Add($"AdditionalAntimicrobialOfClassificationFolders⸮{AdditionalAntimicrobialOfClassificationFolders}");
            }
        }
        // Additional Bullet (if relevant) (Dividers with integrated double-sided paper holders)
        void Dividers(){
            var AdditionalDividersOfClassificationFolders = "";
            if(FilingSystem_Features.HasValue("dividers with integrated double-sided paper holders")){
                AdditionalDividersOfClassificationFolders = "Dividers with fasteners on both sides let you store more information under several subdivisions";
            }
            if(!String.IsNullOrEmpty(AdditionalDividersOfClassificationFolders)){
                Add($"AdditionalDividersOfClassificationFolders⸮{AdditionalDividersOfClassificationFolders}");
            }
        }
        // Additional Bullet (if relevant) (Heavy duty)
        void HeavyDuty(){
            var AdditionalHeavyDutyOfClassificationFolders = "";
            if(FilingSystem_Features.HasValue("heavy-duty")){
                AdditionalHeavyDutyOfClassificationFolders = "Heavy-duty folders provide stable writing surface and make files easier to handle";
            }
            if(!String.IsNullOrEmpty(AdditionalHeavyDutyOfClassificationFolders)){
                Add($"AdditionalHeavyDutyOfClassificationFolders⸮{AdditionalHeavyDutyOfClassificationFolders}");
            }
        }
        // Additional Bullet (if relevant) (SFI certified)
        void SFIcertified(){
            var AdditionalSFIcertifiedOfClassificationFolders = "";
            var Miscellaneous_CompliantStandards = Coalesce(A[380]); //Miscellaneous - Compliant Standards
            if(Miscellaneous_CompliantStandards.HasValue("SFI")){
                AdditionalSFIcertifiedOfClassificationFolders = "Meets or exceeds SFI standard";
            }
            if(!String.IsNullOrEmpty(AdditionalSFIcertifiedOfClassificationFolders)){
                Add($"AdditionalSFIcertifiedOfClassificationFolders⸮{AdditionalSFIcertifiedOfClassificationFolders}");
            }
        }
        // Additional Bullet (if relevant) (Multicolor)
        // Available in a wide variety of colors including: (https://www.staplesadvantage.com/shop/StplShowItem?catalogId=4&item_id=145810058&langId=-1&currentSKUNbr=395783&storeId=10101&itemType=1&addWE1ToCart=true&documentID=38687bf6f3b75c6d6bf11ba8872a57146bae7584)
        void Multicolor(){
            var AdditionalMulticolorOfClassificationFolders = "";
            var Miscellaneous_Color = Coalesce(A[373]); //Miscellaneous - Color
            var AvailableIn_AvailableProductColors = Coalesce(A[1618]); //Available in - Available Product Colors
            if(Miscellaneous_Color.HasValue("assorted")
            && AvailableIn_AvailableProductColors.HasValue()){
                AdditionalMulticolorOfClassificationFolders = $"Available in a variety of colors including: {AvailableIn_AvailableProductColors.FirstValue()}";
            }
            if(!String.IsNullOrEmpty(AdditionalMulticolorOfClassificationFolders)){
                Add($"AdditionalMulticolorOfClassificationFolders⸮{AdditionalMulticolorOfClassificationFolders}");
            }
        }
    }

//§§135391203142831 "Classification Folders END" "Serhii.O" 

//§§135391203141669 "File Folders BEGIN" "Serhii.O" 

TrueColor_FileFolder_Material();
Number_Of_Tabs();
File_Folder_Size();
// 4 - TX_UOM - All
// 5 - Recycled Content - All
AdditonalFeatures();
Capacity();
CornersOfFileFolders();

    // --[FEATURE #1]
    // --True Color & File folder material; Including stock & weight #
void TrueColor_FileFolder_Material(){
    var TrueColorMaterialOfFileFolder = "";
    var TrueColor = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : ""; //True Color
        var FolderMaterial = R("SP-18596").HasValue() ? R("SP-18596").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18596").HasValue() ? R("cnet_common_SP-18596").Replace("<NULL>", "").Text : ""; //Folder Material
        var PaperStockThicknessPoints = R("SP-23621").HasValue() ? R("SP-23621").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-23621").HasValue() ? R("cnet_common_SP-23621").Replace("<NULL>", "").Text : ""; //Paper Stock Thickness (Points)

        if(!String.IsNullOrEmpty(TrueColor)
            && !String.IsNullOrEmpty(FolderMaterial)
            && !String.IsNullOrEmpty(PaperStockThicknessPoints)){
            if(TrueColor.ToLower().Equals(FolderMaterial.ToLower())){
                TrueColorMaterialOfFileFolder = $"{PaperStockThicknessPoints} pt. {FolderMaterial.ToLower()} paper is long-lasting and durable";
            }
            else if(TrueColor.ToLower().Equals("assorted")){
                TrueColorMaterialOfFileFolder = $"{PaperStockThicknessPoints} pt. {FolderMaterial.ToLower()} in {TrueColor.ToLower()} colors is long-lasting and durable";
            }
            else{
                TrueColorMaterialOfFileFolder = $"{PaperStockThicknessPoints} pt. {TrueColor.ToLower()} {FolderMaterial.ToLower()} is long-lasting and durable";
            }
        }
        else if(!String.IsNullOrEmpty(FolderMaterial)){
            TrueColorMaterialOfFileFolder = !String.IsNullOrEmpty(TrueColor) ? $"Comes in {TrueColor.ToLower().Replace("assorted", "assorted colors")} and made of {FolderMaterial.ToLower()} for durability" :
            $"Sturdy {FolderMaterial.ToLower()} prevents bends and tears for lasting organization";
        }
        else if(!String.IsNullOrEmpty(TrueColor)){
            TrueColorMaterialOfFileFolder = !TrueColor.ToLower().Equals("assorted") ? $"{TrueColor} color" :
            TrueColor.ToLower().Equals("assorted") ? "Assorted colors for easy organization" : "";
        }
        if(!String.IsNullOrEmpty(TrueColorMaterialOfFileFolder)){
            Add($"TrueColorMaterialOfFileFolder⸮{TrueColorMaterialOfFileFolder}");
        }
    }

    // --[FEATURE #2]
    // --Number of tabs & File folder type
    // Reinforced two-ply tabs provide extra strength where you need it most
    void Number_Of_Tabs(){
     var NumberOfTabs = "";
     var Tabs = R("SP-18551").HasValue() ? R("SP-18551").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18551").HasValue() ? R("cnet_common_SP-18551").Replace("<NULL>", "").Text : ""; //No Tabs|6-Tab|5-Tab|3-Tab|2-Tab|2/5 Cut|Straight Cut|1/5 Cut
        var FileFolderType = R("SP-18598").HasValue() ? R("SP-18598").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18598").HasValue() ? R("cnet_common_SP-18598").Replace("<NULL>", "").Text : ""; //File Folder Type - End Tab|Accordion Folders|Envelopes|End/Top Tab|Top Tab|Top Tab w/Fastener|End Tab w/Fastener
        var FilingSystem_Features = Coalesce(A[5945]); //Filing System - Features
        if(FilingSystem_Features.HasValue() && FilingSystem_Features.Where("two-ply tab", "reinforced tab").Count() == 2){
            NumberOfTabs = "Reinforced 2-ply tabs provide extra strength where you need it most";
        }
        else if(!String.IsNullOrEmpty(Tabs)){
            switch(Tabs){
                case var tab when tab.Equals("No Tabs") || String.IsNullOrEmpty(tab):
                NumberOfTabs = FilingSystem_Features.HasValue() ?
                $"{FilingSystem_Features.FirstValue().ToUpperFirstChar()} for easy labeling" : "";
                break;
                case var tab when tab.Equals("1"):
                NumberOfTabs = FilingSystem_Features.HasValue("%/%tab%") ?
                $"{FilingSystem_Features.Where("%/%tab%").Flatten(" ").ExtractNumbers().Flatten("/")}-cut {Coalesce(FileFolderType, "tab")} keeps filing and reordering files simple" :
                $"Single {Coalesce(FileFolderType, "tab")} keeps filing and reordering files simple";
                break;
                case var tab when tab.Equals("5+"):
                NumberOfTabs = FilingSystem_Features.HasValue("%/%tab%") ?
                $"{FilingSystem_Features.Where("%/%tab%").Flatten(" ").ExtractNumbers().Flatten("/")}-cut {Coalesce(FileFolderType, "tab")}s allow ample space for labeling to keep your files organized" : !String.IsNullOrEmpty(FileFolderType) ? 
                $"Five {Coalesce(FileFolderType, "tab")}s allow ample space for labeling to keep your files organized" : "";
                break;
            }
        }
        if(!String.IsNullOrEmpty(NumberOfTabs)){
            Add($"NumberOfTabs⸮{NumberOfTabs}");
        }
    }

    // --[FEATURE #3]
    // --File folder size
    void File_Folder_Size(){
        var FileFolderSize = "";
        var FileFolderSizeOfFileFolders = R("SP-22426").HasValue() ? R("SP-22426").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22426").HasValue() ? R("cnet_common_SP-22426").Replace("<NULL>", "").Text : ""; //File Folder Size - Letter/Legal|Other|Legal|Letter
        var DimensionsWeight_Width = R("SP-21044").HasValue() ? R("SP-21044").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21044").HasValue() ? R("cnet_common_SP-21044").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Width
        var DimensionsWeight_Height = R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Depth

        if(!String.IsNullOrEmpty(FileFolderSizeOfFileFolders)
            && !String.IsNullOrEmpty(DimensionsWeight_Width)
            && !String.IsNullOrEmpty(DimensionsWeight_Height)){
            switch(FileFolderSizeOfFileFolders.ToLower()){
                case var a when a.Equals("letter/legal"):
                FileFolderSize = $@"Accommodates letter or legal-size files and measures {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W";
                break;
                case var a when a.Equals("legal"):
                FileFolderSize = $@"Accommodates legal-size files and measures {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W";
                break;
                case var a when a.Equals("letter"):
                FileFolderSize = $@"Letter-size folders measuring {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W easily hold important files";
                break;
            }
        }else if(!String.IsNullOrEmpty(FileFolderSizeOfFileFolders)){
            switch(FileFolderSizeOfFileFolders.ToLower()){
                case var a when a.Equals("letter/legal"):
                FileFolderSize = "Accommodates letter or legal-size files";
                break;
                case var a when a.Equals("legal"):
                FileFolderSize = "Legal-size folders for holding oversize documents without folding";
                break;
                case var a when a.Equals("letter"):
                FileFolderSize = "Letter-size folders easily hold important files";
                break;
            }
            }else if(String.IsNullOrEmpty(FileFolderSizeOfFileFolders)
                && !String.IsNullOrEmpty(DimensionsWeight_Width)
                && !String.IsNullOrEmpty(DimensionsWeight_Height)){
                FileFolderSize = $@"Accommodates {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W size files";
            }
            if(!String.IsNullOrEmpty(FileFolderSize)){
                Add($"FileFolderSize⸮{FileFolderSize}");
            }
        }

    // --[FEATURE #4]
    // --Additonal features
        void AdditonalFeatures(){
            var AdditonalFeaturesOfFileFolders = "";
            if(!(SKU.ProductId is null) && SKU.ProductId.In("20491307")){
                AdditonalFeaturesOfFileFolders = "Preprinted information includes addresses, phone numbers, and key steps";
            };
            if(!String.IsNullOrEmpty(AdditonalFeaturesOfFileFolders)){
                Add($"AdditonalFeaturesOfFileFolders⸮{AdditonalFeaturesOfFileFolders}");
            }
        }

    // --[FEATURE #5]
    // --Capacity
        void Capacity(){
            var CapacityOfFileFolders = "";
        var FilingSystem_Capacity = Coalesce(A[6576]); //Filing System - Capacity

        if(FilingSystem_Capacity.HasValue()
            && FilingSystem_Capacity.Units.First().Name.In("sheet%")){
            CapacityOfFileFolders = $"Holds up to {FilingSystem_Capacity.FirstValue()} sheets for convenience";
        }
        if(!String.IsNullOrEmpty(CapacityOfFileFolders)){
            Add($"CapacityOfFileFolders⸮{CapacityOfFileFolders}");
        }
    }

    // Corners
    void CornersOfFileFolders(){

        var FilingSystem_Features = Coalesce(A[5945]); //Filing System - Features
        Resists();
        RoundedCorners();
        AcidFreeOfFileFolders();
        ReinforcedShelfMaster();
        ProtectCutLess();
        StraightCut();

        // Additional bullets

        void Resists(){
            var AdditionalResistsOfFileFolders = "";
            if(FilingSystem_Features.HasValue("bending-resistant")){
                AdditionalResistsOfFileFolders = "Resists twisting and bending from normal handling";
            }
            else if(FilingSystem_Features.HasValue("%proof%", "%resistance%", "%resistant%")){
                AdditionalResistsOfFileFolders = $"{FilingSystem_Features.Where("%proof%", "%resistance%", "%resistant%").Select(s => s.Value()).FlattenWithAnd(10, ", ").ToUpperFirstChar().Replace("Tearproof and waterproof", "Tear proof and water proof")} for absolute safe keeping";
            }
            if(!String.IsNullOrEmpty(AdditionalResistsOfFileFolders)){
                Add($"AdditionalResistsOfFileFolders⸮{AdditionalResistsOfFileFolders}");
            }
        }
        void RoundedCorners(){
            var AdditionalRoundedCornersOfFileFolders = "";
            if(FilingSystem_Features.HasValue("rounded corners")){
                AdditionalRoundedCornersOfFileFolders = "Stylish rounded corners that aren't likely to snag and tear";
            }
            if(!String.IsNullOrEmpty(AdditionalRoundedCornersOfFileFolders)){
                Add($"AdditionalRoundedCornersOfFileFolders⸮{AdditionalRoundedCornersOfFileFolders}");
            }
        }
        void AcidFreeOfFileFolders(){
            var AdditionalAcidFreeOfFileFolders = "";
            if(FilingSystem_Features.HasValue("acid free")){
                AdditionalAcidFreeOfFileFolders = "Acid free for archival purposes";
            }
            if(!String.IsNullOrEmpty(AdditionalAcidFreeOfFileFolders)){
                Add($"AdditionalAcidFreeOfFileFolders⸮{AdditionalAcidFreeOfFileFolders}");
            }
        }
        // Reinforced Shelf-Master tab gives added strength where you need it most.
        void ReinforcedShelfMaster(){
            var AdditionalReinforcedShelfMasterOfFileFolders = "";
            if(FilingSystem_Features.HasValue("Shelf-Master tab", "reinforced tabs")){
                AdditionalReinforcedShelfMasterOfFileFolders = "Reinforced Shelf-Master tab gives added strength where you need it most";
            }
            if(!String.IsNullOrEmpty(AdditionalReinforcedShelfMasterOfFileFolders)){
                Add($"AdditionalReinforcedShelfMasterOfFileFolders⸮{AdditionalReinforcedShelfMasterOfFileFolders}");
            }
        }
        void ProtectCutLess(){
            var AdditionalProtectCutLessOfFileFolders = "";
            if(FilingSystem_Features.HasValue("CutLess")){
                // Protect your fingers with CutLess folders
                AdditionalProtectCutLessOfFileFolders = "Protect your fingers with CutLess folders";
            }
            else if(FilingSystem_Features.HasValue("WaterShed")){
                // WaterShed® folders that resist liquid spills such as water, coffee, soda, etc.
                AdditionalProtectCutLessOfFileFolders = "WaterShed folders resist liquid spills such as water, coffee, soda, etc.";
            }
            if(!String.IsNullOrEmpty(AdditionalProtectCutLessOfFileFolders)){
                Add($"AdditionalProtectCutLessOfFileFolders⸮{AdditionalProtectCutLessOfFileFolders}");
            }
        }
        // Straight-cut tabs offer plenty of labeling space 
        void StraightCut(){
            var AdditionalStraightCutOfFileFolders = "";
            if(FilingSystem_Features.HasValue("straight-cut tab")){
                AdditionalStraightCutOfFileFolders = "Reinforced Shelf-Master tab gives added strength where you need it most";
            }
            if(!String.IsNullOrEmpty(AdditionalStraightCutOfFileFolders)){
                Add($"AdditionalStraightCutOfFileFolders⸮{AdditionalStraightCutOfFileFolders}");
            }
        }
    }

//§§135391203141669 "File Folders END" "Serhii.O" 

//§§1385810842142725 "Labels BEGIN" "Serhii.O" 

Label_Type();
LayFlatLabel_Dimensions();
// TrueColor_Material - All
Adhesive_Type();
Package_Contents();
Additional_Labels();
Printer_Compatibility();

// "Bullet 1" "Label Type" 
void Label_Type(){
    var LabelTypeOfLabels = "";
    var FilingStorageFilingSystem_ManufacturerProductType = Coalesce(A[5921]); // iling & Storage - Filing System - Manufacturers Product Type
    var PrinterConsumables_Media_MediaType = Coalesce(A[4759]); // Printer Consumables - Media - Media Type
    var PrinterConsumables_RFID_CompliantStandards = Coalesce(A[4665]); // Printer Consumables - RFID - Compliant Standards
    var PrinterConsumables_Media_AdhesiveType = Coalesce(A[10619]); // Printer Consumables - Media - Adhesive Type
    var PresentationAccessories_General_ProductType = Coalesce(A[6116]); // Presentation Accessories - General - Product Type
    var LabelType = R("SP-21117").HasValue() ? R("SP-21117").Text : 
        R("cnet_common_SP-21117").HasValue() ? R("cnet_common_SP-21117").Text : ""; // Address|Thermal Transfer|Shipping|Media|Label Holder|Multipurpose|Identification & Color Coding|Specialty|File Folder

    if(FilingStorageFilingSystem_ManufacturerProductType.HasValue("%year label")){
        LabelTypeOfLabels = "This label is perfect for designating the year that a file was opened or purged";
    }
    else if(PrinterConsumables_Media_MediaType.HasValue("%CD/DVD label applicator%")){
        LabelTypeOfLabels = "Easy-to-use applicator for applying CD/DVD labels";
    }
    else if(PrinterConsumables_Media_MediaType.HasValue("%CD/DVD labels%")){
        LabelTypeOfLabels = "CD/DVD labels for organizing digital media";
    }
    else{
        switch(LabelType.ToLower()){
            case var a when a.Equals("shipping"):
            LabelTypeOfLabels = "These shipping labels create a professional, custom-printed look";
            break;
            case var a when a.Equals("file folder"):
            LabelTypeOfLabels = PrinterConsumables_Media_AdhesiveType.HasValue("%Removable%") ? "Make quick changes to your file folders with these Removable File Folder Labels" :
            "Great for making quick changes and reusing file folders";
            break;
            case var a when a.Equals("multipurpose") && PrinterConsumables_Media_AdhesiveType.HasValue("%Removable%"):
            LabelTypeOfLabels = "Multipurpose labels stay in place until you take them off and leave no residue when removed";
            break;
            case var a when !String.IsNullOrEmpty(a) && PrinterConsumables_Media_AdhesiveType.HasValue("%Removable%"):
            LabelTypeOfLabels = $"{LabelType.Replace("&", "and")} labels stay in place until you take them off";
            break;
            case var a when a.Equals("label holder"):
            var PAProductType = PresentationAccessories_General_ProductType.HasValue() && 
            PresentationAccessories_General_ProductType.Where("%magnetic%").First().Value().ToString().Length > 0 ? "with magnetic backings " : "";
            LabelTypeOfLabels = $"Label holders {PAProductType.ToLower()}for easy repositioning or removal";
            break;
            case var a when !String.IsNullOrEmpty(a):
            LabelTypeOfLabels = PrinterConsumables_RFID_CompliantStandards.HasValue() ? $"These {PrinterConsumables_RFID_CompliantStandards.Values.Flatten("/").ToLower()} {LabelType.Replace("&", "and").Replace("&amp;", "and").ToLower()} labels make your most important messages stand out" :
            $"These {LabelType.Replace("&", "and").Replace("&amp;", "and").ToLower()} labels make your most important messages stand out";
            break;
        }
    }
    if(!String.IsNullOrEmpty(LabelTypeOfLabels)){
        Add($"LabelTypeOfLabels⸮{LabelTypeOfLabels}");
    }
}

// "Bullet 2" "Lay Flat (Label) Dimensions: Height (top edge to bottom edge) x Width (left edge to right edge). Exception: If a full page label, reverse and show Width (left edge to right edge) x Height (top edge to bottom edge). EG: 8.5'' x 11''" 
void LayFlatLabel_Dimensions(){
    var LayFlatLabelDimensions = "";
    var LabelLengthInches = R("SP-22102").HasValue() ? R("SP-22102").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22102").HasValue() ? R("cnet_common_SP-22102").Replace("<NULL>", "").Text : ""; // Label Length (Inches)
    var LabelWidthInches = R("SP-22103").HasValue() ? R("SP-22103").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22103").HasValue() ? R("cnet_common_SP-22103").Replace("<NULL>", "").Text : ""; // Label Length (Inches)
    var LabelDiameterInches = R("SP-22258").HasValue() ? R("SP-22258").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22258").HasValue() ? R("cnet_common_SP-22258").Replace("<NULL>", "").Text : ""; // Label Diameter (Inches)

    if(!String.IsNullOrEmpty(LabelLengthInches)
    && !String.IsNullOrEmpty(LabelWidthInches)){
        LayFlatLabelDimensions = LabelLengthInches.Equals("11") && LabelWidthInches.Equals("8.5") ? @"Individual labels measure 8.5""W x 11""H" :
            $@"Individual labels measure {LabelLengthInches}""H x {LabelWidthInches}""W";
    }
    else if(!String.IsNullOrEmpty(LabelDiameterInches)){
        LayFlatLabelDimensions = $@"Each label measures {LabelDiameterInches}""Dia.";
    }
    if(!String.IsNullOrEmpty(LayFlatLabelDimensions)){
        Add($"LayFlatLabelDimensions⸮{LayFlatLabelDimensions}");
    }
}

// "Bullet 3" "True color & Label Material" - All

// "Bullet 4" "Adhesive Type" 
void Adhesive_Type(){
    var AdhesiveType = "";
    var Adhesive_Type_Ref = R("SP-22254").HasValue() ? R("SP-22254").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22254").HasValue() ? R("cnet_common_SP-22254").Replace("<NULL>", "").Text : ""; // Adhesive Type - Removable|Permanent|Magnetic
    var PrinterConsumables_Media_AdhesiveType = Coalesce(A[10619]); // Printer Consumables - Media - Adhesive Type

    if(!String.IsNullOrEmpty(Adhesive_Type_Ref)){
        switch(Adhesive_Type_Ref.ToLower()){
            case "permanent":
            AdhesiveType = "Features a permanent adhesive backing for a secure bond";
            break;
            case "removable":
            AdhesiveType = "Removable adhesive for residue free cleanup";
            break;
            case "magnetic":
            AdhesiveType = "Magnetic back allows quick repositioning or removal";
            break;
        }
    }
    else if(PrinterConsumables_Media_AdhesiveType.HasValue()){
        switch(PrinterConsumables_Media_AdhesiveType){
            case var type when type.HasValue("%permanent%"):
            AdhesiveType = "Features a permanent adhesive backing for a secure bond";
            break;
            case var type when type.HasValue("%self-adhesive%"):
            AdhesiveType = "Use the self-adhesive labels to stick them on papers, desks, and other places where you need a reminder";
            break;
            case var type when type.HasValue("%Removable%"):
            AdhesiveType = "Removable adhesive for residue free cleanup";
            break;
            case var type when type.HasValue("%Repositionable%"):
            AdhesiveType = "Repositionable self-adhesive provides extra versatility";
            break;
        }
    }
    if(!String.IsNullOrEmpty(AdhesiveType)){
        Add($"AdhesiveType⸮{AdhesiveType}");
    }
}

// "Bullet 5" "Package Contents as Labels per Sheet and labels total" 
void Package_Contents(){
    var result = "";
    var NumberOfLabelsPerSheet = R("SP-22252").HasValue() ? R("SP-22252").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22252").HasValue() ? R("cnet_common_SP-22252").Replace("<NULL>", "").Text : ""; // Number of Labels Per Sheet
    var NumberOfLabelsPerRoll = R("SP-8953").HasValue() ? R("SP-8953").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-8953").HasValue() ? R("cnet_common_SP-8953").Replace("<NULL>", "").Text : ""; // Number of Labels per Roll
    var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM"));
    var Size = TX_UOM.ExtractNumbers().Any() ? TX_UOM.ExtractNumbers().First() : 0; //Filing System - Features

    if(Coalesce(NumberOfLabelsPerSheet).ExtractNumbers().Any()){
        var PerSheet = Coalesce(NumberOfLabelsPerSheet).ExtractNumbers().First();
        result = PerSheet == 1 && Size > 0 ? $"{PerSheet} label per sheet, {Size} labels total" :
        PerSheet > 1 && Size > 0 ? $"{PerSheet} labels per sheet, {Size} labels total" : "";
    }
    else if(Coalesce(NumberOfLabelsPerRoll).ExtractNumbers().Any()){
        var PerRoll = Coalesce(NumberOfLabelsPerRoll).ExtractNumbers().First();
        result = PerRoll == 1 && Size > 0 ? $"{PerRoll} label per roll, {Size} labels total" :
        PerRoll > 1 && Size > 0 ? $"{PerRoll} labels per roll, {Size} labels total" : "";
    }
    else if(Size > 0 && TX_UOM.Text.Split("/").Count() > 1){
        result = $@"{Size} per {TX_UOM.Text.Split("/").Last().ToLower()}";
    }
    else if(TX_UOM.Text.Contains("Dozen")){
        result = "12 per pack";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"PackageContentsLabels⸮{result}");
    }
}

// "Use for additional product and/or manufacturer information relevant to the customer buying decision" 
void Additional_Labels(){
    var PrinterConsumables_Media_Features = Coalesce(A[4783]); // Printer Consumables - Media - Features
    var PrinterConsumables_Media_MediaType = Coalesce(A[4759]); // Printer Consumables - Media - Media Type
    var PrinterConsumables_Consumable_ConsumableType = Coalesce(A[4760]); // Printer Consumables - Consumable - Consumable Type
    var Miscellaneous_IncludedAccessories = Coalesce(A[4879]); // Miscellaneous - Included Accessories
    var SeriesOrCollectionRef = R("SP-22915").HasValue() ? R("SP-22915").Text : 
        R("cnet_common_SP-22915").HasValue() ? R("cnet_common_SP-22915").Text : ""; 

    // "Bullet 6" 
    var PopUpEdge = "";
    var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM"));
    if(PrinterConsumables_Media_Features.HasValue("%Pop%up%")){
        PopUpEdge = TX_UOM.HasValue("Each") ? "Label features pop-up edge for fast peeling, simply bend the sheet to expose the label edge" :
        "Labels feature pop-up edge for fast peeling, simply bend the sheet to expose the label edge";
    }
    if(!String.IsNullOrEmpty(PopUpEdge)){
        Add($"PopUpEdge⸮{PopUpEdge}");
    }

    // "Bullet 7" 
    var TrueBlockTechnology = "";
    if(PrinterConsumables_Media_Features.HasValue("%TrueBlock%") || SeriesOrCollectionRef.Equals("TrueBlock")){
        TrueBlockTechnology = "TrueBlock Technology completely blocks out everything underneath to ensure a higher rate of scanning accuracy of barcodes";
    }
    if(!String.IsNullOrEmpty(TrueBlockTechnology)){
        Add($"TrueBlockTechnology⸮{TrueBlockTechnology}");
    }

    // "Bullet 8, 9, 10, 11" 
    var AdditionalLabels891011 = new List<string>();
    if(PrinterConsumables_Media_MediaType.HasValue("%CD/DVD%labels%") && PrinterConsumables_Consumable_ConsumableType.HasValue("CD/DVD labels / label applicator")){
        AdditionalLabels891011.Add("Print technology: Write-on");
        AdditionalLabels891011.Add("Unique design fits conveniently on top or in a desk drawer");
        AdditionalLabels891011.Add("You've designed the perfect disc label - Shouldn't you be just as meticulous when applying it to a disc");
        AdditionalLabels891011.Add("This CD/DVD Label Applicator helps you center labels perfectly on CDs and DVDs every time");
    }
    int i = 8;
    foreach(var add in AdditionalLabels891011){
        if(!String.IsNullOrEmpty(add)){
            Add($"AdditionalLabels{i}⸮{add}");
            i++;
        }
    }

    // "Bullet 12" 
    var DesignProSoftware = "";
    if(Miscellaneous_IncludedAccessories.HasValue("Avery DesignPro SE CD")){
        DesignProSoftware = "Utilize DesignPro software for professional-looking labels that stand out";
    }
    if(!String.IsNullOrEmpty(DesignProSoftware)){
        Add($"DesignProSoftware⸮{DesignProSoftware}");
    }
}

void Printer_Compatibility(){
    var result = "";
    var PrinterCompatibilityRef = R("SP-18390").HasValue() ? R("SP-18390").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18390").HasValue() ? R("cnet_common_SP-18390").Replace("<NULL>", "").Text : ""; // Printer Compatibility Hand Written|Laser|Laser/Inkjet|Inkjet|Copier|Dot Matrix|Thermal
    if(!String.IsNullOrEmpty(PrinterCompatibilityRef)){
        switch(PrinterCompatibilityRef){
            case var a when a.Equals("Laser/Inkjet"):
            result = "Compatible with laser and inkjet printers for maximum flexibility";
            break;
            case var a when a.Equals("Laser"):
            result = "Compatible with laser printers for efficient, hassle-free printing";
            break;
            case var a when a.Equals("Inkjet"):
            result = "Compatible with inkjet printers for convenient printing";
            break;
        }
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"PrinterCompatibility⸮{result}");
    }
}

//§§1385810842142725 "Labels END" "Serhii.O" 

//§§1114953110001 "Pens" "Alex K." 

PenType();
PenInkTrueColor();
PenPointTypeAndSize();
PenBarrelColorAndBarrelMaterial();
//Pack size
PenGrip();
PenPocketClip();
PenRefillable();
BarrelShape();
InkLevelViewingWindow();
ArchivalAcidFree();
UniSuperInk();
UniFlowSystem();
LiquidInkSystem();
// "Pen Type" "Bullet 1" 
void PenType() {
    var penType = "";
    var penTypeVal = R("SP-18609").HasValue() ? R("SP-18609").Replace("<NULL>", "").Text : R("cnet_common_SP-18609").HasValue() ? R("cnet_common_SP-18609").Replace("<NULL>", "").Text : "";
    var penPackSize = R("SP-18666").HasValue() ? R("SP-18666").Replace("<NULL>", "").Text : R("cnet_common_SP-18666").HasValue() ? R("cnet_common_SP-18666").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(penTypeVal) && !String.IsNullOrEmpty(penPackSize)) {
        if (penTypeVal.ToLower().Contains("erasable") && penPackSize.Equals("1")) {
            penType = ($"{penTypeVal} pen allows you to write, erase and rewrite with no wear-and-tear");
        }
        else if (penTypeVal.ToLower().Equals("retractable gel") && penPackSize.Equals("1")) {
            penType = ($"Retractable gel pen for everyday writing tasks");
        }
        else if (penTypeVal.ToLower().Contains("retractable") && penPackSize.Equals("1")) {
            penType = ($"{penTypeVal} pen is ready to write with just a click");
        }
        else if (penTypeVal.ToLower().Contains("ballpoint") && penPackSize.Equals("1")) {
            penType = ($"{penTypeVal} pen for clear, consistent writing");
        }
        else if (penTypeVal.ToLower().Contains("ballpoint") && penPackSize.Equals("1")) {
            penType = ($"{penTypeVal} pen for clear, consistent writing");
        }
        else if (penTypeVal.ToLower().Contains("fountain") && penPackSize.Equals("1")) {
            penType = ($"The everyday {penTypeVal} pen for smooth, expressive writing");
        }
        else if (penTypeVal.ToLower().Contains("gel") && penPackSize.Equals("1")) {
            penType = ($"{penTypeVal} pink pen for any task");
        }
        else if (penTypeVal.ToLower().Contains("erasable")) {
            penType = ($"{penTypeVal} pens allow you to write, erase and rewrite with no wear-and-tear");
        }
        else if (penTypeVal.ToLower().Equals("retractable ballpoint")) {
            penType = ($"Retractable ballpoint pens for ease of use");
        }
        else if (penTypeVal.ToLower().Contains("retractable")) {
            penType = ($"{penTypeVal} pens are ready to write with just a click");
        }
        else if (penTypeVal.ToLower().Equals("felt")) {
            penType = ($"Felt tip draws bold and expressive lines");
        }
        else if (penTypeVal.ToLower().Contains("ballpoint")) {
            penType = ($"{penTypeVal} pens for clear, consistent writing");
        }
        else if (penTypeVal.ToLower().Contains("fountain")) {
            penType = ($"The everyday {penTypeVal} pens for smooth, expressive writing");
        }
        else if (penTypeVal.ToLower().Contains("rollerball")) {
            penType = ($"Rollerball design delivers a bold ink laydown");
        }
        else if (penTypeVal.ToLower().Contains("gel")) {
            penType = ($"{penTypeVal} ink pens for any task");
        }
        else if (penTypeVal.ToLower().Contains("counter top")) {
            penType = ($"Counter top pen is always handy for your customers");
        }
    }
    if (!String.IsNullOrEmpty(penType)) { 
        Add($"PenType⸮{penType}"); 
    }
}

// "Bullet 2"  "Pen Ink True color" 
void PenInkTrueColor() {
    var penInkTrueColor = "";
    var trueColor = !(R("SP-22967") is null) || !R("SP-22967").Text.Equals("<NULL>") ? R("SP-22967").Text : 
    !(R("cnet_common_SP-22967") is null) || !R("cnet_common_SP-22967").Text.Equals("<NULL>") ? R("cnet_common_SP-22967").Text : "";

    if (!String.IsNullOrEmpty(trueColor)) {
        switch (trueColor) {
            case var a when a.ToLower().Contains("assorted"):
                penInkTrueColor = ($"{trueColor} colors for a variety of tasks");
                break;
            case var a when a.ToLower().Contains("black"):
                penInkTrueColor = ($"Black ink has a rich color for excellent readability");
                break;
            case var a when a.ToLower().StartsWith("blue"):
            case var b when b.ToLower().Equals("navy"):
                penInkTrueColor = ($"{trueColor[0].ToString().ToUpper()}{trueColor.Substring(1).ToLower()} ink is easy to read");
                break;
            case var a when a.ToLower().EndsWith("brown"):
                penInkTrueColor = ($"The color {trueColor.ToLower()} brings a sense of stability and comfort");
                break;
            case var a when a.ToLower().EndsWith("clear"):
                break;
            case var a when a.ToLower().StartsWith("silver"):
            case var b when b.ToLower().Contains("grey"):
            case var c when c.ToLower().Contains("gray"):
                penInkTrueColor = ($"The {trueColor.ToLower()} color is great for a professional-looking writing utensil that gets the job done");
                break;
            case var a when a.ToLower().EndsWith("clear"):
                break;
            case var a when !(String.IsNullOrEmpty(a)):
                penInkTrueColor = ($"{trueColor[0].ToString().ToUpper()}{trueColor.Substring(1).ToLower()} ink for bright and clear writing");
                break;
        }
    if (!String.IsNullOrEmpty(penInkTrueColor)) { Add($"PenInkTrueColor⸮{penInkTrueColor}");} 
    }
}

//"Bullet 3" "Pen Point Type & Size" 
void PenPointTypeAndSize() {
    var penPointTypeAndSize = "";
    var penPointType = R("SP-18608").Text;
    var penPointSize = R("SP-16585").Text;
    var pointTypeIsNotNull = !String.IsNullOrEmpty(penPointType);
    var pointSizeIsNotNull = !String.IsNullOrEmpty(penPointSize);

    if (pointTypeIsNotNull 
        && penPointType.ToLower().Equals("micro point")
        && pointSizeIsNotNull) {
            penPointTypeAndSize = $"{penPointSize} microo-point tip delivers crisp, precise lines";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("ultra micro")
        && pointSizeIsNotNull) {
            penPointTypeAndSize = $"{penPointType} {penPointSize} is highly precise for crisp writing";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("micro")
        && pointSizeIsNotNull) {
            penPointTypeAndSize = $"{penPointSize} {penPointType} tip delivers crisp, precise line";
    }
    else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("micro")) {
            penPointTypeAndSize = "Micro-point tip delivers crisp, precise lines";
    }
    else if (pointTypeIsNotNull
        && (
            penPointType.ToLower().Contains("medium") 
            || penPointType.ToLower().Contains("standard"))
        ) {
            penPointTypeAndSize = $"{penPointType} tip";
    }
    else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("fine")
        && pointSizeIsNotNull) {
            var tmp = penPointType.ToLower().Equals("ultra fine") ? "Ultra-fine" : penPointType.ToLower().ToUpperFirstChar();
            penPointTypeAndSize = $"{tmp} {penPointSize.ToLower().ToUpperFirstChar()} tip ensures crisp lines with every use";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("broad")
        && pointSizeIsNotNull) {
            penPointTypeAndSize = $"{penPointType.ToLower().ToUpperFirstChar()} {penPointSize} point provides easy readability";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("broad")) {
            penPointTypeAndSize = $"{penPointType.ToLower().ToUpperFirstChar()} point provides easy readability";
    }
    else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("bold")
        && pointSizeIsNotNull) {
            penPointTypeAndSize = $"{penPointSize} {penPointType.ToLower()} point provides easy readability";
    }
        else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("tip")
        && pointSizeIsNotNull) {
            penPointTypeAndSize = $"{penPointSize} {penPointType.ToLower()} delivers crisp, precise lines";
    }
    else if (pointTypeIsNotNull
        && penPointType.ToLower().Contains("tip")) {
            penPointTypeAndSize = $"{penPointType.ToLower().ToUpperFirstChar()} delivers crisp, precise lines";
    }
    else if (pointTypeIsNotNull && pointSizeIsNotNull) {
        penPointTypeAndSize = $"{penPointSize} {penPointType.ToLower()} tip delivers crisp, precise lines";
    }
    else if (pointTypeIsNotNull || pointSizeIsNotNull) {
        var tmp = pointTypeIsNotNull ? penPointType : penPointSize;
        penPointTypeAndSize =$"{tmp.ToLower().ToUpperFirstChar()} tip delivers crisp, precise lines";
    }
    if (!String.IsNullOrEmpty(penPointTypeAndSize)) {
        Add($"PenPointTypeAndSize⸮{penPointTypeAndSize}");
    }
}

    // "Bullet 4" "Pen Barrel Color & Barrel Material" 
void PenBarrelColorAndBarrelMaterial () {
    var penBarrelColorAndBarrelMaterial = "";
    var penBarrelColor = R("SP-18606").Text;
    var barrelMaterial = R("SP-4882").Text;
    var colorIsNotNull = !String.IsNullOrEmpty(penBarrelColor);
    var materialIsNotNull = !String.IsNullOrEmpty(barrelMaterial);
    if (colorIsNotNull && (penBarrelColor.ToLower().Contains("translucent")
            || penBarrelColor.ToLower().Contains("transparent")
            || penBarrelColor.ToLower().Equals("clear"))
        ) {
            var tmp = materialIsNotNull ? $"{penBarrelColor} {barrelMaterial}" : penBarrelColor;
            penBarrelColorAndBarrelMaterial = $"{tmp} barrels";
    }
    else if (colorIsNotNull && penBarrelColor.ToLower().Equals("assorted")) {
        var tmp = materialIsNotNull ? $"{penBarrelColor} {barrelMaterial}" : penBarrelColor;
        penBarrelColorAndBarrelMaterial = $"{tmp} barrels";
    } else if (colorIsNotNull) {
        var tmp = materialIsNotNull ? $"{penBarrelColor} {barrelMaterial}" : penBarrelColor;
        penBarrelColorAndBarrelMaterial =  $"{tmp} barrel";
    }
    if (!String.IsNullOrEmpty(penBarrelColorAndBarrelMaterial)) {
            Add($"PenBarrelColorAndBarrelMaterial⸮{penBarrelColorAndBarrelMaterial}");
    }
 }
 // Bullet 5 "Pack size" (if more then one)

void PenGrip() {
    var penGrip = R("SP-350752").Text;
    if (!String.IsNullOrEmpty(penGrip) && penGrip.ToLower().Equals("yes")) {
        Add("AdditionalPenGrip⸮Comfort grip for superior control");
    }
}

void PenPocketClip() {
    var penClip = R("SP-4878").Text;
    if (!String.IsNullOrEmpty(penClip) && penClip.ToLower().Equals("yes")) {
        Add("AdditionalPenClip⸮Features pocket clip for convenient carrying");
    }
}

void PenRefillable() {
    var penRefillable = R("SP-4878").Text;
    if (!String.IsNullOrEmpty(penRefillable) && penRefillable.ToLower().Equals("yes")) {
        Add("AdditionalPenRefillable⸮Refillable ink allows pens to be reused instead of being replaced to save money");
    }
}

void BarrelShape() {
    var resBarrelShape = "";
    var barrelShape = R("SP-350746").Text;
    if (!String.IsNullOrEmpty(barrelShape) && barrelShape.ToLower().Equals("hexagonal")) {
        resBarrelShape = "Features roll-proof, hexagon-shaped barrel, so it would not roll off surfaces";
    }
    else if (!String.IsNullOrEmpty(barrelShape) && barrelShape.ToLower().Equals("triangular")) {
        resBarrelShape = "Ergonomic triangular shape";
    }
    else if (!String.IsNullOrEmpty(barrelShape) && barrelShape.ToLower().Equals("hourglass")) {
        resBarrelShape = "Unique hourglass shape features a comfortable grip for a natural fit in your hand";
    }
    else if (!String.IsNullOrEmpty(barrelShape) && barrelShape.ToLower().Equals("round")) {
        resBarrelShape = "Comfortable, round barrel design";
    }
    else if (!String.IsNullOrEmpty(barrelShape)) {
        resBarrelShape = $"{barrelShape} barrel design";
    }
    if (!String.IsNullOrEmpty(resBarrelShape)) {
        Add($"AdditionalPenBarrelShape⸮{resBarrelShape}");
    }
}

void InkLevelViewingWindow() {
    var window =  Coalesce(A[5996].HasValue("ink level viewing window"));  
    var barrelColor = Coalesce(R("SP-18606").Text); 
    var clearColor = barrelColor.ToLower().In("%translucent%", "%transparent%");
    if (window) {
        var tmp = clearColor ? "Barrel windows reveal the amount of remaining ink" : "Visible ink supply, so you never run out unexpectedly";
        Add($"AdditionalPenInkLevelViewingWindow⸮{tmp}");
    }
}

void ArchivalAcidFree() {
    var tmp = Coalesce(A[5996].Values);
    if (!String.IsNullOrEmpty(tmp.ToString()) && Coalesce(A[5996].Values.Flatten()).In("archival quality") && Coalesce(A[5996].Values.Flatten()).In("acid-free")) {
        Add("AdditionalArchivalAcidFree⸮Archival quality, acid-free ink");
    }
}

void UniSuperInk() {
    var superInk = Coalesce(A[5996].HasValue("uni Super Ink"));
    if (superInk) {
        Add("AdditionalUniSuperInk⸮Features uni-Super Ink that helps prevent against check fraud and document alteration");
    }
}

void UniFlowSystem() {
    var superInk = Coalesce(A[5996].HasValue("UNI-FLOW SYSTEM"));
    if (superInk) {
        Add("AdditionalUniFlowSystem⸮Uni-flow ink system provides a consistent ink flow and clean lines");
    }
}

void LiquidInkSystem() {
    var superInk = Coalesce(A[5996].HasValue("liquid ink system"));
    if (superInk) {
        Add("AdditionalLiquidInkSystem⸮Unique liquid ink system provides even flow and smooth marking");
    }
}

//§§1114953110001 end of  "Pens" 

//§§167638614736165755 start of "Post-it&reg; & Sticky Notes" "Alex K." 

TypeOfStickyNotes();
PostItSizeColorCollection();
SheetCountPadsPack();
PopUpOrFlat();
LineType();
DispenserIncluded();
//Greener or Recycled Content (If applicable) 
// Post Consumer Content (If applicable)

// "Bullet 1" "Type of Sticky Notes (including the Theme & Adhesive) & Use" 
void TypeOfStickyNotes() {
    var typeOfStickyNotes = "";
    var stickType = !(R("SP-18402") is null) || !R("SP-18402").Text.Equals("<NULL>") ? R("SP-18402").Text : 
    !(R("cnet_common_SP-18402") is null) || !R("cnet_common_SP-18402").Text.Equals("<NULL>") ? R("cnet_common_SP-18402").Text : "";
    var adhesiveType = A[6031];
    if (!adhesiveType.HasValue("%repositionable%")) {
        typeOfStickyNotes = "Repositionable adhesive won't mark paper and other surfaces";
    }
    else if (adhesiveType.HasValue("%self-adhesive%")) {
        typeOfStickyNotes = "Stick these self-adhesive notes on papers, desks, and anywhere else you need to leave a note";
    }
    else if (adhesiveType.HasValue("full adhesive coated back")) {
        typeOfStickyNotes = "There is full adhesion across the back of each note so the whole pad stays together";
    }
    //Super Sticky|Standard|Full|Dura-Hold
    else if (!String.IsNullOrEmpty(stickType)) {
           typeOfStickyNotes = $"Stick these {stickType.ToLower()} adhesive notes on papers, desks, and anywhere else you need to leave a note"; 
    }

    if (!String.IsNullOrEmpty(typeOfStickyNotes)) {
        Add($"TypeOfStickyNotes⸮{typeOfStickyNotes}");
    }
}

// "Bullet 2" "Post-it Size & Post-it color Collection" 
void PostItSizeColorCollection() {
    var postItSizeColorCollection = "";
    var size  = !(R("SP-18403") is null) || !R("SP-18403").Text.Equals("<NULL>") ? R("SP-18403").Text : 
    !(R("cnet_common_SP-18403") is null) || !R("cnet_common_SP-18403").Text.Equals("<NULL>") ? R("cnet_common_SP-18403").Text : "";
    var width  = !(R("SP-21044") is null) || !R("SP-21044").Text.Equals("<NULL>") ? R("SP-21044").Text : 
    !(R("cnet_common_SP-21044") is null) || !R("cnet_common_SP-21044").Text.Equals("<NULL>") ? R("cnet_common_SP-21044").Text : "";// width in inches
    var length  = !(R("SP-20400") is null) || !R("SP-20400").Text.Equals("<NULL>") ? R("SP-20400").Text : 
    !(R("cnet_common_SP-20400") is null) || !R("cnet_common_SP-20400").Text.Equals("<NULL>") ? R("cnet_common_SP-20400").Text : ""; // length in inches
    var hasWidthAndLength = (!String.IsNullOrEmpty(width) && !String.IsNullOrEmpty(length));
    var colorCollection = R("SP-350290").Text;
    var paperColor = A[6022];
    if (!String.IsNullOrEmpty(size) && size.ToLower().Equals("other")
        && hasWidthAndLength) {
          postItSizeColorCollection = $"{width}\"W x {length}\"L sticky notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && paperColor.HasValue("canary yellow")
        && colorCollection.Equals("Assorted")
        && (String.IsNullOrEmpty(size) || !size.ToLower().Equals("other"))
        && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L, Canary Yellow + assorted color notes";
    }
    else if (paperColor.HasValue("%canary yellow%") && hasWidthAndLength) {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the canary yellow attach without staples, paperclips, or tape";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("bora bora")
         && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Bora Bora Collection hold stronger and longer than other notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("bali") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Bali Collection hold stronger and longer than other notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("helsinki") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L Helsinki Collection notes are just the right size to carry around or keep on your desk";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("jaipur") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L Jaipur Collection notes stand out from the crowd to give you peace of mind";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("miami") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Miami Collection are a simple tool to keep your everyday organized";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("rio de janeiro") && hasWidthAndLength) {
            postItSizeColorCollection = $"Eye-catching {width}\"W x {length}\"L Rio de Janeiro notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("rio de janeiro")) {
            postItSizeColorCollection = $"Grab even the busiest person's attention with the Rio de Janeiro Collection";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("marrakesh") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Marrakesh Collection keep your messages visible";
    }
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("new york") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L  New York notes include colors inspired by the skyline, stone and steel";
    } 
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("new york")) {
            postItSizeColorCollection = $"New York collection includes colors inspired by the skyline, stone and steel";
    }  
    else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("marseille") && hasWidthAndLength) {
            postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Marseille Collection are a simple tool to keep you organized";
    } 
    else if (!String.IsNullOrEmpty(colorCollection) && hasWidthAndLength) {
        postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the {colorCollection} Collection are a simple tool to keep you organized";
    }
    else if (colorCollection.ToLower().Contains("assorted")) {
        postItSizeColorCollection = "Assorted colors notes";
    }
    else if (!String.IsNullOrEmpty(colorCollection)) {
        postItSizeColorCollection = $"{colorCollection} Collection notes are a simple tool to keep your everyday organized";
    }

    if (!String.IsNullOrEmpty(postItSizeColorCollection)) {
        Add($"PostItSizeColorCollection⸮{postItSizeColorCollection}");
    }
}

// "Bullet 3" "Sheet Count/Pads per Pack" 
void SheetCountPadsPack() {
    var result = "";
    var pads_x_sheets = Coalesce(A[6110]);
    var TX_UOM =  Coalesce(REQ.GetVariable("TX_UOM"));
    var size = TX_UOM.HasValue("Dozen") ? 12 : TX_UOM.ExtractNumbers().Any() ? TX_UOM.ExtractNumbers().First() : 0;
    var pack = TX_UOM.HasValue() ? TX_UOM.ToString().Split("/").Last() : "";
    var sheets = 0;
    var colorCollection = R("SP-350290").HasValue() ? R("SP-350290").Replace("<NULL>", "").Text :
        R("cnet_common_SP-350290").HasValue() ? R("cnet_common_SP-350290").Replace("<NULL>", "").Text : "";
    if (pads_x_sheets.HasValue()) {
        sheets = pads_x_sheets.FirstValue().ToString().Split("x").Count() == 2 ? pads_x_sheets.FirstValue().ExtractNumbers().Last() : 0; 
    }
    if (pads_x_sheets.HasValue() && pads_x_sheets.FirstValue().ToString().Split("x").Count() == 2
        && size != 0 && sheets != 0) {
        result = $"{sheets} notes per pad; {size} pads per pack";
    }
    else if (size != 0) {
        if(TX_UOM.HasValue("Dozen")) {
            if (String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Contains("assorted")) {
                 result = "12 per pack";
            } else {
               result =  "Dozen per pack";
            }
        }
        result = $"{size} per {pack.ToLower()}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"SheetCountPadsPack⸮{result}");
    }
}

// "Bullet 4" "Pop up or Flat" 
void PopUpOrFlat() {
   var popUpOrFlat = "";
   var popFlat = !(R("SP-350594") is null) || !R("SP-350594").Text.Equals("<NULL>") ? R("SP-350594").Text : 
    !(R("cnet_common_SP-350594") is null) || !R("cnet_common_SP-350594").Text.Equals("<NULL>") ? R("cnet_common_SP-350594").Text : "";
   if (!String.IsNullOrEmpty(popFlat)) {
       if(popFlat.ToLower().Equals("pop up")) {
           popUpOrFlat = "Notes pop up one after another for one-handed dispensing";
       }
       else if (popFlat.ToLower().Equals("flat")) {
           popUpOrFlat = "Flat pads are perfect for on-the-go use";
       }
   }
   if (!String.IsNullOrEmpty(popUpOrFlat)) {
       Add($"PopUpOrFlat⸮{popUpOrFlat}");
   }
}

// "Bullet 5" "Line type" 
void LineType() {
   var lineType = "";
   var type = !(R("SP-350414") is null) || !R("SP-350414").Text.Equals("<NULL>") ? R("SP-350414").Text : 
    !(R("cnet_common_SP-350414") is null) || !R("cnet_common_SP-350414").Text.Equals("<NULL>") ? R("cnet_common_SP-350414").Text : "";
   if (!String.IsNullOrEmpty(type)) {
       if(type.ToLower().Equals("grid")) {
          lineType = "Grid lined notes are great for techies, teachers and students"; 
       }
       else if (type.ToLower().Equals("lined")) {
          lineType = "Lined Notes provide structure for your thoughts"; 
       }
       else if (type.ToLower().Equals("unlined")) {
          lineType = "Unlined notes provide plenty of room to express yourself"; 
       } 
       else {
           lineType = $"{type} notes provide structure for your thoughts";
       }
   }
   if (!String.IsNullOrEmpty(lineType)) {
       Add($"LineType⸮{lineType}");
   }
}

// "Bullet 6" "Dispenser included (If applicable)" 
void DispenserIncluded() {
   var dispenserIncluded = "";
   var yesOrNo = !(R("SP-350414") is null) || !R("SP-350414").Text.Equals("<NULL>") ? R("SP-350414").Text : 
    !(R("cnet_common_SP-350414") is null) || !R("cnet_common_SP-350414").Text.Equals("<NULL>") ? R("cnet_common_SP-350414").Text : "";
   if (!String.IsNullOrEmpty(yesOrNo) && yesOrNo.ToLower().Equals("yes")) {
     dispenserIncluded = "Dispenser keeps notes organized and easy to find";
   }
   if (!String.IsNullOrEmpty(dispenserIncluded)) {
       Add($"DispenserIncluded⸮{dispenserIncluded}");
   }
}

// "Bullet 7" "Greener or Recycled Content" "for all cat" 

// "Bullet 8" "Post Consumer Content (If applicable)" "for all cat" 

//§§167638614736165755 end of "Post-it&reg; & Sticky Notes" 

//§§168339291098380002 "Fans" "Alex K." 

FanTypeAndUse();
SpecialButtonsAndControls();
OscillatingFanPosition();
//Dimentions Adjustable
ColorFamily();
FanCordLength();
// Pack Size (If more than 1) 
AdditionalNumberOfSpeedSettings();
AdditionalMetalHousing();
AdditionalTimer();
AdditionalCarryHandle();
// warranty

// "Bullet 1" "Fan type & Use" 
void FanTypeAndUse() {
    var fanTypeAndUse = "";
    var type = !(R("SP-19036") is null) || !R("SP-19036").Text.Equals("<NULL>") ? R("SP-19036").Text : 
    !(R("cnet_common_SP-19036") is null) || !R("cnet_common_SP-19036").Text.Equals("<NULL>") ? R("cnet_common_SP-19036").Text : "";
    if (!String.IsNullOrEmpty(type) && type.ToLower().Equals("pedestal")) {
        fanTypeAndUse = "Pedestal fan is designed with space-saving structure making it to look compact";
    }
    else if (!String.IsNullOrEmpty(type) && type.ToLower().Equals("portable")) {
        fanTypeAndUse = "Designed for portable use in small rooms";
    }
    else if (!String.IsNullOrEmpty(type) && type.ToLower().Equals("floor")) {
        fanTypeAndUse = "Floor fan is suitable for use on floor";
    }
    else if (!String.IsNullOrEmpty(type) && type.ToLower().Equals("tower")) {
        fanTypeAndUse = "Tower fan works well as a floor fan, though it's also suitable for use on any stable surface";
    }
    else if (!String.IsNullOrEmpty(type) && type.ToLower().Equals("wall")) {
        fanTypeAndUse = "Can be mounted on a wall to save space";
    }
    else if (!String.IsNullOrEmpty(type) && type.ToLower().Equals("window")) {
        fanTypeAndUse = "Window fan has design that allows for use with window screen in place";
    }
    if (!String.IsNullOrEmpty(fanTypeAndUse)) {
        Add($"FanTypeAndUse⸮{fanTypeAndUse}");
    }
}

// "Bullet 2" "Special buttons and controls" 
void SpecialButtonsAndControls() {
    var specialButtonsAndControls = "";
    var controls = !(R("SP-23829") is null) || !R("SP-23829").Text.Equals("<NULL>") ? R("SP-23829").Text : 
    !(R("cnet_common_SP-23829") is null) || !R("cnet_common_SP-23829").Text.Equals("<NULL>") ? R("cnet_common_SP-23829").Text : "";
    if (!String.IsNullOrEmpty(controls) && controls.ToLower().Equals("remote control")) {
        specialButtonsAndControls = "Remote control is ideal if you do not want to interrupt your activities to switch the fan speeds";
    }
    else if (!String.IsNullOrEmpty(controls) && controls.ToLower().Equals("electronic control")) {
        specialButtonsAndControls = "Electronic controls allow you to adjust the settings with ease";
    }
    else if (!String.IsNullOrEmpty(controls)) {
        specialButtonsAndControls = $"{controls} allow you to adjust the settings with ease";
    }
    if (!String.IsNullOrEmpty(specialButtonsAndControls)) {
        Add($"SpecialButtonsAndControls⸮{specialButtonsAndControls}");
    }
}

// "Bullet 4" "Oscillating/Fan position" 
void OscillatingFanPosition() {
    var oscillatingFanPosition = "";
    var oscillating = !(R("SP-19037") is null) || !R("SP-19037").Text.Equals("<NULL>") ? R("SP-19037").Text : 
    !(R("cnet_common_SP-19037") is null) || !R("cnet_common_SP-19037").Text.Equals("<NULL>") ? R("cnet_common_SP-19037").Text : "";
    var fanPosition = !(R("SP-21508") is null) || !R("SP-21508").Text.Equals("<NULL>") ? R("SP-21508").Text : 
    !(R("cnet_common_SP-21508") is null) || !R("cnet_common_SP-21508").Text.Equals("<NULL>") ? R("cnet_common_SP-21508").Text : "";
    if (!String.IsNullOrEmpty(oscillating) && oscillating.ToLower().Equals("yes")) {
        oscillatingFanPosition = "Fan has oscillation for wide-area coverage";
    }
    else if (!String.IsNullOrEmpty(fanPosition) && fanPosition.ToLower().Equals("adjustable")) {
        oscillatingFanPosition = "Adjustable design allows you to direct airflow where you want it";
    }
     else if (!String.IsNullOrEmpty(fanPosition) && fanPosition.ToLower().Equals("fixed position")) {
        oscillatingFanPosition = "Fixed position for better stability";
    }
    if (!String.IsNullOrEmpty(oscillatingFanPosition)) {
        Add($"OscillatingFanPosition⸮{oscillatingFanPosition}");
    }
}

// "Bullet 5" "Color family" 
void ColorFamily() {
    var colorFamily = "";
    var cFamily = !(R("SP-22967") is null) || !R("SP-22967").Text.Equals("<NULL>") ? R("SP-22967").Text : 
    !(R("cnet_common_SP-22967") is null) || !R("cnet_common_SP-22967").Text.Equals("<NULL>") ? R("cnet_common_SP-22967").Text : "";
    if (!String.IsNullOrEmpty(cFamily)) {
        colorFamily = $"Comes in {cFamily}";
    }
    if (!String.IsNullOrEmpty(colorFamily)) {
        Add($"ColorFamily⸮{colorFamily}");
    }
}

// "Bullet 6" "Cord length (ft.)" 
void FanCordLength() {
    var cordLength = "";
    var length = !(R("SP-21372") is null) || !R("SP-21372").Text.Equals("<NULL>") ? R("SP-21372").Text : 
    !(R("cnet_common_SP-21372") is null) || !R("cnet_common_SP-21372").Text.Equals("<NULL>") ? R("cnet_common_SP-21372").Text : "";
    var safety = Coalesce(A[4754].HasValue("Safety Fuse Technology"));
    if (safety && !String.IsNullOrEmpty(length)) {
        cordLength = $"{length}' power cord, fused safety plug";
    }
    else if (!String.IsNullOrEmpty(length)) {
        cordLength = $"Cord Length: {length}'";
    }
    if (!String.IsNullOrEmpty(cordLength)) {
        Add($"FanCordLength⸮{cordLength}");
    }
}

// Bullet 7 Pack Size (If more than 1) 

// "Additional Bullet" "number of speed settings" 

void AdditionalNumberOfSpeedSettings() {
    var numberOfSpeedSettings = "";
    var num = !(R("SP-18992") is null) || !R("SP-18992").Text.Equals("<NULL>") ? R("SP-18992").Text : 
    !(R("cnet_common_SP-18992") is null) || !R("cnet_common_SP-18992").Text.Equals("<NULL>") ? R("cnet_common_SP-18992").Text : "";

    if (String.IsNullOrEmpty(num) && !num.Equals("1")) {
        numberOfSpeedSettings = $"Fan features {num} speed settings that are perfect for your comfort";
    }

    if (!String.IsNullOrEmpty(numberOfSpeedSettings)) {
        Add($"AdditionalNumberOfSpeedSettings⸮{numberOfSpeedSettings}");
    }
}

// 1111111  "can  be used for all categores" 1111111
// "Additional Bullet" "Metal housing" 

void AdditionalMetalHousing() {
    var metalHousing = "";
    var housing = Coalesce(A[4753].HasValue("metal housing"));
    if (housing) {
        metalHousing = "Metal housing for durability";
    }
    if (!String.IsNullOrEmpty(metalHousing)) {
        Add($"AdditionalMetalHousing⸮{metalHousing}");
    }
}

// 1111111  "can  be used for all categores" 1111111
// "Additional Bullet" "Timer" 
void AdditionalTimer() {
    var additionalTimer = "";
    var timer = Coalesce(A[4538].HasValue()) ? A[4538].FirstValue().ExtractNumbers().First() : 0;
    var units = Coalesce(A[4538].HasValue()) ? A[4538].Units.First().Name : "non"; 
    if (timer > 1  && (units.Contains("hr") || units.Equals("min"))) {
        additionalTimer = $"Included {timer} {units.Replace("hrs", "-hour").Replace("hr", "-hour").Replace("min", "-minute")} timer gives you the flexibility to save energy";
    }
    else if (timer == 1 && (units.Contains("hr") || units.Equals("min"))) {
        additionalTimer = $"Included {timer} {units.Replace("hr", "-hour").Replace("hrs", "-hour").Replace("min", "-minute")} timer gives you the flexibility to save energy";
    }
    if (!String.IsNullOrEmpty(additionalTimer)) {
        Add($"AdditionalTimer⸮{additionalTimer}");
    }
}

// "Additional Bullet" "Carry handle included" 
void AdditionalCarryHandle() {
    var carryHandle=  "";
    var handle = Coalesce(A[9898].HasValue("carry handle included"));
    if (handle) {
        carryHandle = "Fan features a handle as a carrying option making its portability easier";
    }
    if (!String.IsNullOrEmpty(carryHandle)) {
        Add($"AdditionalCarryHandle⸮{carryHandle}");
    }
}

// Compliant Standards

// Warranty

//§§168339291098380002 end of "Fans" 

//§§168412514983216257 "Juice" "Alex K." 

JuiceType();
CapacityOz();
// Pack size
Ingredients();
//SugarFree
ArtificialFlavorsOrPreservatives();
//Kosher(); 
//GlutenFree();
Vitamins();
ProductEnergy();
//CaffeineFree();
//ContainerType();
CapriSunVaietyPack();

// "Bullet 1" "Juice Type" 
void JuiceType() {
    var juiceType = "";
    var productType = Coalesce(A[6718]);
    var sparkling = Coalesce(A[6720]);
    var type = !(R("SP-22288") is null) || !R("SP-22288").Text.Equals("<NULL>") ? Coalesce(R("SP-22288").Text) : 
    !(R("cnet_common_SP-22288") is null) || !R("cnet_common_SP-22288").Text.Equals("<NULL>") ? Coalesce(R("cnet_common_SP-22288").Text) : Coalesce("");

    if (productType.HasValue("juice") && type.HasValue() && !(type.In("Variety Pack", "Milk", "Cider", "Lemonade"))) {
        if(sparkling.HasValue("yes"))  {
            juiceType = $"Sparkling {type} juice is a refreshing and delicious drink"; 
        }
        else {
             juiceType = $"{type} juice tastes good and is good for you";
        }
    }
    else if (productType.HasValue("juice") && type.HasValue("Variety Pack")) {
        juiceType = "Enjoy a sampling of your favorite juices with this variety pack";
    }
    else if (SKU.Brand.HasValue("CapriSun") && productType.HasValue("water") && type.HasValue("Variety Pack")) {
        juiceType = "Enjoy a sampling of your favorite juices with this variety pack";
    }
    else if (SKU.Brand.HasValue("CapriSun") && productType.HasValue("water") && !type.In("%Milk", "Cider", "Lemonade")) {
        juiceType = $"{type} juice tastes good and is good for you";
    }
    else if (productType.HasValue("soft drink") && !type.In("%Milk", "Variety Pack")) {
        juiceType = $"Quench your thirst with a delicious {type} drink";
    }
    else if (productType.HasValue("instant tea") && !type.In("%Milk", "Variety Pack")) {
        juiceType = $"Sweeten up that boring water with {type}-flavored tea mix";
    }
    else if (productType.HasValue("instant drink") && !type.In("%Milk")) {
        juiceType = $"{type} drink mix transforms ordinary water into a spectacular water experience";
    }
    if (!String.IsNullOrEmpty(juiceType)) {
         Add($"JuiceType⸮{juiceType}");
    }
}

// "Bullet 2" "Capacity (oz.)" 
void CapacityOz() {
    var capacityResult = "";
    var capacity = !(R("SP-21132") is null) || !R("SP-21132").Text.Equals("<NULL>") ? R("SP-21132").Text : 
    !(R("cnet_common_SP-21132") is null) || !R("cnet_common_SP-21132").Text.Equals("<NULL>") ? R("cnet_common_SP-21132").Text : "";
    if (!String.IsNullOrEmpty(capacity)) {
        capacityResult = $"Comes in capacity of {capacity} oz.";
    }
    if (!String.IsNullOrEmpty(capacityResult)) {
        Add($"CapacityOz⸮{capacityResult}");
    }
}

// "Bullet 4" "Ingredients" 
void Ingredients() {
    var result = "";
    var ingredients = Coalesce(A[6726]).WhereNot("vitamin%", "niacin");
    if (ingredients.Flatten().HasValue()) {
        var tmp = ingredients.Select(s => s.Value()).FlattenWithAnd(10, ", ");
        result = $"Made from: {tmp}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"Ingredients⸮{result}");
    }
}

// "Bullet 6" "Artificial flavors or preservatives" 
void ArtificialFlavorsOrPreservatives() {
    var result = "";
    var noPreservatives = Coalesce(A[6739].HasValue("no preservatives"));
    var noArtificialFlavors = Coalesce(A[6739].HasValue("no artificial flavors"));
    if (noPreservatives && noArtificialFlavors) {
       result = "No artificial flavors or preservatives";
    }
    else if (noPreservatives) {
        result = "No artificial flavors";
    }
    else if (noArtificialFlavors) {
        result = "Just real ingredients - no preservatives";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"ArtificialFlavorsOrPreservatives⸮{result}");
    }
}

// "Additional Vitamins" 
//Good source of vitamin C
//https://www.staples.com/Ocean-Spray-100-Orange-Juice-48-count/product_1381180
void Vitamins() {
    var result = "";
    var vitamins = Coalesce(A[6726]).Where("vitamin%", "niacin");
    if (vitamins.Flatten().HasValue()) {
        var tmp = vitamins.Select(s => s.Value()).FlattenWithAnd(10, ", ");
        result = $"Good source of {tmp}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalVitamins⸮{result}");
    }
}

// "Additional Product Energy" 
void ProductEnergy() {
    var result = "";
    var calorieFree = Coalesce(A[6739].HasValue("calorie-free"));
    var energyPerServe = Coalesce(A[11366], A[11367]);
    var energyPerServePer100ml = Coalesce(A[11368], A[11370]);
    if(calorieFree || (energyPerServe.HasValue() && energyPerServe.FirstValue() == 0) 
        || (energyPerServePer100ml.HasValue()) && energyPerServePer100ml.FirstValue()) {
       result = "Free of calories, so you can drink as much as you want without the fear of gaining weight";
    }
    else if (energyPerServe.HasValue() && energyPerServe.FirstValue() > 0) {
        result = $"Energy of drink is ##{energyPerServe.FirstValue()} {energyPerServe.Units.First().Name}";
    }
    else if (energyPerServePer100ml.HasValue() && energyPerServePer100ml.FirstValue() > 0) {
        result = $"Energy of drink is ##{energyPerServePer100ml.FirstValue()} {energyPerServePer100ml.Units.First().Name} per 100ml";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalProductEnergy⸮{result}");
    }
}
/*
// "Additional Container Type" 
void ContainerType() {
    var result = "";
    var type = !(R("SP-24477") is null) || !R("SP-24477").Text.Equals("<NULL>") ? R("SP-24477").Text : 
    !(R("cnet_common_SP-24477") is null) || !R("cnet_common_SP-24477").Text.Equals("<NULL>") ? R("cnet_common_SP-24477").Text : "";
    if (!String.IsNullOrEmpty(type)) {
        result = $"Every drink comes in {type}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalContainerType⸮{result}");
    }
}
*/

// "Additional for variety pack (capri sun)" 
void CapriSunVaietyPack() {
    var result = "";
    var brand = Coalesce(SKU.Brand);
    var type = !(R("SP-22288") is null) || !R("SP-22288").Text.Equals("<NULL>") ? Coalesce(R("SP-22288").Text) : 
    !(R("cnet_common_SP-22288") is null) || !R("cnet_common_SP-22288").Text.Equals("<NULL>") ? Coalesce(R("cnet_common_SP-22288").Text) : Coalesce("");
    var capriSun =  Coalesce(A[7338]);
    if (capriSun.HasValue("%- Capri Sun%") && brand.HasValue("CapriSun") && type.HasValue("Variety Pack")) {
        var tmp = capriSun.Where("%Capri %").Select(s => s.Value().ToString().Split(" Sun ").Last()).FlattenWithAnd(20, ",");
        result = $"{capriSun.Where("%Capri %").Count()} different flavors, including {tmp}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalCapriSunVaietyPack⸮{result}");
    }
}

//§§168412514983216257 end of "Juice" 

//§§1114953140896 -- "Markers" "Alex K." 

MarkerTypeAndUse();
MarkerInkColor();
// 3 and 4 bullet in MarkerPointType()
MarkerPointType();
// pack size
NonToxic();
Odor();

// "Bullet 1" "Marker type & Use" 
void MarkerTypeAndUse() {
    var result = "";
    var markerType = !(R("SP-22698") is null) || !R("SP-22698").Text.Equals("<NULL>") ? R("SP-22698").Text : 
    !(R("cnet_common_SP-22698") is null) || !R("cnet_common_SP-22698").Text.Equals("<NULL>") ? R("cnet_common_SP-22698").Text : "";
    var quickDry = Coalesce(A[6282]);
    if (!String.IsNullOrEmpty(markerType)) {
        if (quickDry.HasValue() && markerType.ToLower().Equals("alcohol")) {
            result = "These alcohol based markers dry fast and do not bleed";
        }
        else {
            var tmp  = markerType.ToLower();
            switch(tmp) {
                case "alcohol":
                    result = "These alcohol based markers make a great addition to any craft room or design studio";
                    break;
                case "brush tip":
                    result = "Brush tip creates fluid lines and the marker tips are perfect for consistency when coloring";
                    break;
                case "calligraphy":
                    result = "Makes lettering for Holiday, Wedding, Anniversary or other festive events easy";
                    break;
                case "chalk":
                    result = "A wonderful way to create chalkboard art to attract attention and increase sales";
                    break;
                case "dry erase":
                    result = "Dry-erase markers for writing and drawing";
                    break;
                case "wet erase":
                    result = "Washes from most fabrics and can be removed from skin with soap and water";
                    break;
                case "fabric":
                    result = "Fabric marker set for customized designs on clothing, paper, lamps, jewelry, frames and more";
                    break;
                case "art":
                    result = "Perfect for customizing your arts and crafts, accessories and more. Ideal for artists of all ages";
                    break;
                case "paint":
                    result = "These paint markers are perfect for adding a bold touch of color to any project";
                    break;
                case "permanent":
                    result = "Permanent ink formula is designed to last";
                    break;
                case "sketch":
                    result = "Sketch markers are used by manga artists, designers, and architects worldwide";
                    break;
                case "water based":
                    result = "Water-based paint markers for writing and drawing";
                    break;
                case "china":
                    result = "China marker peels for continuous use";
                    break;
                case "kid's markers":
                    result = "Great for arts and crafts, homework and school projects, journal entries, scrapbooks, and more";
                    break;
                case "window":
                    result = "Window markers are suitable for indoor and outdoor use on windows around the house or car";
                    break;
                default: 
                    result = $"{markerType} markers for writing and drawing";
                    break;
            }
        }  
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"MarkerTypeAndUse⸮{result}");
    }
}

// "Bullet 2" "Marker ink color";
void MarkerInkColor() {
    var result = "";
    var trueColor = !(R("SP-23267") is null) || !R("SP-23267").Text.Equals("<NULL>") ? R("SP-23267").Text : 
    !(R("cnet_common_SP-23267") is null) || !R("cnet_common_SP-23267").Text.Equals("<NULL>") ? R("cnet_common_SP-23267").Text : "";
    if (!String.IsNullOrEmpty(trueColor) && trueColor.ToLower().Equals("assorted")) {
        result = "Comes in assorted colors";
    }
    else if (!String.IsNullOrEmpty(trueColor) && !trueColor.ToLower().Equals("<null>")) {
        result = $"Ink comes in {trueColor.ToLower()}";
    }
    if (!String.IsNullOrEmpty(trueColor)) {
        Add($"MarkerInkColor⸮{result}");
    }
}

// "Bullet 3" "Marker point type" 

void MarkerPointType() {
    var result = "";
    var pointType = !(R("SP-16612") is null) || !R("SP-16612").Text.Equals("<NULL>") ? R("SP-16612").Text : 
    !(R("cnet_common_SP-16612") is null) || !R("cnet_common_SP-16612").Text.Equals("<NULL>") ? R("cnet_common_SP-16612").Text : "";
    var firstLineType = Coalesce(A[5981]);
    var secondLineType = Coalesce(A[5984]);

    if (pointType.ToLower().Equals("bold")) {
        result = "Bold point creates thick, strong and detailed lines. Provides high visibility for your needs";
    }
    else if (pointType.ToLower().Equals("bullet")) {
        result = "Bullet tip writes with uniform and defined lines";
    }
    else if (pointType.ToLower().Equals("extra fine")) {
        result = "Extra fine point for detailed writing applications";
    }
    else if (pointType.ToLower().Equals("fine")) {
        result  = "Fine-point tip creates thin, accurate lines";
    }
    else if (pointType.ToLower().Equals("ultra fine")) {
        result = "Ultra-fine point tip creates thin, accurate lines";
    }
    else if (pointType.ToLower().Equals("medium")) {
        result = "Medium point provides accuracy and detail";
    }
    else if (pointType.ToLower().Equals("broad")) {
        result = "Creates broad and fine lines";
    }
    else if (pointType.ToLower().Equals("super fine")) {
        result = "Write with precision and accuracy with this super-fine tip";
    }
    else if (pointType.ToLower().Equals("conical")) {
        result = "Conical tips write broad and narrow";
    }
    // "Bullet 3 and 4" "2nd marker tip type (If applicable)" 
    else if (pointType.ToLower().Equals("twin tip") 
        && firstLineType.HasValue("extra fine") 
        && secondLineType.HasValue("fine")) {
            result = "Features a fine point for thin, detailed lines on one end and an ultra-fine point for even more precise writing on the other end";
    }
    else if (pointType.ToLower().Equals("twin tip") 
        && secondLineType.HasValue("fine")) {
            result = "Dual-tip multi-purpose marker";
    }
    else if (pointType.ToLower().Equals("twin tip")
        || (firstLineType.HasValue("fine") && secondLineType.HasValue("brush"))
        || (firstLineType.HasValue("brush") && secondLineType.HasValue("fine"))) {
            result = "While fine tip allows thin and consistent lines, the brush tip can be used to draw medium to bold strokes by altering the pressure on the tip";
    }
    else if (pointType.ToLower().Equals("twin tip")
        && firstLineType.HasValue() && secondLineType.HasValue()) {
            result = $"Features both {firstLineType.FirstValue().RegexReplace("(.+)", "a $1").RegexReplace("a ultra fine", "an ultra-fine")} tip and {secondLineType.FirstValue().RegexReplace("a ultra fine", "an ultra-fine")} tip, perfect for creating a variety of looks";
    }
     else if (!String.IsNullOrEmpty(pointType)) {
        result = $"{pointType} point provides accuracy and detail";
    }   
    if (!String.IsNullOrEmpty(result)) {
        Add($"MarkerPointType⸮{result}");
    }  
}
// BUllet 5 "Pack size" 

// "Bullet 6" "NON-TOXIC" 
void NonToxic() {
    var result = "";
    var nonToxic = !(R("SP-21018") is null) || !R("SP-21018").Text.Equals("<NULL>") ? R("SP-21018").Text : 
    !(R("cnet_common_SP-21018") is null) || !R("cnet_common_SP-21018").Text.Equals("<NULL>") ? R("cnet_common_SP-21018").Text : "";
    if (nonToxic.ToLower().Equals("Yes")) {
        result = "As these markers are non-toxic, they are extremely safe to use";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NonToxic⸮{result}");
    }
}

// "Bullet 7" "Certification & Standards" 

// "Additional Low Odor" 
void Odor() {
    var result = "";
    var odor = !(R("SP-21015") is null) || !R("SP-21015").Text.Equals("<NULL>") ? R("SP-21015").Text : 
    !(R("cnet_common_SP-21015") is null) || !R("cnet_common_SP-21015").Text.Equals("<NULL>") ? R("cnet_common_SP-21015").Text : "";
    if (odor.ToLower().Equals("low odor")) {
        result = "Low-odor ink for your comfort";
    }
    else if (odor.ToLower().Equals("yes")) {
        result = "Bring this odorless markers with you to work or school for smear-free applications";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalOdor⸮{result}");
    }
}

//§§1114953140896 end of "Markers" 

//§§13529397020006 -- "Binder Accessories" "Alex K." 

BinderAccessoryTypeAndUse();
// True color & Material of Item
// Dimensions
TabIndexPunchInformation();
// Pack Size
ArchivalSafe();

// "Bullet 1" "Binder Accessory Type & Use" 
void BinderAccessoryTypeAndUse() {
    var result = "";
    //Binder Pockets|Spine Inserts|Tab Dividers|Job Ticket Holder|Sheet Lifter|Index Tabs|Zipper Pouch|Fasteners|Sheet Protectors|File Folder Frames|Reinforcement Labels
    var type = !(R("SP-18542") is null) || !R("SP-18542").Text.Equals("<NULL>") ? R("SP-18542").Text : 
    (R("cnet_common_SP-18542") is null) || !R("cnet_common_SP-18542").Text.Equals("<NULL>") ? R("cnet_common_SP-18542").Text : "";
    if (!String.IsNullOrEmpty(type)) {
        switch(Coalesce(type.ToLower())) {
            case var a when a.HasValue("<null>"):
                break;
            case var a when a.HasValue("sheet protectors"):
                result = "Sheet protectors to prevent degradation of paper inserts";
                break;
           case var a when a.HasValue("tab dividers"):
                result = "Tab dividers let you organize projects, personal documents, and more";
                break;
            case var a when a.HasValue("reinforcement labels"):
                result = "For reinforcing or repairing punched holes in loose-leaf paper";
                break;
            case var a when a.HasValue("binder pockets"):
                result = "Convenient pockets help you organize your binder";
                break;
            case var a when a.HasValue("document holder"):
                result = "Document holder to keep the documents you're working with secure and easy to see";
                break;
            case var a when a.HasValue("file folder frames"):
                result = "File folder frame expands your filing system";
                break;
            case var a when a.HasValue("file pockets"):
                result = "File pockets to sort your documents by topic";
                break;
            case var a when a.HasValue("index tabs"):
                result = "Organize your work and make specific pages easier to find with index tabs";
                break;
            case var a when a.HasValue("job ticket holder"):
                result = "Job ticket holder is ideal for storing documents, files and presentation papers";
                break;
            case var a when a.HasValue("sheet lifter"):
                result = "Sheet lifters keep file pages flat and free from curling";
                break;
            case var a when a.HasValue("card protectors"):
                result = "Card Protectors are perfect for storing cards with more protection";
                break;
            case var a when a.HasValue("zipper pouch"):
                result = "Zipper pouch is designed to store items and documents";
                break;
            case var a when a.HasValue("usb pockets"):
                result = "USB pockets hold and protect your USB drive";
                break;
            case var a when a.HasValue("spine inserts"):
                result = "Label binder spine inserts for organized and professional look";
                break;
            case var a when a.HasValue():
                var tmp = a.Text.First().ToString().ToUpper() + a.Text.Substring(1);
                result = $"{tmp} for organized and professional look";
                break;
        }
    }
    if (!String.IsNullOrEmpty(result)) {
       Add($"BinderAccessoryTypeAndUse⸮{result}"); 
    }
}

// "Bullet 2" "True color & Material of Item" 

// "Dimensions" "Bullet 3" 

// "Bullet 4" "Tab/Index/Punch Information" 

void TabIndexPunchInformation() {
    var result = "";
    var tab = A[5947];
    var numOfPunches = A[5940];
    var numOfHoles = A[6548];
    var partsQty = A[5946];
    if (Coalesce(tab.Where("%A-Z%")).HasValue()) {
        result = "A-Z dividers for alphabetical organization";
    }
    else if (Coalesce(tab.Where("Jan%Dec%", "Jan%Dez%", "Jan%Dec%", "Jan%Déc%")).HasValue()) {
        result = "Monthly tab dividers for impactful organizing";
    }
    else if (Coalesce(tab.Where("Mon-Sun", "Mon-Fri", "Mon-Sat")).HasValue()) {
        var tmp = tab.HasValue("Mon-Sun") ? "sunday" : tab.HasValue("Mon-Fri") ? "friday" : tab.HasValue("Mon-Sat") ? "saturday" : "";
        result = $"Organize your documents quickly and easily monday through {tmp}";
    }
    else if (numOfPunches.HasValue() && numOfPunches.Values.ExtractNumbers().Any()) {
        var tmp = numOfPunches.Values.ExtractNumbers().First()
            .Replace("2", "Two")
            .Replace("3", "Three")
            .Replace("4", "Four")
            .Replace("5", "Five")
            .Replace("6", "Six")
            .Replace("7", "Seven")
            .Replace("8", "Eght")
            .Replace("9", "Nine");
        result = $"{tmp}-hole punched and ready for use";
        if (partsQty.HasValue() && partsQty.FirstValue().ExtractNumbers().Any()) {
            result = $"{result}; {partsQty.FirstValue().ExtractNumbers().First()}-tab dividers";
        }
    }
    else if (numOfHoles.HasValue() && numOfHoles.Values.ExtractNumbers().First() > 1) {
        var tmp = numOfHoles.Values.ExtractNumbers().First()
            .Replace("2", "Two")
            .Replace("3", "Three")
            .Replace("4", "Four")
            .Replace("5", "Five")
            .Replace("6", "Six")
            .Replace("7", "Seven")
            .Replace("8", "Eght")
            .Replace("9", "Nine");
        result = $"{tmp}-hole punched and ready for use";
        if (partsQty.HasValue() && partsQty.FirstValue().ExtractNumbers().Any()) {
            result = $"{result}; {partsQty.FirstValue().ExtractNumbers().First()}-tab dividers";
        }    
    }
    else if (partsQty.HasValue() && partsQty.FirstValue().ExtractNumbers().First() > 1) {
        var tmp = partsQty.FirstValue().ExtractNumbers().First()
            .Replace("2", "Two")
            .Replace("3", "Three")
            .Replace("4", "Four")
            .Replace("5", "Five")
            .Replace("6", "Six")
            .Replace("7", "Seven")
            .Replace("8", "Eght")
            .Replace("9", "Nine");
        result = $"{tmp}-tab dividers";
    }    
    if (!String.IsNullOrEmpty(result)) {
       Add($"TabIndexPunchInformation⸮{result}"); 
    }
}
// "Bullet 5" "Pack Size" 

// "Additional Archival Safe" 
void ArchivalSafe() {
    var result = "";
    var archivalSafe = Coalesce(A[5945].Where("archival safe"));
    if (archivalSafe.HasValue()) {
        result = "Archival-safe for long-lasting protection";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalArchivalSafe⸮{result}");
    }
}

//§§13529397020006 end of "Binder Accessories" 

//§§1676378310636165557 -- "Notebooks" "Alex K." 

NotebookTypeAndUse();
NumSheetDimension();
RuleType();
//"Bullet 4" "True color" 
NotebookCoverMaterial();
Perforation();
NotebookBinding();
// "Pack size" 
// "Recycled Content" 
// "Additional Acid Free" 
RemovableDividers();

// "Bullet 1" "Notebook Type & Use" 
void NotebookTypeAndUse() {
    var result = "";
    var manufacturerProductType = A[6014];
    var brand = A[606];
    var numberOfSubjects = R("SP-21618").HasValue() ? R("SP-21618").Replace("<NULL>", "").Text :
		R("cnet_common_SP-21618").HasValue() ? R("cnet_common_SP-21618").Replace("<NULL>", "").Text : "";
    var notebookType = R("SP-18656").HasValue() ? R("SP-18656").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18656").HasValue() ? R("cnet_common_SP-18656").Replace("<NULL>", "").Text : "";
    if (manufacturerProductType.HasValue("%business%")) {
        result = "Business notebook helps you easily keep notes organized and in one place";
    }
    else if (manufacturerProductType.HasValue("arc customizable notebook")) {
        result = "Keep your essentials in one convenient place with the Arc customizable notebook system";
    }
    else if (manufacturerProductType.HasValue("%notebook%") && brand.HasValue("Cambridge")) {
        result = "Cambridge notebook that keeps up with your professional look and style";
    }
    else if (manufacturerProductType.HasValue("composition notebook")) {
        result = "This secure-bound composition notebook is ideal for taking notes";
    }
    else if (!String.IsNullOrEmpty(numberOfSubjects)) {
        var tmp = numberOfSubjects.Replace("1", "One")
            .Replace("2", "Two")
            .Replace("3", "Three")
            .Replace("4", "Four")
            .Replace("5", "Five")
            .Replace("6", "Six")
            .Replace("7", "Seven")
            .Replace("8", "Eight")
            .Replace("9", "Nine");
        result =  $"{tmp}-subject notebook is great for school, home, or work projects";
    }
    else if (manufacturerProductType.HasValue("composition book")) {
        result = "Composition book is great for note-taking in class or at home";
    }
    else if (!String.IsNullOrEmpty(notebookType)) {
        var tmp  = notebookType[0].ToString().ToUpper() + notebookType.Substring(1);
        result = $"{tmp} are a great place to take notes and stay organized";
    }
    else if (manufacturerProductType.HasValue("ark refill")) {
        result = "Arc refill paper";
    }

    else if (manufacturerProductType.HasValue()) {
        var tmp = manufacturerProductType.FirstValue().Replace("1", "One-")
            .Replace("2 ", "Two-")
            .Replace("3 ", "Three-")
            .Replace("4 ", "Four-")
            .Replace("5 ", "Five-")
            .Replace("6 ", "Six-")
            .Replace("7 ", "Seven-")
            .Replace("8 ", "Eight-")
            .Replace("9 ", "Nine-")
            .ToString();
        result = $"{tmp[0].ToString().ToUpper()}{tmp.Substring(1)} is a great place to take notes and stay organized";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotebookTypeAndUse⸮{result}");
    }
}

// "Bullet 2" "Number of Sheets and Sheet Dimension" 
void NumSheetDimension() {
    var result = "";
    var cnetSize = !String.IsNullOrEmpty(cnet_sheet_dimension_notebooks_notepads()) ? cnet_sheet_dimension_notebooks_notepads() : "";
    var sheetsPerPad = !(R("SP-21611") is null) || !R("SP-21611").Text.Equals("<NULL>") ? R("SP-21611").Text : 
    !(R("cnet_common_SP-21611") is null) || !R("cnet_common_SP-21611").Text.Equals("<NULL>") ? R("cnet_common_SP-21611").Text : "";
    if (!String.IsNullOrEmpty(cnetSize)) {
        if (!String.IsNullOrEmpty(sheetsPerPad)) {
            result = $"This {cnetSize} notebook has {sheetsPerPad} sheets";
        } else {
            result = $"Sheet size: {cnetSize}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NumSheetDimension⸮{result}");
    }
}

// "Bullet 3" "Rule Type" 
void RuleType() {
    //Gregg|Unruled|Law|Narrow|Graph|Dotted|Cornell|College|Wide|Quad|Pitman
    var result = "";
    var rulingType = A[6023];
    var ruleType = !(R("SP-18657") is null) || !R("SP-18657").Text.Equals("<NULL>") ? R("SP-18657").Text : 
    !(R("cnet_common_SP-18657") is null) || !R("cnet_common_SP-18657").Text.Equals("<NULL>") ? R("cnet_common_SP-18657").Text : "";
    if (rulingType.HasValue("%squared%")) {
        result = "Solve mathematical equations or create a scale drawing using a quadrille-ruled notebook";
    }
    else if (rulingType.HasValue("%Legal - ruled%")) {
        result = "Legal-ruled paper keeps handwriting neat";
    }
    else if (!String.IsNullOrEmpty(ruleType)) {
        if (ruleType.ToLower().Equals("college")) {
            result = "College-ruled for efficient use of space";
        }
        else if (ruleType.ToLower().Equals("narrow")) {
            result = "The narrow-ruled format provides plenty of space for your notes";
        }
        else if (ruleType.ToLower().Equals("wide")) {
            result = "The wide-ruled paper makes it easy to write legible notes that aren't too cramped";
        }
        else if (!ruleType.ToLower().Equals("unruled")) {
            result = $"Rule type: {ruleType.ToLower()}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"RuleType⸮{result}");
    }

}

//"Bullet 4" "True color" 

//"Bullet 5" "Notebook Cover Material" 
void NotebookCoverMaterial() {
    var result = "";
    var material = A[6024];
    var coverMateral =!R("SP-24138").Text.Equals("<NULL>") ? R("SP-24138").Text : !R("cnet_common_SP-24138").Text.Equals("<NULL>") ? R("cnet_common_SP-24138").Text : "";
    if (material.HasValue("%poly%")) {
        result = "Durable polypropylene covers protect sheets from damage";
    }
    else if (material.HasValue("%cardboard%")) {
        result = "Sturdy cardboard allows you to write without a desk surface";
    }
    else if (material.HasValue("pressboard")) {
        result = "Pressboard covers are durable and keep your notes private";
    }
    else if (!String.IsNullOrEmpty(coverMateral)) {

        result = $"{coverMateral.Replace(" cover", "").ToLower().ToUpperFirstChar().Replace("(pp)", "(PP)")} cover";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotebookCoverMaterial⸮{result}");
    }
}

// "Bullet 6" "Perforation" 
void Perforation() {
    var result = "";
    var material = A[6028];
    if (material.HasValue("Yes")) {
        result = "Micro-perforated sheets for neat and easy sheet removal";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"Perforation⸮{result}");
    }
}

// "Bullet 7" "Notebook Binding" 
void NotebookBinding() {
    var result = "";
    var bindingType = A[6017];
    //Tape|Thermal|Velobind|Spiral|Sewn
    var notebookBinding = !R("SP-24139").Text.Equals("<NULL>") ? R("SP-24139").Text : !R("cnet_common_SP-24139").Text.Equals("<NULL>") ? R("cnet_common_SP-24139").Text : "";

    if (bindingType.HasValue("casebound")) {
        result = "Case-bound construction is resilient";
    }
    else if (bindingType.HasValue("spiral-bound")) {
        result = "Spiral-bound design for easy access of sheets inside";
    }
    else if (bindingType.HasValue("wire-bound")) {
        result = "Wire binding allows book to lie flat on desk surface for maximum writing area";
    }
    else if (!String.IsNullOrEmpty(notebookBinding)) {
        if (notebookBinding.ToLower().Equals("tape")) {
            result = "Tape bound for easy, long-lasting use";
        }
        else if (notebookBinding.ToLower().Equals("sewn")) {
            result = "Sewn binding, so pages won't become loose";
        }
        else {
            result = $"Notebook binding: {notebookBinding.ToLower()}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotebookBinding⸮{result}");
    }
}

// "Bullet 8" "Pack size" 

// "Bullet 9" "Recycled Content" 

// "Additional" "Acid Free" 

// "Additional" "Removable dividers" 

void RemovableDividers() {
    var result = "";
    var checkedVal = A[6031];
    if (checkedVal.HasValue("removable dividers")) {
        result = "Removable dividers allow you to create the number of subjects you need";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalRemovableDividers⸮{result}");
    }
}

// "Additional" "Pen Holder" 
PenHolder();
void PenHolder() {
    var result = "";
    var checkedVal = A[6031];
    if (checkedVal.HasValue("pen holder")) {
        result = "Exterior pen loop ensures your pen is always within reach for quick, convenient use";
    }

    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalPenHolder⸮{result}");
    }
}

//§§1676378310636165557 end of "Notebooks" 

//§§1676393410999220835 -- "Notepads" "Alex K." 

NotepadTypeAndUse();
NotepadsSheetDimension();
NumberSheetsPerPadAndPerforation();
// Pack Size
BindingPosition();
// Rule type -- All
TrueColorAndCoverMateral();
// "Bullet 8" "Recycled Content" 

// Recycled Content
HolePunchedNotepad();
// acid free
FormsPerBook();

// "Bullet 1" "Notepad type & Use" 

void NotepadTypeAndUse() {
    var result = "";
    var type = !(R("SP-23332") is null) || !R("SP-23332").Text.Equals("<NULL>") ? R("SP-23332").Text : 
    !(R("cnet_common_SP-23332") is null) || !R("cnet_common_SP-23332").Text.Equals("<NULL>") ? R("cnet_common_SP-23332").Text : "";

    //Graph Pad|Memo Pad|Message Pad|Notepad|Steno Pad|Notepad &amp; Refill|Refill
    if (!String.IsNullOrEmpty(type)) {
        if(type.ToLower().Equals("graph pad")) {
            result = "Graph pad is great for scale, drawings, drafting, planning, engineering and technical applications";
        }
        else if(type.ToLower().Equals("memo pad")) {
            result = "Memo pad is ideal when you need to write down important information at a remote meeting or job site";
        }
        else if(type.ToLower().Equals("message pad")) {
            result = "This message pad helps you to quickly organize, distribute, and display urgent messages";
        }
        else if(type.ToLower().Equals("steno pad")) {
            result = "Steno Pad for general office note taking and shorthand applications";
        }
        else if(type.ToLower().Equals("notepad")) {
            result = "This notepad is perfect for taking notes and writing down appointments";
        }
        else {
            result = $"This {type.ToLower()} is perfect for taking notes";
        }
    } 
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotepadTypeAndUse⸮{result}");
    }   
}

// "Bullet 2" "Sheet Dimension" 

void NotepadsSheetDimension() {
    var result = "";
    var cnetSize = !String.IsNullOrEmpty(cnet_sheet_dimension_notebooks_notepads()) ? cnet_sheet_dimension_notebooks_notepads() : "";
    if (!String.IsNullOrEmpty(cnetSize)) {
        result = $"Sheet size: {cnetSize}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NotepadsSheetDimension⸮{result}");
    }
}

// "Bullet 3" "Number sheets per pad & Perforation" 

void NumberSheetsPerPadAndPerforation() {
    var result = "";
    var preforation = A[6028];
    var NumberSheetsPerPad = !(R("SP-21611") is null) || !R("SP-21611").Text.Equals("<NULL>") ? R("SP-21611").Text : 
    !(R("cnet_common_SP-21611") is null) || !R("cnet_common_SP-21611").Text.Equals("<NULL>") ? R("cnet_common_SP-21611").Text : "";

    if (!String.IsNullOrEmpty(NumberSheetsPerPad)) {
       if (preforation.HasValue()) {
           result = $"{NumberSheetsPerPad} micro-perforated sheets per notepad for tidy and efficient sheet removal";
       }
       else  {
           result = $"{NumberSheetsPerPad} sheets per pad ensure you don't run out of space for notes";
       }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"NumberSheetsPerPadAndPerforation⸮{result}");
    }
}

// "Bullet 4" "Number of pads per pack"/"Pack Size" 

// "Bullet 5" "Binding position" 

void BindingPosition() {
    var result = "";
    var bound = A[6016];
    var bindingType = A[6017];
    if (bound.HasValue() && bindingType.HasValue()) {
        var tmp = bound.FirstValue().ToString()[0].ToString().ToUpper() + bound.FirstValue().Replace(" bound", "").ToString().Substring(1);
        result = $"{tmp} {bindingType.FirstValue().ToString().ToLower()} design keeps sheets secure and easily accessible";
    }
    else if (bound.HasValue()) {
        var tmp = bound.FirstValue().ToString()[0].ToString().ToUpper() +  bound.FirstValue().ToString().Substring(1);
        result = $"{tmp} design lets you quickly flip through papers to find information";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BindingPosition⸮{result}");
    }
}   

// "Bullet 6" "Rule type"   

// "Bullet 7" "True color & Notepad Cover Material"     
void TrueColorAndCoverMateral() {
    //Gregg|Unruled|Law|Narrow|Graph|Dotted|Cornell|College|Wide|Quad|Pitman
    var result = "";
    var trueColor = !(R("SP-22967") is null) || !R("SP-22967").Text.Equals("<NULL>") ? R("SP-22967").Text : 
    !(R("cnet_common_SP-22967") is null) || !R("cnet_common_SP-22967").Text.Equals("<NULL>") ? R("cnet_common_SP-22967").Text : "";
    var coverMaterial = !(R("SP-24688") is null) || !R("SP-24688").Text.Equals("<NULL>") ? R("SP-24688").Text : 
    !(R("cnet_common_SP-24688") is null) || !R("cnet_common_SP-24688").Text.Equals("<NULL>") ? R("cnet_common_SP-24688").Text : "";
    if (!String.IsNullOrEmpty(trueColor) && !String.IsNullOrEmpty(coverMaterial)) {
        result = $"Comes in {trueColor.ToLower()} and {coverMaterial.ToLower()} cover";
    }
    else if (!String.IsNullOrEmpty(trueColor)) {
        result = $"Comes in {trueColor.ToLower()}";
    }
    else if (!String.IsNullOrEmpty(coverMaterial)) {
        var tmp = coverMaterial[0].ToString().ToUpper() + coverMaterial.Substring(1).ToLower();
        result = $"{tmp} cover";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"TrueColorAndCoverMateral⸮{result}");
    }
}   

// "Bullet 8" "Recycled Content"    

// Additional acid free

// Additional hole-punched design

void HolePunchedNotepad() {
    var result = "";
    var holePunched = A[6562];
    var numOfholes = A[6566];
    if (holePunched.HasValue("Yes") && numOfholes.HasValue("3")) {
        result = "A three-hole punched design lets you easily integrate the pad or individual sheets into a binder for archiving and storage";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"HolePunchedNotepad⸮{result}");
    }
}   

// Additional froms per book
void FormsPerBook() {
    var result = "";
    var numOfForms = A[6053];

    if (numOfForms.HasValue()) {
        result = $"{numOfForms.FirstValue()} total forms per book to record ample messages";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"FormsPerBook⸮{result}");
    }
}   

//§§1676393410999220835 end of "Notepads" 

// §§1676385310823140679 -- "Colored paper" "Alex K." 

ColoredPaperTypeAndUse();
NumberOfHoles();
ColoredPaperSheetDimentions();
PaperWeight();
PaperColorTypeAndBrightness();
// "Bullet 6" "True Color"    
// "Bullet 7" "Pack Size"    
// "Bullet 8" "Certifications & Standards"    
// "Bullet 9" "Recycled Content (%)" 
// additional acid-free

// "Bullet 1" "Paper type & Use" 
void ColoredPaperTypeAndUse() {
    var result = "";
    var mediaType = A[4759];
    var printingTechnology = A[4763];
    var paperType = R("SP-21737").HasValue() ? R("SP-21737").Replace("<NULL>", "").Text :
        R("cnet_common_SP-21737").HasValue() ? R("cnet_common_SP-21737").Replace("<NULL>", "").Text : "";
    if (mediaType.HasValue("%ply%collated%paper%")) {
        result = "{mediaType.FirstValue()} is ideal for every day use and special occasions";
    }
    else if (String.IsNullOrEmpty(paperType)) {
        result = $"Reliable {paperType.ToLower()} paper is perfect for everyday use";
    }
    else if (printingTechnology.HasValue()) {
        result = $"Works great in your {printingTechnology.Values.Select(s => s.Value()).FlattenWithAnd().Replace("and", "or")} printer";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"ColoredPaperTypeAndUse⸮{result}");
    }
}

// "Bullet 2" "Number of holes"    

void NumberOfHoles() {
    var holePunnched = A[4783];
    var arr = new List<string>();
    if (DC.KSP.GetLines().Flatten(" ").HasValue()) {
        arr.Add(DC.KSP.GetLines().Flatten(" ").ToString());
    }
    if (DC.MKT.GetLines().Flatten(" ").HasValue()) {
        arr.Add(DC.MKT.GetLines().Flatten(" ").ToString());
    }
    if (DC.WIB.GetLines().Flatten(" ").HasValue()) {
        arr.Add(DC.WIB.GetLines().Flatten(" ").ToString());
    }
    if (DC.FEAT.GetLines().Flatten(" ").HasValue()) {
        arr.Add(DC.FEAT.GetLines().Flatten(" ").ToString());
    }
    if (REQ.GetVariable("CNET_SD").HasValue()) {
        arr.Add(REQ.GetVariable("CNET_SD").ToString());
    }
    if (REQ.GetVariable("CNET_HL").HasValue()) {
        arr.Add(REQ.GetVariable("CNET_HL").ToString());
    }
    if (!(SKU.ProductLineName is null)) {
       arr.Add(SKU.ProductLineName.ToString());
    }
    if (!(SKU.ModelName is null)) {
       arr.Add(SKU.ModelName.ToString());
    }
    if (!(SKU.Description is null)) {
        arr.Add(SKU.Description.ToString());
    }
    // parcing every element in arr
    var result = new List<string>();

    foreach (var item in arr) {
        var checkAWG = item.Replace(" holes", "_hole")
            .Replace("-holes", "_hole")
            .Replace(" hole", "_hole")
            .Replace("-hole", "_hole");
        if (checkAWG.Contains("_hole")) {
            var splited = checkAWG.Split(" ");
            foreach (var subStr in splited) {
                if (subStr.Contains("_hole")) {
                    var checkNum = Coalesce(subStr);
                    if (checkNum.Replace("_hole", "").ExtractNumbers().Any()) {
                        result.Add(checkNum.Replace("_hole", ""));
                    }
                }
            }
        }
    }
    if (holePunnched.HasValue("%-hole punched%")) {
        var tmp = holePunnched.Where("%-hole punched%").First().Value().ExtractNumbers().First();
        Add($"NumberOfHoles⸮{tmp} holes paper");
    }
    else if (result.Count() > 0 && result.First().Equals("1")) {
        Add($"NumberOfHoles⸮{result.First()} hole paper");
    }
    else if (result.Count() > 0) {
        Add($"NumberOfHoles⸮{result.First()} holes paper");
    }
}

// "Bullet 3" "Sheet dimension"    

// Cnet Size for colored paper
string ColoredPaperCnetSize() {
    var result = "";
    var vortexSize = A[4765];
    var itemSize = R("SP-18229").HasValue() ? R("SP-18229").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18229").HasValue() ? R("cnet_common_SP-18229").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(itemSize) && !itemSize.ToLower().Equals("other")) {
        result = itemSize;
    }
    else if (vortexSize.HasValue()) {
        result = vortexSize.FirstValueUsm().Replace(" in", "\"");
    }
    if (!String.IsNullOrEmpty(result)) {
        return result;
    } else {
        return "";
    }
}

void ColoredPaperSheetDimentions() {
    var result = "";
    var size = Coalesce(ColoredPaperCnetSize());
    if (size.HasValue()) {
        result = $"Dimensions: {size.RegexReplace(@"(\d*"")(\sx\s)(\d*"")", "$1W$2$3L" )}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"ColoredPaperSheetDimentions⸮{result}");
    }
}

// "Bullet 4" "Paper Weight (lbs.)"    

void PaperWeight() {
    var result = "";
    var weight =R("SP-18379").HasValue() ? R("SP-18379").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18379").HasValue() ? R("cnet_common_SP-18379").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(weight)) {
       var units = "lbs.";
       if (weight.Equals("1")) {
           units = "lb."; 
       } else {
           result = $"Paper weight: {weight} {units}";
       }
    }
    if (!String.IsNullOrEmpty(result)) {
       Add($"PaperWeight⸮{result}");
    }
}

// "Bullet 5" "Paper color, type & brightness"    

void PaperColorTypeAndBrightness() {
    var result = "";
    var paperColorType = R("SP-350854").HasValue() ? R("SP-350854").Replace("<NULL>", "").Text :
        R("cnet_common_SP-350854").HasValue() ? R("cnet_common_SP-350854").Replace("<NULL>", "").Text : "";
    var paperBrightness = R("SP-18379").HasValue() ? R("SP-18379").Replace("<NULL>", "").Text :
        R("cnet_common_SP-18379").HasValue() ? R("cnet_common_SP-18379").Replace("<NULL>", "").Text : "";

    if (!String.IsNullOrEmpty(paperColorType) && paperColorType.ToLower().Equals("brights")
        && !String.IsNullOrEmpty(paperBrightness)) {
            result = $"Bright color with {paperBrightness} brightness";
    }
    else if (!String.IsNullOrEmpty(paperColorType) && paperColorType.ToLower().Equals("pastels")
        && !String.IsNullOrEmpty(paperBrightness)) {
            result = $"Pastel color with {paperBrightness} brightness";
    }
    else if (!String.IsNullOrEmpty(paperColorType) && paperColorType.ToLower().Equals("brights")) {
        result = "Bright-colored paper is ideal for direct mail, flyers and office or school projects";
    } 
    else if (!String.IsNullOrEmpty(paperColorType) && paperColorType.ToLower().Equals("brights")) {
        result = $"This pastel paper is ideal for prints with moderate solid areas, graphics and saturated colors";
    }
    else if (!String.IsNullOrEmpty(paperBrightness)) {
        result = $"Brightness rating of {paperBrightness} for sharp, clear print results";
    }
    if (!String.IsNullOrEmpty(result)) {
       Add($"PaperColorTypeAndBrightness⸮{result}");
    }
}

// "Bullet 6" "True Color"    

// "Bullet 7" "Pack Size"    

// "Bullet 8" "Certifications & Standards"    

// "Bullet 9" "Recycled Content (%)" 

// additional acid-free

// §§1676385310823140679 end of "Colored paper" 

// §§1681361910108140557 "Label Maker Tapes & Printer Labels" "Alex K." 

// "Bullet 1" "Label maker tape width (Inches) & overall cartridge yield" 
LabelMakerTapeWidth();
PrintTrueColorOnLabelTrueColor();
CompatibleDevices();
UseSurfacesApplicationsAdhesiveEtc();
// Pack Size
WaterResistant();
TearResistant();
ColdEnvironment();
OilResistance();

// --[FEATURE #1]
// --Flags or Tab type & Use
void LabelMakerTapeWidth() {
    var result = "";
    var size = A[4765];
    var isTape = A[4759];
    if (size.HasValue("%Roll%") 
        && size.HasValue("%m)%") 
        && isTape.HasValue("%tape%")
        && size.FirstValue().ExtractNumbers().Any()) {
            var tmp1 = Math.Round((double)size.FirstValue().ExtractNumbers().First() * 0.393701, 2);
            var tmp2 = Math.Round((double)size.FirstValue().ExtractNumbers().First() * 3.28084, 2);
            result = $"Create labels for home, work, or school with this {tmp1}\" wide and {tmp2}' yield label tape";
        }
    if(!String.IsNullOrEmpty(result)) {
        Add($"LabelMakerTapeWidth{result}");
    }
}
// --[FEATURE #2]
// --True color (print True color on label True color) 

void  PrintTrueColorOnLabelTrueColor() {
    var result = "";
    // Label Printer Labels|Labeler Accessories|Label Maker Tapes
    var type =R("SP-21844").HasValue() ? R("SP-21844").Replace("<NULL>", "").Text :
        R("cnet_common_SP-21844").HasValue() ? R("cnet_common_SP-21844").Replace("<NULL>", "").Text : "";
    var trueColor = A[373];
    var colorFamily = R("SP-17441").HasValue() ? R("SP-17441").Replace("<NULL>", "").Text :
        R("cnet_common_SP-17441").HasValue() ? R("cnet_common_SP-17441").Replace("<NULL>", "").Text : ""; // Assorted

    if (!String.IsNullOrEmpty(type) && type.ToLower().Contains("labels") && trueColor.HasValue()) {
        if (trueColor.HasValue("black%on%white")) {
            result = "Black on white labels make text easy to read";
        }
        else if (trueColor.HasValue("white") && trueColor.HasValue("blue")) {
            result = "White labels with blue print make text easy to read";
        }
        else if ((trueColor.HasValue("%clear%") || trueColor.HasValue("%transparent%"))
        && trueColor.Values.Count() > 1) {
            var tmp = trueColor.WhereNot("%transparent%", "%clear%").First().Value().ToLower().ToUpperFirstChar();
            result = $"{tmp} print on clear label for legibility";
        }
         else if (trueColor.HasValue("% on %") && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            var tmp = trueColor.Where("% on %").First().Value().ToLower().ToUpperFirstChar();
            result = $"{tmp} print label for legibility";
        }
        else if (trueColor.HasValue() && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
        else if (trueColor.HasValue() && String.IsNullOrEmpty(colorFamily)) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
    }
    else if (!String.IsNullOrEmpty(type) && type.ToLower().Contains("tape") && trueColor.HasValue()) {
        if (trueColor.HasValue("black%on%white")) {
            result = "Black print on a white tape creates an easy-to-read text";
        }
        else if (trueColor.HasValue("black") && trueColor.HasValue("silver")) {
            result = "Black print on a silver tape creates an easy-to-read text";
        }
        else if (trueColor.HasValue("black%on%blue")) {
            result = "Black print on a blue tape ensures high visibility on light-colored surfaces";
        }
        else if (trueColor.HasValue("black%on%yellow")) {
            result = "Black print on yellow tape for easy identification";
        }
        else if ((trueColor.HasValue("%clear%") || trueColor.HasValue("%transparent%"))
        && trueColor.Values.Count() > 1) {
            var tmp = trueColor.WhereNot("%transparent%", "%clear%").First().Value().ToLower().ToUpperFirstChar();
            result = $"{tmp} print on clear tape for legibility";
        }
        else if (trueColor.HasValue("% on %") && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            var tmp = trueColor.Where("% on %").First().Value().ToLower().ToUpperFirstChar();
            result = $"{tmp} tape for legibility";
        }
        else if (trueColor.HasValue() && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
        else if (trueColor.HasValue() && String.IsNullOrEmpty(colorFamily)) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
    }
    else if (!String.IsNullOrEmpty(type) && trueColor.HasValue()) {
        if (trueColor.HasValue() && !String.IsNullOrEmpty(colorFamily) && !colorFamily.ToLower().Equals("assorted")) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
        else if (trueColor.HasValue() && String.IsNullOrEmpty(colorFamily)) {
            result = $"Comes in {trueColor.Values.Flatten("/")}";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"PrintTrueColorOnLabelTrueColor⸮{result}");
    }
}

// --[FEATURE #3]
// --Compatible devices; if more than 3 lines: list top 5 and refer to Extended description for more  

void CompatibleDevices() {
    var result = "";
    var cp = Coalesce(RP.GetCiEs(200), RP.GetCiMs(200));
    if (cp.HasValue()) {
        result = $"Compatible with: {cp.ToString().Split(", ").Take(5).Flatten(", ")}";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"CompatibleDevices⸮{result}");
    }
}

// --[FEATURE #4]
// --Use; surfaces, applications, adhesive, etc.   

void UseSurfacesApplicationsAdhesiveEtc() {
    var result = "";
    var termal = A[4763];
    var laminated = A[6073];
    var adhesive = A[10619];

    if (termal.HasValue("direct thermal")) {
        result = "Uses direct thermal printing technology for mess-free printing without toner or ink";
    }
    else if (termal.HasValue("%thermal%")) {
        result = "Thermal print technology requires no ink or toner";
    }
    else if (laminated.HasValue("%laminated%")) {
        result = "Features exclusive lamination for added protection";
    }
    else if (adhesive.HasValue()) {
        result = "Adhesive backing for quick application";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"UseSurfacesApplicationsAdhesiveEtc⸮{result}");
    }
}

// --[FEATURE #5]
// --Label maker pack size

// --[FEATURE #6]
// --additional water

void WaterResistant() {
    var result = "";
    var waterКesistant = A[4783];
    if (waterКesistant.HasValue("%water%")) {
        result = "Water-resistant for durability";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"WaterResistant⸮{result}");
    }
}

// --[FEATURE #7]
// --additional tear resistant

void TearResistant() {
    var result = "";
    var waterКesistant = A[4783];
    if (waterКesistant.HasValue("%tear%")) {
        result = "Tear-resistant for absolute safe keeping";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"TearResistant⸮{result}");
    }
}

// --[FEATURE #8]
// --additional cold environment

void ColdEnvironment() {
    var result = "";
    var waterКesistant = A[4783];
    if (waterКesistant.HasValue("%cold%")) {
        result = "Can withstand cold environments";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"ColdEnvironment⸮{result}");
    }
}

// --[FEATURE #9]
// --additional oil resistance

void OilResistance() {
    var result = "";
    var waterКesistant = A[4783];
    if (waterКesistant.HasValue("%oil%")) {
        result = "Has great resistance against oil";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"OilResistance⸮{result}");
    }
}

// §§1681361910108140557 end of "Label Maker Tapes & Printer Labels" 

//§§135293970167205 "Binders" "Alex K." 

BinderTypeDurabilityColor();
// Dimentions
RingTypeClosure();
BinderCapacity();
InputSize();
MaterialOfItemFinish();
DesignIncludesPockets();
//pack size
//Recycled
IncludedLabels();
DuraHinge();
HandleStrap();

// --[FEATURE #1]
// --Binder Type, Durability & Color
void BinderTypeDurabilityColor() {
    var result = "";
    //Post Binders|Flexible Poly Binders|Hanging Binder|Binding Case|Mini Binders|Special Application Binder|Micro Binders|A4 Binders|Zipper Binders|View Binders|Fashion Binders|Non-View Binders|Ledger Binders|Legal Binders|Better Binders
    var binderType = R("SP-19663").HasValue() ? R("SP-19663").Replace("<NULL>", "").Text : R("cnet_common_SP-19663").HasValue() ? R("cnet_common_SP-19663").Replace("<NULL>", "").Text : "";
    var trueColor = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text :  "";
    //Standard|Heavy Duty|Economy
    var durabilityType = R("SP-21022").HasValue() ? R("SP-21022").Replace("<NULL>", "").Text : R("cnet_common_SP-21022").HasValue() ? R("cnet_common_SP-21022").Replace("<NULL>", "").Text : "";
    if (!String.IsNullOrEmpty(durabilityType)) {
        durabilityType = $"with {durabilityType.ToLower()} durability ";
    }
    if (!String.IsNullOrEmpty(binderType)) {
        if (!String.IsNullOrEmpty(trueColor) && binderType.ToLower().Equals("view binders")) {
            var tmp = trueColor[0].ToString().ToUpper() + trueColor.Substring(1);
            result = $"{tmp} view binder {durabilityType}is an excellent way to store spreadsheets";
        }
        else if (binderType.ToLower().Equals("ledger binders")) {
            result = $"Ledger binders {durabilityType}is ideal for large spreadsheets, blueprints, engineering, mining, construction plans" ;
        }
        else if (binderType.ToLower().Equals("flexible poly binders")) {
            result = $"Flexible poly construction {durabilityType}for easy carrying";
        }
        else if (!String.IsNullOrEmpty(trueColor)) {
             var tmp = trueColor[0].ToString().ToUpper() + trueColor.Substring(1);
             result = $"{tmp} {binderType.ToLower()} {durabilityType}protects and organizes work documents";
        }
        else {
            var tmp = binderType[0].ToString().ToUpper() + binderType.Substring(1);
            result = $"{tmp} {durabilityType}protects and organizes work documents";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BinderTypeDurabilityColor⸮{result}");
    }
}

// --[FEATURE #2]
// --Dimensions: Height in Inches "x" Width in Inches "x" Depth in Inches

// --[FEATURE #3]
// --Closure (Ring type, Number of Rings & Locking Mechanism)

void RingTypeClosure() {
    var result = "";
    var numberOfRings = R("SP-130").HasValue() ? R("SP-130").Replace("<NULL>", "").Text : R("cnet_common_SP-130").HasValue() ? R("cnet_common_SP-130").Replace("<NULL>", "").Text : "";
    //D-Ring|Round Ring|Slant Ring|Grip|Straight Post|One Touch
    var ringType = R("SP-19662").HasValue() ? R("SP-19662").Replace("<NULL>", "").Text : R("cnet_common_SP-19662").HasValue() ? R("cnet_common_SP-19662").Replace("<NULL>", "").Text : "";
    var gapless = A[5921].HasValue("%gapless%");
    if (!String.IsNullOrEmpty(ringType)) {
        var plural = false;
        if (!String.IsNullOrEmpty(numberOfRings)) {
            if (Coalesce(numberOfRings).ExtractNumbers().First() > 1) {
                plural = true;
            }
            if (ringType.ToLower().StartsWith("d-ring"))  {
                if (gapless) {
                    if (plural) {
                        result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} gapless D-rings keep pages secure";
                    } else {
                        result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} gapless D-ring keep pages secure";
                    }
                } else {
                    if (plural) {
                        result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} D-rings keep pages secure";
                    } else {
                        result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} D-ring keep pages secure";
                    }
                }
            }
            else if (ringType.ToLower().StartsWith("ring")) {
                if (plural) {
                    result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType.ToLower()).Pluralize()} keep pages secure";
                } else {
                    result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType.ToLower())} keep pages secure";
                }
            }
            else {
                if (plural) {
                    result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType).ToLower()} rings keep pages secure";
                } else {
                    result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType).ToLower()} ring keep pages secure";
                }
            }
        }
        else {
            if (ringType.ToLower().Equals("one touch")) {
                result = "One touch rings open and close with ease and keep pages secure";
            }  
            else if (ringType.ToLower().Equals("d-ring")) {
                result = "D-rings have a higher page capacity compared to same-size round rings";
            }
            else if (ringType.ToLower().Equals("round ring")) {
                result = "Equipped with round rings for easy page turning";
            }
            else if (ringType.ToLower().Equals("ring binder")) {
                result = "Binder with rings provides document protection and a professional appearance";
            }
            else {
                result = $"Binder with {ringType.ToLower().Replace("ring", "").Replace("rings", "")} rings provides document protection and a professional appearance";
            }
        }
    } 

    if (!String.IsNullOrEmpty(result)) {
        Add($"RingTypeClosure⸮{result}");
    }
}

// --[FEATURE #4]
// --Capacity (Number of pages)

void BinderCapacity() {
    var result = "";
    var capacity = A[6576];
    var capacity2 = A[5928];

    if (capacity.HasValue()) {
        result = $"Holds up to {capacity.FirstValue()} {capacity.Units.First().Name}";
    }
    else if (capacity2.HasValue()) {
        var tmp = capacity2.Values.First().ValueUSM.Replace(" in", "\"");
        result = $"Holds up to {tmp} documents";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"BinderCapacity⸮{result}");
    }
}

// --[FEATURE #5]
// --Input Size
void InputSize() {
    var result = "";
    //8.5" x 11" (US letter)|8" x 11"|14.88" x 11" (Continuous)|3" x 4"|14.88" x 10.98"|Other|check|15" x 18"|8.5" x 14" (legal)|8" x 10"|5" x 7"|13" x 19"|5.5" x 8.5"|12" x 18"|8" x 5"|8" x 10.5"|11" x 9.0625"|6" x 9"|8.5" x 11.75"|12" x 8.5"|11" x 14"|4" x 6"|12" x 15"|9" x 12"|13" x 16.75"|12" x 12"|6.5" x 8.5"|9.5" x 11"|3.5" x 5.5"|7.75" x 5"|A4|3" x 5"|11" x 17" 
    var folderOrPaperSize = R("SP-18288").HasValue() ? R("SP-18288").Replace("<NULL>", "").Text : R("cnet_common_SP-18288").HasValue() ? R("cnet_common_SP-18288").Replace("<NULL>", "").Text : "";
    var supportedFormat = A[5925]; // Multi Value
    var supportedFormatUSM = "";
    if (supportedFormat.HasValue() && supportedFormat.FirstValueUsm().ToString().Contains(" in")) {
            supportedFormatUSM = supportedFormat.FirstValueUsm().ToString();
    }
    if (!String.IsNullOrEmpty(folderOrPaperSize)) {
        if (folderOrPaperSize.ToLower().Contains("11\" x 17\"") 
        || supportedFormatUSM.Contains("Ledger") 
        || supportedFormatUSM.Contains("8.5 in x 11 in")) {
           result = "Accommodates ledger-size pages";
        }
        else if (folderOrPaperSize.ToLower().Contains("legal") 
        || supportedFormatUSM.Contains("8.5 in x 14 in") 
        || supportedFormatUSM.Contains("Legal")) {
           result = "Accommodates legal-size pages";
        }
        else if (folderOrPaperSize.ToLower().Contains("us letter") 
        || supportedFormatUSM.Contains("Letter") 
        || supportedFormatUSM.Contains("8.5 in x 11 in")) {
           result = "Accommodates letter-size pages";
        }
        else if (folderOrPaperSize.ToLower().Contains("check")) {
            result = "Protect checks and keep them organized";
        }

        else if (!String.IsNullOrEmpty(folderOrPaperSize) && !folderOrPaperSize.ToLower().Contains("other")) {
            result = $"Accommodates {folderOrPaperSize.Replace(" (Continuous)", "")} size pages";
        }
    }
    else if (!String.IsNullOrEmpty(supportedFormatUSM)) {
        var tmp = Coalesce(supportedFormatUSM.Split("(").Last()).Replace(" in", "\"").Replace(")", "");
        result = $"Accommodates {tmp} size pages";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"InputSize⸮{result}");
    }
}

// --[FEATURE #6]
// --Material of Item & Finish
void MaterialOfItemFinish() {
    var result = "";
    var material = "Some(pvc) ";//R("SP-21408");
    var surface = A[5945];
    if (!String.IsNullOrEmpty(material)) {
        var tmp = material.ToLower().Replace("(abs)", "(ABS)").Replace("(pvc)", "(PVC)");
        if (surface.HasValue("%non%stick%")) {
            result = $"Made of {tmp} with non-stick surface";
        }
        else if (surface.HasValue("%surface%")) {
            var tmp1 = surface.Where("%surface%").First().Value();
            result = $"Made of {material.ToLower()} with {tmp1}";
        }
        else {
            result = $"Made of {tmp}";
        }
    }     
    if (!String.IsNullOrEmpty(result)) {
        Add($"MaterialOfItemFinish⸮{result}");
    }
}

// --[FEATURE #7]
// --Design (includes pockets)

void DesignIncludesPockets() {
    var result = "";
    var designFeature = A[5945];
    var design = A[5965];

    if (designFeature.HasValue("retractable hooks")) {
        result = "Retractable storage hooks for single-point or drop-file systems";
    }
    else if (design.HasValue()) {
        var tmp  = design.WhereNot("%spine%","front cover full size pocket", "back cover full size pocket", "front cover pocket", "back cover pocket").Match(5971, 5965).Values("x").Where(o => o.In("%inner%pocket%")).Flatten(" ").Replace("<NULL>", "1");
        //Add(tmp);
        if (tmp.ExtractNumbers().Any() 
        && tmp.ExtractNumbers().Sum() == 1) {
            result = "One interior pocket for added organization";
        }
        else if (tmp.ExtractNumbers().Any()
        && tmp.ExtractNumbers().Sum() > 1) {
            result = $"{tmp.ExtractNumbers().Sum()} interior pockets for added organization"; 
        }
        else if (design.Where("%inner%pocket%").Count() > 1
        && !Coalesce(design.WhereNot("%inner%pocket%","front cover full size pocket", "back cover full size pocket", "front cover pocket", "back cover pocket", "%spine%")).HasValue()) {
            result = $"{design.Where("%inner%pocket%").Count()} interior pockets for added organization";
        }
        else if (design.Where("%pocket%").Count() == 1 
        && !Coalesce(design).HasValue("%spine%")
        && design.WhereNot("%spine%").Match(5971, 5965).Values("x").Where(o => o.In("%pocket%")).Flatten().Replace("<NULL>", "1").ExtractNumbers().Any()
        && design.WhereNot("%spine%").Match(5971, 5965).Values("x").Where(o => o.In("%pocket%")).Flatten().Replace("<NULL>", "1").ExtractNumbers().Max() < 2) {
            var tmp1 = design.WhereNot("%spine%").Match(5971, 5965).Values("x") // get items where not "spine" 
                .Where(o => o.In("%pocket%")) // checked item for pockets
                    .Take(3)
                    .Distinct()
                    .FlattenWithAnd(3, " ")
                    .Replace("4", "Four")
                    .Replace("full size", "full-size")
                    .Replace(" pockets", "")
                    .Replace(" pocket", "")
                    .Replace("<NULL>", "")
                    .ToString();
            tmp1 = tmp1[0].ToString().ToUpper() + tmp1.Substring(1).ToLower();
            result = $"{tmp1}  pocket for added organization";
        }
        else if (design.WhereNot("%spine%").Match(5971, 5965).Values("x").Where(o => o.In("%pocket%")).Flatten().Replace("<NULL>", "1").ExtractNumbers().Any()
        && design.WhereNot("%spine%").Match(5971, 5965).Values("x").Where(o => o.In("%pocket%")).Flatten().Replace("<NULL>", "1").ExtractNumbers().Sum() > 0) {
            var tmp1 = design.WhereNot("%spine%").Match(5971, 5965).Values("x") // get items where not "spine" 
                .Where(o => o.In("%pocket%")) // checked item for pockets
                .Flatten()
                .Replace("<NULL>", "1")
                .ExtractNumbers()
                .Sum()
                .ToString();
            tmp1 = tmp1.Equals("1") ? "One pocket" : $"{tmp1} pockets";
            result = $"{tmp1} for added organization";
        }
        else if (design.Where("%pocket%").Count() > 0) {
            var tmp1 = design.Where("%pocket%").Count().ToString();
            tmp1 = tmp1.Equals("1") ? "One pocket" : $"{tmp1} pockets";
            result = $"{tmp1} for added organization";
        }

    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"DesignIncludesPockets⸮{result}");
    }
}

// --[FEATURE #8]
// --Binder Pack Size (If more than 1)

// --[FEATURE #9]
// --Additional Recycled

// --[FEATURE #10]
// --Additional Lables

void IncludedLabels() {
    var result = "";
    var labels = Coalesce(A[5942]);
    var spineLabelHolder = A[6577];
    if (labels.HasValue()) {
        var tmp = labels.Values.Select(o => o.Value().Replace(" label", "")).FlattenWithAnd(10, ", ");
        if (labels.Values.Count() > 1) {
            result = $"Insert custom {tmp} labels for easy identification";
        }
        else {
            result = $"Insert custom {tmp} labelsfor easy identification";
        }
    }
    else if (spineLabelHolder.HasValue("Yes")) {
        result = "Insert a custom label into the spine pocket for easy identification";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"IncludedLabels⸮{result}");
    }
}

// --[FEATURE #11]
// --Additional DuraHinge

void DuraHinge() {
    var result = "";
    var duraEdgeHinge = Coalesce(A[5945]);
    if (duraEdgeHinge.Where("%Dura%").Count() > 1) {
        result = "Binder features a ##DuraHinge design that's stronger, lasts longer, and resists tearing and a ##DuraEdge that makes the sides and top more pliable to resist splitting";
    }
    else if (duraEdgeHinge.HasValue("DuraHinge")) {
        result = "Binder features a ##DuraHinge design that's stronger, lasts longer, and resists tearing"; 
    }
    else if (duraEdgeHinge.HasValue("DuraEdge")) {
        result = "##DuraEdge makes the sides and top more pliable to resist splitting";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"DuraHinge⸮{result}");
    }
}

// --[FEATURE #12]
// --Additional Handle $ Strap

void HandleStrap() {
    var result = "";
    var shoulderStrap = A[5923];
    var handles = A[7065];
    if (shoulderStrap.HasValue("%shoulder%strap%") && handles.HasValue()) {
        result = "Comes with a handle and shoulder strap, so it is more comfortable to tote this binder around";
    }
    else if (shoulderStrap.HasValue("%shoulder%strap%")) {
        result = "Comes with shoulder strap, so it is more comfortable to tote this binder around without a backpack or a case"; 
    }
    else if (handles.HasValue()) {
        result = "Comes with a handle, so it is more comfortable to tote this binder around without a backpack or a case";
    }
    if(!String.IsNullOrEmpty(result)) {
        Add($"HandleStrap⸮{result}");
    }
}

//§§135293970167205 end of "Binders" 

