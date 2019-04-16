//§§1221973163092  "Card Files, Cases & Holders" "Alex K."

CardFileCaseAndHolderTypeAndUse();


// --[FEATURE #1]
// --Card file, case & holder type & Use
void CardFileCaseAndHolderTypeAndUse() {
    var result = "";
    //Covered Files|Cases|Books|Dividers|Binders|Index Card File Box|Pages|Wall File|Card File Drawers|Rotary Files|Rotary Cards|Business Card File|Holders|Open Files
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
	        result = $"{type.ToLower().ToUpperFirstChar()} keeps your cards close at hand";
	    }
	}
	if (!String.IsNullOrEmpty(result)) {
	    Add($"CardFileCaseAndHolderTypeAndUse⸮{result}");
	}
}

// --[FEATURE #2]
// --True color & Card file/holder material

// --[FEATURE #3]
// --Card size & Capacity


// --[FEATURE #4]
// --Overall Dimensions of Container

// --[FEATURE #5]
// --Index type (if applicable)

// --[FEATURE #6]
// --Pack size

// --[FEATURE #7]
// --

// --[FEATURE #8]
// --

// --[FEATURE #9]
// --

// --[FEATURE #10]
// --

// --[FEATURE #11]
// --

// --[FEATURE #12]
// --

//1221973163092 end of "Card Files, Cases & Holders" §§