// §§167638614736165756 -- "Post-it&reg; / Sticky Flags & Tabs" "Alex K."

FlagTabTypeAndUse();
// color famaly SP-12608
// pack size
Messaging();
FlagOrTabWidthInInches();
// Recycled Content (%)

// "Bullet 1" "Flag/Tab type & Use (Include if they come with a dispenser)"		

void FlagTabTypeAndUse() {
    var result = "";
    //Page Markers|Flags|Tabs|Writeable Flags|Sign Here Flags|Page Flags|Arrow Flags
    var flagAndTabType = R("SP-18400").HasValue() ? R("SP-18400").Replace("<NULL>", "").Text :
		R("cnet_common_SP-18400").HasValue() ? R("cnet_common_SP-18400").Replace("<NULL>", "").Text : "";
	
	if (!String.IsNullOrEmpty(flagAndTabType)) {
	    if (flagAndTabType.ToLower().Equals("writeable flags") || flagAndTabType.ToLower().Equals("page flags")) {
	        result = "Page flags feature a writable surface to edit, label, or jot notes";
	    }
	    else if (flagAndTabType.ToLower().Equals("arrow flags")) {
	        result = "Arrow flags make it simple to direct attention to precisely what is important on a page";
	    }
	    else if (flagAndTabType.ToLower().Equals("tabs")) {
	        result = "Tabs create necessary order by helping you to organize and label";
	    }
	    else if (flagAndTabType.ToLower().Equals("page markers")) {
	        result = "Page markers are perfect to help indicate important information to save time";
	    }
	    else if (flagAndTabType.ToLower().Equals("sign here flags")) {
	        result = "Sign flags make it simple to direct attention to precisely what is important on a page";
	    }
	    else if (flagAndTabType.ToLower().Equals("flags"))  {
	        result = "Flags make it simple to direct attention to precisely what is important on a page";
	    }
	}
	if (String.IsNullOrEmpty(result)) {
	    Add($"FlagTabTypeAndUse⸮{result}");
	}
}

// "Bullet 2" "color famaly SP-12608"	


// "Bullet 3" "pack size"	


// "Bullet 4 "Messaging (If Applicable)"
void Messaging() {
    var result = "";
    var prePrinted = A[6037];
    //Page Markers|Flags|Tabs|Writeable Flags|Sign Here Flags|Page Flags|Arrow Flags
    var flagAndTabType = R("SP-18400").HasValue() ? R("SP-18400").Replace("<NULL>", "").Text : R("cnet_common_SP-18400").HasValue() ? R("cnet_common_SP-18400").Replace("<NULL>", "").Text : "";
	
	if (prePrinted.HasValue("%pre%printed%") && !String.IsNullOrEmpty(flagAndTabType)) {
	    result = flagAndTabType + " with pre-printed QR codes easily create necessary order";
	}
	else if (prePrinted.HasValue("%pre%printed%") && !String.IsNullOrEmpty(flagAndTabType)) {
	    result = flagAndTabType + " with pre-printed " + prePrinted.Where("%pre%printed%").First().Value().RegexReplace("(.+\")(.+)(\".+)", "$2") + " easily create necessary order";
	}
	if (!String.IsNullOrEmpty(result)) {
	    Add($"Messaging⸮{result}");
	}
}



// "Bullet 5" "Flag or Tab Width in Inches"	

void FlagOrTabWidthInInches() {
    var result = "";
    var format = A[6018];
    var flagAndTabType = R("SP-18400").HasValue() ? R("SP-18400").Replace("<NULL>", "").Text : R("cnet_common_SP-18400").HasValue() ? R("cnet_common_SP-18400").Replace("<NULL>", "").Text : "";
    if (format.HasValue("%width") && !String.IsNullOrEmpty(flagAndTabType)) {
        var tmp = flagAndTabType[0].ToString().ToUpper() + flagAndTabType.Substring(1).ToLower();
        var tmp2 = format.Values.First().Value().ToString().Replace(" in width", "\"");
        result = $"{tmp} flags measure {tmp2}";
    }
    else if (format.HasValue() && !String.IsNullOrEmpty(flagAndTabType)) {
        if (format.Values.First().Value().ExtractNumbers().Any()) {
            var tmp = flagAndTabType[0].ToString().ToUpper() + flagAndTabType.Substring(1).ToLower();
            var tmp2 = format.Values.First().Value().ExtractNumbers().Min();
            result = $"{tmp} measure {tmp2}\"W";
        }
    }
    if (!String.IsNullOrEmpty(result)) {
        Add($"FlagOrTabWidthInInches⸮{result}");
    }
}

Greener or Recycled Content (%)

