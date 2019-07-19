//§§1683392810982141837  "Brooms & Dustpans" "Alex K."

BroomDustpanTypeAndUse();
BroomDustpanHandleInformation();
BroomDustpanHeadDetails();
BroomDustpanDimensions();
// PackSize 
AdditionalBroomDustpanMaterial();
// true color
AdditionalBroomHolder();
AdditionalBroomHangerHole();
AdditionalBroomWallMountable();
AdditionalBroomMoldedRiges();
AdditionalCleaningUsage();
AdditionalAdjustableHandle();
AdditionalTelescopingHandle();
AdditionalComfortGrip();
AdditionalColorCoded();
AdditionalBroomThread();
AdditionalBroomWetDry();
AdditionalBroomHeavyDuty();
AdditionalBroomBristleType();
AdditionalBroomBrushHolders();
AdditionalBroomWheels();
// warranty

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
void BroomDustpanHandleInformation(){
    var type = GetReferenceBase("SP-23349");
    var length = GetReferenceBase("SP-20400");
    var material = GetReferenceBase("SP-21026");
    if (type.HasValue("%handle%") && length.HasValue() && material.HasValue()){
        Add($"BroomDustpanHandleInformation⸮Handle features {length}\" length and made of {material.ToLower(true)}");
    }
    else if (type.HasValue("%handle%") && length.HasValue() && material.HasValue()) {
        Add($"BroomDustpanHandleInformation⸮Handle made of {material.ToLower(true)}");
    }
    else if (type.HasValue("%handle%") && length.HasValue()){
        Add($"BroomDustpanHandleInformation⸮Handle features {length}\" length");
    }
}
// --[FEATURE #3]
// -- Head Details (material and Size)
void BroomDustpanHeadDetails(){
    var type = GetReferenceBase("SP-23349");
    var height  = GetReferenceBase("SP-20654");
    var width = GetReferenceBase("SP-21044");
    var depth = GetReferenceBase("SP-20657");
    var material = GetReferenceBase("SP-21026");
    if (type.HasValue("%Head%") && height.HasValue() && width.HasValue() && depth.HasValue() && material.HasValue()){
        Add($"BroomDustpanHeadDetails⸮Head size is {height}\"H x {width}\"W x {depth}\"D and made from {material.ToLower(true)} material");
    }
    else if (type.HasValue("%Head%") && width.HasValue() && material.HasValue()) {
        Add($"BroomDustpanHeadDetails⸮Width head size is {width}\" and made of {material.ToLower(true)}");
    }
    else if (type.HasValue("%Head%") && height.HasValue() && width.HasValue() && depth.HasValue() ){
        Add($"BroomDustpanHeadDetails⸮Head size is {height}\"H x {width}\"W x {depth}\"D");
    }
    else if (type.HasValue("%Head%") && width.HasValue()){
        Add($"BroomDustpanHeadDetails⸮It measures {width}\" width");
    }
    else if (type.HasValue("%Head%") && material.HasValue()) {
        Add($"BroomDustpanHeadDetails⸮Head is made of {material.ToLower(true)}");
    }
}
// --[FEATURE #4]
// -- Dimensions (in Inches): L x W
void BroomDustpanDimensions(){
    var type = GetReferenceBase("SP-23349");
    var height  = GetReferenceBase("SP-20654");
    var width = GetReferenceBase("SP-21044");
    var depth = GetReferenceBase("SP-20400");
    var length = GetReferenceBase("SP-21026");
    if (!type.In("%Head%", "%handle%") && width.HasValue() && length.HasValue()) {
        Add($"BroomDustpanDimensions⸮Dimensions: {length}\"L x {width}\"W");
    }
    else if (!type.In("%Head%", "%handle%") && height.HasValue() && width.HasValue() && depth.HasValue()) {
        Add($"BroomDustpanDimensions⸮Dimensions: {height}\"H x {width}\"W x {depth}\"D");
    }
    else  if (!type.In("%Head%", "%handle%") && height.HasValue()) {
        Add($"BroomDustpanDimensions⸮Dimensions: It measures {length}\" length");
    }
}
// --[FEATURE #5]
// -- Pack Size (If more than 1)

// --[FEATURE #6]
// -- Additional material
void AdditionalBroomDustpanMaterial(){
    var type = GetReferenceBase("SP-23349");
    var material = GetReferenceBase("SP-21026");
    if (!type.In("%Head%", "%handle%") && material.HasValue() && !material.HasValue("Corn Bristle")) {
        Add($"AdditionalBroomDustpanMaterial⸮Made of {material.ToLower(true)}");
    }
}

// --[FEATURE #7]
// -- Additional true color

// --[FEATURE #8]
// -- Additional broom holder
void AdditionalBroomHolder(){
    var type = GetReferenceBase("SP-23349");
    if (type.HasValue("Brooms w/Dustpan") && A[6786].HasValue("broom holder")) {
        Add($"AdditionalBroomHolder⸮Broom easily snaps into and stores inside the pan");
    }
}
// --[FEATURE #9]
// -- Additional hanger hole
void AdditionalBroomHangerHole() {
    if (A[6786].HasValue("hanger hole", "hanging hole")) {
        Add($"AdditionalBroomHangerHole⸮{A[6786].Where("hanger hole", "hanging hole").Select(o => o.Value().Replace(" hole", "")).FlattenWithAnd().ToUpperFirstChar()} hole for easy storage");
    }
}
// --[FEATURE #10]
// -- Additional wall mountable
void AdditionalBroomWallMountable() {
    if (A[6786].HasValue("wall mountable")) {
        Add($"AdditionalBroomWallMountable⸮The hanging design helps prevent damage to bristles, handles, walls, and counters");
    }
}

