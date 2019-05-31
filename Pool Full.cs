//§§ "Functions"

//Get reference from item or from common

string GetReference(string referenceCode){
    return REQ.GetVariable(referenceCode).HasValue() ? REQ.GetVariable(referenceCode) :
        R(referenceCode).HasValue() ? R(referenceCode) : R($"cnet_common_{referenceCode}");
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
            if(tmp != ""){
                numbers.Add(double.Parse(tmp));
            }
            tmp = "";
        }
        if(a == str.Length - 1){
            if(tmp != ""){
                numbers.Add(double.Parse(tmp));
            }
        }
        a++;
    }
    return numbers;
}

//Analog Shorten on Templex

string Shorten(string text, int length, char separator)
{
    if (text.Length <= length)
    {
        return text;
    }
    else if (text != ""
    && length > 0)
    {
        int lastSpaceBeforeMax = text.LastIndexOf(separator, length);
        if(lastSpaceBeforeMax != -1){
            return text.Substring(0, lastSpaceBeforeMax);
    }
    else
        return "Try a larger length";
    }
    return null;
}

// end of "Functions" §§

//§§ "Features for all Category BEGIN"

Recycled_Post_Consumer_Content();
Pack_Size();
Compliant_Standards();
Warr_anty();
CableLength();
Dimen_sions();
ShippingDimensions();
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
    var RecycledContentRef = R("SP-21623").HasValue() ? R("SP-21623") : R("cnet_common_SP-21623");
    var PostConsumerContentRef = R("SP-21624").HasValue() ? R("SP-21624") : R("cnet_common_SP-21624");
    
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

// Pack Size for all 
void Pack_Size() { 
var result = ""; 
var containerTypeResult = ""; 
// for juices use container type 
var containerType = !(R("SP-24477") is null) || !R("SP-24477").Text.Equals("<NULL>") ? R("SP-24477").Text : 
!(R("cnet_common_SP-24477") is null) || !R("cnet_common_SP-24477").Text.Equals("<NULL>") ? R("cnet_common_SP-24477").Text : ""; 
var colorFamily = R("SP-17441").HasValue() ? R("SP-17441").Replace("<NULL>", "").Text : 
R("cnet_common_SP-17441").HasValue() ? R("cnet_common_SP-17441").Replace("<NULL>", "").Text : ""; 
var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM")); //Filing System - Features 
var Size = TX_UOM.ExtractNumbers().Any() ? TX_UOM.ExtractNumbers().First() : 0; 
var Pack = TX_UOM.Text.Split("/").Count() > 1 ? TX_UOM.Text.Split("/").Last().ToLower() : ""; 
if (TX_UOM.HasValue("Set")) { 
result = "Sold as set"; 
containerTypeResult = "Sold as set"; 
} 
else if (TX_UOM.HasValue() && TX_UOM.Text.Split('/').Count() < 3) { 
if(TX_UOM.Text.Contains("Dozen") && !String.IsNullOrEmpty(colorFamily) && colorFamily.ToLower().Equals("assorted")) { 
result = "12 per pack"; 
if (!String.IsNullOrEmpty(containerType)) { 
containerTypeResult = $"12 {Coalesce(containerType.ToLower()).Pluralize()} per pack"; 
} else { 
containerTypeResult = "12 per pack"; 
} 
} 
else if(TX_UOM.Text.Contains("Dozen")) { 
result = "Dozen per pack"; 
if (!String.IsNullOrEmpty(containerType)) { 
containerTypeResult = $"Dozen {Coalesce(containerType.ToLower()).Pluralize()} per pack"; 
} else { 
containerTypeResult = "Dozen per pack"; 
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
    var CompliantStandardsRef = GetReference("SP-21659");
    
    if(!string.IsNullOrEmpty(CompliantStandardsRef)){
        switch(CompliantStandardsRef.Split(", ").Count()){
            case 1:
            Add($"CompliantStandards⸮Meets or exceeds {CompliantStandardsRef.Replace(" Certified", "").Replace(" certified", "").Replace(" certifications", "").TrimEnd(',')} standard");
            break;
            default:
            Add($"CompliantStandards⸮Meets or exceeds {CompliantStandardsRef.Split(", ").ToList().FlattenWithAnd().Replace(" Certified", "", " certified", "", " certifications", "")} standards");
            break;
        }
    }
} 

// Warranty information 
void Warr_anty(){ 
var result = ""; 
var referList = new List<string>(){"SP-16", "SP-5675", "SP-21932"}; 
var Warranty = Coalesce(A[430], A[3574], A[7241]); 
var refurbished = A[5312]; 

foreach(var refer in referList){ 
var temp = REQ.GetVariable(refer).HasValue() ? REQ.GetVariable(refer).Replace("<NULL>", "").Text : 
R(refer).HasValue() ? R(refer).Replace("<NULL>", "").Text : 
R($"cnet_common_{refer}").HasValue() ? R($"cnet_common_{refer}").Replace("<NULL>", "").Text : ""; 
if(!String.IsNullOrEmpty(temp)){ 
// Warranty = temp; 
var numbers = Coalesce(temp).ExtractNumbers(); 
if(numbers.Any()){ 
if (refurbished.HasValue("%refurbished")) { 
switch(temp.ToLower()){ 
case var str when str.Contains("year"): 
result = $"{numbers.First()}-year Non-mfg. warranty through Vendor. This product is NOT warrantied through {SKU.Brand}"; 
break; 
case var str when str.Contains("month"): 
result = $"{numbers.First()}-month Non-mfg. warranty through Vendor. This product is NOT warrantied through {SKU.Brand}"; 
break; 
case var str when str.Contains("day"): 
result = $"{numbers.First()}-day Non-mfg. warranty through Vendor. This product is NOT warrantied through {SKU.Brand}"; 
break; 
} 
} else { 
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
} 
else if(temp.ToLower().Contains("replacement")){ 
result = "Lifetime manufacturer limited replacement warranty"; 
break; 
} 
else if(temp.ToLower().Contains("ifetime manufacturer") 
|| temp.ToLower().Contains("imited lifetime warranty") || temp.ToLower().Contains("lifetime manufacturer limited warranty")){ 
result = "Lifetime manufacturer limited warranty"; 
break; 
} 
else if(Warranty.HasValue()){ 
var numbers = Warranty.Values.ExtractNumbers(); 
if(numbers.Any()){ 
if (refurbished.HasValue("%refurbished")) { 
switch(Warranty.FirstValue()) { 
case var str when str.In("%year%"): 
result = $"{numbers.First()}-year Non-mfg. warranty through Vendor. This product is NOT warrantied through {SKU.Brand}"; 
break; 
case var str when str.In("%month%"): 
result = $"{numbers.First()}-month Non-mfg. warranty through Vendor. This product is NOT warrantied through {SKU.Brand}"; 
break; 
case var str when str.In("%day%"): 
result = $"{numbers.First()}-day Non-mfg. warranty through Vendor. This product is NOT warrantied through {SKU.Brand}"; 
break; 
} 
} 
else { 
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
} 
else if(Warranty.HasValue("%replacement%")){ 
result = "Lifetime manufacturer limited replacement warranty"; 
break; 
} 
else if(Warranty.HasValue("%ifetime manufacturer%") 
|| Warranty.HasValue("%imited%ifetime%")){ 
result = "Lifetime manufacturer limited warranty"; 
break; 
} 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"Warranty⸮{result}"); 
} 
}

// Cable Length
void CableLength(){
    var CableLengthRef = GetReference("SP-21212");
    
    if(CableLengthRef == "6"){ 
        Add("CableLength⸮6' cable provides enough length to reach power sources"); 
    }
    else if(!string.IsNullOrEmpty(CableLengthRef)){ 
        Add($"CableLength⸮{CableLengthRef}' cable length"); 
    } 
}
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
        $@"Dimensions: (Adjustable) {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""D" : 
        $@"Dimensions: {DimensionsWeight_Width}""W x {DimensionsWeight_Depth}""D"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Width)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Width}""W x {DimensionsWeight_Height}""H" : 
        $@"Dimensions: {DimensionsWeight_Width}""W x {DimensionsWeight_Height}""H"; 
    } 
    
    else if(!String.IsNullOrEmpty(DimensionsWeight_Width) && !String.IsNullOrEmpty(DimensionsWeight_Length)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Width}""W x {DimensionsWeight_Length}""L" : 
        $@"Dimensions: {DimensionsWeight_Width}""W x {DimensionsWeight_Length}""L"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Depth)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""H x {DimensionsWeight_Depth}""D" : 
        $@"Dimensions: {DimensionsWeight_Height}""H x {DimensionsWeight_Depth}""D"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Length)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""H x {DimensionsWeight_Length}""L" : 
        $@"Dimensions: {DimensionsWeight_Height}""H x {DimensionsWeight_Length}""L"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height) && !String.IsNullOrEmpty(DimensionsWeight_Diameter)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Height}""H x {DimensionsWeight_Diameter}""Dia." : 
        $@"Dimensions: {DimensionsWeight_Height}""H x {DimensionsWeight_Diameter}""Dia."; 
    } 
    else if(FilingSystem_ProductSize.HasValue() 
    && FilingSystem_ProductSize.Values.First().ValueUSM.ExtractNumbers().Any()){ 
    var numbers = FilingSystem_ProductSize.Values.First().ValueUSM.ExtractNumbers(); 
        Dimensions = $@"Dimensions: {numbers.First()}""W x {numbers.Last()}""L"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Width)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Width}""W" : 
        $@"Dimensions: {DimensionsWeight_Width}""W"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Height)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Width}""H" : 
        $@"Dimensions: {DimensionsWeight_Height}""W"; 
    } 
    else if(!String.IsNullOrEmpty(DimensionsWeight_Depth)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Width}""D" : 
        $@"Dimensions: {DimensionsWeight_Depth}""W"; 
    }
    else if(!String.IsNullOrEmpty(DimensionsWeight_Length)){ 
        Dimensions = HeatersCoolers_HeatingCooling_SpecialFeatures_Details.HasValue("adjustable height", "height-adjustable") ? 
        $@"Dimensions: (Adjustable) {DimensionsWeight_Length}""L" : 
        $@"Dimensions: {DimensionsWeight_Width}""W"; 
    } 
    if(!String.IsNullOrEmpty(Dimensions)){ 
        Add($"Dimensions⸮{Dimensions}"); 
    } 
}

// "Shipping Dimensions" 
void ShippingDimensions(){ 
var result = ""; 
var DimensionsWeightShipping_ShippingWidth = Coalesce(A[1556]); // Dimensions & Weight (Shipping) - Shipping Width 
var DimensionsWeightShipping_ShippingDepth = Coalesce(A[1557]); // Dimensions & Weight (Shipping) - Shipping Depth 
var DimensionsWeightShipping_ShippingHeight = Coalesce(A[1558]); // Dimensions & Weight (Shipping) - Shipping Height 

if(DimensionsWeightShipping_ShippingHeight.HasValue() 
&& DimensionsWeightShipping_ShippingWidth.HasValue() 
&& DimensionsWeightShipping_ShippingDepth.HasValue()){ 
if(DimensionsWeightShipping_ShippingHeight.Units.First().NameUSM.In("in")){ 
var Height = DimensionsWeightShipping_ShippingHeight.FirstValueUsm(); 
var Width = DimensionsWeightShipping_ShippingWidth.FirstValueUsm(); 
var Depth = DimensionsWeightShipping_ShippingDepth.FirstValueUsm(); 
result = $@"Shipping dimensions: {Height}""H x {Width}""W x {Depth}""D"; 
} 
else if(DimensionsWeightShipping_ShippingHeight.Units.First().Name.In("mm")){ 
var Height = Math.Round(DimensionsWeightShipping_ShippingHeight.FirstValue() * 0.0393701, 2); 
var Width = Math.Round(DimensionsWeightShipping_ShippingWidth.FirstValue() * 0.0393701, 2); 
var Depth = Math.Round(DimensionsWeightShipping_ShippingDepth.FirstValue() * 0.0393701, 2); 
result = $@"Shipping dimensions: {Height}""H x {Width}""W x {Depth}""D"; 
} 
else if(DimensionsWeightShipping_ShippingHeight.Units.First().Name.In("cm")){ 
var Height = Math.Round(DimensionsWeightShipping_ShippingHeight.FirstValue() * 0.393701, 2); 
var Width = Math.Round(DimensionsWeightShipping_ShippingWidth.FirstValue() * 0.393701, 2); 
var Depth = Math.Round(DimensionsWeightShipping_ShippingDepth.FirstValue() * 0.393701, 2); 
result = $@"Shipping dimensions: {Height}""H x {Width}""W x {Depth}""D"; 
} 
else if(DimensionsWeightShipping_ShippingHeight.Units.First().Name.In("m")){ 
var Height = Math.Round(DimensionsWeightShipping_ShippingHeight.FirstValue() * 39.3701, 2); 
var Width = Math.Round(DimensionsWeightShipping_ShippingWidth.FirstValue() * 39.3701, 2); 
var Depth = Math.Round(DimensionsWeightShipping_ShippingDepth.FirstValue() * 39.3701, 2); 
result = $@"Shipping dimensions: {Height}""H x {Width}""W x {Depth}""D"; 
} 
} 
else if(DimensionsWeightShipping_ShippingHeight.HasValue()){ 
if(DimensionsWeightShipping_ShippingHeight.Units.First().NameUSM.In("in")){ 
var Height = DimensionsWeightShipping_ShippingHeight.FirstValueUsm(); 
var Width = DimensionsWeightShipping_ShippingWidth.HasValue() ? $@" x {DimensionsWeightShipping_ShippingWidth.FirstValueUsm()}""L" : 
DimensionsWeightShipping_ShippingDepth.HasValue() ? $@" x {DimensionsWeightShipping_ShippingDepth.FirstValueUsm()}""L" : ""; 
result = $@"Shipping dimensions: {Height}""W{Width}"; 
} 
else if(DimensionsWeightShipping_ShippingHeight.Units.First().Name.In("mm")){ 
var Height = Math.Round(DimensionsWeightShipping_ShippingHeight.FirstValue() * 0.0393701, 2); 
var Width = DimensionsWeightShipping_ShippingWidth.HasValue() ? $@" x {Math.Round(DimensionsWeightShipping_ShippingWidth.FirstValue() * 0.0393701, 2)}""L" : 
DimensionsWeightShipping_ShippingDepth.HasValue() ? $@" x {Math.Round(DimensionsWeightShipping_ShippingDepth.FirstValue() * 0.0393701, 2)}""L" : ""; 
result = $@"Shipping dimensions: {Height}""W{Width}"; 
} 
else if(DimensionsWeightShipping_ShippingHeight.Units.First().Name.In("cm")){ 
var Height = Math.Round(DimensionsWeightShipping_ShippingHeight.FirstValue() * 0.393701, 2); 
var Width = DimensionsWeightShipping_ShippingWidth.HasValue() ? $@" x {Math.Round(DimensionsWeightShipping_ShippingWidth.FirstValue() * 0.393701, 2)}""L" : 
DimensionsWeightShipping_ShippingDepth.HasValue() ? $@" x {Math.Round(DimensionsWeightShipping_ShippingDepth.FirstValue() * 0.393701, 2)}""L" : ""; 
result = $@"Shipping dimensions: {Height}""W{Width}"; 
} 
else if(DimensionsWeightShipping_ShippingHeight.Units.First().Name.In("m")){ 
var Height = Math.Round(DimensionsWeightShipping_ShippingHeight.FirstValue() * 39.3701, 2); 
var Width = DimensionsWeightShipping_ShippingWidth.HasValue() ? $@" x {Math.Round(DimensionsWeightShipping_ShippingWidth.FirstValue() * 39.3701, 2)}""L" : 
DimensionsWeightShipping_ShippingDepth.HasValue() ? $@" x {Math.Round(DimensionsWeightShipping_ShippingDepth.FirstValue() * 39.3701, 2)}""L" : ""; 
result = $@"Shipping dimensions: {Height}""W{Width}"; 
} 
} 
else if(DimensionsWeightShipping_ShippingWidth.HasValue()){ 
if(DimensionsWeightShipping_ShippingHeight.Units.First().NameUSM.In("in")){ 
var Width = DimensionsWeightShipping_ShippingWidth.FirstValueUsm(); 
var Depth = DimensionsWeightShipping_ShippingDepth.HasValue() ? $@" x {DimensionsWeightShipping_ShippingDepth.FirstValueUsm()}""L" : ""; 
result = $@"Shipping dimensions: {Width}""W{Depth}"; 
} 
else if(DimensionsWeightShipping_ShippingWidth.Units.First().Name.In("mm")){ 
var Width = Math.Round(DimensionsWeightShipping_ShippingWidth.FirstValue() * 0.0393701, 2); 
var Depth = DimensionsWeightShipping_ShippingDepth.HasValue() ? $@" x {Math.Round(DimensionsWeightShipping_ShippingDepth.FirstValue() * 0.0393701, 2)}""L" : ""; 
result = $@"Shipping dimensions: {Width}""W{Depth}"; 
} 
else if(DimensionsWeightShipping_ShippingWidth.Units.First().Name.In("cm")){ 
var Width = Math.Round(DimensionsWeightShipping_ShippingWidth.FirstValue() * 0.393701, 2); 
var Depth = DimensionsWeightShipping_ShippingDepth.HasValue() ? $@" x {Math.Round(DimensionsWeightShipping_ShippingDepth.FirstValue() * 0.393701, 2)}""L" : ""; 
result = $@"Shipping dimensions: {Width}""W{Depth}"; 
} 
else if(DimensionsWeightShipping_ShippingWidth.Units.First().Name.In("m")){ 
var Width = Math.Round(DimensionsWeightShipping_ShippingWidth.FirstValue() * 39.3701, 2); 
var Depth = DimensionsWeightShipping_ShippingDepth.HasValue() ? $@" x {Math.Round(DimensionsWeightShipping_ShippingDepth.FirstValue() * 39.3701, 2)}""L" : ""; 
result = $@"Shipping dimensions: {Width}""W{Depth}"; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"ShippingDimensions⸮{result}"); 
} 
} 

// "Caffeine Free"
void CaffeineFree() {
    if (A[6733].HasValue()) {
        Add("CafFree⸮This drink is caffeine free");
    }
}

// "Additional Kosher"
void Kosher() {
    if (A[6739].HasValue("Orthodox Union Kosher", "kosher")
    || DC.KSP.GetString().In("%kosher%", "%orthodox union kosher%")
    || DC.MKT.GetString().In("%kosher%", "%orthodox union kosher%")
    || DC.WIB.GetString().In("%kosher%", "%orthodox union kosher%")
    // || DC.FEAT.GetString().In("%kosher%", "%orthodox union kosher%")
    || Coalesce(SKU.ProductLineName, SKU.ModelName, SKU.Description).In("%kosher%", "%orthodox union kosher%")) {
        Add("Kosher⸮The product is kosher");
    }
}

// "Additional Acid Free"
void Acid_Free(){
    var itemInfo = REQ.GetVariable("SP-20654").HasValue() ? REQ.GetVariable("SP-20654") : R("SP-21736").HasValue() ? R("SP-21736") : R("cnet_common_SP-21736");
    if(Coalesce(A[5945], A[6031], A[6037], A[6189]).HasValue("%acid%free%")
    || DC.KSP.GetString().In("%acid%free%")
    || DC.MKT.GetString().In("%acid%free%")
    || DC.WIB.GetString().In("%acid%free%")
    // || DC.FEAT.GetString().In("%acid free%")
    || itemInfo.HasValue("Yes")) {
        Add("AcidFree⸮This product is acid free");
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
// || DC.FEAT.GetString().In("%gluten%free%") 
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
        R("cnet_common_SP-21408").HasValue() ? R("cnet_common_SP-21408").Replace("<NULL>", "").Text : 
        R("SP-18513").HasValue() ? R("SP-18513").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18513").HasValue() ? R("cnet_common_SP-18513").Replace("<NULL>", "").Text :
        R("SP-18556").HasValue() ? R("SP-18556").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18556").HasValue() ? R("cnet_common_SP-18556").Replace("<NULL>", "").Text : // Label Material 
        R("SP-21662").HasValue() ? R("SP-21662").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21662").HasValue() ? R("cnet_common_SP-21662").Replace("<NULL>", "").Text : ""; // Craft Supply Material 

    if(!String.IsNullOrEmpty(TrueColor)){ 
        switch(TrueColor.ToLower()){ 
            case var a when !String.IsNullOrEmpty(a) && !String.IsNullOrEmpty(Material): 
            // Made from sticker paper and come in white color 
            var Mat = ""; 
            Mat = Coalesce(Material).RegexReplace(@"(^[A-Z]{2,})|.*", "$1"); 
            var Mat2 = !string.IsNullOrEmpty(Mat) ? Mat : Material.ToLower(true); 
            TrueColorMaterial = TrueColor.ToLower().Contains(Mat2) ? $"Made of {Mat2}" : $"Comes in {TrueColor.ToLower()} and made of {Mat2}"; 
            resultTrueColor = $"Comes in {TrueColor.ToLower()}"; 
            break; 
            case var a when a.Equals("assorted"): 
            TrueColorMaterial = $"{TrueColor.ToUpperFirstChar()} colors"; 
            resultTrueColor = $"{TrueColor.ToUpperFirstChar()} colors"; 
            break; 
            case var a when !a.Equals("multicolor"): 
            TrueColorMaterial = $"Comes in {TrueColor.ToLower().Replace("meteorite with black stand", "meteorite color with black stand")}"; 
            resultTrueColor = $"Comes in {TrueColor.ToLower().Replace("meteorite with black stand", "meteorite color with black stand")}"; 
            break;
            case var a when !String.IsNullOrEmpty(a): 
            TrueColorMaterial = $"Comes in {a}"; 
            break; 
            case var a when String.IsNullOrEmpty(a)
            && !String.IsNullOrEmpty(Material): 
            TrueColorMaterial = $"Made of {Material.Replace("acid free", "acid-free")}"; 
            break;
        } 
    }
    else if(SPEC["MS"].GetLine("Color").Body.HasValue()){ 
        var color = SPEC["MS"].GetLine("Color").Body; 
        switch(color){ 
            case var temp when temp.ToLower().In("transparent", "clear"): 
            TrueColorMaterial = "Comes in сlear"; 
            resultTrueColor = "Comes in сlear"; 
            break; 
            case var temp when temp.ToString().Split(", ").Count() > 0: 
            TrueColorMaterial = $"Comes in {color.ToLower().Replace("meteorite with black stand", "meteorite color with black stand")}"; 
            resultTrueColor = $"Comes in {color.ToLower().Replace("meteorite with black stand", "meteorite color with black stand")}"; 
            break; 
        } 
    } 
    if(!String.IsNullOrEmpty(Material)){ 
        resultMaterial = $"Made of {Material.Replace("acid free", "acid-free")}"; 
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
// AMD A6|AMD E1|AMD A10|Intel Pentium|AMD A8|Intel i5|Intel i3|Rockchip RK3399|Intel|Rockchip Cortex|Broadcom Cortex-A53|AMD A4|Intel i7|Intel Celeron|No Operating System|AMD E2|Teradici Tera2321|MediaTek|AMD A9|Exynos|AMD Other|AMD FX|AMD A12|Intel N3350 
var ProcessorTypeRef = REQ.GetVariable("SP-18435").HasValue() ? REQ.GetVariable("SP-18435").Replace("<NULL>", "").Text : 
R("SP-18435").HasValue() ? R("SP-18435").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18435").HasValue() ? R("cnet_common_SP-18435").Replace("<NULL>", "").Text : ""; 
var ProcessorSpeedRef = REQ.GetVariable("SP-22057").HasValue() ? REQ.GetVariable("SP-22057").Replace("<NULL>", "").Text : 
R("SP-22057").HasValue() ? R("SP-22057").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22057").HasValue() ? R("cnet_common_SP-22057").Replace("<NULL>", "").Text : ""; 
// 4 to 4.9 GHz|3 to 3.9 GHz|2 to 2.9 GHz|1 to 1.9 GHz|5.0 Ghz and above|Less than 1 GHz 
var ProcessorSpeedUpToRef = REQ.GetVariable("SP-18434").HasValue() ? REQ.GetVariable("SP-18434").Replace("<NULL>", "").Text : 
R("SP-18434").HasValue() ? R("SP-18434").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18434").HasValue() ? R("cnet_common_SP-18434").Replace("<NULL>", "").Text : ""; 
var CPUCacheRef = REQ.GetVariable("SP-3507").HasValue() ? REQ.GetVariable("SP-3507").Replace("<NULL>", "").Text : 
R("SP-3507").HasValue() ? R("SP-3507").Replace("<NULL>", "").Text : 
R("cnet_common_SP-3507").HasValue() ? R("cnet_common_SP-3507").Replace("<NULL>", "").Text : ""; 

var Type = ""; 
var NumberOfCores = ""; 
var MaxTurboSpeed = ""; 
var UnitMaxTurboSpeed = ""; 
var InstalledSize = ""; 
var UnitInstalledSize = ""; 
var ClockSpeed = Processor_ClockSpeed.HasValue() ? Processor_ClockSpeed.FirstValue() : ""; 
var Unit = Processor_ClockSpeed.HasValue() ? $"{Processor_ClockSpeed.Units.First().Name.Replace("Ghz", "GHz")} " : ""; 
var Manufacturer = Processor_Manufacturer.HasValue() ? $"{Processor_Manufacturer.FirstValue()} " : ""; 
var ProcessorNumber = Processor_ProcessorNumber.HasValue() ? $"{Processor_ProcessorNumber.FirstValue()} " : ""; 

// S21607123 - 2.8GHz Intel Core i5-8400 hexa-core processor with up to 4GHz speed and 9MB cache memory 
if(Processor_Manufacturer.HasValue("Intel")){ 
Type = Processor_Type.HasValue() && Coalesce(ProcessorNumber).ExtractNumbers().Any() ? 
$"{Processor_Type.FirstValue().RegexReplace(@"(Core i).*", "Core")} " : Processor_Type.HasValue() ? 
$"{Processor_Type.FirstValue()} " : ""; 
NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower().Replace("6-core", "hexa-core")} " : ""; 
MaxTurboSpeed = Processor_MaxTurboSpeed.HasValue() ? $"with up to {Processor_MaxTurboSpeed.FirstValue()}" : ""; 
UnitMaxTurboSpeed = Processor_MaxTurboSpeed.HasValue() ? $"{Processor_MaxTurboSpeed.Units.First().Name.Replace("Ghz", "GHz")} speed " : ""; 
InstalledSize = CacheMemory_InstalledSize.HasValue() ? $" and {CacheMemory_InstalledSize.FirstValue()}" : ""; 
UnitInstalledSize = CacheMemory_InstalledSize.HasValue() ? $"{CacheMemory_InstalledSize.Units.First().Name} cache memory" : "for ultimate performance"; 

ProcessorType = $"{ClockSpeed}{Unit}{Manufacturer}{Type}{ProcessorNumber}{NumberOfCores}processor {MaxTurboSpeed}{UnitMaxTurboSpeed}{InstalledSize}{UnitInstalledSize}"; 
} 
// S21056776 - Features 2.5GHz (up to 2.9GHz) AMD A-series A6-9220 dual-core processor with 1MB cache memory 
else if(Processor_Manufacturer.HasValue("AMD")){ 
MaxTurboSpeed = Processor_MaxTurboSpeed.HasValue() ? $"(up to {Processor_MaxTurboSpeed.FirstValue()}" : ""; 
UnitMaxTurboSpeed = Processor_MaxTurboSpeed.HasValue() ? $"{Processor_MaxTurboSpeed.Units.First().Name.Replace("Ghz", "GHz")}) " : ""; 
Type = Processor_Type.HasValue() ? $"{Processor_Type.FirstValue().ToString().Split(' ').Where(s => Coalesce(s).In("A%")).Flatten().IfLongerThan(0, "A-series")} " : ""; 
NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower()} " : ""; 
InstalledSize = CacheMemory_InstalledSize.HasValue() ? $"with {CacheMemory_InstalledSize.FirstValue()}" : ""; 
UnitInstalledSize = CacheMemory_InstalledSize.HasValue() ? $"{CacheMemory_InstalledSize.Units.First().Name} cache memory" : "for ultimate performance"; 

ProcessorType = $"Features {ClockSpeed}{Unit}{MaxTurboSpeed}{UnitMaxTurboSpeed}{Manufacturer}{Type}{ProcessorNumber}{NumberOfCores}processor {InstalledSize}{UnitInstalledSize}"; 
} 
// S21133725 - Experience responsive performance, power, adaptability and fun with Intel Celeron 2GHz processor 
else if(Processor_Type.HasValue("Celeron")){ 
ClockSpeed = Processor_ClockSpeed.HasValue() ? Processor_ClockSpeed.FirstValue() : ""; 
Unit = Processor_ClockSpeed.HasValue() ? $"{Processor_ClockSpeed.Units.First().Name.Replace("Ghz", "GHz")} " : ""; 

ProcessorType = $"Experience responsive performance, power, adaptability and fun with Intel Celeron {ClockSpeed}{Unit}processor"; 
} 
else if(CAT.Alt.Count() > 0 && CAT.MainAlt.Key.In("3364010221167289")){ 
Manufacturer = Processor_Manufacturer.HasValue() ? $"{Processor_Manufacturer.FirstValue()} " : ""; 
NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower()} " : ""; 
ClockSpeed = Processor_ClockSpeed.HasValue() ? Processor_ClockSpeed.FirstValue() : ""; 
Unit = Processor_ClockSpeed.HasValue() ? $"{Processor_ClockSpeed.Units.First().Name.Replace("Ghz", "GHz")} " : ""; 
var ProcessorNumberOrType = Processor_ProcessorNumber.HasValue() ? $"{Processor_ProcessorNumber.FirstValue()} " : Processor_Type.HasValue() ? $"{Processor_Type.FirstValue()} " : ""; 

ProcessorType = $"{Manufacturer}{ProcessorNumberOrType}{NumberOfCores}{ClockSpeed}{Unit}processor delivers quality performance"; 
} 
else if(Processor_Type.HasValue("no CPU")){ 
ProcessorType = ""; 
} 
else if(!string.IsNullOrEmpty(ProcessorTypeRef)){ 
Type = ""; 
NumberOfCores = ""; 
MaxTurboSpeed = ""; 
InstalledSize = ""; 
ClockSpeed = !string.IsNullOrEmpty(ProcessorSpeedRef) ? $"{ProcessorSpeedRef}GHz " : ""; 

if(ProcessorTypeRef.Contains("Celeron")){ 
ProcessorType = $"Experience responsive performance, power, adaptability and fun with Intel Celeron {ClockSpeed}processor"; 
} 
else if(ProcessorTypeRef.Contains("Intel")){ 
Type = !string.IsNullOrEmpty(ProcessorTypeRef) && Coalesce(ProcessorTypeRef).ExtractNumbers().Any() ? 
$"{ProcessorTypeRef.Split(" ").Flatten(" Core ")} " : !string.IsNullOrEmpty(ProcessorTypeRef) ? 
$"{ProcessorTypeRef} " : ""; 
NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower().Replace("6-core", "hexa-core")} " : ""; 
MaxTurboSpeed = !string.IsNullOrEmpty(ProcessorSpeedRef) && Coalesce(ProcessorSpeedRef).ExtractNumbers().Any() ? $" with up to {Coalesce(ProcessorSpeedRef).ExtractNumbers().Last()}GHz speed" : ""; 
InstalledSize = !string.IsNullOrEmpty(CPUCacheRef) ? $" and {CPUCacheRef} cache memory" : " for ultimate performance"; 

ProcessorType = $"{ClockSpeed}{Type}{NumberOfCores}processor{MaxTurboSpeed}{InstalledSize}"; 
} 
else if(ProcessorTypeRef.Contains("AMD")){ 
MaxTurboSpeed = !string.IsNullOrEmpty(ProcessorSpeedRef) && Coalesce(ProcessorSpeedRef).ExtractNumbers().Any() ? $"(up to {Coalesce(ProcessorSpeedRef).ExtractNumbers().Last()}GHz) " : "";
Type = !string.IsNullOrEmpty(ProcessorTypeRef) ? $"{ProcessorTypeRef.Split(' ').Where(s => Coalesce(s).In("A%")).Flatten().IfLongerThan(0, "A-series")} " : ""; 
NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower()} " : ""; 
InstalledSize = !string.IsNullOrEmpty(CPUCacheRef) ? $"with {CPUCacheRef} cache memory" : "for ultimate performance"; 

ProcessorType = $"Features {ClockSpeed}{MaxTurboSpeed}{Type}{NumberOfCores}processor {InstalledSize}"; 
} 
else if(CAT.Alt.Count() > 0 && CAT.MainAlt.Key.In("3364010221167289")){ 
Type = !string.IsNullOrEmpty(ProcessorTypeRef) ? $"{ProcessorTypeRef} " : ""; 
NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower()} " : ""; 

ProcessorType = $"{Type}{NumberOfCores}{ClockSpeed}processor delivers quality performance"; 
} 
else if(ProcessorTypeRef.Equals("No Operating System")){ 
ProcessorType = ""; 
} 
else{ 
Type = !string.IsNullOrEmpty(ProcessorTypeRef) ? $"{ProcessorTypeRef} " : ""; 
NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower()} " : ""; 

ProcessorType = $"{ClockSpeed}{Type}{NumberOfCores}processor for ultimate performance"; 
} 
} 
// S21124162 - 3.7GHz AMD 2700X 8-core processor for ultimate performance 
else{ 
ClockSpeed = Processor_ClockSpeed.HasValue() ? Processor_ClockSpeed.FirstValue() : ""; 
Unit = Processor_ClockSpeed.HasValue() ? $"{Processor_ClockSpeed.Units.First().Name} " : ""; 
Manufacturer = Processor_Manufacturer.HasValue() ? $"{Processor_Manufacturer.FirstValue()} " : ""; 
var ProcessorNumberOrType = Processor_ProcessorNumber.HasValue() ? $"{Processor_ProcessorNumber.FirstValue().Replace("i3","Core i3")} " : Processor_Type.HasValue() ? $"{Processor_Type.FirstValue()} " : ""; 
NumberOfCores = Processor_NumberOfCores.HasValue() ? $"{Processor_NumberOfCores.FirstValue().ToLower()} " : ""; 

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
var ComputerOperatingSystemRef = REQ.GetVariable("SP-22295").HasValue() ? REQ.GetVariable("SP-22295").Replace("<NULL>", "").Text : 
R("SP-22295").HasValue() ? R("SP-22295").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22295").HasValue() ? R("cnet_common_SP-22295").Replace("<NULL>", "").Text : ""; 

if(OSProvided_Type.HasValue()){ 
switch(OSProvided_Type){ 
case var a when a.HasValue("%Windows 10 pro 64-bit%"): 
OperatingSystem = "Works on Windows 10 Pro, 64-bit operating system for an intuitive ##and user-friendly interface"; 
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
OperatingSystem = $"{windows.RegexReplace(@"( \d*[ -]bit)", ",$1")} operating system provides an intuitive ##and user-friendly interface"; 
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
else if(!string.IsNullOrEmpty(ComputerOperatingSystemRef)){ 
switch(ComputerOperatingSystemRef){ 
case var a when a.Equals("Windows 10 Professional"): 
OperatingSystem = "Works on Windows 10 Pro operating system for an intuitive ##and user-friendly interface"; 
break; 
// S21492722 - Windows 10 Home gives you the familiar feel of Windows with enhanced capabilities 
case var a when a.Equals("Windows 10"): 
OperatingSystem = "Windows 10 Home gives you the familiar feel of Windows with enhanced capabilities"; 
break; 
// S13586477 - Google Chrome operating system enables effortless Internet browsing 
case var a when a.Equals("Google Chrome"): 
OperatingSystem = "Google Chrome operating system enables effortless Internet browsing"; 
break; 
// S21492718 - Windows 10 Pro operating system provides an intuitive and user-friendly interface 
case var a when a.Contains("indows"): 
OperatingSystem = $"{a} operating system provides an intuitive ##and user-friendly interface"; 
break; 
// S3651629 - Linux OS smartly manages your hardware and runs routine applications efficiently 
case var a when a.Contains("Linux"): 
OperatingSystem = "Linux OS smartly manages your hardware and runs routine applications efficiently"; 
break; 
// S20631523 - HP ThinPro operating system offers an intuitive and user-friendly interface 
case var a when a.Equals("HP ThinPro"): 
OperatingSystem = "HP ThinPro operating system offers an intuitive and user-friendly interface"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(OperatingSystem)){ 
Add($"OperatingSystem⸮{OperatingSystem}"); 
} 
} 

// --RAM type 
void RAM_Type(){ 
var result = ""; 
var RAM_InstalledSize = Coalesce(A[53]); // RAM - Installed Size 
var RAM_Technology = Coalesce(A[56]); // RAM - Technology 
bool isNotGb = RAM_InstalledSize.HasValue() && RAM_InstalledSize.Units.First().Name.NotIn("GB") ? true : false; 
// 512MB|256MB|4GB|3GB|2GB|1GB|8GB|7GB|6GB|5GB|16GB|14GB|12GB|Less than 256MB|10GB|64GB +|32GB|24GB 
var InstalledRAMRef = REQ.GetVariable("SP-18437").HasValue() ? REQ.GetVariable("SP-18437").Replace("<NULL>", "").Text : 
R("SP-18437").HasValue() ? R("SP-18437").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18437").HasValue() ? R("cnet_common_SP-18437").Replace("<NULL>", "").Text : ""; 
// DDR3|DDR|DDR2|DDR4|SDRAM|EDO|FPM|SO-DIMM|DDR5|GDDR5 
var DesktopRAMTypeRef = REQ.GetVariable("SP-21852").HasValue() ? REQ.GetVariable("SP-21852").Replace("<NULL>", "").Text : 
R("SP-21852").HasValue() ? R("SP-21852").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21852").HasValue() ? R("cnet_common_SP-21852").Replace("<NULL>", "").Text : ""; 
var LaptopMemoryTypeRef = REQ.GetVariable("SP-3961").HasValue() ? REQ.GetVariable("SP-3961").Replace("<NULL>", "").Text : 
R("SP-3961").HasValue() ? R("SP-3961").Replace("<NULL>", "").Text : 
R("cnet_common_SP-3961").HasValue() ? R("cnet_common_SP-3961").Replace("<NULL>", "").Text : ""; 
var RAM = ""; 

if(RAM_InstalledSize.HasValue()){ 
var UnitRam = $"{RAM_InstalledSize.Units.First().Name}"; 

switch(RAM_InstalledSize.FirstValue()){ 
case var a when a == 0 || isNotGb: 
result = ""; 
break; 
// S4464584 - 2GB DDR2 memory makes you productive with its smooth performance (https://www.staples.com/product_2800417?akamai-feo=off) 
case var a when a < 4: 
RAM = CAT.Alt.Count() > 0 && CAT.MainAlt.Key.In("3364110222167288") && RAM_Technology.HasValue() ? $" {RAM_Technology.FirstValue().Replace(" SDRAM", "")}" : ""; 
result = $"{RAM_InstalledSize.FirstValue()}{UnitRam}{RAM} memory makes you productive with its smooth performance"; 
break; 
// S21340202 - 16GB SDRAM memory to run multiple programs (https://www.staples.com/HP-EliteBook-x360-1030-G2-13-3-Touchscreen-LCD-2-in-1-Notebook-Intel-Core-i7-i7-7600U-Dual-core-2-8GHZ-16GB-DDR4-SDRAM/product_IM19Y3295) 
case var a when a >= 16: 
RAM = CAT.Alt.Count() > 0 && CAT.MainAlt.Key.In("3364110222167288") && RAM_Technology.HasValue() ? $"{RAM_Technology.FirstValue().Replace("DDR4", "")}" : ""; 
result = $"{RAM_InstalledSize.FirstValue()}{UnitRam}{RAM} memory to run multiple programs"; 
break; 
// S21280151 - 8GB DDR4 SDRAM smoothly run your games, photo and video editing applications (https://www.staples.com/Dell-Latitude-7480-14-Laptop-LCD-Core-i5-7300U-256GB-SSD-8GB-RAM-WIN-10-Pro-Black/product_24122677) 
case var a when a >= 8: 
RAM = CAT.Alt.Count() > 0 && CAT.MainAlt.Key.In("3364110222167288") && RAM_Technology.HasValue() ? $" {RAM_Technology.FirstValue()}" : ""; 
result = $"{RAM_InstalledSize.FirstValue()}{UnitRam}{RAM} smoothly run your games, photo and video editing applications"; 
break; 
// S21580875 - With 8GB memory, you can multitask between various applications without issue (https://www.staples.com/acer-aspire-5-a517-51-33q4-17-3-lcd-notebook-intel-core-i3-6th-gen-i3-6006u-2-core-2-ghz-8gb-ddr4-sdram-1-tb-hdd/product_IM12GP105) 
case var a when a == 8: 
result = $"With {RAM_InstalledSize.FirstValue()}{UnitRam} memory, you can multitask between various applications without issue"; 
break; 
// 21251351 - 2GB memory seamlessly handles multiple programs together (https://www.staples.com/asus-vivobook-e203na-dh02-11-6-lcd-netbook-intel-celeron-n3350-dual-core-2-core-1-10-ghz-4-gb-ddr3-sdram/product_IM12ML083) 
default: 
result = $"{RAM_InstalledSize.FirstValue()}{UnitRam} memory seamlessly handles multiple programs together"; 
break; 
} 
} 
else if(!string.IsNullOrEmpty(InstalledRAMRef)){ 
RAM = ""; 
var numb = Coalesce(InstalledRAMRef).ExtractNumbers().Any() ? Coalesce(InstalledRAMRef).ExtractNumbers().First() : 0; 

switch(numb){ 
case var a when a == 0 || !InstalledRAMRef.Contains("GB"): 
result = ""; 
break; 
case var a when a < 4: 
RAM = CAT.Alt.Count() > 0 && CAT.MainAlt.Key.In("3364110222167288") && !string.IsNullOrEmpty(LaptopMemoryTypeRef) ? $" {LaptopMemoryTypeRef.Replace(" SDRAM", "")}" : ""; 
result = $"{InstalledRAMRef}{RAM} memory makes you productive with its smooth performance"; 
break; 
case var a when a >= 16: 
RAM = CAT.Alt.Count() > 0 && CAT.MainAlt.Key.In("3364110222167288") && !string.IsNullOrEmpty(LaptopMemoryTypeRef) ? $"{LaptopMemoryTypeRef.Replace("DDR4", "")}" : ""; 
result = $"{InstalledRAMRef}{RAM} memory to run multiple programs"; 
break; 
case var a when a >= 8: 
RAM = CAT.Alt.Count() > 0 && CAT.MainAlt.Key.In("3364110222167288") && !string.IsNullOrEmpty(LaptopMemoryTypeRef) ? $" {LaptopMemoryTypeRef}" : ""; 
result = $"{InstalledRAMRef}{RAM} smoothly run your games, photo and video editing applications"; 
break; 
case var a when a == 8: 
result = $"With {InstalledRAMRef} memory, you can multitask between various applications without issue"; 
break; 
// 21251351 - 2GB memory seamlessly handles multiple programs together (https://www.staples.com/asus-vivobook-e203na-dh02-11-6-lcd-netbook-intel-celeron-n3350-dual-core-2-core-1-10-ghz-4-gb-ddr3-sdram/product_IM12ML083) 
default: 
result = $"{InstalledRAMRef} memory seamlessly handles multiple programs together"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"RAMType⸮{result}"); 
} 
} 

// --Connectivity (wireless) 
void Wireless_Connectivity(){ 
var result = ""; 
var Networking_DataLinkProtocol = Coalesce(A[306]); // Networking - Data Link Protocol 
var Networking_WirelessProtocol = Coalesce(A[4645]); // Networking - Wireless Protocol 
var Networking_WirelessNIC = Coalesce(A[3266]); // Networking - Wireless NIC 
var Notebooks_Networking_Features = Coalesce(A[2873]); // Notebooks - Networking - Features 
// 802.11/a/n|802.11/n/g/e|802.11/a|802.11n|802.11/g/n|802.11/n/e|802.11/a/b/g/n/ac|802.11/b/g/n/e|Wireless|802.11/a/c|802.11/b/g|802.11/a/b/g|802.11/a/b/g/n|802.11/a/b|802.11b/g/n|No|Wireless A/C|Internal NIC 10/100|802.11ac with MIMO simultaneous dual band (2.4GHz and 5GHz)|802.11ac (2.4GHz/5GHz)|802.11ac (2.4GHz/5Ghz) 1x2 MISO 
var DesktopConnectivityWirelessRef = REQ.GetVariable("SP-22089").HasValue() ? REQ.GetVariable("SP-22089").Replace("<NULL>", "").Text : 
R("SP-22089").HasValue() ? R("SP-22089").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22089").HasValue() ? R("cnet_common_SP-22089").Replace("<NULL>", "").Text : ""; 
// 802.11/g/n|802.11/n/g/e|802.11n|802.11/a/b/g/n|802.11/a/n|802.11/b/g/n/e|802.11/a/b/g/n/ac|802.11/n/e|802.11 a/b/g/n/ac/ad|Internal NIC 10/100|Wireless A/C|802.11/a|802.11ad|802.11b/g/n|802.11ac|802.11b/g/n/ac|Wireless AC|802.11/a/b/g|802.11/a/b|No 
var LaptopsWirelessConnectivityRef = REQ.GetVariable("SP-18438").HasValue() ? REQ.GetVariable("SP-18438").Replace("<NULL>", "").Text : 
R("SP-18438").HasValue() ? R("SP-18438").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18438").HasValue() ? R("cnet_common_SP-18438").Replace("<NULL>", "").Text : ""; 
var wiFi = Coalesce(DesktopConnectivityWirelessRef, LaptopsWirelessConnectivityRef); 
var Bluetooth = Networking_WirelessProtocol.HasValue("bluetooth%") ? $" and {Networking_WirelessProtocol.Where("bluetooth%").First().Value().ToUpperFirstChar()}" : ""; 
var WirelessNIC = Networking_WirelessNIC.HasValue() ? $"Supports {Networking_WirelessNIC.FirstValue()} " : ""; 

// The improved 802.11 ac 2x2 Wi-Fi antenna delivers a stronger, more reliable Internet connection than before. (https://www.staples.com/HP-Stream-Laptop-14-ax069st--Office-365-Personal-included-/product_2802786) 
if(Notebooks_Networking_Features.HasValue("%2x2%")){ 
result = Networking_WirelessProtocol.HasValue("802.11%") ? 
$"The improved {Networking_WirelessProtocol.Where("802.11%").First().Value()} 2x2 Wi-Fi antenna delivers a stronger, more reliable Internet connection than before" : ""; 
} 
// S21498853 - 802.11ac wireless connectivity for easy internet access (https://www.staples.com/product_2634349?akamai-feo=off) 
else if(Networking_DataLinkProtocol.Where("ieee %").Count() == 1){ 
result = Networking_WirelessProtocol.HasValue("802.11%") ? 
$"{Networking_WirelessProtocol.Where("802.11%").First().Value()} wireless connectivity for easy internet access" : ""; 
} 
// S21213908 - Supports 802.11a/b/g/n/ac and Bluetooth 5.0 wireless connectivity for easy internet access 
else if(Networking_DataLinkProtocol.Where("ieee %").Count() > 1){ 
result = Networking_WirelessProtocol.HasValue("802.11%") ? 
$"{WirelessNIC}{Networking_WirelessProtocol.Where("802.11%").First().Value()}{Bluetooth} wireless connectivity for easy internet access" : ""; 
} 
else if(wiFi.HasValue("%2x2%")){ 
result = wiFi.HasValue("802.11%") ? 
$"The improved {wiFi.ToString().Split(' ').Where(s => s.Contains("802.11")).First()} 2x2 Wi-Fi antenna delivers a stronger, more reliable Internet connection than before" : ""; 
} 
else if(wiFi.HasValue("802.11%")){ 
result = $"{WirelessNIC}{wiFi.ToString().Split(' ').Where(s => s.Contains("802.11")).First()}{Bluetooth} wireless connectivity for easy internet access"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"WirelessConnectivity⸮{result}"); 
} 
} 

// --Graphics 
void Graphics_All(){ 
var result = ""; 
var VideoMemory_InstalledSize = Coalesce(A[241]); //Video Memory - Installed Size 
var VideoMemory_Technology = Coalesce(A[244]); // Video Memory - Technology 
var VideoOutput_GraphicsProcessor = Coalesce(A[247]); // Video Output - Graphics Processor 
// Other|None|AMD|NVIDIA|Intel 
var GraphicsCardBrandRef = REQ.GetVariable("SP-350324").HasValue() ? REQ.GetVariable("SP-350324").Replace("<NULL>", "").Text : 
R("SP-350324").HasValue() ? R("SP-350324").Replace("<NULL>", "").Text : 
R("cnet_common_SP-350324").HasValue() ? R("cnet_common_SP-350324").Replace("<NULL>", "").Text : ""; 
var DesktopVideoGraphicsRef = REQ.GetVariable("SP-4006").HasValue() ? REQ.GetVariable("SP-4006").Replace("<NULL>", "").Text : 
R("SP-4006").HasValue() ? R("SP-4006").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4006").HasValue() ? R("cnet_common_SP-4006").Replace("<NULL>", "").Text : ""; 
var LaptopVideoGraphicsRef = REQ.GetVariable("SP-3968").HasValue() ? REQ.GetVariable("SP-3968").Replace("<NULL>", "").Text : 
R("SP-3968").HasValue() ? R("SP-3968").Replace("<NULL>", "").Text : 
R("cnet_common_SP-3968").HasValue() ? R("cnet_common_SP-3968").Replace("<NULL>", "").Text : ""; 
var Size = VideoMemory_InstalledSize.HasValue() ? $" {VideoMemory_InstalledSize.FirstValue()}" : ""; 
var UnitsSize = VideoMemory_InstalledSize.HasValue() ? VideoMemory_InstalledSize.Units.First().Name : ""; 
var Technology = VideoMemory_Technology.HasValue() ? $" {VideoMemory_Technology.FirstValue().Replace("SDRAM", "")}" : ""; 

if(VideoOutput_GraphicsProcessor.HasValue()){ 
var GraphicsProcessor = VideoOutput_GraphicsProcessor.FirstValue().ToString().Split('/').First(); 

switch(VideoOutput_GraphicsProcessor){ 
// S21531860 - Experience smooth, lag-free performance with AMD Radeon Vega 6 graphic card (https://www.staples.com/HP-ZBook-14u-G4-14-Touchscreen-LCD-Mobile-Workstation-Intel-Core-i7-7500U-Dual-core-2-7GHZ-8GB-DDR4-SDRAM-256GB-SSD/product_IM18W3275?akamai-feo=off) 
case var a when a.HasValue("%AMD%"): 
result = $"Experience smooth, lag-free performance with {GraphicsProcessor}{Size}{UnitsSize} graphic card"; 
break; 
// S21204883 - NVIDIA Quadro P600 2GB discrete graphic card provides excellent ability in a variety of multimedia applications and user experiences (https://www.staples.com/lenovo-thinkpad-p52s-20lb0022us-15-6-inch-laptop-computer-core-i7-16-gb-windows-10-pro-english/product_IM13BQ311) 
case var a when a.HasValue("%Quadro%"): 
result = $"{GraphicsProcessor}{Size}{UnitsSize} discrete graphic card provides excellent ability in a variety of multimedia applications and user experiences"; 
break; 
case var a when a.HasValue("%intel%Nvidia%"): 
result = $"{VideoOutput_GraphicsProcessor.FirstValue().ToString().Split('/').Last().TrimStart() + " graphics and"}{Size}{UnitsSize}{Technology} dedicated graphics memory ensure ultimate gaming experience"; 
break; 
// S21174772 - NVIDIA GeForce GTX 1050 graphics and 2GB GDDR5 dedicated graphics memory ensure ultimate gaming experience (https://www.staples.com/hp-omen-x-17-ap020nr-17-3-laptop-intel-core-i7-7820hk-1tb-hdd-256gb-ssd-16gb-ram-win-10-home-geforce-gtx-1080/product_24298705) 
case var a when a.HasValue("%Nvidia%"): 
result = $"{GraphicsProcessor + " graphics and"}{Size}{UnitsSize}{Technology} dedicated graphics memory ensure ultimate gaming experience"; 
break; 
// Intel HD Graphics 620 provides everyday image quality for internet usage, basic photo editing and casual gaming (https://www.staples.com/Dell-Latitude-7480-14-Laptop-LCD-Core-i5-7300U-256GB-SSD-8GB-RAM-WIN-10-Pro-Black/product_24122677) 
case var a when a.HasValue("Intel% 6%", "Intel% 5%", "Intel% P6%", "Intel% P5%", "Intel%HD%Graphics%"): 
result = $"{GraphicsProcessor} provides everyday image quality for Internet usage, basic photo editing, and casual gaming"; 
break; 
// Intel HD Graphics render all the visuals on screen with smooth, vivid quality (https://www.staples.com/HP-15-bs061st-15-6-Laptop-Computer-Intel-Pentium-500GB-SATA-HD-8GB-DDR3L-Windows-10-Intel-HD-Graphics-405/product_2720612) 
case var a when a.HasValue("Intel HD Graphics%"): 
result = "Intel HD Graphics: render all the visuals on screen with smooth, vivid quality"; 
break; 
} 
} 
else if(!string.IsNullOrEmpty(GraphicsCardBrandRef)){ 
switch(GraphicsCardBrandRef){ 
case var a when a.Contains("AMD"): 
result = $"Experience smooth, lag-free performance with {Coalesce(DesktopVideoGraphicsRef, LaptopVideoGraphicsRef)}{Size}{UnitsSize} graphic card"; 
break; 
// S21204883 - NVIDIA Quadro P600 2GB discrete graphic card provides excellent ability in a variety of multimedia applications and user experiences (https://www.staples.com/lenovo-thinkpad-p52s-20lb0022us-15-6-inch-laptop-computer-core-i7-16-gb-windows-10-pro-english/product_IM13BQ311) 
case var a when !string.IsNullOrEmpty(a) 
&& VideoOutput_GraphicsProcessor.HasValue("%Quadro%"): 
result = $"{Coalesce(DesktopVideoGraphicsRef, LaptopVideoGraphicsRef)}{Size}{UnitsSize} discrete graphic card provides excellent ability in a variety of multimedia applications and user experiences"; 
break; 
case var a when !string.IsNullOrEmpty(a) 
&& VideoOutput_GraphicsProcessor.HasValue("%intel%Nvidia%"): 
result = $"{VideoOutput_GraphicsProcessor.FirstValue().ToString().Split('/').Last().TrimStart() + " graphics and"}{Size}{UnitsSize}{Technology} dedicated graphics memory ensure ultimate gaming experience"; 
break; 
// S21174772 - NVIDIA GeForce GTX 1050 graphics and 2GB GDDR5 dedicated graphics memory ensure ultimate gaming experience (https://www.staples.com/hp-omen-x-17-ap020nr-17-3-laptop-intel-core-i7-7820hk-1tb-hdd-256gb-ssd-16gb-ram-win-10-home-geforce-gtx-1080/product_24298705) 
case var a when a.Contains("Nvidia"): 
result = $"{Coalesce(DesktopVideoGraphicsRef, LaptopVideoGraphicsRef) + " graphics and"}{Size}{UnitsSize}{Technology} dedicated graphics memory ensure ultimate gaming experience"; 
break; 
// Intel HD Graphics 620 provides everyday image quality for internet usage, basic photo editing and casual gaming (https://www.staples.com/Dell-Latitude-7480-14-Laptop-LCD-Core-i5-7300U-256GB-SSD-8GB-RAM-WIN-10-Pro-Black/product_24122677) 
case var a when !string.IsNullOrEmpty(a) 
&& VideoOutput_GraphicsProcessor.HasValue("Intel% 6%", "Intel% 5%", "Intel% P6%", "Intel% P5%", "Intel%HD%Graphics%"): 
result = $"{Coalesce(DesktopVideoGraphicsRef, LaptopVideoGraphicsRef)} provides everyday image quality for Internet usage, basic photo editing, and casual gaming"; 
break; 
// Intel HD Graphics render all the visuals on screen with smooth, vivid quality (https://www.staples.com/HP-15-bs061st-15-6-Laptop-Computer-Intel-Pentium-500GB-SATA-HD-8GB-DDR3L-Windows-10-Intel-HD-Graphics-405/product_2720612) 
case var a when !string.IsNullOrEmpty(a) 
&& VideoOutput_GraphicsProcessor.HasValue("Intel HD Graphics%"): 
result = "Intel HD Graphics: render all the visuals on screen with smooth, vivid quality"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"Graphics⸮{result}"); 
} 
} 

// return value in XX"W x YY"H format 
string cnet_sheet_dimension_notebooks_notepads() { 
    var sheetDimention = R("SP-18229").HasValue() ? R("SP-18229") : R("cnet_common_SP-18229");
    var paperFormat = Coalesce(A[6018], A[6069]);
    
    if (!sheetDimention.HasValue("other")) {
        return sheetDimention.RegexReplace(@"(\d*"")(\sx\s)(\d*"")", "$1$2$3" );
    }
    else if(sheetDimention.HasValue("other")
    && paperFormat.HasValue())
    {
        var firstNum = Math.Round(paperFormat.FirstValue().RegexReplace(@"([ABC]\d)", "").ExtractNumbers().First() * 0.0393701, 2);
        var secondNum = Math.Round(paperFormat.FirstValue().RegexReplace(@"([ABC]\d)", "").ExtractNumbers().Last() * 0.0393701, 2);
    
        return firstNum.ToString().Replace(".51",".5").Replace(".52",".5").Flatten("").RegexReplace(@"([0-9]+)\.[0-9](8|9|1|2)", Math.Round(firstNum, 1).ToString()) + @""" x " + secondNum.ToString().Replace(".51",".5").Replace(".76",".75").Flatten("").RegexReplace(@"([0-9]+)\.[0-9](8|9|1|2)", Math.Round(secondNum, 1).ToString()) + @"""";
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
            return $"{num1}\" x {num2}\"";
        }
    }
    return "";
}

// --Weight (lbs.) 
void Weight_All(){ 
    var WeightRef = R("SP-20402").HasValue() ? R("SP-20402") : R("cnet_common_SP-20402");
    
    if(WeightRef.HasValue()){
        Add($"WeightAll⸮Weighs {WeightRef} lbs.");
    }
}

//"Features for all Category END" §§ 

//§§238271078270401 "File Cabinets BEGIN" "Serhii.O"

FileCabinetsTypeFileSize();
// Furnishing color & Material - All
// Dimensions - All
FileCabinetsDrawerGlides();
FileCabinetsLockIncluded();
FileCabinetsSafetyFeatures();
// Ratings and certifications - All
FileCabinetsAssemblyRequired();
AdditionalIntegratedHandle();
// Warranty - All

// --[FEATURE #1]
// --File cabinet type, # of Drawers and File Size
void FileCabinetsTypeFileSize(){
    var result = "";
    var fileCabinetTypeRef = REQ.GetVariable("SP-18228").HasValue() ? REQ.GetVariable("SP-18228") :
        R("SP-18228").HasValue() ? R("SP-18228") : R("cnet_common_SP-18228");
    var fileSizeRef = REQ.GetVariable("SP-22381").HasValue() ? REQ.GetVariable("SP-22381") :
        R("SP-22381").HasValue() ? R("SP-22381") : R("cnet_common_SP-22381");
    var general_DrawersQty = A[7251];
    var general_ProductType = A[7281];
    
    if(fileSizeRef.HasValue("%/%")
    && general_DrawersQty.HasValue()
    && fileCabinetTypeRef.In("%vertical%", "%lateral%")){
        var Type = general_ProductType.HasValue() ? general_ProductType.FirstValue() : "";
        result = $"{general_DrawersQty.FirstValue()}-drawer {Type} holds letter- or legal-size documents";
    }
    else if(fileSizeRef.HasValue("Letter")
    && general_DrawersQty.HasValue()
    && fileCabinetTypeRef.In("%vertical%", "%lateral%")){
        var Type = general_ProductType.HasValue() ? general_ProductType.FirstValue() : "";
        result = $"{general_DrawersQty.FirstValue()}-drawer {Type} holds letter-size documents";
    }
    else if(fileSizeRef.HasValue("Legal")
    && general_DrawersQty.HasValue()
    && fileCabinetTypeRef.In("%vertical%", "%lateral%")){
        var Type = general_ProductType.HasValue() ? general_ProductType.FirstValue() : "";
        result = $"{general_DrawersQty.FirstValue()}-drawer {Type} holds legal-size documents";
    }
    else if(fileSizeRef.HasValue("%/%")
    && general_DrawersQty.HasValue()
    && fileCabinetTypeRef.HasValue()){
        var Type = general_ProductType.HasValue() ? general_ProductType.FirstValue() : "";
        result = $"{fileCabinetTypeRef} cabinet; {general_DrawersQty.FirstValue()}-drawer {Type} holds letter- or legal-size documents";
    }
    else if(fileSizeRef.HasValue("Letter")
    && general_DrawersQty.HasValue()
    && fileCabinetTypeRef.HasValue()){
        var Type = general_ProductType.HasValue() ? general_ProductType.FirstValue() : "";
        result = $"{fileCabinetTypeRef} cabinet; {general_DrawersQty.FirstValue()}-drawer {Type} holds letter-size documents";
    }
    else if(fileSizeRef.HasValue("Legal")
    && general_DrawersQty.HasValue()
    && fileCabinetTypeRef.HasValue()){
        var Type = general_ProductType.HasValue() ? general_ProductType.FirstValue() : "";
        result = $"{fileCabinetTypeRef} cabinet; {general_DrawersQty.FirstValue()}-drawer {Type} holds legal-size documents";
    }
    else if(general_DrawersQty.HasValue()
    && fileCabinetTypeRef.In("%vertical%", "%lateral%")){
        var Type = general_ProductType.HasValue() ? $" {general_ProductType.FirstValue()}" : "";
        result = $"{general_DrawersQty.FirstValue()}-drawer{Type}";
    }
    else if(general_DrawersQty.HasValue()
    && fileCabinetTypeRef.HasValue()){
        var Type = general_ProductType.HasValue() ? $" {general_ProductType.FirstValue()}" : "";
        result = $"{fileCabinetTypeRef} cabinet; {general_DrawersQty.FirstValue()}-drawer{Type}";
    }
    else if(fileSizeRef.HasValue("%/%")
    && fileCabinetTypeRef.HasValue()){
        var Type = general_ProductType.HasValue() ? general_ProductType.FirstValue() : "";
        result = $"{fileCabinetTypeRef} cabinet for everyday use; designed for letter- and legal-size documents";
    }
    else if(fileSizeRef.HasValue("Letter")
    && fileCabinetTypeRef.HasValue()){
        var Type = general_ProductType.HasValue() ? general_ProductType.FirstValue() : "";
        result = $"{fileCabinetTypeRef} cabinet for everyday use; designed for letter-size documents";
    }
    else if(fileSizeRef.HasValue("Legal")
    && fileCabinetTypeRef.HasValue()){
        var Type = general_ProductType.HasValue() ? general_ProductType.FirstValue() : "";
        result = $"{fileCabinetTypeRef} cabinet for everyday use; designed for legal-size documents";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"FileCabinetsTypeFileSize⸮{result}");
    }
}

// --[FEATURE #2] - All
// --Furnishing color & Material

// --[FEATURE #3] - All
// --Dimensions (in Inches): Height x Width x Depth

// --[FEATURE #4]
// --Drawer glides
void FileCabinetsDrawerGlides(){
    if(A[7297].HasValue("Ball bearing slides")){
        Add("FileCabinetsDrawerGlides⸮Precision ball bearing glides for smooth opening and closing");
    }
    else if(A[7297].HasValue("easy glides")){
        Add("FileCabinetsDrawerGlides⸮Easy glides for smooth opening and closing");
    }
}

// --[FEATURE #5]
// --Lock included?
void FileCabinetsLockIncluded(){
    if(A[7297].HasValue("%removable%lock%core%")){
        Add("FileCabinetsLockIncluded⸮Removable core locks provide added security");
    }
    else if(A[7295].HasValue("%key%lock%")){
        Add("FileCabinetsLockIncluded⸮Key lock for added security");
    }
    else if(A[7295].HasValue("%Pin%code%")){
        Add("FileCabinetsLockIncluded⸮Pin code lock for added security");
    }
    else if(A[7295].HasValue("%Combination%lock%")){
        Add("FileCabinetsLockIncluded⸮Combination lock for added security");
    }
}

// --[FEATURE #6]
// --Safety features; anti-tip, counter balance, drawer interlock, wall attachment
void FileCabinetsSafetyFeatures(){
    if(A[7297].HasValue("counterweight")){
        Add("FileCabinetsSafetyFeatures⸮Built-in counterweight stabilizes the cabinet when a drawer is opened");
    }
}

// --[FEATURE #7] - All
// --Ratings and certifications

// --[FEATURE #8]
// --Assembly Required
void FileCabinetsAssemblyRequired(){
    if(A[7114].HasValue("No")){
        Add("FileCabinetsAssemblyRequired⸮Product comes fully assembled for your convenience");
    }
    else if(A[7114].HasValue("Yes")){
        Add("FileCabinetsAssemblyRequired⸮Assembly Required");
    }
}

// --[FEATURE #9]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision	
void AdditionalIntegratedHandle(){
    if(A[7279].HasValue("Integrated handle")){
        Add("AdditionalIntegratedHandle⸮Integrated handle creates a clean appearance");
    }
}

// --[FEATURE #10]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision	

// --[FEATURE #11]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision	

// --[FEATURE #12] - All
// --Warranty

//238271078270401 "File Cabinets END" "Serhii.O" §§

//§§1676378310636166390 "Journals & Diaries BEGIN" "Serhii.O"

JournalsDiariesTypeOfJournalUse();
// True color & Material - All
JournalsDiariesSheetDimensionRuleType();
// Overall Dimensions - All
JournalsDiariesClosureType();
JournalsDiariesDiaryTheme();
// Recycled Content % - All
// Pack Qty (If more than 1) - All
AdditionalJournalsDiariesSpecialFeatures();
AdditionalJournalsDiariesDiary();
AdditionalJournalsDiariesClosureBinding();
// "Acid Free" - All
AdditionalJournalsDiariesCoverMaterial();
AdditionalJournalsDiariesClosureDocumentPocket();
AdditionalJournalsDiariesClosureKeepsPagesInSafe();
AdditionalJournalsDiariesClosureRemovableDividers();
AdditionalJournalsDiariesClosurePenHolder();
AdditionalJournalsDiariesClosureStaplesBrandGuaranteed();

// --[FEATURE #1]
// --Type of Journal & Use
void JournalsDiariesTypeOfJournalUse(){
    var result = "";
    var manufacturerProductType = A[6014]; 
    
    // https://www.staples.com/TOPS-Executive-Journal-11-x-8-1-2-Legal-Ruled-80-Sheets/product_810647
    if(manufacturerProductType.HasValue("%journal%")){
        result = $"Enjoy writing again in a {manufacturerProductType.FirstValue().ToLower()}";
    }
    else if (manufacturerProductType.HasValue("%business%diary")) {
        result = "Business diary helps you easily keep notes organized and in one place";
    }
    else if(manufacturerProductType.HasValue("homework diary%")){
        result = $"{manufacturerProductType.FirstValue().ToLower().ToUpperFirstChar()} is great for home projects";
    }
    else if(manufacturerProductType.HasValue("school diary")){
        result = $"{manufacturerProductType.FirstValue().ToLower().ToUpperFirstChar()} is great for school projects";
    }
    else if(manufacturerProductType.HasValue("%diary%")){
        result = $"{manufacturerProductType.FirstValue().ToLower().ToUpperFirstChar()} is a great place to notes your life events.";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"JournalsDiariesTypeOfJournalUse⸮{result}");
    }
}

// --[FEATURE #2] - All
// --True color & Material

// --[FEATURE #3]
// --Sheet dimension & Rule type
void JournalsDiariesSheetDimensionRuleType(){
    var result = "";
    var cnetSize = !string.IsNullOrEmpty(cnet_sheet_dimension_notebooks_notepads()) ? cnet_sheet_dimension_notebooks_notepads() : ""; 
    // Pitman|Quad|Wide|College|Cornell|Dotted|Graph|Law|Narrow|Unruled|Gregg
    var ruleTypeRef = R("SP-18657").HasValue() ? R("SP-18657") : R("cnet_common_SP-18657");
    
    if (ruleTypeRef.HasValue()
    && !string.IsNullOrEmpty(cnetSize)) {
        var rule = !ruleTypeRef.HasValue("Unruled") ? $"{ruleTypeRef.ToLower()}-ruled" : ruleTypeRef.ToLower();
        result = $"Rule type: {rule}; sheet dimensions: {cnetSize}";
    }
    else if(!string.IsNullOrEmpty(cnetSize)){
        result = $"Sheet dimensions: {cnetSize}";
    }
    else if(ruleTypeRef.HasValue()){
        result = $"Rule type: {ruleTypeRef.ToLower()}";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"JournalsDiariesSheetDimensionRuleType⸮{result}");
    }
}

// --[FEATURE #4] - All
// --Overall Dimensions (in Inches): W (Side to Side) x H (Top to Bottom) x D (Thickness)

// --[FEATURE #5]
// --Closure type (If Applicable)
void JournalsDiariesClosureType(){
    var result = "";
    // Sliding|Snap|Zipper|Ziplock|Lock|Magnetic|Flap|Latch|Clasp|Hook & Loop|Clasp and Moistenable Glue|Moistenable Glue|Open End|Peel and Seal|Elastic|Tab|Lid|Open Top|Button and String
    var closureTypeRef = R("SP-12585").HasValue() ? R("SP-12585") : R("cnet_common_SP-12585");
    
    if(closureTypeRef.HasValue()){
        switch(closureTypeRef){
            case var a when a.HasValue("Elastic"):
            result = "Elastic closure for additional privacy";
            break;
            default:
            result = $"Closure type: {closureTypeRef.ToLower()}";
            break;
        }
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"JournalsDiariesClosureType⸮{result}");
    }
}

// --[FEATURE #6]
// --Special features such as inserted formatted pages, theme, etc.
void JournalsDiariesDiaryTheme(){
    var journalDiaryThemeRef = R("SP-18622").HasValue() ? R("SP-18622") : R("cnet_common_SP-18622");
    
    if(journalDiaryThemeRef.HasValue()){
        Add($"JournalsDiariesDiaryTheme⸮Theme/design: {journalDiaryThemeRef.ToLower()}");
    }
}

// --[FEATURE #7] - All
// --Recycled Content %

// --[FEATURE #8] - All
// --Pack Qty (If more than 1)

// --[FEATURE #9]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalJournalsDiariesSpecialFeatures(){
    if(A[6037].HasValue()){
        Add($"AdditionalJournalsDiariesSpecialFeatures⸮Includes planning pages: {A[6037].Values.Select(s => s.Value()).FlattenWithAnd()}");
    }
}

// --[FEATURE #10]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalJournalsDiariesDiary(){
    var manufacturerProductType = A[6014]; 
    var sheetsQty = A[6021];
    var pageFormatRef = R("SP-18621").HasValue() ? R("SP-18621") : R("cnet_common_SP-18621");
    
    if(pageFormatRef.HasValue()
    && manufacturerProductType.HasValue()
    && sheetsQty.HasValue()){
        Add($"JournalsDiariesDiaryTheme⸮Each {manufacturerProductType.FirstValue()} contains {sheetsQty.FirstValue()} {pageFormatRef.ToLower()} pages for your notes and sketches");
    }
}

// --[FEATURE #11]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalJournalsDiariesClosureBinding() { 
    var result = ""; 
    var bindingType = A[6017]; 
    
    if (bindingType.HasValue("casebound")) {
        result = "Case-bound construction is resilient";
    }
    else if (bindingType.HasValue("spiral-bound")) {
        result = "Spiral-bound design for easy access of sheets inside";
    }
    else if (bindingType.HasValue("wire-bound")) {
        result = "Wire binding allows book to lie flat on desk surface for maximum writing area";
    }
    else if (bindingType.HasValue("sewn-bound")) {
        result = "Section sewn binding keeps pages secure";
    }
    if (!string.IsNullOrEmpty(result)) {
        Add($"AdditionalJournalsDiariesClosureBinding⸮{result}");
    }
}

// --[FEATURE #12] "Acid Free" - All
// --Use for additional product and/or manufacturer information relevant to the customer buying decision

// --[FEATURE #13]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalJournalsDiariesCoverMaterial() {
    var result = "";
    var coverMateral = R("SP-18618").HasValue() ? R("SP-18618") : R("cnet_common_SP-18618");
    
    if (coverMateral.HasValue("Poly Cover")) {
        result = "Durable polypropylene covers protect sheets from damage";
    }
    else if (coverMateral.HasValue("Cardboard")) {
        result = "Sturdy cardboard allows you to write without a desk surface";
    }
    else if (coverMateral.HasValue("Cardboard")) {
        result = "Pressboard covers are durable and keep your notes private";
    }
    else if (coverMateral.HasValue("Soft")) {
        result = "Medium soft cover notebook makes you feel fancy";
    }
    else if (coverMateral.HasValue()) {
        result = $"{coverMateral.ToLower().ToUpperFirstChar().Replace("(pp)", "(PP)")} cover keeps inner pages protected from daily handling";
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"AdditionalJournalsDiariesCoverMaterial⸮{result}");
    }
}

// --[FEATURE #14]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalJournalsDiariesClosureDocumentPocket() {
    if (A[6031].HasValue("%document pocket%")) {
        Add($"AdditionalJournalsDiariesClosureDocumentPocket⸮Interior pocket for professional business cards");
    }
}

// --[FEATURE #15]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalJournalsDiariesClosureKeepsPagesInSafe() {
    if (A[6029].HasValue()) {
        Add($"AdditionalJournalsDiariesClosureKeepsPagesInSafe⸮{A[6029].Values.Select(s => s.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} keeps pages in safe");
    }
}

// --[FEATURE #16]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalJournalsDiariesClosureRemovableDividers() {
    if (A[6031].HasValue("removable dividers")) {
        Add("AdditionalJournalsDiariesClosureRemovableDividers⸮Removable dividers allow you to create the number of subjects you need");
    }
}

// --[FEATURE #17]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalJournalsDiariesClosurePenHolder() {
    if (A[6031].HasValue("pen holder")) {
        Add("AdditionalJournalsDiariesClosurePenHolder⸮Exterior pen loop ensures your pen is always within reach for quick, convenient use");
    }
}

// --[FEATURE #18]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalJournalsDiariesClosureStaplesBrandGuaranteed() {
    var brand = A[606];
    if (A[606].HasValue("Staples")) {
        Add("AdditionalJournalsDiariesClosureStaplesBrandGuaranteed⸮Staples brand 100% satisfaction guaranteed");
    }
}

//1676378310636166390 "Journals & Diaries END" "Serhii.O" §§

//§§168339288178220907 "Microfiber Cloths, Wipers and Rags BEGIN" "Serhii.O"

MCWRClothWiperRagTypeUse();
// Material & True color - All
// Dimensions - All
// Pack Size
AdditionalMCWRDispenser();
AdditionalMCWRRecycledMaterials();
AdditionalMCWAntiStatic();
AdditionalMCWRReCrepedTechnology();
AdditionalMCWRHydroknitTechnology();
AdditionalMCWRSolventResistant();
AdditionalMCWRAntibacterial();
AdditionalMCWRPreMoistened();

// --[FEATURE #1]
// --Cloth, wiper/rag type & use
void MCWRClothWiperRagTypeUse(){
    var result = "";
    // Mop Cloths|Wet Cloths|Rags|Wipers|Dry Cloths
    var clothWiperAndRagTypeRef = REQ.GetVariable("SP-23363").HasValue() ? REQ.GetVariable("SP-23363").Replace("<NULL>", "").Text :
        R("SP-23363").HasValue() ? R("SP-23363").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-23363").HasValue() ? R("cnet_common_SP-23363").Replace("<NULL>", "").Text : "";
    
    if(!string.IsNullOrEmpty(clothWiperAndRagTypeRef)){
        switch(clothWiperAndRagTypeRef){
            case var type when type.Equals("Dry Cloths")
            || type.Equals("Wet Cloths")
            || type.Equals("Mop Cloths"):
            result = $"These {type.ToLower()} pick up dust, dirt, crumbs and hair";
            break;
            case "Wipers":
            result = "Keep your office clean with these cleaning wipers";
            break;
            case "Rags":
            result = "These rags are ideal for thousands of projects - at home and at work";
            break;
            case var type when type.Contains("Cleaning Towel"):
            result = "Use these towels for all your cleaning needs";
            break;
        }
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"MCWRClothWiperRagTypeUse⸮{result}");
    }
}

// --[FEATURE #2] - All
// --Material & True color

// --[FEATURE #3] - All
// --Individual item dimensions

// --[FEATURE #4] - Not any info
// --Any treatments, chemicals or cleaners added

// --[FEATURE #5] - All
// --Pack Size (If more than 1)

// --[FEATURE #6]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalMCWRDispenser(){
    var result = "";
    // Mop Cloths|Wet Cloths|Rags|Wipers|Dry Cloths
    var clothWiperAndRagTypeRef = REQ.GetVariable("SP-23363").HasValue() ? REQ.GetVariable("SP-23363").Replace("<NULL>", "").Text :
        R("SP-23363").HasValue() ? R("SP-23363").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-23363").HasValue() ? R("cnet_common_SP-23363").Replace("<NULL>", "").Text : "";
    var miscellaneous_PackageType = A[6611]; // Miscellaneous - Package Type
    
    if(clothWiperAndRagTypeRef.Equals("Wipers")
    && miscellaneous_PackageType.HasValue("%dispenser%")){
        result = "Dispenser box puts wipers where you need them and dispenses one wiper at a time to help reduce waste";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalMCWRDispenser⸮{result}");
    }
}

// --[FEATURE #7]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalMCWRRecycledMaterials(){
    var result = "";
    var general_Features = A[6786]; // General - Features
    
    if(general_Features.HasValue("recyclable")){
        result = "Made of recycled materials to protect natural environment";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalMCWRRecycledMaterials⸮{result}");
    }
}

// --[FEATURE #8]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalMCWAntiStatic(){
    var result = "";
    var general_Features = A[7367]; // General - Features
    
    if(general_Features.HasValue("%anti%static%")){
        result = "Anti-static material that reduces lint and electrostatic discharge";;
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalMCWAntiStatic⸮{result}");
    }
}

// --[FEATURE #9]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalMCWRReCrepedTechnology(){
    var result = "";
    var general_Features = A[6786]; // General - Features
    
    if(general_Features.HasValue("Double Re-Creped Technology")){
        result = "Double ##Re-Creped design provides greater surface softness";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalMCWRReCrepedTechnology⸮{result}");
    }
}

// --[FEATURE #10]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalMCWRHydroknitTechnology(){
    var result = "";
    var general_Features = A[6786]; // General - Features
    
    if(general_Features.HasValue("Hydroknit technology")){
        result = "HydroKnit technology is fast absorbing to wick away moisture";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalMCWRHydroknitTechnology⸮{result}");
    }
}

// --[FEATURE #11]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalMCWRSolventResistant(){
    var result = "";
    // Mop Cloths|Wet Cloths|Rags|Wipers|Dry Cloths
    var clothWiperAndRagTypeRef = REQ.GetVariable("SP-23363").HasValue() ? REQ.GetVariable("SP-23363").Replace("<NULL>", "").Text :
        R("SP-23363").HasValue() ? R("SP-23363").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-23363").HasValue() ? R("cnet_common_SP-23363").Replace("<NULL>", "").Text : "";
    var general_Features = A[6786]; // General - Features
    
    if(general_Features.HasValue("solvent-resistant")){
        result = $"Solvent-resistant {clothWiperAndRagTypeRef.ToLower()} can be used multiple times";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalMCWRSolventResistant⸮{result}");
    }
}

// --[FEATURE #12]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalMCWRAntibacterial(){
    var result = "";
    var general_Features = A[6786]; // General - Features
    
    if(general_Features.HasValue("antibacterial", "Microban%")){
        result = "Features microban protection that avoids contamination";;
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalMCWRAntibacterial⸮{result}");
    }
}

// --[FEATURE #13]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditionalMCWRPreMoistened(){
    var result = "";
    var general_Features = A[6786]; // General - Features
    
    if(general_Features.HasValue("pre-moistened")){
        result = "Pre-moistened, you won't worry about spilling a liquid cleansing agent";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalMCWRHydroknitTechnology⸮{result}");
    }
}

//168339288178220907 "Microfiber Cloths, Wipers and Rags END" "Serhii.O" §§

//§§530868304162051 "RedBox Chargers & Connectors BEGIN" "Serhii.O" 

RedBox_ChargerTypeUse(); 
RedBox_ChargersConnectorsDimensions(); 
RedBox_ChargersConnectorsConnections(); 
RedBox_ChargersConnectorsAudioFeature(); 
RedBox_ChargersConnectorsVideoFeatureOrVoltage(); 
RedBox_ChargersConnectorsDataFeatureOrVoltage(); 
RedBox_ChargersConnectorsPackSize(); 
RedBox_ChargersConnectorsWarranty(); 
RedBox_ChargersConnectorsWarranty2(); 
RedBox_ChargersConnectorsSomeFeat(); 

// --[FEATURE #1] 
// --Charger type & Use 
void RedBox_ChargerTypeUse(){ 
var result = ""; 
// Lightning to USB|Apple 30-Pin|Speed Dial Controller|Wall Charger|Portable Battery|Microphone Recorder|Headphone Splitter|Audio Cable|FM Transmitter|AV Cable|Battery Case|Charging Kit/Bundle|Charging Station|Adapter|USB Cable|Car Charger 
var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : ""; 
var InputPowerRef = R("SP-4068").HasValue() ? R("SP-4068").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4068").HasValue() ? R("cnet_common_SP-4068").Replace("<NULL>", "").Text : ""; 
var CapacityRef = R("SP-193").HasValue() ? R("SP-193").Replace("<NULL>", "").Text : 
R("cnet_common_SP-193").HasValue() ? R("cnet_common_SP-193").Replace("<NULL>", "").Text : ""; 
var CableLengthRef = R("SP-21212").HasValue() ? R("SP-21212").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21212").HasValue() ? R("cnet_common_SP-21212").Replace("<NULL>", "").Text : ""; 
// 8 Pin|USB|Wireless|Stereo|4 Pin|Micro USB|30 Pin|DVI|DC Outlet|HDMI|Lithium-Ion Battery|Lightning|A/C 
var CellPhoneCableOrConnectorInterfaceRef = R("SP-18038").HasValue() ? R("SP-18038").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18038").HasValue() ? R("cnet_common_SP-18038").Replace("<NULL>", "").Text : ""; 
var WattsRef = R("SP-12618").HasValue() ? R("SP-12618").Replace("<NULL>", "").Text : 
R("cnet_common_SP-12618").HasValue() ? R("cnet_common_SP-12618").Replace("<NULL>", "").Text : ""; 
var CertificationStandardsRef = R("SP-21659").HasValue() ? R("SP-21659").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21659").HasValue() ? R("cnet_common_SP-21659").Replace("<NULL>", "").Text : ""; 
var Cable_AdditionalFeatures = A[1549]; // Cable - Additional Features 
var QI = Coalesce(A[380], A[9497]); // Cable - Additional Features 
var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories 

if(CellPhoneCableOrConnectorTypeRef.Equals("Wireless Charger")){ 
result = QI.HasValue("Qi") || CertificationStandardsRef.Contains("Qi") ? "Wireless Qi charging pad" : !string.IsNullOrEmpty(WattsRef) ? $"{WattsRef}W wireless charging pad" : ""; 
} 
else if(CellPhoneCableOrConnectorTypeRef.Equals("Wall Charger") 
|| Miscellaneous_IncludedAccessories.HasValue("wall charger%")){ 
result = "Wall charger with folding AC plug for portability"; 
} 
//Car Charger 
else if(CellPhoneCableOrConnectorTypeRef.Equals("Car Charger") 
&& !string.IsNullOrEmpty(InputPowerRef)){ 
result = $"{InputPowerRef} car charger for mobile devices"; 
} 
//Car Charger 
//Portable Battery 
else if(CellPhoneCableOrConnectorTypeRef.Equals("Portable Battery") 
|| CellPhoneCableOrConnectorTypeRef.Equals("Power Bank") 
&& !string.IsNullOrEmpty(CapacityRef)){ 
var cap = Coalesce(CapacityRef).ExtractNumbers().Any() ? Coalesce(CapacityRef).ExtractNumbers().First() : 0; 
switch(cap){ 
case var a when a > 0 
&& a < 5000: 
result = "Portable battery is a convenient way to charge most portable devices when there is no AC outlet"; 
break; 
case var a when a >= 5000 
&& a < 10000: 
result = "Power bank fully charges your mobile phone up to 2 times"; 
break; 
case var a when a >= 10000 
&& a < 15000: 
result = "Power bank fully charges your mobile phone up to 3 times"; 
break; 
case var a when a == 15000: 
result = "Power bank fully charges your mobile phone up to 4 times"; 
break; 
case var a when a > 15000: 
result = "Power bank fully charges your mobile phone up to 4 and more times"; 
break; 
} 
} 
//Portable Battery 
else if(CellPhoneCableOrConnectorTypeRef.Equals("USB Cable") 
|| CellPhoneCableOrConnectorTypeRef.Equals("Audio Cable") 
&& !string.IsNullOrEmpty(CableLengthRef)){ 
result = $"{CableLengthRef}' braided cable resists snags and tangles"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"RedBox_ChargerTypeUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Cable Length and gauge -OR- Charger Dimensions 
void RedBox_ChargersConnectorsDimensions(){ 
    var result = "";
    var CellPhoneCableOrConnectorTypeRef = REQ.GetVariable("SP-18037").HasValue() ? REQ.GetVariable("SP-18037").Replace("<NULL>", "").Text :
        R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : "";
    var End1ConnectorTypeRef = R("SP-22582").HasValue() ? R("SP-22582").Text :
        R("cnet_common_SP-22582").HasValue() ? R("cnet_common_SP-22582").Text : ""; // End 1 Connector Type
    var End2ConnectorTypeRef = R("SP-22583").HasValue() ? R("SP-22583").Text :
        R("cnet_common_SP-22583").HasValue() ? R("cnet_common_SP-22583").Text : ""; // End 1 Connector Type
    var NumberOfPortsRef = REQ.GetVariable("SP-21787").HasValue() ? REQ.GetVariable("SP-21787").Replace("<NULL>", "").Text :
        R("SP-21787").HasValue() ? R("SP-21787").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21787").HasValue() ? R("cnet_common_SP-21787").Replace("<NULL>", "").Text : "";
    var WattsRef = R("SP-12618").HasValue() ? R("SP-12618").Replace("<NULL>", "").Text :
		R("cnet_common_SP-12618").HasValue() ? R("cnet_common_SP-12618").Replace("<NULL>", "").Text : "";
		
    var DimensionsWeight_Width = REQ.GetVariable("SP-21044").HasValue() ? REQ.GetVariable("SP-21044").Replace("<NULL>", "").Text :
        R("SP-21044").HasValue() ? R("SP-21044").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21044").HasValue() ? R("cnet_common_SP-21044").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Width
    var DimensionsWeight_Height = REQ.GetVariable("SP-20654").HasValue() ? REQ.GetVariable("SP-20654").Replace("<NULL>", "").Text :
        R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Depth
    var DimensionsWeight_Length = REQ.GetVariable("SP-20400").HasValue() ? REQ.GetVariable("SP-20400").Replace("<NULL>", "").Text :
        R("SP-20400").HasValue() ? R("SP-20400").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20400").HasValue() ? R("cnet_common_SP-20400").Replace("<NULL>", "").Text : ""; //Dimensions & Weight
    var CableLengthRef = REQ.GetVariable("SP-21212").HasValue() ? REQ.GetVariable("SP-21212").Replace("<NULL>", "").Text :
        R("SP-21212").HasValue() ? R("SP-21212").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21212").HasValue() ? R("cnet_common_SP-21212").Replace("<NULL>", "").Text : ""; //Cable Length (ft.)
    var DimensionsWeight_Diameter = Coalesce(R("SP-21045").Replace("<NULL>", "").Text, R("SP-21453").Replace("<NULL>", "").Text, R("SP-21629").Replace("<NULL>", "").Text); //Dimensions & Weight - Depth
    var CableInclude = A[315];
    var CableIncludeLength = A[317];
    var Cable_AdditionalFeatures = A[1549]; // Cable - Additional Features
    var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories
    
    if(CellPhoneCableOrConnectorTypeRef.Equals("USB Cable")
    || CellPhoneCableOrConnectorTypeRef.Equals("Audio Cable")
    && !string.IsNullOrEmpty(End1ConnectorTypeRef) && !End1ConnectorTypeRef.Equals("USB")
    && !string.IsNullOrEmpty(End2ConnectorTypeRef) && !End2ConnectorTypeRef.Equals("USB")){
        result = $"{End1ConnectorTypeRef.Replace("USB-A", "USB Type-A").Replace("USB-B", "USB Type-B")} to {End2ConnectorTypeRef.Replace("USB-A", "USB Type-A").Replace("USB-B", "USB Type-B")}";
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("USB Cable")
    || CellPhoneCableOrConnectorTypeRef.Equals("Audio Cable")
    && !string.IsNullOrEmpty(End1ConnectorTypeRef) && !End1ConnectorTypeRef.Equals("USB")
    && !string.IsNullOrEmpty(End2ConnectorTypeRef)){
        result = $"{End1ConnectorTypeRef.Replace("USB-A", "USB Type-A").Replace("USB-B", "USB Type-B")} to {End2ConnectorTypeRef.Replace("USB-A", "USB Type-A").Replace("USB-B", "USB Type-B")}";
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("USB Cable")
    || CellPhoneCableOrConnectorTypeRef.Equals("Audio Cable")
    && !string.IsNullOrEmpty(End1ConnectorTypeRef)
    && !string.IsNullOrEmpty(End2ConnectorTypeRef) && !End2ConnectorTypeRef.Equals("USB")){
        result = $"{End1ConnectorTypeRef.Replace("USB-A", "USB Type-A").Replace("USB-B", "USB Type-B")} to {End2ConnectorTypeRef.Replace("USB-A", "USB Type-A").Replace("USB-B", "USB Type-B")}";
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("Portable Battery")
    || CellPhoneCableOrConnectorTypeRef.Equals("Power Bank")
    && !string.IsNullOrEmpty(NumberOfPortsRef)
    && !string.IsNullOrEmpty(WattsRef)){
        result = $"{NumberOfPortsRef} USB ports charge up to {WattsRef}W";
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("Car Charger")
    || CellPhoneCableOrConnectorTypeRef.Equals("Wall Charger")
    || Miscellaneous_IncludedAccessories.HasValue("wall charger%")){
        if(!String.IsNullOrEmpty(DimensionsWeight_Length) 
            && !String.IsNullOrEmpty(DimensionsWeight_Width)
            && !String.IsNullOrEmpty(DimensionsWeight_Height)){
            result = $@"Measures: {DimensionsWeight_Length}""L x {DimensionsWeight_Width}""W x {DimensionsWeight_Height}""H";
        }
        else if(!String.IsNullOrEmpty(DimensionsWeight_Diameter)){
            result = $@"Measures: {DimensionsWeight_Diameter}""Dia.";
        }
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("Wireless Charger")){
        var cable = "";
        if(CableInclude.HasValue("%cable%") && CableIncludeLength.HasValue()){
            if(CableIncludeLength.Units.First().Name.In("mm")){
                cable = $". Sold with a {Math.Round(CableIncludeLength.FirstValue() * 0.00328084, 1)}' color matched micro-usb braided cable";
            }
            else if(CableIncludeLength.Units.First().Name.In("cm")){
                cable = $". Sold with a {Math.Round(CableIncludeLength.FirstValue() * 0.0328084, 1)}' color matched micro-usb braided cable";
            }
            else if(CableIncludeLength.Units.First().Name.In("m")){
                cable = $". Sold with a {Math.Round(CableIncludeLength.FirstValue() * 3.28084, 1)}' color matched micro-usb braided cable";
            }
        }
        if(!String.IsNullOrEmpty(DimensionsWeight_Length) 
            && !String.IsNullOrEmpty(DimensionsWeight_Width)
            && !String.IsNullOrEmpty(DimensionsWeight_Height)){
            result = $@"Measures: {DimensionsWeight_Length}""L x {DimensionsWeight_Width}""W x {DimensionsWeight_Height}""H{cable}";
        }
        else if(!String.IsNullOrEmpty(DimensionsWeight_Diameter)){
            result = $@"Measures: {DimensionsWeight_Diameter}""Dia.{cable}";
        }
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"RedBox_ChargersConnectorsDimensions⸮{result}");
    }
} 

// --[FEATURE #3] 
// --Connections 
void RedBox_ChargersConnectorsConnections(){ 
    var result = "";
    var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : "";
    var NumberOfPortsRef = REQ.GetVariable("SP-21787").HasValue() ? REQ.GetVariable("SP-21787").Replace("<NULL>", "").Text :
        R("SP-21787").HasValue() ? R("SP-21787").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21787").HasValue() ? R("cnet_common_SP-21787").Replace("<NULL>", "").Text : "";
    var CapacityRef = R("SP-193").HasValue() ? R("SP-193").Replace("<NULL>", "").Text :
		R("cnet_common_SP-193").HasValue() ? R("cnet_common_SP-193").Replace("<NULL>", "").Text : "";
	var DimensionsWeight_Width = REQ.GetVariable("SP-21044").HasValue() ? REQ.GetVariable("SP-21044").Replace("<NULL>", "").Text :
        R("SP-21044").HasValue() ? R("SP-21044").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21044").HasValue() ? R("cnet_common_SP-21044").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Width
    var DimensionsWeight_Height = REQ.GetVariable("SP-20654").HasValue() ? REQ.GetVariable("SP-20654").Replace("<NULL>", "").Text :
        R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Depth
    var DimensionsWeight_Length = REQ.GetVariable("SP-20400").HasValue() ? REQ.GetVariable("SP-20400").Replace("<NULL>", "").Text :
        R("SP-20400").HasValue() ? R("SP-20400").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20400").HasValue() ? R("cnet_common_SP-20400").Replace("<NULL>", "").Text : ""; //Dimensions & Weight
    var DimensionsWeight_Diameter = Coalesce(R("SP-21045").Replace("<NULL>", "").Text, R("SP-21453").Replace("<NULL>", "").Text, R("SP-21629").Replace("<NULL>", "").Text); //Dimensions & Weight - Depth
	var CableInclude = A[315];
    var CableIncludeLength = A[317];
    var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories
    
    if(CellPhoneCableOrConnectorTypeRef.Equals("Car Charger")
    || CellPhoneCableOrConnectorTypeRef.Equals("Wall Charger")
    || Miscellaneous_IncludedAccessories.HasValue("wall charger%")){
        if(!String.IsNullOrEmpty(NumberOfPortsRef)){
            var numb = Coalesce(NumberOfPortsRef).ExtractNumbers();
            if(numb.Any()
            && numb.First() > 1){
                result = $"{numb.First()} USB ports to charge multiple devices at once";
            }
        }
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("Portable Battery")
    || CellPhoneCableOrConnectorTypeRef.Equals("Power Bank")
    && !string.IsNullOrEmpty(CapacityRef)){
        var cap = Coalesce(CapacityRef).ExtractNumbers().Any() ? Coalesce(CapacityRef).ExtractNumbers().First() : 0;
        var cable = "";
        var dim = "";
        if(CableInclude.HasValue("%cable%") && CableIncludeLength.HasValue()){
            if(CableIncludeLength.Units.First().Name.In("mm")){
                cable = $@". Sold with a {Math.Round(CableIncludeLength.FirstValue() * 0.0393701, 1)}"" color matched micro usb cable";
            }
            else if(CableIncludeLength.Units.First().Name.In("cm")){
                cable = $@". Sold with a {Math.Round(CableIncludeLength.FirstValue() * 0.393701, 1)}"" color matched micro usb cable";
            }
            else if(CableIncludeLength.Units.First().Name.In("m")){
                cable = $@". Sold with a {Math.Round(CableIncludeLength.FirstValue() * 39.3701, 1)}"" color matched micro usb cable";
            }
        }
        if(!String.IsNullOrEmpty(DimensionsWeight_Length) 
            && !String.IsNullOrEmpty(DimensionsWeight_Width)
            && !String.IsNullOrEmpty(DimensionsWeight_Height)){
            dim = $@"{DimensionsWeight_Length}""L x {DimensionsWeight_Width}""W x {DimensionsWeight_Height}""H";
        }
        else if(!String.IsNullOrEmpty(DimensionsWeight_Diameter)){
            dim = $@"{DimensionsWeight_Diameter}""Dia.";
        }
        switch(cap){
            case var a when a > 0
            && a < 5000:
            result = $"Overall dimensions: {dim}{cable}";
            break;
            case var a when a >= 5000
            && a < 10000:
            result = $"Overall dimensions: {dim}{cable}";
            break;
            case var a when a >= 10000
            && a < 15000:
            result = $"Overall dimensions: {dim}{cable}";
            break;
            case var a when a == 15000:
            result = $"Overall dimensions: {dim}{cable}";
            break;
            case var a when a > 15000:
            result = $"Overall dimensions: {dim}{cable}";
            break;
        }
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("Wireless Charger")){
        result = "Intelligent chip efficiently charges your device and with proper use, prolongs the life of your device battery";
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("USB Cable")
    || CellPhoneCableOrConnectorTypeRef.Equals("Audio Cable")){
        result = "Strain relief cable design keeps charging efficiency day after day; tested to withstand up to 10,000 bend cycles";
    }
    if(!String.IsNullOrEmpty(result)){
	    Add($"RedBox_ChargersConnectorsConnections⸮{result}");
	}
} 

// --[FEATURE #4] 
// --Audio feature 
void RedBox_ChargersConnectorsAudioFeature(){ 
var result = ""; 
// Lightning to USB|Apple 30-Pin|Speed Dial Controller|Wall Charger|Portable Battery|Microphone Recorder|Headphone Splitter|Audio Cable|FM Transmitter|AV Cable|Battery Case|Charging Kit/Bundle|Charging Station|Adapter|USB Cable|Car Charger 
var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : ""; 
var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories 

if(!string.IsNullOrEmpty(CellPhoneCableOrConnectorTypeRef)){ 
switch(CellPhoneCableOrConnectorTypeRef){ 
case var a when a.Equals("Audio Cable") 
|| a.Equals("USB Cable"): 
result = "Integrated cable management straps to help keep your cables tangle-free"; 
break; 
case "Car Charger": 
case var a when a.Equals("Portable Battery") 
|| a.Equals("Power Bank"): 
result = "Intelligent chip efficiently charges your device and with proper use, prolongs the life of your device battery"; 
break; 
case "Wireless Charger": 
result = "LED ring illuminates when device is properly connected and charging"; 
break; 
case var a when a.Equals("Wall Charger") 
|| Miscellaneous_IncludedAccessories.HasValue("wall charger%"): 
result = "Intelligent chip efficiently charges your device and with proper use, prolongs the life of your device battery"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"RedBox_ChargersConnectorsAudioFeature⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Video feature (If Applicable) -OR- Voltage 
void RedBox_ChargersConnectorsVideoFeatureOrVoltage(){ 
var result = ""; 
// Lightning to USB|Apple 30-Pin|Speed Dial Controller|Wall Charger|Portable Battery|Microphone Recorder|Headphone Splitter|Audio Cable|FM Transmitter|AV Cable|Battery Case|Charging Kit/Bundle|Charging Station|Adapter|USB Cable|Car Charger 
var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : ""; 
var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories 

if(!string.IsNullOrEmpty(CellPhoneCableOrConnectorTypeRef)){ 
switch(CellPhoneCableOrConnectorTypeRef){ 
case var a when a.Equals("Audio Cable") 
|| a.Equals("USB Cable"): 
var Warranty = REQ.GetVariable("SP-16").HasValue() ? REQ.GetVariable("SP-16").Replace("<NULL>", "").Text : 
R("SP-16").HasValue() ? R("SP-16").Replace("<NULL>", "").Text : 
R($"cnet_common_SP-16").HasValue() ? R($"cnet_common_SP-16").Replace("<NULL>", "").Text : ""; 
var numbers = Coalesce(Warranty).ExtractNumbers(); 
if(numbers.Any()){ 
switch(Warranty.ToLower()){ 
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
else if(Warranty.ToLower().Contains("replacement")){ 
result = "Lifetime manufacturer limited replacement warranty"; 
} 
else if(Warranty.ToLower().Contains("ifetime manufacturer") 
|| Warranty.ToLower().Contains("imited lifetime warranty") || Warranty.ToLower().Contains("lifetime manufacturer limited warranty")){ 
result = "Lifetime manufacturer limited warranty"; 
} 
break; 
case "Car Charger": 
result = "LED illuminated ports help you connect your device in the dark"; 
break; 
case var a when a.Equals("Portable Battery") 
|| a.Equals("Power Bank"): 
result = "LED ring illuminates when device is properly connected and charging"; 
break; 
case "Wireless Charger": 
result = "Automatically dims after eight seconds"; 
break; 
case var a when a.Equals("Wall Charger") 
|| Miscellaneous_IncludedAccessories.HasValue("wall charger%"): 
result = "LED illuminated ports help you connect your device in the dark; turn off the illumination when desired with the toggle switch"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"RedBox_ChargersConnectorsVideoFeatureOrVoltage⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Data feature (If Applicable) -OR- Additional charger data (If Applicable) 
void RedBox_ChargersConnectorsDataFeatureOrVoltage(){ 
    var result = "";
    // Lightning to USB|Apple 30-Pin|Speed Dial Controller|Wall Charger|Portable Battery|Microphone Recorder|Headphone Splitter|Audio Cable|FM Transmitter|AV Cable|Battery Case|Charging Kit/Bundle|Charging Station|Adapter|USB Cable|Car Charger
    var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : "";
	var TrueColor = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : 
        R("SP-21278").HasValue() ? R("SP-21278").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21278").HasValue() ? R("cnet_common_SP-21278").Replace("<NULL>", "").Text : "";
	var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories
	var CableInclude = A[315];
    var CableIncludeLength = A[317];
    
	if(!string.IsNullOrEmpty(CellPhoneCableOrConnectorTypeRef)){
	    switch(CellPhoneCableOrConnectorTypeRef){
	        case var a when a.Equals("Audio Cable")
	        || a.Equals("USB Cable"):
            result = "";
	        break;
	        case var a when a.Equals("Wall Charger")
	        || Miscellaneous_IncludedAccessories.HasValue("wall charger%"):
	        result = "";
	        break;
	        case "Car Charger":
            var color = !string.IsNullOrEmpty(TrueColor) ? TrueColor.ToLower() : "";
	        result = $"Glossy {color} with a matte finish on high-touch areas";
	        break;
	        case var a when a.Equals("Portable Battery")
	        || a.Equals("Power Bank"):
	        result = "Same LED indicator displays the remaining battery life until you need to recharge";
	        break;
	        case "Wireless Charger":
	        var cable = "";
            if(CableInclude.HasValue("%cable%") && CableIncludeLength.HasValue()){
                if(CableIncludeLength.Units.First().Name.In("mm")){
                    cable = $@". Sold with a {Math.Round(CableIncludeLength.FirstValue() * 0.00328084, 1)}"" color matched micro usb cable";
                }
                else if(CableIncludeLength.Units.First().Name.In("cm")){
                    cable = $@". Sold with a {Math.Round(CableIncludeLength.FirstValue() * 0.0328084, 1)}"" color matched micro usb cable";
                }
                else if(CableIncludeLength.Units.First().Name.In("m")){
                    cable = $@". Sold with a {Math.Round(CableIncludeLength.FirstValue() * 3.28084, 1)}"" color matched micro usb cable";
                }
            }
	        result = $"Included {cable}' braided cable resists snags and tangles";
	        break;
	    }
	}
    if(!String.IsNullOrEmpty(result)){
	    Add($"RedBox_ChargersConnectorsDataFeatureOrVoltage⸮{result}");
	}
} 

// --[FEATURE #7] 
// --Pack Size (If more than 1) 
void RedBox_ChargersConnectorsPackSize(){ 
    var result = "";
    // Lightning to USB|Apple 30-Pin|Speed Dial Controller|Wall Charger|Portable Battery|Microphone Recorder|Headphone Splitter|Audio Cable|FM Transmitter|AV Cable|Battery Case|Charging Kit/Bundle|Charging Station|Adapter|USB Cable|Car Charger
    var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : "";
	var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories
    
	if(!string.IsNullOrEmpty(CellPhoneCableOrConnectorTypeRef)){
	    switch(CellPhoneCableOrConnectorTypeRef){
	        case var a when a.Equals("Audio Cable")
	        || a.Equals("USB Cable"):
            result = "";
	        break;
	        case var a when a.Equals("Wall Charger")
	        || Miscellaneous_IncludedAccessories.HasValue("wall charger%"):
	        result = "";
	        break;
	        case "Car Charger":
            var Warranty = REQ.GetVariable("SP-16").HasValue() ? REQ.GetVariable("SP-16").Replace("<NULL>", "").Text : 
                R("SP-16").HasValue() ? R("SP-16").Replace("<NULL>", "").Text : 
                R($"cnet_common_SP-16").HasValue() ? R($"cnet_common_SP-16").Replace("<NULL>", "").Text : "";
	        var numbers = Coalesce(Warranty).ExtractNumbers();
            if(numbers.Any()){
                switch(Warranty.ToLower()){
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
            else if(Warranty.ToLower().Contains("replacement")){
                result = "Lifetime manufacturer limited replacement warranty";
            }
            else if(Warranty.ToLower().Contains("ifetime manufacturer")
            || Warranty.ToLower().Contains("imited lifetime warranty") || Warranty.ToLower().Contains("lifetime manufacturer limited warranty")){          
                result = "Lifetime manufacturer limited warranty";
            }
	        break;
	        case var a when a.Equals("Portable Battery")
	        || a.Equals("Power Bank"):
	        result = "Illuminated ports help you connect your device in the dark";
	        break;
	        case "Wireless Charger":
	        result = "Strain relief cable design keeps charging efficiency day after day; tested to withstand up to 10,000 bend cycles";
	        break;
	    }
	}
    if(!String.IsNullOrEmpty(result)){
	    Add($"RedBox_ChargersConnectorsPackSize⸮{result}");
	}
} 

// --[FEATURE #8] 
// --Warranty 
void RedBox_ChargersConnectorsWarranty(){ 
var result = ""; 
// Lightning to USB|Apple 30-Pin|Speed Dial Controller|Wall Charger|Portable Battery|Microphone Recorder|Headphone Splitter|Audio Cable|FM Transmitter|AV Cable|Battery Case|Charging Kit/Bundle|Charging Station|Adapter|USB Cable|Car Charger 
var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : ""; 
var TrueColor = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : 
R("SP-21278").HasValue() ? R("SP-21278").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21278").HasValue() ? R("cnet_common_SP-21278").Replace("<NULL>", "").Text : ""; 
var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories 

if(!string.IsNullOrEmpty(CellPhoneCableOrConnectorTypeRef)){ 
switch(CellPhoneCableOrConnectorTypeRef){ 
case var a when a.Equals("Audio Cable") 
|| a.Equals("USB Cable"): 
result = ""; 
break; 
case var a when a.Equals("Wall Charger") 
|| Miscellaneous_IncludedAccessories.HasValue("wall charger%"): 
result = ""; 
break; 
case "Car Charger": 
result = ""; 
break; 
case var a when a.Equals("Portable Battery") 
|| a.Equals("Power Bank"): 
case "Wireless Charger": 
var color = !string.IsNullOrEmpty(TrueColor) ? TrueColor.ToLower() : ""; 
result = $"Glossy {color} with a matte finish on high-touch areas"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"RedBox_ChargersConnectorsWarranty⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void RedBox_ChargersConnectorsWarranty2(){ 
var result = ""; 
// Lightning to USB|Apple 30-Pin|Speed Dial Controller|Wall Charger|Portable Battery|Microphone Recorder|Headphone Splitter|Audio Cable|FM Transmitter|AV Cable|Battery Case|Charging Kit/Bundle|Charging Station|Adapter|USB Cable|Car Charger 
var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : ""; 
var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories 

if(!string.IsNullOrEmpty(CellPhoneCableOrConnectorTypeRef)){ 
switch(CellPhoneCableOrConnectorTypeRef){ 
case var a when a.Equals("Audio Cable") 
|| a.Equals("USB Cable"): 
result = ""; 
break; 
case var a when a.Equals("Wall Charger") 
|| Miscellaneous_IncludedAccessories.HasValue("wall charger%"): 
result = ""; 
break; 
case "Car Charger": 
result = ""; 
break; 
case var a when a.Equals("Portable Battery") 
|| a.Equals("Power Bank"): 
case "Wireless Charger": 
var Warranty = REQ.GetVariable("SP-16").HasValue() ? REQ.GetVariable("SP-16").Replace("<NULL>", "").Text : 
R("SP-16").HasValue() ? R("SP-16").Replace("<NULL>", "").Text : 
R($"cnet_common_SP-16").HasValue() ? R($"cnet_common_SP-16").Replace("<NULL>", "").Text : ""; 
var numbers = Coalesce(Warranty).ExtractNumbers(); 
if(numbers.Any()){ 
switch(Warranty.ToLower()){ 
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
else if(Warranty.ToLower().Contains("replacement")){ 
result = "Lifetime manufacturer limited replacement warranty"; 
} 
else if(Warranty.ToLower().Contains("ifetime manufacturer") 
|| Warranty.ToLower().Contains("imited lifetime warranty") || Warranty.ToLower().Contains("lifetime manufacturer limited warranty")){ 
result = "Lifetime manufacturer limited warranty"; 
} 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"RedBox_ChargersConnectorsWarranty2⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void RedBox_ChargersConnectorsSomeFeat(){ 
var result = ""; 
// Lightning to USB|Apple 30-Pin|Speed Dial Controller|Wall Charger|Portable Battery|Microphone Recorder|Headphone Splitter|Audio Cable|FM Transmitter|AV Cable|Battery Case|Charging Kit/Bundle|Charging Station|Adapter|USB Cable|Car Charger 
var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : ""; 
var Miscellaneous_IncludedAccessories = A[2660]; // Miscellaneous - Included Accessories 

if(!string.IsNullOrEmpty(CellPhoneCableOrConnectorTypeRef)){ 
switch(CellPhoneCableOrConnectorTypeRef){ 
case "USB Cable": 
result = ""; 
break; 
case var a when a.Equals("Wall Charger") 
|| Miscellaneous_IncludedAccessories.HasValue("wall charger%"): 
result = ""; 
break; 
case "Car Charger": 
result = ""; 
break; 
case var a when a.Equals("Portable Battery") 
|| a.Equals("Power Bank"): 
result = ""; 
break; 
case "Wireless Charger": 
result = ""; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"RedBox_ChargersConnectorsSomeFeat⸮{result}"); 
} 
} 

//530868304162051 "RedBox Chargers & Connectors END" "Serhii.O" §§ 

//§§5226058, 5226054, 5226006, 12393811005220973, 5226157, 12375510567216950, 12366910285216553, 1212164913166147, 12378610641217427 "Remanufactured Laser Printer Ink, Toner & Drum Units BEGIN" "Serhii.O"

REDBOX_CartridgeYieldType();
REDBOX_CartridgePackageContents();
REDBOX_CartridgeVendorSpecificInformation();
REDBOX_CartridgeSavingsMessage();
REDBOX_CartridgeCompatibility();
REDBOX_CartridgeRecycledContent();
REDBOX_CartridgePackagedRecycledContent();
REDBOX_CartridgeCompatible();
REDBOX_CartridgeMediaIncluded();
REDBOX_CartridgeFeatures();
REDBOX_CartridgeRemanufactured();
REDBOX_CartridgeWarranty();

int BlackCartridge(){
    return A[2294].Where("%black%").Match(2295).Values("-").Count();
}

int ColorCartridge(){
    return A[2294].WhereNot("%black%").Match(2295).Values("-").Count();
}

// --[FEATURE #1]
// --Cartridge yield type; per Cartridge. If cartridge yield type doesn't apply, use generic messaging "Maximize you page yield while you save"
void REDBOX_CartridgeYieldType(){
    var CartridgeYieldTypeRef = REQ.GetVariable("SP-1037").HasValue() ? REQ.GetVariable("SP-1037") :
        R("SP-1037").HasValue() ? R("SP-1037") : R("cnet_common_SP-1037");

if (CartridgeYieldTypeRef =="Other"){
CartridgeYieldTypeRef =A[3531].FirstValueOrDefault();
}
    var InkOrTonerPackSize = REQ.GetVariable("SP-2884").HasValue() ? REQ.GetVariable("SP-2884") :
        R("SP-2884").HasValue() ? R("SP-2884") : R("cnet_common_SP-2884"); 
    var PageYieldPerPackageUpToRef = REQ.GetVariable("SP-1041").HasValue() ? REQ.GetVariable("SP-1041") :
        R("SP-1041").HasValue() ? R("SP-1041") : R("cnet_common_SP-1041");
    var YieldPerCartridgeBlackRef = REQ.GetVariable("SP-351449").HasValue() ? REQ.GetVariable("SP-351449") :
        R("SP-351449").HasValue() ? R("SP-351449") : R("cnet_common_SP-351449");
    var YieldPerCartridgeYellowRef = REQ.GetVariable("SP-351455").HasValue() ? REQ.GetVariable("SP-351455") :
        R("SP-351455").HasValue() ? R("SP-351455") : R("cnet_common_SP-351455");
    var YieldPerCartridgeCyanRef = REQ.GetVariable("SP-351454").HasValue() ? REQ.GetVariable("SP-351454") :
        R("SP-351454").HasValue() ? R("SP-351454") : R("cnet_common_SP-351454");
    var YieldPerCartridgeGrayRef = REQ.GetVariable("SP-351452").HasValue() ? REQ.GetVariable("SP-351452") :
        R("SP-351452").HasValue() ? R("SP-351452") : R("cnet_common_SP-351452");
    var YieldPerCartridgeMagentaRef = REQ.GetVariable("SP-351450").HasValue() ? REQ.GetVariable("SP-351450") :
        R("SP-351450").HasValue() ? R("SP-351450") : R("cnet_common_SP-351450");
    var YieldPerCartridgeTriColorRef = REQ.GetVariable("SP-351451").HasValue() ? REQ.GetVariable("SP-351451") :
        R("SP-351451").HasValue() ? R("SP-351451") : R("cnet_common_SP-351451");
    var YieldType1ColorRef = REQ.GetVariable("SP-351481").HasValue() ? REQ.GetVariable("SP-351481") :
        R("SP-351481").HasValue() ? R("SP-351481") : R("cnet_common_SP-351481");
        
    if(InkOrTonerPackSize.HasValue("1/Pack")
    && PageYieldPerPackageUpToRef.HasValue()
    && CartridgeYieldTypeRef.HasValue()){
        Add($"REDBOX_CartridgeYieldType⸮Yields up to {PageYieldPerPackageUpToRef} pages per {CartridgeYieldTypeRef.ToLower().Replace("standard yield", "standard").Replace("other", "this")} cartridge");
    }
    else if(InkOrTonerPackSize.ExtractNumbers().Any()
    && InkOrTonerPackSize.ExtractNumbers().First() > 1
    && YieldPerCartridgeBlackRef.HasValue()
    && YieldPerCartridgeTriColorRef.HasValue()){
        Add($"REDBOX_CartridgeYieldType⸮Yields up to {YieldPerCartridgeBlackRef} pages for black and {YieldPerCartridgeTriColorRef} pages for the cyan/magenta/yellow cartridges");
    }
    else if(InkOrTonerPackSize.ExtractNumbers().Any()
    && InkOrTonerPackSize.ExtractNumbers().First() > 1
    && YieldPerCartridgeBlackRef.HasValue()){
        var colors = new List<string>();
        if(YieldPerCartridgeCyanRef.HasValue()){
            colors.Add(YieldPerCartridgeCyanRef + "pages for the cyan");
        }
        if(YieldPerCartridgeMagentaRef.HasValue()){
            colors.Add(YieldPerCartridgeMagentaRef + "pages for the magenta");
        }
        if(YieldPerCartridgeYellowRef.HasValue()){
            colors.Add(YieldPerCartridgeYellowRef + "pages for the yellow");
        }
        if(YieldPerCartridgeGrayRef.HasValue()){
            colors.Add(YieldPerCartridgeGrayRef + "pages for the gray");
        }
        Add($"REDBOX_CartridgeYieldType⸮Yields up to {YieldPerCartridgeBlackRef} pages for black and {string.Join("/", colors).ToLower()} cartridges");
    }
}

// --[FEATURE #2]
// --Package contents (even if 1) including counts and True color
void REDBOX_CartridgePackageContents(){
    var CartridgeYieldTypeRef = REQ.GetVariable("SP-1037").HasValue() ? REQ.GetVariable("SP-1037") :
        R("SP-1037").HasValue() ? R("SP-1037") : R("cnet_common_SP-1037");
if (CartridgeYieldTypeRef =="Other"){
CartridgeYieldTypeRef =A[3531].FirstValueOrDefault();
}

    var InkOrTonerColor = REQ.GetVariable("SP-1036").HasValue() ? REQ.GetVariable("SP-1036") :
        R("SP-1036").HasValue() ? R("SP-1036") : R("cnet_common_SP-1036");
    var SupplyType = REQ.GetVariable("SP-1035").HasValue() ? REQ.GetVariable("SP-1035") :
        R("SP-1035").HasValue() ? R("SP-1035") : R("cnet_common_SP-1035");
    var InkOrTonerPackSize = REQ.GetVariable("SP-2884").HasValue() ? REQ.GetVariable("SP-2884") :
        R("SP-2884").HasValue() ? R("SP-2884") : R("cnet_common_SP-2884");
    var YieldType1 = REQ.GetVariable("SP-351485").HasValue() ? REQ.GetVariable("SP-351485") :
        R("SP-351485").HasValue() ? R("SP-351485") : R("cnet_common_SP-351485");;
    var YieldType2 = REQ.GetVariable("SP-351484").HasValue() ? REQ.GetVariable("SP-351484") :
        R("SP-351484").HasValue() ? R("SP-351484") : R("cnet_common_SP-351484");
    var YieldType1ColorRef = REQ.GetVariable("SP-351481").HasValue() ? REQ.GetVariable("SP-351481") :
        R("SP-351481").HasValue() ? R("SP-351481") : R("cnet_common_SP-351481");
    var YieldType2ColorRef = REQ.GetVariable("SP-351484").HasValue() ? REQ.GetVariable("SP-351484") :
        R("SP-351484").HasValue() ? R("SP-351484") : R("cnet_common_SP-351484");
    var YieldType3ColorRef = REQ.GetVariable("SP-351476").HasValue() ? REQ.GetVariable("SP-351476") :
        R("SP-351476").HasValue() ? R("SP-351476") : R("cnet_common_SP-351476");
    var YieldType4ColorRef = REQ.GetVariable("SP-351480").HasValue() ? REQ.GetVariable("SP-351480") :
        R("SP-351480").HasValue() ? R("SP-351480") : R("cnet_common_SP-351480");
    var YieldType5ColorRef = REQ.GetVariable("SP-351479").HasValue() ? REQ.GetVariable("SP-351479") :
        R("SP-351479").HasValue() ? R("SP-351479") : R("cnet_common_SP-351479");
    var black = BlackCartridge();
    var color = ColorCartridge();
    
    if(InkOrTonerColor.HasValue()
    && (InkOrTonerPackSize.HasValue("1/Pack") || InkOrTonerPackSize.HasValue("Each"))
    && SupplyType.HasValue("MICR")){
        var numb = InkOrTonerPackSize.HasValue("Each") ? "1" : "1";
        Add($"REDBOX_CartridgePackageContents⸮Contains {numb} {InkOrTonerColor.ToLower()} cartridge with MICR");
    }
    else if(InkOrTonerColor.HasValue()
    && (InkOrTonerPackSize.HasValue("1/Pack") || InkOrTonerPackSize.HasValue("Each"))
    && CartridgeYieldTypeRef.HasValue()){
        var numb = InkOrTonerPackSize.HasValue("Each") ? "1" : "1";
        Add($"REDBOX_CartridgePackageContents⸮Contains {numb} {InkOrTonerColor.ToLower()} {CartridgeYieldTypeRef.ToLower()} cartridge");
    }
    else if(black == color
    && YieldType1ColorRef.Text.Split(",").Count() > 1
    && InkOrTonerPackSize.HasValue()
    && InkOrTonerPackSize.ExtractNumbers().Any()){
        var str = YieldType1ColorRef.RegexReplace(@"(.*)?\((.*?)\)", "$2").Text;
        Add($"REDBOX_CartridgePackageContents⸮Contains {InkOrTonerPackSize.ExtractNumbers().First()} cartridges including {str.Insert(str.LastIndexOf(",") + 1, " and")}");
    }
    else if(black > 0
    && color > 0
    && YieldType1ColorRef.HasValue()
    && YieldType1.HasValue() && YieldType2.HasValue()
    && !YieldType1.HasValue($"%{YieldType2}%")){
        var colors = new List<string>(){ YieldType2ColorRef, YieldType3ColorRef, YieldType4ColorRef, YieldType5ColorRef };
        colors = colors.Where(d => d != "").ToList();
        Add($"REDBOX_CartridgePackageContents⸮Contains {black} {YieldType1ColorRef.ToLower()} {YieldType1} cartridge and {color} {string.Join("/", colors).ToLower()} {YieldType2.ToLower()} cartridges");
    }
}

// --[FEATURE #3]
// --Vendor specific information/tagline 
void REDBOX_CartridgeVendorSpecificInformation(){
    var InkOrTonerCartridgeType = REQ.GetVariable("SP-2886").HasValue() ? REQ.GetVariable("SP-2886") :
        R("SP-2886").HasValue() ? R("SP-2886") : R("cnet_common_SP-2886");
        
    if(InkOrTonerCartridgeType.HasValue()){
        Add($"REDBOX_CartridgeVendorSpecificInformation⸮TRU RED {InkOrTonerCartridgeType.ToLower()} cartridges produce print reliable copies");
    }
}

// --[FEATURE #4]
// --If cartridge is twin pack or combo pack, bullet should be a savings message
void REDBOX_CartridgeSavingsMessage(){
    var CartridgeYieldTypeRef = REQ.GetVariable("SP-1037").HasValue() ? REQ.GetVariable("SP-1037") :
        R("SP-1037").HasValue() ? R("SP-1037") : R("cnet_common_SP-1037");

if (CartridgeYieldTypeRef =="Other"){
CartridgeYieldTypeRef =A[3531].FirstValueOrDefault();
}

    var InkOrTonerPackSize = REQ.GetVariable("SP-2884").HasValue() ? REQ.GetVariable("SP-2884") :
        R("SP-2884").HasValue() ? R("SP-2884") : R("cnet_common_SP-2884");
    
    switch(CartridgeYieldTypeRef){
        case var Type when Type.HasValue("Standard Yield")
        && InkOrTonerPackSize.HasValue("1/Pack")
        || InkOrTonerPackSize.HasValue("2/Pack"):
        Add($"REDBOX_CartridgeSavingsMessage⸮Remanufactured cartridges will save you money compared to the National Brands");
        break;
        case var Type when Type.HasValue("Standard Yield")
        && InkOrTonerPackSize.ExtractNumbers().Any()
        && InkOrTonerPackSize.ExtractNumbers().First() > 1:
        Add($"REDBOX_CartridgeSavingsMessage⸮Multipacks save you money and time compared to buying a single cartridge");
        break;
        case var Type when Type.HasValue("%High Yield%")
        && InkOrTonerPackSize.HasValue("1/Pack")
        || InkOrTonerPackSize.HasValue("2/Pack"):
        Add($"REDBOX_CartridgeSavingsMessage⸮High yield cartridges save you money and time compared to buying a standard cartridge");
        break;
        case var Type when Type.HasValue("%High Yield%")
        && InkOrTonerPackSize.ExtractNumbers().Any()
        && InkOrTonerPackSize.ExtractNumbers().First() > 1:
        Add($"REDBOX_CartridgeSavingsMessage⸮High Yield multipacks save you money and time compared to buying a single high yield cartridge");
        break;
    }
}

// --[FEATURE #5]
// --Compatibility
void REDBOX_CartridgeCompatibility(){
    var YieldType1ColorRef = REQ.GetVariable("SP-351481").HasValue() ? REQ.GetVariable("SP-351481") :
        R("SP-351481").HasValue() ? R("SP-351481") : R("cnet_common_SP-351481");
    var CompatibleCartridgeModelRef = REQ.GetVariable("SP-351359").HasValue() ? REQ.GetVariable("SP-351359") :
        R("SP-351359").HasValue() ? R("SP-351359") : R("cnet_common_SP-351359");
    
    if(YieldType1ColorRef.Text.Split(",").Count() > 1
    && CompatibleCartridgeModelRef.HasValue()){
        Add($"REDBOX_CartridgeCompatibility⸮Compatible to {CompatibleCartridgeModelRef.ToLower()}"); 
    }
}

// --[FEATURE #6]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
// --Recycled Content
void REDBOX_CartridgeRecycledContent(){
    var PostConsumerContentRef = R("SP-21623").HasValue() ? R("SP-21623") : R("cnet_common_SP-21623");

    if (!String.IsNullOrEmpty(PostConsumerContentRef)) {
        Add($"REDBOX_CartridgeRecycledContent⸮Contains {PostConsumerContentRef}% post-consumer recycled content");
    }
}

// --[FEATURE #7]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
// --Packaged Recycled Content
void REDBOX_CartridgePackagedRecycledContent(){
    var PackageRecycledContentRef = R("SP-351356").HasValue() ? R("SP-351356") : R("cnet_common_SP-351356");

    if (PackageRecycledContentRef.HasValue()) {
        Add($"REDBOX_CartridgePackagedRecycledContent⸮Packaging contains {PackageRecycledContentRef}% recycled content");
    }
}

// --[FEATURE #8]
// --Compatibility
void REDBOX_CartridgeCompatible(){
    var result = "";
    var Miscellaneous_CompatibleCartridge = Coalesce(A[3302]); // Miscellaneous - Compatible Cartridge
    var Miscellaneous_TargetCompany = Coalesce(A[4898]); // Miscellaneous - Target Company
    var Miscellaneous_Refurbished = Coalesce(A[5312]); // Miscellaneous - Refurbished
    var CompatibleProducts = Coalesce(SPEC["MS"].GetLine("Designed For").Body, SPEC["ES"].GetLine("Designed For").Body);
    var Compatible = REQ.GetVariable("cnet_compatible_ink").HasValue() ? REQ.GetVariable("cnet_compatible_ink") : R("SP-22917");
    
    if(Compatible.HasValue()){
        result = $"Compatible with: {Compatible}";
    }
    else if(CompatibleProducts.HasValue()){
        result = Miscellaneous_CompatibleCartridge.HasValue() ? 
        $"Compatible with: {Miscellaneous_CompatibleCartridge.Values.Select(s => s.Value().Replace("Troy", "TROY").Replace("plus", "Plus").Replace("Envy", "ENVY").Replace("Deskjet", "DeskJet").Replace("Officejet", "OfficeJet").Replace("ImageCLASS","imageCLASS").Replace("Kyocera", "KYOCERA").Replace("Copycentre","CopyCentre").Replace("B405/z","B405/Z").Replace("Troy", "TROY")).FlattenWithAnd().ToString().Split(' ').Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).Flatten(" ")}" :
        $"Compatible with: {CompatibleProducts.Text.Replace("plus", "Plus").Replace("Envy", "ENVY").Replace("Deskjet", "DeskJet").Replace("Officejet", "OfficeJet").Replace("ImageCLASS","imageCLASS").Replace("Kyocera", "KYOCERA").Replace("Copycentre","CopyCentre").Replace("B405/z","B405/Z").Replace("Troy", "TROY").Replace(";", ",").Split(' ').Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).Flatten(" ")}";
    }
    else if(Miscellaneous_TargetCompany.HasValue()
        && Miscellaneous_Refurbished.HasValue("remanufactured")){
        result = $"Manufactured specifically for {Miscellaneous_TargetCompany.FirstValue()}, remanufactured toner cartridges offer fast, hassle-free printing";
    }
    if(!String.IsNullOrEmpty(result)){
        result = Shorten(result, 500, ',');
        Add($"REDBOX_CartridgeCompatible⸮{result}");
    }
}

// --[FEATURE #9]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void REDBOX_CartridgeMediaIncluded(){
    var MediaIncluded_Type = A[2322];
    var MediaIncluded_Size = A[2323];
    var MediaIncluded_IncludedQty = A[2326];

    if(MediaIncluded_Type.HasValue()){
        if(MediaIncluded_Type.Values.Flatten().In("glossy photo paper", "matte photo paper", "photo paper")
            && MediaIncluded_IncludedQty.HasValue()
            && MediaIncluded_Size.HasValue() && MediaIncluded_Size.Values.First().ValueUSM.In("%in%")){ //--Media Included - Size
            Add($"REDBOX_CartridgeMediaIncluded⸮Print beautiful {MediaIncluded_Size.Values.First().ValueUSM.Replace(" in", "")} photos with the included {MediaIncluded_Type.FirstValue()} ({MediaIncluded_IncludedQty.FirstValue()}-sheet) and included inks");
        }
        else if(MediaIncluded_Type.Values.Flatten().ToString().Equals("glossy photo paper")
            && MediaIncluded_IncludedQty.HasValue()){ //--Print beautiful photos with the included glossy photo paper and included inks.
            //--Print beautiful photos and documents with an improved ink formulation that gives you a wider range of colors
            Add($"REDBOX_CartridgeMediaIncluded⸮Print beautiful photos with the included {MediaIncluded_Type.FirstValue()} ({MediaIncluded_IncludedQty.FirstValue()}-sheets) and improved ink formulation that gives you a wider range of colors");
        }
    }
    else if(SKU.ProductId.In("14795486")){
        Add("REDBOX_CartridgeMediaIncluded⸮Print beautiful photos with the included 20 sheets 5 x 7 HP Advanced Photo Paper, 50 sheets 4 x 6 HP Advanced Photo Paper, 15 5 x 7 envelopes, creative booklet with photo and card project ideas");
    }
}

// --[FEATURE #10]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void REDBOX_CartridgeFeatures(){
    var result = "";
    // Ink Tank|Photo Ink|Waste Toner|Fax Cartridge|Waste Toner Bottle|Photo Developer Kit|Solid Ink|Fuser Kit|Transfer Kit|Waste Toner Box|Maintenance Kit|Photo Conductor Kit|Oil Bottle|Glossy Photo Print Pack|Refill Kit|Drum|MICR|Other|Printhead/Cleaner|Ink/Printhead/Cleaner|Ink|Usage kit|Imaging Unit|Toner|Printhead|Rolls
    var SupplyType = REQ.GetVariable("SP-1035").HasValue() ? REQ.GetVariable("SP-1035").Replace("<NULL>", "").Text :
        R("SP-1035").HasValue() ? R("SP-1035").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-1035").HasValue() ? R("cnet_common_SP-1035").Replace("<NULL>", "").Text : "";
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
        Add($"REDBOX_CartridgeFeatures⸮{result}");
    }
}

// --[FEATURE #11]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void REDBOX_CartridgeRemanufactured(){
    var inkOrTonerSpecialTypeRef = REQ.GetVariable("SP-22914").HasValue() ? REQ.GetVariable("SP-22914") :
        R("SP-22914").HasValue() ? R("SP-22914") : R("cnet_common_SP-22914");

    if(inkOrTonerSpecialTypeRef.ToLower().Equals("use and return")){
        Add("REDBOX_CartridgeRemanufactured⸮Use and Return toner cartridges are specially priced for customers to use once, and are not to be refilled or remanufactured");
    }
}

// --[FEATURE #12]
// Warranty information
void REDBOX_CartridgeWarranty(){
    var result = "";
    var referList = new List<string>(){"SP-16", "SP-5675", "SP-21932"};
    
    foreach(var refer in referList){
        var temp = REQ.GetVariable(refer).HasValue() ? REQ.GetVariable(refer).Replace("<NULL>", "").Text : 
        R(refer).HasValue() ? R(refer).Replace("<NULL>", "").Text : 
        R($"cnet_common_{refer}").HasValue() ? R($"cnet_common_{refer}").Replace("<NULL>", "").Text : "";
        if(!String.IsNullOrEmpty(temp)){
            // Warranty = temp;
            var numbers = Coalesce(temp).ExtractNumbers();
            if(numbers.Any()){
                switch(temp.ToLower()){
                    case var str when str.Contains("year"):
                    result = $"{numbers.First()}-year guarantee to be free of manufacturers defects, get free replacement or your money back";
                    break;
                    case var str when str.Contains("month"):
                    result = $"{numbers.First()}-month guarantee to be free of manufacturers defects, get free replacement or your money back";
                    break;
                    case var str when str.Contains("day"):
                    result = $"{numbers.First()}-day guarantee to be free of manufacturers defects, get free replacement or your money back";
                    break;
                }
            }
            else if(temp.ToLower().Contains("replacement")){
                result = "Lifetime manufacturer limited replacement warranty";
                break;
            }
            else if(temp.ToLower().Contains("ifetime manufacturer")
            || temp.ToLower().Contains("imited lifetime warranty") || temp.ToLower().Contains("lifetime manufacturer limited warranty")){          
                result = "Lifetime manufacturer limited warranty";
                break;
            }
        }
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"REDBOX_CartridgeWarranty⸮{result}");
    }
}

//5226058, 5226054, 5226006, 12393811005220973, 5226157, 12375510567216950, 12366910285216553, 1212164913166147, 12378610641217427 "Remanufactured Laser Printer Ink, Toner & Drum Units BEGIN" "Serhii.O" §§

//§§1683393110991220928 "Hand Sanitizer & Dispensers BEGIN" "Serhii.O" 

HandSDCleanserFormFactorUse(); 
HandSDScent(); 
HandSDCapacity(); 
HandSDAreaOfUse(); 
HandSDMaterialMainIngredient(); 
// Pack Qty - All 
HandSDSanitizerDispenserTypeUsage(); 
// HandSDCartridgePumpBottle(); 
HandSDMotionDetection(); 
HandSDWallMountableSightWindow(); 
// Compliant Standards - All 
HandSDEcoLogo(); 
HandSDUSDACertifiedBiobased(); 
HandSDUSDABioPreferred(); 
HandSDAlcoholBased(); 
HandSDAlcoholFree(); 
HandSDNonPerfumed(); 
HandSDNonLinting(); 
HandSDSingleWrapped(); 
// Warranty Information - All 

// --[FEATURE #1]
// --Cleanser form factor & use
void HandSDCleanserFormFactorUse(){
    var result = "";
    // Push-Style Dispenser|Automatic Dispensers|Touch-Free Dispenser|Manual Dispensers|Liquid Sanitizers|Foaming Sanitizers|Liquid Soap|Dispenser & Refill Kit|Wipes|Refills|Foaming Soap|Gel Sanitizers|Powder
    var cleanserFormFactorRef = REQ.GetVariable("SP-18148").HasValue() ? REQ.GetVariable("SP-18148").Replace("<NULL>", "").Text :
        R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : "";
    
    if(!string.IsNullOrEmpty(cleanserFormFactorRef)){
        switch(cleanserFormFactorRef){
            // https://www.staples.com/purell-es4-push-style-hand-sanitizer-dispenser-graphite-for-1200-ml-purell-es4-hand-sanitizer-refills-1-ct-5024-01/product_2728961
            // https://www.staples.com/purell-es4-push-style-soap-dispenser-graphite-for-1200-ml-purell-es4-soap-refill-5034-01/product_2728959
            case "Push-Style Dispenser":
            result = "Push-style dispensers helps create a heathy environment and showcases your commitment to cleanliness";
            break;
            case "Foaming Sanitizers":
            result = "Specially designed foaming formula, provides an amazing gentle cleansing experience";
            break;
            case "Gel Sanitizers":
            result = "Advanced gel sanitizer formulation designed for healthcare environments";
            break;
            case "Liquid Sanitizers":
            result = "Liquid sanitizer is designed to keep you germ-free without sacrificing everyday personal comfort";
            break;
            // https://www.staples.com/simplehuman-Compact-Sensor-Pump-Soap-Dispenser-Brushed-Nickel/product_192792
            case "Automatic Dispensers":
            result = "Automatically dispenses the perfect amount of foam soap needed for hand washing in one shot";
            break;
            // https://www.staples.com/Trademark-Home-Touchless-Automatic-Liquid-Soap-Dispenser-Black/product_237561
            // https://www.staples.com/Alpine-Industries-Automatic-Hands-Free-Bulk-Foam-Soap-Dispenser-33-oz-Capacity-Gray-ALP422-GRY/product_24190975
            case var a when a == "Touch-Free Dispenser" || a == "Touch-Free Dispensers": //Touch-free dispenser helps reduce mess and spread of germs
            result = "Touch-free hand sanitizer dispenser offers a hygienic solution for professional restrooms and hand washing areas";
            break;
            // https://www.staples.com/Brighton-Professional-ADX-12-Foam-Soap-Dispenser-White-Gray/product_371982
            // https://www.staples.com/Brighton-Professional-ADX-12-Foam-Soap-Dispenser-White-Gray/product_371982
            case "Foaming Soap":
            result = "Foaming hand soap dispenser";
            break;
            // https://www.staples.com/GOJO-Antibacterial-Foam-Handwash-Starter-Kit-Plum-Scent-23-67-oz-8712-D1/product_148720
            case "Manual Dispensers":
            result = "Manual dispenser is engineered for durability and easy servicing";
            break;
            case "Wipes":
            result = "Wipes are ideal for use anytime your employees do not have easy access to running water";
            break;
        }
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"HandSDCleanserFormFactorUse⸮{result}");
    }
}

// --[FEATURE #2] 
// --Scent (If Applicable) 
void HandSDScent(){ 
var result = ""; 
// Woodsy|Lavender|Tea|Original|Seasons|Strawberry|Clean|Floral|Masculine|Bakery|Fruity|No Scent|Nature|Spicy|Oceanic|Fresh Citrus|Lemon|Unscented|Other|Gain|Low odor|Almond|Vanilla|Herbal 
var scentRef = REQ.GetVariable("SP-18147").HasValue() ? REQ.GetVariable("SP-18147").Replace("<NULL>", "").Text : 
R("SP-18147").HasValue() ? R("SP-18147").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18147").HasValue() ? R("cnet_common_SP-18147").Replace("<NULL>", "").Text : ""; 
var general_Fragrance = A[7356]; // General - Fragrance 

if(!string.IsNullOrEmpty(scentRef)){ 
switch(scentRef){ 
case "No Scent": 
result = "Unscented to cause less irritation, even to people with sensitive skin"; 
break; 
case "Clean": 
result = "Leaves behind the fresh, clean scent for pleasant effect"; 
break; 
case var a when a.Equals("Fruity") 
&& !general_Fragrance.HasValue("Fruity"): 
result = $"The exotic fruity scent of {general_Fragrance.WhereNot("Fruity").Flatten(", ")}"; 
break; 
case "Fruity": 
result = "The exotic fruity scent brings your hands a delicious treat"; 
break; 
case var a when a.Equals("Herbal") 
&& !general_Fragrance.HasValue("Fruity"): 
result = $"Amazing herbal {general_Fragrance.WhereNot("Fruity").Flatten(", ")} scent with no harsh chemical smell"; 
break; 
case "Herbal": 
result = "Amazing herbal scent with no harsh chemical smell"; 
break; 
default: 
result = $"Amazing {scentRef.ToLower()} scent with no harsh chemical smell"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDScent⸮{result}"); 
} 
} 

// --[FEATURE #3]
// --Capacity (oz.) & spa or salon products container type
void HandSDCapacity(){
    var result = "";
    var capacityRef = REQ.GetVariable("SP-21132").HasValue() ? REQ.GetVariable("SP-21132").Replace("<NULL>", "").Text :
        R("SP-21132").HasValue() ? R("SP-21132").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21132").HasValue() ? R("cnet_common_SP-21132").Replace("<NULL>", "").Text : "";
    var generalSheetSize = A[6185]; // General - Sheet Size
    var general_SheetsQty = A[6186]; // General - Sheet Size
    var general_Capacity = A[6803]; //General - Capacity
    var general_ProductType = A[7349]; // General - Product Type
    var general_ContainerType = A[7351]; // General - Container Type
    var SheetSizeFirst = "";
    var SheetSizeSecond = "";
    
    if(!string.IsNullOrEmpty(capacityRef)
    && general_ContainerType.HasValue("pump bottle")
    && general_Capacity.HasValue()
    && general_Capacity.Units.First().NameUSM.In("%gal%")
    && Coalesce(general_Capacity.Values.First().ValueUSM).ExtractNumbers().Any()
    && Coalesce(general_Capacity.Values.First().ValueUSM).ExtractNumbers().First() * 128 < 5.0){
        result = $"The convenient {capacityRef} oz. personal-sized bottle of hand sanitizer is perfect for carrying";
    }
    else if(!string.IsNullOrEmpty(capacityRef)
    && general_ContainerType.HasValue("pump bottle")
    && general_Capacity.HasValue()
    && general_Capacity.Units.First().NameUSM.In("%fl.oz%")
    && Coalesce(general_Capacity.Values.First().ValueUSM).ExtractNumbers().Any()
    && Coalesce(general_Capacity.Values.First().ValueUSM).ExtractNumbers().First() < 5.0){
        result = $"The convenient {capacityRef} oz. personal-sized bottle of hand sanitizer is perfect for carrying with you when you go shopping or when you travel on business";
    }
    else if(!string.IsNullOrEmpty(capacityRef)
    && general_ContainerType.HasValue("pump bottle")
    && general_Capacity.HasValue()
    && general_Capacity.Units.First().NameUSM.In("%gal%")
    && Coalesce(general_Capacity.Values.First().ValueUSM).ExtractNumbers().Any()
    && Coalesce(general_Capacity.Values.First().ValueUSM).ExtractNumbers().First() * 128 < 9.0){
        result = $"Compact {capacityRef} oz. pump bottle is ideal for small spaces and vehicles";
    }
    else if(!string.IsNullOrEmpty(capacityRef)
    && general_ContainerType.HasValue("pump bottle")
    && general_Capacity.HasValue()
    && general_Capacity.Units.First().NameUSM.In("%fl.oz%")
    && Coalesce(general_Capacity.Values.First().ValueUSM).ExtractNumbers().Any()
    && Coalesce(general_Capacity.Values.First().ValueUSM).ExtractNumbers().First() < 9.0){
        result = $"Compact {capacityRef} oz. pump bottle is ideal for small spaces and vehicles";
    }
    else if(!string.IsNullOrEmpty(capacityRef)
    && general_ProductType.HasValue("%Dispenser%")){
        result = $"This {capacityRef} oz. capacity dispenser helps to reduce maintenance";
    }
    else if(!string.IsNullOrEmpty(capacityRef)){
        var type = general_ContainerType.HasValue() ? $"{general_ContainerType.FirstValue().ToLower().ToUpperFirstChar()} contains " : "Contains ";
        result = $"{type}{capacityRef} oz. for multiple uses makes this an economical choice";
    }
    else if(general_SheetsQty.HasValue()
    && general_SheetsQty.FirstValue() < 300
    && general_ProductType.HasValue("cleaning wipes")){
        SheetSizeFirst = generalSheetSize.HasValue() && generalSheetSize.FirstValue().ExtractNumbers().Any() ? $@"{Math.Round(generalSheetSize.FirstValue().ExtractNumbers().First() * 0.0393701, 2)}""W x " : "";
        SheetSizeSecond = generalSheetSize.HasValue() && generalSheetSize.FirstValue().ExtractNumbers().Any() ? $@"{Math.Round(generalSheetSize.FirstValue().ExtractNumbers().Last() * 0.0393701, 2)}""L" : "";
        result = $"Economical container dispenses {general_SheetsQty.FirstValue()} {SheetSizeFirst}{SheetSizeSecond} wipes";
    }
    else if(general_ProductType.HasValue("cleaning wipes")){
        SheetSizeFirst = generalSheetSize.HasValue() && generalSheetSize.FirstValue().ExtractNumbers().Any() ? $@"{Math.Round(generalSheetSize.FirstValue().ExtractNumbers().First() * 0.0393701, 2)}""W x " : "";
        SheetSizeSecond = generalSheetSize.HasValue() && generalSheetSize.FirstValue().ExtractNumbers().Any() ? $@"{Math.Round(generalSheetSize.FirstValue().ExtractNumbers().Last() * 0.0393701, 2)}""L" : "";
        result = $"Wipes measuring {SheetSizeFirst}{SheetSizeSecond}";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"HandSDCapacity⸮{result}");
    }
}

// --[FEATURE #4] 
// --Area of use 
void HandSDAreaOfUse(){ 
var result = ""; 
var general_RecommendedIndustryApplication = A[7093]; // General - Recommended Industry / Application 
var general_RecommendedUse = A[7353]; // General - Recommended Use 

if(general_RecommendedIndustryApplication.HasValue() 
&& general_RecommendedIndustryApplication.Values.Flatten(", ").In("hospitals")){ 
result = "Because of its germ-fighting properties, Kleenex Instant Hand Sanitizer is popular for use in offices and hospitality"; 
} 
else if(general_RecommendedUse.HasValue() 
&& general_RecommendedUse.Values.Flatten(", ").In("hands")){ 
result = "Effective, quick and convenient way to kill germs on hands"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDAreaOfUse⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Material/main ingredient; includes properties & additional ingredients 
void HandSDMaterialMainIngredient(){ 
var result = ""; 
var miscellaneous_ProductMaterial = A[372]; // Miscellaneous - Product Material 
var general_Components = A[7355]; // General - Components 

if(miscellaneous_ProductMaterial.HasValue()) 
{ 
result = ""; 
} 
else if(general_Components.HasValue("Aloe") 
&& general_Components.HasValue("Ethanol", "70% ethanol") 
&& general_Components.Where("Ethanol", "70% ethanol").Any()){ 
result = "Alcohol-based formula contains aloe for a soothing effect on skin"; 
} 
else if(general_Components.HasValue("Aloe") 
&& general_Components.HasValue("Vitamin E")){ 
result = "Contains aloe and vitamin E to moisturize the skin"; 
} 
else if(general_Components.HasValue("Aloe")){ 
result = "Aloe refreshes and soothes skin and leaves no sticky residue"; 
} 
else if(general_Components.HasValue("Ethanol", "70% ethanol") 
&& general_Components.Where("Ethanol", "70% ethanol").Any()){ 
result = $"{general_Components.FirstValue().ToUpperFirstChar()} formula evaporates quickly, making it useful for just about anywhere, especially for healthcare ##and food service environments"; 
} 
else if(general_Components.HasValue("Vitamin E")){ 
result = "Contains vitamin E to moisturize and care for the skin"; 
} 
else if(general_Components.HasValue("triclosan")){ 
result = "Active ingredient triclosan kills a broad spectrum of bacteria and yeasts"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDMaterialMainIngredient⸮{result}"); 
} 
} 

// --[FEATURE #6] - All 
// --Pack Qty (If more than 1) 

// --[FEATURE #7] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDSanitizerDispenserTypeUsage(){ 
var result = ""; 
var general_ProductType = A[7349]; // General - Product Type 
var holder_Type = A[7368]; // Holder - Type 
var holder_Features = A[7379]; // Holder - Features 

if(general_ProductType.HasValue("hand sanitizer")){ 
result = "Sanitizer is a great alternative when soap and water are unavailable"; 
} 
else if(holder_Type.HasValue("hand sanitizer dispenser", "cleaner dispenser")){ 
result = $"Professional looking {holder_Type.FirstValue()} that distributes the precise amounts of liquid"; 
} 
else if(holder_Type.HasValue("cleaning wipes dispenser") 
&& holder_Features.HasValue("wall-mountable")){ 
result = "Sanitizing wipe wall mount dispenser is specially designed to dispense sanitizing wipes"; 
} 
else if(general_ProductType.HasValue("cleaning wipes")){ 
result = "Wipes are ideal for use anytime your employees do not have easy access to running water"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDSanitizerDispenserTypeUsage⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
/*void HandSDCartridgePumpBottle(){ 
var result = ""; 
var general_ProductType = A[7349]; // General - Product Type 
var general_ContainerType = A[7351]; // General - Container Type 
var general_RecommendedUse = A[7353]; // General - Recommended Use 

if(general_ContainerType.HasValue("Pump Bottle") 
&& (general_RecommendedUse.HasValue() 
&& general_RecommendedUse.Values.Flatten(", ").In("hands") || general_ProductType.HasValue("%hand%"))){ 
result = "Each bottle comes with a convenient pump for easy dispensing wherever and whenever you need to clean your hands"; 
} 
else if(general_ContainerType.HasValue("%Cartridge%")){ 
result = "Cartridge pops in easily and works with dispenser internal mechanisms to release the perfect amount of sanitizer with every pump"; 
} 
else if(general_ContainerType.HasValue("%Bag%")){ 
result = "Sanitary, sealed refill bags are easy to load"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDCartridgePumpBottle⸮{result}"); 
} 
}*/ 

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDMotionDetection(){ 
var result = ""; 
var holder_OperatingMode = A[6975]; // Holder - Operating Mode 

if(holder_OperatingMode.HasValue() 
&& holder_OperatingMode.Values.Flatten(" ").In("motion detection")){ 
result = "Touchless soap and sanitizer dispensing systems are uniquely designed to simplify and reduce maintenance time, deliver continuous service with high-capacity refills, and offer a hygienic solution for professional restrooms and hand washing areas"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDMotionDetection⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDWallMountableSightWindow(){ 
var result = ""; 
var holder_Features = A[7379]; // Holder - Features 

if(holder_Features.HasValue("%wall%mountable%")){ 
result = "The wall-mounted system is designed to allow for easy checking and one-hand installation of refill bottles"; 
} 
else if(holder_Features.HasValue("%sight%window%")){ 
result = "Large sight window, skylight and crystal clear refill bottles make it easy to check fill status"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDWallMountableSightWindow⸮{result}"); 
} 
} 

// --[FEATURE #11] - All 
// --Compliant Standards 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDEcoLogo(){ 
var result = ""; 
var miscellaneous_CompliantStandards = A[380]; // Miscellaneous - Compliant Standards 

if(miscellaneous_CompliantStandards.HasValue("EcoLogo")){ 
result = "Meets the EcoLogo environmental standard for instant hand antiseptics based on use of less intrusive raw materials, a reduction of environmental hazards and an increase of product recyclability"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDEcoLogo⸮{result}"); 
} 
} 

// --[FEATURE #13] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDUSDACertifiedBiobased(){ 
var result = ""; 
var miscellaneous_CompliantStandards = A[380]; // Miscellaneous - Compliant Standards 

if(miscellaneous_CompliantStandards.HasValue("USDA Certified Biobased")){ 
result = "USDA certified biobased product for sustainability"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDUSDACertifiedBiobased⸮{result}"); 
} 
} 

// --[FEATURE #14] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDUSDABioPreferred(){ 
var result = ""; 
var miscellaneous_CompliantStandards = A[380]; // Miscellaneous - Compliant Standards 

if(miscellaneous_CompliantStandards.HasValue("USDA BioPreferred")){ 
result = "Qualifies as a USDA BioPreferred product for Federal preferred procurement"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDUSDABioPreferred⸮{result}"); 
} 
} 

// --[FEATURE #15] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDAlcoholBased(){ 
var result = ""; 
var general_Features = A[7367]; // Miscellaneous - Compliant Standards 

if(general_Features.HasValue("alcohol-based")){ 
result = "Alcohol-based formula provides superior sanitation in the absence of soap and water"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDAlcoholBased⸮{result}"); 
} 
} 

// --[FEATURE #16] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDAlcoholFree(){ 
var result = ""; 
var general_Features = Coalesce(A[7367], A[6786]); // Miscellaneous - Compliant Standards 

if(general_Features.HasValue("alcohol-free")){ 
result = "Alcohol-free hand sanitizer effectively kills germs and bacteria that spread disease without the use of alcohol"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDAlcoholFree⸮{result}"); 
} 
} 

// --[FEATURE #17] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDNonPerfumed(){ 
var result = ""; 
var general_Features = A[7367]; // Miscellaneous - Compliant Standards 

if(general_Features.HasValue("%non perfumed%")){ 
result = "This product is unscented, making it ideal for those sensitive to perfumes"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDNonPerfumed⸮{result}"); 
} 
} 

// --[FEATURE #18] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDNonLinting(){ 
var result = ""; 
var general_ProductType = A[7349]; // General - Product Type 
var general_Features = A[7367]; // Miscellaneous - Compliant Standards 

if(general_Features.HasValue("%non-linting%") 
&& general_ProductType.HasValue("%wipes")){ 
result = "These soft wipes feature a strong, durable material that resists tearing, pilling and linting"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDNonLinting⸮{result}"); 
} 
} 

// --[FEATURE #19] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HandSDSingleWrapped(){ 
var result = ""; 
var general_Features = A[7367]; // Miscellaneous - Compliant Standards 

if(general_Features.HasValue("%single wrapped%")){ 
result = "Toss a couple of these individually wrapped hand-sanitizing wipes into your purse or suitcase to ensure sanitation during business travel"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HandSDNonPerfumed⸮{result}"); 
} 
} 

// --[FEATURE #13] - All 
// --Warranty Information 

//1683393110991220928 "Hand Sanitizer & Dispensers END" "Serhii.O" §§ 

//§§5435443731158948 "Scanners BEGIN" "Serhii.O" 

ScannersTypeConnectivityUse(); //Not yet 
ScannersScannerResolution(); 
ScannersScanningModeSpeed(); //Not yet 
ScannersFormatsSupported(); 
ScannersDocumentFeederCapacityLoadingType(); 
ScannersInterface(); 
ScannersCompatability(); 
ScannersDataPorts(); 
ScannersRefurbished(); 
// Certifications and Standards - All 
ScannersOCR(); 
ScannersPoweredByUSBPort(); 
ScannersDigitalICETechnology(); 
ScannersEasyPhotoFix(); 
ScannersFlashMemory(); 
ScannersEasyViewing(); 
// Warranty - All 

// --[FEATURE #1] 
// --Scanner type, connectivity & use 
void ScannersTypeConnectivityUse(){ 
    var result = "";
    // Portable/Mobile Scanner|Photo Sheetfed Scanner|Flatbed Scanner|Sheetfed Scanner|Desktop|Portable
    var scannerTypeRef = REQ.GetVariable("SP-18369").HasValue() ? REQ.GetVariable("SP-18369").Replace("<NULL>", "").Text :
        R("SP-18369").HasValue() ? R("SP-18369").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18369").HasValue() ? R("cnet_common_SP-18369").Replace("<NULL>", "").Text : "";
    // Wired/Ethernet|Wireless|HDMI|Micro USB|USB/Wireless|MicroSD|USB|DVI|Wireless & Ethernet
    var scannerConnectivityRef = REQ.GetVariable("SP-18368").HasValue() ? REQ.GetVariable("SP-18368").Replace("<NULL>", "").Text :
        R("SP-18368").HasValue() ? R("SP-18368").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18368").HasValue() ? R("cnet_common_SP-18368").Replace("<NULL>", "").Text : "";
    
    if(!string.IsNullOrEmpty(scannerTypeRef)){
        switch(scannerTypeRef){
            case var Type when Type.Equals("Desktop")
            && scannerConnectivityRef.Equals("Wired/Ethernet"):
            result = "Desktop scanner with ##Ethernet connection is ideal for your home or office";
            break;
            case var Type when Type.Equals("Desktop")
            && scannerConnectivityRef.Equals("Wireless & Ethernet"):
            result = "Desktop scanner with wireless and ##Ethernet connections is ideal for your home or office";
            break;
            case var Type when Type.Equals("Desktop")
            && scannerConnectivityRef.Equals("USB/Wireless"):
            result = "Desktop scanner with wireless and ##USB connections is ideal for your home or office";
            break;
            case var Type when Type.Equals("Desktop")
            && !string.IsNullOrEmpty(scannerConnectivityRef):
            result = $"Desktop scanner with {scannerConnectivityRef.ToLower()} connection is ideal for your home or office";
            break;
            case var Type when Type.Equals("Portable")
            && scannerConnectivityRef.Equals("Wired/Ethernet"):
            result = "Portable scanner with ##Ethernet connection for easy and fast on-the-go scanning";
            break;
            case var Type when Type.Equals("Portable")
            && scannerConnectivityRef.Equals("Wireless & Ethernet"):
            result = "Portable scanner with wireless and ##Ethernet connections for easy and fast on-the-go scanning";
            break;
            case var Type when Type.Equals("Portable")
            && scannerConnectivityRef.Equals("USB/Wireless"):
            result = "Portable scanner with wireless and ##USB connections for easy and fast on-the-go scanning";
            break;
            case var Type when Type.Equals("Portable")
            && !string.IsNullOrEmpty(scannerConnectivityRef):
            result = $"Portable scanner with {scannerConnectivityRef.ToLower()} connection for easy and fast on-the-go scanning";
            break;
            case var Type when Type.Equals("Photo Sheetfed Scanner")
            && scannerConnectivityRef.Equals("Wired/Ethernet"):
            result = "Photo sheetfed scanner with ##Ethernet connection captures images, photographs and diagrams";
            break;
            case var Type when Type.Equals("Photo Sheetfed Scanner")
            && scannerConnectivityRef.Equals("Wireless & Ethernet"):
            result = "Photo sheetfed scanner with wireless and ##Ethernet connections captures images, photographs and diagrams";
            break;
            case var Type when Type.Equals("Photo Sheetfed Scanner")
            && scannerConnectivityRef.Equals("USB/Wireless"):
            result = "Photo sheetfed scanner with wireless and ##USB connections captures images, photographs and diagrams";
            break;
            case var Type when Type.Equals("Photo Sheetfed Scanner")
            && !string.IsNullOrEmpty(scannerConnectivityRef):
            result = $"Photo sheetfed scanner with {scannerConnectivityRef.ToLower()} connection captures images, photographs and diagrams";
            break;
            case var Type when Type.Equals("Flatbed Scanner")
            && scannerConnectivityRef.Equals("Wired/Ethernet"):
            result = "Flatbed scanner with ##Ethernet connection for making quick, easy copies of documents and images";
            break;
            case var Type when Type.Equals("Flatbed Scanner")
            && scannerConnectivityRef.Equals("Wireless & Ethernet"):
            result = "Flatbed scanner with wireless and ##Ethernet connections for making quick, easy copies of documents";
            break;
            case var Type when Type.Equals("Flatbed Scanner")
            && scannerConnectivityRef.Equals("USB/Wireless"):
            result = "Flatbed scanner with wireless and ##USB connections for making quick, easy copies of documents";
            break;
            case var Type when Type.Equals("Flatbed Scanner")
            && !string.IsNullOrEmpty(scannerConnectivityRef):
            result = $"Flatbed scanner with {scannerConnectivityRef.ToLower()} connection for making quick, easy copies of documents and images";
            break;
            case var Type when Type.Equals("Sheetfed Scanner")
            && scannerConnectivityRef.Equals("Wired/Ethernet"):
            result = "Sheetfed scanner with ##Ethernet connection is a good choice for scanning large documents";
            break;
            case var Type when Type.Equals("Sheetfed Scanner")
            && scannerConnectivityRef.Equals("Wireless & Ethernet"):
            result = "Sheetfed scanner with wireless and ##Ethernet connections is a good choice for scanning large documents";
            break;
            case var Type when Type.Equals("Sheetfed Scanner")
            && scannerConnectivityRef.Equals("USB/Wireless"):
            result = "Sheetfed scanner with wireless and ##USB connections is a good choice for scanning large documents";
            break;
            case var Type when Type.Equals("Sheetfed Scanner")
            && !string.IsNullOrEmpty(scannerConnectivityRef):
            result = $"Sheetfed scanner with {scannerConnectivityRef.ToLower()} connection is a good choice for scanning large documents";
            break;
            case var Type when Type.Equals("Portable/Mobile Scanner")
            && scannerConnectivityRef.Equals("Wired/Ethernet"):
            result = "Portable/Mobile Scanner with ##Ethernet connection is ideal to scan books, magazines & newspapers";
            break;
            case var Type when Type.Equals("Portable/Mobile Scanner")
            && scannerConnectivityRef.Equals("Wireless & Ethernet"):
            result = "Portable/Mobile Scanner with wireless and ##Ethernet connections is ideal to scan books and magazines";
            break;
            case var Type when Type.Equals("Portable/Mobile Scanner")
            && scannerConnectivityRef.Equals("USB/Wireless"):
            result = "Portable/Mobile Scanner with wireless and ##USB connections is ideal to scan books and magazines";
            break;
            case var Type when Type.Equals("Portable/Mobile Scanner")
            && !string.IsNullOrEmpty(scannerConnectivityRef):
            result = $"Portable/Mobile Scanner with {scannerConnectivityRef.ToLower()} connection is ideal to scan books and magazines";
            break;
            case var Type when !string.IsNullOrEmpty(Type)
            && scannerConnectivityRef.Equals("Wired/Ethernet"):
            result = $"{Type.ToLower().ToUpperFirstChar()} with ##Ethernet connection is ideal for your home or office";
            break;
            case var Type when !string.IsNullOrEmpty(Type)
            && scannerConnectivityRef.Equals("Wireless & Ethernet"):
            result = $"{Type.ToLower().ToUpperFirstChar()} with wireless and ##Ethernet connections is ideal for your home or office";
            break;
            case var Type when !string.IsNullOrEmpty(Type)
            && scannerConnectivityRef.Equals("USB/Wireless"):
            result = $"{Type.ToLower().ToUpperFirstChar()} with wireless and ##USB connections is ideal for your home or office";
            break;
            case var Type when !string.IsNullOrEmpty(Type)
            && !string.IsNullOrEmpty(scannerConnectivityRef):
            result = $"{Type.ToLower().ToUpperFirstChar()} with {scannerConnectivityRef.ToLower()} connection is ideal for your home or office";
            break;
        }
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"ScannersTypeConnectivityUse⸮{result}");
    }
} 

// --[FEATURE #2] 
// --Scanner resolution 
void ScannersScannerResolution(){ 
var result = ""; 
// 9600 x 9600 dpi|9600 dpi|600 dpi|900 dpi|7200 dpi|7200 x 7200 dpi|1200 dpi|Up to 300 dpi|Up 2400 dpi|Up to 1200 dpi|4800 x 9600 dpi|6400 x 9600 dpi|6400 dpi|600 x 600 dpi|4800 x 4800 dpi|285 - 218 dpi|2500 dpi|2400 x 4800 dpi|2400 x 2800 dpi|4800 dpi|400 dpi|300 dpi|4800 x 6400 dpi|1200 x 1200 dpi|Up to 5000 dpi|2400 dpi|1600 dpi|Up to 9 Mp|Up to 7200|1201 - 6399 dpi|Up to 600 dpi 
var imageFileFormatsRef = REQ.GetVariable("SP-13117").HasValue() ? REQ.GetVariable("SP-13117").Replace("<NULL>", "").Text : 
R("SP-13117").HasValue() ? R("SP-13117").Replace("<NULL>", "").Text : 
R("cnet_common_SP-13117").HasValue() ? R("cnet_common_SP-13117").Replace("<NULL>", "").Text : ""; 

if(SKU.ProductId.In("17233753")){ 
result = "300 dpi optical resolution offers accurate rendition"; 
} 
else if(!string.IsNullOrEmpty(imageFileFormatsRef)){ 
switch(imageFileFormatsRef){ 
case "300 dpi": 
result = "300 dpi optical resolution offers accurate rendition"; 
break; 
case var res when res.Equals("600 dpi") 
|| res.Equals("600 x 600 dpi") 
|| res.Equals("900 dpi"): 
result = $"{res} optical resolution for good-quality printed text and images"; 
break; 
case "Up to 600 dpi": 
result = "Creates high-quality images with a resolution of up to 600 dpi"; 
break; 
case "Up to 1200 dpi": 
result = "Creates high-quality images with a resolution of up to 1200 dpi"; 
break; 
case "Up to 2400 dpi": 
result = "Creates high-quality images with a resolution of up to 2400 dpi"; 
break; 
case "1200 dpi": 
result = "1200 dpi scan resolution for crisp high-quality color output"; 
break; 
case var res when res.Equals("2400 dpi") 
|| res.Equals("2400 x 2800 dpi") 
|| res.Equals("2400 x 4800 dpi"): 
result = $"{res} resolution produces crisp images"; 
break; 
case var res when res.Equals("4800 dpi") 
|| res.Equals("4800 x 4800 dpi"): 
result = $"{res} resolution generates exceptional detail and image clarity"; 
break; 
case "4800 x 9600 dpi": 
result = "4800 x 9600 dpi resolution captures every detail"; 
break; 
case var res when res.Contains("6400") 
|| res.Contains("7200") 
|| res.Contains("9600"): 
result = $"{res} resolution for high-quality images"; 
break; 
default: 
result = $"Creates high-quality images with a resolution of {imageFileFormatsRef}"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersScannerResolution⸮{result}"); 
} 
} 

// --[FEATURE #3]
// --Scanning mode & speed; combined
void ScannersScanningModeSpeed(){
    var result = "";
    var scanner_ScanMode = A[202];
    var scanner_ScanSpeed = A[211];
    var scannerSpeedDetails_Mode = A[2311];
    var scannerSpeedDetails_ModeBlack = A[2312];
    var scanner_MaxDocumentScanSpeed = A[4615];
    var scanner_MaxDocumentScanSpeedColor = A[4616];
    var scannerSpeedDetails_Speed = A[2309];
    var scannerSpeedDetails_Document = A[2310];
    var scannerSpeedDetails_Duplex = A[2679];
    
    if(scanner_ScanMode.HasValue("single/duplex pass")
    && (scannerSpeedDetails_Mode.HasValue("%color%") 
    || scannerSpeedDetails_ModeBlack.HasValue("%color%") 
    || scanner_MaxDocumentScanSpeedColor.HasValue())
    && (scannerSpeedDetails_Mode.HasValue("%black&white%", "%B&W%", "greyscale") 
    || scannerSpeedDetails_ModeBlack.HasValue("%black&white%", "%B&W%", "greyscale")
    || scanner_MaxDocumentScanSpeed.HasValue())
    && scanner_ScanSpeed.HasValue()){
        var speed = scanner_ScanSpeed.GetValuesWithUnits();
        result = $"Single-pass duplex scanning of color and black/white documents at speed of up to {speed.First().Value.Value()} {speed.First().Unit.Name.RegexReplace(@"\s?\(.*\)$", "")}";
    }
    else if(scanner_ScanMode.HasValue("single/duplex pass")
    && (scannerSpeedDetails_Mode.HasValue("%color%") 
    || scannerSpeedDetails_ModeBlack.HasValue("%color%") 
    || scanner_MaxDocumentScanSpeedColor.HasValue())
    && (scannerSpeedDetails_Mode.HasValue("%black&white%", "%B&W%", "greyscale") 
    || scannerSpeedDetails_ModeBlack.HasValue("%black&white%", "%B&W%", "greyscale")
    || scanner_MaxDocumentScanSpeed.HasValue())
    && scannerSpeedDetails_Speed.HasValue()){
        var speed = scannerSpeedDetails_Speed.GetValuesWithUnits();
        result = $"Single-pass duplex scanning of color and black/white documents at speed of up to {speed.First().Value.Value()} {speed.First().Unit.Name.RegexReplace(@"\s?\(.*\)$", "")}";
    }
    else if(scannerSpeedDetails_Duplex.HasValue()
    && scannerSpeedDetails_Duplex.Values.Match(2309).Values("/").Any()){
        var speed = scannerSpeedDetails_Speed.GetValuesWithUnits();
        result = $"Double-sided scanning at {speed.First().Value.Value()} {speed.First().Unit.Name.RegexReplace(@"\s?\(.*\)$", "")} to save time";
    }
    else if((scannerSpeedDetails_Mode.HasValue("%color%") 
    || scannerSpeedDetails_ModeBlack.HasValue("%color%") 
    || scanner_MaxDocumentScanSpeedColor.HasValue())
    && (scannerSpeedDetails_Mode.HasValue("%black&white%", "%B&W%", "greyscale") 
    || scannerSpeedDetails_ModeBlack.HasValue("%black&white%", "%B&W%", "greyscale")
    || scanner_MaxDocumentScanSpeed.HasValue())
    && scanner_ScanSpeed.HasValue()){
        var speed = scanner_ScanSpeed.GetValuesWithUnits();
        result = $"Scans up to {speed.First().Value.Value()} {speed.First().Unit.Name.RegexReplace(@"\s?\(.*\)$", "")} in both color and black/white";
    }
    else if((scannerSpeedDetails_Mode.HasValue("%color%") 
    || scannerSpeedDetails_ModeBlack.HasValue("%color%") 
    || scanner_MaxDocumentScanSpeedColor.HasValue())
    && (scannerSpeedDetails_Mode.HasValue("%black&white%", "%B&W%", "greyscale") 
    || scannerSpeedDetails_ModeBlack.HasValue("%black&white%", "%B&W%", "greyscale")
    || scanner_MaxDocumentScanSpeed.HasValue())
    && scannerSpeedDetails_Speed.HasValue()){
        var speed = scannerSpeedDetails_Speed.GetValuesWithUnits();
        result = $"Scans up to {speed.First().Value.Value()} {speed.First().Unit.Name.RegexReplace(@"\s?\(.*\)$", "")} in both color and black/white";
    }
    else if(scannerSpeedDetails_Document.HasValue("%negatives%")
    && scannerSpeedDetails_Speed.HasValue()){
        result = $"Scans up to {scannerSpeedDetails_Speed.Values.Match(2310).Values(" in ").Max().Replace("film (negatives)", "negative").Replace("film (positives)", "positive")}";
    }
    else if((scanner_ScanSpeed.HasValue() || scannerSpeedDetails_Speed.HasValue())
    && (scannerSpeedDetails_Mode.HasValue() || scannerSpeedDetails_ModeBlack.HasValue())){
        var speed = scanner_ScanSpeed.HasValue() ? scanner_ScanSpeed.GetValuesWithUnits() : scannerSpeedDetails_Speed.GetValuesWithUnits();
        result = $"Scans up to {speed.First().Value.Value()} {speed.First().Unit.Name.RegexReplace(@"\s?\(.*\)$", "")}";
    }
    else if(scanner_ScanSpeed.HasValue() || scannerSpeedDetails_Speed.HasValue()){
        var speed = scanner_ScanSpeed.HasValue() ? scanner_ScanSpeed.GetValuesWithUnits() : scannerSpeedDetails_Speed.GetValuesWithUnits();
        var color = Coalesce(
                    A[2311].Where("%color%").First().Value().Substitute("%color%", "color"),
                    A[2312].Where("%color%").First().Value().Substitute("%color%", "color"),
                    A[4616].FirstValueOrDefault().Substitute("No", "color").Substitute("Yes", ""),
                    A[2311].Where("%black&white%", "%B&W%", "greyscale").First().Value().Substitute("%black&white%", "black and white").Substitute("%B&W%", "black and white"),
                    A[2312].Where("%black&white%", "%B&W%", "greyscale").First().Value().Substitute("%black&white%", "black and white").Substitute("%B&W%", "black and white"),
                    A[4615].FirstValueOrDefault().Substitute("No", "black and white").Substitute("Yes", "")
                );
        result = $"Scans up to {speed.First().Value.Value()} {speed.First().Unit.Name.RegexReplace(@"\s?\(.*\)$", "")} in {color}";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"ScannersScanningModeSpeed⸮{result}");
    }
}

// --[FEATURE #4] 
// --Formats supported 
void ScannersFormatsSupported(){ 
    var imageFileFormatsRef = REQ.GetVariable("SP-4031").HasValue() ? REQ.GetVariable("SP-4031") :
        R("SP-4031").HasValue() ? R("SP-4031") : R("cnet_common_SP-4031");
    
    if(imageFileFormatsRef.HasValue("PDF, JPEG")){
        Add("ScannersFormatsSupported⸮Easily transfer scans into ##PDF and ##JPEG files");
    }
    else if(imageFileFormatsRef.HasValue("PDF")){
        Add("ScannersFormatsSupported⸮Easily transfer scans into ##PDF files");
    }
    else if(imageFileFormatsRef.HasValue("JPEG")){
        Add("ScannersFormatsSupported⸮Easily transfer scans into ##JPEG files");
    }
    else if(imageFileFormatsRef.HasValue()){
        Add($"ScannersFormatsSupported⸮Easily transfer scans into {imageFileFormatsRef.ToLower()} files");
    }
} 

// --[FEATURE #5] 
// --Document feeder capacity and loading type 
void ScannersDocumentFeederCapacityLoadingType(){ 
var result = ""; 
// Autoload|Manual load|Autoload, Manual load, Continuous Manual|Autoload, Manual load 
var documentFeederTypeRef = REQ.GetVariable("SP-22200").HasValue() ? REQ.GetVariable("SP-22200").Replace("<NULL>", "").Text : 
R("SP-22200").HasValue() ? R("SP-22200").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22200").HasValue() ? R("cnet_common_SP-22200").Replace("<NULL>", "").Text : ""; 
var documentFeederCapacityRef = REQ.GetVariable("SP-350275").HasValue() ? REQ.GetVariable("SP-350275").Replace("<NULL>", "").Text : 
R("SP-350275").HasValue() ? R("SP-350275").Replace("<NULL>", "").Text : 
R("cnet_common_SP-350275").HasValue() ? R("cnet_common_SP-350275").Replace("<NULL>", "").Text : ""; 

if(documentFeederTypeRef.Contains("Autoload, Manual load") 
&& !string.IsNullOrEmpty(documentFeederCapacityRef)){ 
result = $"Auto or manual document feeding feature lets you load up to {documentFeederCapacityRef}"; 
} 
else if(documentFeederTypeRef.Equals("Autoload") 
&& !string.IsNullOrEmpty(documentFeederCapacityRef)){ 
result = $"Auto-loading feeder with a capacity of up to {documentFeederCapacityRef}"; 
} 
else if(documentFeederTypeRef.Equals("Manual load") 
&& !string.IsNullOrEmpty(documentFeederCapacityRef)){ 
result = $"Manual-loading feeder with a capacity of up to {documentFeederCapacityRef}"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersDocumentFeederCapacityLoadingType⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Interface 
void ScannersInterface(){ 
var result = ""; 
// 1 x USB 2.0, 1 x Gigabit Ethernet, 1 x USB 2.0 Host|USB 2.0 x 1|USB|1 x USB 3.1 Gen 1|Up to 9 Mp|1 x Gigabit Ethernet|4 x USB|1 x USB 2.0, 1 x Ethernet Ports|2 x USB Port|1 x USB 3.0 Port|1 x USB Port|1 x USB 3.0, 1 x Gigabit Ethernet 
var dataPortsRef = REQ.GetVariable("SP-4015").HasValue() ? REQ.GetVariable("SP-4015").Replace("<NULL>", "").Text : 
R("SP-4015").HasValue() ? R("SP-4015").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4015").HasValue() ? R("cnet_common_SP-4015").Replace("<NULL>", "").Text : ""; 

if(dataPortsRef.Contains("USB 2.0")){ 
result = dataPortsRef.Contains("USB 3.1") ? "It has USB 2.0/USB 3.1 interface for reliable connectivity" : 
dataPortsRef.Contains("USB 3.0") ? "It has USB 2.0/USB 3.0 interface for reliable connectivity" : 
"Practical and convenient connectivity with the USB 2.0 interface"; 
} 
else if(dataPortsRef.Contains("USB 3.0")){ 
result = "High-speed interface allow you to save directly to a computer or thumb drive"; 
} 
else if(dataPortsRef.Contains("USB 3.")){ 
result = dataPortsRef.Contains("USB 3.0") ? "High-speed USB 3.0 interface allow you to save directly to a computer or thumb drive" : 
dataPortsRef.Contains("USB 3.1") ? "High-speed USB 3.1 interface allow you to save directly to a computer or thumb drive" : ""; 
} 
else if(dataPortsRef.Contains("USB")){ 
result = "Practical and convenient connectivity with the USB interface"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"temp⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Compatability 
void ScannersCompatability(){ 
var result = ""; 
var OSCompatibilityRef = REQ.GetVariable("SP-23547").HasValue() ? REQ.GetVariable("SP-23547").Replace("<NULL>", "").Text : 
R("SP-23547").HasValue() ? R("SP-23547").Replace("<NULL>", "").Text : 
R("cnet_common_SP-23547").HasValue() ? R("cnet_common_SP-23547").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(OSCompatibilityRef)){ 
result = $"Use with {OSCompatibilityRef}"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersCompatability⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Data ports 
void ScannersDataPorts(){ 
    var scanner_InterfaceType = A[7086];
    
    if(A[7086].HasValue()){
        Add($"ScannersDataPorts⸮Connectivity: {scanner_InterfaceType.Values.Match(7089, 7086).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2 x $1").Replace("##", "##1")).FlattenWithAnd()}");
    }
} 

// --[FEATURE #9] 
// --Refurbished? 
void ScannersRefurbished(){ 
var result = ""; 
// No|Yes 
var refurbishedConnectivityRef = REQ.GetVariable("SP-22197").HasValue() ? REQ.GetVariable("SP-22197").Replace("<NULL>", "").Text : 
R("SP-22197").HasValue() ? R("SP-22197").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22197").HasValue() ? R("cnet_common_SP-22197").Replace("<NULL>", "").Text : ""; 
var miscellaneous_Refurbished = A[5312]; // Miscellaneous - Refurbished 

if(!string.IsNullOrEmpty(refurbishedConnectivityRef)){ 
result = "Refurbished"; 
} 
else if(miscellaneous_Refurbished.HasValue()){ 
result = "Refurbished"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersRefurbished⸮{result}"); 
} 
} 

// --[FEATURE #10] - All 
// --Certifications and Standards 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void ScannersOCR(){ 
var result = ""; 
var software_SoftwareIncluded = A[2561]; // Flash Memory - Installed Size 
var scanner_AdditionalFunctions = A[3294]; // Scanner - Scanner Features 

if(software_SoftwareIncluded.HasValue("%OCR%") 
|| scanner_AdditionalFunctions.HasValue("%OCR%")){ 
result = "Includes OCR and document management suite for PDF and PDF/A files to create editable text and searchable PDFs for better document management"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersOCR⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void ScannersPoweredByUSBPort(){ 
var result = ""; 
var scanner_ScannerFeatures = A[3824]; // Scanner - Scanner Features 

if(scanner_ScannerFeatures.HasValue("powered by USB port")){ 
result = "USB-powered scanner eliminates the need for an electrical outlet or adapter"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersPoweredByUSBPort⸮{result}"); 
} 
} 

// --[FEATURE #13] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void ScannersDigitalICETechnology(){ 
var result = ""; 
var scanner_ScannerFeatures = A[3824]; // Scanner - Scanner Features 

if(scanner_ScannerFeatures.HasValue("Digital ICE Technology")){ 
result = "Achieve robust photo restorations: Digital ICE technology removes the appearance of dust and scratches on film"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersDigitalICETechnology⸮{result}"); 
} 
} 

// --[FEATURE #14] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void ScannersEasyPhotoFix(){ 
var result = ""; 
var software_SoftwareIncluded = A[2561]; // Flash Memory - Installed Size 

if(software_SoftwareIncluded.HasValue("%Easy Photo Fix%")){ 
result = "Bring faded photos back to life - Easy Photo Fix for one-touch photo restorations"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersEasyPhotoFix⸮{result}"); 
} 
} 

// --[FEATURE #15] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void ScannersFlashMemory(){ 
var result = ""; 
var flashMemory_InstalledSize = A[411]; // Flash Memory - Installed Size 

if(flashMemory_InstalledSize.HasValue()){ 
result = $"Equipped with a flash memory of {flashMemory_InstalledSize.FirstValue()} {flashMemory_InstalledSize.Units.First().Name} for efficient performance"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersFlashMemory⸮{result}"); 
} 
} 

// --[FEATURE #16] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void ScannersEasyViewing(){ 
var result = ""; 
var scanner_ScannerFeatures = A[3824]; // Scanner - Scanner Features 

if(scanner_ScannerFeatures.HasValue("%screen%", "%display%")){ 
result = $"{scanner_ScannerFeatures.Where("%screen%", "%display%").First().Value().ToUpperFirstChar()} for easy viewing"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ScannersEasyViewing⸮{result}"); 
} 
} 

// --[FEATURE #17] - All 
// --Warranty 

//5435443731158948 "Scanners END" "Serhii.O" §§ 

//§§1374511089166024 "Glue & Glue Sticks BEGIN" "Serhii.O" 

GlueGlueSticksTypeUse(); 
GlueGlueSticksAdhesiveType(); 
GlueGlueSticksCapacity(); 
// Glue True color - All 
GlueGlueSticksNonToxic(); 
// Pack Size - All 
GlueGlueSticksOdorlessAcidPVC(); 
GlueGlueSticksAntiClogCap(); 
GlueGlueSticksOutdoorUse(); 
GlueGlueSticksWashable(); 
GlueGlueSticksResistantBottle(); 
GlueGlueSticksPrecisionApplicator(); 
GlueGlueSticksPhotoSafe(); 

// --[FEATURE #1] 
// --Glue & glue stick type & use 
void GlueGlueSticksTypeUse(){ 
var result = ""; 
// Adhesive Putty|Glue|Spray Adhesive|Glue Gun|Glue Sticks|Super Glue|Glue Pens|Glue Tape 
var glueGlueSticksTypeRef = R("SP-4580").HasValue() ? R("SP-4580").Text : 
R("cnet_common_SP-4580").HasValue() ? R("cnet_common_SP-4580").Text : ""; 
var general_ApplicationAreas = A[6182]; // General - Application Areas 

if(!string.IsNullOrEmpty(glueGlueSticksTypeRef)){ 
switch(glueGlueSticksTypeRef){ 
case var Type when Type.Equals("Glue Sticks") 
&& general_ApplicationAreas.HasValue(): 
result = $"{Type} are perfect for use on {general_ApplicationAreas.Values.Take(5).Select(s => s.Value()).FlattenWithAnd().RegexReplace(@"((\S+[,]?\s?){4})\s(and\s{1}\S+)", "$1, and other materials")}"; 
break; 
case var Type when !string.IsNullOrEmpty(Type) 
&& general_ApplicationAreas.HasValue(): 
result = $"{Type} is perfect for use on {general_ApplicationAreas.Values.Take(5).Select(s => s.Value()).FlattenWithAnd().RegexReplace(@"((\S+[,]?\s?){4})\s(and\s{1}\S+)", "$1, and other materials")}"; 
break; 
case "Adhesive Putty": 
result = "Adhesive putty is perfect for hanging decorations and posters"; 
break; 
case "Glue Sticks": 
result = "Glue stick provides permanent bonding once dried"; 
break; 
case "Glue Gun": 
result = "Create beautiful crafts, durable origami pieces, artificial bonsai structures, and more with Glue Gun"; 
break; 
case "Spray Adhesive": 
result = "Glue thick surfaces firmly with the Spray Adhesive"; 
break; 
case "Super Glue": 
result = "Carry out quick and durable repairs with the Super Glue"; 
break; 
case "Glue Pens": 
result = "No tools or instruments are needed to securely glue surfaces with glue pens"; 
break; 
case "Glue Tape": 
result = "Glue tape is good for clippings, memos and paper crafts"; 
break; 
case "Glue": 
result = "Glue holds items securely in place"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksTypeUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Adhesive type 
void GlueGlueSticksAdhesiveType(){ 
var result = ""; 
// Removable|Permanent|Magnetic|Washable 
var adhesiveTypeRef = R("SP-4580").HasValue() ? R("SP-4580").Text : 
R("cnet_common_SP-4580").HasValue() ? R("cnet_common_SP-4580").Text : ""; 

if(!string.IsNullOrEmpty(adhesiveTypeRef)){ 
switch(adhesiveTypeRef){ 
case "Permanent": 
// Permanent glue stick offer a simple method for creating a permanent hold 
result = "Advanced adhesive strength for safely and permanently holding surfaces together"; 
break; 
case "Removable": 
result = "Removes cleanly without damaging"; 
break; 
// https://www.staples.com/staplesreg-washable-purple-glue-sticks-18-pack/product_2706590 
case "Washable": 
result = "Washable glue sticks"; 
break; 
case "Magnetic": 
result = "Magnetic glue sticks"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksAdhesiveType⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Capacity (oz.) 
void GlueGlueSticksCapacity(){ 
var result = ""; 
var capacity = R("SP-21132").HasValue() ? R("SP-21132").Text : 
R("cnet_common_SP-21132").HasValue() ? R("cnet_common_SP-21132").Text : ""; 

if (!string.IsNullOrEmpty(capacity)) { 
result = $"Comes in capacity of {capacity} oz."; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksCapacity⸮{result}"); 
} 
} 

// --[FEATURE #4] - All 
// --Glue True color 

// --[FEATURE #5] 
// --Non-toxic 
void GlueGlueSticksNonToxic(){ 
var result = ""; 
var General_Features = A[6189]; // General - Features 

if(General_Features.HasValue("%toxic%")){ 
result = "Featuring a non-toxic nature"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksNonToxic⸮{result}"); 
} 
} 

// --[FEATURE #6] - All 
// --Pack Size (If more than 1) 

// --[FEATURE #7] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void GlueGlueSticksOdorlessAcidPVC(){ 
var result = ""; 
var General_Features = A[6189]; // General - Features 

if(General_Features.HasValue("odorless","acid-free","PVC-free")){ 
result = $"{General_Features.Where("odorless","acid-free","PVC-free").FlattenWithAnd().ToUpperFirstChar()} formula is safe for use"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksOdorlessAcidPVC⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void GlueGlueSticksAntiClogCap(){ 
var result = ""; 
var General_Features = A[6189]; // General - Features 

if(General_Features.HasValue("anti-clog cap")){ 
result = "Added an anti-clog cap to ensure an air-tight seal for maximum reusability"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksAntiClogCap⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void GlueGlueSticksOutdoorUse(){ 
var result = ""; 
var General_Features = A[6189]; // General - Features 

if(General_Features.HasValue("outdoor use")){ 
result = "Great for outdoor use"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksOutdoorUse⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void GlueGlueSticksWashable(){ 
var result = ""; 
var General_Features = A[6189]; // General - Features 

if(General_Features.HasValue("washable")){ 
result = "Washable glue for use on porous and semi-porous surfaces"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksWashable⸮{result}"); 
} 
} 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void GlueGlueSticksResistantBottle(){ 
var result = ""; 
var General_Features = A[6189]; // General - Features 

if(General_Features.HasValue("clog-resistant bottle")){ 
result = "Features a clog-resistant bottle to ensure use any time it is needed"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksResistantBottle⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void GlueGlueSticksPrecisionApplicator(){ 
var result = ""; 
var General_Features = A[6189]; // General - Features 

if(General_Features.HasValue("precision applicator")){ 
result = "Precision applicator on glue bottle ensures accuracy"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksPrecisionApplicator⸮{result}"); 
} 
} 

// --[FEATURE #13] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void GlueGlueSticksPhotoSafe(){ 
var result = ""; 
var General_Features = A[6189]; // General - Features 

if(General_Features.HasValue("photo safe") 
&& General_Features.HasValue("odorless", "acid-free", "PVC-free")){ 
result = "Photo-safe formula to protect important materials"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"GlueGlueSticksPhotoSafe⸮{result}"); 
} 
} 

//1374511089166024 "Glue & Glue Sticks END" "Serhii.O" §§ 

//§§1374510541215292 "Hooks & Hanging Strips BEGIN" "Serhii.O" 

HookStripTypeUse(); 
HookStripCapacity(); 
HookStripSize(); 
// Material of item & True color - All 
// Hook & strip Pack Size (If more than 1) - All 
HookStripVarietyOfSurfaces(); 
HookStripAppliesEasily(); 
HookStripWeatherproofResistant(); 
HookStripadhesiveBonds(); 

// --[FEATURE #1] 
// --Hook/strip type & use 
void HookStripTypeUse(){ 
var result = ""; 
// Large|Jumbo|Assorted|Mini|Small|Medium 
var hookStripSizeRef = REQ.GetVariable("SP-20502").HasValue() ? REQ.GetVariable("SP-20502").Replace("<NULL>", "").Text : 
R("SP-20502").HasValue() ? R("SP-20502").Replace("<NULL>", "").Text : 
R("cnet_common_SP-20502").HasValue() ? R("cnet_common_SP-20502").Replace("<NULL>", "").Text : ""; 
// Bath Hook|Oval Hook|Outdoor Terrace Hook|Picture Hanging Strip|Designer Hook|Cord Clip|Picture Hanger|Key Rail|Refill Strip|Jumbo Hook|Poster Strip|Metal Hook|Suction Cup Hook|Spring Clip|Micro Hook|Broom Gripper|Window Hook|Utility Hook|Metallic Coated Hook|Outdoor Light Clips|Double Hook|Cord Organizer|Wire Hook|Mini Hook 
var hookStripTypeRef = REQ.GetVariable("SP-20503").HasValue() ? REQ.GetVariable("SP-20503").Replace("<NULL>", "").Text : 
R("SP-20503").HasValue() ? R("SP-20503").Replace("<NULL>", "").Text : 
R("cnet_common_SP-20503").HasValue() ? R("cnet_common_SP-20503").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(hookStripTypeRef)){ 
switch(hookStripTypeRef){ 
case var Type when Type.Contains("Hook") 
&& hookStripSizeRef.Equals("Medium"): 
result = "Medium hook is a perfect choice for hanging belts, seasonal decor, cleaning tools and more"; 
break; 
case "Cord Organizer": 
result = "Cord organizers are perfect for work and home offices, as well as entertainment areas"; 
break; 
case var Type when Type.Contains("Picture Hanging Strip") 
|| Type.Contains("Poster Strip"): 
result = $"{Type.ToLower().ToUpperFirstChar()} for mounting photos, posters, artwork, or papers"; 
break; 
case "Broom Gripper": 
result = "Broom gripper is great for neatly storing your brooms when not in use"; 
break; 
case "Double Hook": 
result = "Double hooks keep both your floor and your hanging items cleaner and more organized"; 
break; 
case var Type when !Type.Contains("Outdoor Light Clips") 
|| !Type.Contains("Extreme fasteners"): 
result = $"{Type.ToLower().ToUpperFirstChar()} makes decorating quick and easy"; 
break; 
case var Type when Type.Contains("Outdoor Light Clips") 
|| Type.Contains("Extreme fasteners"): 
result = $"{Type.ToLower().ToUpperFirstChar()} make decorating quick and easy"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HookStripTypeUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Hook & strip capacity in pounds 
void HookStripCapacity(){ 
var result = ""; 
// 12|9|3|2|7.5|16|0.5|4|5|1|6 
var hookStripCapacityRef = REQ.GetVariable("SP-350489").HasValue() ? REQ.GetVariable("SP-350489").Replace("<NULL>", "").Text : 
R("SP-350489").HasValue() ? R("SP-350489").Replace("<NULL>", "").Text : 
R("cnet_common_SP-350489").HasValue() ? R("cnet_common_SP-350489").Replace("<NULL>", "").Text : ""; 

if(hookStripCapacityRef.Equals("1")){ 
result = $"Capable of holding up to {hookStripCapacityRef} lb. of weight"; 
} 
else if(Coalesce(hookStripCapacityRef).In("4", "0.5", "5", "6", "2", "3")){ 
result = $"Capable of holding up to {hookStripCapacityRef} lbs. of weight"; 
} 
else if(!string.IsNullOrEmpty(hookStripCapacityRef)){ 
result = $"Hold a surprising amount of weight up to {hookStripCapacityRef} pounds"; 
} 
else if(REQ.GetVariable("CNET_SD").In("%weight capacity of 3 pounds per pair%")){ 
result = "Capable of holding up to 3 lbs. of weight per pair"; 
} 
else if(SPEC["ES"].GetLines().Select(l => l.Body).Flatten(" ").In("%holds up to 4.54 kg%")){ 
result = "Capable of holding up to 10 lbs. of weight"; 
} 
else if(DC.KSP.GetLines().Flatten(" ").In("%holds up to 4.54 kg%")){ 
result = @"Capable of holding up to 1 lb. per 4"" of tape"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HookStripCapacity⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Hook & strip size 
void HookStripSize(){ 
var result = ""; 
// Large|Jumbo|Assorted|Mini|Small|Medium 
var hookStripSizeRef = REQ.GetVariable("SP-20502").HasValue() ? REQ.GetVariable("SP-20502").Replace("<NULL>", "").Text : 
R("SP-20502").HasValue() ? R("SP-20502").Replace("<NULL>", "").Text : 
R("cnet_common_SP-20502").HasValue() ? R("cnet_common_SP-20502").Replace("<NULL>", "").Text : ""; 
// Dimensions & Weight - Width 
var dimensionsWeight_Width = REQ.GetVariable("SP-21044").HasValue() ? REQ.GetVariable("SP-21044").Replace("<NULL>", "").Text : 
R("SP-21044").HasValue() ? R("SP-21044").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21044").HasValue() ? R("cnet_common_SP-21044").Replace("<NULL>", "").Text : ""; 
// Dimensions & Weight - Depth 
var dimensionsWeight_Depth = REQ.GetVariable("SP-20657").HasValue() ? REQ.GetVariable("SP-20657").Replace("<NULL>", "").Text : 
R("SP-20657").HasValue() ? R("SP-20657").Replace("<NULL>", "").Text : 
R("cnet_common_SP-20657").HasValue() ? R("cnet_common_SP-20657").Replace("<NULL>", "").Text : ""; 
// Dimensions & Weight - Depth 
var dimensionsWeight_Height = REQ.GetVariable("SP-20654").HasValue() ? REQ.GetVariable("SP-20654").Replace("<NULL>", "").Text : 
R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(hookStripSizeRef)){ 
switch(hookStripSizeRef){ 
case "Jumbo": 
result = "Extra large hooks are ideal for large tools and bikes"; 
break; 
case "Assorted": 
result = "Product comes in ##assorted sizes"; 
break; 
default: 
result = $"Product comes in {hookStripSizeRef.ToLower()} size"; 
break; 
} 
} 
else if(!string.IsNullOrEmpty(dimensionsWeight_Height) 
&& !string.IsNullOrEmpty(dimensionsWeight_Depth) 
&& !string.IsNullOrEmpty(dimensionsWeight_Width)){ 
result = $@"Size: {dimensionsWeight_Height}""H x {dimensionsWeight_Depth}""D x {dimensionsWeight_Width}""W"; 
} 
else if(!string.IsNullOrEmpty(dimensionsWeight_Depth) 
&& !string.IsNullOrEmpty(dimensionsWeight_Width)){ 
result = $@"Size: {dimensionsWeight_Depth}""D x {dimensionsWeight_Width}""W"; 
} 
else if(!string.IsNullOrEmpty(dimensionsWeight_Height) 
&& !string.IsNullOrEmpty(dimensionsWeight_Width)){ 
result = $@"Size: {dimensionsWeight_Height}""H x {dimensionsWeight_Width}""W"; 
} 
else if(!string.IsNullOrEmpty(dimensionsWeight_Height) 
&& !string.IsNullOrEmpty(dimensionsWeight_Depth)){ 
result = $@"Size: {dimensionsWeight_Height}""H x {dimensionsWeight_Depth}""D"; 
} 
else if(!string.IsNullOrEmpty(dimensionsWeight_Height)){ 
result = $@"Size: {dimensionsWeight_Height}""H"; 
} 
else if(!string.IsNullOrEmpty(dimensionsWeight_Width)){ 
result = $@"Size: {dimensionsWeight_Width}""W"; 
} 
else if(!string.IsNullOrEmpty(dimensionsWeight_Depth)){ 
result = $@"Size: {dimensionsWeight_Depth}""D"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HookStripSize⸮{result}"); 
} 
} 

// --[FEATURE #4] - All 
// --Material of item & True color 

// --[FEATURE #5] - All 
// --Hook & strip Pack Size (If more than 1) 

// --[FEATURE #6] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HookStripVarietyOfSurfaces(){ 
var result = ""; 
var A6182 = A[6182]; 

if(A6182.HasValue() 
&& A6182.Values.Count >= 2){ 
result = $"Works on a variety of surfaces including {A6182.Values.Select(s => s.Value()).FlattenWithAnd()}"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HookStripVarietyOfSurfaces⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HookStripAppliesEasily(){ 
var result = ""; 
var A6189 = A[6189]; 

if(A6189.HasValue("removable without trace", "residue-free removal")){ 
result = "Applies easily to clean surfaces, removes without leaving residue"; 
} 
else if(REQ.GetVariable("CNET_SD").In("%are entirely removable%")){ 
result = "Easy to apply and remove"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HookStripAppliesEasily⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HookStripWeatherproofResistant(){ 
var result = ""; 
var A6189 = A[6189]; 

if(A6189.HasValue("UV-resistant") 
&& A6189.HasValue("weatherproof")){ 
result = "Provides resistance against water and UV rays"; 
} 
else if(A6189.HasValue("UV-resistant")){ 
result = A[6189].Where("UV-resistant").First().Value().Replace("UV-resistant", "UV rays"); 
} 
else if(A6189.HasValue("weatherproof")){ 
result = A[6189].Where("weatherproof").First().Value().Replace("weatherproof", "water").ToUpperFirstChar(); 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HookStripWeatherproofResistant⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HookStripadhesiveBonds(){ 
var result = ""; 
var A6189 = A[6189]; 

if(A6189.HasValue("double-sided")){ 
result = "Double-sided adhesive bonds on contact"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"HookStripadhesiveBonds⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 

//1374510541215292 "Hooks & Hanging Strips END" "Serhii.O" §§ 

//§§5884389110931140978 "Laptop Bags & Cases BEGIN" "Serhii.O" 

LaptopBagCasestyleUse(); 
// True color & Material - All 
LaptopBagsCasesStrapHandleConfiguration(); 
// Dimensions - All 
LaptopBagsCasesInnerDimensions(); 
LaptopBagsCasesBottleHolder(); 
LaptopBagsCasesDetailCompartments(); 
LaptopBagsCasesWheeled(); 
// Pack Size - All 
LaptopBagsCasesCheckPointFriendly(); 
LaptopBagsCasesWaterResistant(); 
LaptopBagsCasesAntiScratch(); 
// Warranty - All 

// --[FEATURE #1] 
// --Laptop Bag/Case style & Use 
void LaptopBagCasestyleUse(){ 
var result = ""; 
var General_ProductType = A[371]; // General - Product Type 
var CarryingCase_TelescopicHandleWheels = A[4688]; // Carrying Case - Telescopic Handle / Wheels 

if(General_ProductType.HasValue("%notebook%case%")){ 
result = CarryingCase_TelescopicHandleWheels.HasValue("yes") ? "This rolling case allows you to safely transport your notebook with you when you're traveling" 
: "Notebook carrying case keeps your laptop and other work essentials neatly packed"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"LaptopBagCasestyleUse⸮{result}"); 
} 
} 

// --[FEATURE #2] - All 
// --True color & Material 

// --[FEATURE #3] 
// --Strap/Handle configuration (i.e. Adjustable shoulder straps, reinforced handles, etc.) 
void LaptopBagsCasesStrapHandleConfiguration(){ 
var result = ""; 
var Miscellaneous_ProductMaterial = A[372]; // Miscellaneous - Product Material 
var CarryingCase_CarryingStrap = A[859]; // Carrying Case - Carrying Strap 
var CarryingCase_Features = A[860]; // Carrying Case - Features 

if(Coalesce(CarryingCase_CarryingStrap, CarryingCase_Features).HasValue()){ 
switch(Coalesce(CarryingCase_CarryingStrap, CarryingCase_Features)){ 
case var a when a.HasValue("%adjustable shoulder strap%"): 
result = "Comfortable handle and adjustable shoulder strap for easy portability"; 
break; 
case var a when a.HasValue("%reinforced ergonomic grab handle%"): 
result = "Adjustable padded reinforced shoulder strap for extra shoulder support"; 
break; 
case var a when a.HasValue("%hand%") 
&& a.HasValue("%shoulder strap%"): 
result = "Compact and durable with padded shoulder strap and top handles"; 
break; 
case var a when a.HasValue("%shoulder%strap%") 
&& a.HasValue("%padded%handle%") 
&& Miscellaneous_ProductMaterial.HasValue("%neoprene%"): 
result = "Neoprene padded handle and ergonomic shoulder strap makes it easy to carry"; 
break; 
case var a when a.HasValue("%padded%handle%"): 
result = "Padded carry handles for comfortable transportation"; 
break; 
case var a when a.HasValue("%removable%strap%") 
&& a.HasValue("%top%handle%"): 
result = "Traditional top handle and removable carrying strap for comfort and convenience"; 
break; 
case var a when a.HasValue("%removable shoulder strap%") 
&& a.HasValue("%carry%%handle"): 
result = "Removable, adjustable shoulder strap and cushioned carry handles provide superior comfort and versatility"; 
break; 
case var a when a.HasValue("%ergonomic trolley handle%") 
&& a.HasValue("%padded%shoulder%strap%"): 
result = "Ergonomic handles distribute weight evenly, and padded shoulder strap provides comfort"; 
break; 
case var a when a.HasValue("%shoulder%carrying%strap%"): 
result = "Articulating shoulder straps make it easy to carry"; 
break; 
case var a when a.HasValue("%ergonomic%handle%") 
&& a.HasValue("%removable%shoulder%"): 
result = "Ergonomic handle comes with a removable shoulder strap that provides a comfortable way to carry"; 
break; 
case var a when a.HasValue("%handle%") 
&& a.HasValue("%removable%shoulder%strap%"): 
result = "Removable shoulder strap and padded carry handle make this case easy to transport"; 
break; 
case var a when a.HasValue("%top%carry%handle%"): 
result = "Top carry handle to easily lift the case"; 
break; 
case var a when a.HasValue("%retractable%handle%"): 
result = "Retractable handle for easy transportation"; 
break; 
case var a when a.HasValue("%hand%grip%"): 
result = "Features hand grip for user convenience"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"LaptopBagsCasesStrapHandleConfiguration⸮{result}"); 
} 
} 

// --[FEATURE #4] - All 
// --Dimensions (in Inches): H x W x D; If expandable, include range. 

// --[FEATURE #5] 
// --Detail compartments and their individual measurements 
void LaptopBagsCasesInnerDimensions(){ 
var result = ""; 
var General_DisplayScreenSizeCompatibility = A[2215]; // General - Display Screen Size Compatibility 
var NotebookCompatibilityDimensions_NotebookCompatibility = A[4430]; // Notebook Compatibility Dimensions - Notebook 

if(Coalesce(NotebookCompatibilityDimensions_NotebookCompatibility, General_DisplayScreenSizeCompatibility).HasValue()){ 
if(Coalesce(NotebookCompatibilityDimensions_NotebookCompatibility, General_DisplayScreenSizeCompatibility).Units.First().Name.In("in")){ 
switch(Coalesce(NotebookCompatibilityDimensions_NotebookCompatibility, General_DisplayScreenSizeCompatibility).FirstValue()){ 
case var size when size < 13: 
result = $@"Universal sleeve fits {size}"" wide tablets and laptops"; 
break; 
case var size when size >= 13 
&& size < 15: 
result = $@"Padded sleeve provides quality protection for laptops up to {size}"""; 
break; 
case var size when size >= 15 
&& size < 17: 
result = $@"Padded laptop pocket fits up to {size}"" laptop for additional safety"; 
break; 
case var size when size >= 17 
&& size < 19: 
result = $@"Padded compartment holds up to {size}"" laptops to accommodate full-size screens"; 
break; 
case var size when size >= 19: 
result = $@"Fits most laptops up to {size}"""; 
break; 
} 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"LaptopBagsCasesInnerDimensions⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Detail compartments and their individual measurements 
void LaptopBagsCasesBottleHolder(){ 
var result = ""; 
var CarryingCase_AdditionalCompartments = A[2527]; // Carrying Case - Additional Compartments 

if(CarryingCase_AdditionalCompartments.HasValue("bottle")){ 
result = "Easily accessible water bottle holder placed safely away from sensitive electronics and important files"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"LaptopBagsCasesBottleHolder⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Detail compartments and their individual measurements 
void LaptopBagsCasesDetailCompartments(){ 
var result = ""; 
var CarryingCase_AdditionalCompartments = A[2527]; // Carrying Case - Additional Compartments 

if(CarryingCase_AdditionalCompartments.HasValue()){ 
switch(CarryingCase_AdditionalCompartments){ 
case var a when a.HasValue("%adapter%") 
&& a.HasValue("%CD%") 
&& CarryingCase_AdditionalCompartments.Values.Count >= 4: 
result = "Full-size pocket for AC adapter, files, CDs and other accessories"; 
break; 
case var a when a.HasValue("%phone%") 
&& a.HasValue("%glass%") 
&& CarryingCase_AdditionalCompartments.Values.Count >= 3: 
result = "Exterior pocket features organizer pockets for phone, glasses, and all school/business needs"; 
break; 
case var a when a.HasValue("%pen%") 
&& a.HasValue("%card%"): 
result = "Organizer pocket neatly holds your accessories and smaller items like pens, notepads, and business cards"; 
break; 
case var a when a.HasValue() 
&& CarryingCase_AdditionalCompartments.Values.Count >= 3: 
result = "Organizer panel in pocket with dedicated storage for cards, pens and other accessories"; 
break; 
case var a when a.HasValue("organizer"): 
result = "Organizer panel in pocket with dedicated storage for cards, pens and other accessories"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"LaptopBagsCasesDetailCompartments⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --If wheeled, state here 
void LaptopBagsCasesWheeled(){ 
var result = ""; 
var CarryingCase_WheelsQty = A[7246]; // Carrying Case - Wheels Qty 

if(CarryingCase_WheelsQty.HasValue()){ 
switch(CarryingCase_WheelsQty){ 
case var a when a.FirstValue().In("8"): 
result = "An 8-wheel spinner/rolling system makes maneuverability and travel easy"; 
break; 
case var a when a.FirstValue().In("4"): 
result = "Four multi-directional spinner wheels allow upright rolling in any direction for easy mobility"; 
break; 
case var a when a.HasValue(): 
result = "In-line skate wheels let you roll a fully loaded case through airports or office buildings"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"LaptopBagsCasesWheeled⸮{result}"); 
} 
} 

// --[FEATURE #9] - All 
// --Pack Size (If more than 1) 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void LaptopBagsCasesCheckPointFriendly(){ 
var result = ""; 
var CarryingCase_CheckpointFriendly = A[8054]; // Carrying Case - Checkpoint Friendly 

if(CarryingCase_CheckpointFriendly.HasValue("yes")){ 
result = "Checkpoint friendly: you do not have to take your laptop out of the bag when going through security at the airport"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"LaptopBagsCasesCheckPointFriendly⸮{result}"); 
} 
} 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void LaptopBagsCasesWaterResistant(){ 
var result = ""; 
var CarryingCase_Features = A[860]; // Carrying Case - Features 

if(CarryingCase_Features.HasValue("%water%proof%", "%water%resistant%")){ 
result = "Water-resistant material keeps belongings dry in wet weathe"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"LaptopBagsCasesWaterResistant⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void LaptopBagsCasesAntiScratch(){ 
var result = ""; 
var CarryingCase_Features = A[860]; // Carrying Case - Features 

if(CarryingCase_Features.HasValue("%scratch%proof%", "%anti%scratch%")){ 
result = "Scratch-resistant finish provides comfortable grip and resists fingerprints and smudges"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"LaptopBagsCasesAntiScratch⸮{result}"); 
} 
} 

// --[FEATURE #13] - All 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
// --Warranty 

//5884389110931140978 "Laptop Bags & Cases END" "Serhii.O" §§ 

//§§5829362510113210844 "Headphones BEGIN" "Serhii.O" 

HeadphonesTypeUse(); 
HeadphonesConnectivityOperationalRange(); 
HeadphonesInterfaceBluetoothCompatibility(); 
HeadphonesConstruction(); 
HeadphonesBatteryType(); 
HeadphonesRechargeBattery(); 
HeadphonesPowerSourceRechargeTimeBatteryLife(); 
HeadphonesBuiltRemote(); 
HeadphonesBuiltMicAndControls(); 
HeadphonesTechnologyFeature(); 
HeadphonesEarParts(); 
HeadphonesPushToTalk(); 
HeadphonesBuiltInMicrophone(); 
// Warranty - All 

// --[FEATURE #1] 
// --Headphone type & Use 
void HeadphonesTypeUse(){ 
var result = ""; 
var Brand = SKU.Brand; 
var ModelName = SKU.ModelName; 
// Over-Ear|On-Ear|Earbuds 
var HeadphoneTypeRef = R("SP-18729").HasValue() ? R("SP-18729").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18729").HasValue() ? R("cnet_common_SP-18729").Replace("<NULL>", "").Text : ""; 
var AudioOutput_HeadphonesMount = A[7426]; // Audio Output - Headphones Mount 
var Miscellaneous_EarpadMaterial = A[8468]; // Miscellaneous - Earpad Material 

if(Brand.HasValue() 
&& Brand.In("Apple") 
&& ModelName.HasValue() 
&& ModelName.In("EarPods") 
&& HeadphoneTypeRef.Equals("Earbuds")){ 
result = "The design of the EarPods is defined by the geometry of the ear"; 
} 
else if(HeadphoneTypeRef.Equals("On-Ear")){ 
result = "On-ear design with high-comfort earpads"; 
} 
else if(HeadphoneTypeRef.Equals("Over-Ear")){ 
result = "Over-the-ear design makes these headphones comfortable"; 
} 
else if(HeadphoneTypeRef.Equals("Earbuds")){ 
result = "Comfortable-wearing earbuds for all-day use"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesTypeUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Headphone connectivity & Operational range 
void HeadphonesConnectivityOperationalRange(){ 
var result = ""; 
// Wired/Wireless|Wireless|Wired 
var HeadphoneConnectivityRef = R("SP-22214").HasValue() ? R("SP-22214").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22214").HasValue() ? R("cnet_common_SP-22214").Replace("<NULL>", "").Text : ""; 

if(HeadphoneConnectivityRef.Equals("Wired")){ 
result = $"Wired connectivity offers high-fidelity sound"; 
} 
else if(HeadphoneConnectivityRef.Contains("Wireless")){ 
result = "Completely wireless, so you'll enjoy freedom of movement"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesConnectivityOperationalRange⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Interface & Bluetooth compatibility; include bluetooth version (If Applicable) 
void HeadphonesInterfaceBluetoothCompatibility(){ 
    var result = "";
    var HeadphoneJackRef = R("SP-22207").HasValue() ? R("SP-22207").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22207").HasValue() ? R("cnet_common_SP-22207").Replace("<NULL>", "").Text : "";
    // 3.0 + EDR|4.0 + EDR|2.1 + EDR|2|5.0|2.1|4.2|3|4
	var BluetoothVersionRef = R("SP-22215").HasValue() ? R("SP-22215").Replace("<NULL>", "").Text :
		R("cnet_common_SP-22215").HasValue() ? R("cnet_common_SP-22215").Replace("<NULL>", "").Text : "";
    var AudioOutput_WirelessTechnology = A[1102]; // Audio Output - Wireless Technology
    var AudioSystem_MaxOperatingDistance = A[10022]; // Audio System - Max Operating Distance
    
    if(BluetoothVersionRef.Equals("4.1")
    || BluetoothVersionRef.Equals("4.2")
    || BluetoothVersionRef.Equals("5.0")
    && AudioSystem_MaxOperatingDistance.HasValue()
    && AudioSystem_MaxOperatingDistance.Units.First().NameUSM.In("ft")){
        result = $"Bluetooth ##{BluetoothVersionRef} prolongs battery life and allowing you to listen clearly up to {AudioSystem_MaxOperatingDistance.Units.First().NameUSM}' from your device";
    }
    else if(AudioOutput_WirelessTechnology.HasValue("%Bluetooth%")){
        var version = !string.IsNullOrEmpty(BluetoothVersionRef) ? $"##{BluetoothVersionRef}" : "";
        result = $"Bluetooth {version} connectivity for connecting external Bluetooth-enabled devices";
    }
    else if(AudioOutput_WirelessTechnology.HasValue("%DECT%")){
        result = "DECT frequency band transmits your voice and gives transcendent voice and sound quality";
    }
    else if(HeadphoneJackRef.Equals("3.5")){
        result = "Standard 3.5mm earphone plug works with almost every 3.5mm audio jack device";
    }
    if(!String.IsNullOrEmpty(result)){
	    Add($"HeadphonesInterfaceBluetoothCompatibility⸮{result}");
	}
} 

// --[FEATURE #4] 
// --Construction; headphone volume control, sound isolating, noise canceling (Use multiple bullets as needed) 
void HeadphonesConstruction(){ 
var result = ""; 
var Brand = SKU.Brand; 
var ModelName = SKU.ModelName; 
// Yes|No 
var NoiseCancelingRef = R("SP-22207").HasValue() ? R("SP-22207").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22207").HasValue() ? R("cnet_common_SP-22207").Replace("<NULL>", "").Text : ""; 
var SoundIsolatingRef = R("SP-22218").HasValue() ? R("SP-22218").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22218").HasValue() ? R("cnet_common_SP-22218").Replace("<NULL>", "").Text : ""; 
var AudioOutput_DiaphragmDiameter = A[879]; // Audio Output - Diaphragm Diameter 
var AudioOutput_MagnetMaterial = A[1574]; // Audio Output - Magnet Material 
var AudioOutput_HeadphonesEarPartsType = A[7425]; // Audio Output - Headphones Ear-Parts Type 
var AudioOutput_HeadphonesCupType = A[7427]; // Audio Output - Headphones Cup Type 
var AudioOutput_ActiveNoiseCanceling = A[3813]; // Audio Output - Active Noise Canceling 
var AudioOutput_ActiveNoiseCancelingTechnology = A[8574]; // Audio Output - Active Noise Canceling Technology 
var NoiseCanc = NoiseCancelingRef.Equals("Yes") ? " with noise canceling" : ""; 

if(Brand.HasValue() 
&& Brand.In("Apple", "apple") 
&& ModelName.HasValue() 
&& ModelName.In("EarPods", "earpods") 
&& AudioOutput_HeadphonesEarPartsType.HasValue("ear-bud")){ 
result = $"The speakers inside the EarPods{NoiseCanc} have been engineered to maximize sound output and minimize sound loss, giving you high-quality audio"; 
} 
else if(AudioOutput_HeadphonesCupType.HasValue("closed") 
&& AudioOutput_DiaphragmDiameter.HasValue() 
&& AudioOutput_DiaphragmDiameter.Units.First().Name.In("mm") 
&& AudioOutput_DiaphragmDiameter.FirstValue() > 39 
&& AudioOutput_MagnetMaterial.HasValue("neodymium")){ 
result = $"Includes a {AudioOutput_DiaphragmDiameter.FirstValue()}mm neodymium drivers{NoiseCanc} in a closed headphone design"; 
} 
else if(AudioOutput_HeadphonesCupType.HasValue("closed") 
&& AudioOutput_DiaphragmDiameter.HasValue() 
&& AudioOutput_DiaphragmDiameter.Units.First().Name.In("mm") 
&& AudioOutput_DiaphragmDiameter.FirstValue() < 39 
&& AudioOutput_MagnetMaterial.HasValue("neodymium")){ 
result = $"Finely tuned {AudioOutput_DiaphragmDiameter.FirstValue()}mm neodymium drivers combined with a closed-back acoustic system design{NoiseCanc}"; 
} 
else if(AudioOutput_DiaphragmDiameter.HasValue() 
&& AudioOutput_DiaphragmDiameter.Units.First().Name.In("mm")){ 
result = $"Their {AudioOutput_DiaphragmDiameter.FirstValue()}mm drivers{NoiseCanc} are tailored for comfort and render detailed powerful sound"; 
} 
else if(AudioOutput_ActiveNoiseCancelingTechnology.HasValue("ActiveShield")){ 
result = "ActiveShield active noise canceling techonolgy features two feed-backward microphones used for canceling low frequencies"; 
} 
else if(AudioOutput_ActiveNoiseCanceling.HasValue()){ 
result = "Active noise canceling so you hear only the music"; 
} 
else if(SoundIsolatingRef.Equals("Yes")){ 
result = "Provide sound isolation from background noise"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesConstruction⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Headphone battery type 
void HeadphonesBatteryType(){ 
var result = ""; 
// AA|AAA|Lithium-Polymer|Ni-MH battery|Lithium|Rechargeable|Lithium-Ion 
var HeadphoneBatteryTypeRef = R("SP-22220").HasValue() ? R("SP-22220").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22220").HasValue() ? R("cnet_common_SP-22220").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(HeadphoneBatteryTypeRef)){ 
switch(HeadphoneBatteryTypeRef){ 
case "Lithium-Polymer": 
result = "Equipped with rechargeable lithium-polymer battery"; 
break; 
case "Lithium-Ion": 
result = "Rechargeable lithium-ion battery for an efficient operation"; 
break; 
case "Ni-MH battery": 
result = "Powered by Ni-MH battery"; 
break; 
case "Lithium": 
result = "Lithium battery"; 
break; 
case "AAA": 
result = "Required AAA battery"; 
break; 
case "AA": 
result = "Required AA battery"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesBatteryType⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Recharge Battery 
void HeadphonesRechargeBattery(){ 
var result = ""; 
// AA|AAA|Lithium-Polymer|Ni-MH battery|Lithium|Rechargeable|Lithium-Ion 
var HeadphoneBatteryTypeRef = R("SP-22220").HasValue() ? R("SP-22220").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22220").HasValue() ? R("cnet_common_SP-22220").Replace("<NULL>", "").Text : ""; 

if(HeadphoneBatteryTypeRef.Equals("Rechargeable")){ 
result = "Rechargeable battery for an efficient operation"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesRechargeBattery⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Power source, recharge time & battery life (for wireless devices) 
void HeadphonesPowerSourceRechargeTimeBatteryLife(){ 
    var result = "";
    var Battery_Technology = A[332]; // Battery - Technology
    var Battery_RechargeTime = A[340]; // Battery - Recharge Time
    var Battery_RunTime = A[341]; // Battery - Run Time (Up To)
    var Battery_StandbyTime = A[3296]; // Battery - Standby Time
    var AudioOutput_ConnectivityInterfaces = A[9506]; // Audio Output - Connectivity Interfaces
    
    if(AudioOutput_ConnectivityInterfaces.HasValue("wired")
    && AudioOutput_ConnectivityInterfaces.Values.Count < 2){
        result = "";
    }
    else if(Battery_RechargeTime.HasValue()
    && Battery_RechargeTime.Units.First().Name.In("%hour%")
    && Battery_RunTime.HasValue()
    && Battery_RunTime.Units.First().Name.In("%hour%")
    && Battery_StandbyTime.HasValue()
    && Battery_StandbyTime.Units.First().Name.In("%hour%")){
        var rechargeTime = $"{Battery_RechargeTime.FirstValue()} hours and provide up to ".Replace("1 hours", " 1 hour");
        result = $"These headphones have a charging time of {rechargeTime}{Battery_RunTime.FirstValue()} hours of play time with {Battery_StandbyTime.FirstValue()} hours of standby power";
    }
    else if(Battery_RechargeTime.HasValue()
    && Battery_RechargeTime.Units.First().Name.In("%min%")
    && Battery_RunTime.HasValue()
    && Battery_RunTime.Units.First().Name.In("%hour%")){
        result = $"Up to {Battery_RechargeTime.FirstValue()}-minute charge provides up to {Battery_RunTime.FirstValue()} hours of play time";
    }
    else if(Battery_RunTime.HasValue()
    && Battery_RunTime.Units.First().Name.In("%hour%")
    && Battery_Technology.HasValue()){
        var bat = Battery_Technology.HasValue("alkaline") ? $"an {Battery_Technology.FirstValue()}" : Battery_Technology.FirstValue();
        result = $"Up to {Battery_RunTime.FirstValue()} hours of play time with {bat} battery";
    }
    else if(Battery_RunTime.HasValue()
    && Battery_RunTime.Units.First().Name.In("%hour%")
    && Battery_RunTime.FirstValue() == 10){
        result = "You get 10 hours of uninterrupted listening time";
    }
    else if(Battery_RunTime.HasValue()
    && Battery_RunTime.Units.First().Name.In("%hour%")
    && Battery_RunTime.FirstValue() > 11){
        result = $"Impressive {Battery_RunTime.FirstValue()}-hour battery life to keep the music playing for a long time";
    }
    else if(Battery_RunTime.HasValue()
    && Battery_RunTime.Units.First().Name.In("%hour%")){
        result = $"Up to {Battery_RunTime.FirstValue()} hours of play time";
    }
    if(!String.IsNullOrEmpty(result)){
	    Add($"HeadphonesPowerSourceRechargeTimeBatteryLife⸮{result}");
	}
} 

// --[FEATURE #8] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HeadphonesBuiltRemote(){ 
var result = ""; 
var Brand = SKU.Brand; 
var ModelName = SKU.ModelName; 
// Over-Ear|On-Ear|Earbuds 
var HeadphoneTypeRef = R("SP-18729").HasValue() ? R("SP-18729").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18729").HasValue() ? R("cnet_common_SP-18729").Replace("<NULL>", "").Text : ""; 
var CableDetails_Details = A[8627]; // Cable Details - Details 

if(Brand.HasValue() 
&& Brand.In("Apple", "apple") 
&& ModelName.HasValue() 
&& ModelName.In("EarPods", "earpods") 
&& HeadphoneTypeRef.Equals("Earbuds")){ 
result = "The EarPods also include a built-in remote that lets you adjust the volume, control the playback of music ##and video"; 
} 
else if(CableDetails_Details.HasValue("reflective")){ 
result = "The reflective cord material enhances your visibilty at night"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesBuiltRemote⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HeadphonesBuiltMicAndControls(){ 
var result = ""; 
var AudioOutput_Controls = A[819]; // Audio Output - Controls 
var AudioOutput_AvailableMicrophone = A[8571]; // Audio Output - Available Microphone 

if(AudioOutput_AvailableMicrophone.HasValue() 
&& AudioOutput_Controls.HasValue() 
&& AudioOutput_Controls.Where("%answer%", "%end%").Any()){ 
result = "Easily answer and end calls thanks to the built-in mic and controls"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesBuiltMicAndControls⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HeadphonesTechnologyFeature(){ 
var result = ""; 
var AudioOutput_Details = A[9410]; // Audio Output - Details 

if(AudioOutput_Details.HasValue("%TwistLock%")){ 
result = "TwistLock technology aids to keep your headphones secured with a simple twist"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesTechnologyFeature⸮{result}"); 
} 
} 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HeadphonesEarParts(){ 
var result = ""; 
var Brand = SKU.Brand; 
var ModelName = SKU.ModelName; 
var AudioOutput_HeadphonesEarPartsType = A[7425]; // Audio Output - Headphones Ear-Parts Type 
var Miscellaneous_EarpadMaterial = A[8468]; // Miscellaneous - Earpad Material 

if(Brand.HasValue() 
&& Brand.In("Apple", "apple") 
&& ModelName.HasValue() 
&& ModelName.In("EarPods", "earpods") 
&& AudioOutput_HeadphonesEarPartsType.HasValue("ear-bud")){ 
result = "The remote and mic are supported by all models of iPod, iPhone, and iPad (not all models support volume up/down functions)"; 
} 
else if(Miscellaneous_EarpadMaterial.HasValue("leatherette")){ 
result = "Leatherette ear cushions for a superior fit"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesEarParts⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HeadphonesPushToTalk(){ 
var result = ""; 
var AudioOutput_Controls = A[819]; // Audio Output - Controls 

if(AudioOutput_Controls.HasValue("Push to Talk (PTT)")){ 
result = "Push-to-talk for effective communication"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesPushToTalk⸮{result}"); 
} 
} 

// --[FEATURE #13] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HeadphonesBuiltInMicrophone(){ 
var result = ""; 
// No|Yes 
var BuiltInMicrophoneRef = R("SP-22213").HasValue() ? R("SP-22213").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22213").HasValue() ? R("cnet_common_SP-22213").Replace("<NULL>", "").Text : ""; 
var AudioOutput_AvailableMicrophone = A[8571]; // Audio Output - Available Microphone 

if(!AudioOutput_AvailableMicrophone.HasValue() 
&& BuiltInMicrophoneRef.Equals("Yes")){ 
result = "Headphones come with an integrated microphone"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesBuiltInMicrophone⸮{result}"); 
} 
} 

// --[FEATURE #14] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HeadphonesNoiseCanceling(){ 
var result = ""; 
var Brand = SKU.Brand; 
var ModelName = SKU.ModelName; 
// Yes|No 
var NoiseCancelingRef = R("SP-22207").HasValue() ? R("SP-22207").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22207").HasValue() ? R("cnet_common_SP-22207").Replace("<NULL>", "").Text : ""; 
var SoundIsolatingRef = R("SP-22218").HasValue() ? R("SP-22218").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22218").HasValue() ? R("cnet_common_SP-22218").Replace("<NULL>", "").Text : ""; 
var AudioOutput_DiaphragmDiameter = A[879]; // Audio Output - Diaphragm Diameter 
var AudioOutput_MagnetMaterial = A[1574]; // Audio Output - Magnet Material 
var AudioOutput_HeadphonesEarPartsType = A[7425]; // Audio Output - Headphones Ear-Parts Type 
var AudioOutput_HeadphonesCupType = A[7427]; // Audio Output - Headphones Cup Type 
var AudioOutput_ActiveNoiseCanceling = A[3813]; // Audio Output - Active Noise Canceling 
var AudioOutput_ActiveNoiseCancelingTechnology = A[8574]; // Audio Output - Active Noise Canceling Technology 
var NoiseCanc = NoiseCancelingRef.Equals("Yes") ? " with noise canceling" : ""; 
var tmpTest = ""; 

if(Brand.HasValue() 
&& Brand.In("Apple", "apple") 
&& ModelName.HasValue() 
&& ModelName.In("EarPods", "earpods") 
&& AudioOutput_HeadphonesEarPartsType.HasValue("ear-bud")){ 
tmpTest = "Some"; 
} 
else if(AudioOutput_HeadphonesCupType.HasValue("closed") 
&& AudioOutput_DiaphragmDiameter.HasValue() 
&& AudioOutput_DiaphragmDiameter.Units.First().Name.In("mm") 
&& AudioOutput_DiaphragmDiameter.FirstValue() > 39 
&& AudioOutput_MagnetMaterial.HasValue("neodymium")){ 
tmpTest = "Some"; 
} 
else if(AudioOutput_HeadphonesCupType.HasValue("closed") 
&& AudioOutput_DiaphragmDiameter.HasValue() 
&& AudioOutput_DiaphragmDiameter.Units.First().Name.In("mm") 
&& AudioOutput_DiaphragmDiameter.FirstValue() < 39 
&& AudioOutput_MagnetMaterial.HasValue("neodymium")){ 
tmpTest = "Some"; 
} 
else if(AudioOutput_DiaphragmDiameter.HasValue() 
&& AudioOutput_DiaphragmDiameter.Units.First().Name.In("mm")){ 
tmpTest = "Some"; 
} 

if(!string.IsNullOrEmpty(tmpTest)){ 
if(AudioOutput_ActiveNoiseCancelingTechnology.HasValue("ActiveShield")){ 
result = "ActiveShield active noise canceling techonolgy features two feed-backward microphones used for canceling low frequencies"; 
} 
else if(AudioOutput_ActiveNoiseCanceling.HasValue()){ 
result = "Active noise canceling so you hear only the music"; 
} 
else if(SoundIsolatingRef.Equals("Yes")){ 
result = "Provide sound isolation from background noise"; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HeadphonesNoiseCanceling⸮{result}"); 
} 
} 

// --[FEATURE #15] - All 
// --Warranty information 

//5829362510113210844 "Headphones END" "Serhii.O" §§ 

//§§530868304206053 "Cell Phone Cases BEGIN" "Serhii.O" 

CellPhoneCaseStyleCompatibility(); 
CellPhoneCaseMaterialtrueColor(); 
CellPhoneCaseGripType(); 
CellPhoneCaseFashionCase(); 
CellPhoneCasesScreenProtector(); 
CellPhoneCasesProtections(); 
CellPhoneCasesRaisedEdge(); 
CellPhoneCasesMagnetic(); 
CellPhoneCasesLayerDefense(); 
CellPhoneCasesUnderwater(); 
CellPhoneCasesMicrofiber(); 
CellPhoneCasesUltraSlim(); 
HeadphonesNoiseCanceling(); 
// Warranty - All 

string prodFunc(string text, string compProducts){ 
return !string.IsNullOrEmpty(compProducts) ? $"{text}{compProducts}" : ""; 
} 

// --[FEATURE #1] 
// --Cell phone case style & compatibility 
void CellPhoneCaseStyleCompatibility(){ 
    var result = "";
    // Belt Strap|Clutch Bag|Bumper|Battery Case|Rugged Case|Slim Case|Waterproof|Case|Snap Case|Cover|Slider Case|Skin Case|Invisible Case|Pouch|Stand Case|Credit Card Case|Bundle|Arm Band|Clip/Holster|Wallet|Sleeve
    var cellPhoneCaseStyleRef = R("SP-18041").HasValue() ? R("SP-18041").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18041").HasValue() ? R("cnet_common_SP-18041").Replace("<NULL>", "").Text : "";
	var compatibleProducts = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body, SPEC["MS"].GetLine("Designed For").Body, SPEC["ES"].GetLine("Designed For").Body);
	var products = compatibleProducts.HasValue() ? compatibleProducts.Text.Replace(";", ",").Split(", ").Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).FlattenWithAnd().Replace("Note8", "Note ##8").Replace("samsung", "").Replace("apple", "").ToString() : "";
	var carryingCase_AdditionalCompartments = A[2527]; // Carrying Case - Additional Compartments
	
	if(!string.IsNullOrEmpty(cellPhoneCaseStyleRef)){
	    switch(cellPhoneCaseStyleRef){
            case "Arm Band":
            result = $"Arm band provides protection{prodFunc(" for ", products)} when you're working out";
            break;
            case var a when a.Equals("Battery Case")
            && compatibleProducts.HasValue():
            result = $"Battery case is{prodFunc(" compatible with your ", products.Replace("iPhone 6, 6s", "iPhone 6/6s"))}";
            break;
            case "Battery Case":
            result = $"Battery case is a rechargeable external battery{prodFunc(" for ", products).Replace("yes", "").Replace("No", " concealed inside of a protective form-fitting case")}";
            break;
            case "Snap Case":
            result = $"Quick snap-on assembly{prodFunc(" for ", products).Replace("yes", "").Replace("No", " concealed inside of a protective form-fitting case")}";
            break;
            case "Clip/Holster":
            result = $"Holster works as a belt clip and a hands-free kickstand{prodFunc(" for ", products)}";
            break;
            case "Credit Card Case":
            var additionalCompartments = carryingCase_AdditionalCompartments.HasValue("%credit cards%") && Coalesce(carryingCase_AdditionalCompartments.Where("%credit cards%").First()).ExtractNumbers().Any() ? $"up to {Coalesce(carryingCase_AdditionalCompartments.Where("%credit cards%").First()).ExtractNumbers().First()} cards" : "several cards";
            result = $"Credit card case features a built-in pocket that holds {additionalCompartments}{prodFunc(" and fits ", products)}";
            break;
            case "Invisible Case":
            result = $"Clear design allows your phone's beauty to shine through{prodFunc(" for ", products)}";
            break;
            case "Rugged Case":
            result = $"Rugged case offers protection{prodFunc(" to your ", products).Replace("yes", "").Replace("No", " against drops, bumps, and other everyday accidents and mishaps")} with this cover that securely encases the corners and back";
            break;
            case "Stand Case":
            result = $"Stand case{prodFunc(" with your ", products).Replace("yes", "").Replace("No", "can be converted into a stand that")}";
            break;
            case "Cover":
            var prods = prodFunc("", products).Replace("yes", "").Replace("No", "the device");
            result = prods != "" ? $"Protect your ##{prods} with this cover that securely encases the corners and back" : "This case offers protection for your cell phone";
            break;
            case "Waterproof":
            result = $"Waterproof case{prodFunc(" for your ", products).Replace("yes", "").Replace("No", "the device")}";
            break;
            case "Wallet":
            result = $"Cover folio provides screen protection{prodFunc(" for your ", products).Replace("yes", "").Replace("No", "the device")}";
            break;
            case "Case":
            result = $"This case is compatible with the ##{prodFunc(" for your ", products).Replace("5G", "5Gen").Replace("6G", "6Gen").Replace("yes", "").Replace("No", "cell phone")} with this cover that securely encases the corners and back";
            break;
        }
	}
    if(!string.IsNullOrEmpty(result)){
        Add($"CellPhoneCaseStyleCompatibility⸮{result}");
    }
} 

// --[FEATURE #2]
// --Cell phone case material & true color
void CellPhoneCaseMaterialtrueColor(){
    var result = "";
    var cellPhoneCaseStyleRef = R("SP-18041").HasValue() ? R("SP-18041") : R("cnet_common_SP-18041");
    var cellPhoneCaseMaterialRef = R("SP-18042").HasValue() ? R("SP-18042") : R("cnet_common_SP-18042");
    var trueColorRef = R("SP-22967").HasValue() ? R("SP-22967") : R("cnet_common_SP-22967");
    var carryingCase_Color = A[857];
    var carryingCase_Material = A[858];
    
    if(cellPhoneCaseMaterialRef.HasValue()
    && trueColorRef.HasValue("%/%")){
        result = $"Comes in {trueColorRef.ToLower().ToUpperFirstChar()} and features {cellPhoneCaseMaterialRef.ToLower()} construction";
    }
    else if(cellPhoneCaseMaterialRef.HasValue()
    && trueColorRef.HasValue("Big Sur")){
        result = $"##Big ##Sur and features {cellPhoneCaseMaterialRef.ToLower()} construction";
    }
    else if(carryingCase_Color.HasValue()
    && carryingCase_Color.Where("%Transparent%", "%clear%").Any()
    && carryingCase_Material.HasValue()){
        result = $"Comes in {carryingCase_Color.FirstValue().Replace("(", "").Replace(")", "").ToUpperFirstChar()} and features {carryingCase_Material.FirstValue().Replace("bio-enchanced", "")} construction for added durability";
    }
    else if(carryingCase_Color.HasValue()
    && carryingCase_Material.Values.Count > 2){
        result = $"Comes in {carryingCase_Color.FirstValue().Replace("black/black", "black").ToUpperFirstChar()} with {carryingCase_Material.FirstValue()} construction";
    }
    else if(carryingCase_Color.HasValue()
    && carryingCase_Material.HasValue("%high-%")){
        result = $"Comes in {carryingCase_Color.FirstValue().Replace("black/black", "black").ToUpperFirstChar()} with {carryingCase_Material.FirstValue()} construction";
    }
    else if(carryingCase_Color.HasValue()
    && carryingCase_Material.Values.Count == 2){
        result = $"Comes in {carryingCase_Color.FirstValue().Replace("(", "").Replace(")", "").ToUpperFirstChar()} and features {carryingCase_Material.FirstValue().Replace("(", "").Replace(")", "").Replace("bio-enchanced", "")} construction for added durability";
    }
    else if(carryingCase_Color.HasValue("%Transparent%")){
        result = "Clear design allows you to show off your device";
    }
    else if(trueColorRef.HasValue()
    && cellPhoneCaseStyleRef.HasValue("Waterproof")){
        result = $"Comes in {trueColorRef.ToLower().ToUpperFirstChar()} {cellPhoneCaseStyleRef.ToLower()}";
    }
    else if(trueColorRef.HasValue()){
        result = $"Features {cellPhoneCaseMaterialRef.ToLower()} construction for added durability";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"CellPhoneCaseMaterialtrueColor⸮{result}");
    }
}

// --[FEATURE #3] 
// --Cell phone grip type 
void CellPhoneCaseGripType(){ 
var result = ""; 

if(REQ.GetVariable("CNET_SD").In("%synthetic rubber slipcover%slip-resistant grip%")){ 
result = "Synthetic rubber slipcover offers a slip-resistant grip"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"CellPhoneCaseGripType⸮{result}"); 
} 
} 

// --[FEATURE #4] 
// --Fashion case (If Applicable) 
void CellPhoneCaseFashionCase(){ 
var result = ""; 
var carryingCase_Color = A[857]; // Carrying Case - Color 
var Miscellaneous_ThemeDesign = A[8931]; // Miscellaneous - Theme / Design 

if(carryingCase_Color.HasValue("%Disney%")){ 
result = "Classic Disney graphics express your fun personality"; 
} 
else if(Miscellaneous_ThemeDesign.HasValue("%star wars%", "%darth vader%") 
|| carryingCase_Color.HasValue("%star wars%", "%darth vader%")){ 
result = "Features inspiring star war graphics to express your allegiance"; 
} 
else if(Miscellaneous_ThemeDesign.HasValue()){ 
result = "Stylish designs: dynamic graphics and exclusive designer patterns dress up your device"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"CellPhoneCaseFashionCase⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void CellPhoneCasesScreenProtector(){ 
var result = ""; 
var Miscellaneous_IncludedAccessories = A[1443]; // Miscellaneous - Included Accessories 

if(Miscellaneous_IncludedAccessories.HasValue("%screen%protector%")){ 
result = $"Included {Miscellaneous_IncludedAccessories.Where("%screen%protector%").First().Value().ToLower()} prevents scratches without losing any touch sensitivity"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"CellPhoneCasesScreenProtector⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void CellPhoneCasesProtections(){ 
var result = "";
    var miscellaneous_CompliantStandards = A[380]; // Miscellaneous - Compliant Standards
    var miscellaneous_Protection = A[5810]; // Miscellaneous - Protection
    
    if(miscellaneous_Protection.HasValue("%shock%")
    && miscellaneous_CompliantStandards.HasValue("%mil-%")){
        result = "Reinforced shock absorbing corners deliver military grade defense";
    }
    else if(miscellaneous_Protection.HasValue("%scratch%")){
        result = "Case is fully scratch-resistant, prevents unsightly scrapes and discoloration";
    }
    else if(miscellaneous_Protection.HasValue()
    && miscellaneous_Protection.Where("%shock%", "%impact%").Any()){
        result = "Offers shock-absorbing and impact-resistant protection for durability";
    }
    else if(miscellaneous_Protection.HasValue()
    && miscellaneous_Protection.Where("%protection%", "%proof%", "%resistant%", "%repellent%").Any()){
        result = $"{miscellaneous_Protection.Where("%protection%", "%proof%", "%resistant%", "%repellent%").Select(s => s.Value()).FlattenWithAnd(30, ", ").ToUpperFirstChar()} design for added protection";
    }
    else if(miscellaneous_Protection.Where("%protection%", "%proof%", "%resistant%", "%repellent%").Any()){
        result = $"{miscellaneous_Protection.Where("%protection%", "%proof%", "%resistant%", "%repellent%").Select(s => s.Value()).FlattenWithAnd(30, ", ").ToUpperFirstChar()} design for added protection";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"CellPhoneCasesProtections⸮{result}");
    }
} 

// --[FEATURE #7] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void CellPhoneCasesRaisedEdge(){ 
var result = ""; 

if(DC.MKT.GetString().In("%Raised edge%")){ 
result = "Raised edge protects screen. The raised edge around screen provides extra protection against drops"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"CellPhoneCasesRaisedEdge⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void CellPhoneCasesMagnetic(){ 
var result = ""; 
var carryingCase_ClosureType = A[8615]; // Carrying Case - Closure Type 

if(carryingCase_ClosureType.HasValue("%Magnetic%")){ 
result = "Magnetic closure keeps your case securely fastened"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"CellPhoneCasesMagnetic⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void CellPhoneCasesLayerDefense(){ 
var result = ""; 
var miscellaneous_Protection = A[5810]; // Carrying Case - Closure Type 

if(DC.MKT.GetString().In("%dual layer%") 
&& miscellaneous_Protection.HasValue("%SHock%") 
&& miscellaneous_Protection.HasValue("%drop%")){ 
result = "Double-layered cover provides shock-absorption protection from drops and falls"; 
} 
else if(DC.MKT.GetString().In("%three layers%", "%multi-layered%")){ 
result = "Triple-layer defense such as inner shell, outer slipcover and touchscreen protector"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"CellPhoneCasesLayerDefense⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void CellPhoneCasesUnderwater(){ 
var result = ""; 
var miscellaneous_UnderwaterDepth = A[2108]; // Miscellaneous - Underwater Depth 

if(miscellaneous_UnderwaterDepth.HasValue()){ 
result = "Take photos even under water"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"CellPhoneCasesUnderwater⸮{result}"); 
} 
} 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void CellPhoneCasesMicrofiber(){ 
var result = ""; 
var carryingCase_LiningMaterial = A[8631]; // Carrying Case - Lining Material 

if(carryingCase_LiningMaterial.HasValue("% microfiber %")){ 
result = "Inside of case is made of material that will not scratch your phone"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"CellPhoneCasesMicrofiber⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void CellPhoneCasesUltraSlim(){ 
var result = ""; 

if(DC.KSP.GetString().In("%ultra slim%")){ 
result = "Slim profile: sleek design slips easily into pockets and purses"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"CellPhoneCasesUltraSlim⸮{result}"); 
} 
} 

// --[FEATURE #13] - All 
// --Warranty information 

//530868304206053 "Cell Phone Cases END" "Serhii.O" §§ 

//§§168134749830140542 "Projectors BEGIN" "Serhii.O" 

ProjectorsTypeUse(); 
ProjectorsSystem(); 
ProjectorsColorBrightness(); 
ProjectorsResolution(); 
ProjectorsLampTypeLife(); 
ProjectorsLensFocusType(); 
ProjectorsProjectionDistance(); 
ProjectorsThrowRatio(); 
ProjectorsAudioSupport(); 
ProjectorsConnectivity(); 
ProjectorsCompatibility(); 
// Warranty - All 

// --[FEATURE #1] 
// --Projector type & Use 
void ProjectorsTypeUse(){ 
var result = ""; 
// Seasonal|Home Theater|Gaming|Business|Overhead|Pico (Handheld) 
var projectorTypeRef = R("SP-18105").HasValue() ? R("SP-18105").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18105").HasValue() ? R("cnet_common_SP-18105").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(projectorTypeRef)){ 
switch(projectorTypeRef){ 
// https://www.amazon.com/Total-HomeFX-Projector-Pre-Loaded-Projection/dp/B0165ZJ0VI 
case "Seasonal": 
result = "Seasonal projector will surely light up your window with holiday cheer"; 
break; 
case "Business": 
result = "Business projector provides superior quality for vibrant pictures and crisp text"; 
break; 
case "Home Theater": 
result = "Home Theater projector for enjoying movies, live sporting events, games and more"; 
break; 
case "Overhead": 
result = "Overhead projector is the perfect choice for on-the-go meetings and small room gatherings"; 
break; 
case "Pico (Handheld)": 
result = "Output your favorite devices wherever you want with this Pico Projector"; 
break; 
// https://www.staples.com/viewsonic-px706hd-3d-ready-short-throw-dlp-projector-1080p-hdtv-16-9/product_IM13GQ606 
case "Gaming": 
result = "Gaming projector is ideal for intense action-packed gaming"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsTypeUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Projection system 
void ProjectorsSystem(){ 
var result = ""; 
// Other|DLP|LCD|LCOS 
var projectorDisplayTechnologyRef = R("SP-18079").HasValue() ? R("SP-18079").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18079").HasValue() ? R("cnet_common_SP-18079").Replace("<NULL>", "").Text : ""; 
var projectorType = A[6259]; // Projector - Type 

if(!string.IsNullOrEmpty(projectorDisplayTechnologyRef)){ 
switch(projectorDisplayTechnologyRef){ 
case "DLP": 
result = "DLP technology inside provides a bright, crisp and dependable picture"; 
break; 
case "LCOS": 
result = "LCOS technology provides a high-quality pictures"; 
break; 
case "LCD": 
result = "The 3LCD projection makes images sharper when presenting colorful graphics and pictures"; 
break; 
case var a when a.Equals("Other") 
&& projectorType.HasValue("D-ILA projector", "SXRD projector"): 
result = $"{projectorType.FirstValue().Replace(" projector", "")} technology"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsSystem⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Color brightness (Lumens) 
void ProjectorsColorBrightness(){ 
var result = ""; 
var colorBrightnessRef = R("SP-23893").HasValue() ? R("SP-23893").Replace("<NULL>", "").Text : 
R("cnet_common_SP-23893").HasValue() ? R("cnet_common_SP-23893").Replace("<NULL>", "").Text : ""; 
var Projector_BrightnessColor = A[6949]; // Projector - Brightness (Color) 
var Projector_BrightnessWhite = A[7519]; // Projector - Brightness (White) 
var lm = Coalesce(colorBrightnessRef).ExtractNumbers(); 

if(!string.IsNullOrEmpty(colorBrightnessRef) 
&& lm.Any()){ 
switch(lm.First()){ 
case var lumens when lumens < 2500: 
result = $"With a brightness of {lumens} lm, it produces vivid colors with subtle details for rich image quality"; 
break; 
case var lumens when lumens < 3600 
&& Projector_BrightnessWhite.HasValue() 
&& Projector_BrightnessColor.HasValue(Projector_BrightnessWhite.FirstValue().ToString()): 
result = $"{lumens} lumens of equal color and white brightness provide a high-quality picture"; 
break; 
case var lumens when lumens < 3600: 
result = $"With a brightness rating of {lumens} lm, projector ensures your important presentations really shine"; 
break; 
case var lumens when lumens > 3600 
&& Projector_BrightnessWhite.HasValue() 
&& Projector_BrightnessColor.HasValue(Projector_BrightnessWhite.FirstValue().ToString()): 
result = $"{lumens} lumens of color and white brightness for vivid, colorful images, even in well-lit rooms"; 
break; 
case var lumens when lumens > 3600: 
result = $"Enhance demonstrations and classroom materials with ease when using this {lumens} lumens projector"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsColorBrightness⸮{result}"); 
} 
} 

// --[FEATURE #4] 
// --Resolution 
void ProjectorsResolution(){ 
var result = ""; 
// WXGA|Full HD|XGA|FWVGA|SVGA|HD 720p|UXGA|WUXGA|Ultra HD 4K|WVGA 
var projectorResolutionRef = R("SP-22143").HasValue() ? R("SP-22143").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22143").HasValue() ? R("cnet_common_SP-22143").Replace("<NULL>", "").Text : ""; 
var colorBrightnessRef = R("SP-23893").HasValue() ? R("SP-23893").Replace("<NULL>", "").Text : 
R("cnet_common_SP-23893").HasValue() ? R("cnet_common_SP-23893").Replace("<NULL>", "").Text : ""; 
// Seasonal|Home Theater|Gaming|Business|Overhead|Pico (Handheld) 
var projectorTypeRef = R("SP-18105").HasValue() ? R("SP-18105").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18105").HasValue() ? R("cnet_common_SP-18105").Replace("<NULL>", "").Text : ""; 
// Other|DLP|LCD|LCOS 
var projectorDisplayTechnologyRefRef = R("SP-18079").HasValue() ? R("SP-18079").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18079").HasValue() ? R("cnet_common_SP-18079").Replace("<NULL>", "").Text : ""; 
var projector_MaxResolutionResized = A[2348]; // Projector - Max Resolution (Resized) 
var projector_NativeResolution = A[6261]; // Projector - Native Resolution 
var projector_DisplayResolutionAbbreviation = A[6262]; // Projector - Display Resolution Abbreviation 

switch(projectorResolutionRef){ 
case var res when res.Equals("SVGA") 
&& projector_NativeResolution.HasValue("800 x 600"): 
result = "SVGA 800 x 600 resolution is ideal for everyday presentations and graphics"; 
break; 
case var res when res.Equals("SVGA"): 
result = "SVGA is the least expensive solution that delivers image sharper and cleaner"; 
break; 
case var res when res.Equals("WUXGA") 
&& projector_NativeResolution.HasValue("1920 x 1200"): 
result = "Featuring WUXGA resolution (1920 x 1200), 4.5x more than SVGA"; 
break; 
case var res when res.Equals("WUXGA"): 
result = "Enjoy crystal clear projection with WUXGA resolution"; 
break; 
case var res when (res.Equals("WUXGA") 
|| res.Equals("WXGA")) 
&& projector_NativeResolution.HasValue("1280 x 800"): 
result = $"{res} 1280 x 800 resolution is ideal for HD presentations and video; perfect for widescreen laptops"; 
break; 
case var res when res.Equals("WXGA"): 
result = $"Offers {res} native resolution for enhanced clarity"; 
break; 
case var res when res.Equals("XGA") 
&& projector_NativeResolution.HasValue("1024 x 768"): 
result = "XGA 1024 x 768 resolution is ideal for text-heavy presentations with greater detail"; 
break; 
case var res when res.Equals("XGA"): 
result = "XGA - a graphic mode that is suitable for spreadsheets and presentations at a budget-friendly price"; 
break; 
default:{ 
if(projector_DisplayResolutionAbbreviation.HasValue("Full HD") 
&& !string.IsNullOrEmpty(colorBrightnessRef) 
&& !string.IsNullOrEmpty(projectorTypeRef) 
&& !string.IsNullOrEmpty(projectorDisplayTechnologyRefRef)){ 
result = "##This machine projects clear and crisp ##Full ##HD images with brightest of whites and deepest of blacks, every color and pixel preserved"; 
} 
else if(projector_DisplayResolutionAbbreviation.HasValue("Full HD")){ 
result = "This machine projects crisp and clear ##Full ##HD images with brightest of whites and deepest of blacks"; 
} 
else if(projector_DisplayResolutionAbbreviation.HasValue("Ultra HD 4K")){ 
result = $"Bring the cinematic 4K UHD experience to your home with the {SKU.ProductLineName.Text} {SKU.ModelName.Text}"; 
} 
else if(projector_DisplayResolutionAbbreviation.HasValue("WVGA")){ 
var Resolution = projector_NativeResolution.HasValue() ? $" {projector_NativeResolution.FirstValue()}" : ""; 
var MaxResolutionResized = projector_MaxResolutionResized.HasValue() ? $", but accepts signals up to {projector_MaxResolutionResized.FirstValue()}" : ""; 
result = $"Features a WVGA{Resolution} native resolution{MaxResolutionResized}"; 
} 
else if(projector_NativeResolution.HasValue()){ 
result = $"{projector_NativeResolution.FirstValue()} native resolution is ideal for everyday presentations and graphics"; 
} 
} 
break; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsResolution⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Lamp type & life; ECO Mode (Hours) 
void ProjectorsLampTypeLife(){ 
var result = ""; 
// 15000|5000|12000|6000|4000|6500|4500|10000|1500|2000|20000|7000|8000|3500|2500|30000|50000|3000|9000 
var lampLife_ECOModeHoursRef = R("SP-22148").HasValue() ? R("SP-22148").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22148").HasValue() ? R("cnet_common_SP-22148").Replace("<NULL>", "").Text : ""; 
var projectorLamp_LampType = A[157]; // Projector Lamp - Lamp Type 
var projectorLamp_LampLifeCycle = A[158]; // Projector Lamp - Lamp Life Cycle 
var projector_ProjectorFeatures = A[2338]; // Projector - Projector Features 
var projectorLamp_LampLifeCycleEconomicMode = A[3828]; // Projector Lamp - Lamp Life Cycle (Economic Mode) 
var projectorLampLifeDetails_LampLife = A[11617]; // Projector Lamp Life Details - Lamp Life 

if(projectorLamp_LampType.HasValue("laser/LED") 
&& projectorLamp_LampLifeCycle.HasValue() 
&& !string.IsNullOrEmpty(lampLife_ECOModeHoursRef)){ 
result = $"The projector has laser and LED hybrid illumination technology with the service life of up to {projectorLamp_LampLifeCycle.FirstValue()} hours and up to {lampLife_ECOModeHoursRef} hours in ##ECO Mode"; 
} 
else if(projectorLamp_LampType.HasValue("laser/LED") 
&& projectorLamp_LampLifeCycle.HasValue()){ 
result = $"The projector has laser and LED hybrid illumination technology with the service life of up to {projectorLamp_LampLifeCycle.FirstValue()} hrs"; 
} 
else if(projectorLamp_LampType.HasValue("LED") 
&& projectorLamp_LampLifeCycle.HasValue() 
&& !string.IsNullOrEmpty(lampLife_ECOModeHoursRef)){ 
result = $"Energy-efficient LED lamp is rated to last over {projectorLamp_LampLifeCycle.FirstValue()} hours and {lampLife_ECOModeHoursRef} hours in ##ECO Mode"; 
} 
else if(projectorLamp_LampType.HasValue("LED") 
&& projectorLamp_LampLifeCycle.HasValue()){ 
result = $"Energy-efficient LED lamp is rated to last over {projectorLamp_LampLifeCycle.FirstValue()} hours"; 
} 
else if(projectorLamp_LampType.HasValue("laser/phosphor") 
&& projectorLamp_LampLifeCycle.HasValue() 
&& !string.IsNullOrEmpty(lampLife_ECOModeHoursRef)){ 
result = $"Projector has a laser/phosphor light source that makes it ideal for higher education classrooms, corporate boardrooms and government training rooms with the service life of up to {projectorLamp_LampLifeCycle.FirstValue()} hours and up to {lampLife_ECOModeHoursRef} hours in ##ECO Mode"; 
} 
else if(projectorLamp_LampType.HasValue("laser/phosphor") 
&& projectorLamp_LampLifeCycle.HasValue()){ 
result = $"Projector has a laser/phosphor light source that makes it ideal for higher education classrooms, corporate boardrooms and government training rooms with the service life of up to {projectorLamp_LampLifeCycle.FirstValue()} hrs"; 
} 
else if(projectorLamp_LampType.HasValue("EYB", "UHE", "UHM", "UHP") 
&& projectorLamp_LampLifeCycle.HasValue() 
&& !string.IsNullOrEmpty(lampLife_ECOModeHoursRef)){ 
result = $"{projectorLamp_LampType.FirstValue()} lamp has life of up to {projectorLamp_LampLifeCycle.FirstValue()} hours for reliable extended use and up to {lampLife_ECOModeHoursRef} hours in ##ECO Mode"; 
} 
else if(projectorLamp_LampType.HasValue("EYB", "UHE", "UHM", "UHP") 
&& projectorLamp_LampLifeCycle.HasValue()){ 
result = $"{projectorLamp_LampType.FirstValue()} lamp has life of up to {projectorLamp_LampLifeCycle.FirstValue()} hours for reliable extended use"; 
} 
else if(projectorLampLifeDetails_LampLife.HasValue() 
&& projectorLampLifeDetails_LampLife.Values.Select(s => s.Value()).Max() > 0 
&& projectorLamp_LampLifeCycle.HasValue() 
&& projectorLamp_LampLifeCycle.FirstValue() > 0 
&& projectorLamp_LampLifeCycle.FirstValue() < projectorLampLifeDetails_LampLife.Values.Select(s => s.Value()).Max()){ 
result = $"Energy-efficient lamp can run for up to {projectorLamp_LampLifeCycle.FirstValue()} hours or up to {projectorLampLifeDetails_LampLife.Values.Select(s => s.Value()).Max()} hours in ##ECO Mode"; 
} 
else if(projectorLamp_LampType.HasValue() 
&& projectorLamp_LampLifeCycle.HasValue()){ 
var Features = projector_ProjectorFeatures.HasValue() ? $" or up to {projector_ProjectorFeatures.FirstValue()} hours in ##ECO Mode" : ""; 
result = $"Energy-efficient {projectorLamp_LampType.FirstValue()} lamp can run for up to {projectorLamp_LampLifeCycle.FirstValue()} hours{Features}"; 
} 
else if(projectorLamp_LampLifeCycle.HasValue()){ 
var Features = projector_ProjectorFeatures.HasValue() ? $" or up to {projector_ProjectorFeatures.FirstValue()} hours in ##ECO Mode" : ""; 
result = $"Energy-efficient lamp can run for up to {projectorLamp_LampLifeCycle.FirstValue()} hours{Features}"; 
} 
else if(projectorLamp_LampLifeCycleEconomicMode.HasValue() 
&& projector_ProjectorFeatures.HasValue("EcoProjection Technology")){ 
result = $"EcoProjection technology reduces power in standby mode and its lamp lasts up to {projectorLamp_LampLifeCycleEconomicMode.FirstValue()} hours in ##ECO Mode"; 
} 
else if(projectorLamp_LampLifeCycleEconomicMode.HasValue() 
&& projector_ProjectorFeatures.HasValue("SuperEco")){ 
result = $"An energy-saving SuperEco feature reduces power consumption and extends the lamp life by up to {projectorLamp_LampLifeCycleEconomicMode.FirstValue()} hours"; 
} 
else if(projectorLamp_LampLifeCycleEconomicMode.HasValue() 
&& projector_ProjectorFeatures.HasValue("DynamicEco Mode")){ 
result = $"An energy-saving DynamicEco® feature reduces power consumption by up to 70%, and extends the lamp life up to {projectorLamp_LampLifeCycleEconomicMode.FirstValue()} hours"; 
} 
else if(projectorLamp_LampLifeCycleEconomicMode.HasValue()){ 
result = $"Lamp lasts up to {projectorLamp_LampLifeCycleEconomicMode.FirstValue()} hours in ##ECO Mode"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsLampTypeLife⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Lens focus type 
void ProjectorsLensFocusType(){ 
var result = ""; 
// Manual|Auto|Fixed|Auto, Manual 
var lensFocusTypeRef = R("SP-22147").HasValue() ? R("SP-22147").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22147").HasValue() ? R("cnet_common_SP-22147").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(lensFocusTypeRef)){ 
switch(lensFocusTypeRef){ 
case "Auto, Manual": 
result = "Lens with manual ##and auto focus for superior-quality projection"; 
break; 
// https://www.bhphotovideo.com/c/product/629269-REG/Panasonic_ET_DLE055_ET_DLE055_Fixed_Focus_Lens.html 
case "Fixed": 
result = "Fixed focus lens adds flexibility for rear projection short throw installations"; 
break; 
case "Manual": 
result = "Manual focus lens delivers high-quality projection of images and videos that enable users to fit any image onto the screen"; 
break; 
case "Auto": 
result = "Lens with autofocus for superior-quality projection"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsLensFocusType⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Projection Distance 
void ProjectorsProjectionDistance(){ 
var result = ""; 
var projectionDistanceRef = R("SP-429").HasValue() ? R("SP-429").Replace("<NULL>", "").Text : 
R("cnet_common_SP-429").HasValue() ? R("cnet_common_SP-429").Replace("<NULL>", "").Text : ""; 
var projector_MaxScreenDistance = A[144]; // Projector - Max Screen Distance 
var projector_MinScreenDistance = A[145]; // Projector - Min Screen Distance 

if(!string.IsNullOrEmpty(projectionDistanceRef) 
&& projector_MaxScreenDistance.HasValue() 
&& projector_MinScreenDistance.HasValue()){ 
result = $"Project distance between {projector_MinScreenDistance.FirstValueUsm().RegexReplace(@"(?:(\.\d*?[1-9]+)|\.)0*$", "$1")}{projector_MinScreenDistance.Units.First().NameUSM.Replace("ft", "'").Replace("in", @"""")} and {projector_MaxScreenDistance.FirstValueUsm().RegexReplace(@"(?:(\.\d*?[1-9]+)|\.)0*$", "$1")}{projector_MaxScreenDistance.Units.First().NameUSM.Replace("ft", "'").Replace("in", @"""")} offers versatility in placement"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsProjectionDistance⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Throw Ratio 
void ProjectorsThrowRatio(){ 
var result = ""; 
var throwRatioRef = R("SP-4041").HasValue() ? R("SP-4041").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4041").HasValue() ? R("cnet_common_SP-4041").Replace("<NULL>", "").Text : ""; 
var Projector_ThrowRatio = A[4516]; // Projector - Throw Ratio 

if(Projector_ThrowRatio.HasValue() 
&& Projector_ThrowRatio.FirstValue().ExtractNumbers().Any() 
&& Projector_ThrowRatio.FirstValue().ExtractNumbers().Min() < 0.38){ 
result = $"A {throwRatioRef} throw lens enables the projector to be placed several inches away from a wall or screen, making it flexible enough to be ceiling-mounted or simply placed on the tabletop"; 
} 
else if(Projector_ThrowRatio.HasValue() 
&& Projector_ThrowRatio.FirstValue().ExtractNumbers().Any() 
&& Projector_ThrowRatio.FirstValue().ExtractNumbers().Min() < 1.5){ 
result = $"A {throwRatioRef} throw lens enables large images to be projected from short distances in small rooms"; 
} 
else if(!string.IsNullOrEmpty(throwRatioRef)){ 
result = $"{throwRatioRef} throw ratio for convenient viewing"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsThrowRatio⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Audio Support 
void ProjectorsAudioSupport(){ 
var result = ""; 
var audioOutput_Type = A[264]; // Audio Output - Type 
var audioOutput_FormFactor = A[265]; // Audio Output - Form Factor 
var audioOutput_OutputPowerChannel = A[270]; // Audio Output - Output Power / Channel 
var audioOutputDetails_SpeakersQty = A[512]; // Audio Output Details - Speakers Qty 
var interfaceProvided_Interface = A[2674]; // Interface Provided - Interface 

if(audioOutput_Type.HasValue("speaker(s)") 
&& audioOutput_FormFactor.HasValue("integrated") 
&& audioOutput_OutputPowerChannel.HasValue() 
&& interfaceProvided_Interface.HasValue() 
&& interfaceProvided_Interface.Where("audio line-in", "audio line-out", " microphone input").Count() > 2){ 
result = $"Projector features {audioOutput_OutputPowerChannel.FirstValue()}W audio speaker with multiple audio ports for easy connection to other audio devices"; 
} 
else if(audioOutput_Type.HasValue("speaker(s)") 
&& audioOutput_FormFactor.HasValue("integrated") 
&& audioOutput_OutputPowerChannel.HasValue() 
&& audioOutput_OutputPowerChannel.FirstValue().ExtractNumbers().Any() 
&& audioOutput_OutputPowerChannel.FirstValue().ExtractNumbers().First() > 7){ 
result = $"Built-in {audioOutput_OutputPowerChannel.FirstValue()}W speaker delivers crisp audio"; 
} 
else if(audioOutput_Type.HasValue("speaker(s)") 
&& audioOutput_FormFactor.HasValue("integrated") 
&& audioOutput_OutputPowerChannel.HasValue() 
&& audioOutput_OutputPowerChannel.FirstValue().ExtractNumbers().Any() 
&& audioOutput_OutputPowerChannel.FirstValue().ExtractNumbers().First() > 19){ 
result = $"It has powerful full range {audioOutput_OutputPowerChannel.FirstValue()}-watt speaker to offer a better quality audio"; 
} 
else if(audioOutput_Type.HasValue("speaker(s)") 
&& audioOutput_FormFactor.HasValue("integrated") 
&& audioOutput_OutputPowerChannel.HasValue() 
&& audioOutputDetails_SpeakersQty.HasValue() 
&& audioOutputDetails_SpeakersQty.Values.Count == 1 
&& audioOutputDetails_SpeakersQty.FirstValue() == 2){ 
result = $"Dual {audioOutput_OutputPowerChannel.FirstValue()}W stereo speakers generate loud and crisp audio"; 
} 
else if(audioOutput_Type.HasValue("speaker(s)") 
&& audioOutput_FormFactor.HasValue("integrated") 
&& audioOutput_OutputPowerChannel.HasValue()){ 
result = $"Equipped with ##{audioOutput_OutputPowerChannel.FirstValue()}W speaker to provide high-quality audio output"; 
} 
else if(audioOutput_Type.HasValue("speaker(s)") 
&& audioOutput_FormFactor.HasValue("internal")){ 
result = "Internal speaker delivers room-filling sound for an immersive audiovisual experience"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsAudioSupport⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Projector connectivity (USB/LAN/WLAN/USB/HDMI) 
void ProjectorsConnectivity(){ 
var result = ""; 
// HDMI/USB/Audio Line In/VGA In|HDMI|HDMI/USB/Composite Video/Component Video/S-Video/Audio Line In/ Audio Line|HDMI/USB/Composite Video/Audio Line In/Audio Line Out/VGA In|HDMI/DVI|Wireless|HDMI/USB|HDMI/USB/Wireless 
var projectorConnectivityRef = R("SP-350474").HasValue() ? R("SP-350474").Replace("<NULL>", "").Text : 
R("cnet_common_SP-350474").HasValue() ? R("cnet_common_SP-350474").Replace("<NULL>", "").Text : ""; 
var InterfaceProvided_Interface = A[2674]; // Interface Provided - Interface 

if(!string.IsNullOrEmpty(projectorConnectivityRef)){ 
result = $"Connectivity: {projectorConnectivityRef}"; 
} 
else if(InterfaceProvided_Interface.HasValue()){ 
result = $"Connectivity: {InterfaceProvided_Interface.Values.FlattenWithAnd()}"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsConnectivity⸮{result}"); 
} 
} 

// --[FEATURE #11] 
// --Projector compatibility 
void ProjectorsCompatibility(){ 
var result = ""; 
// PC|PC, Laptop|Android, PC, Mac|Mac|Smartphone, Notebook, Tablet|PC, Laptop, Mobile Devices|PC, Mac|PC, Mac, Linux 
var projectorCompatibilityRef = R("SP-22154").HasValue() ? R("SP-22154").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22154").HasValue() ? R("cnet_common_SP-22154").Replace("<NULL>", "").Text : ""; 
var compatibleProducts = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body, SPEC["MS"].GetLine("Designed For").Body, SPEC["ES"].GetLine("Designed For").Body); 
var header_Compatibility = A[603]; // Header - Compatibility 

if(!string.IsNullOrEmpty(projectorCompatibilityRef) 
&& header_Compatibility.HasValue()){ 
result = $"Compatible with ##{header_Compatibility.Values.FlattenWithAnd().Replace("mac", "##Mac")} to give you enhanced functionality"; 
} 
else if(!string.IsNullOrEmpty(projectorCompatibilityRef) 
&& projectorCompatibilityRef.Equals("PC, Mac")){ 
result = "Compatible with ##PC and ##Mac to give you enhanced functionality"; 
} 
else if(!string.IsNullOrEmpty(projectorCompatibilityRef)){ 
result = $"Compatible with {projectorCompatibilityRef.Split(", ").FlattenWithAnd()} to give you enhanced functionality"; 
} 
else if(compatibleProducts.HasValue()){ 
result = $"Compatible with {compatibleProducts.Text.Replace(";", ",").Split(", ").Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).FlattenWithAnd()}"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"ProjectorsCompatibility⸮{result}"); 
} 
} 

// --[FEATURE #12] - All 
// --Warranty 

//168134749830140542 "Projectors END" "Serhii.O" §§ 

//§§1683392410972140733 "Vacuums BEGIN" "Serhii.O" 

VacuumsTypeUse();
VacuumsBagless();
VacuumsFiltrationSystem();
VacuumsLightingPowerType();
VacuumsPowerVoltage();
VacuumsCordLength();
// Certification & standards - All
// Dimensions - All
AdditionalVacuumsMotorDetails();
AdditionalVacuumsBatteries();
AdditionalVacuumsCordlessDesign();
AdditionalVacuumsFingertipControls();
AdditionalVacuumsIndicators();
AdditionalVacuumsSwivelSteering();
AdditionalVacuumsBrushRoll();
AdditionalVacuumsEdgeCreviceTool();
AdditionalVacuumsCyclonicFiltration();
AdditionalVacuumsCarpetHeightAdjustments();
AdditionalVacuumsEdgeCleaning();
AdditionalVacuumsPackageContent();
// True Color - All
// Warranty - All

// --[FEATURE #1] 
// --Type of vacuum & designed for use 
void VacuumsTypeUse(){ 
    var result = "";
    var Cleaning_ProductType = A[3600]; // Cleaning - Product Type
    var Cleaning_CleanerType = A[3602]; // Cleaning - Cleaner Type
    var TypeOfVacuumRef = R("SP-18161").HasValue() ? R("SP-18161").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18161").HasValue() ? R("cnet_common_SP-18161").Replace("<NULL>", "").Text : "";
    
    // S21197393 - Stick vacuum cleaner quickly removes pet hair, dust, dirt and other debris
    if(Cleaning_ProductType.HasValue()
    && Cleaning_CleanerType.HasValue()){
        result = $"{Cleaning_CleanerType.FirstValue().Replace("stick/handheld", "2-in-1 stick and handheld").ToUpperFirstChar()} {Cleaning_ProductType.FirstValue().ToLower()} quickly removes pet hair, dust, dirt and other debris";
    }
    else if(TypeOfVacuumRef.ToLower().Contains("filter")){
        result = "High-grade functional vacuum cleaner filter for eliminating all kind of dirt particles";
    }
    else if(!string.IsNullOrEmpty(TypeOfVacuumRef)){
        result = $"Product type: {TypeOfVacuumRef.ToLower()}";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"VacuumsTypeUse⸮{result}");
    }
} 

// --[FEATURE #2] 
// --Bagless 
void VacuumsBagless(){ 
    var result = "";
    var CapacityRef = R("SP-21131").HasValue() ? R("SP-21131").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21131").HasValue() ? R("cnet_common_SP-21131").Replace("<NULL>", "").Text : "";
    var BaglessRef = R("SP-18113").HasValue() ? R("SP-18113").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18113").HasValue() ? R("cnet_common_SP-18113").Replace("<NULL>", "").Text : "";
    var CapacityInGallonsRef = R("SP-21345").HasValue() ? R("SP-21345").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21345").HasValue() ? R("cnet_common_SP-21345").Replace("<NULL>", "").Text : "";
        
    if(BaglessRef.Equals("Yes")){
        result = "Bagless technology saves time and energy with no bags to replace";
    }
    else if(Coalesce(CapacityRef).ExtractNumbers().Any() && Coalesce(CapacityRef).ExtractNumbers().First() > 0.0){
        result = $"Capacity of ##{CapacityRef} qt. for collecting dirt or grime in the dust bag";
    }
    else if(!string.IsNullOrEmpty(CapacityInGallonsRef)){
        result = $"Capacity volume: {CapacityInGallonsRef} gal.";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"VacuumsBagless⸮{result}");
    }
} 

// --[FEATURE #3] 
// --Vacuum filtration system 
void VacuumsFiltrationSystem(){ 
var result = ""; 
var Cleaning_FilterType = A[3597]; // Cleaning - Filter Type 

if(Cleaning_FilterType.HasValue()){ 
result = $"{Cleaning_FilterType.Values.Select(s => s.Value().Replace(" filters", "").Replace("filter", "").Replace("2 ", "").ToLower()).FlattenWithAnd().ToUpperFirstChar()} filtration eliminates more than 99.97% of dirt, dust, and pollen"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"VacuumsFiltrationSystem⸮{result}"); 
} 
} 

// --[FEATURE #4] 
// --Lighting power type 
void VacuumsLightingPowerType(){ 
var result = ""; 
// Battery Powered|Standard Voltage - Wired (120V)|Solar Powered|Low Voltage - Wired (12V) 
var LightingPowerTypeRef = R("SP-15895").HasValue() ? R("SP-15895").Replace("<NULL>", "").Text : 
R("cnet_common_SP-15895").HasValue() ? R("cnet_common_SP-15895").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(LightingPowerTypeRef)){ 
switch(LightingPowerTypeRef){ 
// https://www.staples.com/pyle-home-pucvcbat48-handheld-cordless-cyclone-vacuum-cleaner/product_2677905 
case "Standard Voltage - Wired (120V)": 
result = "Wired vacuum cleaner with standard voltage"; 
break; 
case "Low Voltage - Wired (12V)": 
result = "Wired vacuum cleaner with low voltage"; 
break; 
// https://www.staples.com/Impress-GoVac-Handheld-Cordless-Rechargeable-Vacuum-Cleaner-With-Charging-Base-Black/product_1460677 
case "Battery Powered": 
result = "Battery powered vacuum cleaner"; 
break; 
// https://ifworlddesignguide.com/entry/131460-solar-powered-vacuum 
case "Solar Powered": 
result = "Solar powered vacuum cleaner"; 
break; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"VacuumsLightingPowerType⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Power-voltage 
void VacuumsPowerVoltage(){ 
var result = ""; 
var VoltageVoltsRef = R("SP-20650").HasValue() ? R("SP-20650").Replace("<NULL>", "").Text : 
R("cnet_common_SP-20650").HasValue() ? R("cnet_common_SP-20650").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(VoltageVoltsRef)){ 
result = $"Voltage: {VoltageVoltsRef}V"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"VacuumsPowerVoltage⸮{result}"); 
} 
} 

// --[FEATURE #6]
// --Cord length; if cordless state along with battery life
void VacuumsCordLength(){
    var CordLengthRef = R("SP-872").HasValue() ? R("SP-872") : R("cnet_common_SP-872");
    
    if(CordLengthRef.HasValue()){
        Add($"VacuumsCordLength⸮{CordLengthRef}' cord lets you clean large areas before having to stop and re-plug");
    }
}

// --[FEATURE #7] - All 
// --Certification & standards 

// --[FEATURE #8] Dimensions - All 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsMotorDetails(){ 
    var result = "";
    var Power_MaxPowerConsumption = A[3548]; // Power - Max Power Consumption
    var Power_RatedCurrent = A[4404]; // Power - Rated Current
    var Cleaning_MaximumMotorPower = A[4551]; // Cleaning - Maximum Motor Power
    
    if(Power_MaxPowerConsumption.HasValue()){
        result = $"{Power_MaxPowerConsumption.FirstValue().Replace(" ", "")}{Power_MaxPowerConsumption.FirstUnit()} motor offers outstanding cleaning performance";
    }
    else if(Power_RatedCurrent.HasValue()){
        result = $"{Power_RatedCurrent.FirstValue().Replace(" ", "")}{Power_RatedCurrent.FirstUnit()} motor offers outstanding cleaning performance";
    }
    else if(Cleaning_MaximumMotorPower.HasValue()){
        result = $"{Cleaning_MaximumMotorPower.FirstValue().Replace(" ", "")}{Cleaning_MaximumMotorPower.FirstUnit()} motor offers outstanding cleaning performance";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalVacuumsMotorDetails⸮{result}");
    }
} 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsBatteries(){ 
    var result = "";
    var Power_BatteryTechnology = A[3736]; // Power - Battery Technology
    var Power_PowerType = A[4610]; // Power - Power Type
    
    if(Power_PowerType.HasValue("%batteries%"))
    {
        // https://www.staples.com/shark-ionflex-cordless-ultra-light-vacuum-with-duoclean-technology-blue-refurbished-if203qbl/product_24374762
        var Technology = Power_BatteryTechnology.HasValue() ? $" {Power_BatteryTechnology.FirstValue().Replace("lithium ion", "lithium-ion").ToLower()}" : "";
        result = $"Battery type:{Technology} {Power_PowerType.Where("%batteries%").First().Value().ToLower()}";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalVacuumsBatteries⸮{result}");
    }
} 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsCordlessDesign(){ 
    var result = "";
    var Cleaning_Cordless = A[11123]; // Cleaning - Cordles
    
    if(Cleaning_Cordless.HasValue("yes"))
    {
        result = "Cordless design is perfect for comfortable use";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalVacuumsCordlessDesign⸮{result}");
    }
} 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsFingertipControls(){ 
var result = ""; 
var SettingsControlsIndicators_ControlsOnTheHandle = A[5635]; // Settings, Controls & Indicators - Controls on the Handle 
var Cleaning_Details = A[9934]; // Cleaning - Details 

if(SettingsControlsIndicators_ControlsOnTheHandle.HasValue("yes") 
|| Cleaning_Details.HasValue("fingertip controls")) 
{ 
result = "Controls are conveniently positioned right at your fingertips"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"AdditionalVacuumsFingertipControls⸮{result}"); 
} 
} 

// --[FEATURE #13] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsIndicators(){ 
    var result = "";
    var SettingsControlsIndicators_Indicators = A[4553]; // Settings, Controls & Indicators - Indicators
    
    if(SettingsControlsIndicators_Indicators.HasValue()){
        result = $"Incorporates {SettingsControlsIndicators_Indicators.Values.Select(s => s.Value()).FlattenWithAnd().ToLower()} for added assistance";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalVacuumsIndicators⸮{result}");
    }
} 

// --[FEATURE #14] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsSwivelSteering(){ 
var result = ""; 
var Cleaning_Details = A[9934]; // Cleaning - Details 

if(Cleaning_Details.HasValue("swivel steering")){ 
result = "Swivel steering is easy to maneuver around your home"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"AdditionalVacuumsSwivelSteering⸮{result}"); 
} 
} 

// --[FEATURE #15] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsBrushRoll(){ 
var result = ""; 
var Miscellaneous_IncludedTools = A[4554]; // Miscellaneous - Included Tools 
var Cleaning_Details = A[9934]; // Cleaning - Details 

if(Cleaning_Details.HasValue("motorized brush roll")){ 
result = Cleaning_Details.Where("motorized brush roll").First().Value().Replace("motorized brush roll", "Motorized brush roll allows you clean hard or carpeted floors"); 
} 
else if(Cleaning_Details.HasValue("brush roll shut-off")){ 
result = Cleaning_Details.Where("brush roll shut-off").First().Value().Replace("brush roll shut-off", "Brush roll shutoff offers superior carpet and bare floor cleaning"); 
} 
else if(Miscellaneous_IncludedTools.HasValue("motorized brush")){ 
result = "Brush roll allows you to clean hard or carpeted floors"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"AdditionalVacuumsBrushRoll⸮{result}"); 
} 
} 

// --[FEATURE #16] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsEdgeCreviceTool(){ 
var result = ""; 
var Miscellaneous_IncludedTools = A[4554]; // Miscellaneous - Included Tools 

if(Miscellaneous_IncludedTools.HasValue("crevice tool")){ 
result = "Crevice tool effectively cleans cracks as well as crevices with precision"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"AdditionalVacuumsEdgeCreviceTool⸮{result}"); 
} 
} 

// --[FEATURE #17] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsCyclonicFiltration(){ 
var result = ""; 
var Cleaning_CyclonicTechnology = A[9935]; // Cleaning - Cyclonic Technology 

if(Cleaning_CyclonicTechnology.HasValue("Yes")){ 
result = "Cyclonic filtration cuts through any mess with constant powerful suction"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"AdditionalVacuumsCyclonicFiltration⸮{result}"); 
} 
} 

// --[FEATURE #18] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsCarpetHeightAdjustments(){ 
    var result = "";
    var Cleaning_Details = A[9934]; // Cleaning - Details

    if(Cleaning_Details.HasValue("carpet height adjustments")){
        result = "Height adjustment offers a range of cleaning capabilities";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalVacuumsCarpetHeightAdjustments⸮{result}");
    }
} 

// --[FEATURE #19] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsEdgeCleaning(){ 
var result = ""; 
var Cleaning_Details = A[9934]; // Cleaning - Details 

if(Cleaning_Details.HasValue("edge cleaning")){ 
result = "Edge-cleaning capability makes it easy to clean along walls and in corners"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"AdditionalVacuumsEdgeCleaning⸮{result}"); 
} 
} 

// --[FEATURE #20] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalVacuumsPackageContent(){ 
    var result = "";
    var Miscellaneous_IncludedTools = A[4554]; // Miscellaneous - Included Tools
    
    if(Miscellaneous_IncludedTools.HasValue()){
        result = $"Package includes: {Miscellaneous_IncludedTools.Values.Select(s => s.Value()).FlattenWithAnd()}";
    }
    else if(SPEC["WIB"].GetLines().Select(s => s.Body).Any()
    && SPEC["WIB"].GetLines().Select(s => s.Body).Flatten(" ").NotIn("")){
        var wib = SPEC["WIB"].GetLines().Select(s => s.Body).Flatten(", ").Replace(" ¦", ",").ToString();
        wib = SPEC["WIB"].GetLines().Select(s => s.Body).Count() <= 2 ? wib.Replace(",", " and") :
        wib.Insert(wib.LastIndexOf(",") + 1, " and");
        result = $"Package includes: {wib.Replace(" x ", " ")}";
    }
    if(!string.IsNullOrEmpty(result)){
        Add($"AdditionalVacuumsPackageContent⸮{result}");
    }
} 

// --[FEATURE #21] - All 
// --Warranty information 

//1683392410972140733 "Vacuums END" "Serhii.O" §§ 

//§§530868304162051 "Chargers & Connectors BEGIN" "Serhii.O" 

ChargerTypeUse(); 
// Dimensions - All 
ChargersConnectorsConnections(); 
ChargersConnectorsCompatibility(); 
ChargersConnectorsAudioFeatureOrVoltage(); 
ChargersConnectorsVideoFeatureOrAdditionalChargerData(); 
// Pack Size - All 
// True Color - All
ChargersConnectorsKitContent(); 
ChargersConnectorsFastCharging(); 
ChargersConnectorsBraided(); 
ColorFamilyForTwoSKU(); 
// Warranty - All 

string CablesReplaceConn(List<AttributeValue> Values){ 
return Values.Select(s => s.Value().RegexReplace(@"(.*)\s3.5mm", "3.5mm $1").RegexReplace(@"(.*)?\s?(pin USB [Tt]ype)(.*)?\s?([Aa])\s?(.*)?", "##USB-##A").RegexReplace(@"(.*)?(pin USB [Tt]ype)(.*)?\s?([Bb])\s?(.*)?", "##USB-##B").RegexReplace(@"(.*)?(pin USB)([Tt]ype)?(.*)?\s?([Cc])\s?(.*)?", "##USB-##C").RegexReplace(@"(.*)?(pin USB)$", "##USB").RegexReplace(@"(.*)?([Mm]icro-USB)(.*)?", "##Micro ##USB").Replace("[Pp]ower", "power interface").Replace("Lightning", "##Lightning").Replace("Apple Dock","##Apple dock").Replace("4 pin mini-USB Type B","mini ##USB ##Type-##B")).FlattenWithAnd().RegexReplace(@"(\d+)", "##$1"); 
} 

// --[FEATURE #1] 
// --Charger type & Use 
void ChargerTypeUse(){ 
var result = ""; 
// Lightning to USB|Apple 30-Pin|Speed Dial Controller|Wall Charger|Portable Battery|Microphone Recorder|Headphone Splitter|Audio Cable|FM Transmitter|AV Cable|Battery Case|Charging Kit/Bundle|Charging Station|Adapter|USB Cable|Car Charger 
var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : ""; 
var CableDetails_Type = A[315]; // Cable Details - Type 
var PowerDevice_InputConnectorType = A[608]; // Power Device - Input Connector Type 
var PowerDevice_OutputConnectorType = A[610]; // Power Device - Output Connector Type 
var Cable_AVCableType = A[1045]; // Cable - AV Cable Type 
var Cable_InterfaceSupported = A[1046]; // Cable - Interface Supported 
var accType = A[7520]; // MI (Accessories) -- Product Type 


if(CellPhoneCableOrConnectorTypeRef.Equals("Car Charger") 
&& PowerDevice_InputConnectorType.HasValue() 
&& PowerDevice_InputConnectorType.Values.Flatten(" ").ToLower().In("automobile cigarette lighter") 
&& CableDetails_Type.HasValue("micro-USB cable")){ 
result = "Adapter plugs into your car's cigarette lighter jack to become a powered ##Micro ##USB connector"; 
} 
else if(CellPhoneCableOrConnectorTypeRef.Equals("Car Charger") 
&& PowerDevice_InputConnectorType.HasValue() 
&& PowerDevice_InputConnectorType.Values.Flatten(" ").ToLower().In("automobile cigarette lighter") 
&& PowerDevice_OutputConnectorType.HasValue() 
&& PowerDevice_OutputConnectorType.Values.Count == 1){ 
result = $"Adapter plugs into your car's cigarette lighter jack to become a powered {PowerDevice_OutputConnectorType.FirstValue().Replace("5 pin Micro-USB Type B", "##Micro-USB connector").Replace("Apple Lightning", "##Apple ##Lightning connector").Replace("4 pin USB Type A","##4-pin ##USB ##Type-##A")}"; 
} 
else if(CellPhoneCableOrConnectorTypeRef.Equals("Car Charger") 
&& PowerDevice_InputConnectorType.HasValue() 
&& PowerDevice_InputConnectorType.Values.Flatten(" ").ToLower().In("automobile cigarette lighter")){ 
result = "Adapter plugs into your car's cigarette lighter jack to become a powered connector"; 
} 
else if(CellPhoneCableOrConnectorTypeRef.Equals("Portable Battery") 
|| CellPhoneCableOrConnectorTypeRef.Equals("Power Bank")){ 
result = "Portable battery is a convenient way to charge most portable devices when there is no AC outlet"; 
} 
else if(CellPhoneCableOrConnectorTypeRef.Equals("Charging Station")){ 
result = "Charging Station allows multiple devices to charge simultaneously with ease"; 
} 
else if(CellPhoneCableOrConnectorTypeRef.Equals("Charging Kit/Bundle")){ 
result = "Charging kit/bundle"; 
} 
else if(CellPhoneCableOrConnectorTypeRef.Equals("USB Cable") 
&& Coalesce(Cable_AVCableType, Cable_InterfaceSupported).HasValue("%Lightning%")){ 
result = "Use ##Lightning to ##USB cable to charge and sync your iPhone or iPad at your desk"; 
} 
else if(CellPhoneCableOrConnectorTypeRef.Equals("USB Cable")){ 
var USBRef = CellPhoneCableOrConnectorTypeRef.Split(" ").ToList(); 
var USB = USBRef.Select(s => USBRef.IndexOf(s) == 0 ? s : s.ToLower()).Flatten(" "); 
result = $"Use ##{USB} to charge and sync your phone or tablet at your desk"; 
} 
else if(CellPhoneCableOrConnectorTypeRef.Equals("Adapter")){ 
result = "This adapter provides a safe way to charge your devices"; 
} 
else if(!String.IsNullOrEmpty(CellPhoneCableOrConnectorTypeRef)){ 
var USBRef = CellPhoneCableOrConnectorTypeRef.Split(" ").ToList(); 
var USB = USBRef.Select(s => USBRef.IndexOf(s) == 0 || s.Contains("USB") ? s : s.ToLower()).Flatten(" "); 
result = $"{USB} is reliable power solution to charge your devices when you need them most"; 
} 
if (accType.HasValue("connector plug protector")) { 
result = "Connector plug protector prevents cables from breakage"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"ChargerTypeUse⸮{result}"); 
} 
} 

// --[FEATURE #2] - All 
// --Cable Length and gauge -OR- Charger Dimensions 

// --[FEATURE #3] 
// --Connections 
void ChargersConnectorsConnections(){ 
    var result = "";
    var CellPhoneCableOrConnectorTypeRef = R("SP-18037").HasValue() ? R("SP-18037").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18037").HasValue() ? R("cnet_common_SP-18037").Replace("<NULL>", "").Text : "";
    var Cable_LeftConnectorType = A[588]; // Cable - Left Connector Type
    var Cable_RightConnectorType = A[589]; // Cable - Right Connector Type
    var Cable_RightConnectorGender = A[590]; // Cable - Right Connector Gender
    var Cable_LeftConnectorGender = A[591]; // Cable - Left Connector Gender
    var PowerDevice_InputConnectorType = A[608]; // Power Device - Input Connector Type
    var PowerDevice_OutputConnectorType = A[610]; // Power Device - Output Connector Type
    var PowerDevice_OutputConnectorQty = A[611]; // Power Device - Output Connector Qty
    var Cable_RightConnectorQty = A[768]; // Cable - Right Connector Qty
    var Cable_LeftConnectorQty = A[769]; // Cable - Left Connector Qty
    var PowerDevice_DetachableCableConnectorType = A[9713]; // Power Device - Detachable Cable Connector Type
    var DetachableCableConnectorType = "";
    
    if(SKU.ProductId.In("21064075")){
        result = "The charger provides one ##Lightning connector along with a ##USB port for an additional device";
    } 
    else if(Cable_LeftConnectorType.HasValue()
    && Cable_RightConnectorType.HasValue()
    && Cable_RightConnectorGender.HasValue()
    && Cable_LeftConnectorGender.HasValue()
    && Cable_RightConnectorQty.HasValue()
    && Cable_LeftConnectorQty.HasValue()){
        result = $"Connections: {Cable_LeftConnectorQty.FirstValue()} {Cable_LeftConnectorGender.FirstValue()} {CablesReplaceConn(Cable_LeftConnectorType.Values)}, {Cable_RightConnectorQty.FirstValue()} {Cable_RightConnectorGender.FirstValue()} {CablesReplaceConn(Cable_RightConnectorType.Values)}";
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("Portable Battery")
    || CellPhoneCableOrConnectorTypeRef.Equals("Power Bank")
    && PowerDevice_OutputConnectorType.HasValue()){
        if(PowerDevice_OutputConnectorQty.HasValue()){
           if(PowerDevice_OutputConnectorQty.FirstValue() > 1){
            result = $"{PowerDevice_OutputConnectorQty.FirstValue()} {CablesReplaceConn(PowerDevice_OutputConnectorType.Values)} ports can recharge {PowerDevice_OutputConnectorQty.FirstValue()} devices at once";
            } 
        }
    }
    else if(CellPhoneCableOrConnectorTypeRef.Equals("Portable Battery")
    || CellPhoneCableOrConnectorTypeRef.Equals("Power Bank")
    && PowerDevice_OutputConnectorType.HasValue()){
        var ConnectorQty = PowerDevice_OutputConnectorQty.HasValue() ? $" {PowerDevice_OutputConnectorQty.FirstValue()}" : "";
        result = $"Increases battery life on any{ConnectorQty} {CablesReplaceConn(PowerDevice_OutputConnectorType.Values)} compatible device";
    }
    else if(PowerDevice_InputConnectorType.HasValue()
    && PowerDevice_InputConnectorType.WhereNot("automobile cigarette lighter").Any()
    && PowerDevice_OutputConnectorType.HasValue()){
        DetachableCableConnectorType = PowerDevice_DetachableCableConnectorType.HasValue() ? $", and detachable {CablesReplaceConn(PowerDevice_DetachableCableConnectorType.Values)}" : "";
        result = $"Features {CablesReplaceConn(PowerDevice_InputConnectorType.WhereNot("automobile cigarette lighter").ToList())} on the first end and {CablesReplaceConn(PowerDevice_OutputConnectorType.Values)} on the second end connectors{DetachableCableConnectorType}";
    }
    else if(PowerDevice_InputConnectorType.HasValue()
    && PowerDevice_InputConnectorType.Where("automobile cigarette lighter").Any()
    && PowerDevice_OutputConnectorType.HasValue()){
        DetachableCableConnectorType = PowerDevice_DetachableCableConnectorType.HasValue() ? $" and detachable {CablesReplaceConn(PowerDevice_DetachableCableConnectorType.Values)}" : "";
        var res = CablesReplaceConn(PowerDevice_OutputConnectorType.Values);
        result = Coalesce(res).ExtractNumbers().Any() && Coalesce(res).ExtractNumbers().First() > 1 ? $"The charger provides {res} connectors{DetachableCableConnectorType}" : $"The charger provides {res} connectors{DetachableCableConnectorType}";
    }
    if(!String.IsNullOrEmpty(result)){
	    Add($"ChargersConnectorsConnections⸮{result}");
	} 
} 

// --[FEATURE #4] 
// --Compatibility 
void ChargersConnectorsCompatibility(){ 
var result = ""; 
// Universal|Nokia|Motorola|LG|Amazon Fire|Samsung Galaxy S2|iPhone 6 Plus/6s Plus/7 Plus/8 Plus|iPhone 6/6S|Blackberry|Google Nexus|iPhone 7/8|Google Pixel 2|iPhone 6/6S/7/8|Samsung Galaxy Note 8|iPhone 5/5s/SE|iPhone 8 Plus|iPhone X|Samsung Galaxy Note 4|iPhone 6 Plus/6s Plus|Galaxy S8|ZTE|Mini Mobile|Most Smartphones|Samsung Galaxy Note 3|Samsung Galaxy Note 2|HTC M9|Samsung Galaxy S6|iPhone 6s Plus|6" Cell Phone|iPhone 6s|5" Cell Phone|iPhone 7 Plus /iPhone 8 Plus|iPhone 7/ iPhone 8|iPhone 3G/3GS|HTC Desire 526|HTC Desire 510|HTC 8Xt|Electrify/Mb855/Photon 4G|iPhone 5/5S/5C/6/6 Plus|HTC Evo 4G Lte|HTC Desire 626/626S|HTC Desire 626|HTC Desire 601/Zara|HTC One M9|iPhone/iPad/iPod Touch|HTC One M8|Utstarcom|Samsung Galaxy S 8+|Cisco|Samsung Galaxy S 8|Sanyo E4100Taho|Samsung Galaxy 8|HTC M7|Huawei|Coolpad|HTC Windows Phone 8X/Htc Zenith|HTC One/M7|PCD|iPhone 4/4S/5 & iPod Touch|iPhone Se|HTC One Vx|iPhone 7 Plus|iPhone XS|iPhone 8|iPhone XS Max|myTouch|HTC One X|Samsung Galaxy S9+|iPhone 6 Plus|Microsoft|Apple iPhone X, Xs|iPhone 7/6s/6|Samsung D710, R760, Galaxy S II 4G|Samsung D700|Microsoft Lumia 640|Microsoft Lumia|iPhone 5 & Newer|Samsung Galaxy S9|Samsung Galaxy Note|Pantech|Samsung (Other)|HP|BLU|iPhone 3G/3GS|Dell|Palm|Kyocera|Sony|Alcatel|Samsung Galaxy S5|Samsung Galaxy S4|Samsung Galaxy S3|iPhone 6|Samsung R360|iPhone 4/4s|CASIOC811|iPhone 7|Samsung Galaxy S6 Edge|Samsung M580|iPhone 5/5s|Alcatel 7024W|Samsung Intensity III|iPhone 5c|Samsung i9260|Alcatel 5020T|Samsung S390G/T189N|Samsung R830 Galaxy Axiom|HTC|Samsung R480|Samsung Galaxy S7|Samsung Galaxy S4 Mini|Samsung Galaxy Note 5|Google Pixel 2 XL|All iPhones|Multiple Brands 
var CellPhoneCompatibilityRef = R("SP-18043").HasValue() ? R("SP-18043").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18043").HasValue() ? R("cnet_common_SP-18043").Replace("<NULL>", "").Text : ""; 
var CompatibleProducts = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body, SPEC["MS"].GetLine("Designed For").Body, SPEC["ES"].GetLine("Designed For").Body); 

if(CellPhoneCompatibilityRef.Equals("iPhone/iPad/iPod Touch")){ 
result = "Compatible with iPod/iPhone/iPad"; 
} 
else if(!string.IsNullOrEmpty(CellPhoneCompatibilityRef)){ 
result = "Compatible with most smartphones"; 
} 
else if(CompatibleProducts.HasValue()){ 
result = Shorten($"Charges {CompatibleProducts.Text.Replace(";", ",").Split(", ").Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).FlattenWithAnd().RegexReplace(@"(-in\w+)", @"""").RegexReplace(@"\(.*?\)", "").Replace("lightning", "Lightning")}", 250, ','); 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"ChargersConnectorsCompatibility⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Audio feature (If Applicable) -OR- Voltage 
void ChargersConnectorsAudioFeatureOrVoltage(){ 
var result = ""; 
var InputPowerRef = R("SP-4068").HasValue() ? R("SP-4068").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4068").HasValue() ? R("cnet_common_SP-4068").Replace("<NULL>", "").Text : ""; 
var OutputPowerRef = R("SP-13576").HasValue() ? R("SP-13576").Replace("<NULL>", "").Text : 
R("cnet_common_SP-13576").HasValue() ? R("cnet_common_SP-13576").Replace("<NULL>", "").Text : ""; 
var PowerDevice_VoltageProvided = A[387]; // Power Device - Voltage Provided 
var PowerDevice_MaxElectricCurrent = A[1883]; // Power Device - Max Electric Current 

var InputPower = !String.IsNullOrEmpty(InputPowerRef) ? $"{InputPowerRef.Replace("12 - 24", "12-24")} (input)" : ""; 
var OutputPower = !String.IsNullOrEmpty(InputPowerRef) && PowerDevice_VoltageProvided.HasValue() ? $", {PowerDevice_VoltageProvided.FirstValue()}{PowerDevice_VoltageProvided.Units.First().Name} (output)" : PowerDevice_VoltageProvided.HasValue() ? $"{PowerDevice_VoltageProvided.FirstValue()}{PowerDevice_VoltageProvided.Units.First().Name} (output)" : ""; 
var MaxElectricCurrent = PowerDevice_MaxElectricCurrent.HasValue() && PowerDevice_VoltageProvided.HasValue() ? $"; max current: {PowerDevice_MaxElectricCurrent.FirstValue()}{PowerDevice_MaxElectricCurrent.Units.First().Name}" : ""; 
var VoltageProvided = (!String.IsNullOrEmpty(InputPowerRef) || PowerDevice_VoltageProvided.HasValue() || (PowerDevice_MaxElectricCurrent.HasValue() && PowerDevice_VoltageProvided.HasValue())) 
&& !String.IsNullOrEmpty(OutputPowerRef) ? $"; power: {OutputPowerRef.Replace(" Watt", "W")}" : ""; 
var power = $"{InputPower}{OutputPower}{MaxElectricCurrent}{VoltageProvided}"; 

if(!String.IsNullOrEmpty(power)){ 
result = $"Voltage: {power}"; 
} 
else if(!String.IsNullOrEmpty(OutputPowerRef)){ 
result = $"Power: {OutputPowerRef.Replace(" Watt", "W")}"; 
} 
else{ 
var current = PowerDevice_MaxElectricCurrent.HasValue() ? $"Max output current: {PowerDevice_MaxElectricCurrent.FirstValue()}{PowerDevice_MaxElectricCurrent.Units.First().Name}" : ""; 
result = $"{current}"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"ChargersConnectorsAudioFeatureOrVoltage⸮{result}"); 
} 
} 

string[] ChargerData(string feat){
    var result = "";
    var CellPhoneCompatibilityRef = GetReference("SP-18043");
    var PowerDevice_Features = A[2685];
    var General_ProductType = A[4659];
    
    if(PowerDevice_Features.HasValue()){
        if(PowerDevice_Features.WhereNot("Built-in LED torch", "Integrated Apple Lightning cable").Any()){
            switch(PowerDevice_Features.WhereNot("Built-in LED torch", "Integrated Apple Lightning cable", $"%{feat}%").Select(s => s.Value().ToString())){
                case var a when a.Contains("on/off button")
                && General_ProductType.HasValue("power bank"):
                feat = "on/off button";
                result = "On/Off switch helps conserve battery life";
                break;
                case var a when a.Contains("folding plugs"):
                feat = "folding plugs";
                result = "Features travel-friendly design with a folding plug";
                break;
                case var a when a.Contains("LED indicator")
                || a.Contains("LED Indicator"):
                feat = "LED indicator";
                result = "LED charging indicator shows charging status";
                break;
                case var a when a.Contains("rapid charging")
                || a.Contains("fast charging"):
                feat = a.Contains("rapid charging") ? "rapid charging" : a.Contains("fast charging") ? "fast charging" : "";
                result = $"Features a USB fast charger for rapid charging of {CellPhoneCompatibilityRef.ToLower(true)}";
                break;
                case var a when a.Contains("coiled"):
                feat = "coiled";
                result = "Coiled cable reduces clutter";
                break;
                case var a when a.Contains("over-current protection"):
                feat = "over-current protection";
                result = "Features built-in over-current protection";
                break;
            }
        }
    }
    return new string[]{result, feat};
}

// --[FEATURE #6] 
// --Video feature (If Applicable) -OR- Additional charger data (If Applicable) 
// --[FEATURE #7] 
// --Data feature (If Applicable) -OR- Additional charger data (If Applicable) 
void ChargersConnectorsVideoFeatureOrAdditionalChargerData(){ 
var resultList = ChargerData("olegOleguch"); 
if(!String.IsNullOrEmpty(resultList[0])){ 
Add($"ChargersConnectorsVideoFeatureOrAdditionalChargerData⸮{resultList[0]}"); 
} 
resultList = ChargerData(resultList[1]); 
if(!String.IsNullOrEmpty(resultList[0])){ 
Add($"ChargersConnectorsDataFeatureOrAdditionalChargerData⸮{resultList[0]}"); 
} 
} 

// --[FEATURE #8] - All 
// --Pack Size (If more than 1) 

// --[FEATURE #9] - All
// --True Color
// --Use for additional product and/or manufacturer information relevant to the customer buying decision

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void ChargersConnectorsKitContent(){ 
    var general_KitContent = A[11130];  
    
    if(general_KitContent.HasValue()){
        Add($"ChargersConnectorsKitContent⸮Includes: {general_KitContent.Values.Select(d => d.Value().ToLower()).FlattenWithAnd()}");
    }
} 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void ChargersConnectorsFastCharging(){ 
    var cellPhoneCableOrConnectorTypeRef = GetReference("SP-18037");
	var PowerDevice_MaxElectricCurrent = A[1883];
	
	if(new[]{ "Portable Battery", "Portable Battery" }.Where(s => s == cellPhoneCableOrConnectorTypeRef).Any()
	&& PowerDevice_MaxElectricCurrent.HasValue()
	&& PowerDevice_MaxElectricCurrent.FirstValue() > 2.3
	&& PowerDevice_MaxElectricCurrent.Units.First().Name.In("A")){
	    Add($"ChargersConnectorsFastCharging⸮{PowerDevice_MaxElectricCurrent.FirstValue()}A power output for fast charging");
	}
} 

// --[FEATURE #12]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void ChargersConnectorsBraided(){
    var cable_AdditionalFeatures = A[1549];
    
    if(cable_AdditionalFeatures.HasValue("%braided%")){
        Add($"ChargersConnectorsBraided⸮Durable braided cable is made to handle everyday use");
    }
}

void ColorFamilyForTwoSKU() {
    if (SKU.ProductId.In("21863912", "21863909")) {
        Add("ColorFamilyForTwoSKU⸮Comes in multicolor");
    }
}

// --[FEATURE #13] - All
// --Warranty

//530868304162051 "Chargers & Connectors END" "Serhii.O" §§ 

//§§11012078166382 "Dry Erase Whiteboards BEGIN" "Serhii.O" 

DryEraseWhiteboardTypeUse(); 
WhiteboardsOverallDimensions(); 
WhiteboardsSurfaceMaterial(); 
WhiteboardsFrameConstruction(); 
WhiteboardsDesign(); 
WhiteboardsAssemblyInformation(); 
WhiteboardsPackageContents(); 
AdditionalWhiteboardsStainResistant(); 
AdditionalWhiteboardsBoardDesign(); 
AdditionalWhiteboardsMarkerTrayHolds(); 
AdditionalWhiteboardsMarkerGhostingResistant(); 
AdditionalWhiteboardsMagnetic(); 
// Warranty - All 

// --[FEATURE #1] 
// --Dry erase whiteboard type & use 
void DryEraseWhiteboardTypeUse(){ 
var result = ""; 
// Moderate Use|Light Use|Heavy Use 
var BoardUsageRef = ""; 
var ProductType = Coalesce(A[6050], A[6080], A[6173]); // Manufacturer's Product Type 

if(!String.IsNullOrEmpty(BoardUsageRef)){ 
switch(BoardUsageRef){ 
case "Light Use": 
result = "Suitable for light use in personal or low-traffic environments"; 
break; 
case "Moderate Use": 
result = "Best suited for moderate use in spaces with regular traffic"; 
break; 
case "Heavy Use": 
result = "Ideal for heavy use in spaces with moderate traffic"; 
break; 
} 
} 
// https://www.staples.com/Quartet-Arc-Cubicle-Whiteboard-14-x-11-Magnetic-Aluminum-Frame-ARC1411/product_689623 
else if(ProductType.HasValue()){ 
result = $"Stay organized and share ideas with this {ProductType.FirstValue().ToLower()}"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"DryEraseWhiteboardTypeUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Overall Dimensions: H x W 
void WhiteboardsOverallDimensions(){ 
var result = ""; 
var WidthRef = R("SP-21044").HasValue() ? R("SP-21044").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21044").HasValue() ? R("cnet_common_SP-21044").Replace("<NULL>", "").Text : ""; 
var HeightRef = R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : ""; 

if(!String.IsNullOrEmpty(HeightRef) 
&& !String.IsNullOrEmpty(WidthRef)){ 
result = $@"{HeightRef}""H x {WidthRef}""W board size"; 
} 
else if(!String.IsNullOrEmpty(HeightRef)){ 
result = $@"{HeightRef}""H board size"; 
} 
else if(!String.IsNullOrEmpty(WidthRef)){ 
result = $@"{WidthRef}""W board size"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"WhiteboardsOverallDimensions⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Surface material 
void WhiteboardsSurfaceMaterial(){ 
var result = ""; 
// Film|Nitrile|Paper|Metal|Resin|Wire Mesh|Steel|Vinyl|Rubber|Silicone|Plastic|Polyvinyl Chloride|Lacquered Steel|Tin|Painted Steel|Electro-Adhesion|Plexiglas|Enamel|Cork|Total Erase|Wood|Porcelain|Graphite|Stainless Steel|Magnetic|Mesh|Plastic|Glass|Melamine|Film / Adhesive|Styrene|Polypropylene|Dry-Erase Paint|Laminate|Fabric|Cork & Dry Erase|Rubber-Tak|Combination Board|Polyethylene|Dry-Erase|Wrought Iron|Poster|Cardstock|Wood|Foam|Wood/Veneer|Corrugated|Polystyrene|Granite 
var SurfaceMaterialRef = R("SP-17454").HasValue() ? R("SP-17454").Replace("<NULL>", "").Text : 
R("cnet_common_SP-17454").HasValue() ? R("cnet_common_SP-17454").Replace("<NULL>", "").Text : ""; 
var BoardEasel_SurfaceMaterial = A[6090]; // Board / Easel - Surface Material 

if(!String.IsNullOrEmpty(SurfaceMaterialRef)){ 
switch(SurfaceMaterialRef){ 
case var a when a.Equals("Combination Board") 
&& BoardEasel_SurfaceMaterial.HasValue(): 
result = $"Combination board includes {BoardEasel_SurfaceMaterial}"; 
break; 
case "Polyvinyl Chloride": 
result = "Board features economical PVC coated surface for long lasting durability"; 
break; 
case "Stainless Steel": 
result = "Dry erase board with stainless steel surface"; 
break; 
case "Film / Adhesive": 
result = "Adhesive material makes installation easy"; 
break; 
case "Cork & Dry Erase": 
result = "Combo dry-erase and cork bulletin board offers a variety of messaging options"; 
break; 
case "Total Erase": 
result = "Total erase board surface resists staining, ghosting, scratching and denting"; 
break; 
case "Laminate": 
result = "The dry erase surface is laminated to provide additional durability for use"; 
break; 
case "Porcelain": 
result = "High-quality porcelain on steel surface for crisp, clear messages"; 
break; 
case "Melamine": 
result = "Melamine surface is smooth and easy to clean"; 
break; 
case "Glass": 
result = "Glass is easy to keep clean"; 
break; 
case "Film": 
result = "Dry-erase board has a smooth film surface for writing and erasing"; 
break; 
case "Steel": 
result = "Steel surface to stick magnetic items onto the whiteboard surface"; 
break; 
case "Plastic": 
result = "Made of durable plastic"; 
break; 
case "Cork": 
result = "Combo dry-erase and cork bulletin board offers a variety of messaging options"; 
break; 
default: 
result = $"Surface material: {SurfaceMaterialRef.ToLower()}"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"WhiteboardsSurfaceMaterial⸮{result}"); 
} 
} 

// --[FEATURE #4] 
// --Frame Construction; material, properties & mechanism 
void WhiteboardsFrameConstruction(){ 
var result = ""; 
var BoardEasel_FrameMaterial = A[6098]; // Board / Easel - Frame Material 
var BoardEasel_FrameColor = A[6099]; // Board / Easel - Frame Color 

if(BoardEasel_FrameMaterial.HasValue("%translucent%") 
|| BoardEasel_FrameColor.HasValue("%translucent%")){ 
result = "Translucent frame provides a full-size writing surface"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%steel%") 
&& BoardEasel_FrameColor.HasValue() 
&& BoardEasel_FrameColor.FirstValue().ToString().Split(" ").Where(s => s.Equals("black") || s.Equals("gray") || s.Equals("silver") || s.Equals("graphite")).Any()){ 
result = $"{BoardEasel_FrameColor.FirstValue().ToString().Split(" ").Where(s => s.Equals("black") || s.Equals("gray") || s.Equals("silver") || s.Equals("graphite")).First().ToUpperFirstChar()} {BoardEasel_FrameMaterial.FirstValue()} frame for durability"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%plastic%")){ 
result = "Plastic frame for durability"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%aluminum%") 
&& BoardEasel_FrameColor.HasValue() 
&& BoardEasel_FrameColor.FirstValue().ToString().Split(" ").Where(s => s.Equals("black") || s.Equals("gray") || s.Equals("silver") || s.Equals("graphite")).Any()){ 
result = $"{BoardEasel_FrameColor.FirstValue().ToString().Split(" ").Where(s => s.Equals("black") || s.Equals("gray") || s.Equals("silver") || s.Equals("graphite")).First().ToUpperFirstChar()} {BoardEasel_FrameMaterial.FirstValue()} frame"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%aluminum%")){ 
result = $"{BoardEasel_FrameMaterial.FirstValue().ToUpperFirstChar()} frame"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%mahogany%") 
|| BoardEasel_FrameColor.HasValue("%mahogany%")){ 
result = "Mahogany finish frame"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%wood%")){ 
result = "Sturdy wood decor frame perfect for home, office, classroom, or commercial use"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%oak%") 
|| BoardEasel_FrameColor.HasValue("%oak%")){ 
result = "Premium frame is oak with a natural finish"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%steel%")){ 
result = $"{BoardEasel_FrameMaterial.FirstValue().ToUpperFirstChar()} frame for durability"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%MDF%")){ 
result = "The MDF frame is durable for frequent and long-term use"; 
} 
else if(!BoardEasel_FrameMaterial.HasValue() 
&& BoardEasel_FrameColor.HasValue()){ 
result = $"{BoardEasel_FrameColor.FirstValue().Replace("frame", "").ToUpperFirstChar()} finish frame for durability"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"WhiteboardsFrameConstruction⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Design; include integrated features 
void WhiteboardsDesign(){ 
var result = ""; 
// Eyelet tie down|Rolling|Free Standing|Wall Mounted|Ceiling Hung|Adhesive Mounted 
var BoardOrSignDesignRef = R("SP-22953").HasValue() ? R("SP-22953").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22953").HasValue() ? R("cnet_common_SP-22953").Replace("<NULL>", "").Text : ""; 
// Lined|Blank|List/Planning|Multi-Surface|Calendar|Grid 
var SurfaceDesignRef = R("SP-22964").HasValue() ? R("SP-22964").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22964").HasValue() ? R("cnet_common_SP-22964").Replace("<NULL>", "").Text : ""; 

if(BoardOrSignDesignRef.Equals("Eyelet tie down") 
&& !String.IsNullOrEmpty(SurfaceDesignRef)){ 
result = $"{SurfaceDesignRef.ToLower().ToUpperFirstChar()} boards come eyelet tie down mounting"; 
} 
else if(BoardOrSignDesignRef.Equals("Eyelet tie down")){ 
result = "Boards come with eyelet tie down mounting"; 
} 
else if(BoardOrSignDesignRef.Equals("Adhesive Mounted") 
&& !String.IsNullOrEmpty(SurfaceDesignRef)){ 
result = $"{SurfaceDesignRef.ToLower().ToUpperFirstChar()} boards come with adhesive mounting"; 
} 
else if(BoardOrSignDesignRef.Equals("Adhesive Mounted")){ 
result = "Boards come with adhesive mounting"; 
} 
else if(BoardOrSignDesignRef.Equals("Ceiling Hung") 
&& !String.IsNullOrEmpty(SurfaceDesignRef)){ 
result = $"{SurfaceDesignRef.ToLower().ToUpperFirstChar()} boards come ceiling hung mounting"; 
} 
else if(BoardOrSignDesignRef.Equals("Ceiling Hung")){ 
result = "Boards come with ceiling hung mounting"; 
} 
else if(BoardOrSignDesignRef.Equals("Free Standing") 
&& !String.IsNullOrEmpty(SurfaceDesignRef)){ 
result = $"{SurfaceDesignRef.ToLower().ToUpperFirstChar()} Free-standing board"; 
} 
else if(BoardOrSignDesignRef.Equals("Free Standing")){ 
result = "Free-standing board"; 
} 
else if(BoardOrSignDesignRef.Equals("Wall Mounted") 
&& !String.IsNullOrEmpty(SurfaceDesignRef)){ 
result = $"{SurfaceDesignRef.ToLower().ToUpperFirstChar()} board is perfect for wall-mounting"; 
} 
else if(BoardOrSignDesignRef.Equals("Wall Mounted")){ 
result = "Wall mount makes it easy to write on the board for extended periods"; 
} 
else if(BoardOrSignDesignRef.Equals("Rolling") 
&& !String.IsNullOrEmpty(SurfaceDesignRef)){ 
result = $"{SurfaceDesignRef.ToLower().ToUpperFirstChar()} board features rolling caster that let you move it from room to room"; 
} 
else if(BoardOrSignDesignRef.Equals("Rolling")){ 
result = "Rolling casters make it easy move around a room"; 
} 
else if(!String.IsNullOrEmpty(SurfaceDesignRef)){ 
switch(SurfaceDesignRef){ 
case "Calendar": 
result = "Calendar board helps to get organized"; 
break; 
case "List/Planning": 
result = "List/planning board lets you plan ahead and stay on track"; 
break; 
case "Multi-Surface": 
result = "Jot down reminders and post notices with this multi-surface whiteboard"; 
break; 
case "Lined": 
result = "Lined to practice writing block letters or cursive"; 
break; 
case "Blank": 
result = "Blank for drawing or other problem solving"; 
break; 
case "Grid": 
result = "Grid lines make planning easy and professional looking"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"WhiteboardsDesign⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Assembly information 
void WhiteboardsAssemblyInformation(){ 
    var result = ""; 
    var General_AssemblyRequired = Coalesce(A[7254], A[7114]); // General - Assembly Required 

    if(General_AssemblyRequired.HasValue("Yes")){ 
        result = "Assembly required"; 
    } 
    else if(General_AssemblyRequired.HasValue("No")){ 
        result = "Comes fully assembled for immediate use"; 
    } 
    if(!String.IsNullOrEmpty(result)){ 
        Add($"WhiteboardsAssemblyInformation⸮{result}"); 
    } 
} 

// --[FEATURE #7] 
// --Package contents 
void WhiteboardsPackageContents(){ 
var result = ""; 
var Miscellaneous_IncludedAccessories = A[6108]; // Miscellaneous - Included Accessories 
if(Miscellaneous_IncludedAccessories.HasValue()){ 
result = $"Package includes: {Miscellaneous_IncludedAccessories.Values.Select(s => s.Value()).FlattenWithAnd().Replace("3 year", "3-year").Replace("dry erase", "dry-erase").Replace("(%)", "")}"; 
} 
else if(SPEC["WIB"].GetLines().Select(s => s.Body).Any()){ 
var box = SPEC["WIB"].GetLines().Select(s => s.Body).FlattenWithAnd().Replace("dry erase", "dry-erase").Replace("12 x whiteboard", "12 whiteboards").Replace("(%)", "").ToLower(); 
result = !String.IsNullOrEmpty(box) ? $"Package includes: {box}" : ""; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"WhiteboardsPackageContents⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalWhiteboardsStainResistant(){ 
var result = ""; 
var General_Features = A[6087]; // General - Features 

if(General_Features.HasValue() 
&& General_Features.WhereNot("%stainless%").Where(s => s.HasValue("%stain%")).Any()){ 
result = "Board surface won't stain and is super easy to clean"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"AdditionalWhiteboardsStainResistant⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalWhiteboardsBoardDesign(){ 
var result = ""; 
var General_Features = A[6087]; // General - Features 
var BoardEasel_CombinationBoardType = A[6088]; // Board / Easel - Combination Board Type 
var BoardEasel_FrameMaterial = A[6098]; // Board / Easel - Frame Material 
// Moderate Use|Light Use|Heavy Use 
var BoardUsageRef = R("SP-12797").HasValue() ? R("SP-12797").Replace("<NULL>", "").Text : 
R("cnet_common_SP-12797").HasValue() ? R("cnet_common_SP-12797").Replace("<NULL>", "").Text : ""; 

if(General_Features.HasValue("%frameless%")){ 
result = "Frameless board design blends seamlessly into any environment"; 
} 
else if(BoardEasel_FrameMaterial.HasValue("%wood%")){ 
result = "Solid wood frames are designed to coordinate with wood furniture"; 
} 
else if(BoardUsageRef.ToLower().Contains("light")){ 
result = "Designed for home and office use"; 
} 
else if(BoardEasel_CombinationBoardType.HasValue() 
&& BoardEasel_CombinationBoardType.Values.Count() >= 2){ 
result = $"Combination design adds value by providing {BoardEasel_CombinationBoardType.Values.Count().ToString().Replace("2", "two").Replace("3", "three").Replace("4", "four").Replace("5", "five").Replace("6", "six").Replace("7", "seven")} boards in one"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"AdditionalWhiteboardsBoardDesign⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalWhiteboardsMarkerTrayHolds(){ 
var result = ""; 
var General_Features_IncludedAccessories = Coalesce(A[6087], A[6108]); // General - Features | Miscellaneous - Included Accessories 

if(General_Features_IncludedAccessories.HasValue("%marker%tray%")){ 
result = "Marker tray holds accessories for convenient use"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"AdditionalWhiteboardsMarkerTrayHolds⸮{result}"); 
} 
} 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalWhiteboardsMarkerGhostingResistant(){ 
var result = ""; 
var General_Features = A[6087]; // General - Features 

if(General_Features.HasValue("%ghost%")){ 
result = "Write-and-wipe surface will not ghost to prevent it from dulling"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"AdditionalWhiteboardsMarkerTrayHolds⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void AdditionalWhiteboardsMagnetic(){ 
var result = ""; 
// No|Yes 
var MagneticRef = R("SP-12596").HasValue() ? R("SP-12596").Replace("<NULL>", "").Text : 
R("cnet_common_SP-12596").HasValue() ? R("cnet_common_SP-12596").Replace("<NULL>", "").Text : ""; 

if(MagneticRef.Equals("Yes")){ 
result = "Surface accepts magnetic accessories"; 
} 
else if(MagneticRef.Equals("No")){ 
result = "Non-magnetic whiteboards offer excellent writing and erasing qualities"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"AdditionalWhiteboardsMagnetic⸮{result}"); 
} 
} 

// --[FEATURE #13] - All 
// --Warranty 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 

//11012078166382 "Dry Erase Whiteboards END" "Serhii.O" §§ 

//§§5435445243161518, 5435445243142044, 5435445243167883, 543544524340300 "Printers BEGIN" "Serhii.O" 

PrintersTypeOfPrinterUse(); 
PrintersPrintTechnologyResolution(); 
PrintersPrintSpeedMode(); 
PrintersConnectivity(); 
// Dimensions - All 
PrintersInputLoadingFeature(); 
PrintersProcessorMemory(); 
AutomaticPrintingFeatures(); 
PrintersDisplay(); 
PrintersScanningCapabilities(); 
// Standards - All 
LargeFormatPrinter(); 
// Warranty Information - All 

// --[FEATURE #1] 
// --Type of Printer & Use 
void PrintersTypeOfPrinterUse(){ 
var result = ""; 
// Single-Function|All-in-One| 
var TypeOfPrinterRef = R("SP-13860").HasValue() ? R("SP-13860").Replace("<NULL>", "").Text : 
R("cnet_common_SP-13860").HasValue() ? R("cnet_common_SP-13860").Replace("<NULL>", "").Text : ""; 

if(!String.IsNullOrEmpty(TypeOfPrinterRef)){ 
switch(TypeOfPrinterRef){ 
case "All-in-One": 
result = "All-in-one printer gives you printing, copying, and scanning capability"; 
break; 
case "Single-Function": 
result = "This single-function printer focuses exclusively on printing, so it is easy to use"; 
break; 
case "Wide/Large Format": 
result = "Wide and large format printer to accommodate a range of printing projects"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"PrintersTypeOfPrinterUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Print Technology & Resolution 
void PrintersPrintTechnologyResolution(){ 
var result = ""; 
// Dot Matrix|Borderless|Laser|Inkjet 
var PrintTechnologyRef = R("SP-13581").HasValue() ? R("SP-13581").Replace("<NULL>", "").Text : 
R("cnet_common_SP-13581").HasValue() ? R("cnet_common_SP-13581").Replace("<NULL>", "").Text : ""; 
var Printing_MaxResolutionBW_Color = Coalesce(A[2758], A[2759]); // Printing - Max Resolution B/W | Max Resolution Color 

if(!String.IsNullOrEmpty(PrintTechnologyRef)){ 
switch(PrintTechnologyRef){ 
case var a when a.Equals("Laser"): 
result = Printing_MaxResolutionBW_Color.HasValue() ? 
$"Laser printing with up to {Printing_MaxResolutionBW_Color.FirstValue()} resolution ensures detailed, high-quality prints" : 
"Invest in a laser printer that will keep your business running smoothly"; 
break; 
case var a when a.Equals("Inkjet"): 
result = Printing_MaxResolutionBW_Color.HasValue() ? 
$"Inkjet printer has a resolution quality that goes up to {Printing_MaxResolutionBW_Color.FirstValue()} for excellent readability" : 
"Simplify your office printing solutions with this inkjet printer"; 
break; 
case var a when a.Equals("Dot Matrix"): 
result = Printing_MaxResolutionBW_Color.HasValue() ? 
$"Dot matrix printer creates documents up to {Printing_MaxResolutionBW_Color.FirstValue()} for sharp resolution" : 
"Simplify your office printing solutions with this dot matrix printer"; 
break; 
case var a when a.Equals("Borderless"): 
result = Printing_MaxResolutionBW_Color.HasValue() ? 
$"Borderless printer creates documents up to {Printing_MaxResolutionBW_Color.FirstValue()} for sharp resolution" : 
"Simplify your office printing solutions with this borderless printer"; 
break; 
case var a when a.Equals("PageWide"): 
result = Printing_MaxResolutionBW_Color.HasValue() ? 
$"PageWide printer has a resolution quality that goes up to {Printing_MaxResolutionBW_Color.FirstValue()} for excellent readability" : 
"Simplify your office printing solutions with this PageWide"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"PrintersPrintTechnologyResolution⸮{result}"); 
} 
} 

// --[FEATURE #3]
// --Print Speed & Mode (simplex/duplex), Output Color & Monthly Duty Cycle
void PrintersPrintSpeedMode(){ 
    var result = "";
	var MaximumPrinterMonthlyDutyCycleRef = R("SP-4540").HasValue() ? R("SP-4540").Replace("<NULL>", "").Text :
		R("cnet_common_SP-4540").HasValue() ? R("cnet_common_SP-4540").Replace("<NULL>", "").Text : "";
	var OfficeMachine_MonthlyDutyCycle = A[2694]; // Office Machine - Monthly Duty Cycle (max)
	var Printing_MaxPrintingSpeed = A[2760]; // Printing - Max Printing Speed B/W (ppm)
	var Printing_MaxPrintingSpeedColor = A[2761]; // Printing - Max Printing Speed Color (ppm)
	var Printing_MaxPrintingSpeed2 = A[5435]; // Printing - Max Printing Speed B/W
	var Printing_MaxPrintingSpeedColor2 = A[5436]; // Printing - Max Printing Speed Color
	var DutyCycle = OfficeMachine_MonthlyDutyCycle.HasValue() ? $" with a monthly duty cycle of {OfficeMachine_MonthlyDutyCycle.FirstValue()} {OfficeMachine_MonthlyDutyCycle.Units.First().Name.Replace("page", "pages").Replace("sheet", "sheets").Replace("pagess", "pages").Replace("sheetss", "sheets")}" : "";

	if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed.HasValue()
	&& Printing_MaxPrintingSpeedColor.HasValue()
	&& Printing_MaxPrintingSpeed.HasValue(Printing_MaxPrintingSpeedColor.FirstValue().ToString())){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()} ppm for color and black/white images{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed.HasValue()
	&& Printing_MaxPrintingSpeedColor.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()} ppm in black/white and ##{Printing_MaxPrintingSpeedColor.FirstValue()}ppm in color{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()} ppm in black/white{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeedColor.HasValue()){
		result = $"##{Printing_MaxPrintingSpeedColor.FirstValue()} ppm in color{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed2.HasValue()
	&& Printing_MaxPrintingSpeedColor2.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed2.FirstValue()} {Printing_MaxPrintingSpeed2.Units.First().Name} in black/white and ##{Printing_MaxPrintingSpeedColor2.FirstValue()} {Printing_MaxPrintingSpeedColor2.Units.First().Name} in color{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeed2.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed2.FirstValue()} {Printing_MaxPrintingSpeed2.Units.First().Name} in black/white{DutyCycle}";
	}
	else if(!String.IsNullOrEmpty(MaximumPrinterMonthlyDutyCycleRef)
	&& Printing_MaxPrintingSpeedColor2.HasValue()){
		result = $"##{Printing_MaxPrintingSpeedColor2.FirstValue()} {Printing_MaxPrintingSpeedColor2.Units.First().Name} in color{DutyCycle}";
	}
	else if(Printing_MaxPrintingSpeed.HasValue()
	&& Printing_MaxPrintingSpeedColor.HasValue()
	&& Printing_MaxPrintingSpeed.HasValue(Printing_MaxPrintingSpeedColor.FirstValue().ToString())){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()} ppm for color and black/white images";
	}
	else if(Printing_MaxPrintingSpeed.HasValue()
	&& Printing_MaxPrintingSpeedColor.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()} ppm in black/white and ##{Printing_MaxPrintingSpeedColor.FirstValue()}ppm in color";
	}
	else if(Printing_MaxPrintingSpeed.HasValue()){
		result = $"##{Printing_MaxPrintingSpeed.FirstValue()} ppm in black/white";
	}
	else if(Printing_MaxPrintingSpeedColor.HasValue()){
		result = $"Has a printing speed of ##{Printing_MaxPrintingSpeedColor.FirstValue()} ppm in color";
	}
	else if(Printing_MaxPrintingSpeed2.HasValue()
	&& Printing_MaxPrintingSpeedColor2.HasValue()){
		result = $"Has a printing speed of ##{Printing_MaxPrintingSpeed2.FirstValue()} {Printing_MaxPrintingSpeed2.Units.First().Name} in black/white and ##{Printing_MaxPrintingSpeedColor2.FirstValue()} {Printing_MaxPrintingSpeedColor2.Units.First().Name} in color";
	}
	else if(Printing_MaxPrintingSpeed2.HasValue()){
		result = $"Has a printing speed of ##{Printing_MaxPrintingSpeed2.FirstValue()} {Printing_MaxPrintingSpeed2.Units.First().Name} in black/white";
	}
	else if(Printing_MaxPrintingSpeedColor2.HasValue()){
		result = $"Has a printing speed of ##{Printing_MaxPrintingSpeedColor2.FirstValue()} {Printing_MaxPrintingSpeedColor2.Units.First().Name} in color";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrintersPrintSpeedMode⸮{result}");
	}
}

// --[FEATURE #4] 
// --Connectivity (No. of Ports & Interface); Includes wireless 
void PrintersConnectivity(){ 
var result = ""; 
var WirelessReadyRef = R("SP-4560").HasValue() ? R("SP-4560").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4560").HasValue() ? R("cnet_common_SP-4560").Replace("<NULL>", "").Text : ""; 
var Connectivity_Interface = A[2703]; // Connectivity - Interface 
var InterfaceRequired_Type = A[6836]; // Interface Required - Type 

if(Connectivity_Interface.HasValue() 
&& Connectivity_Interface.Where("%Wi-Fi%").Any() 
&& InterfaceRequired_Type.HasValue()){ 
result = $@"{InterfaceRequired_Type.Values.Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2 x $1")).Flatten(", ").Replace("<>", "##1")} and Wi-Fi connectivity help to enhance productivity"; 
} 
else if(Connectivity_Interface.HasValue() 
&& Connectivity_Interface.WhereNot("%Bluetooth%").Any() 
&& Connectivity_Interface.Where("%Wi-Fi%").Any() 
&& Connectivity_Interface.Values.Count() > 1){ 
result = $@"{InterfaceRequired_Type.WhereNot("%Wi-Fi%").Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2 x $1")).Flatten(", ").Replace("<>", "##1")} and Wi-Fi connectivity help to enhance productivity"; 
} 
else if(Connectivity_Interface.HasValue() 
&& Connectivity_Interface.WhereNot("%Bluetooth%").Any() 
&& Connectivity_Interface.Where("%LAN%").Any() 
&& InterfaceRequired_Type.HasValue()){ 
var Lan = $"{InterfaceRequired_Type.Where("%lan%").Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2")).First()} x".Replace("## x", "##1 x"); 
result = $@"Network-ready wired {Lan} {Connectivity_Interface.Where("%LAN%").First().Value()} Ethernet and {InterfaceRequired_Type.WhereNot("%LAN%").Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2 x $1")).Flatten(", ").RegexReplace(@"(## x)", "##1 x")} connectivity"; 
} 
else if(Connectivity_Interface.HasValue() 
&& Connectivity_Interface.WhereNot("%Bluetooth%").Any() 
&& Connectivity_Interface.Where("%LAN%").Any()){ 
result = $@"Network-ready wired {Connectivity_Interface.Where("%LAN%").First().Value()} Ethernet and {Connectivity_Interface.WhereNot("%LAN%").Flatten(", ")} connectivity"; 
} 
else if(InterfaceRequired_Type.HasValue()){ 
var Bluetooth = Connectivity_Interface.HasValue() && Connectivity_Interface.Where("%Bluetooth%").Any() ? $", and {Connectivity_Interface.Where("%Bluetooth%").First().Value()} interface" : ""; 
result = $@"Printer features {InterfaceRequired_Type.Values.Match(23, 6836).Values(" x ").Select(s => s.RegexReplace(@"(.*)\sx\s(\d*).*", "##$2 x $1")).Flatten(", ").Replace("<>", "##1")}{Bluetooth}"; 
} 
else if(WirelessReadyRef.Equals("Wired")){ 
result = "Network-ready wired Ethernet connectivity"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"PrintersConnectivity⸮{result}"); 
} 
} 

// --[FEATURE #5] - All 
// --Dimensions (in Inches): H x W x D 

// --[FEATURE #6] 
// --Input loading feature, Input capacity & Media type supported (includes ink cartridges) 
void PrintersInputLoadingFeature(){ 
var result = ""; 
var PaperInputCapacityRef = R("SP-4547").HasValue() ? R("SP-4547").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4547").HasValue() ? R("cnet_common_SP-4547").Replace("<NULL>", "").Text : ""; 
var Printer_MediaType = A[193]; // Printer - Media Type 
var DocumentMediaHandlingDetails_Type = A[2710]; // Document & Media Handling Details - Type 
var DocumentMediaHandling_SupportedMediaType = A[2730]; // Document & Media Handling - Supported Media Type 
var TotalMediaCapacity = A[443]; // Total Media Capacity 
var MediaCapacity = TotalMediaCapacity.HasValue() ? $"{TotalMediaCapacity.FirstValue()} {TotalMediaCapacity.Units.First().Name.Replace("page", "pages").Replace("sheet", "sheets").Replace("pagess", "pages").Replace("sheetss", "sheets")}" : ""; 

if(!String.IsNullOrEmpty(PaperInputCapacityRef) 
&& Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).HasValue() 
&& DocumentMediaHandlingDetails_Type.HasValue("bypass tray") 
&& DocumentMediaHandlingDetails_Type.Where("bypass tray").Match(2712).Values().First().In("%1%") 
&& !String.IsNullOrEmpty(MediaCapacity)){ 
result = $"{MediaCapacity} input capacity and a single-sheet bypass tray; types of media supported include {Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).Values.Select(s => s.Value()).FlattenWithAnd(10, ",").TrimEnd(",")}"; 
} 
else if(!String.IsNullOrEmpty(PaperInputCapacityRef) 
&& DocumentMediaHandlingDetails_Type.HasValue("bypass tray") 
&& DocumentMediaHandlingDetails_Type.Where("bypass tray").Match(2712).Values().First().In("%1%") 
&& !String.IsNullOrEmpty(MediaCapacity)){ 
result = $"{MediaCapacity} input capacity lessens the need for constant reloading; single sheet bypass tray"; 
} 
else if(!String.IsNullOrEmpty(PaperInputCapacityRef) 
&& Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).HasValue() 
&& !String.IsNullOrEmpty(MediaCapacity)){ 
result = $"{MediaCapacity} input capacity; types of media supported include {Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).Values.Select(s => s.Value()).FlattenWithAnd(10, ",").TrimEnd(",")}"; 
} 
else if(!String.IsNullOrEmpty(PaperInputCapacityRef) 
&& !String.IsNullOrEmpty(MediaCapacity)){ 
result = $"{MediaCapacity} input capacity lessens the need for constant reloading"; 
} 
else if(Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).HasValue()){ 
result = $"Types of media supported include {Coalesce(DocumentMediaHandling_SupportedMediaType, Printer_MediaType).Values.Select(s => s.Value()).FlattenWithAnd(10, ",").TrimEnd(",")}"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"PrintersInputLoadingFeature⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Printer processor & printer/fax memory 
void PrintersProcessorMemory(){ 
var result = ""; 
var PrinterProcessorRef = R("SP-5129").HasValue() ? R("SP-5129").Replace("<NULL>", "").Text : 
R("cnet_common_SP-5129").HasValue() ? R("cnet_common_SP-5129").Replace("<NULL>", "").Text : ""; 
var PrinterMemoryRef = R("SP-4539").HasValue() ? R("SP-4539").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4539").HasValue() ? R("cnet_common_SP-4539").Replace("<NULL>", "").Text : ""; 
var FaxMachine_TotalMemoryCapacity = A[2747]; // Fax Machine - Total Memory Capacity 

if(!String.IsNullOrEmpty(PrinterProcessorRef) 
&& !String.IsNullOrEmpty(PrinterMemoryRef) 
&& FaxMachine_TotalMemoryCapacity.HasValue()){ 
result = $"{PrinterProcessorRef} processor, {PrinterMemoryRef} printer memory and fax memory of up to {FaxMachine_TotalMemoryCapacity.FirstValue()} pages provides convenient, reliable printing of your most important documents"; 
} 
else if(!String.IsNullOrEmpty(PrinterProcessorRef) 
&& !String.IsNullOrEmpty(PrinterMemoryRef)){ 
result = $"{PrinterProcessorRef} processor and {PrinterMemoryRef.Split(":").Last()} of memory ensure speed and accuracy"; 
} 
else if(!String.IsNullOrEmpty(PrinterProcessorRef) 
&& FaxMachine_TotalMemoryCapacity.HasValue()){ 
result = $"{PrinterProcessorRef} processor and fax memory of up to {FaxMachine_TotalMemoryCapacity.FirstValue()} pages provides convenient, reliable printing of your most important documents"; 
} 
else if(!String.IsNullOrEmpty(PrinterMemoryRef) 
&& FaxMachine_TotalMemoryCapacity.HasValue()){ 
var MemoryRef = PrinterMemoryRef.Split(" ").ToList(); 
var Memory = MemoryRef.Select(s => MemoryRef.IndexOf(s) == 1 ? s.ToLower() : s).Flatten(" "); 
result = $"{Memory}, up to {FaxMachine_TotalMemoryCapacity.FirstValue()} pages fax memory"; 
} 
else if(!String.IsNullOrEmpty(PrinterProcessorRef)){ 
result = $"Processor speed is {PrinterProcessorRef} to handle large printing jobs"; 
} 
else if(!String.IsNullOrEmpty(PrinterMemoryRef)){ 
var MemoryRef = PrinterMemoryRef.Split(" ").ToList(); 
var Memory = MemoryRef.Select(s => MemoryRef.IndexOf(s) == 1 ? s.ToLower() : s).Flatten(" "); 
result = $"{Memory} printer memory provides convenient, reliable printing of your most important documents"; 
} 
else if(FaxMachine_TotalMemoryCapacity.HasValue()){ 
result = $"The fax memory accommodates up to {FaxMachine_TotalMemoryCapacity.FirstValue()} pages for snag-free operation"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"PrintersProcessorMemory⸮{result}"); 
} 
} 

// --[FEATURE #8]
// --Automatic Printing Features; including duplex printing
void AutomaticPrintingFeatures(){
    var result = "";
    var DuplexPrintingRef = R("SP-13862").HasValue() ? R("SP-13862") : R("cnet_common_SP-13862");
    var DocumentFeederCapacityRef = R("SP-350275").HasValue() ? R("SP-350275") : R("cnet_common_SP-350275");
    var DocumentMediaHandlingDetails_Type = A[2710];
    var Copying_AutomaticDuplexing = A[2778];
    var Printing_AutomaticDuplexing = A[5425];
    var Scanning_AutomaticDuplexing = A[5738];
    var Details_Type = DocumentMediaHandlingDetails_Type.HasValue()
    && DocumentMediaHandlingDetails_Type.Where("%ADF%").Any()
    && A[2725].HasValue()
    && A[2725].HasValue(DocumentMediaHandlingDetails_Type.Where("%ADF%").Match(2712).Values().Select(s => s.RegexReplace(@"(.*)\s(\d*).*", "$2")).First().ToString()) ?
    $" and an auto feeder with a {A[2725].FirstValue()}-{A[2725].Units.First().Name} capacity" : "";
    
    if(Copying_AutomaticDuplexing.HasValue("yes")
    && Printing_AutomaticDuplexing.HasValue("yes")
    && Scanning_AutomaticDuplexing.HasValue("yes")){
        result = $"Automatic duplexing for effortless two-sided copying, scanning, and printing{Details_Type}";
    }
    else if(Copying_AutomaticDuplexing.HasValue("yes")
    && Printing_AutomaticDuplexing.HasValue("yes")){
        result = $"Automatic two-sided printing and copying{Details_Type} helps save paper";
    }
    else if(Copying_AutomaticDuplexing.HasValue("yes")){
        result = $"Automatic two-sided duplexing enables hands-free two-sided copying{Details_Type}";
    }
    else if(Printing_AutomaticDuplexing.HasValue("yes")){
        result = $"Help save paper and create two-sided documents with automatic duplex printing{Details_Type}";
    }
    else if(Scanning_AutomaticDuplexing.HasValue("yes")
    && Copying_AutomaticDuplexing.HasValue("yes")){
        result = $"Save time with single pass duplex scanning and copying, by scanning both sides of two-sided documents simultaneously{Details_Type}";
    }
    else if(Scanning_AutomaticDuplexing.HasValue("yes")){
        result = $"Single-pass duplex scanning allows fast multi-page scanning{Details_Type}";
    }
    else if(DocumentFeederCapacityRef.HasValue()){
        result = $"{SKU.ProductType} with automatic duplex printing and {DocumentFeederCapacityRef} sheets auto feeder";
    }
    else if(DuplexPrintingRef.HasValue()){
        result = $"{SKU.ProductType} with automatic duplex printing";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AutomaticPrintingFeatures⸮{result}");
    }
}

// --[FEATURE #9] 
// --Display, Indicators, and Controls (Button, Touch Screen, LCD, etc) 
void PrintersDisplay(){ 
var result = ""; 
var PrinterDisplayRef = R("SP-4562").HasValue() ? R("SP-4562").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4562").HasValue() ? R("cnet_common_SP-4562").Replace("<NULL>", "").Text : ""; 
var Display_Features = A[5299]; // Display - Features 
var Chassis_BuiltInDevices = A[2239]; // Chassis - Built-In Devices 

if(!String.IsNullOrEmpty(PrinterDisplayRef) 
&& Display_Features.HasValue("touch screen")){ 
result = $"Easily manage tasks with the {PrinterDisplayRef.Replace("touch screen", "")} touchscreen"; 
} 
else if(!String.IsNullOrEmpty(PrinterDisplayRef) 
&& Coalesce(PrinterDisplayRef).ExtractNumbers().Any() 
&& Coalesce(PrinterDisplayRef).ExtractNumbers().First() >= 2){ 
result = $"{PrinterDisplayRef.Replace("Five lines", "Five-line").Replace("touch screen", "touchscreen")} display enables easy setup and navigation"; 
} 
else if(!String.IsNullOrEmpty(PrinterDisplayRef)){ 
result = $"{PrinterDisplayRef.Replace("Five lines", "Five-line").Replace("touch screen", "touchscreen")} display enables easy setup and navigation"; 
} 
else if(Display_Features.HasValue("touch screen")){ 
result = "Easily manage your print jobs with the touch screen display"; 
} 
else if(Chassis_BuiltInDevices.HasValue() 
&& Chassis_BuiltInDevices.Where("%touch screen%", "%display%").Any()){ 
var BuiltInDevices = Chassis_BuiltInDevices.HasValue("control panel") ? $" and {Chassis_BuiltInDevices.FirstValue()}" : ""; 
result = $"Easily manage your print jobs with the {Chassis_BuiltInDevices.Where("%touch screen%").First().Value().Replace("touch screen", "touchscreen").Replace(" inch", @"""").Replace(" display", "")} display{BuiltInDevices}"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"PrintersDisplay⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Scanning capabilities (includes scanner type, technology/software, resolution & output color) 
void PrintersScanningCapabilities(){ 
var result = ""; 
var ScanResolutionRef = R("SP-4557").HasValue() ? R("SP-4557").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4557").HasValue() ? R("cnet_common_SP-4557").Replace("<NULL>", "").Text : ""; 
var Scanning_OpticalResolution = A[2754]; // Scanning - Optical Resolution 
var InterfaceRequired_Type = A[2755]; // Interface Required - Type 
var Scanning_ColorDepth = A[2756]; // Scanning - Color Depth 
var Scanning_GrayscaleDepth = A[2757]; // Scanning - Grayscale Depth 
var Scanning_ColorDepthInternal = A[4724]; // Scanning - Color Depth (Internal) 
var OpticalResolution = Scanning_OpticalResolution.HasValue() && InterfaceRequired_Type.HasValue() ? 
$"{Scanning_OpticalResolution.FirstValue()} optical resolution, " : Scanning_OpticalResolution.HasValue() ? 
$"{Scanning_OpticalResolution.FirstValue()} optical resolution " : ""; 
var Required_Type = InterfaceRequired_Type.HasValue() ? $"{InterfaceRequired_Type.FirstValue()} interpolated resolution" : ""; 
var ColorDepth = Scanning_ColorDepth.HasValue() ? $"{Scanning_ColorDepth.FirstValue()} color depth, " : ""; 
var GrayscaleDepth = Scanning_GrayscaleDepth.HasValue() ? $"{Scanning_GrayscaleDepth.FirstValue()} grayscale depth " : ""; 

if(Scanning_GrayscaleDepth.HasValue()){ 
result = $"{OpticalResolution}{Required_Type}{ColorDepth}{GrayscaleDepth}for good quality printed text ##and images"; 
} 
else if(Scanning_ColorDepth.HasValue()){ 
result = $"{OpticalResolution}{Required_Type}{ColorDepth}for good quality printed text ##and images"; 
} 
else if(InterfaceRequired_Type.HasValue()){ 
result = $"{OpticalResolution}{Required_Type}for good quality printed text ##and images"; 
} 
else if(Scanning_OpticalResolution.HasValue()){ 
result = $"{OpticalResolution}for good quality printed text and images"; 
} 
else if(!String.IsNullOrEmpty(ScanResolutionRef) 
&& (Scanning_ColorDepth.HasValue() || Scanning_ColorDepthInternal.HasValue())){ 
result = $"Color scanner captures resolutions up to {ScanResolutionRef} to capture every detail"; 
} 
else if(!String.IsNullOrEmpty(ScanResolutionRef)){ 
result = $"Scanner captures resolutions up to {ScanResolutionRef} to capture every detail"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"PrintersScanningCapabilities⸮{result}"); 
} 
} 

// --[FEATURE #11] - All 
// --Certification and Standards 

// --[FEATURE #12] - All 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void LargeFormatPrinter(){ 
var result = ""; 
var DocumentMediaHandling_LargeFormatPrinterSize = A[4919]; // Document & Media Handling - Large Format Printer Size 

if(DocumentMediaHandling_LargeFormatPrinterSize.HasValue()){ 
result = $"{DocumentMediaHandling_LargeFormatPrinterSize.FirstValue()} large-format printer is equipped to satisfy many diverse needs of architects, engineers, constructors, GIS and other professional"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"LargeFormatPrinter⸮{result}"); 
} 
} 

// --[FEATURE #13] - All 
// --Warranty Information. NOTE: If REFURBISHED, then “Warranty Length Non-mfg. warranty through Vendor. This product is NOT warrantied through Brand Name” 

//5435445243161518, 5435445243142044, 5435445243167883, 543544524340300 "Printers END" "Serhii.O" §§ 

//§§532628898138204449 "Tablets & iPads BEGIN" "Serhii.O" 

TabletsSizeAndUse(); 
ScreenSizeDisplayTypeAndScreenResolution(); 
TabletsProcessor(); 
TabletsOperatingSystem(); 
TabletsMemory(); 
TabletsBatteryLife(); 
TabletsWirelessConnectivity(); 
TabletsCameraDetails(); 
TabletsAudioSpeakerInfo(); 
TabletsImputsAndOutputs(); 
// Dimensions - All 
// Warranty - All 

// --[FEATURE #1] 
// --Tablet Size and Use 
void TabletsSizeAndUse(){ 
var result = ""; 
var HeightInInchesRef = R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : ""; 
var WeightRef = R("SP-20402").HasValue() ? R("SP-20402").Replace("<NULL>", "") : 
R("cnet_common_SP-20402").HasValue() ? R("cnet_common_SP-20402").Replace("<NULL>", "") : Coalesce(""); 

if(!String.IsNullOrEmpty(HeightInInchesRef) 
&& WeightRef.In("1")){ 
result = $@"Tablet is just {HeightInInchesRef}"" thin and weighs just {WeightRef.Text} lb., so you can take it wherever you go"; 
} 
else if(!String.IsNullOrEmpty(HeightInInchesRef) 
&& (WeightRef.In("1.%") || WeightRef.HasValue())){ 
result = $@"Tablet is just {HeightInInchesRef}"" thin and weighs just {WeightRef.Text} lbs., so you can take it wherever you go"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"TabletsSizeAndUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Screen Size, Display Type and Screen Resolution 
void ScreenSizeDisplayTypeAndScreenResolution(){ 
var result = ""; 
var Display_DiagonalSize = Coalesce(A[140]); // Display - Diagonal Size 
var Display_NativeResolution = Coalesce(A[149]); // Display - Native Resolution 
var Display_MonitorFeatures = Coalesce(A[2337]); // Display - Monitor Features 
var Display_TFTTechnology = Coalesce(A[3315]); // Display - TFT Technology 

var Size = Display_DiagonalSize.HasValue() ? $@"{Display_DiagonalSize.FirstValue()}"" " : ""; 

var MonitorFeaturesTechnology = Display_MonitorFeatures.HasValue() && Display_MonitorFeatures.Where("%display%").Any() && 
Display_MonitorFeatures.Where("%display%").Where(s => s.HasValue("%etina%")).Any() ? 
Display_MonitorFeatures.Where("%display%").Where(s => s.HasValue("%etina%")).First().Value() : 
Display_MonitorFeatures.Where("%display%").Any() ? 
Display_MonitorFeatures.Where("%display%").First().Value() : 
Display_TFTTechnology.HasValue() ? 
$"display with {Display_TFTTechnology.FirstValue()} technology" : ""; 

var Resolution = Display_NativeResolution.HasValue() ? 
$" with {Display_NativeResolution.FirstValue()} resolution" : ""; 

var Display = !String.IsNullOrEmpty(MonitorFeaturesTechnology) ? MonitorFeaturesTechnology : "display"; 

result = $"{Size}{Display}{Resolution}"; 

if(!String.IsNullOrEmpty(result)){ 
Add($"ScreenSizeDisplayTypeAndScreenResolution⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Processor 
void TabletsProcessor(){ 
var result = ""; 
var TabletProcessorRef = R("SP-13748").HasValue() ? R("SP-13748").Replace("<NULL>", "").Text : 
R("cnet_common_SP-13748").HasValue() ? R("cnet_common_SP-13748").Replace("<NULL>", "").Text : ""; 
var ProcessorSpeedRef = R("SP-22057").HasValue() ? R("SP-22057").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22057").HasValue() ? R("cnet_common_SP-22057").Replace("<NULL>", "").Text : ""; 
var Processor_NumberOfCores = Coalesce(A[3483]); // Processor - Number of Cores 
var Processor_Manufacturer = ""; 

if(!String.IsNullOrEmpty(TabletProcessorRef)){ 
switch(TabletProcessorRef){ 
case var Processor when Processor.Equals("A10X Fusion"): 
result = $"{Processor} chip with 64-bit desktop-class architecture delivers more power than most PC laptops"; 
break; 
case var Processor when Processor.Equals("A10 Fusion"): 
result = "New 64-bit A10 Fusion chip delivers faster performance for playing games and using the newest apps"; 
break; 
case var Processor when Processor.Equals("A9"): 
result = "The 64-bit A9 chip delivers performance that makes every app feel fast and fluid"; 
break; 
case var Processor when Processor.Equals("A8"): 
result = "A8 second-generation chip with 64-bit desktop-class architecture"; 
break; 
case var Processor when !String.IsNullOrEmpty(Processor) 
&& Processor_NumberOfCores.HasValue("Quad-Core") 
&& !String.IsNullOrEmpty(ProcessorSpeedRef): 
result = $"{ProcessorSpeedRef}GHz {Coalesce(Processor).RegexReplace(@"(\d*.?\d*?GHz )(.*)", "$2")} processor for reliable operation"; 
break; 
case var Processor when !String.IsNullOrEmpty(Processor) 
&& Processor_NumberOfCores.HasValue() && Processor_NumberOfCores.FirstValue().NotIn("Quad-Core") 
&& Processor_NumberOfCores.FirstValue().ExtractNumbers().Any() && Processor_NumberOfCores.FirstValue().ExtractNumbers().First() > 2 
&& !String.IsNullOrEmpty(ProcessorSpeedRef): 
Processor_Manufacturer = A[39].HasValue() ? $" {A[39].FirstValue()}" : ""; 
result = $"A tablet features {ProcessorSpeedRef}GHz {Processor_Manufacturer}{TabletProcessorRef} {Processor_NumberOfCores.FirstValue().Replace("8-core", "octa-core")} processor for fast performance"; 
break; 
case var Processor when !String.IsNullOrEmpty(Processor) 
&& !String.IsNullOrEmpty(ProcessorSpeedRef): 
Processor_Manufacturer = Processor_NumberOfCores.HasValue() ? $" {Processor_NumberOfCores.FirstValue().ToLower()}" : ""; 
result = $"Tablet offers stunning performance with {ProcessorSpeedRef}GHz {TabletProcessorRef}{Processor_Manufacturer} processor"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"TabletsProcessor⸮{result}"); 
} 
} 

// --[FEATURE #4] 
// --Operating System 
void TabletsOperatingSystem(){ 
var result = ""; 
var OperatingPlatformRef = R("SP-18433").HasValue() ? R("SP-18433").Replace("<NULL>", "") : 
R("cnet_common_SP-18433").HasValue() ? R("cnet_common_SP-18433").Replace("<NULL>", "") : Coalesce(""); 

if(OperatingPlatformRef.HasValue()){ 
switch(OperatingPlatformRef){ 
case var OS when OS.In("%Android%"): 
result = $"{OS.Text} offers an intuitive user interface with smooth multitasking"; 
break; 
case var OS when OS.In("%Windows%"): 
result = $"Runs {OS.Text} perfectly so you can use all the touch apps and desktop software you need"; 
break; 
case var OS when OS.In("%iOS%"): 
result = "iOS - advanced mobile operating system"; 
break; 
default: 
result = $"Runs on the powerful {OperatingPlatformRef.Text} Operating System"; 
break; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"TabletsOperatingSystem⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Memory 
void TabletsMemory(){ 
var result = ""; 
var MemoryTypeRef = R("SP-176").HasValue() ? R("SP-176").Replace("<NULL>", "").Text : 
R("cnet_common_SP-176").HasValue() ? R("cnet_common_SP-176").Replace("<NULL>", "").Text : ""; 
var TabletMemoryCapacityRef = R("SP-22083").HasValue() ? R("SP-22083").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22083").HasValue() ? R("cnet_common_SP-22083").Replace("<NULL>", "").Text : ""; 
var CardReader_FlashMemoryCardsMaxSupportedCapacity = Coalesce(A[5648]); // Card Reader - Flash Memory Cards Max Supported Capacity 
var Capacity1 = CardReader_FlashMemoryCardsMaxSupportedCapacity.HasValue() ? $", expandable up to {CardReader_FlashMemoryCardsMaxSupportedCapacity.FirstValue().Replace(" GB", "GB")}" : ""; 

if(!String.IsNullOrEmpty(MemoryTypeRef) 
&& !String.IsNullOrEmpty(TabletMemoryCapacityRef)){ 
result = $"{TabletMemoryCapacityRef}GB of internal storage{Capacity1} and {MemoryTypeRef} RAM for easy multitasking"; 
} 
else if(!String.IsNullOrEmpty(TabletMemoryCapacityRef)){ 
result = $"{TabletMemoryCapacityRef}GB of internal storage{Capacity1}"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"TabletsMemory⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Battery Life 
void TabletsBatteryLife(){ 
var result = ""; 
var BatteryLifeDetails_UsageMode = Coalesce(A[5652]); // Battery Life Details - Usage Mode 
var Battery_RunTime = Coalesce(A[3386], A[341]); // Battery - Run Time (Up To) 

if(BatteryLifeDetails_UsageMode.HasValue() 
&& BatteryLifeDetails_UsageMode.Where("web browsing over Wi-Fi", "video playback", "audio playback").Any() 
&& BatteryLifeDetails_UsageMode.Where("web browsing over Wi-Fi", "video playback", "audio playback").Count() == 3){ 
result = $"The battery lasts up to {BatteryLifeDetails_UsageMode.Where("web browsing over Wi-Fi").Match(800).Values(" ").Select(s => Coalesce(s).RegexReplace(@"(.*)\s(\d*).*", "$2 hours for $1")).Flatten(" ")}, {BatteryLifeDetails_UsageMode.Where("video playback").Match(800).Values(" ").Select(s => Coalesce(s).RegexReplace(@"(.*)\s(\d*).*", "$2 hours for $1")).Flatten(" ")}, and up to {BatteryLifeDetails_UsageMode.Where("audio playback").Match(800).Values(" ").Select(s => Coalesce(s).RegexReplace(@"(.*)\s(\d*).*", "$2 hours for $1")).Flatten(" ")}"; 
} 
else if(Battery_RunTime.HasValue()){ 
result = $"Up to {Battery_RunTime.FirstValue()} {Battery_RunTime.Units.First().Name} of battery life"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"TabletsBatteryLife⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Wireless Connectivity 
void TabletsWirelessConnectivity(){ 
var result = ""; 
//802.11/a/n|802.11/n/g/e|802.11/a|802.11n|802.11/g/n|802.11/n/e|802.11/a/b/g/n/ac|802.11/b/g/n/e|Wireless|802.11/a/c|802.11/b/g|802.11/a/b/g|802.11/a/b/g/n|802.11/a/b|802.11b/g/n|No|Wireless A/C|Internal NIC 10/100|802.11ac with MIMO simultaneous dual band (2.4GHz and 5GHz)|802.11ac (2.4GHz/5GHz)|802.11ac (2.4GHz/5Ghz) 1x2 MISO 
var ConnectivityWirelessRef = R("SP-22089").HasValue() ? R("SP-22089").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22089").HasValue() ? R("cnet_common_SP-22089").Replace("<NULL>", "").Text : ""; 

if(!String.IsNullOrEmpty(ConnectivityWirelessRef)){ 
result = $"Features IEEE {ConnectivityWirelessRef} for Internet web browsing"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"TabletsWirelessConnectivity⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Camera Details 
void TabletsCameraDetails(){ 
var result = ""; 
var FrontCameraRef = R("SP-22084").HasValue() ? R("SP-22084").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22084").HasValue() ? R("cnet_common_SP-22084").Replace("<NULL>", "").Text : ""; 
var RearCameraRef = R("SP-22082").HasValue() ? R("SP-22082").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22082").HasValue() ? R("cnet_common_SP-22082").Replace("<NULL>", "").Text : ""; 
var DigitalCamera_DigitalZoom = Coalesce(A[3040]); // Digital Camera - Digital Zoom 
var DigitalCamera_LensAperture = Coalesce(A[3042]); // Digital Camera - Lens Aperture 
var DigitalCamera_HDVideoRecording = Coalesce(A[5658]); // Digital Camera - HD Video Recording 
var DigitalCamera_FrameRate = Coalesce(A[7997]); // Digital Camera - Frame Rate 
var DigitalCamera_FrontFacingLensAperture = Coalesce(A[11227]); // Digital Camera - Front-facing Lens Aperture 

if(!String.IsNullOrEmpty(FrontCameraRef) 
&& !String.IsNullOrEmpty(RearCameraRef)){ 
var DigitalZoom = DigitalCamera_DigitalZoom.HasValue() ? $", {DigitalCamera_DigitalZoom.FirstValue()}x digital zoom" : ""; 
var LensAperture = DigitalCamera_LensAperture.HasValue() && DigitalCamera_FrontFacingLensAperture.HasValue() ? $", {DigitalCamera_LensAperture.FirstValue().Replace("/", "").ToUpper()} (rear)/" : DigitalCamera_LensAperture.HasValue() ? $", {DigitalCamera_LensAperture.FirstValue().Replace("/", "").ToUpper()} (rear)" : ""; 
var FrontFacingLensAperture = DigitalCamera_FrontFacingLensAperture.HasValue() ? $"{DigitalCamera_FrontFacingLensAperture.FirstValue().Replace("/", "").ToUpper()} (front)" : ""; 
var HDVideoRecording = DigitalCamera_HDVideoRecording.HasValue() ? $", video recording solution: {DigitalCamera_HDVideoRecording.FirstValue()}" : ""; 
var FrameRate = DigitalCamera_FrameRate.HasValue() && DigitalCamera_FrameRate.FirstValue().ExtractNumbers().Any() ? $" at {DigitalCamera_FrameRate.FirstValue().ExtractNumbers().First()}fps" : ""; 
result = $"Camera: {FrontCameraRef}MP (front), {RearCameraRef}MP (rear){DigitalZoom}{LensAperture}{FrontFacingLensAperture}{HDVideoRecording}{FrameRate}"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"TabletsCameraDetails⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Audio/Speaker Info 
void TabletsAudioSpeakerInfo(){ 
var result = ""; 
var AudioOutput_Type1 = Coalesce(A[5740]); // Audio Output - Type 
var AudioOutput_Type2 = Coalesce(A[5785]); // Audio Output - Type 
var AudioInput_Type = Coalesce(A[276]); // Audio Input - Type 

if((AudioOutput_Type1.HasValue("stereo speakers") || AudioOutput_Type2.HasValue() && AudioOutput_Type2.Values.Flatten(" ").In("%stereo%speakers%")) 
&& AudioInput_Type.HasValue()){ 
result = $"Built-in {AudioInput_Type.FirstValue()} and surround sound stereo speakers for expansive, rich audio"; 
} 
else if(AudioOutput_Type1.HasValue("four speakers") || (AudioOutput_Type2.HasValue() && AudioOutput_Type2.Values.Flatten(" ").In("%four%speakers%"))){ 
result = "Listen to your favorite music or watch TV and movies with clear audio through built-in four speakers"; 
} 
else if(AudioOutput_Type1.HasValue() || AudioOutput_Type2.HasValue()){ 
var Type = AudioOutput_Type1.HasValue() ? AudioOutput_Type1.FirstValue() : 
AudioOutput_Type2.HasValue() ? AudioOutput_Type1.Values.Flatten(" ") : ""; 
result = $"Listen to your favorite music or watch TV and movies with clear audio through built-in {Type}"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"TabletsAudioSpeakerInfo⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Imputs and Outputs 
void TabletsImputsAndOutputs(){ 
var result = ""; 
var InterfaceOrPortTypeRef = R("SP-21791").HasValue() ? R("SP-21791").Replace("<NULL>", "") : 
R("cnet_common_SP-21791").HasValue() ? R("cnet_common_SP-21791").Replace("<NULL>", "") : Coalesce(""); 

if(InterfaceOrPortTypeRef.HasValue()){ 
result = InterfaceOrPortTypeRef 
.Replace(" x 1", "") 
.Replace("1 x ", "") 
.Replace(" x ", "") 
.Replace("Smart Connector", "connects to your device with a Smart Connector, eliminating the need for Bluetooth pairing") 
.RegexReplace("((.* x )*Micro-USB)", "features a $1 port for simple charging and file transfers") 
.RegexReplace("((.* x )*[Hh]eadphones)[.*$]?", "headphone jack lets you connect your PC tablet to a sound system") 
.RegexReplace(@"((.* x )*[Hh]eadphones mini jack \(3\.5mm\))", "$1 lets you connect your tablet to a sound system") 
.RegexReplace("((.* x )*USB-C.*)", "tablet has $1 interfaces for external device connection") 
.RegexReplace("((.* x )*USB 3.1)", "tablet has $1 interfaces for external device connection") 
.RegexReplace("((.* x )*[Hh]eadset)", "headset jack lets you connect your PC tablet to a sound system") 
.RegexReplace("((.* x )*[Ll]ightning)", "enjoy revolutionary data transfer speeds with a Lightning connector") 
.Replace("2 x Micro-USB port", "2 x Micro-USB ports").Text.ToUpperFirstChar(); 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"TabletsImputsAndOutputs⸮{result}"); 
} 
} 

// --[FEATURE #11] - All 
// --Dimensions 

// --[FEATURE #12] - All 
// --Warranty *NEEDS TO BE LAST BULLET* If REFURBISHED, then “Warranty Length Non-mfg. warranty through Vendor. This product is NOT warrantied through Brand Name” 

//532628898138204449 "Tablets & iPads END" "Serhii.O" §§ 

//§§54353597404140828 "Printer Parts BEGIN" "Serhii.O" 

PrinterPartTypeUse(); 
// Shipping Dimensions - All 
PrinterPartCompatibility(); 
// True color & Material - All 
// Pack Size - All 
OffersDutyCycle(); 
// Compliant Standards - All 
PrinterPartsFoldTypes();
PrinterPartsPunchingType();
PrinterPartsWeight();
// Warranty information - All 

// --[FEATURE #1] 
// --Printer part type & Use 
void PrinterPartTypeUse(){ 
var result = ""; 
// Staple Cartridge|Stapler/Stacker|Spindle|Media Bin|Printer Imaging Unit|Paper Tray|Cutter Blade|Fax Server|Caster Base|Waste Cartridge|Battery|Maintenance Kit|Photoconductor Unit|Print Server|Memory Module|Photoconductor Kit|Toner Collection Unit|Suction Filter|Roll Feeder|Envelope Feeder|Printing Assembly|Roll Holder|Fuser Unit|Transfer Belt|Printer Transfer And Roller Kit|Print Head|Printer Cover|Moblie Print Accessory|Belt Unit|Developer|Drum Kit|Printhead Replacement Kit 
var PrinterPartTypeRef = R("SP-18364").HasValue() ? R("SP-18364").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18364").HasValue() ? R("cnet_common_SP-18364").Replace("<NULL>", "").Text : ""; 

switch(PrinterPartTypeRef.ToLower()){ 
case "print server": 
result = "Connect multiple devices for fast printing with this print server"; 
break; 
case "fax server": 
result = "Connect multiple devices for fast fax printing with this fax server"; 
break; 
case "print head": 
result = "Printhead offer fine quality and consistency for all your documents and projects"; 
break; 
case "spindle": 
result = "Simplify printing and speed up your workflow with this spindle"; 
break; 
case "staple cartridge": 
result = "This staple cartridge ensures accurate stapling for professional-looking documents"; 
break; 
case "caster base": 
result = "Printer caster base provides stability for product configuration"; 
break; 
case var Type when !String.IsNullOrEmpty(Type): 
result = $"{Type.ToLower().ToUpperFirstChar()} for your printer"; 
break; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"PrinterPartTypeUse⸮{result}"); 
} 
} 

// --[FEATURE #2] - All 
// --Shipping Dimensions 

// --[FEATURE #3] 
// --Compatibility 
void PrinterPartCompatibility(){ 
    var result = "";
    var Miscellaneous_CompatibleCartridge = Coalesce(A[3302]); // Miscellaneous - Compatible Cartridge
    var Miscellaneous_Refurbished = Coalesce(A[5312]); // Miscellaneous - Refurbished
    var CompatibleProducts = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body, SPEC["MS"].GetLine("Designed For").Body, SPEC["ES"].GetLine("Designed For").Body);
    var Header_Compatibility = A[603]; // Header - Compatibility
    
    if(CompatibleProducts.HasValue()){
        result = Miscellaneous_CompatibleCartridge.HasValue() ? 
        $"Compatible with: {Miscellaneous_CompatibleCartridge.Values.Select(s => s.Value().Replace("Troy", "TROY").Replace("plus", "Plus").Replace("Envy", "ENVY").Replace("Deskjet", "DeskJet").Replace("Officejet", "OfficeJet").Replace("ImageCLASS","imageCLASS").Replace("Kyocera", "KYOCERA").Replace("Copycentre","CopyCentre").Replace("B405/z","B405/Z").Replace("Troy", "TROY")).Flatten(", ").ToString().Split(", ").FlattenWithAnd(10, ",")}" :
        $"Compatible with: {CompatibleProducts.Text.Replace("plus", "Plus").Replace("Envy", "ENVY").Replace("Deskjet", "DeskJet").Replace("Officejet", "OfficeJet").Replace("ImageCLASS","imageCLASS").Replace("Kyocera", "KYOCERA").Replace("Copycentre","CopyCentre").Replace("B405/z","B405/Z").Replace("Troy", "TROY").Replace(";", ",").Split(", ").FlattenWithAnd(10, ",")}";
    }
    else if(Header_Compatibility.HasValue()){
        result = Header_Compatibility.Values.Count == 1 ? $"Compatible with {Header_Compatibility.Values.Select(s => s.Value().ToLower()).FlattenWithAnd()}" : $"Compatible with: {Header_Compatibility.Values.Select(s => s.Value().ToLower()).FlattenWithAnd()}";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"PrinterPartCompatibility⸮{result}");
    }
} 

// --[FEATURE #4] - All 
// --True color & Material; as applicable 

// --[FEATURE #5] - All 
// --Pack Size (If more than 1) 

// --[FEATURE #6] 
// --Offers duty cycle 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void OffersDutyCycle(){ 
var result = ""; 
var Consumable_DutyCycle = Coalesce(A[4785]); // Consumable - Duty Cycle 

if(Consumable_DutyCycle.HasValue()){ 
result = $"Offers duty cycle of {Consumable_DutyCycle.FirstValue()} for increased productivity"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"OffersDutyCycle⸮{result}"); 
} 
} 

// --[FEATURE #7] - All 
// --Compliant Standards 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 

// --[FEATURE #8] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void PrinterPartsFoldTypes(){
	var result = "";
	var Printer_MediaType = Coalesce(A[193]); // Printer - Media Type

	if(Printer_MediaType.HasValue()){
		result = $"Fold types: {Printer_MediaType.Values.Select(s => s.Value().Replace(" paper", "").Replace("-fold", "")).FlattenWithAnd()}";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrinterPartsFoldTypes⸮{result}");
	}
}

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void PrinterPartsPunchingType(){
	var result = "";
	var General_PunchingType = Coalesce(A[10651]); // Consumable - Duty Cycle

	if(General_PunchingType.HasValue()){
		result = $"Punches {General_PunchingType.FirstValue().ExtractNumbers().First()} circular holes";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrinterPartsPunchingType⸮{result}");
	}
}

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void PrinterPartsWeight(){
	var result = "";
	var WeightRef = R("SP-20402").HasValue() ? R("SP-20402").Replace("<NULL>", "").Text :
		R("cnet_common_SP-20402").HasValue() ? R("cnet_common_SP-20402").Replace("<NULL>", "").Text : "";

	if(!string.IsNullOrEmpty(WeightRef)){
		result = $"Weight: {WeightRef} lbs.";
	}
	if(!String.IsNullOrEmpty(result)){
	    Add($"PrinterPartsWeight⸮{result}");
	}
}

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 

// --[FEATURE #12] - All 
// --Warranty information 

//54353597404140828 "Printer Parts END" "Serhii.O" §§ 

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
    var ergonomicRef = GetReference("SP-21540");
    var chairTypeRef = GetReference("SP-21546");
    var AdjustableSeatDepthRef = GetReference("SP-22281");
    var general_ProductType = A[7156];
    var general_Class = A[7157];
    var general_DesignedFor = A[7158];
    var general_Casters = A[7164];
    var general_Swivel = A[7165];
    var general_HoursADay = A[7170];
    var general_Features = A[7175];
    var seat_Features = A[7176];
    var backrest_Features = A[7205];
    var base_CastersQty = A[7210];
    var dimensionsWeight_BackrestHeight = A[7237];
    var general_Style = A[10997];

    if(general_ProductType.HasValue("kneeling chair")){
        result = "Ergonomic kneeling chair for therapeutic comfort at work";
    }
    else if(general_Class.HasValue("visitor")
        && (!general_Casters.HasValue() || !base_CastersQty.HasValue())){
        result = "Guest chair to enhance the look of your waiting area";
    }
    else if(general_Class.HasValue("visitor")
        || general_DesignedFor.HasValue() && general_DesignedFor.Where("conference room","reception", "library").Any()){
        result = "Conference chair for a reception area, library, or study room";
    }
    else if(general_Class.HasValue("club chair")
        || general_ProductType.HasValue("armchair")){
        result = "Elegant side chair perfect for use in waiting rooms, reception areas and conference rooms";
    }
    else if(general_ProductType.HasValue("%gaming%")
        || general_DesignedFor.HasValue("%gaming%")){
        result = "Elegant side chair perfect for use in waiting rooms, reception areas and conference rooms";
    }
    else if(general_Style.HasValue("Contemporary")){
        result = string.IsNullOrEmpty(AdjustableSeatDepthRef) ? "Contemporary office chair with adjustable swivel seat for comfort" : "Contemporary office chair with a swivel seat for comfort";
    }
    else if(dimensionsWeight_BackrestHeight.HasValue()
        && dimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
        && dimensionsWeight_BackrestHeight.FirstValueUsm() >= 22
        && (ergonomicRef == "ergonomic" || general_Class.HasValue("ergonomic"))
        && general_Features.HasValue("pneumatic seat height adjustment")){
        result = "High-back ergonomic chair with pneumatic seat height adjustment for customization";
    }
    else if((ergonomicRef == "ergonomic" || general_Class.HasValue("ergonomic"))
        && backrest_Features.HasValue("molded")){
        result = "Ergonomic office chair with molded back for daily office work";
    }
    else if(general_Swivel.HasValue("Yes")
        && general_Class.HasValue("executive")){
        result = "Executive office chair with a swivel seat for maximum workspace use";
    }
    else if(dimensionsWeight_BackrestHeight.HasValue()
        && dimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
        && dimensionsWeight_BackrestHeight.FirstValueUsm() >= 22
        && general_Class.HasValue("executive")){
        result = "Executive high-back chair brings style and comfort to your office";
    }
    else if(general_Class.HasValue("manager")){
        result = "Manager chair offers comfort and complements a contemporary look";
    }
    else if((seat_Features.HasValue("padded") || backrest_Features.HasValue("padded"))
        && (base_CastersQty.HasValue() || general_Casters.HasValue("%Yes%"))){
        result = "Padded office chair with caster wheels for easy mobility";
    }
    else if(general_Features.HasValue("foldable")){
        result = "Lightweight chairs are easy to move, fold, and stack";
    }
    else if(!String.IsNullOrEmpty(chairTypeRef)){
        result = chairTypeRef == "Computer and Desk" ? "Computer and desk chair is a smart addition to any office space" : $"{chairTypeRef} chair is a smart addition to any office space";
    }
    else if(chairTypeRef == "Computer and Desk"){
        result = "Computer and desk chair is a smart addition to any office space";
    }
    else if(general_Class.HasValue("task")
        && general_Style.HasValue("Contemporary")){
        result = "Task chair offers comfort and complements a contemporary look";
    }
    else if(general_Class.HasValue("task")
        && general_HoursADay.HasValue() && general_HoursADay.FirstValue() >= 6){
        result = "Task chair ideal for time-intensive tasks";
    }
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
    var officeChairBackMaterialRef = R("SP-22290").HasValue() ? R("SP-22290") : R("cnet_common_SP-22290");
    var seatMaterialRef = R("SP-522").HasValue() ? R("SP-522") : R("cnet_common_SP-522");
    var chairMaterialRef = R("SP-21525").HasValue() ? R("SP-21525") : R("cnet_common_SP-21525");
    var colorFamilyRef = R("SP-17441").HasValue() ? R("SP-17441") : R("cnet_common_SP-17441");
    var trueColorRef = R("SP-22967").HasValue() ? R("SP-22967") : R("cnet_common_SP-22967");
    var officeChairBaseMaterial = R("SP-22289").HasValue() ? R("SP-22289") : R("cnet_common_SP-22289");
    var miscellaneous_ProductMaterial = A[372];
    var miscellaneous_Color = A[373];
    var miscellaneous_ColorCategory = A[5811];
    var general_Class = A[7157];
    var seatFeatures = A[7176];
    var seat_Color = A[7177];
    var seat_Material = A[7178];
    var backrest_Material = A[7203];
    var backrest_Color = A[7204];
    var base_Color = A[7209];

    if(officeChairBackMaterialRef.ToLower().NotIn($"%{seatMaterialRef.ToLower()}%")
        && seatMaterialRef.ToLower().NotIn($"%{officeChairBackMaterialRef.ToLower()}%")
        && officeChairBackMaterialRef.HasValue()
        && seatMaterialRef.HasValue()
        && colorFamilyRef.HasValue()
        && backrest_Color.HasValue() && seat_Color.HasValue()
        && backrest_Color.FirstValue().In(seat_Color.FirstValue())){
        var backrestColor = $"{backrest_Color.FirstValue().ToUpperFirstChar()} ";
        result = $"{backrestColor}{officeChairBackMaterialRef.ToLower()} back and {colorFamilyRef.ToLower()} {seatMaterialRef.ToLower()} seat";
    }
    else if(officeChairBackMaterialRef.ToLower().NotIn($"%{seatMaterialRef.ToLower()}%")
        && seatMaterialRef.ToLower().NotIn($"%{officeChairBackMaterialRef.ToLower()}%")
        && officeChairBackMaterialRef.HasValue()
        && seatMaterialRef.HasValue()
        && backrest_Color.HasValue() && seat_Color.HasValue()
        && backrest_Color.FirstValue().In(seat_Color.FirstValue())){
        var backrestColor = $"{backrest_Color.FirstValue().ToUpperFirstChar()} ";
        result = $"{backrestColor}{officeChairBackMaterialRef.ToLower()} back and {seatMaterialRef.ToLower()} seat";
    }
    else if(officeChairBackMaterialRef.ToLower().NotIn($"%{seatMaterialRef.ToLower()}%")
        && seatMaterialRef.ToLower().NotIn($"%{officeChairBackMaterialRef.ToLower()}%")
        && officeChairBackMaterialRef.HasValue()
        && seatMaterialRef.HasValue()
        && backrest_Color.HasValue() && seat_Color.HasValue()
        && backrest_Color.FirstValue().NotIn(seat_Color.FirstValue())){
        var backrestColor = $"{backrest_Color.FirstValue().ToUpperFirstChar()} ";
        var seatColor = $"{seat_Color.FirstValue().ToLower()} ";
        result = $"{backrestColor}{officeChairBackMaterialRef.ToLower()} back and {seatColor}{seatMaterialRef.ToLower()} seat";
    }
    else if(chairMaterialRef.HasValue("leather")
        && trueColorRef.HasValue("burgundy")){
        result = $"Burgundy {chairMaterialRef.ToLower()} upholstery adds style and comfort";
    }
    // --These plastic back task chairs in choice of vibrant colors are the perfect addition to your office
    else if(backrest_Color.HasValue("%vibrant%")
        && officeChairBackMaterialRef.HasValue()
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
        var backrestColor = $" {backrest_Color.FirstValue().ToLower()}";
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
        && chairMaterialRef.HasValue()
        && backrest_Material.HasValue("mesh")){
        var notMesh = !chairMaterialRef.HasValue("mesh") ? chairMaterialRef.ToLower() : "";
        result = $"{seat_Color.FirstValue().ToUpperFirstChar()} {notMesh} seat with breathable mesh back for comfort";
    }
    // -- Black bonded leather and mesh provide breathable durability
    else if(miscellaneous_ColorCategory.HasValue()
        && chairMaterialRef.HasValue("bonded leather")
        && (miscellaneous_ProductMaterial.HasValue("mesh") || seat_Material.HasValue("mesh") || backrest_Material.HasValue("mesh"))){
        var tmp = miscellaneous_Color.HasValue() ? miscellaneous_Color.FirstValue().ToUpperFirstChar() : 
        miscellaneous_ColorCategory.FirstValue().ToUpperFirstChar();
        result = $"{tmp} bonded leather and mesh provide breathable durability";
    }
    // -- Black leather-like cushioned seat is easy to clean with a damp cloth
    else if(chairMaterialRef.HasValue("leather")
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
    else if(colorFamilyRef.HasValue()
        && chairMaterialRef.HasValue("mesh")){
        var tmp = trueColorRef.HasValue() ? trueColorRef.ToUpperFirstChar() : colorFamilyRef.ToUpperFirstChar();
        result = $"{tmp} {chairMaterialRef.ToLower()} upholstery allows air to circulate";
    }
    else if(colorFamilyRef.HasValue()
        && !chairMaterialRef.HasValue("mesh")
        && !chairMaterialRef.HasValue("leather")
        && chairMaterialRef.HasValue()
        && (miscellaneous_ProductMaterial.HasValue("mesh", "mesh fabric") || seat_Material.HasValue("mesh", "mesh fabric") || backrest_Material.HasValue("mesh", "mesh fabric"))){
        var tmp = trueColorRef.HasValue() ? trueColorRef.ToUpperFirstChar() : colorFamilyRef.ToUpperFirstChar();
        result = $"{tmp} {chairMaterialRef.ToLower()} upholstery allows air to circulate";
    }
    // -- Black vinyl upholstery provides damage resistance
    else if(seat_Color.HasValue()
        && (miscellaneous_ProductMaterial.HasValue("%vinyl%") || seat_Material.HasValue("%vinyl%") || backrest_Material.HasValue("%vinyl%"))){
        result = $"{seat_Color.FirstValue().ToUpperFirstChar()} vinyl upholstery provides damage resistance";
    }
    // -- Black fabric chair for softness and a professional look
    else if(chairMaterialRef.HasValue()
        && !chairMaterialRef.HasValue("wood")
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
    else if(trueColorRef.HasValue()
        && officeChairBaseMaterial.HasValue()){
        result = $"Comes in {trueColorRef.ToLower()} and made of {officeChairBaseMaterial.ToLower()} base";
    }
    else if(seatMaterialRef.HasValue()
    && backrest_Material.HasValue()
    && seatMaterialRef.HasValue($"{backrest_Material.FirstValue()}")){
        result = $"Upholstery material: {seatMaterialRef.ToLower()}";
    }
    else if(seatMaterialRef.HasValue()){
        result = $"{seatMaterialRef} seat with breathable mesh back for comfort";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"OfficeChairsTrueColorUpholsteryMaterial⸮{result}"); 
    }
}

// --[FEATURE #3]
// --Type of support and Ergonomic Construction/Design
void Type_Of_Support_And_Ergonomic(){
    var result = ""; 
    var LumbarSupportRef = GetReference("SP-1096");
    var TypeOfSupportRef = GetReference("SP-24164");
    var ErgonomicRef = GetReference("SP-21540");
    var Backrest_LumbarSupport = A[7199];
    var Seat_AdjustableHeight = A[7188];
    var General_TiltTensionAdjustment = A[7168];
    var General_TiltLock = A[7167];
    var General_Features = A[7175];
    var General_Swivel = A[7165];
    var Armrests_Features = A[7223];
    var Base_CastersQty = A[7210];
    var Base_WheelType = A[7212];
    var General_Mechanism = A[7169];
    var Seat_Features = A[7176];
    var DimensionsWeight_BackrestHeight = A[7237];
    var Backrest_MaxHeight = A[7193];
    var Ergonomic = ErgonomicRef == "Ergonomic" ? "Ergonomic" : "This";
    
    if(!string.IsNullOrEmpty(LumbarSupportRef)
    && TypeOfSupportRef.ToLower().Contains("headrest")){
        result = $"{Ergonomic} chair provides lumbar and head support";
    }
    else if(!string.IsNullOrEmpty(LumbarSupportRef)
        && TypeOfSupportRef.ToLower().Contains("footrest")){
        result = $"{Ergonomic} chair provides lumbar and foot support";
    }
    else if(!string.IsNullOrEmpty(LumbarSupportRef)
        && TypeOfSupportRef.ToLower().Contains("lumbar")){
        result = $"{Ergonomic} chair provides lumbar support";
    }
    else if(TypeOfSupportRef.ToLower() == "lumbar support"){
        result = "Built-in lumbar support encourages proper posture";
    }
    else if(TypeOfSupportRef.ToLower() == "footrest"){
        result = "Built in footrest";
    }
    else if(TypeOfSupportRef.ToLower() == "headrest"){
        result = "Built-in headrest provides additional upper body support";
    }
    else if(Backrest_LumbarSupport.HasValue("Yes")
        && Seat_AdjustableHeight.HasValue("Yes")
        && General_TiltTensionAdjustment.HasValue("Yes")
        && General_TiltLock.HasValue("Yes")){
        result = "Lumbar support, seat height adjustment with tilt tension and tilt lock offer customized comfort";
    }
    else if(General_Features.HasValue("gas-lift height adjustment")
        && General_Swivel.HasValue("Yes")){
        result = "Gas-lift seat height adjustment and 360-degree swivel for customized ergonomic comfort";
    }
    else if(General_Features.HasValue("pneumatic seat height adjustment")
        && Armrests_Features.HasValue("sculpted")){
        result = "Pneumatic seat height adjustment and sculpted arms provide ergonomic support";
    }
    else if(General_Features.HasValue("pneumatic seat height adjustment")){
        result = "Pneumatic gas-lift height adjustment allows you to customize the chair to your specific needs";
    }
    else if(Base_CastersQty.HasValue("5")
        && Base_WheelType.HasValue("carpet")){
        result = "Five-star base with carpet casters for stability";
    }
    else if(General_Mechanism.HasValue("butterfly")){
        result = "Butterfly mechanism for easy adjustment of height and tilt";
    }
    else if(Seat_Features.HasValue("waterfall edge")){
        result = "Waterfall seat helps reduce leg strain";
    }
    else if((DimensionsWeight_BackrestHeight.HasValue() 
    && DimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
    && DimensionsWeight_BackrestHeight.FirstValueUsm() >= 22)
    || (Backrest_MaxHeight.HasValue()
    && Backrest_MaxHeight.Units.First().NameUSM.In("in")
    && Backrest_MaxHeight.FirstValueUsm() >= 22)){
        result = "High-back design offers adequate support to neck and head";
    }
    else if((DimensionsWeight_BackrestHeight.HasValue()
    && DimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
    && DimensionsWeight_BackrestHeight.FirstValueUsm() >= 15
    && DimensionsWeight_BackrestHeight.FirstValueUsm() <= 21)
    || (Backrest_MaxHeight.HasValue()
    && Backrest_MaxHeight.Units.First().NameUSM.In("in")
    && Backrest_MaxHeight.FirstValueUsm() >= 15
    && Backrest_MaxHeight.FirstValueUsm() <= 21)){
        result = "Mid-back design minimizes stress and strain";
    }
    // -- Low-back design relieves lower back strain
    else if((DimensionsWeight_BackrestHeight.HasValue()
    && DimensionsWeight_BackrestHeight.Units.First().NameUSM.In("in")
    && DimensionsWeight_BackrestHeight.FirstValueUsm() < 15)
    || (Backrest_MaxHeight.HasValue()
    && Backrest_MaxHeight.Units.First().NameUSM.In("in")
    && Backrest_MaxHeight.FirstValueUsm() < 15)){
        result = "Low-back design may help relieve lower back strain";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"TypeOfSupportAndErgonomic⸮{result}"); 
    }
}

// --[FEATURE #4] 
// --Overall Dimensions (in Inches): Height (should include range) x Width x Depth 
void Office_Chairs_Overall_Dimensions(){ 
    var result = ""; 
    var AdjustableHeightMinimumRef = GetReference("SP-21870");
    var AdjustableHeightMaximumRef = GetReference("SP-21871");
    var HeightRef = GetReference("SP-20654");
    var DepthRef = GetReference("SP-20657");
    var WidthRef = GetReference("SP-21044");
    var DimensionsWeight_MinWidth = A[7232];
    
    if(!string.IsNullOrEmpty(AdjustableHeightMinimumRef)
    && (!string.IsNullOrEmpty(HeightRef) || !string.IsNullOrEmpty(AdjustableHeightMaximumRef))
    && !string.IsNullOrEmpty(WidthRef)
    && !string.IsNullOrEmpty(DepthRef)){
        var height = !string.IsNullOrEmpty(AdjustableHeightMaximumRef) ? AdjustableHeightMaximumRef : HeightRef;
        result = $@"Overall dimensions: {AdjustableHeightMinimumRef}-{height}""H x {WidthRef}""W x {DepthRef}""D";
    }
    else if(!string.IsNullOrEmpty(HeightRef)
        && !string.IsNullOrEmpty(WidthRef)
        && !string.IsNullOrEmpty(DepthRef)){
        result = $@"Overall dimensions: {HeightRef}""H x {WidthRef}""W x {DepthRef}""D";
    }
    else if(DimensionsWeight_MinWidth.HasValue()){
        if(!string.IsNullOrEmpty(HeightRef)
        && !string.IsNullOrEmpty(DepthRef)
        && DimensionsWeight_MinWidth.Units.First().NameUSM.In("in")){
            result = $@"Overall dimensions: {HeightRef}""H x {DimensionsWeight_MinWidth.FirstValueUsm()}""W x {DepthRef}""D";
        }
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"OfficeChairsOverallDimensions⸮{result}"); 
    }
}

// --[FEATURE #5]
// --Seat Dimension (in Inches): Height (should include range) x Width x Depth
void Seat_Dimension(){
    var seatWidthRef = GetReference("SP-21885");
    var seatDepthRef = GetReference("SP-21886");
    var minimumSeatHeightRef = GetReference("SP-22033");
    var maximumSeatHeightRef = GetReference("SP-22034");
    
    if(!string.IsNullOrEmpty(minimumSeatHeightRef)
    && !string.IsNullOrEmpty(maximumSeatHeightRef)
    && !string.IsNullOrEmpty(seatWidthRef)
    && !string.IsNullOrEmpty(seatDepthRef)){
        Add($@"SeatDimension⸮Seat dimensions: {minimumSeatHeightRef}-{maximumSeatHeightRef}""H x {seatWidthRef}""W x {seatDepthRef}""D");
    }
    else if(!string.IsNullOrEmpty(seatWidthRef)
    && !string.IsNullOrEmpty(seatDepthRef)){
        Add($@"SeatDimension⸮Seat dimensions: {seatWidthRef}""W x {seatDepthRef}""D");
    }
}

// --[FEATURE #6]
// --Back Dimensions (in Inches): Width x Depth
void Back_Dimension(){ 
    var backWidthRef = GetReference("SP-21937");
    var backDepthRef = GetReference("SP-21938");

    if(!string.IsNullOrEmpty(backWidthRef)
    && !string.IsNullOrEmpty(backDepthRef)){
        Add($@"BackDimension⸮Back dimensions: {backWidthRef}""W x {backDepthRef}""D");
    }
    else if(!string.IsNullOrEmpty(backWidthRef)){
        Add($@"BackDimension⸮Back dimensions: {backWidthRef}""W");
    }
    else if(!string.IsNullOrEmpty(backDepthRef)){
        Add($@"BackDimension⸮Back dimensions: {backDepthRef}""D");
    }
}

// --[FEATURE #7]
// --Arm Type (include padding, measurements and adjustability)
void Arm_Type(){
    var result = "";
    var armTypeRef = R("SP-220").HasValue() ? R("SP-220") : R("cnet_common_SP-220");
    var General_Armrests = A[7161];
    var Armrests_AdjustableArmrests = A[7214];
    var Armrests_Features = A[7223];
    var Armrests_Shape = A[7625];
    var Armrests_PaddedArmrests = A[9499];
    var General_PiecesIncluded = A[10460];
    
    if(General_PiecesIncluded.HasValue("armless chair")
    || General_Armrests.HasValue("No")){
        result = "Armless design allows for easy movement and versatility";
    }
    else if(Armrests_AdjustableArmrests.HasValue()
        && Armrests_Features.HasValue("padded")
        && Armrests_AdjustableArmrests.Where("height", "width", "tilt").Count() > 2){
        result = "Padded adjustable arms allow you to set the height, width and angle for custom ergonomics";
    }
     else if(Armrests_AdjustableArmrests.HasValue()
        && Armrests_AdjustableArmrests.Where("height", "width").Count() > 1){
        var Armrests = Armrests_PaddedArmrests.HasValue("Yes") ? "padded " : "";
        result = $"Width and height {Armrests}adjustable arms allow you to rest your forearms comfortably";
    }
    else if(Armrests_AdjustableArmrests.HasValue("height")){
        result = Armrests_PaddedArmrests.HasValue("Yes") ? "Padded height-adjustable arms to support the shoulders and upper body" :
        "Height-adjustable arms to support the shoulders and upper body";
    }
    else if(Armrests_AdjustableArmrests.HasValue("width")){
        result = Armrests_PaddedArmrests.HasValue("Yes") ? "Padded width-adjustable arms to support the shoulders and upper body":
        "Width-adjustable arms to support the shoulders and upper body";
    }
    else if(!Armrests_AdjustableArmrests.HasValue("No")){
        result = Armrests_PaddedArmrests.HasValue("Yes") || Armrests_Features.HasValue("padded") ? "Padded adjustable arms for supporting your forearms":
        "Adjustable arms for supporting your forearms";
    }
    else if(Armrests_Shape.HasValue()
        && Armrests_AdjustableArmrests.HasValue("No")){
        result = $"Fixed {Armrests_Shape.FirstValue()} arms provide added comfort";
    }
    else if(armTypeRef.HasValue()){
        result = $"{armTypeRef} arms provide added comfort";
    }
    if(!String.IsNullOrEmpty(result)){ 
        Add($"ArmType⸮{result}"); 
    }
}

// --[FEATURE #8]
// --Tilt Mechanism type
void Tilt_Mechanism_Type(){
    var result = ""; 
    var ChairTiltTypeRef = GetReference("SP-22280");
    var General_TiltLock = A[7167];
    var General_TiltTensionAdjustment = A[7168];
    var General_Mechanism = A[7169];

    if(General_TiltLock.HasValue("Yes")
    && General_TiltTensionAdjustment.HasValue("Yes")
    && General_Mechanism.HasValue("synchro")){
        result = "Synchro-Tilt mechanism for a smooth recline with tilt tension and tilt lock for secure support";
    }
    else if(ChairTiltTypeRef == "Center-Tilt"){
        result = "Center-Tilt mechanism rotates the seat from a point at the center to recline comfortably";
    }
    else if(General_Mechanism.HasValue("synchro")){
        result = "High-performance asynchronous tilt lets you fine-tune your posture for any task";
    }
    else if(!string.IsNullOrEmpty(ChairTiltTypeRef)){
        result = General_TiltTensionAdjustment.HasValue("Yes") ? $"{ChairTiltTypeRef} with adjustable tension control for comfort": $"{ChairTiltTypeRef} for comfort";
    }
    if(!string.IsNullOrEmpty(result)){ 
        Add($"TiltMechanismType⸮{result}"); 
    }
}

// --[FEATURE #9]
// --Capacity in lbs. as well as time (if available) detailed as: "Rated for up to 250 lbs. based on an 8 hour work day" or "Rated for up to 350 lbs. based on continuous use"
void Office_Chairs_Capacity(){
    var result = ""; 
    var MaximumWeightCapacityRef = GetReference("SP-17524");
    var general_HoursADay = A[7170];

    switch(MaximumWeightCapacityRef){
        case var CapacityRef when !string.IsNullOrEmpty(CapacityRef)
        && Coalesce(MaximumWeightCapacityRef).ExtractNumbers().First() >= 250
        && general_HoursADay.HasValue() && general_HoursADay.FirstValue() > 8:
        result = $"This chair can safely support a user who weighs up to {CapacityRef} lbs. for a full work day";
        break;
        case var CapacityRef when !string.IsNullOrEmpty(CapacityRef):
        result = general_HoursADay.HasValue() ? $"Rated for up to {CapacityRef} lbs. based on an {general_HoursADay.FirstValue()} hours work day" : $"Rated for up to {CapacityRef} lbs. based on continuous use";
        break;
    }
    if(!string.IsNullOrEmpty(result)){ 
        Add($"OfficeChairsCapacity⸮{result}"); 
    }
}

// --[FEATURE #10]
// --Assembly required
void Office_Chairs_Assembly_Required(){
    var result = ""; 
    var General_AssemblyRequired = A[7173];
    var General_Features = A[7175];

    if(General_Features.HasValue("partial assembly required")){
        result = "Partial assembly required";
    }
    else if(General_AssemblyRequired.HasValue()){
        result = General_AssemblyRequired.HasValue("Yes") ? "Assembly required" :
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
    var Miscellaneous_CompliantStandards = A[380];

    if(Miscellaneous_CompliantStandards.HasValue("%ANSI%")
    && Miscellaneous_CompliantStandards.HasValue("%BIFMA%")){
        result = "Meets or exceeds ANSI/BIFMA standards";
    }
    else{
        result = Miscellaneous_CompliantStandards.HasValue("%ANSI%") ? "Meets or exceeds ANSI standard" :
        Miscellaneous_CompliantStandards.HasValue("%BIFMA%") ? "Meets or exceeds BIFMA standard" : "";
    }
    if(!string.IsNullOrEmpty(result)){ 
        Add($"CertificationsStandardsANSIBIFMA⸮{result}"); 
    }
}

// --[FEATURE #12] - All 
// --Warranty 

//2324927166253 "Office Chairs END" "Serhii.O" §§ 

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
	var MonitorMountStandTypeRef = R("SP-18455").HasValue() ? R("SP-18455").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18455").HasValue() ? R("cnet_common_SP-18455").Replace("<NULL>", "").Text : "";

	// -- Raise your monitor to a comfortable viewing height (https://www.staples.com/Mind-Reader-Harmonize-Adjustable-Plastic-Monitor-Stand-with-Drawer-Black/product_2599043)
	if(!String.IsNullOrEmpty(MonitorMountStandTypeRef)){
		result = $"Raise your monitor to a comfortable viewing height with this monitor {MonitorMountStandTypeRef.ToLower()}";
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

//590434034623141470 "Monitor Mounts & Stands END" "Serhii.O" §§ 

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

//135391203158855 "Filing Accessories END" "Serhii.O" §§ 

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
var result = "";
	// Gooseneck Microphone|Headset Microphone|Ear Cushion|Ear Loops|Shotgun Microphone|Lapel Microphone|Microphone Holder|Desktop Microphone|Condenser Microphone|Handheld Microphone|Desktop Microphone|Computer Headset|Phone Headset
	var HeadsetMicrophoneTypeRef = R("SP-18064").HasValue() ? R("SP-18064") : 
        R("cnet_common_SP-18064").HasValue() ? R("cnet_common_SP-18064") : Coalesce("");
    var Type = SPEC["ES"].GetLines().Select(l => l.Body).Flatten(" ");
    // --Plantronics® EncorePro HW510V® Black monaural voice tube headset is suitable for customer service centers and offices. (https://www.staples.com/product_IM1XU0898)
    // --headset is perfect for everyday use with compatible electronic devices (https://www.staples.com/product_24274917)
    if(HeadsetMicrophoneTypeRef.ToLower().In("%headset%", "%Headset%")){
    	result = $"{HeadsetMicrophoneTypeRef.Text.ToLower().ToUpperFirstChar()} is perfect for everyday use with compatible electronic devices";
    }
    else if(SPEC["ES"].GetLines().Select(l => l.Body).Flatten(" ").In("%headset%", "%Headset%")){
        result = "This headset is perfect for everyday use with compatible electronic devices";
    }
	if (!String.IsNullOrEmpty(result)) {
        Add($"HeadsetMicrophoneTypeUse⸮{result}");
    }
} 

// --[FEATURE #2] 
// --Cord (length and type) 
void Cord_Headset_Microphone(){ 
    var result = "";
	var CableLength_ftRef = R("SP-21212").HasValue() ? R("SP-21212").Text : 
        R("cnet_common_SP-21212").HasValue() ? R("cnet_common_SP-21212").Text : "";
    var AudioOutput_CableLength = Coalesce(A[8577]); // Audio Output - Cable Length
    var CableDetails_Length = Coalesce(A[317]); // Cable Details - Length

    // --This microphone features 3.5 mm audio jack to connect to audio sources and 6.5' cable for extra reach
    // --Provides wired connectivity with 0.39' cable length (https://www.staples.com/product_IM12MZ909)
    // --Plantronics® Male patch cord has 18 of length and right angle plug for easy connection. (https://www.staples.com/product_IM1F35659)
    // --patch cord has 18 of length and right angle plug for easy connection. (https://www.staples.com/product_IM1F35659)
    // --The headphones with cord length of 2.5 m, has nickel-plated plug and ferrite magnet. (https://www.staples.com/product_IM14T7276)
    if(!String.IsNullOrEmpty(CableLength_ftRef)){
    	result = $"Provides wired connectivity with {CableLength_ftRef}' cable";
    }
    else if(AudioOutput_CableLength.HasValue()){
    	switch(AudioOutput_CableLength.Units.First().NameUSM){
    		case var Unit when Unit.In("%in%"):
    		result = $@"Provides wired connectivity with {AudioOutput_CableLength.Values.First().ValueUSM}"" cable";
    		break;
    		case var Unit when Unit.In("%ft%"):
    		result = $"Provides wired connectivity with {AudioOutput_CableLength.Values.First().ValueUSM}' cable";
    		break;
    	}
    }
    else if(CableDetails_Length.HasValue()){
    	switch(CableDetails_Length.Units.First().NameUSM){
    		case var Unit when Unit.In("%in%"):
    		result = $@"Provides wired connectivity with {CableDetails_Length.Values.First().ValueUSM}"" cable";
    		break;
    		// S21308633 - Provides wired connectivity with 8'L cable
    		case var Unit when Unit.In("%ft%"):
    		result = $"Provides wired connectivity with {CableDetails_Length.Values.First().ValueUSM}' cable";
    		break;
    	}
    }
	if (!String.IsNullOrEmpty(result)) {
        Add($"CordHeadsetMicrophone⸮{result}");
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
// --Headset with foldable design ensures comfortable fit behind neck design. (https://www.staples.com/product_788621) 
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
HeadsetDesignColor = "Earbuds deliver optimum sound quality and comfortable wearing for all-day use"; 
} 
else if(AudioOutput_HeadphonesEarPartsType.HasValue("ear-bud") 
&& !String.IsNullOrEmpty(TrueColorRef)){ 
HeadsetDesignColor = $"Comfortable-wearing {TrueColorRef.ToLower()} earbuds for all-day use"; 
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
// S21308633 - ABS plastic construction provides reliability where it matters 
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
var AudioOutput_Details = Coalesce(A[9410]); // Audio Output - Details 

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
    	InterfaceHeadsetMicrophone = "Bluetooth for wireless use";
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
    	InterfaceHeadsetMicrophone = $"USB {HeadsetMicrophoneTypeRef.ToLower()} can be used across many different devices without hassle";
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
    var result = "";
	var MicrophoneSensitivityRef = R("SP-22015").HasValue() ? R("SP-22015").Text : 
        R("cnet_common_SP-22015").HasValue() ? R("cnet_common_SP-22015").Text : "";
    var MicrophoneSensitivity = A[278];
    
    // --Enables safe listening experience with maximum 104 dB limited sensitivity by ActiveGard™ SPL (https://www.staples.com/product_IM1RG6774)
    // --The 113 dB/1 mW sensitivity ensures noiseless clear audio (https://www.staples.com/product_IM1TW9698)
    // --105 dB/1 mW sensitivity for better audio fidelity (https://www.staples.com/product_208098)
    if(!String.IsNullOrEmpty(MicrophoneSensitivityRef)
    && MicrophoneSensitivity.HasValue()){
    	result = $"{MicrophoneSensitivityRef} {A[278].Units.First().Name} microphone sensitivity ensures noiseless clear audio";
    }
	if (!String.IsNullOrEmpty(result)) {
        Add($"SensitivityHeadsetMicrophone⸮{result}");
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
			ImpedanceHeadsetMicrophone = $"Impedance: {Impedance} Ohms";
			break;
			case var Impedance when Impedance > 99:
			ImpedanceHeadsetMicrophone = $"{Impedance} Ohms impendence for superior performance";
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

//168130738704140478 "Headsets & Microphones END" "Serhii.O" §§ 

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
var MouseTrackingMethodRef = R("SP-13644").HasValue() ? R("SP-13644").Replace("<NULL>", "").Text : 
R("cnet_common_SP-13644").HasValue() ? R("cnet_common_SP-13644").Replace("<NULL>", "").Text : ""; 
// Gaming|Non Gaming 
var GamingRef = R("SP-13567").HasValue() ? R("SP-13567").Replace("<NULL>", "").Text : 
R("cnet_common_SP-13567").HasValue() ? R("cnet_common_SP-13567").Replace("<NULL>", "").Text : ""; 

if(!String.IsNullOrEmpty(MouseTrackingMethodRef)){ 
switch(MouseTrackingMethodRef){ 
case var TrackingMethod when TrackingMethod.Equals("Advanced Optical"): 
MouseTrackingMethod = $"Advanced Optical Mouse delivers reliable connectivity along with the responsive performance of a high-resolution optical sensor"; 
break; 
// --BlueTrack mouse for improved precision 
// --BlueTrack technology combines laser precision and optical power 
case var TrackingMethod when TrackingMethod.Equals("Bluetrack"): 
MouseTrackingMethod = $"BlueTrack technology combines laser precision and optical power for improved precision"; 
break; 
// --Bluetooth connectivity supports a clutter-free workplace 
case var TrackingMethod when TrackingMethod.Equals("Bluetooth"): 
MouseTrackingMethod = $"Bluetooth connectivity supports a clutter-free workplace"; 
break; 
// --Darkfield technology for improved precision 
case var TrackingMethod when TrackingMethod.Equals("Darkfield"): 
MouseTrackingMethod = $"Darkfield technology for improved precision"; 
break; 
// --Precision laser tracking works on most surfaces 
case var TrackingMethod when TrackingMethod.Equals("Laser"): 
MouseTrackingMethod = $"Precision laser tracking works on most surfaces"; 
break; 
case var TrackingMethod when TrackingMethod.Equals("Optical"): 
MouseTrackingMethod = $"Optical mouse allows you to have complete control over cursor movement"; 
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
// -- 3-button design for speed and enhanced functionality (https://www.staples.com/product_784641) 
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
GestureFunction = $"Includes {ScrollWheelRef.ToLower()} for easy movement through documents and pages"; 
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
// S2133532 - Signal lets you control your cursor from up to 3.3 away 
else if(InputDevice_MaxOperatingDistance.HasValue()){ 
ControlCursor = $"Signal lets you control your cursor from up to {InputDevice_MaxOperatingDistance.Values.First().ValueUSM}{InputDevice_MaxOperatingDistance.Units.First().NameUSM.Replace("ft", "\'").Replace("in", "\"")} away"; 
} 
if(!String.IsNullOrEmpty(ControlCursor)){ 
Add($"ControlCursor⸮{ControlCursor}"); 
} 
} 

// --[FEATURE #16] - All 
// --Warranty information 

//5904340310149141983 "Computer Mice END" "Serhii.O" §§ 

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
result = $@"The {Size}"" curved monitor draws you in and won't set you back"; 
break; 
// -- This large format Full HD monitor boasts a diagonal of stunning 27 (https://www.staples.com/product_2708012) 
case var Size when Size >= 27 && MonitorResolutionRef.Contains("1080"): 
result = $@"This large-format full HD monitor has a diagonal measurement of {MonitorScreenSizeInInchesRef}"""; 
break; 
case var Size when Size >= 27 && UltraHD4KRef.Equals("4K Ultra HD"): 
result = $@"This large-format 4K ultra HD monitor has a diagonal measurement of {Size}"""; 
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
var DisplayPortRef = R("SP-21023").HasValue() ? R("SP-21023").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21023").HasValue() ? R("cnet_common_SP-21023").Replace("<NULL>", "").Text : ""; // HDMI Ports 
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
result = "VGA/HDMI: ready to connect with both VGA and HDMI ports"; 
} 
// -- DVI & VGA inputs so you can easily power and extend the enjoyment from your smartphone or tablet(https://www.staples.com/Acer-K242HYL-Abd-23-8-Widescreen-LCD-Monitor/product_2738924) 
else if(MonitorVGAInputRef.ToLower().Equals("yes") 
&& DVIInputRef.ToLower().Equals("yes")){ 
result = "DVI and VGA inputs allow you to display the contents of your smartphone or tablet screen onto the monitor"; 
} 
else if(HDMIPortsRef.ToLower().Equals("yes") 
&& DisplayPortRef.ToLower().Equals("yes")){ 
result = "Provides HDMI and DisplayPort interfaces"; 
} 
else if(!string.IsNullOrEmpty(HDCPSupportedRef) 
&& !HDCPSupportedRef.ToLower().Equals("no")){ 
result = "Provides HDMI interface"; 
} 
else if(!string.IsNullOrEmpty(MonitorVGAInputRef) 
&& !MonitorVGAInputRef.ToLower().Equals("no")){ 
result = "Provides VGA interface"; 
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
result = $"Viewing angle: {MaxViewingAngle_HorizontalRef} degrees horizontal, {MaxViewingAngle_VerticalRef} degrees vertical"; 
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
result = "Fully integrated sound: get powerful built-in audio without cluttering your desk with external speakers"; 
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

//590442000142219 "Computer Monitors END" "Serhii.O" §§ 

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
    var videoCableTypeRef = GetReference("SP-18471");
    var cableTypeRef = GetReference("SP-19208");
    var interfaceRef = GetReference("SP-19209");
    var ceCables_RightConnectorType = A[2058];
    
    if(!string.IsNullOrEmpty(videoCableTypeRef)
    && !string.IsNullOrEmpty(interfaceRef)){
        Add($"TypeOfCableInterfaceUse⸮This {videoCableTypeRef.ToLower()} is ideal to connect your {interfaceRef.ToLower()}-compatible devices");
    }
    else if(!string.IsNullOrEmpty(cableTypeRef)
    && !string.IsNullOrEmpty(interfaceRef)){
        Add($"TypeOfCableInterfaceUse⸮This {cableTypeRef.ToLower()} is ideal to connect your {interfaceRef.ToLower()}-compatible devices");
    }
    else if(ceCables_RightConnectorType.HasValue("%3.5%")
    && !string.IsNullOrEmpty(cableTypeRef)){
        Add($"TypeOfCableInterfaceUse⸮This {cableTypeRef.ToLower()} is ideal to connect your 3.5mm-compatible devices");
    }
} 

// --[FEATURE #2] 
// --Dimensions (in Inches) if less than 3 feet. If more than 3 feet dimensions (in Feet). Include gauge of wire (If Applicable) 
void Dimensions_Audio_Video_Cables(){ 
var DimensionsAudioVideoCables = ""; 
var CableLength_Inches_Ref = R("SP-21325").HasValue() ? R("SP-21325").Text : 
R("cnet_common_SP-21325").HasValue() ? R("cnet_common_SP-21325").Text : ""; // Cable Length (Inches) 
var CableLength_ft_Ref = !R("SP-21877").Text.Equals("<NULL>") ? R("SP-21877").Text : 
R("cnet_common_SP-21877").HasValue() ? R("cnet_common_SP-21877").Text : ""; // Cable Length (ft) 
var CableGaugeAWGRef = !R("SP-24322").Text.Equals("<NULL>") ? R("SP-24322").Text : 
R("cnet_common_SP-24322").HasValue() ? R("cnet_common_SP-24322").Text : ""; // Cable Gauge (AWG) 
var CableLength_Inches = 0; 
var CableLength_ft = 0; 

if(!String.IsNullOrEmpty(CableLength_Inches_Ref)){ 
var Inches = Coalesce(CableLength_Inches_Ref).ExtractNumbers(); 
CableLength_Inches = Inches.Any() ? Inches.First() : 0; 
} 
if(!String.IsNullOrEmpty(CableLength_ft_Ref)){ 
var ft = Coalesce(CableLength_ft_Ref).ExtractNumbers(); 
CableLength_ft = ft.Any() ? ft.First() : 0; 
} 
if(CableLength_Inches != 0 
&& CableLength_ft < 3.0 
&& !String.IsNullOrEmpty(CableGaugeAWGRef)){ 
DimensionsAudioVideoCables = $@"{CableLength_Inches}"" length {CableGaugeAWGRef} AWG cable"; 
} 
else if(CableLength_ft > 2.9 
&& !String.IsNullOrEmpty(CableGaugeAWGRef)){ 
DimensionsAudioVideoCables = $@"{CableLength_ft}' length {CableGaugeAWGRef} AWG cable"; 
} 
else if(CableLength_ft < 3.0 
&& CableLength_Inches != 0 
&& CableLength_Inches < 36.0){ 
DimensionsAudioVideoCables = $@"Cable Length {CableLength_Inches}"""; 
} 
else if(CableLength_ft > 2.9){ 
DimensionsAudioVideoCables = $@"{CableLength_ft}' length cable"; 
} 
if(!String.IsNullOrEmpty(DimensionsAudioVideoCables) && !DimensionsAudioVideoCables.Contains("Cable Length 0")){ 
Add($"DimensionsAudioVideoCables⸮{DimensionsAudioVideoCables}"); 
} 
} 

// --[FEATURE #3] 
// --Connectors & Plating 
void Connectors_Plating(){ 
var result = ""; 
var End1ConnectorTypeRef = R("SP-21232").HasValue() ? R("SP-21232").Text : 
R("cnet_common_SP-21232").HasValue() ? R("cnet_common_SP-21232").Text : ""; // End 1 Connector Type 
var End2ConnectorTypeRef = R("SP-21233").HasValue() ? R("SP-21233").Text : 
R("cnet_common_SP-21233").HasValue() ? R("cnet_common_SP-21233").Text : ""; // End 1 Connector Type 
var ConnectorGenderRef = R("SP-18423").HasValue() ? R("SP-18423").Text : 
R("cnet_common_SP-18423").HasValue() ? R("cnet_common_SP-18423").Text : "";// Connector Gender 
var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features 
var SystemPowerCables_Cable_CableFeatures = Coalesce(A[1979]); /// System & Power Cables - Cable - Cable Features 

if(!String.IsNullOrEmpty(End1ConnectorTypeRef) 
&& !String.IsNullOrEmpty(End2ConnectorTypeRef) 
&& !String.IsNullOrEmpty(ConnectorGenderRef)){ 
var AdditionalFeatures = 
Cables_Cable_AdditionalFeatures.HasValue() && Cables_Cable_AdditionalFeatures.Where("corrosion-resistant connectors").Any() ? 
$" {Cables_Cable_AdditionalFeatures.Where("corrosion-resistant connectors").First().Value().Replace("corrosion-resistant connectors"," with corrosion resistance").Replace("Yes"," with corrosion resistance").Replace("No", "")}" : ""; 
var CableFeatures = 
SystemPowerCables_Cable_CableFeatures.HasValue() && SystemPowerCables_Cable_CableFeatures.Where("corrosion-resistant connectors").Any() ? 
SystemPowerCables_Cable_CableFeatures.Where("corrosion-resistant connectors").First().Value().Replace("corrosion-resistant connectors"," with corrosion resistance").Replace("Yes"," with corrosion resistance").Replace("No", "") : ""; 

var GenderFirst = ConnectorGenderRef.Split(" to ").Count() > 1 ? $" ({ConnectorGenderRef.Split(" to ").First()}" : ""; 
var GenderLast = ConnectorGenderRef.Split(" to ").Count() > 1 ? $" to {ConnectorGenderRef.Split(" to ").Last()})" : ""; 
result = $"{End1ConnectorTypeRef} to {End2ConnectorTypeRef}{GenderFirst}{GenderLast} connectors{AdditionalFeatures}{CableFeatures}"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"ConnectorsPlating⸮{result}"); 
} 
} 

// --[FEATURE #4] 
// --Cable jacket material 
void Cable_Jacket_Material(){ 
var result = ""; 
var CableJacketMaterialRef = R("SP-21209").HasValue() ? R("SP-21209").Text : 
R("cnet_common_SP-21209").HasValue() ? R("cnet_common_SP-21209").Text : ""; // Cable Jacket Material 

switch(CableJacketMaterialRef){ 
case var JacketMaterial when JacketMaterial.Equals("PVC"): 
result = "PVC jacket protects the conductor from environmental factors"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Aluminum"): 
result = "Cable Jacket Material: Aluminum"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Plastic"): 
result = "To enhance durability this cable is enclosed in a plastic jacket"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Rubber"): 
result = "Cable Jacket Material: Rubber"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("SVT"): 
result = "Cable Jacket Material: SVT"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Nylon/Cotton/Kevlar Polymer"): 
result = "Cable Jacket Material: Nylon/Cotton/Kevlar Polymer"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("CMG"): 
result = "It features CMG rated jacket for in-wall applications"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Polypropylene"): 
result = "Cable Jacket Material: Polypropylene"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Nylon"): 
result = "Cable Jacket Material: Nylon"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Rubberized Plastic"): 
result = "Cable Jacket Material: Rubberized Plastic"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("CL2"): 
result = "CL2 rating ensures that your cable installation complies with fire safety codes and insurance requirements"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("TPE"): 
result = "Cable Jacket Material: TPE"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Plenum"): 
result = "Cable Jacket Material: Plenum"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Xtra-Flex PVC"): 
result = "Xtra flexible PVC jacket offers double shielding against outside noise"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Silver Braid Shielding"): 
result = "Cable Jacket Material: Silver Braid Shielding"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Duraflex"): 
result = "Duraflex jacket for easy routing and installation in tight spaces"; 
break; 
case var JacketMaterial when JacketMaterial.Equals("Polyethylene"): 
result = "Cable Jacket Material: Polyethylene"; 
break; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"CableJacketMaterial⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Specific Audio/Visual Feature (if applicable) 
void Specific_Audio_Visual_Feature(){ 
    var result = "";
    var Cables_Cable_CableKeyFeatures = Coalesce(A[10424]); // Cables - Cable - Cable Key Features
    var SystemPowerCables_Cable_CableFeatures = Coalesce(A[1979]); // System & Power Cables - Cable - Cable Features
    var Cables_Cable_TechnologyFeatures = Coalesce(A[2478]); // Cables - Cable - Technology Features
    var notMolded = "";
    
    if(Cables_Cable_CableKeyFeatures.HasValue()
    && SystemPowerCables_Cable_CableFeatures.HasValue()){
        notMolded = Cables_Cable_CableKeyFeatures.WhereNot("%molded%").Any() ? $"{Cables_Cable_CableKeyFeatures.WhereNot("%molded%").Flatten(", ")}, " : "";
        result = $"Features: {notMolded}{SystemPowerCables_Cable_CableFeatures.Values.Select(s => s.Value()).FlattenWithAnd()}";
    }
    //  S21590273 - Features: plenum, 4K support, active cable, HDMI CEC support (Consumer Electronics Control)
    else if(Cables_Cable_CableKeyFeatures.HasValue()
    && Cables_Cable_TechnologyFeatures.HasValue()){
        notMolded = Cables_Cable_CableKeyFeatures.WhereNot("%molded%").Any() ? $"{Cables_Cable_CableKeyFeatures.WhereNot("%molded%").Flatten(", ")}, " : "";
        result = $"Features: {notMolded}{Cables_Cable_TechnologyFeatures.Values.Select(s => s.Value()).FlattenWithAnd().Replace(" carat", "-carat")}";
    }
    else if(Cables_Cable_CableKeyFeatures.HasValue()
    && !Cables_Cable_CableKeyFeatures.HasValue("%molded%")){
        result = $"Features: {Cables_Cable_CableKeyFeatures.Values.Select(s => s.Value()).FlattenWithAnd().Replace(" carat", "-carat")}";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"SpecificAudioVisualFeature⸮{result}");
    }
} 

// --[FEATURE #6] 
// --Connector finish and/or Connector material 
void Connector_Finish_And_Or_Connector_Material(){ 
var result = ""; 
var ConnectorFinishRef = R("SP-21208").HasValue() ? R("SP-21208").Text : 
R("cnet_common_SP-21208").HasValue() ? R("cnet_common_SP-21208").Text : ""; // Connector Finish 
var InterfaceRef = R("SP-19209").HasValue() ? R("SP-19209").Text : 
R("cnet_common_SP-19209").HasValue() ? R("cnet_common_SP-19209").Text : ""; // Interface 

switch(ConnectorFinishRef){ 
case var ConnectorFinish when ConnectorFinish.Equals("Gold-Plated") 
&& InterfaceRef.Contains("HDMI"): 
result = "Gold-plated connectors for the highest signal transfer rate"; 
break; 
case var ConnectorFinish when ConnectorFinish.Equals("Gold-Plated"): 
result = "Gold-plated connectors for maximum conductivity"; 
break; 
case var ConnectorFinish when ConnectorFinish.Equals("Nickel-Plated"): 
result = "Nickel-plated connectors that are rustproof"; 
break; 
case var ConnectorFinish when ConnectorFinish.Equals("Silver-Plated"): 
result = "Silver connector plating for good conductivity"; 
break; 
case var ConnectorFinish when ConnectorFinish.Equals("Tinned Copper"): 
result = "Tinned copper conductor for better conductivity"; 
break; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"ConnectorFinishAndOrConnectorMaterial⸮{result}"); 
} 
} 

// --[FEATURE #7] - All 
// --Certification & standards 

// --[FEATURE #8] 
// --Serial ATA (SATA) 
void Serial_ATA(){ 
var result = ""; 
var Cables_Cable_LeftConnectorType = Coalesce(A[588]); // Cables - Cable - Left Connector Type 
var Cables_Cable_RightConnectorType = Coalesce(A[589]); // Cables - Cable - Right Connector Type 

if(Cables_Cable_LeftConnectorType.HasValue("%serial%ata%") 
|| Cables_Cable_RightConnectorType.HasValue("%serial%ata%")){ 
result = "Serial ATA (SATA) Yes" ; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"SerialATA⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void Provides_Protection(){ 
var result = ""; 
var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features 

if(Cables_Cable_AdditionalFeatures.HasValue("EMI/RFI protection")){ 
result = "Provides protection against RF and EM"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"ProvidesProtection⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void HDCP_Compliant(){ 
var result = ""; 
var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features 

if(Cables_Cable_AdditionalFeatures.HasValue("HDCP compliant")){ 
result = "HDCP compliant to provide the highest level of signal quality"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HDCPCompliant⸮{result}"); 
} 
} 

// --[FEATURE #11] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void Latched_Connectors(){ 
var result = ""; 
var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features 

if(Cables_Cable_AdditionalFeatures.HasValue("latched")){ 
result = "Latched connectors avoid accidental disconnections"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"LatchedConnectors⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Use for additional product and/or manufacturer information relevant to the customer buying decision 
void Strain_Relief(){ 
var result = ""; 
var Cables_Cable_AdditionalFeatures = Coalesce(A[1549]); // Cables - Cable - Additional Features 
var Cables_Cable_CableKeyFeatures = Coalesce(A[10424]); // Cables - Cable - Cable Key Features 

if(Cables_Cable_AdditionalFeatures.HasValue("strain relief") 
&& Cables_Cable_CableKeyFeatures.HasValue("%molded%")){ 
result = "Molded strain relief for long-lasting usage"; 
} 
else if(Cables_Cable_AdditionalFeatures.HasValue("strain relief")){ 
result = "Strain relief minimizes wear and tear"; 
} 
else if(Cables_Cable_CableKeyFeatures.HasValue("%molded%")){ 
result = "Cable features molded construction"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"StrainRelief⸮{result}"); 
} 
} 

// --[FEATURE #13] - All 
// --Warranty Information 

//590434031426140452 "Audio/Video Cables END" "Serhii.O" §§ 

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
var TVSize = R("SP-18763").HasValue() ? R("SP-18763").Text : 
R("cnet_common_SP-18763").HasValue() ? R("cnet_common_SP-18763").Text : ""; 
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
var TVType = R("SP-18760").HasValue() ? R("SP-18760").Text : 
R("cnet_common_SP-18760").HasValue() ? R("cnet_common_SP-18760").Text : ""; 
var TVHDQuality = R("SP-18761").HasValue() ? R("SP-18761").Text : 
R("cnet_common_SP-18761").HasValue() ? R("cnet_common_SP-18761").Text : ""; 

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
var Enabled3DRef = R("SP-18762").HasValue() ? R("SP-18762").Text : 
R("cnet_common_SP-18762").HasValue() ? R("cnet_common_SP-18762").Text : ""; 

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
var TelevisionsDisplayResolution = R("SP-14438").HasValue() ? R("SP-14438").Text : 
R("cnet_common_SP-14438").HasValue() ? R("cnet_common_SP-14438").Text : ""; 

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
var result = ""; 
var IntergratedIOFeaturesRef = R("SP-23601").HasValue() ? R("SP-23601").Text : 
R("cnet_common_SP-23601").HasValue() ? R("cnet_common_SP-23601").Text : ""; 
// --Has two 10 W built-in speakers and tuner enhance the experience with superior sound 
// --Built-in 2 x 10 W speaker for enhanced sound 

if(!String.IsNullOrEmpty(IntergratedIOFeaturesRef)){ 
result = $"Features {IntergratedIOFeaturesRef} for enhanced sound"; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"IntergratedIOFeatures⸮{result}"); 
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
var HDMI = Connections_ConnectorType.Where("%HDMI%").Match(1415).Values().Count() > 0 ? $"{Connections_ConnectorType.Where("%HDMI%").Match(1415).Values().Select(s => Coalesce(s).ExtractNumbers().Any() ? Coalesce(s).ExtractNumbers().Last() : 1).Sum()} x HDMI, " : ""; 
var USB = Connections_ConnectorType.Where("%USB%").Match(1415).Values().Count() > 0 ? $"{Connections_ConnectorType.Where("%USB%").Match(1415).Values().Select(s => Coalesce(s).ExtractNumbers().Any() ? Coalesce(s).ExtractNumbers().Last() : 1).Sum()} x USB, " : ""; 
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
var WithStand = DimensionsWeightDetails_Details.Where("panel with stand").Match(519).HasValue() && DimensionsWeightDetails_Details.Where("panel with stand").Match(519).Values().ExtractNumbers().Any() ? DimensionsWeightDetails_Details.Where("panel with stand").Match(519).Values().ExtractNumbers().First() : 0; 
var panelWithStand = WithStand != 0 ? $"{Math.Round(WithStand * 2.20462, 1)} lbs. (with stand), " : ""; 
var WithoutStand = DimensionsWeightDetails_Details.Where("panel without stand").Match(519).HasValue() && DimensionsWeightDetails_Details.Where("panel without stand").Match(519).Values().ExtractNumbers().Any() ? DimensionsWeightDetails_Details.Where("panel without stand").Match(519).Values().ExtractNumbers().First() : 0; 
var panelWithoutStand = WithoutStand != 0 ? $"{Math.Round(WithoutStand * 2.20462, 1)} lbs. (without stand)" : ""; 
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

//591714025317164870 "TVs END" "Serhii.O" §§ 

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
var result = ""; 
var SSDCapacityRef = REQ.GetVariable("SP-18331").HasValue() ? REQ.GetVariable("SP-18331").Replace("<NULL>", "").Text : 
R("SP-18331").HasValue() ? R("SP-18331").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18331").HasValue() ? R("cnet_common_SP-18331").Replace("<NULL>", "").Text : ""; 
var HDDCapacityRef = REQ.GetVariable("SP-18431").HasValue() ? REQ.GetVariable("SP-18431").Replace("<NULL>", "").Text : 
R("SP-18431").HasValue() ? R("SP-18431").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18431").HasValue() ? R("cnet_common_SP-18431").Replace("<NULL>", "").Text : ""; 
var HardDrive_SSDFormFactor = Coalesce(A[7705]); // Hard Drive - SSD Form Factor 
var HardDrive_SpindleSpeed = Coalesce(A[84]); // Hard Drive - Spindle Speed 

if(!String.IsNullOrEmpty(SSDCapacityRef) 
&& HardDrive_SSDFormFactor.HasValue("eMMC")){ 
result = Coalesce(SSDCapacityRef).ExtractNumbers().First() >= 32 ? 
// S21486289 - 128GB eMMC hard drive provides ample photo storage 
$"{SSDCapacityRef} eMMC hard drive provides ample photo storage" : // S16715349 - 16GB eMMC flash hard drive offers adequate internal storage space 
$"{SSDCapacityRef} eMMC flash hard drive internal storage space"; 
} 
else if(!String.IsNullOrEmpty(SSDCapacityRef) 
& !String.IsNullOrEmpty(HDDCapacityRef)){ 
result = $"{SSDCapacityRef} SSD hard drive and {HDDCapacityRef} HDD hard drive enables you to store thousands of files"; 
} 
else if(!String.IsNullOrEmpty(SSDCapacityRef)){ 
result = SSDCapacityRef.Contains("TB") ? $"{SSDCapacityRef} SSD hard drive enables you to store thousands of files" : 
$"{SSDCapacityRef} SSD keeps your running programs active while your computer resumes from suspension in seconds"; 
} 
else if(!String.IsNullOrEmpty(HDDCapacityRef)){ 
if(SSDCapacityRef.Contains("TB")){ 
result = HardDrive_SpindleSpeed.HasValue() && HardDrive_SpindleSpeed.Units.First().Name.In("rpm") ? 
$"{HDDCapacityRef} HDD SATA {HardDrive_SpindleSpeed.FirstValue()}{HardDrive_SpindleSpeed.Units.First().Name} hard drive for storage" : 
$"{HDDCapacityRef} HDD gives you tons of storage space"; 
} 
else{ 
result = HardDrive_SpindleSpeed.HasValue() && HardDrive_SpindleSpeed.Units.First().Name.In("rpm") ? 
// S4481330 - 160GB HDD storage drive with 5400rpm spindle speed provides adequate internal storage space 
$"{HDDCapacityRef} HDD storage drive with {HardDrive_SpindleSpeed.FirstValue()}{HardDrive_SpindleSpeed.Units.First().Name} spindle speed provides adequate internal storage space" : 
// --500GB HDD provides enough space to store all your documents and multimedia content 
$"{HDDCapacityRef} HDD provides enough space to store all your documents and multimedia content" ; 
} 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"HardDriveType⸮{result}"); 
} 
} 

// --[FEATURE #3] - All 
// --Computer Operating System 

// --[FEATURE #4] - All 
// --Installed RAM 

// --[FEATURE #5] 
// --Laptop Memory Type 
void Laptop_Memory_Type(){ 
var result = ""; 
var RAM_Technology = Coalesce(A[56]); // RAM - Technology 
var LaptopMemoryTypeRef = REQ.GetVariable("SP-3961").HasValue() ? REQ.GetVariable("SP-3961").Replace("<NULL>", "").Text : 
R("SP-3961").HasValue() ? R("SP-3961").Replace("<NULL>", "").Text : 
R("cnet_common_SP-3961").HasValue() ? R("cnet_common_SP-3961").Replace("<NULL>", "").Text : ""; 

switch(RAM_Technology){ 
// S18674103 - DDR4 RAM: With its higher bandwidth, everything from multi-tasking to playing games gets a performance boost 
case var a when a.HasValue("DDR4%"): 
result = "DDR4 RAM: with its higher bandwidth, everything from multi-tasking to playing games gets a performance boost"; 
break; 
// S21500408 - DDR3L SDRAM memory technology for high-performance solution 
case var a when a.HasValue(): 
result = $"{RAM_Technology.FirstValue()} memory technology for high-performance solution"; 
break; 
default:{ 
if(!string.IsNullOrEmpty(LaptopMemoryTypeRef)){ 
if(LaptopMemoryTypeRef.Contains("DDR4")){ 
result = "DDR4 RAM: with its higher bandwidth, everything from multi-tasking to playing games gets a performance boost"; 
} 
else 
result = $"{LaptopMemoryTypeRef} memory technology for high-performance solution"; 
} 
} 
break; 
} 
if(!String.IsNullOrEmpty(result)){ 
Add($"LaptopMemoryType⸮{result}"); 
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

//3364010221167289 "Laptops END" "Serhii.O" §§ 

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
// SSD Capacity - 200GB|180GB|1TB|250GB|900GB|240GB|300GB|800GB|600GB|256GB|96GB|4TB|3TB|2TB|1.6TB|700GB|960GB|480GB|400GB|512GB|500GB|160GB|4GB|16GB|36GB|32GB|30GB|525GB|64GB|60GB|90GB|450GB|100GB|80GB|128GB|120GB|146GB|8GB|5TB+
var SSDCapacity = REQ.GetVariable("SP-18331").HasValue() ? REQ.GetVariable("SP-18331").Replace("<NULL>", "").Text : 
R("SP-18331").HasValue() ? R("SP-18331").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18331").HasValue() ? R("cnet_common_SP-18331").Replace("<NULL>", "").Text : ""; 
// HDD Capacity - Under 320GB|512GB|1.2TB|320GB|5TB|4.5TB|640GB|450GB|1.5TB|1TB|750GB|6TB +|5.5TB|500GB|3.5TB|3TB|2.5TB|2TB|4TB 
var HDDCapacity = REQ.GetVariable("SP-18431").HasValue() ? REQ.GetVariable("SP-18431").Replace("<NULL>", "").Text : 
R("SP-18431").HasValue() ? R("SP-18431").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18431").HasValue() ? R("cnet_common_SP-18431").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(HDDCapacity) 
&& !String.IsNullOrEmpty(SSDCapacity)){ 
HardDriveMemoryTypeCapacityOfDesktop = $"{HDDCapacity} HDD and {SSDCapacity} SDD hard drives enables you to store thousands of files"; 
} 
else if(Coalesce(HDDCapacity).In("16GB", "32GB")){ 
HardDriveMemoryTypeCapacityOfDesktop = $"{HDDCapacity} HDD hard drive for ample storage"; 
} 
else if(Coalesce(SSDCapacity).In("16GB", "32GB")){ 
HardDriveMemoryTypeCapacityOfDesktop = $"{SSDCapacity} SSD hard drive for ample storage"; 
} 
else if(!string.IsNullOrEmpty(HDDCapacity)){ 
HardDriveMemoryTypeCapacityOfDesktop = $"{HDDCapacity} HDD hard drive enables you to store thousands of files"; 
} 
else if(!string.IsNullOrEmpty(SSDCapacity)){ 
HardDriveMemoryTypeCapacityOfDesktop = $"{SSDCapacity} SSD hard drive enables you to store thousands of files"; 
} 
if(!string.IsNullOrEmpty(HardDriveMemoryTypeCapacityOfDesktop)){ 
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
// Desktop Optical Drive 
var DesktopOpticalDriveRef = REQ.GetVariable("SP-3999").HasValue() ? REQ.GetVariable("SP-3999").Replace("<NULL>", "").Text : 
R("SP-3999").HasValue() ? R("SP-3999").Replace("<NULL>", "").Text : 
R("cnet_common_SP-3999").HasValue() ? R("cnet_common_SP-3999").Replace("<NULL>", "").Text : ""; 

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
if(!string.IsNullOrEmpty(OpticalDriveOfDesktop)){ 
Add($"OpticalDriveOfDesktop⸮{OpticalDriveOfDesktop}"); 
} 
} 

// --[FEATURE #8] 
// --Desktop USB ports 
void USB_Ports_Of_Desktop(){ 
var result = ""; 
var USBPortsOfDesktopRef = REQ.GetVariable("SP-4003").HasValue() ? REQ.GetVariable("SP-4003").Replace("<NULL>", "").Text : 
R("SP-4003").HasValue() ? R("SP-4003").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4003").HasValue() ? R("cnet_common_SP-4003").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(USBPortsOfDesktopRef)){ 
var ports = USBPortsOfDesktopRef.Split(", ").FlattenWithAnd(10, ", ").RegexReplace(@"(\d* x)", "##$1"); 
result = $"Use {ports} ports for lightning data movement speed"; 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"USBPortsOfDesktop⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Additional Ports/Interface (If Applicable) 
void Additional_PortsInterface(){ 
var result = ""; 
// Additional Ports/Interface 
var DesktopOtherPortsRef = REQ.GetVariable("SP-4004").HasValue() ? REQ.GetVariable("SP-4004").Replace("<NULL>", "").Text : 
R("SP-4004").HasValue() ? R("SP-4004").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4004").HasValue() ? R("cnet_common_SP-4004").Replace("<NULL>", "").Text : ""; 

if(!string.IsNullOrEmpty(DesktopOtherPortsRef)){ 
var ports = DesktopOtherPortsRef.Split(", ").Where(s => !s.ToLower().Contains("input") && (s.Contains("VGA") || s.Contains("HDMI") || s.ToLower().Contains("displayport"))); 
if(ports.Any() && ports.Count() > 1){ 
result = $"Built-in {ports.FlattenWithAnd().Replace(" output", "").RegexReplace(@"(\d* x )", "")} ports for lightning data movement speed output to connect to TVs or multiple displays for stunning HD entertainment"; 
} 
} 
if(!string.IsNullOrEmpty(result)){ 
Add($"AdditionalPortsInterface⸮{result}"); 
} 
} 

// --[FEATURE #10] - All Features 
// --Dimensions (in Inches): Height x Width x Depth 

// --[FEATURE #11] 
// --Detail any peripherals such as Keyboard, mouse, monitor, etc. 
void Box_Contents(){ 
var BoxContentsOfDesktop = ""; 
if(SPEC["WIB"].GetLines().Where(l => l.Body.In("%eyboard%", "%mouse%", "%trackpad%")).Any()){ 
BoxContentsOfDesktop = $"Includes {SPEC["WIB"].GetLines().Where(l => l.Body.In("%eyboard%", "%mouse%", "%trackpad%")).Select(l => l.Body.ToLower(true).Replace(" (on selected markets only)", "").Replace(" ", "")).Flatten(',').Split(",").FlattenWithAnd()} for immediate setup"; 
} 
if(!string.IsNullOrEmpty(BoxContentsOfDesktop)){ 
Add($"BoxContentsOfDesktop⸮{BoxContentsOfDesktop}"); 
} 
} 

// --[FEATURE #12] - All Features 
// --Warranty information 

//3364110222167288 "Desktop Computers END" "Serhii.O" §§ 

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
result = $"Convenient multi-pack contains individual color cartridges ({consumable_Color.Values.Select(s => s.Value().RegexReplace(@"color \((.+)\)", "tricolor")).FlattenWithAnd()})"; 
} 
else if(!inkOrTonerPackSizeRef.Equals("1/pack") 
&& consumablesIncluded_Color.HasValue() && consumablesIncluded_Color.FirstValue().ToString().Length < 37){ 
result = $"Convenient multi-pack contains individual color cartridges ({consumablesIncluded_Color.Values.Select(s => s.Value().RegexReplace(@"color \((.+)\)", "tricolor")).FlattenWithAnd()})"; 
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

//12375510567216950 "MICR Ink for All Units END" "Serhii.O" §§ 

//§§5226058, §12366910285216553, §1212164913166147, §5226157, §12393811005220973, §5226006, §5226054, §12378610641217427 "Remanufactured Laser Printer Ink, Toner & Drum Units, InkJet Printer Ink, Toner & Drum Units BEGIN" "Serhii.O" 

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

if (CartridgeYieldTypeRef =="Other"){
CartridgeYieldTypeRef =A[3531].FirstValueOrDefault();
}


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
$"{CartridgeYieldTypeRef.Replace("High Yield", "High-Yield").ToLower().ToUpperFirstChar()} ink cartridge yields up to {PageYieldPerPackage} pages" : 
$"{CartridgeYieldTypeRef.Replace("High Yield", "High-Yield").ToLower().ToUpperFirstChar()} {SupplyType.ToLower()} yields up to {PageYieldPerPackage} pages"; 
} 
else if(CartridgeYieldTypeRef.Equals("High Yield")){ 
if(Consumable_CartridgeYield.HasValue("XL") 
&& Consumable_ConsumableType.HasValue("%cartridge", "ink tank")){ 
result = !String.IsNullOrEmpty(InkOrTonerPackSize) && !InkOrTonerPackSize.Equals("1/Pack") && !InkOrTonerPackSize.Equals("Other") ? 
$"Save time and money with these high-yield (XL Series) {Consumable_ConsumableType.FirstValue().Pluralize().ToString().Split(" ").Last()} compared to standard ones" : 
$"Save time and money with this high-yield (XL Series) {Consumable_ConsumableType.FirstValue().ToString().Split(" ").Last()} compared to standard ones"; 
} 
else if(Consumable_ConsumableType.HasValue("%cartridge", "ink tank", "ink tank / paper kit", "ink pack")){ 
result = !String.IsNullOrEmpty(InkOrTonerPackSize) && !InkOrTonerPackSize.Equals("1/Pack") && !InkOrTonerPackSize.Equals("Other") ? 
$"Save time and money with these high-yield {Consumable_ConsumableType.FirstValue().Replace(" / paper kit", "").Pluralize()} compared to standard ones" : 
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
else if(CartridgeYieldTypeRef.Equals("Standard Yield") 
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
else if(CartridgeYieldTypeRef.Equals("Other") 
&& !String.IsNullOrEmpty(PageYieldPerPackage)){ 
result = $"Yields up to {PageYieldPerPackage} pages"; 
} 
else if(CartridgeYieldTypeRef.Equals("Other")){ 
result = "Maximize your page yield while you save"; 
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
result = $"Get standard yield capacity per {Consumable_ConsumableType.FirstValue()} while enjoying cost savings"; 
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
var Type = ""; 

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
Type = Consumable_ConsumableType.HasValue() ? Consumable_ConsumableType.FirstValue().Replace(" / paper kit", "").Replace("toner cartridge", "cartridge").Replace("print cartridge", "cartridge").Pluralize() : ""; 
VendorSpecificInformation = $"The unmatched reliability of {InkOrTonerCartridgeType}{" " + Brand + " "}{Type} means consistent convenience and better value"; 
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
VendorSpecificInformation = $"This {Manufacturer.Text.Split(" ").Select(s => s.Replace("Kyocera","KYOCERA").Replace(" Inc.","").Replace("Sustainable Earth by Staples","Staples Sustainable Earth")).Flatten(" ")} {Consumable_ConsumableType.FirstValue().Replace(" cartridge", "")} cartridge delivers high-quality printing"; 
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
var CompatibleProducts = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body, SPEC["MS"].GetLine("Designed For").Body, SPEC["ES"].GetLine("Designed For").Body); 

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

//§5226058, §12366910285216553, §1212164913166147, §5226157, §12393811005220973, §5226006, §5226054, §12378610641217427 "Remanufactured Laser Printer Ink, Toner & Drum Units, InkJet Printer Ink, Toner & Drum Units END" "Serhii.O" §§ 

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

//168339291098380001 "Heaters END" "Serhii.O" §§ 

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
// Red 25 pt. durable pressboard covers with 2 partitions (17 pt. Kraft) (https://www.staples.com/Staples-Top-Tab-Pressboard-Classification-Folders-Letter-Red-2-5-Cut-Tab-2-Partitions-10-Box-18334/product_807772) 
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
SizeLetterAndlegalOfClassificationFolders = $@"{FilingSystem_SupportedFormat.Where("%Letter%", "%Legal%", "%216 x 279 mm%", "%216 x 356 mm%", "%215.9 x 279.4 mm%").First().Value().RegexReplace(".*(Letter).*", "Letter").RegexReplace(".*(Legal).*", "Legal").RegexReplace(".*(216 x 279 mm).*", "Letter").RegexReplace(".*(216 x 356 mm).*", "Legal").RegexReplace(".*(215.9 x 279.4 mm).*", "Letter") + " size with Tyvek tape on spine for extra durability and "}{"up to " + FilingSystem_Expansion.Values.First().ValueUSM.ToLower() + @""" capacity"}"; 
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
var cutTab = Features.Where("%cut tab").Flatten(", ").ToLower().Replace(" cut", "-cut").Replace(" tab", " "); 
var Tab = Features.Where("end tab", "left side tab", "right of center tab", "right side tab", "top side tab").Count() > 0 ? $"{Features.Where("end tab", "left side tab", "right of center tab", "right side tab", "top side tab").Flatten(", ").ToLower().Replace("end tab", "end").Replace("left side tab", "left").Replace("right of center tab", "right of center").Replace("right side tab", "right").Replace("top side tab", "top")} " : ""; 

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

//135391203142831 "Classification Folders END" "Serhii.O" §§ 

//§§135391203141669 "File Folders BEGIN" "Serhii.O"

FileFoldersTrueColorMaterial();
FileFoldersTabPostion();
FileFoldersFolderFeatures();
FileFoldersPaperSize();
// <Pack Qty> (If more than 1) - ALl
// <Recycled Content (%)> - All
// <Post Consumer Content (%)> - All
AdditonalFeaturesOfFileFolders();
AdditonalFileFoldersRoundedCorners();
AdditonalFileFoldersAcidFree();
AdditonalFileFoldersCapacity();
AdditonalFileFoldersReinforcedShelfMaster();
AdditonalFileFoldersProtectCutLess();
AdditonalFileFoldersStraightCut();

// --[FEATURE #1]
// --<Folder Material>, <True Color> & <Folder Durability (If Standard = Leave blank); Including stock & weight #
void FileFoldersTrueColorMaterial(){
    var result = "";
    var trueColorRef = R("SP-22967").HasValue() ? R("SP-22967") : R("cnet_common_SP-22967");
    var paperStockThicknessRef = R("SP-23621").HasValue() ? R("SP-23621") : R("cnet_common_SP-23621");
    var folderMaterialRef = R("SP-18596").HasValue() ? R("SP-18596") : R("cnet_common_SP-18596");
    var folderDurabilityRef = R("SP-13113").HasValue() ? R("SP-13113") : R("cnet_common_SP-13113");
    var durability = folderDurabilityRef.HasValue() && !folderDurabilityRef.HasValue("Standard") ? $" & {folderDurabilityRef}" : "";
    var stock = paperStockThicknessRef.HasValue() ? $"; {paperStockThicknessRef}" : "";
    
    if(trueColorRef.HasValue()
    && trueColorRef.ToLower().HasValue($"{folderMaterialRef.ToLower()}")){
        result = $"{trueColorRef.ToLower().ToUpperFirstChar()}{durability}{stock}";
    }
    else if(trueColorRef.HasValue("Assorted")
    && folderMaterialRef.HasValue()){
        result = $"{folderMaterialRef.ToLower()} in {trueColorRef.ToLower()}{durability}{stock}";
    }
    else if(trueColorRef.HasValue()
    && folderMaterialRef.HasValue()){
        result = $"{folderMaterialRef.ToLower()}, {trueColorRef.ToLower()}{durability}{stock}";
    }
    else if(trueColorRef.HasValue("Assorted")){
        result = $"{trueColorRef.ToUpperFirstChar()} colors";
    }
    else if(trueColorRef.HasValue()){
        result = $"Comes in {trueColorRef.ToLower()}";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"FileFoldersTrueColorMaterial⸮{result}");
    }
}

// --[FEATURE #2]
// --If <Tab Postion> = Top: <Tab Orientation> & <Tabs>. If <Tab Position> = End: <Tab Position> & <Tabs>
void FileFoldersTabPostion(){
    var result = "";
    //No Tabs|6-Tab|5-Tab|3-Tab|2-Tab|2/5 Cut|Straight Cut|1/5 Cut
    var tabsRef = R("SP-18551").HasValue() ? R("SP-18551").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18551").HasValue() ? R("cnet_common_SP-18551").Replace("<NULL>", "").Text : ""; 
    // Top|End
    var tabPositionRef = R("SP-603").HasValue() ? R("SP-603").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-603").HasValue() ? R("cnet_common_SP-603").Replace("<NULL>", "").Text : "";
    // Left of Center|Assorted|Right Position|Left Position|Right of Center|Center Position|Straight
    var tabOrientationRef = R("SP-351194").HasValue() ? R("SP-351194").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-351194").HasValue() ? R("cnet_common_SP-351194").Replace("<NULL>", "").Text : "";
    
    if(tabPositionRef.Equals("Top")
    && !string.IsNullOrEmpty(tabOrientationRef)
    && !string.IsNullOrEmpty(tabsRef)){
        result = $"Top: {tabOrientationRef.ToLower()} & {tabsRef.ToLower()}";
    }
    else if(tabPositionRef.Equals("End")
    && !string.IsNullOrEmpty(tabOrientationRef)
    && !string.IsNullOrEmpty(tabsRef)){
        result = $"End: {tabPositionRef.ToLower()} & {tabsRef.ToLower()}";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"FileFoldersTabPostion⸮{result}");
    }
}

// --[FEATURE #3]
// --<Folder Features> (If Applicable)
void FileFoldersFolderFeatures(){
    var result = "";
    // Moisture Resistant|Box Bottom|Recycled
    var folderFeaturesRef = R("SP-351199").HasValue() ? R("SP-351199").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-351199").HasValue() ? R("cnet_common_SP-351199").Replace("<NULL>", "").Text : "";
    
    if(!string.IsNullOrEmpty(folderFeaturesRef)){
        result = $"Features: {folderFeaturesRef.ToLower()}";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"FileFoldersFolderFeatures⸮{result}");
    }
}

// --[FEATURE #4]
// --<Paper Size>
void FileFoldersPaperSize(){
    var result = "";
    // Letter/Legal|Letter|Legal|Other
    var paperSizeRef = R("SP-12517").HasValue() ? R("SP-12517").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-12517").HasValue() ? R("cnet_common_SP-12517").Replace("<NULL>", "").Text : "";
    var DimensionsWeight_Width = R("SP-21044").HasValue() ? R("SP-21044").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-21044").HasValue() ? R("cnet_common_SP-21044").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Width
    var DimensionsWeight_Height = R("SP-20654").HasValue() ? R("SP-20654").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-20654").HasValue() ? R("cnet_common_SP-20654").Replace("<NULL>", "").Text : ""; //Dimensions & Weight - Depth

    if(!String.IsNullOrEmpty(paperSizeRef)
    && !String.IsNullOrEmpty(DimensionsWeight_Width)
    && !String.IsNullOrEmpty(DimensionsWeight_Height)){
        switch(paperSizeRef.ToLower()){
            case var a when a.Equals("letter/legal"):
            result = $@"Accommodates letter or legal-size files and measures {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W";
            break;
            case var a when a.Equals("legal"):
            result = $@"Accommodates legal-size files and measures {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W";
            break;
            case var a when a.Equals("letter"):
            result = $@"Letter-size folders measuring {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W easily hold important files";
            break;
        }
    }
    else if(!String.IsNullOrEmpty(paperSizeRef)){
        switch(paperSizeRef.ToLower()){
            case var a when a.Equals("letter/legal"):
            result = "Accommodates letter or legal-size files";
            break;
            case var a when a.Equals("legal"):
            result = "Legal-size folders for holding oversize documents without folding";
            break;
            case var a when a.Equals("letter"):
            result = "Letter-size folders easily hold important files";
            break;
        }
    }
    else if(String.IsNullOrEmpty(paperSizeRef)
    && !String.IsNullOrEmpty(DimensionsWeight_Width)
    && !String.IsNullOrEmpty(DimensionsWeight_Height)){
        result = $@"Accommodates {DimensionsWeight_Height}""H x {DimensionsWeight_Width}""W size files";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"FileFoldersPaperSize⸮{result}");
    }
}

// --[FEATURE #5] - All
// --<Pack Qty> (If more than 1)

// --[FEATURE #6]
// --<Fashion> (If Applicable)
void FileFoldersFashion(){
    var result = "";
    // No|Yes
    var fashionRef = R("SP-12517").HasValue() ? R("SP-12517").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-12517").HasValue() ? R("cnet_common_SP-12517").Replace("<NULL>", "").Text : "";
    
    if(fashionRef.Equals("Yes")){
        result = "Be sleek, stylish, and organized with these file folders";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"FileFoldersFashion⸮{result}");
    }
}

// --[FEATURE #7] - All
// --<Recycled Content (%)>

// --[FEATURE #8] - All
// --<Post Consumer Content (%)>

// --[FEATURE #9]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditonalFeaturesOfFileFolders(){
    var result = "";
    
    if(!(SKU.ProductId is null) && SKU.ProductId.In("20491307")){
        result = "Preprinted information includes addresses, phone numbers, and key steps";
    };
    if(!String.IsNullOrEmpty(result)){
        Add($"AdditonalFeaturesOfFileFolders⸮{result}");
    }
}
        
// --[FEATURE #10]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditonalFileFoldersRoundedCorners(){
    var result = "";
    var filingSystem_Features = Coalesce(A[5945]); //Filing System - Features
    
    if(filingSystem_Features.HasValue("rounded corners")){
        result = "Stylish rounded corners that aren't likely to snag and tear";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AdditonalFileFoldersRoundedCorners⸮{result}");
    }
}

// --[FEATURE #11]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditonalFileFoldersAcidFree(){
    var result = "";
    var filingSystem_Features = Coalesce(A[5945]); //Filing System - Features
    
    if(filingSystem_Features.HasValue("acid free")){
        result = "Acid-free for archival purposes";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AdditonalFileFoldersAcidFree⸮{result}");
    }
}

// --[FEATURE #12]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditonalFileFoldersCapacity(){
    var result = "";
    var filingSystem_Capacity = Coalesce(A[6576]); //Filing System - Capacity
    
    if(filingSystem_Capacity.HasValue()
    && filingSystem_Capacity.Units.First().Name.In("sheet%")){
        result = $"Holds up to {filingSystem_Capacity.FirstValue()} sheets for convenience";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AdditonalFileFoldersCapacity⸮{result}");
    }
}

// --[FEATURE #13]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditonalFileFoldersReinforcedShelfMaster(){
    var result = "";
    var filingSystem_Features = Coalesce(A[5945]); //Filing System - Features
    
    if(filingSystem_Features.HasValue("Shelf-Master tab", "reinforced tabs")){
        result = "Reinforced Shelf-Master tab gives added strength where you need it most";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AdditonalFileFoldersReinforcedShelfMaster⸮{result}");
    }
}

// --[FEATURE #14]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditonalFileFoldersProtectCutLess(){
    var result = "";
    var filingSystem_Features = Coalesce(A[5945]); //Filing System - Features
    
    if(filingSystem_Features.HasValue("CutLess")){
        result = "Protect your fingers with CutLess folders";
    }
    else if(filingSystem_Features.HasValue("WaterShed")){
        result = "WaterShed folders resist liquid spills such as water, coffee, soda, etc.";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AdditonalFileFoldersProtectCutLess⸮{result}");
    }
}
        
// --[FEATURE #15]
// --Use for additional product and/or manufacturer information relevant to the customer buying decision
void AdditonalFileFoldersStraightCut(){
    var result = "";
    var filingSystem_Features = Coalesce(A[5945]); //Filing System - Features
    
    if(filingSystem_Features.HasValue("straight-cut tab")){
        result = "Reinforced Shelf-Master tab gives added strength where you need it most";
    }
    if(!String.IsNullOrEmpty(result)){
        Add($"AdditonalFileFoldersStraightCut⸮{result}");
    }
}

//135391203141669 "File Folders END" "Serhii.O" §§

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
var NumberOfLabelsPerPack = R("SP-22253").HasValue() ? R("SP-22253").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22253").HasValue() ? R("cnet_common_SP-22253").Replace("<NULL>", "").Text : ""; // Number of Labels Per Pack 

if(!String.IsNullOrEmpty(NumberOfLabelsPerSheet) 
&& !string.IsNullOrEmpty(NumberOfLabelsPerPack)){ 
result = NumberOfLabelsPerSheet.Equals("1") ? 
$"{NumberOfLabelsPerSheet} label per sheet; {NumberOfLabelsPerPack} labels total" : 
$"{NumberOfLabelsPerSheet} labels per sheet; {NumberOfLabelsPerPack} labels total"; 
} 
else if(!String.IsNullOrEmpty(NumberOfLabelsPerRoll) 
&& !string.IsNullOrEmpty(NumberOfLabelsPerPack)){ 
result = NumberOfLabelsPerRoll.Equals("1") ? 
$"{NumberOfLabelsPerRoll} label per roll; {NumberOfLabelsPerPack} labels total" : 
$"{NumberOfLabelsPerRoll} labels per roll; {NumberOfLabelsPerPack} labels total"; 
} 
else if(!string.IsNullOrEmpty(NumberOfLabelsPerPack)){ 
result = NumberOfLabelsPerPack.Equals("1") ? 
$"{NumberOfLabelsPerPack} label per pack" : 
$"{NumberOfLabelsPerPack} labels per pack"; 
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
PopUpEdge = TX_UOM.HasValue("Each") ? "Label features pop-up edge for fast peeling; simply bend the sheet to expose the label edge" : 
"Labels feature pop-up edge for fast peeling; simply bend the sheet to expose the label edge"; 
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

//1385810842142725 "Labels END" "Serhii.O" §§ 

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
var result = ""; 
var penTypeVal = R("SP-18609").HasValue() ? R("SP-18609").Replace("<NULL>", "").Text : R("cnet_common_SP-18609").HasValue() ? R("cnet_common_SP-18609").Replace("<NULL>", "").Text : ""; 
var penPackSize = R("SP-18666").HasValue() ? R("SP-18666").Replace("<NULL>", "").Text : R("cnet_common_SP-18666").HasValue() ? R("cnet_common_SP-18666").Replace("<NULL>", "").Text : ""; 
var isPluralPen = "pens"; 
var isAre = "are"; 
if (!String.IsNullOrEmpty(penTypeVal)) { 

if (!String.IsNullOrEmpty(penPackSize) && penPackSize.Equals("1")) { 
isPluralPen = "pen"; 
isAre = "is"; 
} 
if (penTypeVal.ToLower().Contains("erasable")) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} allows you to write, erase and rewrite with no wear-and-tear"; 
} 
else if (penTypeVal.ToLower().Equals("retractable gel") ) { 
result = $"Retractable gel {isPluralPen} for everyday writing tasks"; 
} 
else if (penTypeVal.ToLower().Contains("retractable")) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} {isAre} ready to write with just a click"; 
} 
else if (penTypeVal.ToLower().Contains("ballpoint")) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} for clear, consistent writing"; 
} 
else if (penTypeVal.ToLower().Contains("ballpoint")) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} for clear, consistent writing"; 
} 
else if (penTypeVal.ToLower().Contains("fountain")) { 
result = $"The everyday {penTypeVal.ToLower()} {isPluralPen} for smooth, expressive writing"; 
} 
else if (penTypeVal.ToLower().Contains("gel")) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} for any task"; 
} 
else if (penTypeVal.ToLower().Contains("erasable")) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} allow you to write, erase and rewrite with no wear-and-tear"; 
} 
else if (penTypeVal.ToLower().Equals("retractable ballpoint")) { 
result = $"Retractable ballpoint {isPluralPen} for ease of use"; 
} 
else if (penTypeVal.ToLower().Contains("retractable")) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} {isAre} ready to write with just a click"; 
} 
else if (penTypeVal.ToLower().Equals("felt")) { 
result = $"Felt tip draws bold and expressive lines"; 
} 
else if (penTypeVal.ToLower().Contains("ballpoint")) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen} for clear, consistent writing"; 
} 
else if (penTypeVal.ToLower().Contains("fountain")) { 
result = $"The everyday {penTypeVal.ToLower()} {isPluralPen} for smooth, expressive writing"; 
} 
else if (penTypeVal.ToLower().Contains("rollerball")) { 
result = $"Rollerball design delivers a bold ink laydown"; 
} 
else if (penTypeVal.ToLower().Contains("gel")) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} ink {isPluralPen} for any task"; 
} 
else if (penTypeVal.ToLower().Contains("counter top")) { 
result = $"Counter top {isPluralPen} {isAre} always handy for your customers"; 
} 
else if (!String.IsNullOrEmpty(penTypeVal)) { 
result = $"{penTypeVal.ToLower().ToUpperFirstChar()} {isPluralPen}"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"PenType⸮{result}"); 
} 
} 

// "Bullet 2" "Pen Ink True color" 
void PenInkTrueColor() { 
var penInkTrueColor = ""; 
var trueColor = R("SP-18607").HasValue() ? R("SP-18607").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18607").HasValue() ? R("cnet_common_SP-18607").Replace("<NULL>", "").Text : ""; 
var writingDrawingToolWritingColor = A[5977]; 

if (!String.IsNullOrEmpty(trueColor)) { 
switch (trueColor) { 
case var a when a.ToLower().Contains("assorted"): 
penInkTrueColor = ($"{writingDrawingToolWritingColor.Values.Select(s => s.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} colors for a variety of tasks"); 
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
penInkTrueColor = a.Split('/').Count() > 1 ? ($"{trueColor[0].ToString().ToUpper()}{trueColor.Substring(1).ToLower()} inks for bright and clear writing") : ($"{trueColor[0].ToString().ToUpper()}{trueColor.Substring(1).ToLower()} ink for bright and clear writing"); 
break; 
} 
if (!String.IsNullOrEmpty(penInkTrueColor)) { Add($"PenInkTrueColor⸮{penInkTrueColor}");} 
} 
} 

//"Bullet 3" "Pen Point Type & Size" 
void PenPointTypeAndSize() { 
    var result = ""; 
    var penPointType = R("SP-18608").HasValue() ? R("SP-18608").Replace("<NULL>", "").Text : 
    R("cnet_common_SP-18608").HasValue() ? R("cnet_common_SP-18608").Replace("<NULL>", "").Text : ""; 
    
    var penPointSize = R("SP-16585").HasValue() ? R("SP-16585").Replace("<NULL>", "").Text : 
    R("cnet_common_SP-16585").HasValue() ? R("cnet_common_SP-16585").Replace("<NULL>", "").Text : ""; 
    
    var pointTypeIsNotNull = !String.IsNullOrEmpty(penPointType); 
    var pointSizeIsNotNull = !String.IsNullOrEmpty(penPointSize); 
    
    if (pointTypeIsNotNull 
    && penPointType.ToLower().Equals("micro point") 
    && pointSizeIsNotNull) { 
        result = $"{penPointSize} microo-point tip delivers crisp, precise lines"; 
    } 
    else if (pointTypeIsNotNull 
    && penPointType.ToLower().Contains("ultra micro") 
    && pointSizeIsNotNull) { 
        result = $"{penPointType} {penPointSize} is highly precise for crisp writing"; 
    } 
    else if (pointTypeIsNotNull 
    && penPointType.ToLower().Contains("micro") 
    && pointSizeIsNotNull) { 
        result = $"{penPointSize} {penPointType} tip delivers crisp, precise line"; 
    } 
    else if (pointTypeIsNotNull 
    && penPointType.ToLower().Contains("micro")) { 
        result = "Micro-point tip delivers crisp, precise lines"; 
    } 
    else if (pointTypeIsNotNull 
    && ( 
        penPointType.ToLower().Contains("medium") 
        || penPointType.ToLower().Contains("standard")) 
    ) { 
        var tip = !string.IsNullOrEmpty(penPointSize) ? $"{penPointSize} " : ""; 
        if (!string.IsNullOrEmpty(tip)) {
            result = $"{tip}{penPointType.ToLower()} tip"; 
        } else {
            result  =$"{penPointType.ToLower().ToUpperFirstChar()} tip";
        }
        
    } 
    else if (pointTypeIsNotNull 
    && penPointType.ToLower().Contains("fine") 
    && pointSizeIsNotNull) { 
        var tmp = penPointType.ToLower().Equals("ultra fine") ? "Ultra-fine" : penPointType.ToLower().ToUpperFirstChar(); 
        result = $"{tmp} {penPointSize.ToLower().ToUpperFirstChar()} tip ensures crisp lines with every use"; 
    } 
    else if (pointTypeIsNotNull 
    && penPointType.ToLower().Contains("broad") 
    && pointSizeIsNotNull) { 
        result = $"{penPointType.ToLower().ToUpperFirstChar()} {penPointSize} point provides easy readability"; 
    } 
    else if (pointTypeIsNotNull 
    && penPointType.ToLower().Contains("broad")) { 
        result = $"{penPointType.ToLower().ToUpperFirstChar()} point provides easy readability"; 
    } 
    else if (pointTypeIsNotNull 
    && penPointType.ToLower().Contains("bold") 
    && pointSizeIsNotNull) { 
        result = $"{penPointSize} {penPointType.ToLower()} point provides easy readability"; 
    } 
    else if (pointTypeIsNotNull 
    && penPointType.ToLower().Contains("tip") 
    && pointSizeIsNotNull) { 
        result = $"{penPointSize} {penPointType.ToLower()} delivers crisp, precise lines"; 
    } 
    else if (pointTypeIsNotNull 
    && penPointType.ToLower().Contains("tip")) { 
        result = $"{penPointType.ToLower().ToUpperFirstChar()} delivers crisp, precise lines"; 
    } 
    else if (pointTypeIsNotNull && pointSizeIsNotNull) { 
        result = $"{penPointSize} {penPointType.ToLower()} tip delivers crisp, precise lines"; 
    } 
    else if (pointTypeIsNotNull || pointSizeIsNotNull) { 
        var tmp = pointTypeIsNotNull ? penPointType : penPointSize; 
        result =$"{tmp.ToLower().ToUpperFirstChar()} tip delivers crisp, precise lines"; 
    } 
    if (!String.IsNullOrEmpty(result)) { 
        Add($"PenPointTypeAndSize⸮{result}"); 
    } 
} 

// "Bullet 4" "Pen Barrel Color & Barrel Material" 
void PenBarrelColorAndBarrelMaterial () { 
var penBarrelColorAndBarrelMaterial = ""; 
var penBarrelColor = R("SP-18606").HasValue() ? R("SP-18606").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18606").HasValue() ? R("cnet_common_SP-18606").Replace("<NULL>", "").Text : ""; 
var barrelMaterial = R("SP-4882").HasValue() ? R("SP-4882").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4882").HasValue() ? R("cnet_common_SP-4882").Replace("<NULL>", "").Text : ""; 
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
penBarrelColorAndBarrelMaterial = $"{tmp.ToLower().ToUpperFirstChar()} barrels"; 
} else if (colorIsNotNull) { 
var tmp = materialIsNotNull ? $"{penBarrelColor} {barrelMaterial}" : penBarrelColor; 
penBarrelColorAndBarrelMaterial = $"{tmp.ToLower().ToUpperFirstChar()} barrel"; 
} 
if (!String.IsNullOrEmpty(penBarrelColorAndBarrelMaterial)) { 
Add($"PenBarrelColorAndBarrelMaterial⸮{penBarrelColorAndBarrelMaterial}"); 
} 
} 
// Bullet 5 "Pack size" (if more then one) 

void PenGrip() { 
var penGrip = R("SP-350752").HasValue() ? R("SP-350752").Replace("<NULL>", "").Text : 
R("cnet_common_SP-350752").HasValue() ? R("cnet_common_SP-350752").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(penGrip) && penGrip.ToLower().Equals("yes")) { 
Add("AdditionalPenGrip⸮Comfort grip for superior control"); 
} 
} 

void PenPocketClip() { 
var penClip = R("SP-4878").HasValue() ? R("SP-4878").Replace("<NULL>", "").Text : 
R("cnet_common_SP-4878").HasValue() ? R("cnet_common_SP-4878").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(penClip) && penClip.ToLower().Equals("yes")) { 
Add("AdditionalPenClip⸮Features pocket clip for convenient carrying"); 
} 
} 

void PenRefillable() { 
var penRefillable = R("SP-12520").HasValue() ? R("SP-12520").Replace("<NULL>", "").Text : 
R("cnet_common_SP-12520").HasValue() ? R("cnet_common_SP-12520").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(penRefillable) && penRefillable.ToLower().Equals("yes")) { 
Add("AdditionalPenRefillable⸮Refillable ink allows pens to be reused instead of being replaced to save money"); 
} 
} 

void BarrelShape() { 
var resBarrelShape = ""; 
var barrelShape = R("SP-350746").HasValue() ? R("SP-350746").Replace("<NULL>", "").Text : 
R("cnet_common_SP-350746").HasValue() ? R("cnet_common_SP-350746").Replace("<NULL>", "").Text : ""; 
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
var window = Coalesce(A[5996].HasValue("ink level viewing window")); 
var barrelColor = R("SP-18606").HasValue() ? R("SP-18606").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18606").HasValue() ? R("cnet_common_SP-18606").Replace("<NULL>", "").Text : ""; 
var clearColor = Coalesce(barrelColor).ToLower().In("%translucent%", "%transparent%"); 
if (window) { 
var tmp = clearColor ? "Barrel windows reveal the amount of remaining ink" : "Visible ink supply, so you never run out unexpectedly"; 
Add($"AdditionalPenInkLevelViewingWindow⸮{tmp}"); 
} 
} 

void ArchivalAcidFree() { 
var tmp = A[5996]; 

if(tmp.HasValue("archival quality") 
&& tmp.HasValue("acid-free")){ 
Add("AdditionalArchivalAcidFree⸮Archival quality, acid-free ink"); 
} 
else if(tmp.HasValue("acid-free")){ 
Add("AdditionalArchivalAcidFree⸮Acid-free ink"); 
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

//1114953110001 end of "Pens" §§ 

//§§167638614736165755 start of "Post-it&reg; & Sticky Notes" "Alex K." 

TypeOfStickyNotes(); 
PostItSizeColorCollection(); 
SheetCountPadsPack(); 
PopUpOrFlat(); 
LineType(); 
DispenserIncluded(); 
//Greener or Recycled Content (If applicable) 
// Post Consumer Content (If applicable) 
PostItShape(); 

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
var size = !(R("SP-18403") is null) || !R("SP-18403").Text.Equals("<NULL>") ? R("SP-18403").Text : 
!(R("cnet_common_SP-18403") is null) || !R("cnet_common_SP-18403").Text.Equals("<NULL>") ? R("cnet_common_SP-18403").Text : ""; 
var width = !(R("SP-21044") is null) || !R("SP-21044").Text.Equals("<NULL>") ? R("SP-21044").Text : 
!(R("cnet_common_SP-21044") is null) || !R("cnet_common_SP-21044").Text.Equals("<NULL>") ? R("cnet_common_SP-21044").Text : "";// width in inches 
var length = !(R("SP-20400") is null) || !R("SP-20400").Text.Equals("<NULL>") ? R("SP-20400").Text : 
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
postItSizeColorCollection = $"{width}\"W x {length}\"L New York notes include colors inspired by the skyline, stone and steel"; 
} 
else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("new york")) { 
postItSizeColorCollection = $"New York Collection includes colors inspired by the skyline, stone and steel"; 
} 
else if (!String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Equals("marseille") && hasWidthAndLength) { 
postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the Marseille Collection are a simple tool to keep you organized"; 
} 
else if (!String.IsNullOrEmpty(colorCollection) && hasWidthAndLength) { 
postItSizeColorCollection = $"{width}\"W x {length}\"L notes in the {colorCollection.Replace(@"Assorted", @"ass_orted")} Collection are a simple tool to keep you organized"; 
} 
else if (colorCollection.ToLower().Contains("assorted")) { 
postItSizeColorCollection = "Assorted colors notes"; 
} 
else if (!String.IsNullOrEmpty(colorCollection)) { 
postItSizeColorCollection = $"{colorCollection} Collection notes are a simple tool to keep your everyday organized"; 
} 
else if (!String.IsNullOrEmpty(size)) { 
postItSizeColorCollection = $"{Coalesce(size).RegexReplace(@"(\d*"")(\sx\s)(\d*"")", "$1W$2$3L" )} sticky notes"; 
} 

if (!String.IsNullOrEmpty(postItSizeColorCollection)) { 
Add($"PostItSizeColorCollection⸮{postItSizeColorCollection}"); 
} 
} 

// "Bullet 3" "Sheet Count/Pads per Pack" 
void SheetCountPadsPack() { 
var result = ""; 
var pads_x_sheets = Coalesce(A[6110]); 
var pagesSheetsQty = A[6021]; 
var TX_UOM = Coalesce(REQ.GetVariable("TX_UOM")); 
var size = TX_UOM.HasValue("Dozen") ? 12 : TX_UOM.ExtractNumbers().Any() ? TX_UOM.ExtractNumbers().First() : 0; 
var pack = TX_UOM.HasValue() ? TX_UOM.ToString().Split("/").Last() : ""; 
var sheets = 0; 
var colorCollection = "";//R("SP-350290").HasValue() ? R("SP-350290").Replace("<NULL>", "").Text : 
//R("cnet_common_SP-350290").HasValue() ? R("cnet_common_SP-350290").Replace("<NULL>", "").Text : ""; 
if (pads_x_sheets.HasValue()) { 
sheets = pads_x_sheets.FirstValue().ToString().Split("x").Count() == 2 ? pads_x_sheets.FirstValue().ExtractNumbers().Last() : 0; 
} 
if (pads_x_sheets.HasValue() && pads_x_sheets.FirstValue().ToString().Split("x").Count() == 2 
&& size != 0 && sheets != 0) { 
result = $"{sheets} notes per pad; {size} pads per pack"; 
} 
else if (pagesSheetsQty.HasValue()) { 
result = $"They come in a pack of {pagesSheetsQty.FirstValue()}"; 
} 
else if (size != 0) { 
if(TX_UOM.HasValue("Dozen")) { 
if (String.IsNullOrEmpty(colorCollection) && colorCollection.ToLower().Contains("assorted")) { 
result = "12 per pack"; 
} else { 
result = "Dozen per pack"; 
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
lineType = "Lined notes provide structure for your thoughts"; 
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

// "Bullet 9" "Shape" 
void PostItShape() { 
var result = ""; 
var shape = A[6633]; 
if (shape.HasValue()) { 
result = $"Features {shape.Values.Select(o => o.Value()).FlattenWithAnd()} shape"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"PostItShape⸮{result}"); 
} 
} 

//167638614736165755 end of "Post-it&reg; & Sticky Notes" §§ 

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
colorFamily = $"Comes in {cFamily.ToLower()}"; 
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

// 1111111 "can be used for all categores" 1111111 
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

// 1111111 "can be used for all categores" 1111111 
// "Additional Bullet" "Timer" 
void AdditionalTimer() { 
var additionalTimer = ""; 
var timer = Coalesce(A[4538].HasValue()) ? A[4538].FirstValue().ExtractNumbers().First() : 0; 
var units = Coalesce(A[4538].HasValue()) ? A[4538].Units.First().Name : "non"; 
if (timer > 1 && (units.Contains("hr") || units.Equals("min"))) { 
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
var carryHandle= ""; 
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

//168339291098380002 end of "Fans" §§ 

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
var result = ""; 
var productType = Coalesce(A[6718]); 
var sparkling = Coalesce(A[6720]); 
var type = !(R("SP-22288") is null) || !R("SP-22288").Text.Equals("<NULL>") ? Coalesce(R("SP-22288").Text) : 
!(R("cnet_common_SP-22288") is null) || !R("cnet_common_SP-22288").Text.Equals("<NULL>") ? Coalesce(R("cnet_common_SP-22288").Text) : Coalesce(""); 

if (productType.HasValue("juice") && type.HasValue() && !(type.In("Variety Pack", "Milk", "Cider", "Lemonade"))) { 
if(sparkling.HasValue("yes")) { 
result = $"Sparkling {type.ToLower()} juice is a refreshing and delicious drink"; 
} 
else { 
result = $"{type.ToLower().ToUpperFirstChar()} juice tastes good and is good for you"; 
} 
} 
else if (productType.HasValue("juice") && type.HasValue("Variety Pack")) { 
result = "Enjoy a sampling of your favorite juices with this variety pack"; 
} 
else if (SKU.Brand.HasValue("CapriSun") && productType.HasValue("water") && type.HasValue("Variety Pack")) { 
result = "Enjoy a sampling of your favorite juices with this variety pack"; 
} 
else if (SKU.Brand.HasValue("CapriSun") && productType.HasValue("water") && !type.In("%Milk", "Cider", "Lemonade")) { 
result = $"{type.ToLower().ToUpperFirstChar()} juice tastes good and is good for you"; 
} 
else if (productType.HasValue("soft drink") && !type.In("%Milk", "Variety Pack")) { 
result = $"Quench your thirst with a delicious {type.ToLower()} drink"; 
} 
else if (productType.HasValue("instant tea") && !type.In("%Milk", "Variety Pack")) { 
result = $"Sweeten up that boring water with {type.ToLower()}-flavored tea mix"; 
} 
else if (productType.HasValue("instant drink") && !type.In("%Milk")) { 
result = $"{type.ToLower().ToUpperFirstChar()} drink mix transforms ordinary water into a spectacular water experience"; 
} 
else if (productType.HasValue("juice") && type.HasValue()) { 
result = $"{type.ToLower().ToUpperFirstChar()} juice is a refreshing and delicious drink"; 
} 

if (!String.IsNullOrEmpty(result)) { 
Add($"JuiceType⸮{result}"); 
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
var capriSun = Coalesce(A[7338]); 
if (capriSun.HasValue("%- Capri Sun%") && brand.HasValue("CapriSun") && type.HasValue("Variety Pack")) { 
var tmp = capriSun.Where("%Capri %").Select(s => s.Value().ToString().Split(" Sun ").Last()).FlattenWithAnd(20, ","); 
result = $"{capriSun.Where("%Capri %").Count()} different flavors, including {tmp}"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"AdditionalCapriSunVaietyPack⸮{result}"); 
} 
} 

//168412514983216257 end of "Juice" §§ 

//§§1114953140896 -- "Markers" "Alex K." 

MarkerTypeAndUse(); 
MarkerInkColor(); 
// 3 and 4 bullet in MarkerPointType() 
MarkerPointType(); 
MarkerPointType2(); 
// pack size 
NonToxic(); 
Odor(); 

// "Bullet 1" "Marker type & Use" 
void MarkerTypeAndUse() { 
var result = ""; 
var markerType = R("SP-22698").HasValue() ? R("SP-22698").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22698").HasValue() ? R("cnet_common_SP-22698").Replace("<NULL>", "").Text : ""; 
var quickDry = Coalesce(A[6282]); 
if (!String.IsNullOrEmpty(markerType)) { 
if (quickDry.HasValue() && markerType.ToLower().Equals("alcohol")) { 
result = "These alcohol based markers dry fast and do not bleed"; 
} 
else { 
var tmp = markerType.ToLower(); 
switch(tmp) { 
case "alcohol": 
result = "These alcohol based markers make a great addition to any craft room or design studio"; 
break; 
case "brush tip": 
result = "Brush tip creates fluid lines and the marker tips are perfect for consistency when coloring"; 
break; 
case "calligraphy": 
result = "Calligraphy markers for writing and drawing"; 
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
result = "Make long-lasting signs and write notes with these permanent markers"; 
break; 
case "sketch": 
result = "Sketch markers for writing and drawing"; 
break; 
case "water based": 
result = "Water-based paint markers for writing and drawing"; 
break; 
case "china": 
result = "China marker peels for continuous use"; 
break; 
case "kid's markers": 
result = "Kid markers are great for arts, crafts, homework, school projects, journal entries, scrapbooks, and more"; 
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
var trueColor = A[5977]; 
if (Coalesce(trueColor).HasValue("%assorted%") && trueColor.Values.Count() == 1) { 
result = $"Comes in ##{trueColor.Where("%assorted%").First().Value().Replace(" glitter", "").Replace("assorted", "ass_orted")} that can be mixed"; 
} 
else if (Coalesce(trueColor).HasValue("%assorted%")) { 
result = $"Comes in {trueColor.WhereNot("%assorted%").Flatten(", ")} that can be mixed"; 
} 
else if ((Coalesce(trueColor).HasValue("% red") || Coalesce(trueColor).HasValue("red")) 
&& trueColor.Values.Count() == 1) { 
result = $"{trueColor.FirstValue().ToLower().ToUpperFirstChar()} ink marker stands out easily on any light-colored background"; 
} 
else if ((Coalesce(trueColor).HasValue("% green") || Coalesce(trueColor).HasValue("green")) 
&& trueColor.Values.Count() == 1) { 
result = $"High-contrast ink in rich {trueColor.FirstValue()} color stands out on all types of materials"; 
} 
else if ((Coalesce(trueColor).HasValue("% black") || Coalesce(trueColor).HasValue("black")) 
&& trueColor.Values.Count() == 1) { 
result = $"Create clear {trueColor.FirstValue()} lines with these markers, that is well-suited for a variety of office tasks"; 
} 
else if (trueColor.HasValue("% white") || Coalesce(trueColor).HasValue("white") 
&& trueColor.Values.Count() == 1) { 
result = "White paint for marking on dark backgrounds"; 
} 
else if (trueColor.HasValue() 
&& trueColor.Values.Count == 1) { 
result = $"Comes in {trueColor.FirstValue()}"; 
} 
else if (trueColor.HasValue()) { 
result = $"Assorted colors include {trueColor.Values.Select(s => s.Value()).FlattenWithAnd()}"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"MarkerInkColor⸮{result}"); 
} 
} 

// "Bullet 3" "Marker point type" 
void MarkerPointType() { 
var result = ""; 
var pointType = R("SP-16612").HasValue() ? R("SP-16612").Replace("<NULL>", "").Text : 
R("cnet_common_SP-16612").HasValue() ? R("cnet_common_SP-16612").Replace("<NULL>", "").Text : ""; 
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
result = "Fine-point tip creates thin, accurate lines"; 
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
else if (!String.IsNullOrEmpty(pointType)) { 
result = $"{pointType} point provides accuracy and detail"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"MarkerPointType⸮{result}"); 
} 
} 

// BUllet 4 "2nd marker tip type (If applicable)" 
void MarkerPointType2() { 
var result = ""; 
var pointType = R("SP-16612").HasValue() ? R("SP-16612").Replace("<NULL>", "").Text : 
R("cnet_common_SP-16612").HasValue() ? R("cnet_common_SP-16612").Replace("<NULL>", "").Text : ""; 
// Chisel|Bullet 
var markerTipType2 = R("SP-350750").HasValue() ? R("SP-350750").Replace("<NULL>", "").Text : 
R("cnet_common_SP-350750").HasValue() ? R("cnet_common_SP-350750").Replace("<NULL>", "").Text : ""; 
var secondTipType_LineType = Coalesce(A[5983], A[5984]); 

if(pointType.ToLower().Equals("twin tip")){ 
if (markerTipType2.ToLower().Equals("bold") 
|| secondTipType_LineType.HasValue("bold")) { 
result = "Bold point creates thick, strong and detailed lines. Provides high visibility for your needs"; 
} 
else if (markerTipType2.ToLower().Equals("bullet") 
|| secondTipType_LineType.HasValue("bullet")) { 
result = "Bullet tip writes with uniform and defined lines"; 
} 
else if (markerTipType2.ToLower().Equals("extra fine") 
|| secondTipType_LineType.HasValue("extra fine")) { 
result = "Extra fine point for detailed writing applications"; 
} 
else if (markerTipType2.ToLower().Equals("ultra fine") 
|| secondTipType_LineType.HasValue("ultra fine")) { 
result = "Ultra-fine point tip creates thin, accurate lines"; 
} 
else if (markerTipType2.ToLower().Equals("medium") 
|| secondTipType_LineType.HasValue("medium")) { 
result = "Medium point provides accuracy and detail"; 
} 
else if (markerTipType2.ToLower().Equals("broad") 
|| secondTipType_LineType.HasValue("broad")) { 
result = "Creates broad and fine lines"; 
} 
else if (markerTipType2.ToLower().Equals("super fine") 
|| secondTipType_LineType.HasValue("super fine")) { 
result = "Write with precision and accuracy with this super-fine tip"; 
} 
else if (markerTipType2.ToLower().Equals("conical") 
|| secondTipType_LineType.HasValue("conical")) { 
result = "Conical tips write broad and narrow"; 
} 
else if (markerTipType2.ToLower().Equals("fine") 
|| secondTipType_LineType.HasValue("fine")) { 
result = "Fine-point tip creates thin, accurate lines"; 
} 
else if (!String.IsNullOrEmpty(markerTipType2)) { 
result = $"{markerTipType2.ToLower().ToUpperFirstChar()} point provides accuracy and detail"; 
} 
else if (secondTipType_LineType.HasValue()) { 
result = $"{secondTipType_LineType.FirstValue().ToLower().ToTitleCase()} point provides accuracy and detail"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"MarkerPointType2⸮{result}"); 
} 
} 

// BUllet 5 "Pack size" 

// "Bullet 6" "NON-TOXIC" 
void NonToxic() { 
var result = ""; 
var nonToxic = R("SP-21018").HasValue() ? R("SP-21018").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21018").HasValue() ? R("cnet_common_SP-21018").Replace("<NULL>", "").Text : ""; 
if (nonToxic.ToLower().Equals("yes")) { 
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
var odor = R("SP-21015").HasValue() ? R("SP-21015").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21015").HasValue() ? R("cnet_common_SP-21015").Replace("<NULL>", "").Text : ""; 

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

//1114953140896 end of "Markers" §§ 

//§§13529397020006 -- "Binder Accessories" "Alex K." 

BinderAccessoryTypeAndUse(); 
// True color & Material of Item 
// Dimensions 
TabIndexPunchInformation(); 
// Pack Size 
ResistantOrProof(); 
ArchivalSafe(); 
BindersPages(); 

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
if (tab.HasValue() 
&& tab.Where("%A-Z%").Any()) { 
result = "A-Z dividers for alphabetical organization"; 
} 
else if (tab.HasValue() 
&& tab.Where("Jan%Dec%", "Jan%Dez%", "Jan%Dec%", "Jan%Déc%").Any()) { 
result = "Monthly tab dividers for impactful organizing"; 
} 
else if (tab.HasValue() 
&& tab.Where("Mon-Sun", "Mon-Fri", "Mon-Sat").Any()) { 
var tmp = tab.HasValue("Mon-Sun") ? "sunday" : tab.HasValue("Mon-Fri") ? "friday" : tab.HasValue("Mon-Sat") ? "saturday" : ""; 
result = $"Organize your documents quickly and easily monday through {tmp}"; 
} 
else if (numOfPunches.HasValue() && numOfPunches.Values.ExtractNumbers().Any()) { 
var tmp = numOfPunches.Values.ExtractNumbers().First(); 
if (numOfHoles.Values.ExtractNumbers().Any() 
&& numOfHoles.Values.ExtractNumbers().First() < 10) { 
tmp = tmp 
.Replace("2", "Two") 
.Replace("3", "Three") 
.Replace("4", "Four") 
.Replace("5", "Five") 
.Replace("6", "Six") 
.Replace("7", "Seven") 
.Replace("8", "Eght") 
.Replace("9", "Nine"); 
} 
result = $"{tmp}-hole punched and ready for use"; 
if (partsQty.HasValue() && partsQty.FirstValue().ExtractNumbers().Any()) { 
result = $"{result}; {partsQty.FirstValue().ExtractNumbers().First()}-tab dividers"; 
} 
} 
else if (numOfHoles.HasValue() 
&& numOfHoles.Values.ExtractNumbers().Any() 
&& numOfHoles.Values.ExtractNumbers().First() > 1) { 
var tmp = numOfHoles.Values.ExtractNumbers().First(); 
if (numOfHoles.Values.ExtractNumbers().First() < 10) { 
tmp = tmp 
.Replace("2", "Two") 
.Replace("3", "Three") 
.Replace("4", "Four") 
.Replace("5", "Five") 
.Replace("6", "Six") 
.Replace("7", "Seven") 
.Replace("8", "Eght") 
.Replace("9", "Nine"); 
} 
result = $"{tmp}-hole punched and ready for use"; 
if (partsQty.HasValue() && partsQty.FirstValue().ExtractNumbers().Any()) { 
result = $"{result}; {partsQty.FirstValue().ExtractNumbers().First()}-tab dividers"; 
} 
} 
else if (partsQty.HasValue() 
&& partsQty.FirstValue().ExtractNumbers().Any() 
&& partsQty.FirstValue().ExtractNumbers().First() > 1) { 
result = $"{partsQty.FirstValue().ExtractNumbers().First()}-tab dividers"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"TabIndexPunchInformation⸮{result}"); 
} 
} 

// "Bullet 5" "Pack Size" 
void ResistantOrProof() { 
var result = ""; 
var features = A[5945]; 
if (Coalesce(features).HasValue("%resistant%", "%proof%") && features.Where("%resistant%", "%proof%").Count() > 1) { 
result = $"{features.Where("%resistant%", "%proof%").Select(o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} for absolute safe keeping"; 
} 
else if (Coalesce(features).HasValue("%resistant%", "%proof%")) { 
result = $"{features.Where("%resistant%", "%proof%").Select(o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} for long-term use"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"ResistantOrProof⸮{result}"); 
} 
} 

// "Additional Archival Safe" 
void ArchivalSafe() { 
var result = ""; 
var archivalSafe = A[5945]; 
if (archivalSafe.HasValue() 
&& archivalSafe.Where("archival safe").Any()) { 
result = "Archival-safe for long-lasting protection"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"AdditionalArchivalSafe⸮{result}"); 
} 
} 

// Additional Binders Pages 
void BindersPages() { 
var result = ""; 
if (SKU.ProductId.In("21711102")) { 
result = @"Pages measure 5.5"" x 8.5"", with one week per two-page spread"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"BindersPages⸮{result}"); 
} 
} 

//13529397020006 end of "Binder Accessories" §§ 

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
DocumentPocket(); 
// "Additional Acid Free" 
KeepsPagesInSafe(); 
RemovableDividers(); 
PenHolder(); 
StaplesBrandGuaranteed(); 
// "Post Consumer Content" 

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
result = $"{tmp}-subject notebook is great for school, home, or work projects"; 
} 
else if (manufacturerProductType.HasValue("composition book")) { 
result = "Composition book is great for note-taking in class or at home"; 
} 
else if (!String.IsNullOrEmpty(notebookType)) { 
result = $"{Coalesce(notebookType.ToLower().ToUpperFirstChar()).Pluralize()} are a great place to take notes and stay organized"; 
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
result = $"{tmp.ToLower().ToUpperFirstChar()} is a great place to take notes and stay organized"; 
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
result = $"This {cnetSize.Flatten("").RegexReplace(@"(\s\(.+?\))", "")} notebook has {sheetsPerPad} sheets"; 
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
if (coverMateral.Contains("poly")) { 
result = "Durable polypropylene covers protect sheets from damage"; 
} 
else if (coverMateral.Contains("cardboard")) { 
result = "Sturdy cardboard allows you to write without a desk surface"; 
} 
else if (coverMateral.Contains("pressboard")) { 
result = "Pressboard covers are durable and keep your notes private"; 
} 
else if (material.HasValue("soft cover")) { 
result = "Medium soft cover notebook makes you feel fancy"; 
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

// "Additional" "Document Pocket" 

void DocumentPocket() { 
var result = ""; 
var checkedVal = A[6031]; 
if (checkedVal.HasValue("%document pocket%")) { 
result = "Interior pocket for professional business cards"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"AdditionalDocumentPocket⸮{result}"); 
} 
} 

// "Additional" "Acid Free" 

// "Additional" "Keeps pages in safe" 

void KeepsPagesInSafe() { 
var result = ""; 
var checkedVal = A[6029]; 
if (checkedVal.HasValue()) { 
result = $"{checkedVal.Values.Select(s => s.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} keeps pages in safe"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"AdditionalKeepsPagesInSafe⸮{result}"); 
} 
} 

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

// "Additional" "Staples brand guaranteed" 

void StaplesBrandGuaranteed() { 
var result = ""; 
var brand = A[606]; 
if (brand.HasValue("Staples")) { 
result = "Staples brand 100% satisfaction guaranteed"; 
} 

if (!String.IsNullOrEmpty(result)) { 
Add($"AdditionalStaplesBrandGuaranteed⸮{result}"); 
} 
} 

//1676378310636165557 end of "Notebooks" §§ 

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
else { 
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
var tmp = bound.FirstValue().ToString()[0].ToString().ToUpper() + bound.FirstValue().ToString().Substring(1); 
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

//1676393410999220835 end of "Notepads" §§ 

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
/*if (DC.FEAT.GetLines().Flatten(" ").HasValue()) { 
arr.Add(DC.FEAT.GetLines().Flatten(" ").ToString()); 
}*/ 
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

// 1676385310823140679 end of "Colored paper" §§ 

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
    var smallerDimention = A[4926]; 
    var isTape = A[4759]; 
    var yield = A[353]; 
    var leght = R("SP-21130").HasValue() ? R("SP-21130") : R("cnet_common_SP-21130");
    var tapeWidth = R("SP-21265").HasValue() ? R("SP-21265") : R("cnet_common_SP-21265");
    
    if (size.HasValue("%Roll%")
    && isTape.HasValue("%tape%")
    && leght.HasValue()
    && tapeWidth.HasValue()) {
        result = $"Create labels for home, work, or school with this {tapeWidth}\" wide ##and {leght}' yield label tape";
    }
    else if (isTape.HasValue("%label%")
    && yield.HasValue()
    && yield.FirstValue().ExtractNumbers().Any() 
    && yield.FirstValue().ExtractNumbers().First() > 1 
    && tapeWidth.HasValue()) {
        result = $"Creates a {tapeWidth}\" label one products, files, and drawers; yields up to {yield.FirstValue()} labels";
    }
    else if (tapeWidth.HasValue()) { 
        var tmp1 = Math.Round((double)smallerDimention.FirstValue().ExtractNumbers().First() * 0.0393701, 2); 
        result = $"Creates a {tapeWidth}\" label one products, files, and drawers"; 
    }
    if (size.HasValue("%Roll%") 
    && size.HasValue("%m)%") 
    && isTape.HasValue("%tape%") 
    && size.FirstValue().ExtractNumbers().Any()) { 
        var tmp1 = Math.Round((double)size.FirstValue().ExtractNumbers().First() * 0.393701, 2); 
        var tmp2 = Math.Round((double)size.FirstValue().ExtractNumbers().Last() * 3.28084, 2); 
        result = $"Create labels for home, work, or school with this {tmp1}\" wide ##and {tmp2}' yield label tape"; 
    } 
    else if (size.HasValue("%mm%") 
    && isTape.HasValue("%label%") 
    && yield.HasValue()
    && yield.FirstValue().ExtractNumbers().Any() 
    && yield.FirstValue().ExtractNumbers().First() > 1 
    && smallerDimention.HasValue()) { 
        var tmp1 = Math.Round((double)smallerDimention.FirstValue().ExtractNumbers().First() * 0.0393701, 2); 
        result = $"Creates a {tmp1}\" label one products, files, and drawers; yields up to {yield.FirstValue()} labels"; 
    } 
    else if (size.HasValue("%mm%")) { 
        var tmp1 = Math.Round((double)smallerDimention.FirstValue().ExtractNumbers().First() * 0.0393701, 2); 
        result = $"Creates a {tmp1}\" label one products, files, and drawers"; 
    }
    else if (size.HasValue("%cm%") 
    && isTape.HasValue("%label%") 
    && yield.HasValue()
    && yield.FirstValue().ExtractNumbers().Any() 
    && yield.FirstValue().ExtractNumbers().First() > 1 
    && smallerDimention.HasValue()) { 
        var tmp1 = Math.Round((double)smallerDimention.FirstValue().ExtractNumbers().First() * 0.393701, 2); 
        result = $"Creates a {tmp1}\" label one products, files, and drawers; yields up to {yield.FirstValue()} labels"; 
    }
    else if (size.HasValue("%cm%")) { 
        var tmp1 = Math.Round((double)smallerDimention.FirstValue().ExtractNumbers().First() * 0.393701, 2); 
        result = $"Creates a {tmp1}\" label one products, files, and drawers"; 
    }
    if(!String.IsNullOrEmpty(result)) { 
        Add($"LabelMakerTapeWidth⸮{result}"); 
    } 
} 


// --[FEATURE #2] 
// --True color (print True color on label True color) 

void PrintTrueColorOnLabelTrueColor() { 
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
    var cp = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body); 
    if (cp.HasValue()) { 
        result = $"Compatible with: {cp.Text.Split(", ").Take(5).Flatten(", ").Replace(";", ",")}"; 
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

// 1681361910108140557 end of "Label Maker Tapes & Printer Labels" §§ 

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
var trueColor = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : ""; 
//Standard|Heavy Duty|Economy 
var durabilityType = R("SP-21022").HasValue() ? R("SP-21022").Replace("<NULL>", "").Text : R("cnet_common_SP-21022").HasValue() ? R("cnet_common_SP-21022").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(durabilityType)) { 
durabilityType = $"with {durabilityType.ToLower()} durability "; 
} 
if (!String.IsNullOrEmpty(binderType)) { 
if (!String.IsNullOrEmpty(trueColor) && binderType.ToLower().Equals("view binders")) { 
var tmp = trueColor[0].ToString().ToUpper() + trueColor.Substring(1).ToLower(); 
result = $"{tmp} view binder {durabilityType}is an excellent way to store spreadsheets"; 
} 
else if (!String.IsNullOrEmpty(trueColor)) { 
var tmp = trueColor[0].ToString().ToUpper() + trueColor.Substring(1).ToLower(); 
result = tmp.Split(' ').Count() > 1 ? $"{tmp}, {binderType.ToLower().Replace("binders", "binder")} {durabilityType}protects and organizes working documents" : 
$"{tmp} {binderType.ToLower().Replace("binders", "binder")} {durabilityType}protects and organizes working documents"; 
} 
else if (binderType.ToLower().Equals("ledger binders")) { 
result = $"Ledger binder {durabilityType}is ideal for large spreadsheets, blueprints, engineering, mining, and construction plans" ; 
} 
else if (binderType.ToLower().Equals("flexible poly binders")) { 
result = $"Flexible poly construction {durabilityType}for easy carrying"; 
} 

else { 
var tmp = binderType[0].ToString().ToUpper() + binderType.Substring(1).ToLower(); 
result = $"{tmp.Replace("binders", "binder")} {durabilityType}protects and organizes working documents"; 
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
if (ringType.ToLower().StartsWith("d-ring")) { 
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
result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType).ToLower().Replace(" ring", "")} rings keep pages secure"; 
} else { 
result = $"{Coalesce(numberOfRings).ExtractNumbers().First()} {Coalesce(ringType).ToLower().Replace(" ring", "")} ring keep pages secure"; 
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
|| supportedFormatUSM.Contains("8.5 in x 17 in")) { 
result = @"Accommodates 8.5"" x 17"" ledger-size pages"; 
} 
else if (folderOrPaperSize.ToLower().Contains("legal") 
|| supportedFormatUSM.Contains("8.5 in x 14 in") 
|| supportedFormatUSM.Contains("Legal")) { 
result = @"Accommodates 8.5"" x 14"" legal-size pages"; 
} 
else if (folderOrPaperSize.ToLower().Contains("us letter") 
|| supportedFormatUSM.Contains("Letter") 
|| supportedFormatUSM.Contains("8.5 in x 11 in")) { 
result = @"Accommodates 8.5"" x 11"" letter-size pages"; 
} 
else if (folderOrPaperSize.ToLower().Contains("check")) { 
result = "Protect checks and keep them organized"; 
} 

else if (!String.IsNullOrEmpty(folderOrPaperSize) && !folderOrPaperSize.ToLower().Contains("other")) { 
result = $"Accommodates {folderOrPaperSize.Replace(" (Continuous)", "")} pages"; 
} 
} 
else if (!String.IsNullOrEmpty(supportedFormatUSM)) { 
var tmp = Coalesce(supportedFormatUSM.Split("(").Last()).Replace(" in", "\"").Replace(")", ""); 
result = $"Accommodates {tmp} pages"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"InputSize⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Material of Item & Finish 
void MaterialOfItemFinish() { 
var result = ""; 
var material = R("SP-21408").HasValue() ? R("SP-21408").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21408").HasValue() ? R("cnet_common_SP-21408").Replace("<NULL>", "").Text : ""; 
var surface = A[5945]; 
if (!String.IsNullOrEmpty(material)) { 
var tmp = material.ToLower(true); 
if (surface.HasValue("%non%stick%")) { 
result = $"Made of {tmp} with non-stick surface"; 
} 
else if (surface.HasValue("%surface%")) { 
var tmp1 = surface.Where("%surface%").First().Value(); 
result = $"Made of {material.ToLower(true)} with {tmp1}"; 
} 
else { 
result = $"Made of {material.ToLower(true)}"; 
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
        var tmp = design.WhereNot("%spine%","front cover full size pocket", "back cover full size pocket", "front cover pocket", "back cover pocket").Match(5971, 5965).Values("x").Where(o => o.In("%inner%pocket%")).Flatten(" ").Replace("<NULL>", "1"); 
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
                .Replace("x<NULL>", "") 
                .ToString(); 
            tmp1 = tmp1.ToUpperFirstChar();
            result = $"{tmp1} pocket for added organization"; 
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
result = $"Insert custom {tmp} labels for easy identification"; 
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

//135293970167205 end of "Binders" §§ 

//§§168136191010890400 "Label Makers" "Alex K." 

LabelMakerTypeAndUse(); 
LabelMakerMaxPrintSpeed(); 
LabelMakerMaximumResolution(); 
LabelMakerPrintingTechnology(); 
MediaSupportedTypeOfLabels(); 
InterfaceOrPortType(); 
ScreenSizeInInches(); 
OutputTypeFontSizeStyles(); 
LabelMakerConnectionType(); 
// Dimentions 
LabelMakerBatterySize(); 
// Warranty 

// --[FEATURE #1] 
// --Label maker type & Use 
void LabelMakerTypeAndUse() { 
var result = ""; 
//Printhead|Cleaning Card|Label Maker|Charger|Label Printer 
var type = R("SP-21843").HasValue() ? R("SP-21843").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21843").HasValue() ? R("cnet_common_SP-21843").Replace("<NULL>", "").Text : ""; 


if (!String.IsNullOrEmpty(type)) { 
if (type.ToLower().Equals("label maker")) { 
result = "Label maker for quick organization of items and documents"; 
} 
else if (type.ToLower().Equals("label printer")) { 
result = "Label printer for customized creation and printing of labels"; 
} 
//https://www.staples.com/Brother-AD24-Label-Maker-Power-Adapter/product_407288 
//https://www.staples.com/rollo-shipping-label-printer-direct-thermal-4-6-printer-compare-to-dymo-4xl/product_24307174 
else { 
result = $"{type.ToLower().ToUpperFirstChar()} for label makers"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"LabelMakerTypeAndUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Max print speed 
void LabelMakerMaxPrintSpeed() { 
var result = ""; 
var printSpeed = R("SP-21839").HasValue() ? R("SP-21839").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21839").HasValue() ? R("cnet_common_SP-21839").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(printSpeed)) { 
result = $"Maximum print speed up to ##{printSpeed} ips for enhanced productivity"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"LabelMakerMaxPrintSpeed⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Maximum resolution 
void LabelMakerMaximumResolution() { 
var result = ""; 
var maximumResolution = R("SP-25").HasValue() ? R("SP-25").Replace("<NULL>", "").Text : R("cnet_common_SP-25").HasValue() ? R("cnet_common_SP-25").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(maximumResolution)) { 
result = $"Offers printing with {maximumResolution.ToLower()} maximum resolution"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"LabelMakerMaximumResolution⸮{result}"); 
} 
} 

// --[FEATURE #4] 
// --Printing technology 
void LabelMakerPrintingTechnology() { 
var result = ""; 
var printingTechnology = R("SP-22121").HasValue() ? R("SP-22121").Replace("<NULL>", "").Text : R("cnet_common_SP-22121").HasValue() ? R("cnet_common_SP-22121").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(printingTechnology)) { 
if (printingTechnology.ToLower().Equals("thermal transfer")) { 
result = "Thermal transfer printing technology eliminates the need for ink"; 
} 
else if (printingTechnology.ToLower().Equals("direct thermal")) { 
result = "Direct thermal print is not affected by sunlight or friction"; 
} 
else { 
result = $"{printingTechnology.ToLower().ToUpperFirstChar()} technology prints professional-quality labels"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"LabelMakerPrintingTechnology⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Media supported/Type of labels (Type & Size) 

void MediaSupportedTypeOfLabels() { 
var result = ""; 
var compatibleTapes = A[2942]; 
var maximumLabelWidth = R("SP-22123").HasValue() ? R("SP-22123").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22123").HasValue() ? R("cnet_common_SP-22123").Replace("<NULL>", "").Text : ""; 

var maximumLabelLength = R("SP-22122").HasValue() ? R("SP-22122").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22122").HasValue() ? R("cnet_common_SP-22122").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(maximumLabelWidth) && !String.IsNullOrEmpty(maximumLabelLength) && compatibleTapes.HasValue()) { 
if (compatibleTapes.Values.Count() > 1) { 
result = $"Uses {compatibleTapes.Values.Select(o => o.Value().Replace(" tape", "")).FlattenWithAnd()} tapes up to {maximumLabelWidth}\" wide and {maximumLabelLength}\" long"; 
} 
else if (compatibleTapes.Values.Count() == 1) { 
result = $"Uses {compatibleTapes.FirstValue().Replace(" tape", "")} tape up to {maximumLabelWidth}\" wide and {maximumLabelLength}\" long"; 
} 
} 
else if (!String.IsNullOrEmpty(maximumLabelWidth) && compatibleTapes.HasValue()) { 
if (compatibleTapes.Values.Count() > 1) { 
result = $"Uses {compatibleTapes.Values.Select(o => o.Value().Replace(" tape", "")).FlattenWithAnd()} tapes up to {maximumLabelWidth}\" wide"; 
} 
else if (compatibleTapes.Values.Count() == 1) { 
result = $"Uses {compatibleTapes.FirstValue().Replace(" tape", "")} tape up to {maximumLabelWidth}\" wide"; 
} 
} 
else if (!String.IsNullOrEmpty(maximumLabelLength) && compatibleTapes.HasValue()) { 
if (compatibleTapes.Values.Count() > 1) { 
result = $"Uses {compatibleTapes.Values.Select(o => o.Value().Replace(" tape", "")).FlattenWithAnd()} tapes up to {maximumLabelLength}\" long"; 
} 
else if (compatibleTapes.Values.Count() == 1) { 
result = $"Uses {compatibleTapes.FirstValue().Replace(" tape", "")} tape up to {maximumLabelLength}\" long"; 
} 
} 
else if (compatibleTapes.HasValue()) { 
if (compatibleTapes.Values.Count() > 1) { 
result = $"Uses {compatibleTapes.Values.Select(o => o.Value().Replace(" tape", "")).FlattenWithAnd()} tapes"; 
} 
else if (compatibleTapes.Values.Count() == 1) { 
result = $"Uses {compatibleTapes.FirstValue().Replace(" tape", "")} tape"; 
} 
} 
else if (!String.IsNullOrEmpty(maximumLabelWidth) && !String.IsNullOrEmpty(maximumLabelLength) && compatibleTapes.HasValue()) { 
result = $"Uses tape up to {maximumLabelWidth}\" wide and {maximumLabelLength}\" long"; 
} 
else if (!String.IsNullOrEmpty(maximumLabelWidth)) { 
result = $"Uses tape up to {maximumLabelWidth}\" wide"; 
} 
else if (!String.IsNullOrEmpty(maximumLabelLength)) { 
result = $"Uses tape up to {maximumLabelLength}\" long"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"MediaSupportedTypeOfLabels⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Interface or port type (If Applicable) 
void InterfaceOrPortType() { 
var result = ""; 
var interfaceOrPortType = R("SP-21791").HasValue() ? R("SP-21791").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21791").HasValue() ? R("cnet_common_SP-21791").Replace("<NULL>", "").Text : ""; 

if (!String.IsNullOrEmpty(interfaceOrPortType) && !interfaceOrPortType.ToLower().Contains("none")) { 
result = $"{interfaceOrPortType.Split(", ").Select(o => "##"+ o.Replace("Wi-Fi(n)", "Wi-Fi (n)")).FlattenWithAnd(10, ", ")} for easy ##and flexible connectivity"; 
} 
if (!String.IsNullOrEmpty(result)) { 
var tmp = $"InterfaceOrPortType⸮{result}"; 
result = tmp.Split("⸮##").Last()[0].ToString().ToUpper() + tmp.Split("⸮##").Last().Substring(1); 
Add($"InterfaceOrPortType⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Screen size in Inches 
void ScreenSizeInInches() { 
var result = ""; 
var vortexScreenLines = A[2950]; 
var screenSize = R("SP-21574").HasValue() ? R("SP-21574").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21574").HasValue() ? R("cnet_common_SP-21574").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(screenSize)) { 
result = $"Features {screenSize}\" display helps to view the images on screen"; 
} 
else if (vortexScreenLines.HasValue()) { 
if (vortexScreenLines.HasValue("1 line x % characters")) { 
result = "Screen displays up to 12 characters to view text before printing"; 
} 
else if (vortexScreenLines.HasValue("% line x % characters")) { 
var tmpLast = vortexScreenLines.FirstValue().ToString().Split(" x ").Last().Replace(" ", "-").Replace("characters", "character"); 
var tmpFirst = vortexScreenLines.FirstValue().ToString().Split(" x ").First().Replace(" ", "-").Replace("lines", "line"); 
result = $"{tmpLast}, {tmpFirst} graphical display to view text before printing"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"ScreenSizeInInches⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Output Type (font sizes/styles, templates/columns, symbols and clip-art); 
void OutputTypeFontSizeStyles() { 
var result = ""; 
var fonts = A[2944]; 
var characterSizes = A[2945]; 
var stylesEffects = A[2946]; 
if (fonts.HasValue() || characterSizes.HasValue() || stylesEffects.HasValue()) { 
//_A[2944].Values.Count.Postfix(" fonts").Prefix(" ").Replace(" 1 fonts", " one font") 
result = "Choose from"; 
if (fonts.HasValue()) { 
var numOfFonts = ""; 
if (fonts.Values.ExtractNumbers().Any() 
&& !fonts.HasValue("Arial 65") 
&& !fonts.HasValue("Monospace 821") 
&& !fonts.HasValue("Chinese Simplified (SimSun GB18030)") 
&& !fonts.HasValue("PT Dingbats 2") && !fonts.HasValue("Swiss 721%") 
&& !fonts.HasValue("Traditional Chinese (Big5)")) { 
numOfFonts = fonts.Values.ExtractNumbers().First().ToString(); 
} 
else { 
numOfFonts = fonts.Values.Count().ToString(); 
} 
result = result + " " + (numOfFonts.Equals("1") ? "one font" : (numOfFonts + " fonts")); 
} 
if (characterSizes.HasValue()) { 
var separator = result.Length > 11 ? ", " : " "; 
result =(result + separator + characterSizes.Values.Count() + " font sizes").Replace(" 1 font sizes", " one font size"); 
} 
if (stylesEffects.HasValue()) { 
var separator = result.Length > 11 ? ", " : " "; 
result =(result + separator + stylesEffects.Values.Count() + " font styles").Replace(" 1 font styles", " one font style"); 
} 
result = result + " for user convenience"; 
if (result.Equals("Choose from one font style for user convenience")) { 
result = "One font style for user convenience"; 
} 
else if (result.Equals("Choose from one font size for user convenience")) { 
result = "One font size for user convenience"; 
} 
else if (result.Equals("Choose from one font for user convenience")) { 
result = "One font for user convenience"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"OutputTypeFontSizeStyles⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Label maker connection type 
void LabelMakerConnectionType() { 
var result = ""; 
//None|Bluetooth|Wireless|USB, Wireless, Ethernet|USB, Ethernet, Serial|USB & Ethernet|USB, Serial|USB & Wireless|USB, Wireless, Ethernet, Serial|USB, Serial, Parallel|USB|USB, Wireless, Ethernet, Serial, Bluetooth 
var connectionType = R("SP-18625").HasValue() ? R("SP-18625").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18625").HasValue() ? R("cnet_common_SP-18625").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(connectionType) && !connectionType.ToLower().Equals("none")) { 
if (connectionType.ToLower().Equals("usb")) { 
result = "Connects easily with a USB cable"; 
} else { 
var isPluralInteface = ""; 
if (connectionType.Split(", ").Count() > 1) { 
isPluralInteface = "intefaces"; 
} else { 
isPluralInteface = "inteface"; 
} 
result = $"Connects easily with {connectionType.ToLower().Split(", ").FlattenWithAnd().Replace("usb", "USB").Replace("ethernet", "Ethernet").Replace("bluetooth", "Bluetooth").Replace("&", "and")} {isPluralInteface}"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"LabelMakerConnectionType⸮{result}"); 
} 
} 
// --[FEATURE #10] 
// --Dimensions 

// --[FEATURE #11] 
// --Battery size 
void LabelMakerBatterySize() { 
var result = ""; 
//Lithium Ion|AAAA|AAA|AA|C|223|D|123|9V|CR2|3V|6LF22|6V|1.5V|12V|3.7V|CR17345|2CR5|CRV3|N|CR2016|AAA/AA|CR2032 
var batterySize = R("SP-18114").HasValue() ? R("SP-18114").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18114").HasValue() ? R("cnet_common_SP-18114").Replace("<NULL>", "").Text : ""; 

var count = A[861]; 
if (!String.IsNullOrEmpty(batterySize) && !batterySize.ToLower().Equals("none")) { 
if (count.HasValue() && count.Values.ExtractNumbers().Any() && count.Values.ExtractNumbers().First() > 1) { 
result = $"Runs on ##{count.FirstValue()} x {batterySize.Replace("Lithium Ion", "lithium ion").Replace("Lithium Polymer", "Lithium Polymer")} batteries"; 
} 
else { 
result = $"Runs on {batterySize.Replace("Lithium Ion", "lithium ion").Replace("Lithium Polymer", "Lithium Polymer")} battery"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"LabelMakerBatterySize⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Warranty 

//168136191010890400 end of "Label Makers" §§ 

//§§16831375514580302 "Batteries" "Alex K." 

BatterySize(); 
BatteryType(); 
Rechargeable(); 
BatteryCapacity(); 
BatteryUse(); 
AdditionalHiDensity(); 
AdditionalDuralockPowerPreserve(); 
AdditionalM3Technology(); 
AdditionalBatteriesCompatibleProducts();
// warranty 

// --[FEATURE #1] 
// --Battery size 
void BatterySize() { 
    var result = ""; 
    //Lithium Ion|AAAA|AAA|AA|C|223|D|123|9V|CR2|3V|6LF22|6V|1.5V|12V|3.7V|CR17345|2CR5|CRV3|N|CR2016|AAA/AA|CR2032 
    var batterySize = R("SP-18114").HasValue() ? R("SP-18114").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18114").HasValue() ? R("cnet_common_SP-18114").Replace("<NULL>", "").Text : ""; 
    // Lithium|NiMH|Lithium Polymer|Alkaline|Silver Oxide|Charger
    var batteryType = R("SP-18115").HasValue() ? R("SP-18115").Replace("<NULL>", "").Text : 
        R("cnet_common_SP-18115").HasValue() ? R("cnet_common_SP-18115").Replace("<NULL>", "").Text : ""; 
    
    if (!String.IsNullOrEmpty(batterySize)) { 
        if (batterySize.ToLower().Equals("aaa")) { 
            result = "AAA batteries power a variety of handheld electronics, gadgets and alarms"; 
        } 
        else if (batterySize.ToLower().Equals("aa")) { 
            result = "AA batteries give your devices the consistent power they need to work at their best"; 
        } 
        else if (batterySize.ToLower().Equals("c")) { 
            result = "C batteries provide reliable, convenient and long-lasting power for your electronic devices"; 
        } 
        else if (batterySize.ToLower().Equals("d")) { 
            result = "D batteries easily power your small electronic devices"; 
        } 
        else if (batterySize.ToLower().Equals("9v")) { 
            result = "9V batteries are the perfect choice when you need to operate with essential electronic items"; 
        } 
        else if (batterySize.ToLower().Equals("9v")) { 
            result = "9V batteries are the perfect choice when you need to operate with essential electronic items"; 
        } 
        else { 
            result = $"{batterySize} batteries for everyday use in devices"; 
        } 
    } 
    else if(!String.IsNullOrEmpty(batteryType)){
        result = $"Battery type: {batteryType.ToLower()}";
    }
    if (!String.IsNullOrEmpty(result)) { 
        Add($"BatterySize⸮{result}"); 
    } 
} 

// --[FEATURE #2] 
// --Battery Type 
void BatteryType() { 
var result = ""; 
//Lithium|NiMH|Lithium Polymer|Alkaline|Silver Oxide|Charger 
var type = R("SP-18115").HasValue() ? R("SP-18115").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18115").HasValue() ? R("cnet_common_SP-18115").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(type)) { 
if (type.ToLower().Equals("alkaline")) { 
result = "The alkaline battery technology is designed to release the right amount of power when needed"; 
} 
else if (type.ToLower().Contains("lithium")) { 
result = $"{type.ToLower().ToUpperFirstChar()} technology for ultimate power"; 
} 
else if (type.ToLower().Equals("silver oxide")) { 
result = "Silver oxide batteries provide reliable, long-lasting power for your electronic devices"; 
} 
else if (type.ToLower().Equals("nimh")) { 
result = "NiMH batteries provide a reusable power source for frequently used and high-drain devices"; 
} 
else if (type.ToLower().Equals("charger")) { 
result = $"{type.ToLower().ToUpperFirstChar()} technology for ultimate power"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"BatteryType⸮{result}"); 
} 
} 
// --[FEATURE #3] 
// --Rechargeable 
void Rechargeable() { 
var result = ""; 
var type = R("SP-22066").HasValue() ? R("SP-22066").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22066").HasValue() ? R("cnet_common_SP-22066").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(type)) { 
if (type.ToLower().Equals("rechargeable")) { 
result = "Battery can be charged again and again to keep your device powered while you're on-the-go"; 
} 
else if (type.ToLower().Equals("not rechargeable")) { 
result = $"Not rechargeable"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"Rechargeable⸮{result}"); 
} 
} 

// --[FEATURE #4] 
// --Capacity (mAh) 
void BatteryCapacity() { 
var result = ""; 
var capacity = R("SP-21541").HasValue() ? R("SP-21541").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21541").HasValue() ? R("cnet_common_SP-21541").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(capacity)) { 
result = $"{capacity}mAh capacity"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"BatteryCapacity⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// --Battery general use 
void BatteryUse() { 
var result = ""; 
//General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset 
var use = R("SP-24312").HasValue() ? R("SP-24312").Replace("<NULL>", "").Text : 
R("cnet_common_SP-24312").HasValue() ? R("cnet_common_SP-24312").Replace("<NULL>", "").Text : ""; 
// 
if (!String.IsNullOrEmpty(use)) { 
if (use.ToLower().Equals("general-purpose use")) { 
result = "These general-purpose batteries are perfect for cameras and other small devices"; 
} 
else { 
result = $"Battery is ideal to use in {use.ToLower()}"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"BatteryUse⸮{result}"); 
} 
} 
// --[FEATURE #6] 
// --additional Hi-Density 
void AdditionalHiDensity() { 
var result = ""; 
//General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset 
var features = A[1505]; 
if (features.HasValue("Hi-Density Core")) { 
result = "Features ##Hi-Density ##Core technology for unbeatable long-lasting power"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"AdditionalHiDensity⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --additional Power Check 
AdditionalPowerCheck(); 
void AdditionalPowerCheck() { 
var result = ""; 
//General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset 
var features = A[1505]; 
if (features.HasValue("Duracell Power Check")) { 
result = "Power check lets you see how much energy is left with the push of a button"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"AdditionalPowerCheck⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --additional 
void AdditionalDuralockPowerPreserve() { 
var result = ""; 
//General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset 
var features = A[1505]; 
if (features.HasValue("Duralock Power Preserve Technology")) { 
result = "Duralock Power Preserve™ Technology is guaranteed for 10 years in storage"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"AdditionalDuralockPowerPreserve⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --additional 

void AdditionalM3Technology() { 
var result = ""; 
//General-Purpose Use|Car Security, Organizers and glucometers|Toys, Flashlights, remote controls and fire alarms|Portable CD players, Shavers, Radios and Smoke alarms|Headset 
var features = A[1505]; 
if (features.HasValue("Duracell M3 technology")) { 
result = "Features Duracell M3 technology"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"AdditionalM3Technology⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --additional 
void AdditionalBatteriesCompatibleProducts() { 
    var result = ""; 
    var CompatibleProducts = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body, SPEC["MS"].GetLine("Designed For").Body, SPEC["ES"].GetLine("Designed For").Body);
    
    if (CompatibleProducts.HasValue()) { 
        result = $"Compatible with: {CompatibleProducts.Text.Replace("plus", "Plus").Replace("Envy", "ENVY").Replace("Deskjet", "DeskJet").Replace("Officejet", "OfficeJet").Replace("ImageCLASS","imageCLASS").Replace("Kyocera", "KYOCERA").Replace("Copycentre","CopyCentre").Replace("B405/z","B405/Z").Replace("Troy", "TROY").Replace(";", ",").Split(' ').Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).Flatten(" ")}"; 
    } 
    if (!String.IsNullOrEmpty(result)) { 
        Add($"AdditionalBatteriesCompatibleProducts⸮{result}"); 
    } 
} 

// Warranty 

//16831375514580302 end of "Batteries" §§ 

//§§1683393110991141836 -- "Hand Soap & Dispensers" "Alex K." 

CleanserFormFactorAndUse(); 
SoapDispensersScent(); 
CapacityAndContainerTypes(); 
//Pack Qty (If more than 1) 
SoapDispensersAntibacterial(); 
Moisturizer(); 
Hypoallergenic(); 
StainRemoval(); 
pHBalaced(); 
SoapDispensersVitamins(); 
AloeGlycerin(); 
Pumice(); 
LockingMechanism(); 
ContainerFeatures(); 
DispenserMaterial(); 
AutomaticManualDispensers(); 
// Standards 
// Warranty 

// --[FEATURE #1] 
// --Cleanser form factor & use 
void CleanserFormFactorAndUse() { 
var result = ""; 
//Push-Style Dispenser|Automatic Dispensers|Touch-Free Dispenser|Manual Dispensers|Liquid Sanitizers|Foaming Sanitizers|--Liquid Soap|Dispenser & Refill Kit|Wipes|Refills|--Foaming Soap|Gel Sanitizers 
var cleanserFormFactor = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : ""; 

if (!String.IsNullOrEmpty(cleanserFormFactor)) { 
if (cleanserFormFactor.ToLower().EndsWith("soap")) { 
result = $"{cleanserFormFactor.ToLower().ToUpperFirstChar()} provides an efficient way to maintain hygiene levels"; 
} 
else if (cleanserFormFactor.ToLower().Equals("automatic dispensers")) { 
result = "Automatically dispenses the perfect amount of foam soap needed for hand washing in one shot"; 
} 
else if (cleanserFormFactor.ToLower().Equals("manual dispensers")) { 
result = "Manual soap dispenser for reliable, hygienic soap dispensing at all your handwashing stations"; 
} 
else if (cleanserFormFactor.ToLower().Equals("foaming sanitizers")) { 
result = "Specially designed foaming formula provides gentle cleansing"; 
} 
else if (cleanserFormFactor.ToLower().Equals("gel sanitizers")) { 
result = "Advanced gel sanitizer formulation designed for healthcare environments"; 
} 
else if (cleanserFormFactor.ToLower().Equals("liquid sanitizers")) { 
result = "Liquid sanitizer is designed to keep you germ-free without sacrificing everyday personal comfort"; 
} 
else if (cleanserFormFactor.ToLower().Equals("wipes")) { 
result = "Wipes are ideal for use anytime your employees do not have easy access to running water"; 
} 
else if (cleanserFormFactor.ToLower().Equals("refills")) { 
result = "Refills are an economical alternative to bar soaps"; 
} 
else { 
result = $"{cleanserFormFactor.ToLower().ToUpperFirstChar()} provides gentle cleansing"; 
} 
} 

if(!String.IsNullOrEmpty(result)) { 
Add($"CleanserFormFactorAndUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Scent (If Applicable) 
void SoapDispensersScent() { 
var result = ""; 
var fragrance = A[7356]; 
//Push-Style Dispenser|Automatic Dispensers|Touch-Free Dispenser|Manual Dispensers|Liquid Sanitizers|Foaming Sanitizers|Liquid Soap|Dispenser & Refill Kit|Wipes|Refills|Foaming Soap|Gel Sanitizers 
var cleanserFormFactor = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : ""; 
//Woodsy|Lavender|Tea|Original|Seasons|Strawberry|Clean|Floral|Masculine|Bakery|Fruity|No Scent|Nature|Spicy|Oceanic|Fresh Citrus|Lemon|Unscented|Other|Gain|Low odor|Almond|Vanilla|Herbal 
var scent = R("SP-18147").HasValue() ? R("SP-18147").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18147").HasValue() ? R("cnet_common_SP-18147").Replace("<NULL>", "").Text : ""; 
/* 
if (!String.IsNullOrEmpty(cleanserFormFactor) && fragrance.HasValue()) { 
result = $"{cleanserFormFactor.ToLower().ToUpperFirstChar()} leaves a pleasant {fragrance.Values.Select(o => o.Value()).FlattenWithAnd().ToLower()}"; 
} 
*/ //else 
if (fragrance.HasValue()) { 
result = $"{fragrance.Values.Select(o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} scent leaves a pleasant aroma"; 
} 
else if (!String.IsNullOrEmpty(scent) && !scent.ToLower().Equals("no scent")) { 
result = $"{scent.ToLower().ToUpperFirstChar()} scent leaves a pleasant aroma"; 
} 
else if (!String.IsNullOrEmpty(cleanserFormFactor) && !cleanserFormFactor.ToLower().Contains("dispenser")) { 
result = $"Unscented formula for those sensitive to fragrances"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"SoapDispensersScent⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Capacity (oz.) & container types 
void CapacityAndContainerTypes() { 
var result = ""; 
var capacityOz = R("SP-21132").HasValue() ? R("SP-21132").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21132").HasValue() ? R("cnet_common_SP-21132").Replace("<NULL>", "").Text : ""; 
//Push-Style Dispenser|Automatic Dispensers|Touch-Free Dispenser|Manual Dispensers|Liquid Sanitizers|Foaming Sanitizers|Liquid Soap|Dispenser & Refill Kit|Wipes|Refills|Foaming Soap|Gel Sanitizers 
var cleanserFormFactor = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : ""; 
var containerTypes = R("SP-24477").HasValue() ? R("SP-24477").Replace("<NULL>", "").Text : 
R("cnet_common_SP-24477").HasValue() ? R("cnet_common_SP-24477").Replace("<NULL>", "").Text : ""; 

if (!String.IsNullOrEmpty(capacityOz)) { 
if (!String.IsNullOrEmpty(cleanserFormFactor) && cleanserFormFactor.ToLower().EndsWith("dispensers")) { 
result = $"Comes in {capacityOz} fl. oz. dispenser"; 
} 
else if (!String.IsNullOrEmpty(containerTypes)) { 
result = $"{capacityOz} oz. {containerTypes.ToLower()}"; 
} else { 
result = $"Comes in capacity of {capacityOz} oz."; 
} 
} 

if(!String.IsNullOrEmpty(result)) { 
Add($"CapacityAndContainerTypes⸮{result}"); 
} 
} 
// --[FEATURE #4] 
// --Pack Qty (If more than 1) 

// --[FEATURE #5] 
// --Additional Antibacterial 
void SoapDispensersAntibacterial() { 
var result = ""; 
var antibacterial = A[7363]; 
var features = A[7379]; 
var components = A[7355]; 

if (antibacterial.HasValue()) { 
result = "Provides a high level of germ-killing action to stop spread of infection"; 
} 
else if (features.HasValue("antibacterial")) { 
result = "Antibacterial for a healthier workplace"; 
} 
else if (components.HasValue("triclosan")) { 
result = "Contains triclosan to kill bacteria and microbes"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"SoapDispensersAntibacterial⸮{result}"); 
} 
} 
// --[FEATURE #6] 
// --Additional Moisturizer 
void Moisturizer() { 
var result = ""; 
var moisturizer = A[7361]; 

if (moisturizer.HasValue()) { 
result = "Contains moisturizers that help prevent your hands from drying"; 
} 

if(!String.IsNullOrEmpty(result)) { 
Add($"Moisturizer⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Additional Hypoallergenic 
void Hypoallergenic() { 
var result = ""; 
var features = A[7367]; 

if (features.HasValue("hypoallergenic")) { 
result = "Hypoallergenic, so it has less of a chance of causing flare-ups of skin allergies"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"Hypoallergenic⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Additional Stain Removal 
void StainRemoval() { 
var result = ""; 
var stainRemoval = A[7358]; 

if (stainRemoval.HasValue()) { 
result = $"Effectively removes {stainRemoval.Values.Select(o => o.Value().ToLower()).FlattenWithAnd()} stains"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"StainRemoval⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Additional pH Balaced 
void pHBalaced() { 
var result = ""; 
var features = A[7367]; 

if (features.HasValue("balanced pH")) { 
result = "pH balanced to promote skin comfort"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"pHBalaced⸮{result}"); 
} 
} 

// --[FEATURE #10] 
// --Additional Vitamins 
void SoapDispensersVitamins() { 
var result = ""; 
var components = A[7355]; 
if (Coalesce(components).HasValue("%vitamin%")) { 
result = $"Enriches your skin with vitamin {components.Where("%vitamin%").Select(o => o.Value().Replace("vitamin ", "")).FlattenWithAnd()}"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"SoapDispensersVitamins⸮{result}"); 
} 
} 

// --[FEATURE #11] 
// --Additional Aloe Glycerin 
void AloeGlycerin() { 
var result = ""; 
var components = A[7355]; 
if (Coalesce(components).HasValue("aloe%", "glycerin%")) { 
result = $"Includes {components.Where("aloe%","glycerin%").Select(o => o.Value()).FlattenWithAnd()} to leave skin soft, smooth and refreshed"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"AloeGlycerin⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Additional Pumice 
void Pumice() { 
var result = ""; 
var components = A[7355]; 
if (Coalesce(components).HasValue("%pumice%")) { 
result = $"{components.Where("%pumice%").Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} for an effective hand cleanup"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"Pumice⸮{result}"); 
} 
} 

// --[FEATURE #13] 
// --Additional Locking Mechanism 
void LockingMechanism() { 
var result = ""; 
var lockable = A[7372]; 
var features = A[7379]; 
if (lockable.HasValue() || features.HasValue("key lock")) { 
result = "Locking mechanism helps prevent tampering and vandalism"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"LockingMechanism⸮{result}"); 
} 
} 

// --[FEATURE #14] 
// --Additional 
void ContainerFeatures() { 
var result = ""; 
var transparentCover = A[7371]; 
var features = A[7379]; 
if (features.HasValue("sight window")) { 
result = "Sight window makes it easy to check fill status"; 
} 
else if (transparentCover.HasValue()) { 
result = "Transparent container allows for quick monitoring of soap level"; 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"ContainerFeatures⸮{result}"); 
} 
} 

// --[FEATURE #15] 
// --Additional 
void DispenserMaterial() { 
var result = ""; 
var type = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : ""; 

var material = A[372]; 
if (!String.IsNullOrEmpty(type) 
&& type.ToLower().EndsWith("dispensers") 
&& material.HasValue()) { 
result = $"{material.Values.Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} construction withstands everyday use in a wide variety of settings"; 
} 

if(!String.IsNullOrEmpty(result)) { 
Add($"DispenserMaterial⸮{result}"); 
} 
} 

// --[FEATURE #16] 
// --Additional Automatic Manual Dispensers 
void AutomaticManualDispensers() { 
var result = ""; 
var type = R("SP-18148").HasValue() ? R("SP-18148").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18148").HasValue() ? R("cnet_common_SP-18148").Replace("<NULL>", "").Text : ""; 

if (!String.IsNullOrEmpty(type)) { 
if (type.ToLower().Equals("automatic dispensers")) { 
result = "Touchless technology helps reduce mess and spread of germs"; 
} 
else if (type.ToLower().Equals("manual dispensers")) { 
result = "Features an improved design that eliminates soap waste"; 
} 
} 
if(!String.IsNullOrEmpty(result)) { 
Add($"AutomaticManualDispensers⸮{result}"); 
} 
} 

// --[FEATURE #17] 
// --Additional standards 

// --[FEATURE #18] 
// --Warranty Information 

//1683393110991141836 end of "Hand Soap & Dispensers" §§ 

//§§1221973163092 "Card Files, Cases & Holders" "Alex K." 

CardFileCaseAndHolderTypeAndUse(); 
// true color 
CardSizeAndCapacity(); 
// Overall Dimensions of Container (4) 
CardFilesCasesAndHoldersDimensions(); 
IndexTypes(); 
// Recicle content 
RoundShapeCorners(); 
// Round Shape Corners 
FlexibleDividers(); 
SecureClosure(); 
LabelHolder(); 
TransparentDesign(); 

// Bullet 1 
void CardFileCaseAndHolderTypeAndUse() { 
var result = ""; 
var type = R("SP-18557").HasValue() ? R("SP-18557").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18557").HasValue() ? R("cnet_common_SP-18557").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(type)) { 
if (type.ToLower().Equals("holders")) { 
result = "Keep your business cards clean and unbent in this stylish holder"; 
} 
else if (type.ToLower().Equals("pages")) { 
result = "Business/credit card holder pages"; 
} 
else if (type.ToLower().Equals("rotary files")) { 
result = "Classic style rotary files"; 
} 

else if (type.ToLower().Equals("cases")) { 
result = "Business card cases are practical and stylish"; 
} 
else if (type.ToLower().Equals("binders")) { 
result = "Binder keeps business cards organized"; 
} 
else { 
result = $"{type.ToLower().ToUpperFirstChar().Replace("boxes", "box").Replace("walls", "wall").Replace("Books", "Book")} keeps your cards close at hand"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"CardFileCaseAndHolderTypeAndUse⸮{result}"); 
} 
} 

// Bullet 2 True color & Card file/holder material 

// Bullet 3 Card size & Capacity 
void CardSizeAndCapacity() { 
var result = ""; 
var cardCapacity = R("SP-12659").HasValue() ? R("SP-12659").Replace("<NULL>", "").Text : 
R("cnet_common_SP-12659").HasValue() ? R("cnet_common_SP-12659").Replace("<NULL>", "").Text : ""; 
var cardSize = R("SP-12551").HasValue() ? R("SP-12551").Replace("<NULL>", "").Text : 
R("cnet_common_SP-12551").HasValue() ? R("cnet_common_SP-12551").Replace("<NULL>", "").Text : ""; 
var compartmentsQty = A[6546]; 

if (!String.IsNullOrEmpty(cardCapacity) && !String.IsNullOrEmpty(cardSize)) { 
result = $"Holds up to ##{cardCapacity} ##{cardSize} cards"; 
} 
else if (!String.IsNullOrEmpty(cardCapacity) && compartmentsQty.HasValue()) { 
result = $"{compartmentsQty.FirstValue()} compartments holding approximately ##{cardCapacity} cards"; 
} 
else if (!String.IsNullOrEmpty(cardCapacity)) { 
result = $"Holds approximately {cardCapacity} cards"; 
} 
else if (!String.IsNullOrEmpty(cardSize)) { 
result = $"Holds ##{cardSize} cards"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"CardSizeAndCapacity⸮{result}"); 
} 
} 

// Bullet 4 Overall Dimensions of Container (4)
void CardFilesCasesAndHoldersDimensions() {
    var dimensionsWeight_Width = REQ.GetVariable("SP-21044").HasValue() ? REQ.GetVariable("SP-21044") :
        R("SP-21044").HasValue() ? R("SP-21044") : R("cnet_common_SP-21044");
    var dimensionsWeight_Height = REQ.GetVariable("SP-20654").HasValue() ? REQ.GetVariable("SP-20654") :
        R("SP-20654").HasValue() ? R("SP-20654"): R("cnet_common_SP-20654");
    var dimensionsWeight_Depth = REQ.GetVariable("SP-20657").HasValue() ? REQ.GetVariable("SP-20657") :
        R("SP-20657").HasValue() ? R("SP-20657") : R("cnet_common_SP-20657");
    
    if(dimensionsWeight_Width.HasValue()
    && dimensionsWeight_Depth.HasValue()
    && dimensionsWeight_Height.HasValue()) {
        Add($@"CardFilesCasesAndHoldersDimensions⸮Overall dimensions: {dimensionsWeight_Height}""H x {dimensionsWeight_Width}""W x {dimensionsWeight_Depth}""D");
    }
    else if (dimensionsWeight_Width.HasValue()
    && dimensionsWeight_Height.HasValue()) {
        Add($@"CardFilesCasesAndHoldersDimensions⸮Overall dimensions: {dimensionsWeight_Width}""W x {dimensionsWeight_Height}""H");
    }
}

// Bullet 5 

void IndexTypes() { 
var result = ""; 
var tabs = A[5947]; 
if (tabs.HasValue()) { 
result = $"{tabs.Values.Select(o => o.Value()).FlattenWithAnd()} tab indexes make locating the right card quick and easy"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"IndexTypes⸮{result}"); 
} 
} 

//Pack size (6) 

// recicle content 

// Round Shape Corners 
void RoundShapeCorners() { 
var result = ""; 
var edges = A[5945]; 
if (Coalesce(edges).HasValue("round shape")) { 
result = "Round edges offer a smooth, clean appearance"; 
} 
else if (Coalesce(edges).HasValue("%corners%")) { 
result = $"{edges.Where("%corners%").Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar().ToLower().ToUpperFirstChar()} offer a smooth, clean appearance"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"RoundShapeCorners⸮{result}"); 
} 
} 

// Flexible Dividers 

void FlexibleDividers() { 
var result = ""; 
var features = A[5945]; 
if (Coalesce(features).HasValue("flexible dividers")) { 
result = "Includes flexible dividers for easy organization"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"FlexibleDividers⸮{result}"); 
} 
} 

// Secure Closure 
void SecureClosure() { 
var result = ""; 
var closures = A[5939]; 
if (Coalesce(closures).HasValue("%closure%")) { 
result = $"{closures.Where("%closure%").Flatten(", ")} keeps cards secure"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"SecureClosure⸮{result}"); 
} 
} 

// Label Holder 
void LabelHolder() { 
var result = ""; 
var features = A[5945]; 
if (Coalesce(features).HasValue("%label holder%")) { 
result = $"Features {features.Where("%label holder%").First().Value()} that offers easy filing/retrieval"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"LabelHolder⸮{result}"); 
} 
} 

// Label Holder 
void TransparentDesign() { 
var result = ""; 
var color1 = R("SP-22967").HasValue() ? R("SP-22967").Replace("<NULL>", "").Text : 
R("cnet_common_SP-22967").HasValue() ? R("cnet_common_SP-22967").Replace("<NULL>", "").Text : ""; 
var color2 = R("SP-17441").HasValue() ? R("SP-17441").Replace("<NULL>", "").Text : 
R("cnet_common_SP-17441").HasValue() ? R("cnet_common_SP-17441").Replace("<NULL>", "").Text : ""; 
if (!String.IsNullOrEmpty(color1) && color1.ToLower().Contains("transparent")) { 
result = $"Designed to offer a clear view of all cards"; 
} 
else if (!String.IsNullOrEmpty(color2) && color2.ToLower().Contains("transparent")) { 
result = $"Designed to offer a clear view of all cards"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"TransparentDesign⸮{result}"); 
} 
} 

// 1221973163092 end of "Card Files, Cases & Holders" "Alex K." §§ 

//§§168328278181141928 "Trash Cans & Waste Receptacles" "Alex K." 

TypeOfTrashCanAndUse(); 
TrashCanCapacity(); 
//Color & Material of item 
//Dimensions 
LidType(); 
RimBorderDetails(); 
HandleDetails(); 
SafetyFeatures(); 
// Standards 
SlideLockSecurity(); 
HandsFreeOperation(); 
TrashCansPedal(); 
FingerPrintResistantCoating(); 
DentProof(); 
TrashCanBase(); 
StayOpenFunction(); 
LinerPocket(); 
RemovableInnerBucket(); 
ShoxSilentLid(); 
VentingChannels(); 
InternalHinge(); 
TrashCanWheels(); 
TrashCanPowered(); 
CarbonFilterGate();

// --[FEATURE #1] 
// --Type of trash can & use 
void TypeOfTrashCanAndUse() { 
var result = ""; 
//Step Trash Cans|Swing Lid Trash Cans|Ash Urns|Lids|Sensor Trash Cans|Trash Cans w/Lid|Trash Cans w/ no Lid|Pop-Up Bin 
var type = R("SP-18159").HasValue() ? R("SP-18159").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18159").HasValue() ? R("cnet_common_SP-18159").Replace("<NULL>", "").Text : ""; 
//Square|Pentagon|Round|Rectangular|Hexagon|Octagon|Semi-Round 
var receptacleShape = R("SP-24133").HasValue() ? R("SP-24133").Replace("<NULL>", "").Text : 
R("cnet_common_SP-24133").HasValue() ? R("cnet_common_SP-24133").Replace("<NULL>", "").Text : ""; 
var cigaretteReceptacle = A[6599]; 

if (!String.IsNullOrEmpty(type)) { 
if (type.ToLower().Equals("trash cans w/ no lid")) { 
if (!String.IsNullOrEmpty(receptacleShape)) { 
result = $"{receptacleShape.ToLower().ToUpperFirstChar()} trash can without lid ensures clean and litter-free space"; 
} else { 
result = "Trash can without lid ensures clean and litter-free space"; 
} 
} 
else if (type.ToLower().Equals("step trash cans")) { 
if (!String.IsNullOrEmpty(receptacleShape)) { 
result = $"{receptacleShape.ToLower().ToUpperFirstChar()} step trash can ensures clean and litter-free space and provides hands-free operation"; 
} else { 
result = "Step trash can ensures clean and litter-free space and provides hands-free operation"; 
} 
} 
else if (type.ToLower().Equals("swing lid trash cans")) { 
if (!String.IsNullOrEmpty(receptacleShape)) { 
result = $"{receptacleShape.ToLower().ToUpperFirstChar()} swing lid trash can allows easy trash disposal and conceals odors within the basket"; 
} else { 
result = "Swing-lid trash can allows easy trash disposal and conceals odors within the basket"; 
} 
} 
else if (type.ToLower().Equals("ash urns") && cigaretteReceptacle.HasValue("cigarette receptacle")) { 
if (!String.IsNullOrEmpty(receptacleShape)) { 
result = $"{receptacleShape.ToLower().ToUpperFirstChar()} cigarette receptacle extinguishes cigarettes without sand or water"; 
} else { 
result = "Cigarette receptacle extinguishes cigarettes without sand or water"; 
} 
} 
else if (type.ToLower().Equals("ash urns")) { 
if (!String.IsNullOrEmpty(receptacleShape)) { 
result = $"{receptacleShape.ToLower().ToUpperFirstChar()} ash urn fits perfectly anywhere you want to keep free of cigarette litter"; 
} else { 
result = "Ash urn fits perfectly anywhere you want to keep free of cigarette litter"; 
} 
} 
else if (type.ToLower().Equals("lids")) { 
if (!String.IsNullOrEmpty(receptacleShape)) { 
result = $"{receptacleShape.ToLower().ToUpperFirstChar()} lid keeps your waste out of sight and minimizes odors"; 
} else { 
result = "Lid keeps your waste out of sight and minimizes odors"; 
} 
} 
else if (type.ToLower().Equals("sensor trash cans")) { 
if (!String.IsNullOrEmpty(receptacleShape)) { 
result = $"{receptacleShape.ToLower().ToUpperFirstChar()} sensor trash can provides hands-free lid operation for a more hygienic environment"; 
} else { 
result = "Sensor trash can provides hands-free lid operation for a more hygienic environment"; 
} 
} 
else if (type.ToLower().Equals("trash cans w/lid")) { 
if (!String.IsNullOrEmpty(receptacleShape)) { 
result = $"{receptacleShape.ToLower().ToUpperFirstChar()} trash can with lid lets you dispose of waste items efficiently"; 
} else { 
result = "Trash can with lid lets you dispose of waste items efficiently"; 
} 
} 
else if (type.ToLower().Equals("pop-up bin")) { 
if (!String.IsNullOrEmpty(receptacleShape)) { 
result = $"{receptacleShape.ToLower().ToUpperFirstChar()} pop-up bin is exactly what you need for portability and utility"; 
} else { 
result = "Pop-up bin is exactly what you need for portability and utility"; 
} 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"TypeOfTrashCanAndUse⸮{result}"); 
} 
} 

// --[FEATURE #2] 
// --Trash can & recycling bin capacity (Gallons) 
void TrashCanCapacity() { 
var result = ""; 
//Step Trash Cans|Swing Lid Trash Cans|Ash Urns|Lids|Sensor Trash Cans|Trash Cans w/Lid|Trash Cans w/ no Lid|Pop-Up Bin 
var capacity = R("SP-18118").HasValue() ? R("SP-18118").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18118").HasValue() ? R("cnet_common_SP-18118").Replace("<NULL>", "").Text : ""; 

if (!String.IsNullOrEmpty(capacity)) { 
if (Coalesce(capacity).ExtractNumbers().Any()) { 
if(Coalesce(capacity).ExtractNumbers().First() < 15) { 
result = $"Storage capacity of {capacity} gal. to easily accommodate all your waste"; 
} 
else { 
result = $"{capacity} gal. capacity for large amounts of trash"; 
} 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"TrashCanCapacity⸮{result}"); 
} 
} 

// --[FEATURE #3] 
// --Color & Material of item 

// --[FEATURE #4] 
// --Dimensions (in Inches): Height x Width x Depth 

// --[FEATURE #5] 
// --Lid type; including locking/fastening system 
void LidType() { 
var result = ""; 
//No Lid|Removable|Hinged|Automatic|Push-door|Flip-Top|Locking 
var binLidType = R("SP-21195").HasValue() ? R("SP-21195").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21195").HasValue() ? R("cnet_common_SP-21195").Replace("<NULL>", "").Text : ""; 
var footPedal = A[6603]; 
var features = Coalesce(A[6609]); 
var closureType = A[6601]; 

if (!String.IsNullOrEmpty(binLidType)) { 
if (binLidType.ToLower().Equals("flip-top")) { 
result = "Flip lid opens easily and gently swings back into place"; 
} 
else if (binLidType.ToLower().Equals("push-door")) { 
result = "Push-door lid ensures zero spillage"; 
} 
else if (binLidType.ToLower().Equals("automatic")) { 
result = "Motion-activated lid opens automatically with just the wave of your hand"; 
} 
else if (binLidType.ToLower().Equals("hinged")) { 
if (footPedal.HasValue()) { 
result = "Hinged lid opens via a step-on foot pedal"; 
} else { 
result = "Hinged lid ensures zero spillage"; 
} 
} 
else if (binLidType.ToLower().Equals("removable") 
|| features.HasValue("% lid","% lid %","lid %") 
|| closureType.HasValue()) { 
result = "Lid ensures zero spillage"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"LidType⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// --Rim/Border Details 
void RimBorderDetails() { 
var result = ""; 
var features = A[6609]; 
if (features.HasValue("rolled rims")) { 
result = "Rolled rims add durability and are a breeze to clean"; 
} 
else if (Coalesce(features).HasValue("% rim", "% rims")) { 
result = $"{features.Where("% rim", "% rims").Select(o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} for added strength"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"RimBorderDetails⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// --Handle details; style & material (If applicable) 
void HandleDetails() { 
var result = ""; 
var features = A[6609]; 
var gripHandle = DC.KSP.GetString().ToLower(); 
var handle = R("SP-299").HasValue() ? R("SP-299").Replace("<NULL>", "").Text : 
R("cnet_common_SP-299").HasValue() ? R("cnet_common_SP-299").Replace("<NULL>", "").Text : ""; 

if (gripHandle.In("%side grip handle%") && gripHandle.In("%bottom handle%")) { 
result = "Bottom side handling grips for easy lifting, tilting, or pouring"; 
} 
else if (features.HasValue("rounded handles")) { 
result = "Rounded handles reduce strain on hands while making lifting easier"; 
} 
else if (features.HasValue() && Coalesce(features).HasValue("handling grips", "%handles", "%handle")) { 
result = $"{features.Where("handling grips", "%handles", "%handle").Select(o => o.Value()).FlattenWithAnd().ToLower().ToUpperFirstChar()} for easy lifting and movement"; 
} 
else if (!String.IsNullOrEmpty(handle)) { 
result = "Handles for easy lifting"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"HandleDetails⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// --Safety features (If applicable) 
void SafetyFeatures() { 
var result = ""; 
var features = A[6609]; 
var fireResistant = A[6817]; 
Add(features.Values); 
var test = features.WhereNot("stackable").Where(o => Coalesce(o.Value()).In("ergonomic grip", "curved rim")).Select(o => o.Value().Replace(" rim", "")).Flatten(", "); 
Add(test); 
if ((Coalesce(features).HasValue("%fire-resistant%") || fireResistant.HasValue()) 
&& Coalesce(features).WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Where(o => Coalesce(o.Value()).In("%resist%","%proof%")).Count() > 1) { 
var tmp = Coalesce(features).WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Where(o => Coalesce(o.Value()).In("%resist%","%proof%")).Select(o => o.Value().Replace("-resistant", "").Replace("proof", "")).FlattenWithAnd(); 
result = $"Resistant to fire, {tmp} to provide you with years of service"; 
} 
else if ((Coalesce(features).HasValue("%fire-resistant%") || fireResistant.HasValue()) 
&& Coalesce(features).WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Where(o => Coalesce(o.Value()).In("%resist%","%proof%")).Count() == 1) { 
var tmp = Coalesce(features).WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Where(o => Coalesce(o.Value()).In("%resist%","%proof%")).Select(o => o.Value().Replace("-resistant", "").Replace("proof", "")).FlattenWithAnd(); 
result = $"Resistant to fire and {tmp} to provide you with years of service"; 
} 
else if (Coalesce(features).HasValue("%fire-resistant%") || fireResistant.HasValue()) { 
result = "Firesafe construction will not burn, melt, or emit toxic fumes"; 
} 
else if (Coalesce(features).WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Where(o => Coalesce(o.Value()).In("%resist%","%proof%")).Count() > 1) { 
var tmp = Coalesce(features).WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Where(o => Coalesce(o.Value()).In("%resist%","%proof%")).Select(o => o.Value().Replace("-resistant", "").Replace("proof", "")).FlattenWithAnd(); 
result = $"Resistant to {tmp} to provide you with years of service"; 
} 
else if (Coalesce(features).HasValue("%weather-resistant%")) { 
result = "Weather-resistant construction for year-round use"; 
} 
else if (Coalesce(features).HasValue("%UV-resistant%")) { 
result = "UV-resistant to limit fading and provide you with years of service"; 
} 
else if (Coalesce(features).WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Where(o => Coalesce(o.Value()).In("%resist%","%proof%")).Count() > 0) { 
var tmp = Coalesce(features).WhereNot("% lid %", "% lid","%fingerprint%","%fire%").Where(o => Coalesce(o.Value()).In("%resist%","%proof%")).Select(o => o.Value().Replace("-resistant", "").Replace("proof", "")).FlattenWithAnd(); 
result = $"Resistant to {tmp} to provide you with years of service"; 
} 

if (!String.IsNullOrEmpty(result)) { 
Add($"SafetyFeatures⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// --Certifications & Standards (If applicable) 

// --[FEATURE #10] 
// --Additional Slide Lock Security 
void SlideLockSecurity() { 
var result = ""; 
var features = A[6609]; 
var lidLock = R("SP-21194").HasValue() ? R("SP-21194").Replace("<NULL>", "").Text : 
R("cnet_common_SP-21194").HasValue() ? R("cnet_common_SP-21194").Replace("<NULL>", "").Text : ""; 

if (features.HasValue("slide lock")) { 
result = "A slide lock securely locks the lid to help keep pets and curious children from getting into the trash"; 
} 
else if (!String.IsNullOrEmpty(lidLock) && lidLock.ToLower().Equals("yes")) { 
result = "Locks for secure use"; 
} 
else if (Coalesce(features).HasValue("screw closure", "tie-down rings")) { 
result = $"{features.Where("screw closure", "tie-down rings").Select(o=>o.Value()).FlattenWithAnd().ToUpperFirstChar()} help make security simple"; 
} 

if (!String.IsNullOrEmpty(result)) { 
Add($"SlideLockSecurity⸮{result}"); 
} 
} 

// --[FEATURE #11] 
// --Additional 
void HandsFreeOperation() { 
var result = ""; 
var features = A[6609]; 
var type = R("SP-18159").HasValue() ? R("SP-18159").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18159").HasValue() ? R("cnet_common_SP-18159").Replace("<NULL>", "").Text : ""; 

if (!String.IsNullOrEmpty(type)) { 
if (features.HasValue("odorless") && type.ToLower().Equals("sensor trash cans")) { 

result = "100% touch-free, odor-free and eliminates cross-contamination of germs"; 
} 
else if (type.ToLower().Equals("step trash cans") || type.ToLower().Equals("sensor trash cans")) { 
result = "Touch-free feature prevents the cross-contamination of germs"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"HandsFreeOperation⸮{result}"); 
} 
} 
// --[FEATURE #12] 
// --Additional Trash Cans Pedal 
void TrashCansPedal() { 
var result = ""; 
var features = A[6609]; 
if (features.HasValue("steel pedal")) { 
result = "Strong steel pedal is engineered for a smooth and easy step"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"TrashCansPedal⸮{result}"); 
} 
} 

// --[FEATURE #13] 
// --Additional Finger-print Resistant Coating 
void FingerPrintResistantCoating() { 
var result = ""; 
var features = A[6609]; 

if (features.HasValue("fingerprint-resistant coating")) { 
result = "Fingerprint-resistant coating is easy to clean"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"FingerPrintResistantCoating⸮{result}"); 
} 
} 

// --[FEATURE #14] 
// --Additional fingerprint-resistant coating dent-proof plastic lid
void DentProof() { 
var result = ""; 
var features = A[6609]; 

if (features.HasValue("dent-proof plastic lid")) { 
result = "Dent-proof plastic lid won't show dirt or fingerprints"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"DentProof⸮{result}"); 
} 
} 

// --[FEATURE #15] 
// --Additional 

void TrashCanBase() { 
var result = ""; 
var features = A[6609]; 

if (features.HasValue("anti-slip base")) { 
result = "Non-skid base to prevent the bin from sliding"; 
} 
else if (features.HasValue("solid base")) { 
result = "Solid base keeps potential liquid spills contained"; 
} 
else if (features.HasValue("reinforced base")) { 
result = "Reinforced base to reduce wear and tear from dragging"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"TrashCanBase⸮{result}"); 
} 
} 

// --[FEATURE #16] 
// --Additional Stay Open Function 

void StayOpenFunction() { 
var result = ""; 
var features = A[6609]; 

if (features.HasValue("stay-open function")) { 
result = "The lid stays open for as long as you like — perfect for longer chores"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"StayOpenFunction⸮{result}"); 
} 
} 

// --[FEATURE #17] 
// --Additional Liner Pocket 
void LinerPocket() { 
var result = ""; 
var features = A[6609]; 

if (features.HasValue("liner pocket")) { 
result = "Liner pocket keeps liners where you need them and dispenses them one by one from inside the can for a faster liner change"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"LinerPocket⸮{result}"); 
} 
} 

// --[FEATURE #18] 
// --Additional Removable Inner Bucket 
void RemovableInnerBucket() { 
var result = ""; 
var features = A[6609]; 
if (features.HasValue("removable inner bucket")) { 
result = "Inner trash bucket is fully removable for easy emptying and cleaning"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"RemovableInnerBucket⸮{result}"); 
} 
} 

// --[FEATURE #19] 
// --Additional lid shox technology/silent lid 
void ShoxSilentLid () { 
var result = ""; 
var features = A[6609]; 
if (features.HasValue("lid shox technology")) { 
result = "Lid shox technology controls the motion of the lid for a slow, silent close"; 
} 
else if (Coalesce(features).HasValue("silent%lid")) { 
result = "Lid closes slowly with whisper quiet operation"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"ShoxSilentLid⸮{result}"); 
} 
} 

// --[FEATURE #20] 
// --Additional venting channels 
void VentingChannels () { 
var result = ""; 
var features = A[6609]; 
if (features.HasValue("venting channels")) { 
result = "Venting channels reduce the force needed to lift each trash bin"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"VentingChannels⸮{result}"); 
} 
} 

// --[FEATURE #21] 
// --Additional internal hinge 
void InternalHinge () { 
var result = ""; 
var features = A[6609]; 
if (features.HasValue("internal hinge")) { 
result = "Internal hinge prevents the lid from bumping the wall"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"InternalHinge⸮{result}"); 
} 
} 

// --[FEATURE #22] 
// --Additional wheels 
void TrashCanWheels () { 
var result = ""; 
var features = A[6604]; 
if (features.HasValue("wheels")) { 
result = "Wheels make the can easy to move"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"TrashCanWheels⸮{result}"); 
} 
} 

// --[FEATURE #23] 
// --Additional powered 
void TrashCanPowered () { 
    if (A[335].HasValue() && A[861].HasValue()) { 
        Add($"TrashCanPowered⸮Powered by {A[861].FirstValue()} {A[335].FirstValue()} batteries"); 
    } 
}

// --[FEATURE #24] 
// --Additional Carbon Filter Gate
void CarbonFilterGate () { 
    if (A[6609].HasValue("Carbon filter gate%")) { 
        Add($"CarbonFilterGate⸮Equipped with Carbon Filter Gate (CFG) to neutralize the toughest odors"); 
    } 
} 

//168328278181141928 end of "Trash Cans & Waste Receptacles" §§ 

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
else if (backpackType.ToLower().Equals("laptop backpack")) { 
result = "Laptop backpack makes travel with electronics simple"; 
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
        result = $"Features {num.Values.Select(s => s.Value()).FlattenWithAnd()} compartments to keep you organized"; 
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
result = $"Weight capacity of {Math.Round(CarryingCase_Capacity.FirstValue() * 2.2, 2)} lbs."; 
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
    if (A[860].HasValue("shock absorbing shoulder straps")) {
        Add("ShockAbsorbingShoulderStraps⸮Shock-absorbing shoulder straps for added comfort");
    }
}

// --[FEATURE #10]
// --additional air-flow back padding

void AirFlowBackPadding() {
    if (A[860].HasValue("air-flow back padding")) {
        Add("AirFlowBackPadding⸮Stay cool with the air-flow back padding");
    }
}

// --[FEATURE #11]
// --additional Quik Pocket
void QuikPocket() {
    if (A[860].HasValue("Quik Pocket")) {
        Add("QuikPocket⸮Quik pocket provides easy access to items you need most frequently");
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

//§§ 1221973140991 "Desktop Organizers" "Alex K." 

TypeOfDesktopOrganizerAndItsUse(); 
// true color material 
// Dimensions 
FeatureBenefitCompartments(); 
FeatureBenefitNonSlip(); 
OrganizerRemovableDivider(); 
OrganizersStackable(); 
PaperHoldersInfo(); 
OrganizerWallMount(); 
OrganizerPhoneHolder(); 

// --[FEATURE #1] 
// --Type of Desktop Organizer and its use 

void TypeOfDesktopOrganizerAndItsUse() { 
var result = ""; 
//Card Holders|Pencil Holders|Rotating Organizers|File Organizers|Storage Drawers|Copy Holders|Compartment Storage|Racks|Cabinets|Pen Cups|Accessory Trays|Mobile Device Stand/Holders|Pad Holders|Accessory Holders|Dispensers|Sets|Storage/Document Boxes|Table Tops|Stacking Supports|Letter Holder 
var desktopOrganizerType = R("SP-18586").HasValue() ? R("SP-18586").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18586").HasValue() ? R("cnet_common_SP-18586").Replace("<NULL>", "").Text : ""; 
var votexType = A[6284]; 
if (!String.IsNullOrEmpty(desktopOrganizerType)) { 
if (desktopOrganizerType.ToLower().Equals("accessory trays")) { 
result = "Provides the extra space we all crave"; 
} 
else if (desktopOrganizerType.ToLower().Equals("mobile device stand/holders")) { 
result = "The smart phone holder provides a removable ergonomic system to hold most mobile devices"; 
} 
else if (desktopOrganizerType.ToLower().Equals("pad holders")) { 
result = "Pad holders are useful desk accessory"; 
} 
else if (desktopOrganizerType.ToLower().Equals("accessory holders")) { 
result = "Accessory holders are useful desk accessory"; 
} 
else if (desktopOrganizerType.ToLower().Equals("dispensers")) { 
result = "Dispensers are useful desk accessory"; 
} 
else if (desktopOrganizerType.ToLower().Equals("storage/document boxes")) { 
result = "Document box keeps important documents and file folders neatly tucked away"; 
} 
else if (desktopOrganizerType.ToLower().Equals("table tops")) { 
result = "Table tops are useful desk accessory"; 
} 
else if (desktopOrganizerType.ToLower().Equals("stacking supports")) { 
result = "Stacking supports for trays provide solid support for stacking"; 
} 
else if (desktopOrganizerType.ToLower().Equals("letter holder")) { 
result = "Letter holder helps organize cluttered desks"; 
} 
else if (desktopOrganizerType.ToLower().Equals("pen cups")) { 
result = "Pen cup keeps pens and pencils organized"; 
} 
else if (desktopOrganizerType.ToLower().Equals("cabinets")) { 
result = "Cabinets are useful desk accessory"; 
} 
else if (desktopOrganizerType.ToLower().Equals("desk pad")) { 
result = "Desk pad is an excellent desk protector"; 
} 
else if (desktopOrganizerType.ToLower().Equals("racks")) { 
result = "Keep messages and other materials organized with this message rack"; 
} 
else if (desktopOrganizerType.ToLower().Equals("compartment storage")) { 
result = "Compartment storage is useful desk accessory for organizing your office, craft, or school supplies"; 
} 
else if (desktopOrganizerType.ToLower().Equals("copy holders")) { 
result = "Copy holder makes reading at your desk an easier task"; 
} 
else if (desktopOrganizerType.ToLower().Equals("pencil holders")) { 
result = "Pencil holder provides plenty of room to store your writing instruments, rulers, and scissors"; 
} 
else if (desktopOrganizerType.ToLower().Equals("rotating organizers")) { 
result = "Rotating desk organizer offers easy access to office supplies"; 
} 
else if (desktopOrganizerType.ToLower().Equals("file organizers")) { 
result = "File organizer is great for keeping your desktop neat and tidy"; 
} 
else if (desktopOrganizerType.ToLower().Equals("Storage Drawers")) { 
result = "Storage Drawer keeps small supplies, writing instruments, and scissors organized"; 
} 
else if (desktopOrganizerType.ToLower().Equals("set")) { 
result = "This set is a useful desk accessory"; 
} 
else { 
result = $"{Coalesce(desktopOrganizerType.ToLower().ToUpperFirstChar()).Pluralize()} are useful desk accessory"; 
} 
} 
else if (votexType.HasValue("paper clip dispenser")) { 
result = "Paper clip dispenser reduces clutter and adds convenience to your workspace"; 
} 

if (!String.IsNullOrEmpty(result)) { 
Add($"TypeOfDesktopOrganizerAndItsUse⸮{result}"); 
} 
} 
// --[FEATURE #2] 
// --True color and Desktop Organizer Material 

// --[FEATURE #3] 
// --Dimensions in Inches: Height x Width x Depth 

// --[FEATURE #4] 
// --Feature/Benefit (Capacity as in Letter Size, 7-compartment tray, etc.) Use multiple bullets as necessary 

void FeatureBenefitCompartments() { 
var result = ""; 
var compartments = Coalesce(A[6289], A[6546]); 
if (compartments.HasValue() && compartments.FirstValue().ExtractNumbers().First() > 1) { 
result = $"Desk organizer provides {compartments.FirstValue().ExtractNumbers().First()} compartments, which can easily store all your supplies"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"FeatureBenefitCompartments⸮{result}"); 
} 
} 

void FeatureBenefitNonSlip() { 
var result = ""; 
var nonSlipVortex = Coalesce(A[6767], A[6294].Where("%non-slip%"), A[5945].Where("%non-slip%")); 

if (nonSlipVortex.HasValue()) { 
result = $"Non-slip rubber feet reduce movement and protect your work surface from scratches and scuffs"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"FeatureBenefitNonSlip⸮{result}"); 
} 
} 

// --[FEATURE #5] 
// -- Additional removable divider 
void OrganizerRemovableDivider() { 
var result = ""; 
var nonSlipVortex = A[6294]; 

if (Coalesce(nonSlipVortex).HasValue("%removable divider%")) { 
result = "Removable divider panel for in-drawer organization"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"OrganizerRemovableDivider⸮{result}"); 
} 
} 

// --[FEATURE #6] 
// -- Additional stackable 

void OrganizersStackable() { 
var result = ""; 
var desktopOrganizerType = R("SP-18586").HasValue() ? R("SP-18586").Replace("<NULL>", "").Text : 
R("cnet_common_SP-18586").HasValue() ? R("cnet_common_SP-18586").Replace("<NULL>", "").Text : ""; 
var stackable = A[6294]; 

if (!String.IsNullOrEmpty(desktopOrganizerType) && Coalesce(stackable).HasValue("%stackable%")) { 
result = $"{Coalesce(desktopOrganizerType.ToLower().ToUpperFirstChar()).Pluralize()} are stackable for customized storage"; 
} 

if (!String.IsNullOrEmpty(result)) { 
Add($"OrganizersStackable⸮{result}"); 
} 
} 

// --[FEATURE #7] 
// -- Additional 

void PaperHoldersInfo() { 
var result = ""; 
var paperHolder = Coalesce(A[181], A[5925]); 
if (paperHolder.HasValue("%letter%", "%legal%", "%A4%")) { 
if (paperHolder.Where("%letter%", "%legal%", "%A4%").Count() == 3) { 
result = "Holds A4, legal-, letter-size paper"; 
} 
else if (paperHolder.Where("%letter%", "%legal%").Count() == 2) { 
result = "Holds both legal and letter-size paper"; 
} 
else if (paperHolder.Where("%A4%", "%letter%").Count() == 2) { 
result = "Holds both A4 and letter-size paper"; 
} 
else if (paperHolder.Where("%A4%", "%legal%").Count() == 2) { 
result = "Holds both A4 and legal-size paper"; 
} 
else if (paperHolder.Where("%letter%").Count() == 2) { 
result = "Holds letter size-paper"; 
} 
else if (paperHolder.Where("%legal%").Count() == 2) { 
result = "Holds legal size-paper"; 
} 
else if (paperHolder.Where("%A4%").Count() == 2) { 
result = "Holds A4 size paper"; 
} 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"PaperHoldersInfo⸮{result}"); 
} 
} 

// --[FEATURE #8] 
// -- Additional wall mount 
void OrganizerWallMount() { 
var result = ""; 
var features = A[5945]; 
if (Coalesce(features).HasValue("%wall%mount%")) { 
result = "Unit can be wall-mounted to save space"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"OrganizerWallMount⸮{result}"); 
} 
} 

// --[FEATURE #9] 
// -- Additional phone holder 
void OrganizerPhoneHolder() { 
var result = ""; 
var features = A[6294]; 
if (Coalesce(features).HasValue("phone holder")) { 
result = "Angled platform holds phone at just the right degree for quick and easy dialing and picking up"; 
} 
if (!String.IsNullOrEmpty(result)) { 
Add($"OrganizerPhoneHolder⸮{result}"); 
} 
} 

// --[FEATURE #12] 
// --Warranty 

//1221973140991 end of "Desktop Organizers" §§ 


//548610392096142211 "USB Flash Drives" "Alex K." 

USBFlashDriveCapacityAndType(); 
USBDlashDriveDesignColorMaterial(); 
USBFlashDriveInterface(); 
MaximumReadAndWriteSpeed(); 
CompatibleWithOS(); 
FlashDrivesEncryptionAESSHA(); 
USBFlashDriveSecurityFeatures(); 
AdditionalKeyRingLoop(); 
AdditionalLEDAccessIndicator(); 
AdditionalOpertaingTemperature(); 
// Warranty 

// --[FEATURE #1] 
// --USB flash drive capacity & type 
void USBFlashDriveCapacityAndType() { 
    var capacity = REQ.GetVariable("SP-18334").HasValue() ? REQ.GetVariable("SP-18334") : R("SP-18334").HasValue() ? R("SP-18334") : R("cnet_common_SP-18334");
    //Flash Drive|Encrypted Secure|Wireless|Key Ring|Fashion|Rugged|Water Resistant|Micro|Compact|Portable|Nano|Other|High Speed|External|Basic|Mini 
    var flashDriveType = REQ.GetVariable("SP-14923").HasValue() ? REQ.GetVariable("SP-14923") : R("SP-14923").HasValue() ? R("SP-14923") : R("cnet_common_SP-14923");
    
    if (capacity.HasValue() && flashDriveType.HasValue()) { 
        if (flashDriveType.HasValue("flash drive") || flashDriveType.HasValue("Other")) { 
            if (Coalesce(capacity).In("2GB", "4GB", "8GB", "16GB")) { 
                Add($"USBFlashDriveCapacityAndType⸮{capacity} flash drive offers reliable data storage"); 
            } else { 
                Add($"USBFlashDriveCapacityAndType⸮{capacity} USB flash drive for convenient storage"); 
            } 
        } 
        else { 
            if (Coalesce(capacity).In("2GB", "4GB", "8GB", "16GB")) { 
                Add($"USBFlashDriveCapacityAndType⸮{capacity} {flashDriveType.ToLower(true)} flash drive offers reliable data storage"); 
            } 
            else { 
               Add($"USBFlashDriveCapacityAndType⸮{capacity} {flashDriveType.ToLower(true)} USB flash drive for convenient storage"); 
            } 
        } 
    } 
}

// --[FEATURE #2] 
// --USB flash drive design, True color& USB flash drive material 
void USBDlashDriveDesignColorMaterial() { 
    var design = REQ.GetVariable("SP-22140").HasValue() ? REQ.GetVariable("SP-22140") : R("SP-22140").HasValue() ? R("SP-22140") : R("cnet_common_SP-22140");
    var color = REQ.GetVariable("SP-22967").HasValue() ? REQ.GetVariable("SP-22967") : R("SP-22967").HasValue() ? R("SP-22967") : R("cnet_common_SP-22967");
    var material = REQ.GetVariable("SP-14924").HasValue() ? REQ.GetVariable("SP-14924") : R("SP-14924").HasValue() ? R("SP-14924") : R("cnet_common_SP-14924");
    if (design.HasValue() 
    && color.HasValue() 
    && material.HasValue()) { 
        if (!color.HasValue("assorted")) { 
            Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, comes in {color.ToLower()}, made of {material.ToLower(true)}"); 
        } else { 
            Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, comes in assorted colors, made of {material.ToLower(true)}"); 
        } 
    } 
    //No material 
    else if (design.HasValue() 
    && color.HasValue()) { 
        if (design.HasValue("swivel")) { 
            if (color.HasValue("assorted")) { 
                Add($"USBDlashDriveDesignColorMaterial⸮Capless design with a rotating case, comes in assorted colors"); 
            } else { 
                Add($"USBDlashDriveDesignColorMaterial⸮Capless design with a rotating case, comes in {color.ToLower()}"); 
            } 
        } else { 
            if (color.HasValue()) { 
                Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, comes in assorted colors"); 
            } else { 
                Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, comes in {color.ToLower()}"); 
            } 
        } 
    } 
    //no design 
    else if (color.HasValue() 
    && material.HasValue()) { 
        if (color.HasValue("assorted")) { 
            Add($"USBDlashDriveDesignColorMaterial⸮Comes in assorted colors, made of {material.ToLower(true)}"); 
        } else { 
            Add($"USBDlashDriveDesignColorMaterial⸮Comes in {color.ToLower()}, made of {material.ToLower(true)}"); 
        } 
    } 
    // no color 
    else if (design.HasValue() 
    && material.HasValue()) { 
        Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design, made of {material.ToLower(true)}");
    } 
    else if (design.HasValue()) { 
        Add($"USBDlashDriveDesignColorMaterial⸮{design.ToLower().ToUpperFirstChar()} design"); 
    } 
    else if (color.HasValue()) { 
        if (color.HasValue("assorted")) { 
            Add($"USBDlashDriveDesignColorMaterial⸮Comes in assorted colors"); 
        } else { 
            Add($"USBDlashDriveDesignColorMaterial⸮Comes in {color.ToLower()}"); 
        } 
    } 
    else if (material.HasValue()) { 
        Add($"USBDlashDriveDesignColorMaterial⸮Made of {material.ToLower(true)}"); 
    } 
}

// --[FEATURE #3] 
// --USB flash drive interface 
void USBFlashDriveInterface() { 
    //Type C|USB|USB 3.0|USB 2.0|USB 3.1 
    var interfaces = REQ.GetVariable("SP-18335").HasValue() ? REQ.GetVariable("SP-18335") : R("SP-18335").HasValue() ? R("SP-18335") : R("cnet_common_SP-18335");
    if (interfaces.HasValue("usb 2.0") || A[2448].HasValue("usb 2.0")) { 
        Add($"USBFlashDriveInterfaceUSB 2.0 interface is compatible with various USB-ready devices"); 
    }
    else if (interfaces.HasValue("usb 3.0") || A[2448].HasValue("usb 3.0")) { 
        Add($"USBFlashDriveInterfaceUSB 3.0 interface makes transporting and sharing files simple and convenient"); 
    } 
    else if (interfaces.HasValue("usb 3.1") || A[2448].HasValue("usb 3.1")) { 
        Add($"USBFlashDriveInterfaceUSB 3.1 interface enables blazing-fast data transferring ability"); 
    } 
    else if (interfaces.HasValue("usb c") || A[2448].HasValue("usb c")) { 
        Add($"USBFlashDriveInterfaceUSB Type-C flash drive provides a seamless way to move content between your Type-C devices"); 
    } 
    else if (interfaces.HasValue()) { 
        Add($"USBFlashDriveInterface{interfaces.ToUpperFirstChar()} interface makes transporting and sharing files simple and convenient"); 
    }
    else if (A[2448].HasValue()) {
        Add($"USBFlashDriveInterface{A[2448].FirstValue()} interface makes transporting and sharing files simple and convenient");
    }
} 


// --[FEATURE #4] 
// --Maximum read & write speed (Mb per seconds) 
void MaximumReadAndWriteSpeed() { 
    var maximumReadSpeed = REQ.GetVariable("SP-22260").HasValue() ? REQ.GetVariable("SP-22260") : R("SP-22260").HasValue() ? R("SP-22260") : R("cnet_common_SP-22260");
    var maximumWriteSpeed = REQ.GetVariable("SP-22261").HasValue() ? REQ.GetVariable("SP-22261") : R("SP-22261").HasValue() ? R("SP-22261") : R("cnet_common_SP-22261");

    if (maximumReadSpeed.HasValue() 
    && maximumWriteSpeed.HasValue() 
    && maximumReadSpeed.ExtractNumbers().Any()) { 
        if (maximumReadSpeed.ExtractNumbers().First() < 45) { 
            Add($"MaximumReadAndWriteSpeed⸮{maximumReadSpeed}MB/s read speed and {maximumWriteSpeed}MB/s write speed for quick data access and transfer"); 
        } else { 
            Add($"MaximumReadAndWriteSpeed⸮{maximumReadSpeed}MB/s read and {maximumWriteSpeed}MB/s write speeds provide swift performance"); 
        } 
    } 
    else if (maximumReadSpeed.HasValue()) { 
        if (maximumReadSpeed.ExtractNumbers().First() < 45) { 
            Add($"MaximumReadAndWriteSpeed⸮{maximumReadSpeed}MB/s read speed for quick data access and transfer"); 
        } else { 
            Add($"MaximumReadAndWriteSpeed⸮Up to {maximumReadSpeed}MB/s read speed provides swift performance"); 
        } 
    } 
    else if (maximumWriteSpeed.HasValue()) { 
        if (maximumWriteSpeed.ExtractNumbers().First() < 45) { 
            Add($"MaximumReadAndWriteSpeed⸮{maximumWriteSpeed}MB/s write speed for quick data access and transfer"); 
        } else { 
            Add($"MaximumReadAndWriteSpeed⸮Up to {maximumWriteSpeed}MB/s write speed provides swift performance"); 
        } 
    } 
}

// --[FEATURE #5] 
// --Compatible With 
void CompatibleWithOS() { 
    //var test = new List<string>(){"some pc", "some windows", "some2 android", "some2 android2"}; 
    var compWith = REQ.GetVariable("SP-382").HasValue() ? REQ.GetVariable("SP-382") : R("SP-382").HasValue() ? R("SP-382") : R("cnet_common_SP-382");
    if (A[429].HasValue()) { 
        var ckeckedList = new List<string>(){"Windows", "MacOS", "Linux", "Android", "Symbian"};
        var formatted = A[429].Values.Select(s => s.Value().ToString()); 
        foreach (var item in ckeckedList) { 
            formatted = formatted.Select(s => s.Contains(item) ? item : s).Distinct(); 
        }    
        Add($"CompatibleWithOS⸮Compatible with: {formatted.FlattenWithAnd()}"); 
    } 
    else if (A[603].HasValue()) { 
        Add($"CompatibleWithOS⸮Compatible with: {A[603].Values.Select(o => o.Value()).FlattenWithAnd().Replace("&", "and")}"); 
    } 
    else if (compWith.HasValue()) { 
        
        Add($"CompatibleWithOS⸮Compatible with: {compWith.ToString().Split(", ").Distinct().FlattenWithAnd().Replace("&", "and")}"); 
    } 
}

// --[FEATURE #6] 
// --Encryption 
void FlashDrivesEncryptionAESSHA(){ 
    if (A[1997].HasValue("%-bit AES%")) { 
       Add($"FlashDrivesEncryptionAESSHA⸮{A[1997].Where("%-bit AES%", "%SHA%").Select(o => o.Value()).FlattenWithAnd()} encryption protects your data from unauthorized access"); 
    } 
} 

// --[FEATURE #7] 
// --Security features 
void USBFlashDriveSecurityFeatures() { 
    var result = ""; 
    if (A[1997].HasValue("FIPS 140-2 Level 3")) { 
        result = result + "FIPS 140-2 Level ##3, "; 
    } 
    if (A[2909].HasValue("auto-locking")) { 
        result = result + "auto-lock, "; 
    } 
    if (A[344].HasValue("SanDisk SecureAccess")) { 
        result = result + "SanDisk SecureAccess software, "; 
    } 
    if (A[2909].HasValue("password protection")) { 
        result = result + "password security, "; 
    } 
    if (A[344].HasValue("MerlinSafe")) { 
        result = result + "MerlinSafe encryption software, "; 
    } 
    if (A[344].HasValue("EncryptStick Lite")) { 
        result = result + "EncryptStick software"; 
    } 
    if(!String.IsNullOrEmpty(result)) { 
        Add($"USBFlashDriveSecurityFeatures⸮Security features: {Coalesce(result.ToUpperFirstChar()).RegexReplace("(, $)", "")}"); 
    } 
} 

// --[FEATURE #8] 
// --Additional Key Loop 
void AdditionalKeyRingLoop() { 
    if (A[2909].HasValue("key ring loop")) { 
        Add("AdditionalKeyRingLoop⸮The included key loop easily attaches to key chains, so important files are never out of reach"); 
    } 
}  

// --[FEATURE #9] 
// --Additional LED light 

void AdditionalLEDAccessIndicator() { 
    if (A[2909].HasValue("LED access indicator")) { 
       Add("AdditionalLEDAccessIndicator⸮LED activity light that blinks during file transfer, so you know when the job is done"); 
    }
}

// --[FEATURE #10] 
// --Additional Opertaing temperature 
void AdditionalOpertaingTemperature() { 
    if (A[358].HasValue() && A[359].HasValue()) { 
        Add($"AdditionalOpertaingTemperature⸮Temperature: {A[358].FirstValueUsm()} to {A[359].FirstValueUsm()} degrees F (operating)");
    } 
} 

// --[FEATURE #12] 
// --Languages Supported Warranty 

//548610392096142211 end of "USB Flash Drives" §§ 


//§§542131121982140608  "Literature & Sign Holders BEGIN" "Kretinin N."  §§ 

LiteratureSignHolderTypeStyleUse();
LoadingStyleOrientation();
SignSize();
NumberOfPockets();
AdditionalRoundedCornersEdges();
AdditionalDoubleSided();
AdditionalInstallationHolders();
AdditionalAdhesive();
AdditionalHoles();
AdditionalSuctionCup();
AdditionalSlanted();
AdditionalBaseFeature();
AdditionalWeatherProof();

// --[FEATURE #1]  Literature/Sign holder type, style & use	
void LiteratureSignHolderTypeStyleUse(){ 
    var type = GetReference("SP-17357").ToLower();   
    var style = GetReference("SP-18514").ToLower();
    string result="LiteratureSignHolderTypeStyleUse⸮";
    if (!(String.IsNullOrEmpty(type)||String.IsNullOrEmpty(style))){
        if (type == "magazine") {
            result=result+
                "Displays the latest editions of your magazines and books in this magazine "+style;
        }
        else if (type=="document"&& new[] {"desktop holder", "floor holder", "wall holder"}.Any(c => style.Contains(c))){
            result=result+style.ToUpperFirstChar().Replace("holder", "")+"document holder to keep important documents organized and at your fingertips";
        }
        else if (type=="document"){
            result=result+"Document "+style+" to keep important documents organized and at your fingertips";
        }
        else if (new[] {"banner", "brochure", "catalog", "card", "poster", "ticket"}.Any(c => type.Contains(c))){
            result=result+style.ToUpperFirstChar()+" effectively displays marketing materials to clients and business associates";
        }
        else if (new[] {"desktop holder", "floor holder", "wall holder"}.Any(c => style.Contains(c))){
            result=result+style.ToUpperFirstChar().Replace("holder", "")+" "+type+" holder lets you conveniently display signage to ensure maximum visibility";
        }
    }
    else if (!String.IsNullOrEmpty(type)&&type!="non-specified") {
        result=result+type.ToUpperFirstChar()+" holder lets you conveniently display signage to ensure maximum visibility";
    }
    else if (!String.IsNullOrEmpty(style)) {
        result=result+style.ToUpperFirstChar()+" lets you conveniently display signage to ensure maximum visibility";
    }
    else {
        result="";
    }
    if (!String.IsNullOrEmpty(result)){
        Add(result);
    }
}
//[FEATURE #2] True color & holder material	[USED COMMON]

//[FEATURE #3] Loading style & orientation

void LoadingStyleOrientation(){
    var loadingStyle = GetReference("SP-21027").ToLower(); 
    var orientation = GetReference("SP-21028").ToLower();
    string result="LoadingStyleOrientation⸮";
    if (!String.IsNullOrEmpty(loadingStyle)&&!String.IsNullOrEmpty(orientation)){
        result=result+loadingStyle.ToUpperFirstChar()+" design with "+orientation+" orientation";
    }
    else if (!String.IsNullOrEmpty(loadingStyle)){
        result=result+loadingStyle.ToUpperFirstChar()+" design for easy insertion of printed material";
    }
    else if (orientation=="landscape/portrait") {
        result=result+"Can be displayed vertically or horizontally";
    }
    else if (!String.IsNullOrEmpty(orientation)){
        result=result+"Holds pages with "+orientation+" orientation";
    }
    else {
        result="";
    }
    if (!String.IsNullOrEmpty(result)){
        Add(result);
    }
}

//[FEATURE #4] Overal Literature or Sign Holder Dimension (in Inches): Height x Width x Depth [USED COMMON]

//[FEATURE #5] Sign size (Size of the sign or paper that goes into the Literature Holder)

void SignSize(){
    var orientation = GetReference("SP-21028").ToLower();
    var size = GetReference("SP-21296").ToLower();
    if (size=="Other"||String.IsNullOrEmpty(size)){
        size="";
        if (A[5925].HasValue("% x %mm%")){
            double w = A[5925].Where("% x %mm%").First().Value().Replace("A3","").Replace("A4","").Replace("A5","").ExtractNumbers().Last();
            w = w * 0.0393701;
            double h = A[5925].Where("% x %mm%").First().Value().Replace("A3","").Replace("A4","").Replace("A5","").ExtractNumbers().First();
            h = h * 0.0393701;
            size = w.ToString("F1").Replace(".0","")+"\""+" x "+h.ToString("F1").Replace(".0","")+"\"";
        }
    }
    if (!String.IsNullOrEmpty(size)){
        if (orientation!="landscape"){
            size = Coalesce(size).RegexReplace("(.+)( x )(.+)","$3$2$1");
        }
        size = size.Replace(" x ", "W x ")+"H";
        var result = "SignSize⸮Designed to hold display literature that measures "+size;
        Add(result);
    }
}

//[FEATURE #6] Number of pockets (If applicable)	

void NumberOfPockets (){
    var numberOfPocketsItem = GetReference("SP-14154").ToLower();
    var numberOfPockets = A[5934];
    if (numberOfPocketsItem!="none"&&numberOfPockets.HasValue()){
        var result = "NumberOfPockets⸮";
        if (numberOfPockets.FirstValueOrDefault() > 10){
            result = result+numberOfPockets.FirstValueOrDefault()+" pockets to place all the literature you wish to display";
        }
        else if (numberOfPockets.FirstValueOrDefault() > 5){
            result = result+numberOfPockets.FirstValueOrDefault()+" pockets for easy arrangement of different literature types";
        }
        else if (numberOfPockets.FirstValueOrDefault() > 1){
            result = result+"Features "+numberOfPockets.FirstValueOrDefault()+" pockets to organize and display your literature";
        }
        else {
            result = result+"Features "+numberOfPockets.FirstValueOrDefault()+" pocket to organize and display your literature";
        }
        Add(result);
    }
}

//[FEATURE #7] Pack Size (If more than 1) [USED COMMON]

//[FEATURE #8-...] ADDITIONAL

void AdditionalRoundedCornersEdges(){
    var features = A[6087];
    var result = "AdditionalRoundedCornersEdges⸮";
    if (features.HasValue("rounded edges")){
        result = result+"Rounded edges for a smooth, clean appearance";
    }
    else if (features.HasValue("rounded corners")){
        result = result+"Rounded corners for safety in handling";
    }
    else {
        result="";
    }
    if (!String.IsNullOrEmpty(result)){
        Add(result);
    }
}

void AdditionalDoubleSided(){
    if (A[6084].HasValue()){
        Add("AdditionalDoubleSided⸮Double-sided design lets you display and highlight information on both sides");
    }
}

void AdditionalInstallationHolders(){
    var installationType = A[6082];
    var result = "AdditionalInstallationHolders⸮";
    if (installationType.HasValue("%door%")&&installationType.HasValue("%wall%")){
        result=result+"Can be placed on door or mounted on a wall for easy viewing";
    }
    else if (installationType.WhereNot("hanging", "%wall%", "%pole%").Any()){
        string installationList="";
        foreach (var val in installationType.WhereNot("hanging", "%wall%")){
            if (val.Value().ToString().ToLower().Contains("surface")){
                installationList=installationList!=""?installationList+", any flat surface":"any flat surface";
            }
            else if (val.Value().ToString().ToLower().Contains("floor")){
                installationList=installationList!=""?installationList+", floors":"floors";
            }
            else if (val.Value().ToString().ToLower().Contains("window")){
                installationList=installationList!=""?installationList+", windows":"windows";
            }
            else {
                installationList=installationList!=""?installationList+", "+val.Value().Pluralize():val.Value().Pluralize();
            }
        };
        result=result+"Can be placed on "+installationList.Split(",").FlattenWithAnd(); 
        if (installationType.HasValue("%wall%")){
            result=result+" or mounted on a wall for easy viewing";
        }
        else {
            result=result+" for easy viewing";
        }
    }
    else if (installationType.HasValue("%wall%")){
        result=result+"Can be mounted on a wall for easy viewing";
    }
    else if (installationType.HasValue("%wall%")){
        result=result+"Can be mounted on a wall for easy viewing";
    }
    else if (installationType.HasValue("pole-mounted")){
        result=result+"Can be mounted on a pole for easy viewing";
    }
    else {
        result="";
    }
    if (!String.IsNullOrEmpty(result)){
        Add(result);
    }
}

void AdditionalAdhesive(){
    if (A[6108].HasValue("%adhesive tape%", "%adhesive strip%")){
        Add("AdditionalAdhesive⸮"+A[6108].Where("%adhesive tape%", "%adhesive strip%").FlattenWithAnd().ToLower(true).ToUpperFirstChar()+" on the back for hanging on most surfaces");
    }
}

void AdditionalHoles(){
    var features = A[6087];
    var result = "AdditionalHoles⸮";
    if (features.HasValue("%pre-drilled holes%", "%pre-punched holes%")){
        result=result+"Pre-drilled holes make wall mounting easy";
    }
    else if (features.HasValue("%hanging holes%")){
        result=result+features.Where("%hanging holes%").First().Value().ToString().ToUpperFirstChar()+" make wall mounting easy";
    }
    else {
        result="";
    }
    if (!String.IsNullOrEmpty(result)){
        Add(result);
    }
}

void AdditionalSuctionCup(){
    var result="AdditionalSuctionCup⸮";
    if (A[6108].HasValue("%suction cups%")){
        result=result+A[6108].Where("%suction cups%").First().Value().ToString().ToUpperFirstChar();
    }
    else if (A[6087].HasValue("%suction cups%")){
        result=result+A[6087].Where("%suction cups%").First().Value().ToString().ToUpperFirstChar();
    }
    else {
        result="";
    }
    if (result!=""){
        result=result+" offer long-lasting holding power";
    }
    Add(result);
}

void AdditionalSlanted() {
    if (A[6087].HasValue("%slanted%")){
        Add("AdditionalSlanted⸮Features a slanted face for easier readability");
    }
}

void AdditionalBaseFeature(){
    if (A[6087].HasValue("% base")){
        Add("AdditionalBaseFeature⸮"+A[6087].Where("% base").First().Value().ToString().ToLower().ToUpperFirstChar()+" gives great stability");
    }
}

void AdditionalWeatherProof(){
    if (A[6087].HasValue("%Weatherproof%")){
        Add("AdditionalWeatherproof⸮Weatherproof for effective usage");
    }
}


//§§542131121982140608  "Literature & Sign Holders END" "Kretinin N."  §§ 

//§1114953212724  "Pen Refills BEGIN" "Kretinin N."  §§ 

RefillTypeColorInk();
CompatiblePens();
AdditionalBullets5996();
AdditionalQuickDry();
AdditionalWritesOn();

// --[FEATURE #1] Refill type & Color ink
void RefillTypeColorInk(){
    var refillType = GetReference("SP-16157").ToLower();
    var color = GetReference("SP-18607").ToLower();
    var result = "RefillTypeColorInk⸮";

    if (refillType=="other"){
        refillType="";
        if (A[6012].HasValue()){
            refillType=A[6012].FirstValueOrDefault().ToLower().Replace(" pen", "");
        }
    }

    if (!String.IsNullOrEmpty(refillType)&&color=="assorted"){
        result=result+refillType.ToUpperFirstChar()+" pen refills, assorted color ink";
    }
    else if (!String.IsNullOrEmpty(refillType)&&color=="multi colors"){
        result=result+refillType.ToUpperFirstChar()+" pen refills, multi colors";
    }
    else if (!String.IsNullOrEmpty(refillType)&&!String.IsNullOrEmpty(color)){
        result=result+refillType.ToUpperFirstChar()+" pen refill, "+color+" color";
    }
    else if (!String.IsNullOrEmpty(refillType)){
        result=result+refillType.ToUpperFirstChar()+" pen refill";
    }
    else if (!String.IsNullOrEmpty(color)){
        result=result+color.ToUpperFirstChar()+" color";
    }
    else {
        result="";
    }
    if (!String.IsNullOrEmpty(result)){
        Add(result);
    }
}

//[FEATURE #2] Pen point size - used similar feature from Pens category

//[FEATURE #3] Pack Size (If more than 1) - used common one

//[FEATURE #4] Compatibility

void CompatiblePens(){
    var compatibilityInfo = SPEC["MS"].GetLine("Designed For").Body.ToString();
    if (!String.IsNullOrEmpty(compatibilityInfo)){
        compatibilityInfo=compatibilityInfo.Replace("; ", ", ");
        Add("CompatiblePens⸮Compatible with "+compatibilityInfo+" pens");
    }
}

//[FEATURE #5 -...] Use for additional product and/or manufacturer information relevant to the customer buying decision


void AdditionalWritesOn(){ 
    if (A[5987].HasValue()){
    Add("AdditionalWritesOn⸮Writes on "+A[5987].Values.Flatten(", "));
    }
}

void AdditionalBullets5996(){
    if (A[5996].HasValue()){
        var result = "AdditionalBullets⸮";
        foreach (var feature in A[5996].Values.Take(3)){
            if (feature.Value()!="non-waterproof"){
                if (result=="AdditionalBullets5996⸮"){
                    result=result+feature.Value().ToUpperFirstChar();
                }
                else {
                    result=result+"|"+feature.Value().ToUpperFirstChar();
                }
            }
        }
        Add(result);
    }
}

void AdditionalQuickDry(){
    if (A[6282].HasValue()){
        Add("AdditionalQuickDry⸮Quick-dry ink prevent smears");
    }
}

//§1114953212724  "Pen Refills END" "Kretinin N."  §§ 

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
    //Foil/Paper|Craft Kits/Sets|Craft Tools|Paint|Pencils|Fabric|Pens|Glitter/Powder|Crayons|Clay|Markers|Knives/Blades|Jewelry/Beads|Stamps|Craft Materials|Colored Pencils|Glues/Adhesives|Clothing|Button Complete Set
    var type = REQ.GetVariable("SP-17332").HasValue() ? REQ.GetVariable("SP-17332") : R("SP-17332").HasValue() ? R("SP-17332") : R("cnet_common_SP-17332");
    if (type.HasValue() && A[6173].HasValue()) {
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
            Add($"CraftSupplyTypeUse⸮Make crafting experiences more fun with these {A[6173].FirstValue().Pluralize()}");
        }
        else if (type.HasValue("Jewelry/Beads")) {
            Add($"CraftSupplyTypeUse⸮These {A[6173].FirstValue()} is great for arts ##and crafts projects");
        }
        else if (type.HasValue("Button Complete Set")) {
             Add($"CraftSupplyTypeUse⸮Button complete set is used to create buttons with your individual design");
        }
        else if (Coalesce(type).In("Pens", "%Pencils", "Markers", "Crayons")) {
            Add($"CraftSupplyTypeUse⸮{type} for everyday drawing tasks");
        } else {
             Add($"CraftSupplyTypeUse⸮{A[6173].FirstValue().ToLower().ToUpperFirstChar().Pluralize()} are perfect for arts and crafts projects");
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
        Add($"CraftSuppliesDesign⸮{A[7084].Values.Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} pattern design");
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


//§§530868304206054 "Docks & Mounts" "Alex K." "Serhii.O"

DocksMountsTypeUse();
DocksAndMountsCompatibility();
ConnectionOrHoldingDesign();
// Dimentions
DocksAndMountsPowerSource();
DocksAndMountsAdjustments();
DocksAndMountsDegreeViewingAngles();
DocksAndMountsScreenSizeCompatibility();
DocksAndMountsPackageContent();
// true color material
// Warranty


// --[FEATURE #1]
// --Type of Piece and UseType of Mount/Dock
void DocksMountsTypeUse(){
    //Sticky Mat/Pad|Mount|Dock
    var type = R("SP-18044").HasValue() ? R("SP-18044") : R("cnet_common_SP-18044");
    var miscellaneous_PlacingMounting = A[1031];
    if (type.HasValue("Mount") && A[7522].HasValue()) {
        Add($"DocksMountsTypeUse⸮This mount is a stable mounting option for your {A[7522].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
    else if (type.HasValue("Mount")) {
        Add($"DocksMountsTypeUse⸮Secure your devices with this mount");
    }
    else if (type.HasValue("Dock")) {
        Add($"DocksMountsTypeUse⸮This dock is the ideal solution when you want to charge your devices");
    }
    else if (type.HasValue("Sticky Mat/Pad")) {
        Add($"DocksMountsTypeUse⸮Secure your devices with this sticky mat/pad");
    }
}

// --[FEATURE #2]
// --Product Compatibility (if it exceeds bullet size list in Extended Description)
void DocksAndMountsCompatibility(){ 
    // Universal|Nokia|Motorola|LG|Amazon Fire|Samsung Galaxy S2|iPhone 6 Plus/6s Plus/7 Plus/8 Plus|iPhone 6/6S|Blackberry|Google Nexus|iPhone 7/8|Google Pixel 2|iPhone 6/6S/7/8|Samsung Galaxy Note 8|iPhone 5/5s/SE|iPhone 8 Plus|iPhone X|Samsung Galaxy Note 4|iPhone 6 Plus/6s Plus|Galaxy S8|ZTE|Mini Mobile|Most Smartphones|Samsung Galaxy Note 3|Samsung Galaxy Note 2|HTC M9|Samsung Galaxy S6|iPhone 6s Plus|6" Cell Phone|iPhone 6s|5" Cell Phone|iPhone 7 Plus /iPhone 8 Plus|iPhone 7/ iPhone 8|iPhone 3G/3GS|HTC Desire 526|HTC Desire 510|HTC 8Xt|Electrify/Mb855/Photon 4G|iPhone 5/5S/5C/6/6 Plus|HTC Evo 4G Lte|HTC Desire 626/626S|HTC Desire 626|HTC Desire 601/Zara|HTC One M9|iPhone/iPad/iPod Touch|HTC One M8|Utstarcom|Samsung Galaxy S 8+|Cisco|Samsung Galaxy S 8|Sanyo E4100Taho|Samsung Galaxy 8|HTC M7|Huawei|Coolpad|HTC Windows Phone 8X/Htc Zenith|HTC One/M7|PCD|iPhone 4/4S/5 & iPod Touch|iPhone Se|HTC One Vx|iPhone 7 Plus|iPhone XS|iPhone 8|iPhone XS Max|myTouch|HTC One X|Samsung Galaxy S9+|iPhone 6 Plus|Microsoft|Apple iPhone X, Xs|iPhone 7/6s/6|Samsung D710, R760, Galaxy S II 4G|Samsung D700|Microsoft Lumia 640|Microsoft Lumia|iPhone 5 & Newer|Samsung Galaxy S9|Samsung Galaxy Note|Pantech|Samsung (Other)|HP|BLU|iPhone 3G/3GS|Dell|Palm|Kyocera|Sony|Alcatel|Samsung Galaxy S5|Samsung Galaxy S4|Samsung Galaxy S3|iPhone 6|Samsung R360|iPhone 4/4s|CASIOC811|iPhone 7|Samsung Galaxy S6 Edge|Samsung M580|iPhone 5/5s|Alcatel 7024W|Samsung Intensity III|iPhone 5c|Samsung i9260|Alcatel 5020T|Samsung S390G/T189N|Samsung R830 Galaxy Axiom|HTC|Samsung R480|Samsung Galaxy S7|Samsung Galaxy S4 Mini|Samsung Galaxy Note 5|Google Pixel 2 XL|All iPhones|Multiple Brands 
    var CellPhoneCompatibilityRef = REQ.GetVariable("SP-18043").HasValue() ? REQ.GetVariable("SP-18043") : R("SP-18043").HasValue() ? R("SP-18043") : R("cnet_common_SP-18043");
 
    var CompatibleProducts = Coalesce(SPEC["MS"].GetLine("Compatible with").Body, SPEC["ES"].GetLine("Compatible with").Body, SPEC["MS"].GetLine("Designed For").Body, SPEC["ES"].GetLine("Designed For").Body); 
    
    if(CellPhoneCompatibilityRef.HasValue("iPhone/iPad/iPod Touch")){ 
        Add($"DocksAndMountsCompatibility⸮Compatible with iPod/iPhone/iPad"); 
    } 
    else if(CellPhoneCompatibilityRef.HasValue() && !CellPhoneCompatibilityRef.HasValue("Universal")){ 
        Add($"DocksAndMountsCompatibility⸮Compatible with {CellPhoneCompatibilityRef.Replace("All iPhones", "all iPhones", "Multiple Brands", "multiple brands")}"); 
    }
    else if(CompatibleProducts.HasValue()){ 
        Add($"DocksAndMountsCompatibility⸮{Shorten($"Charges {CompatibleProducts.Text.Replace(";", ",").Split(", ").Select(d => Coalesce(d).ExtractNumbers().Any() ? $"##{d}" : d).FlattenWithAnd().RegexReplace(@"(-in\w+)", @"""").RegexReplace(@"\(.*?\)", "").Replace("lightning", "Lightning")}", 250, ',')}"); 
    } 
}

// --[FEATURE #3]
// --Connection or Holding Design
void ConnectionOrHoldingDesign() {
    // Securely attaches to dash or window
    if (A[1031].HasValue()) {
        Add($"ConnectionOrHoldingDesign⸮Securely attaches to {A[1031].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
}

// --[FEATURE #4]
// --Dimensions as HxWxD

// --[FEATURE #5]
// --Power Source
void DocksAndMountsPowerSource() {
    // https://www.staples.com/Lenovo-Proprietary-Interface-Docking-Station-for-Notebook-Tablet-PC-230-W-40A50230US/product_IM11X6392
    // Power: Power adapter 230 Watt AC 120/230 V ( 50/60 Hz )
    var type = REQ.GetVariable("SP-98").HasValue() ? REQ.GetVariable("SP-98") : R("SP-98").HasValue() ? R("SP-98") : R("cnet_common_SP-98");
    if (type.HasValue()) {
        Add($"DocksAndMountsPowerSource⸮Power: {type.ToLower(true)}");
    }
}

// --[FEATURE #6]
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

// --[FEATURE #7]
// --Additional Degree
void DocksAndMountsDegreeViewingAngles() {
    //  360-degree free rotation for optimal viewing angles
    if (A[9726].HasValue()) {
        Add($"DocksAndMountsDegreeViewingAngles⸮{A[9726].FirstValue().Replace("°", "")}-degree free rotation for optimal viewing angles");
    }
}

// --[FEATURE #8]
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

// --[FEATURE #9] 
// --Additional Package Content
void DocksAndMountsPackageContent() {
    //  Includes AC power adapter, power cord and publications
    if (A[7521].HasValue()) {
        Add($"DocksAndMountsPackageContent⸮Includes {A[7521].Values.Select(o => o.Value()).FlattenWithAnd()}");
    }
}

// --[FEATURE #10]
// -- Additional true color material

// --[FEATURE #11]
// --Additional Warranty

//§§530868304206054 end of "Docks & Mounts"