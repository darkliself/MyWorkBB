//§§1114953158890 "Highlighters" "Alex K."

HighlighteTtypeAndUse();
HighlighterTipStyle();
HighlighterInkColor();
QuickDrying();
BarrelTransparency();
HighlightersPocketClip();
HighlightersGrip();
// PackSize
Fluorescent();
HighlightersToxicOdorFree();
HighlightersSafetySeal();
HighlightersCapOff();
// standards

// --[FEATURE #1]
// -- Highlighter type & Use
void HighlighteTtypeAndUse(){
    var type = GetReferenceBase("SP-16603");
    if (type.HasValue("Liquid")) {
        Add($"HighlighteTtypeAndUse⸮Liquid ink highlighters allow marked passages to easily stand out");
    }
    else if (type.HasValue("Tank")) {
        Add($"HighlighteTtypeAndUse⸮Highlighters feature a tank-style barrel with long-lasting ink supply");
    }
    else if (type.HasValue("Stick")) {
        Add($"HighlighteTtypeAndUse⸮Stick conforms to all handwriting styles");
    }
    else if (type.HasValue("Retractable")) {
        Add($"HighlighteTtypeAndUse⸮Retractable highlighter eliminates the need for capping and uncapping");
    }
    else if (type.HasValue("Clear View", "Mini", "Tape")) {
        Add($"HighlighteTtypeAndUse⸮{type.ToLower().ToUpperFirstChar()} highlighter is a dream to use - just highlight");
    }
    else if (type.HasValue("Blade")) {
        Add($"HighlighteTtypeAndUse⸮Blade-style tip delivers precise highlighting with the ability to select three different line widths");
    }
    else if (type.HasValue("Pink Ribbon")) {
        Add($"HighlighteTtypeAndUse⸮Pink ribbon highlighter");
    }
    else if (type.HasValue("Erasable")) {
        Add($"HighlighteTtypeAndUse⸮Erasable highlighter erases clean and highlights brightly");
    }
    else if (type.HasValue()) {
        Add($"HighlighteTtypeAndUse⸮{type.ToLower().ToUpperFirstChar()} highlighter");
    }
}
// --[FEATURE #2]
// -- Highlighter tip style
void HighlighterTipStyle(){
    var inkColor = GetReferenceBase("SP-16334");
    var tipStyle = GetReferenceBase("SP-16332");
    if (tipStyle.HasValue("chisel") && inkColor.HasValue("Yellow", "Orange", "Blue")) {
        Add($"HighlighterTipStyle⸮Chisel tip for precise marking");
    }
    else if (tipStyle.HasValue("chisel") && inkColor.HasValue("Assorted")) {
        Add($"HighlighterTipStyle⸮Versatile chisel tip allows you to precisely highlight both wide and narrow lines of text");
    }
    else if (tipStyle.HasValue("chisel") &&  inkColor.HasValue("Green", "Pink", "Purple")) {
        Add($"HighlighterTipStyle⸮Versatile chisel tip easily highlights or underlines text");
    }
    else if (tipStyle.HasValue("bullet")) {
        Add($"HighlighterTipStyle⸮Bullet-tip highlighter is ideal for highlighting, writing, doodling and drawing");
    }
    else if (tipStyle.HasValue("gel")) {
        Add($"HighlighterTipStyle⸮Gel highlighter is ideal for highlighting, writing, doodling and drawing");
    }
   else if (tipStyle.HasValue("Micro Chisel")) {
        Add($"HighlighterTipStyle⸮Highlighter features a micro chisel tip for precise and clean underlining or highlighting");
    }
}
// --[FEATURE #3]
// -- Highlighter ink color
void HighlighterInkColor(){
    var color = GetReferenceBase("SP-22967");
    if (color.HasValue("Assorted")) {
        Add($"HighlighterTipStyle⸮Highlighters come in assorted colors and are ideal for marking important documents");
    }
    else if (color.HasValue()) {
        Add($"HighlighterTipStyle⸮{color.ToLower().ToUpperFirstChar()} ink");
    }
}
// --[FEATURE #4]
// -- Quick drying (If Applicable)
void QuickDrying(){
    if (GetReferenceBase("SP-21016").HasValue("Yes")) {
        Add($"QuickDrying⸮Smooth, quick-drying ink for clean highlighting");
    }
    else if (A[5996].HasValue("%Smear Safe%")) {
        Add($"QuickDrying⸮Smear ##Safe ink formula resists smearing");
    }
    else if (A[5996].HasValue("%smear guard%")) {
        Add($"QuickDrying⸮Features ##Smear ##Guard ink technology which is specially formulated to resist smearing of many pen and marker inks");
    }
}
// --[FEATURE #5]
// -- Barrel transparency
void BarrelTransparency(){
    // Transparent|Translucent|Solid
    var transparency = GetReferenceBase("SP-350785");
    if (transparency.HasValue("Translucent") && !REQ.GetVariable("TX_UOM").HasValue("Each")) {
        Add($"barrelTransparency⸮Highlighters features a translucent barrel for ink supply monitoring");
    }
    else if (transparency.HasValue("Translucent") && REQ.GetVariable("TX_UOM").HasValue("Each")) {
        Add($"barrelTransparency⸮Highlighter features a translucent barrel for ink supply monitoring");
    }
    else if (transparency.HasValue("Transparent") && !REQ.GetVariable("TX_UOM").HasValue("Each")) {
        Add($"barrelTransparency⸮Highlighters feature a clear barrel design to easily monitor the ink supply");
    }
     else if (transparency.HasValue("Transparent") && REQ.GetVariable("TX_UOM").HasValue("Each")) {
        Add($"barrelTransparency⸮Highlighter features a clear barrel design to easily monitor the ink supply");
    }
}
// --[FEATURE #6]
// -- Pocket clip (If Applicable)
void HighlightersPocketClip(){
    if (A[5994].HasValue("Yes") && A[5995].HasValue("Yes")) {
        Add($"HighlightersPocketClip⸮Pocket clip-on cap keeps marker in easy reach");
    }
    else if (A[5995].HasValue("Yes")) {
        Add($"HighlightersPocketClip⸮Built-in pocket clip for convenience");
    }
}
// --[FEATURE #7]
// -- Grip (If Applicable)
void HighlightersGrip(){
    var grip = GetReferenceBase("SP-350752");
    
    if (grip.HasValue("Yes") && !A[5996].HasValue("rubber grip zone") && !REQ.GetVariable("TX_UOM").HasValue("Each")) {
        Add($"HighlightersGrip⸮Gripped highlighters are comfortable to use for extended periods of time");
    }
    else if (grip.HasValue("Yes") && !A[5996].HasValue("rubber grip zone") && REQ.GetVariable("TX_UOM").HasValue("Each")) {
        Add($"HighlightersGrip⸮Gripped highlighter is comfortable to use for extended periods of time");
    }
    else if (grip.HasValue("Yes") && A[5996].HasValue("rubber grip zone") && REQ.GetVariable("TX_UOM").HasValue("Each")) {
        Add($"HighlightersGrip⸮This highlighter feature a comfortable and ergonomic rubber grip for easy highlighting on the go");
    }
    else if (grip.HasValue("Yes") && A[5996].HasValue("rubber grip zone") && REQ.GetVariable("TX_UOM").HasValue("Each")) {
        Add($"HighlightersGrip⸮These highlighters feature a comfortable and ergonomic rubber grip for easy highlighting on the go");
    }
}
// --[FEATURE #8]
// -- Highlighter pack size