// --[FEATURE #11]
// -- Additional molded riges
void AdditionalBroomMoldedRiges() {
    if (A[6786].HasValue("molded ridges") || DC.KSP.GetString().In("%molded ridge%")) {
        Add($"AdditionalBroomMoldedRiges⸮Molded ridges for broom and counter brush cleaning");
    }
    else if (A[6786].HasValue("molded teeth") || DC.KSP.GetString().In("%molded teeth%")) {
        Add($"AdditionalBroomMoldedRiges⸮Molded teeth to help clean bristles");
    }
}

// --[FEATURE #12]
// -- Additional Cleaning usage
void AdditionalCleaningUsage(){
    if (GetReferenceBase("SP-15361").HasValue()) {
        Add($"AdditionalCleaningUsage⸮Perfect for {GetReferenceBase("SP-15361").ToLower()}");
    }
}
// --[FEATURE #13]
// -- Additional adjustable handle
void AdditionalAdjustableHandle(){
    if (A[6786].HasValue("Adjustable handle")) {
        Add($"AdditionalAdjustableHandle⸮Adjustable handle enhances user comfort");
    }
    else if (A[6786].HasValue("Ergonomic handle")) {
        Add($"AdditionalAdjustableHandle⸮Ergonomic handle to prevent hand fatigue");
    }
}
// --[FEATURE #14]
// -- Additional elescoping handle
void AdditionalTelescopingHandle(){
    if (A[6784].HasValue("Yes") || DC.KSP.GetString().HasValue("%Telescop%handle%") || DC.MKT.GetString().HasValue("%Telescop%handle%")) {
        Add($"AdditionalTelescopingHandle⸮Telescoping handle makes it easy to use");
    }
}

// --[FEATURE #15]
// Additional Comfort Grip
void AdditionalComfortGrip(){
    if (A[6784].HasValue("comfort grip%")) {
        Add($"AdditionalComfortGrip⸮Comfort grip for added convenience");
    }
    else if (A[6786].HasValue("% grip", "% grip %")) {
        Add($"AdditionalComfortGrip⸮{A[6786].Where("% grip", "% grip %").Select(o => o).First().Value().Replace(" handle", "").ToUpperFirstChar()} for added convenience");
    }
}

// --[FEATURE #16]
// Additional Color Coded
void AdditionalColorCoded(){
    if (A[6786].HasValue("color-coded")) {
        Add($"AdditionalColorCoded⸮Color-coded feature makes it ideal for assigning it to a specific work area");
    }
}

// --[FEATURE #17]
// Additional Thread
void AdditionalBroomThread(){
    if (A[6786].HasValue("% thread")) {
        Add($"AdditionalBroomThread⸮{A[6786].Where("% thread").Select(o => o.Value()).FlattenWithAnd().ToUpperFirstChar()} for easy and secure attachment");
    }
}

// --[FEATURE #18]
// Additional wet dry
void AdditionalBroomWetDry(){
    if (A[7091].HasValue("wet") && A[7091].HasValue("dry")) {
        Add($"AdditionalBroomWetDry⸮Can be used in dry or wet conditions");
    }
}

// --[FEATURE #19]
// Additional heavy duty
void AdditionalBroomHeavyDuty(){
    if (A[6786].HasValue("heavy-duty")) {
        Add($"AdditionalBroomHeavyDuty⸮Heavy-duty for tough cleaning jobs");
    }
}

// --[FEATURE #20]
// Additional Bristle Type
void AdditionalBroomBristleType(){
    var type = GetReferenceBase("SP-23349");
    if (A[6786].HasValue("%angled bristle%") && type.HasValue("%angled%")) {
        Add($"AdditionalBroomBristleType⸮Angled bristle are great for sweeping in corners and other hard to sweep areas");
    }
    else if (A[6786].HasValue("%stiff bristle%")) {
        Add($"AdditionalBroomBristleType⸮Stiff bristles provide aggressive cleaning");
    }
    else if (A[6786].HasValue("%heat-resistant bristles%")) {
        Add($"AdditionalBroomBristleType⸮High heat resistant bristles offer long term economy");
    }
    else if (A[6786].HasValue("%stain-resistant bristles%")) {
        Add($"AdditionalBroomBristleType⸮Stain-resistant bristles for longer product life");
    }
}
// --[FEATURE #21]
// Additional brush holders
void AdditionalBroomBrushHolders(){
    var type = GetReferenceBase("SP-23349");
    if (A[6786].HasValue("%handle holders", "%brush holders") &&  A[6786].Where("%handle holders", "%brush holders").ExtractNumbers().Any()
    && A[6786].Where("%handle holders", "%brush holders").Flatten(" ").ExtractNumbers().Max() > 0
    && type.HasValue("%tool holder%")) {
        Add($"AdditionalBroomBrushHolders⸮Can hold up to {A[6786].Where("%brush holders").Flatten(" ").ExtractNumbers().Max()} items");
    }
}
// --[FEATURE #22]
// Additional wheels
void AdditionalBroomWheels(){
    var type = GetReferenceBase("SP-23349");
    if (A[6786].HasValue("wheels") ) {
        Add($"AdditionalBroomWheels⸮Wheels improve wear resistance");
    }
}
// --[FEATURE #11]
// --Warranty information


//§§1683392810982141837  end of "Brooms & Dustpans"