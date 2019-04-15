// §168339291098380002 "Fans" "Alex K."


FanTypeAndUse();
SpecialButtonsAndControls();
OscillatingFanPosition();
//Dimentions Adjustable
ColorFamily();
CordLength();
// Pack Size (If more than 1) 
AdditionalNumberOfSpeedSettings();
AdditionalMetalHousing();
AdditionalTimer();
AdditionalCarryHandle();
// warranty

// "Bullet 1" "Fan type & Use"
void FanTypeAndUse() {
    var fanTypeAndUse = "";
    var type = GetReference("SP-19036");
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
        Add($"FanTypeAndUse⸮##{fanTypeAndUse}");
    }
}

// "Bullet 2" "Special buttons and controls" 
void SpecialButtonsAndControls() {
    var specialButtonsAndControls = "";
    var controls = GetReference("SP-23829");
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
        Add($"SpecialButtonsAndControls⸮##{specialButtonsAndControls}");
    }
}

// "Bullet 4" "Oscillating/Fan position" 
void OscillatingFanPosition() {
    var oscillatingFanPosition = "";
    var oscillating = GetReference("SP-19037");
    var fanPosition = GetReference("SP-21508");
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
    var cFamily = GetReference("SP-22967");
    if (!String.IsNullOrEmpty(cFamily)) {
        colorFamily = $"Comes in {cFamily}";
    }
    if (!String.IsNullOrEmpty(colorFamily)) {
        Add($"ColorFamily⸮##{colorFamily}");
    }
}

// "Bullet 6" "Cord length (ft.)" 
void FanCordLength() {
    var cordLength = "";
    var length = GetReference("SP-21372");
    var safety = Coalesce(A[4754].HasValue("Safety Fuse Technology"));
    if (safety && !String.IsNullOrEmpty(length)) {
        cordLength = $"{length}' power cord, fused safety plug";
    }
    else if (!String.IsNullOrEmpty(length)) {
        cordLength = $"Cord Length: {length}'";
    }
    if (!String.IsNullOrEmpty(cordLength)) {
        Add($"FanCordLength⸮##{cordLength}");
    }
}

// Bullet 7 Pack Size (If more than 1) 

// "Additional Bullet" "number of speed settings" 

void AdditionalNumberOfSpeedSettings() {
    var numberOfSpeedSettings = "";
    var num = GetReference("SP-18992");
    if (String.IsNullOrEmpty(num) && !num.Equals("1")) {
        numberOfSpeedSettings = "Fan features {num} speed settings that are perfect for your comfort";
    }

    if (!String.IsNullOrEmpty(numberOfSpeedSettings)) {
        Add($"AdditionalNumberOfSpeedSettings⸮##{numberOfSpeedSettings}");
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
        Add($"AdditionalMetalHousing⸮##{metalHousing}");
    }
}


// 1111111  "can  be used for all categores" 1111111
// "Additional Bullet" "Timer" 
void AdditionalTimer() {
    var additionalTimer = "";
    var timer = Coalesce(A[4538].HasValue()) ? A[4538].FirstValue().ExtractNumbers().First() : 0;
    var units = Coalesce(A[4538].HasValue()) ? A[4538].Units.First().Name : "non"; 
    if (timer > 1  && (units.Contains("hr") || units.Equals("min"))) {
        additionalTimer = $"Included {timer} {units.Replace("hrs", "hours").Replace("hr", "hours").Replace("min", "minutes")} timer gives you the flexibility to save energy";
    }
    else if (timer == 1 && (units.Contains("hr") || units.Equals("min"))) {
        additionalTimer = $"Included {timer} {units.Replace("hr", "hour").Replace("hrs", "hour").Replace("min", "minute")} timer gives you the flexibility to save energy";
    }
    if (!String.IsNullOrEmpty(additionalTimer)) {
        Add($"AdditionalTimer⸮##{additionalTimer}");
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
        Add($"AdditionalCarryHandle⸮##{carryHandle}");
    }
}

// Compliant Standards

// Warranty

// §168339291098380002 end of "Fans"