// --[FEATURE #9]
// -- Additional fluorescent
void Fluorescent(){    
    if (A[5996].HasValue("%fluorescent%") || A[5977].HasValue("%fluorescent%")) {
        Add($"Fluorescent⸮Bright, see-through fluorescent inks");
    }
}
// --[FEATURE #10]
// -- Additional Toxic- Oder- free
void HighlightersToxicOdorFree(){
    
    if (A[5996].HasValue("%non-toxic%") && A[5996].HasValue("%odor-free%")) {
        Add($"HighlightersToxicOdorFree⸮Odor-free ink is non-toxic");
    }
    else  if (A[5996].HasValue("%non-toxic%")) {
        Add($"HighlightersToxicOdorFree⸮The non-toxic ink is ideal for marking documents");
    }
}
// --[FEATURE #11]
// -- Additional safety seal
void HighlightersSafetySeal(){
    if (A[5996].HasValue("%safety seal%")) {
        Add($"HighlightersSafetySeal⸮Safety Seal prevents leaks and dry-out so the highlighter stays fresh and ready to use");
    }
}
// --[FEATURE #12]
// -- Additional up to 1 week cap off"

void HighlightersCapOff(){
    if (A[5996].HasValue("up to 1 week cap off")) {
        Add($"HighlightersCapOff⸮You can even leave the cap off for up to a week without drying it out");
    }
}

//§§1114953158890 end of "Highlighters"